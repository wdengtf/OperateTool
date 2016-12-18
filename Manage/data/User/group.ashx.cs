using System;
using System.Collections.Generic;
using Framework;
using Framework.EF;
using Framework.Model;
using WebControllers.Handle;
using YYT.BLL;
using Framework.Log;
using YYT.Model;
using System.Linq.Expressions;

namespace Web.Manage.data.User
{
    /// <summary>
    /// group 的摘要说明
    /// </summary>
    public class group : BaseHandle
    {
        private HT_UserGroupBO userGroupBO = new HT_UserGroupBO();
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
        /// 后台组列表
        /// </summary>
        /// <returns></returns>
        private JsonResult GetList()
        {
            JsonResult re = new JsonResult();
            try
            {
                Expression<Func<HT_UserGroup, bool>> expre = PredicateExtensionses.True<HT_UserGroup>();
                Expression<Func<HT_UserGroup, int>> orderBy = p => p.id;
                re = GetListByObject<HT_UserGroup>(expre, userGroupBO, orderBy);
            }
            catch (Exception ex)
            {
                re = JsonResult.FailResult(MsgShowConfig.Exception);
                LogService.LogDebug(ex);
            }
            return re;
        }

        /// <summary>
        /// 删除Group数据
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
                Expression<Func<HT_UserGroup, bool>> where = PredicateExtensionses.True<HT_UserGroup>();
                where = where.AndAlso(p => list.Contains(p.id.ToString()));
                if (userGroupBO.DeleteByWhere(where) > 0)
                {
                    re = JsonResult.SuccessResult(MsgShowConfig.Success);
                }
            }
            catch (Exception ex)
            {
                re = JsonResult.FailResult(MsgShowConfig.Exception);
                LogService.LogDebug(ex);
            }
            return re;
        }

    }
}