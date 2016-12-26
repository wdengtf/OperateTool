using Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ayx
{
    /// <summary>
    /// 悦园数据接口
    /// </summary>
    public interface IAyx<T> : IEventHandler where T : class
    {
        /// <summary>
        /// 参数设置
        /// </summary>
        /// <param name="req"></param>
        void Set(T req);

        /// <summary>
        /// 执行
        /// </summary>
        /// <returns></returns>
        bool Excute();

        /// <summary>
        /// 返回消息
        /// </summary>
        /// <returns></returns>
        string GetMessage();

        /// <summary>
        /// 返回数据
        /// </summary>
        /// <returns></returns>
        object GetData();
    }
}
