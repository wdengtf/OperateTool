using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YYT.Model
{

    /// <summary>
    /// 默认状态
    /// </summary>
    public enum StatusEnmu
    {
        /// <summary>
        /// 正常
        /// </summary>
        Normal = 1,
        /// <summary>
        /// 锁定
        /// </summary>
        Locking = 0,
    }

    /// <summary>
    /// 栏目菜单
    /// </summary>
    public enum HT_MenuMenu
    {
        /// <summary>
        /// 主菜单
        /// </summary>
        MainMenu = 1,
        /// <summary>
        /// 列表菜单
        /// </summary>
        ListMenu = 2,
        /// <summary>
        /// 其他菜单
        /// </summary>
        OtherMenu = 0
    }

    /// <summary>
    /// 抽奖奖品奖池状态
    /// </summary>
    public enum LuckActivityJackpotStatus
    {
        /// <summary>
        /// 未抽奖
        /// </summary>
        NotDraw = 0,
        /// <summary>
        /// 已抽奖
        /// </summary>
        AlreadyDraw = 1
    }

    /// <summary>
    /// 抽奖活动规则
    /// </summary>
    public enum Luck_ActivityRules
    {
        Everyday = 1,
        Total = 2,
    }
}
