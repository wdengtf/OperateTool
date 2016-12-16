using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebControllers.Member;
using Framework.Model;
using LuckDraw;
using Framework.Log;
using LuckDraw.Model;
using YYT.Model;

namespace Web.Data
{
    /// <summary>
    /// Lottery 的摘要说明
    /// </summary>
    public class Lottery : BaseHandle
    {
        private LotteryCall lotteryCall = new LotteryCall();
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
                LotteryModel lotteryModel = lotteryCall.GetLotteryActivity(memberBaseModel, actitityId);
                if (lotteryCall.GetResultState())
                    re = JsonResult.SuccessResult(MsgShowConfig.Success);
                else
                    re = JsonResult.SuccessResult(lotteryCall.GetMessage());
            }
            catch (Exception ex)
            {
                re = JsonResult.FailResult(MsgShowConfig.Exception);
                LogService.logDebug(ex);
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
                Luck_ActivityPrize luckActivityPrizeModel = new Luck_ActivityPrize();

                List<int> actitityIdList = new List<int>() { actitityId };
                List<WinRecordModel> winRecordList = lotteryCall.GetLotteryPrize(memberBaseModel, actitityIdList);
                if (lotteryCall.GetResultState())
                {
                    if (winRecordList == null || winRecordList.Count < 1)
                    {
                        //未抽奖
                        luckActivityPrizeModel = lotteryCall.MemberBindLottery(memberBaseModel, actitityId);
                        if (lotteryCall.GetResultState())
                            re = JsonResult.SuccessResult(luckActivityPrizeModel.Id - 2);
                        else
                            re = JsonResult.FailResult(lotteryCall.GetMessage());
                    }
                    else
                    {
                        //已抽奖 返回抽奖记录
                        WinRecordModel winRecordModel = winRecordList.OrderBy(n => n.Id).FirstOrDefault();
                        re = JsonResult.SuccessResult(winRecordModel.PrizeId - 2);
                    }
                }
                else
                {
                    re = JsonResult.SuccessResult(lotteryCall.GetMessage());
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