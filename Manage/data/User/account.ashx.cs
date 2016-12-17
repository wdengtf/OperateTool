using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Framework;
using Framework.EF;
using Framework.Log;
using Framework.Model;
using WebControllers.Handle;
using YYT.BLL;
using YYT.Model;

namespace Web.Manage.data.User
{
    /// <summary>
    /// account 的摘要说明
    /// </summary>
    public class account : BaseHandle
    {
        private HT_AccountBO accountBO = new HT_AccountBO();

        public override JsonResult HandleProcess()
        {
            JsonResult re = new JsonResult();
            switch (action)
            {
                case "getList":
                    re = GetList();
                    break;
                case "delData":
                    re = DelData();
                    break;
            }
            return re;
        }
        /// <summary>
        /// 后台权限列表
        /// </summary>
        /// <returns></returns>
        private JsonResult GetList()
        {
            JsonResult re = new JsonResult();
            try
            {
                string userName = Utility.RF("userName");

                Expression<Func<HT_Account, bool>> expre = PredicateExtensionses.True<HT_Account>();
                if (manageUserModel.GroupId != jumpDroitGroupId || jumpDroitGroupId == 0)
                    expre = expre.AndAlso(p => p.groupid == manageUserModel.UserId);

                if (!String.IsNullOrEmpty(userName))
                {
                    expre = expre.AndAlso(p => p.username.Contains(userName));
                }

                Expression<Func<HT_Account, int>> orderBy = p => p.id;
                re = GetListByObject<HT_Account>(expre, accountBO, orderBy);
            }
            catch (Exception ex)
            {
                re = JsonResult.FailResult(MsgShowConfig.Exception);
                LogService.logDebug(ex);
            }
            return re;
        }

        /// <summary>
        ///  删除Droit数据
        /// </summary>
        /// <returns></returns>
        private JsonResult DelData()
        {
            JsonResult re = new JsonResult();
            try
            {
                string idStr = Utility.RF("id");
                if (String.IsNullOrEmpty(idStr))
                {
                    return JsonResult.FailResult(MsgShowConfig.ParmNotEmpty);
                }

                List<string> list = new List<string>(idStr.Split(','));
                Expression<Func<HT_Account, bool>> where = PredicateExtensionses.True<HT_Account>();
                where = where.AndAlso(p => list.Contains(p.id.ToString()));
                if (accountBO.DeleteByWhere(where) > 0)
                {
                    re = JsonResult.SuccessResult(MsgShowConfig.Success);
                }
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