using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using Framework;
using Framework.EF;
using Framework.Log;
using Framework.Model;
using WebControllers.Handle;
using YYT.BLL;
using YYT.Model;

namespace Manage.data.Activity
{
    /// <summary>
    /// LotteryJackpotList 的摘要说明
    /// </summary>
    public class LotteryJackpotList : BaseHandle
    {
        private Luck_ActivityPrizeBO luckActivityPrizeBo = new Luck_ActivityPrizeBO();

        public override JsonResult HandleProcess()
        {
            JsonResult re = new JsonResult();
            switch (action)
            {
                case "getList":
                    re = GetList();
                    break;
                case "getPrizeName":
                    re = GetPrizeName();
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

                int activity_id = Utility.FNumeric("activity_id");
                int award_id = Utility.FNumeric("award_id");
                string out_id = Utility.RF("out_id");
                int win_status = Utility.FNumeric("status");

                StringBuilder strWhere = new StringBuilder(255);
                strWhere.Append(" 1=1 ");
                if (!String.IsNullOrWhiteSpace(out_id))
                    strWhere.Append(String.Format(" and a.out_id ={0}", out_id));
                if (award_id > 0)
                    strWhere.Append(String.Format(" and a.PrizeId ={0}", award_id));
                if (activity_id > 0)
                    strWhere.Append(String.Format(" and a.ActivityId ={0}", activity_id));
                if (win_status == 0 || win_status == 1)
                    strWhere.Append(String.Format(" and a.Status ={0}", win_status));

                clspage.PageSize = pageSize;
                clspage.Table = "  Luck_ActivityJackpot a inner join Luck_ActivityPrize b on a.PrizeId=b.id inner join Luck_Activity c on c.id=a.ActivityId left join YYT_Member d on d.out_id=a.out_id";
                clspage.Order = " a.id desc";
                clspage.columns = " a.id,b.name as prizeName,c.name as activityName,a.out_id,a.data_type,a.Status,a.createtime,a.updatetime,a.PrizeId,a.ActivityId,d.Mobile,d.addr";
                clspage.where = strWhere.ToString();

                re = GetListBySql(clspage);
            }
            catch (Exception ex)
            {
                re = JsonResult.FailResult(MsgShowConfig.Exception);
                LogService.logDebug(ex);
            }
            return re;
        }

        /// <summary>
        /// 获取奖品数据
        /// </summary>
        /// <returns></returns>
        protected JsonResult GetPrizeName()
        {
            JsonResult re = new JsonResult();
            try
            {
                int activity_id = Utility.FNumeric("activity_id");

                if (activity_id < 1)
                    return JsonResult.FailResult(MsgShowConfig.ParmNotEmpty);

                Expression<Func<Luck_ActivityPrize, bool>> expre = PredicateExtensionses.True<Luck_ActivityPrize>();
                expre = expre.AndAlso(p => p.sortid == activity_id);
                Expression<Func<Luck_ActivityPrize, int>> orderBy = p => p.id;
                re = GetListByObject<Luck_ActivityPrize>(expre, luckActivityPrizeBo, orderBy);
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