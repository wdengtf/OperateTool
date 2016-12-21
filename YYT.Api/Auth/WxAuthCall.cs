using Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YYT.Api.Auth
{
    public class WxAuthCall
    {
        private readonly IEvent events = new Log4NetEvents();
        /// <summary>
        /// 构造函数
        /// </summary>
        public WxAuthCall()
        {
           

        }
        #region 事件操作
        /// <summary>
        /// 添加事件
        /// </summary>
        //private void AddEvent()
        //{
        //    lottery.OnBegin += new EventHandler(events.OnBegin);
        //    lottery.OnTipMsg += new EventHandler(events.OnTipMsg);
        //    lottery.OnSuccess += new EventHandler(events.OnSuccess);
        //    lottery.OnFail += new EventHandler(events.OnFail);
        //    lottery.OnException += new EventHandler(events.OnException);
        //    lottery.OnCompelete += new EventHandler(events.OnCompelete);
        //}

        /// <summary>
        /// 取消事件
        /// </summary>
        //private void DeleteEvent()
        //{
        //    lottery.OnBegin -= new EventHandler(events.OnBegin);
        //    lottery.OnTipMsg -= new EventHandler(events.OnTipMsg);
        //    lottery.OnException -= new EventHandler(events.OnException);
        //    lottery.OnSuccess -= new EventHandler(events.OnSuccess);
        //    lottery.OnFail -= new EventHandler(events.OnFail);
        //    lottery.OnCompelete -= new EventHandler(events.OnCompelete);
        //}
        #endregion
    }
}
