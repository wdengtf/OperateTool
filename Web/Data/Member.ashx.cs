using Framework.EF;
using Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using WebControllers.Member;
using YYT.BLL;
using Framework;
using Framework.Log;
using LuckDraw;
using LuckDraw.Model;

namespace Web.Data
{
    /// <summary>
    /// Member 的摘要说明
    /// </summary>
    public class Member : BaseHandle
    {
        private YYT_MemberBO memberBo = new YYT_MemberBO();
        private LotteryCall lotteryCall = new LotteryCall();
        public override JsonResult HandleProcess()
        {
            JsonResult re = new JsonResult();
            switch (action)
            {
                case "updateMember":
                    re = UpdateMember();
                    break;
            }
            return re;
        }

        /// <summary>
        /// 更新会员信息
        /// </summary>
        /// <returns></returns>
        private JsonResult UpdateMember()
        {
            JsonResult re = new JsonResult();
            try
            {
                string mobile = Utility.FilterString(Utility.RF("mobile"));
                string addr = Utility.FilterString(Utility.RF("addr"));
                string realName = Utility.FilterString(Utility.RF("name"));

                Expression<Func<YYT.Model.YYT_Member, bool>> where = PredicateExtensionses.True<YYT.Model.YYT_Member>();
                where = where.AndAlso(p => p.out_id.Contains(memberBaseModel.out_id));
                YYT.Model.YYT_Member memberModel = memberBo.GetSingle<int>(where);
                if (memberModel == null)
                {
                    LogService.logFatal("未找到该会员信息" + Utility.ToJson(memberBaseModel));
                    return JsonResult.FailResult("请重新刷新授权");
                }

                List<int> actitityIdList = new List<int>() { 3 };
                List<WinRecordModel> winRecordList = lotteryCall.GetLotteryPrize(memberBaseModel, actitityIdList);
                if (!lotteryCall.GetResultState())
                    return JsonResult.FailResult(lotteryCall.GetMessage());

                if (winRecordList == null || winRecordList.Count < 1)
                    return JsonResult.FailResult("未查到您的中奖信息");

                //更新会员信息
                memberModel.Mobile = mobile;
                memberModel.addr = addr;
                memberModel.realName = realName;
                memberBo.Update(memberModel);
                re = JsonResult.SuccessResult(MsgShowConfig.Success);
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