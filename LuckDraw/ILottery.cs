using Framework.Model;
using Act.LuckDraw.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YYT.Model;
using Events;

namespace Act.LuckDraw
{
    /// <summary>
    /// 抽奖活动接口
    /// </summary>
    public interface ILottery : IEventHandler
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
        Luck_ActivityPrize MemberBindLottery(MemberBaseModel member, int activity_id);

        /// <summary>
        /// 显示中奖奖品
        /// </summary>
        /// <returns></returns>
        List<WinRecordModel> GetLotteryPrize(MemberBaseModel member, List<int> activityIdList);

        /// <summary>
        /// 验证抽奖活动
        /// </summary>
        /// <param name="activity_id"></param>
        /// <returns></returns>
        Luck_Activity VerifyLottery(int activity_id);

        /// <summary>
        /// 返回提示消息
        /// </summary>
        /// <returns></returns>
        string GetMessage();

        /// <summary>
        /// 执行结果
        /// </summary>
        /// <returns></returns>
        bool GetResultState();
    }
}
