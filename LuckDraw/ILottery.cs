using Framework.Model;
using LuckDraw.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckDraw
{
    /// <summary>
    /// 抽奖活动接口
    /// </summary>
    public interface ILottery
    {
        /// <summary>
        /// 获取抽奖活动
        /// </summary>
        /// <returns></returns>
        LotteryModel GetLotteryActivity(MemberBaseModel member, int activity_id);

        /// <summary>
        /// 会员绑定抽奖记录
        /// </summary>
        /// <returns></returns>
        int MemberBindLottery(MemberBaseModel member, int activity_id, string win_name, string win_phone);

        /// <summary>
        /// 显示中奖奖品
        /// </summary>
        /// <returns></returns>
        List<WinRecordModel> GetLotteryPrize(MemberBaseModel member, int activity_id);

        string GetMessage();

        /// <summary>
        /// 事件
        /// </summary>
        event EventHandler OnBegin;
        event EventHandler OnTipMsg;
        event EventHandler OnException;
        event EventHandler OnCompelete;
        event EventHandler OnSuccess;
        event EventHandler OnFail;
    }
}
