using Framework.Model;
using Act.LuckDraw;
using Act.LuckDraw.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YYT.Api.Model;

namespace YYT.Api.LuckDraw
{
    /// <summary>
    /// 获取抽奖活动数据
    /// </summary>
    public class GetLotteryActivity : OperationBase<ReqLotteryActivityModel>
    {
        private readonly ILottery lottery = null;
        /// <summary>
        /// 构造函数
        /// </summary>
        public GetLotteryActivity()
        {
            lottery = Factory.Lottery();
        }

        /// <summary>
        /// 带参数构造函数
        /// </summary>
        /// <param name="className">类名</param>
        public GetLotteryActivity(string className)
        {
            lottery = Factory.Lottery(className);
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
            //LotteryModel lotteryModel = call.GetLotteryActivity(user, req.activity_id);
            DeleteEvent(lottery);
            AddEvent(lottery);
            LotteryModel lotteryModel = lottery.GetLotteryActivity(user, req.activity_id);
            if (call.GetResultState())
            {
                this.result = true;
                this.date = lotteryModel;
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
