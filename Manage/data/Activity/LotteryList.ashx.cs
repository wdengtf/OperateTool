﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Framework.Model;
using System.Linq.Expressions;
using Framework.EF;
using Framework;
using WebControllers.Handle;
using Framework.Log;
using YYT.BLL;
using YYT.Model;

namespace Web.Manage.data.Activity
{
    /// <summary>
    /// LotteryList 的摘要说明
    /// </summary>
    public class LotteryList : BaseHandle
    {
        private Luck_ActivityBO luckActivityBo = new Luck_ActivityBO();
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
        /// 抽奖列表
        /// </summary>
        /// <returns></returns>
        private JsonResult GetList()
        {
            JsonResult re = new JsonResult();
            try
            {
                int pageIndex = Utility.FNumeric("page");
                int pageSize = Utility.FNumeric("rows") == 0 ? 10 : Utility.FNumeric("rows");
                int totalRecord = 0;
                string sortName = Utility.RF("sortName");
                string beginTime = Utility.RF("beginTime");
                string endTime = Utility.RF("endTime");

                Expression<Func<Luck_Activity, bool>> expre = PredicateExtensionses.True<Luck_Activity>();
                if (!String.IsNullOrEmpty(sortName))
                {
                    expre = expre.AndAlso(p => p.Name.Contains(sortName));
                }
                if (!String.IsNullOrEmpty(beginTime))
                {
                    DateTime beginDate = DateTime.Parse(beginTime);
                    expre = expre.AndAlso(p => p.Startdate >= beginDate);
                }
                if (!String.IsNullOrEmpty(endTime))
                {
                    DateTime endDate = DateTime.Parse(endTime).AddDays(1);
                    expre = expre.AndAlso(p => p.Startdate < endDate);
                }

                List<Luck_Activity> list = luckActivityBo.FindAllByPage<int>(expre, null, defaultSort, pageIndex, pageSize, out totalRecord);

                int totalPage = GetTotalPage(totalRecord, pageSize);
                JqGridPagingModel<Luck_Activity> jqGridPagingModel = new JqGridPagingModel<Luck_Activity>(pageIndex, totalPage, totalRecord, list);
                re = JsonResult.SuccessResult(jqGridPagingModel);
            }
            catch (Exception ex)
            {
                re = JsonResult.FailResult(MsgShowConfig.Exception);
                LogService.logDebug(ex);
            }
            return re;
        }

        /// <summary>
        ///  删除数据
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

                List<string> idList = new List<string>(idStr.Split(','));
                Expression<Func<Luck_Activity, bool>> where = PredicateExtensionses.True<Luck_Activity>();
                where = where.AndAlso(p => idList.Contains(p.Id.ToString()));
                if (luckActivityBo.DeleteByWhere(where) > 0)
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