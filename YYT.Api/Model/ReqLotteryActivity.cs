using Framework.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YYT.Api.Model
{
    /// <summary>
    /// 抽奖活动对象
    /// </summary>
    public class ReqLotteryActivityModel : BaseModel
    {
        /// <summary>
        /// 平台类型
        /// </summary>
        [Required(ErrorMessage = "平台类型不能为空")]
        public string data_type { get; set; }
        /// <summary>
        /// out_id
        /// </summary>
        [Required(ErrorMessage = "out_id不能为空")]
        public string out_id { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string mobile { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string nickname { get; set; }

        /// <summary>
        /// 活动id
        /// </summary>
        [Required(ErrorMessage = "活动id不能为空")]
        public int activity_id { get; set; }
    }

    /// <summary>
    /// 获取会员抽奖奖品对象
    /// </summary>
    public class ReqLotteryPrizeModel : BaseModel
    {
        /// <summary>
        /// 平台类型
        /// </summary>
        [Required(ErrorMessage = "平台类型不能为空")]
        public string data_type { get; set; }
        /// <summary>
        /// out_id
        /// </summary>
        [Required(ErrorMessage = "out_id不能为空")]
        public string out_id { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string mobile { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string nickname { get; set; }

        /// <summary>
        /// 活动id
        /// </summary>
        public List<int> activityIdList { get; set; }
    }
}
