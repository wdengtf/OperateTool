using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Act.LuckDraw
{
    public class Factory
    {
        /// <summary>
        /// 抽奖接口
        /// </summary>
        /// <param name="className">类型名</param>
        /// <returns></returns>
        public static LuckDraw.ILottery Lottery(string className = null)
        {
            LuckDraw.ILottery handle = null;
            switch (className)
            {
                default:
                    handle = new LuckDraw.LotteryBase();
                    break;
            }
            return handle;
        }
    }
}
