using Events;
using Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Validate;
using System.Reflection;

namespace Auth
{
    public abstract class AuthBase<T, K> : EventBase, IAuth<T, K>
        where T : class
        where K : class
    {
        protected MethodBase methodBase = System.Reflection.MethodBase.GetCurrentMethod();
        protected bool result = false;
        protected T req;

        public AuthBase()
        { }

        /// <summary>
        /// 参数设置
        /// </summary>
        /// <param name="req"></param>
        public virtual void Set(T req, string channelUser)
        {
            this.OperationUserName = channelUser;
            this.req = req;
        }

        /// <summary>
        /// 执行操作
        /// </summary>
        /// <returns></returns>
        public abstract K Auth();

        /// <summary>
        /// 返回消息
        /// </summary>
        /// <returns></returns>
        public virtual string GetMessage()
        {
            return BaseMessage;
        }

        /// <summary>
        /// 返回结果状态
        /// </summary>
        /// <returns></returns>
        public bool GetResultState()
        {
            return result;
        }

        /// <summary>
        /// 参数验证
        /// </summary>
        /// <returns></returns>
        protected virtual bool Validate()
        {
            bool result = false;
            try
            {
                #region 非空验证
                if (req == null)
                {
                    BaseMessage = MsgShowConfig.ParmNotEmpty;
                    return false;
                }
                var listError = req.IsValid();
                if (listError.Count > 0)
                {
                    BaseMessage = listError[0].Message;
                    return false;
                }
                #endregion
                result = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return result;
        }
    }
}
