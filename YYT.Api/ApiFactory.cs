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

        /// <summary>
        /// 抽奖接口
        /// </summary>
        /// <param name="className">类型名</param>
        /// <returns></returns>
        //public static LuckDraw.ILottery Lottery(string className = null)
        //{
        //    LuckDraw.GetLotteryActivity

        //    LuckDraw.ILottery handle = null;
        //    switch (className)
        //    {
        //        default:
        //            handle = new LuckDraw.LotteryBase();
        //            break;
        //    }
        //    return handle;
        //}

    }
}
