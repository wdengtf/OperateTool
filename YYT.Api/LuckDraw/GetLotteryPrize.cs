using Framework.Model;
using Act.LuckDraw;
using Act.LuckDraw.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YYT.Api.Model;
using YYT.Model;

namespace YYT.Api.LuckDraw
{
    /// <summary>
    /// 获取抽奖活动数据
    /// </summary>
    public class GetLotteryPrize : OperationBase<ReqLotteryPrizeModel>
    {
        public override bool Excute()
        {
            LotteryCall call = new LotteryCall();
            MemberBaseModel user = new MemberBaseModel
            {
                data_type = req.data_type,
                out_id = req.out_id,
                mobile = req.mobile,
                nickname = req.nickname,
            };
            List<WinRecordModel> winRecordList = call.GetLotteryPrize(user, req.activityIdList);
            if (call.GetResultState())
            {
                this.result = true;
                this.date = winRecordList;
            }
            else
            {
                this.result = false;
                this.message = call.GetMessage();
            }
            return result;
        }
    }
}
