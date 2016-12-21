using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YYT.Api.Model;
using Act.LuckDraw;

namespace YYT.Api
{
    public class ApiFactory
    {
        public ApiFactory()
        { }

        #region 抽奖活动接口
        /// <summary>
        /// 获取抽奖活动数据
        /// </summary>
        /// <returns></returns>
        public static IOperation<ReqLotteryActivityModel> GetLotteryActivity()
        {
            return new LuckDraw.GetLotteryActivity();
        }

        /// <summary>
        /// 获取会员抽奖奖品数据
        /// </summary>
        /// <returns></returns>
        public static IOperation<ReqLotteryPrizeModel> GetLotteryPrize()
        {
            return new LuckDraw.GetLotteryPrize();
        }

        /// <summary>
        /// 绑定会员抽奖数据
        /// </summary>
        /// <returns></returns>
        public static IOperation<ReqLotteryActivityModel> MemberBindLottery()
        {
            return new LuckDraw.MemberBindLottery();
        }
        #endregion

    }
}
