using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Framework;
using Framework.Log;
using Framework.Model;
using WebControllers.Handle;
using YYT.BLL;
using YYT.Model;
using System.Linq.Expressions;
using Framework.EF;

namespace Web.Manage.data
{
    /// <summary>
    /// menus 的摘要说明
    /// </summary>
    public class menus : BaseHandle
    {
        private HT_MenuBO menuBO = new HT_MenuBO();
        private HT_AccountBO accountBo = new HT_AccountBO();
        private HT_UserGroupBO userGroupBO = new HT_UserGroupBO();
        public override JsonResult HandleProcess()
        {
            JsonResult re = new JsonResult();
            switch (action)
            {
                case "top":
                    re = GetTopMenu();
                    break;
                case "leftmenu":
                    re = GetLeftMenu();
                    break;
                case "getUserNameList":
                    re = GetUserNameList();
                    break;
                case "changeUserDroit":
                    re = ChangeUserDroit();
                    break;
            }
            return re;
        }

        /// <summary>
        /// 获取主菜单
        /// </summary>
        /// <returns></returns>
        private JsonResult GetTopMenu()
        {
            JsonResult re = new JsonResult();
            try
            {
                Expression<Func<HT_Menu, bool>> where = PredicateExtensionses.True<HT_Menu>();
                where = where.AndAlso(p => p.Pid == 0 && p.isMenu == (int)HT_MenuMenu.MainMenu && p.status == (int)StatusEnmu.Normal);
                List<string> listDroit = new List<string>(manageUserModel.UserDroit.Split(','));
                where = where.AndAlso(p => listDroit.Contains(p.id.ToString()));
                Expression<Func<HT_Menu, int>> orderBy = p => p.SortId.Value;
                defaultSort = "asc";
                List<HT_Menu> list = menuBO.FindAll<int>(where, orderBy, defaultSort);

                re = JsonResult.SuccessResult(list);
            }
            catch (Exception ex)
            {
                re = JsonResult.FailResult(MsgShowConfig.Exception);
                LogService.logDebug(ex);
            }
            return re;
        }
        /// <summary>
        /// 账号信息
        /// </summary>
        /// <returns></returns>
        private JsonResult GetUserNameList()
        {
            JsonResult re = new JsonResult();
            List<HT_Account> accountList = null;
            try
            {
                if (manageUserModel.LoginGroupId == jumpDroitGroupId && jumpDroitGroupId > 0)
                {
                    Expression<Func<HT_Account, bool>> expre = PredicateExtensionses.True<HT_Account>();
                    expre = expre.AndAlso(p => p.status == (int)StatusEnmu.Normal);
                    accountList = accountBo.FindAll<int>(expre, p => p.id, defaultSort);
                }
                re = JsonResult.SuccessResult(accountList);
            }
            catch (Exception ex)
            {
                re = JsonResult.FailResult(MsgShowConfig.Exception);
                LogService.logDebug(ex);
            }
            return re;
        }

        /// <summary>
        /// 切换会员
        /// </summary>
        /// <returns></returns>
        private JsonResult ChangeUserDroit()
        {
            JsonResult re = new JsonResult();
            try
            {
                int userId = Utility.FNumeric("userid");
                int currUserid = Utility.FNumeric("currUserid");
                if (currUserid < 1 || userId < 1)
                    return JsonResult.FailResult(MsgShowConfig.ParmNotEmpty);

                if (manageUserModel.LoginGroupId != jumpDroitGroupId || jumpDroitGroupId == 0)
                    return JsonResult.FailResult(MsgShowConfig.IllegalOperation);

                if (currUserid != manageUserModel.UserId)
                    return JsonResult.FailResult(MsgShowConfig.IllegalOperation);

                HT_Account accountModel = accountBo.Find(userId);
                if (accountModel == null)
                    return JsonResult.FailResult(MsgShowConfig.ObjectIsEmpty);
                HT_UserGroup adminGroupModel = userGroupBO.Find(accountModel.groupid.Value);
                if (adminGroupModel == null)
                    return JsonResult.FailResult(MsgShowConfig.ObjectIsEmpty);

                cookieHandle.SetManagerUser(accountModel.id, accountModel.username, adminGroupModel.Droit, adminGroupModel.id, manageUserModel.LoginGroupId);
                re = JsonResult.SuccessResult(MsgShowConfig.Success);
            }
            catch (Exception ex)
            {
                re = JsonResult.FailResult(MsgShowConfig.Exception);
                LogService.logDebug(ex);
            }
            return re;
        }


        /// <summary>
        /// 获取二级菜单
        /// </summary>
        /// <returns></returns>
        private JsonResult GetLeftMenu()
        {
            JsonResult re = new JsonResult();
            try
            {
                int pid = Utility.FNumeric("pid");
                Expression<Func<HT_Menu, bool>> where = PredicateExtensionses.True<HT_Menu>();
                where = where.And(p => p.Pid == pid && p.isMenu == (int)HT_MenuMenu.ListMenu && p.status == (int)StatusEnmu.Normal);
                List<string> listDroit = new List<string>(manageUserModel.UserDroit.Split(','));
                where = where.And(p => listDroit.Contains(p.id.ToString()));
                Expression<Func<HT_Menu, int>> orderBy = p => p.SortId.Value;
                defaultSort = "asc";
                List<HT_Menu> list = menuBO.FindAll<int>(where, orderBy, defaultSort);

                re = JsonResult.SuccessResult(list);
            }
            catch (Exception ex)
            {
                re = JsonResult.FailResult(MsgShowConfig.Exception);
                LogService.logDebug(ex);
            }
            return re;
        }
    }
}