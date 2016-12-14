using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Framework.Log;
using Framework.Model;
using System.Linq.Expressions;
using Framework.EF;
using Framework;
using System.Data;
using WebControllers.Handle;
using YYT.BLL;
using YYT.Model;
using YYT.BLL.Common;
using Framework.Handle;

namespace Web.Manage.data.Activity.Lottery
{
    /// <summary>
    /// LotteryPrize 的摘要说明
    /// </summary>
    public class LotteryPrize : BaseHandle
    {
        private Luck_ActivityPrizeBO luckActivityPrizeBo = new Luck_ActivityPrizeBO();
        private Luck_ActivityJackpotBO luckActivityJackpotBo = new Luck_ActivityJackpotBO();
        private CommonPage clspage = new CommonPage();
        private SqlBO sqlBo = new SqlBO();

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
                case "createJackpot":
                    re = CreateJackpot();
                    break;
            }
            return re;
        }

        /// <summary>
        /// 奖品列表
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
                string strWhere = "";
                int sortId = Utility.FNumeric("id");
                if (sortId > 0)
                    strWhere += String.Format(" sortid = {0}", sortId);

                clspage.PageSize = pageSize;
                clspage.Table = "  Luck_ActivityPrize ";
                clspage.Order = " id ";
                clspage.columns = " *,NotReceiveTotal=(select COUNT(id) from Luck_ActivityJackpot where PrizeId= Luck_ActivityPrize.id and Status=" + (int)Luck_ActivityJackpotStatus.NotDraw + ")";
                clspage.where = strWhere;

                DataTable dt = clspage.getDataByPage(pageIndex, out totalRecord);
                List<Luck_ActivityJackpot> lotteryPrizeList = new FromToObj().ConvertToModel<Luck_ActivityJackpot>(dt);
                int totalPage = GetTotalPage(totalRecord, pageSize);

                JqGridPagingModel<Luck_ActivityJackpot> jqGridPagingModel = new JqGridPagingModel<Luck_ActivityJackpot>(pageIndex, totalPage, totalRecord, lotteryPrizeList);
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

                //判断奖池记录是否存在
                Expression<Func<Luck_ActivityJackpot, bool>> jacketWhere = PredicateExtensionses.True<Luck_ActivityJackpot>();
                jacketWhere = jacketWhere.AndAlso(p => idList.Contains(p.PrizeId.ToString()));
                Expression<Func<Luck_ActivityJackpot, int>> orderBy = p => p.id;
                List<Luck_ActivityJackpot> luckActivityJackpotList = luckActivityJackpotBo.FindAll<int>(jacketWhere, orderBy, defaultSort);
                if (luckActivityJackpotList != null && luckActivityJackpotList.Count > 0)
                {
                    return JsonResult.FailResult("请先删除奖池记录");
                }

                Expression<Func<Luck_ActivityPrize, bool>> where = PredicateExtensionses.True<Luck_ActivityPrize>();
                where = where.AndAlso(p => idList.Contains(p.Id.ToString()));
                if (luckActivityPrizeBo.DeleteByWhere(where) > 0)
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

        /// <summary>
        /// 创建奖池记录
        /// </summary>
        /// <returns></returns>
        private JsonResult CreateJackpot()
        {
            JsonResult re = new JsonResult();
            try
            {
                int id = Utility.FNumeric("id");
                if (id < 1)
                    return JsonResult.FailResult(MsgShowConfig.ParmNotEmpty);

                Luck_ActivityPrize luckActivityPrizeModel = luckActivityPrizeBo.Find(id);
                if (luckActivityPrizeModel == null)
                    return JsonResult.FailResult(MsgShowConfig.ObjectIsEmpty);

                Expression<Func<Luck_ActivityJackpot, bool>> where = PredicateExtensionses.True<Luck_ActivityJackpot>();
                where = where.AndAlso(p => p.PrizeId == id);
                List<Luck_ActivityJackpot> actJackpotList = luckActivityJackpotBo.FindAll<int>(where, null, defaultSort);
                if (actJackpotList == null)
                    return JsonResult.FailResult(MsgShowConfig.ObjectIsEmpty);

                if (luckActivityPrizeModel.num - actJackpotList.Count > 0)
                {
                    List<CommandInfo> list = new List<CommandInfo>();
                    Luck_ActivityJackpot actJackpotModel = new Luck_ActivityJackpot();
                    actJackpotModel.channelUserId = manageUserModel.UserId;
                    actJackpotModel.ActivityId = luckActivityPrizeModel.Id;
                    actJackpotModel.PrizeId = id;
                    actJackpotModel.Status = (int)Luck_ActivityJackpotStatus.NotDraw;
                    actJackpotModel.data_type = "";
                    actJackpotModel.out_id = "";
                    actJackpotModel.Ip = "";
                    actJackpotModel.Createtime = DateTime.Now;
                    actJackpotModel.Updatetime = DateTime.Parse("1900-01-01");
                    actJackpotModel.UpdateAddtime = DateTime.Parse("1900-01-01");
                    
                    for (int i = actJackpotList.Count; i < luckActivityPrizeModel.num; i++)
                    {
                        list.Add(luckActivityJackpotBo.AddSql(actJackpotModel));
                    }

                    if (sqlBo.ExecuteSqlTran(list) > 0)
                        re = JsonResult.SuccessResult("生成奖池成功");
                    else
                        re = JsonResult.FailResult("生成奖池失败");
                }
                else
                {
                    return JsonResult.FailResult("无需生成奖池");
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