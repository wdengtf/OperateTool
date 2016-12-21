using Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YYT.Api
{
    /// <summary>
    /// 外部接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IOperation<T> where T : BaseApiModel
    {
        /// <summary>
        /// 设置请求数据
        /// </summary>
        /// <param name="req"></param>
        void Set(T req);

        /// <summary>
        /// 获取消息
        /// </summary>
        /// <returns></returns>
        string GetMessage();


        /// <summary>
        /// 获取返回数据
        /// </summary>
        /// <returns></returns>
        object GetData();

        /// <summary>
        /// 执行操作
        /// </summary>
        bool Excute();

        /// <summary>
        /// 验证数据
        /// </summary>
        /// <returns></returns>
        bool Validate();
    }
}
