using Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YYT.Model;

namespace Act.LuckDraw.Model
{
    /// <summary>
    /// 获取抽奖对象
    /// </summary>
    public class LotteryModel
    {
        /// <summary>
        ///  活动id
        /// </summary>
        public int ActivityId { get; set; }
        /// <summary>
        /// 活动名称
        /// </summary>
        public string ActivityName { get; set; }
        /// <summary>
        /// 可抽奖次数
        /// </summary>
        public int LotteryNumber { get; set; }
        /// <summary>
        /// 奖品List
        /// </summary>

        public List<Luck_ActivityPrize> LuckActivityPrize { get; set; }
    }

    /// <summary>
    /// 用户中奖对象
    /// </summary>
    public class WinRecordModel : MemberBaseModel
    {
        /// <summary>
        /// 中奖id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 奖品id
        /// </summary>
        public int PrizeId { get; set; }
        /// <summary>
        /// 活动id
        /// </summary>
        public int ActivityId { get; set; }
        /// <summary>
        /// 奖品名称
        /// </summary>
        public string PrizeName { get; set; }

        /// <summary>
        /// 奖品金额
        /// </summary>
        public decimal PrizePrice { get; set; }

        /// <summary>
        /// 奖品等级
        /// </summary>
        public string PrizeLevel { get; set; }

        /// <summary>
        /// 奖品类型
        /// </summary>
        public int PrizeType { get; set; }

        /// <summary>
        /// 奖品图片
        /// </summary>
        public string PrizeImg { get; set; }
        /// <summary>
        /// 奖品介绍
        /// </summary>
        public string Introduction { get; set; }
        /// <summary>
        /// 领取日期
        /// </summary>
        public DateTime Receivetime { get; set; }
    }
}
