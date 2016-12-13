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
                where = where.AndAlso(p => p.Pid == 0 && p.isMenu == (int)HT_MenuMenu.MainMenu && p.status == (int)HT_MenuStatus.Normal);
                List<string> listDroit = new List<string>(manageUserModel.UserDroit.Split(','));
                where = where.AndAlso(p => listDroit.Contains(p.isMenu.Value.ToString()));
                Expression<Func<HT_Menu, int>> orderBy = p => p.SortId;
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
                where = where.AndAlso(p => p.Pid == pid && p.isMenu == (int)HT_MenuMenu.ListMenu && p.status == (int)HT_MenuStatus.Normal);
                List<string> listDroit = new List<string>(manageUserModel.UserDroit.Split(','));
                where = where.AndAlso(p => listDroit.Contains(p.isMenu.Value.ToString()));
                Expression<Func<HT_Menu, int>> orderBy = p => p.SortId;
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