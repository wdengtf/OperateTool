using Act.LuckDraw;
using Act.LuckDraw.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YYT.Api.Model;
using YYT.Entities;
using YYT.Model;

namespace YYT.Api.LuckDraw
{
    /// <summary>
    /// 绑定会员抽奖数据
    /// </summary>
    public class MemberBindLottery : OperationBase<ReqLotteryActivityModel>
    {
         /// <summary>
        /// 构造函数
        /// </summary>
        public MemberBindLottery()
        {
            
        }


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
            Luck_ActivityPrize luckActivityPrize = call.MemberBindLottery(user, req.activity_id, req.channelUser);
            if (call.GetResultState())
            {
                this.result = true;
                this.date = luckActivityPrize;
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
