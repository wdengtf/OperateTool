using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Events;

namespace Auth
{
    public interface IAuth<T, M> : IEventHandler where T : class where M : class
    {
        /// <summary>
        /// 设置请求数据
        /// </summary>
        /// <param name="req"></param>
        void Set(T req, string channelUser);

        /// <summary>
        /// 返回结果
        /// </summary>
        /// <returns></returns>
        M Auth();

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
