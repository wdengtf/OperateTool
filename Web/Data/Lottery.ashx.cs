using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebControllers.Member;
using Framework.Model;
using Framework.Log;
using YYT.Model;
using YYT.Api;
using YYT.Api.Model;
using LuckDraw.Model;
using Framework;

namespace Web.Data
{
    /// <summary>
    /// Lottery 的摘要说明
    /// </summary>
    public class Lottery : BaseHandle
    {
        private int actitityId = 3;
        public override JsonResult HandleProcess()
        {
            JsonResult re = new JsonResult();
            switch (action)
            {
                case "getLotteryActivity":
                    re = GetLotteryActivity();
                    break;
                //case "getLotteryPrize":
                //    re = GetLotteryPrize();
                //    break;
                case "memberBindLottery":
                    re = MemberBindLottery();
                    break;
            }
            return re;
        }

        /// <summary>
        /// 获取抽奖记录
        /// </summary>
        /// <returns></returns>
        private JsonResult GetLotteryActivity()
        {
            JsonResult re = new JsonResult();
            try
            {
                ReqLotteryActivityModel req = new ReqLotteryActivityModel()
                {
                    activity_id = actitityId,
                    data_type = memberBaseModel.data_type,
                    out_id = memberBaseModel.out_id,
                    mobile = memberBaseModel.mobile,
                    nickname = memberBaseModel.nickname
                };
                IOperation<ReqLotteryActivityModel> oper = ApiFactory.GetLotteryActivity();
                re = APICall.MainExcute(req, oper);
            }
            catch (Exception ex)
            {
                re = JsonResult.FailResult(MsgShowConfig.Exception);
                LogService.LogDebug(ex);
            }
            return re;
        }

        /// <summary>
        /// 显示中奖奖品
        /// </summary>
        /// <returns></returns>
        //private JsonResult GetLotteryPrize()
        //{
        //    JsonResult re = new JsonResult();
        //    try
        //    {
        //        List<int> actitityIdList = new List<int>() { actitityId };
        //        List<WinRecordModel> winRecordList = lotteryCall.GetLotteryPrize(memberBaseModel, actitityIdList);
        //        if (lotteryCall.GetResultState())
        //        {
        //            re = JsonResult.SuccessResult(MsgShowConfig.Success);
        //        }
        //        else
        //        {
        //            re = JsonResult.SuccessResult(lotteryCall.GetMessage());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        re = JsonResult.FailResult(MsgShowConfig.Exception);
        //        LogService.logDebug(ex);
        //    }
        //    return re;
        //}

        /// <summary>
        /// 会员绑定抽奖记录   
        /// </summary>
        /// <returns></returns>
        private JsonResult MemberBindLottery()
        {
            JsonResult re = new JsonResult();
            try
            {
                List<int> actitityIdList = new List<int>() { actitityId };
                ReqLotteryPrizeModel req = new ReqLotteryPrizeModel()
                {
                    activityIdList = actitityIdList,
                    data_type = memberBaseModel.data_type,
                    out_id = memberBaseModel.out_id,
                    mobile = memberBaseModel.mobile,
                    nickname = memberBaseModel.nickname
                };
                IOperation<ReqLotteryPrizeModel> oper = ApiFactory.GetLotteryPrize();
                JsonResult reLotteryPrize = APICall.MainExcute(req, oper);
                if (reLotteryPrize.Result != Result.success)
                {
                    return JsonResult.FailResult(reLotteryPrize.Message);
                }

                List<WinRecordModel> winRecordList = Utility.JsonToObject<List<WinRecordModel>>(reLotteryPrize.Data.ToString());
                if (winRecordList == null || winRecordList.Count < 1)
                {
                    //未抽奖
                    ReqLotteryActivityModel reqActivity = new ReqLotteryActivityModel()
                    {
                        activity_id = actitityId,
                        data_type = memberBaseModel.data_type,
                        out_id = memberBaseModel.out_id,
                        mobile = memberBaseModel.mobile,
                        nickname = memberBaseModel.nickname
                    };
                    IOperation<ReqLotteryActivityModel> operActivity = ApiFactory.MemberBindLottery();
                    JsonResult reLotteryActivity = APICall.MainExcute(reqActivity, operActivity);
                    if (reLotteryActivity.Result != Result.success)
                    {
                        return JsonResult.FailResult(reLotteryActivity.Message);
                    }
                    Luck_ActivityPrize luckActivityPrizeModel = Utility.JsonToObject<Luck_ActivityPrize>(reLotteryActivity.Data.ToString());
                    re = JsonResult.SuccessResult(luckActivityPrizeModel.id - 2);
                }
                else
                {
                    //已经抽奖
                    WinRecordModel winRecordModel = winRecordList.OrderBy(n => n.Id).FirstOrDefault();
                    re = JsonResult.SuccessResult(winRecordModel.PrizeId - 2);
                }
            }
            catch (Exception ex)
            {
                re = JsonResult.FailResult(ex.ToString());
                LogService.LogDebug(ex);
            }
            return re;
        }
    }
}