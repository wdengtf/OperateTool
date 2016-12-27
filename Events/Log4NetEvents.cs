using Framework;
using Framework.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YYT.Model;

namespace Events
{
    /// <summary>
    /// 基础事件处理类 通过日志记录
    /// </summary>
    public class Log4NetEvents : IEvent
    {
        public Log4NetEvents()
        {

        }

        public virtual void OnBegin(object sender, EventArgs e)
        {
            DataHandleEventArgs Args = (DataHandleEventArgs)e;
            LogService.LogInfo(Utility.ToJson(Args));
        }

        public virtual void OnTipMsg(object sender, EventArgs e)
        {
            DataHandleEventArgs Args = (DataHandleEventArgs)e;
            LogService.LogInfo(Utility.ToJson(Args));
        }

        public virtual void OnException(object sender, EventArgs e)
        {
            DataHandleEventArgs Args = (DataHandleEventArgs)e;
            LogService.LogError(Utility.ToJson(Args));
        }

        public virtual void OnSuccess(object sender, EventArgs e)
        {
            DataHandleEventArgs Args = (DataHandleEventArgs)e;
            LogService.LogInfo(Utility.ToJson(Args));
        }

        public virtual void OnFail(object sender, EventArgs e)
        {
            DataHandleEventArgs Args = (DataHandleEventArgs)e;
            LogService.LogError(Utility.ToJson(Args));
        }

        public virtual void OnCompelete(object sender, EventArgs e)
        {
            DataHandleEventArgs Args = (DataHandleEventArgs)e;
            LogService.LogInfo(Utility.ToJson(Args));
        }
    }
}
