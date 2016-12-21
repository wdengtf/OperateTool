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
    /// 绑定会员抽奖数据
    /// </summary>
    public class MemberBindLottery : OperationBase<ReqLotteryActivityModel>
    {
        private readonly ILottery lottery = null;
         /// <summary>
        /// 构造函数
        /// </summary>
        public MemberBindLottery()
        {
            lottery = Factory.Lottery();
        }

        /// <summary>
        /// 带参数构造函数
        /// </summary>
        /// <param name="className">类名</param>
        public MemberBindLottery(string className)
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
            //Luck_ActivityPrize luckActivityPrize = call.MemberBindLottery(user, req.activity_id);
            DeleteEvent(lottery);
            AddEvent(lottery);
            Luck_ActivityPrize luckActivityPrize = lottery.MemberBindLottery(user, req.activity_id);
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

        #region 事件操作
        /// <summary>
        /// 添加事件
        /// </summary>
        private void AddEvent()
        {
            lottery.OnBegin += new EventHandler(events.OnBegin);
            lottery.OnTipMsg += new EventHandler(events.OnTipMsg);
            lottery.OnSuccess += new EventHandler(events.OnSuccess);
            lottery.OnFail += new EventHandler(events.OnFail);
            lottery.OnException += new EventHandler(events.OnException);
            lottery.OnCompelete += new EventHandler(events.OnCompelete);
        }

        /// <summary>
        /// 取消事件
        /// </summary>
        private void DeleteEvent()
        {
            lottery.OnBegin -= new EventHandler(events.OnBegin);
            lottery.OnTipMsg -= new EventHandler(events.OnTipMsg);
            lottery.OnException -= new EventHandler(events.OnException);
            lottery.OnSuccess -= new EventHandler(events.OnSuccess);
            lottery.OnFail -= new EventHandler(events.OnFail);
            lottery.OnCompelete -= new EventHandler(events.OnCompelete);
        }
        #endregion
    }
}
