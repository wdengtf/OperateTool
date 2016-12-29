using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Events;
using Framework.Log;

namespace Auth
{
    public class AuthCall<T, M>
        where T : class 
        where M :class
    {
        private readonly IAuth<T,M> iAuth = null;
        //private readonly IEvent events = new MsmqEvents();
        private readonly IEvent events = new Log4NetEvents();

        public AuthCall()
        {
            iAuth = Factory.Auth<T,M>();
        }

        /// <summary>
        /// 设置参数
        /// </summary>
        /// <param name="req"></param>
        /// <param name="channelUser"></param>
        public void Set(T req)
        {
            iAuth.Set(req);
        }

        /// <summary>
        /// 执行 返回结果
        /// </summary>
        /// <returns></returns>
        public M Excute()
        {
            DeleteEvent();
            AddEvent();
            return iAuth.Excute();
        }

        /// <summary>
        /// 获取错误消息
        /// </summary>
        /// <returns></returns>
        public string GetMessage()
        {
            string message = "";
            try
            {
                message = iAuth.GetMessage();
            }
            catch (Exception ex)
            {
                LogService.LogDebug(ex);
            }
            return message;
        }

        /// <summary>
        /// 返回执行结果状态
        /// </summary>
        /// <returns></returns>
        public bool GetResultState()
        {
            bool result = false;
            try
            {
                result = iAuth.GetResultState();
            }
            catch (Exception ex)
            {
                LogService.LogDebug(ex);
            }
            return result;
        }

        #region 事件操作
        /// <summary>
        /// 添加事件
        /// </summary>
        private void AddEvent()
        {
            iAuth.OnBegin += new EventHandler(events.OnBegin);
            iAuth.OnTipMsg += new EventHandler(events.OnTipMsg);
            iAuth.OnSuccess += new EventHandler(events.OnSuccess);
            iAuth.OnFail += new EventHandler(events.OnFail);
            iAuth.OnException += new EventHandler(events.OnException);
            iAuth.OnCompelete += new EventHandler(events.OnCompelete);
        }

        /// <summary>
        /// 取消事件
        /// </summary>
        private void DeleteEvent()
        {
            iAuth.OnBegin -= new EventHandler(events.OnBegin);
            iAuth.OnTipMsg -= new EventHandler(events.OnTipMsg);
            iAuth.OnException -= new EventHandler(events.OnException);
            iAuth.OnSuccess -= new EventHandler(events.OnSuccess);
            iAuth.OnFail -= new EventHandler(events.OnFail);
            iAuth.OnCompelete -= new EventHandler(events.OnCompelete);
        }
        #endregion

    }
}
