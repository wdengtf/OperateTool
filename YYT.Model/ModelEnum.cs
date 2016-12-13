using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YYT.Model
{
    public enum HT_MenuStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        Normal = 0,
        /// <summary>
        /// 锁定
        /// </summary>
        Locking = 1,
    }

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
    public enum Luck_ActivityJackpotStatus
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
}
