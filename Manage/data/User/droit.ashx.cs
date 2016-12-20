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
    /// droit 的摘要说明
    /// </summary>
    public class droit : BaseHandle
    {
        private HT_MenuBO menuBO = new HT_MenuBO();
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
                string title = Utility.RF("title");
                int menuid = Utility.FNumeric("menuid");

                Expression<Func<HT_Menu, bool>> expre = PredicateExtensionses.True<HT_Menu>();
                if (!String.IsNullOrEmpty(title))
                {
                    expre = expre.And(p => p.Title.Contains(title));
                }
                if (menuid > 0)
                {
                    expre = expre.And(p => p.Pid == menuid);
                }
                Expression<Func<HT_Menu, int>> orderBy = p => p.SortId.Value;
                defaultSort = "asc";
                re = GetListByObject<HT_Menu>(expre, menuBO, orderBy);
            }
            catch (Exception ex)
            {
                re = JsonResult.FailResult(MsgShowConfig.Exception);
                LogService.LogDebug(ex);
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
                Expression<Func<HT_Menu, bool>> where = PredicateExtensionses.True<HT_Menu>();
                where = where.AndAlso(p => list.Contains(p.id.ToString()));
                if (menuBO.DeleteByWhere(where) > 0)
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