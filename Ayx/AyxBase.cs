using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Validate;
using Events;
using System.Reflection;

namespace Ayx
{
    public abstract class AyxBase<T> : EventBase, IAyx<T> where T : class
    {
        protected MethodBase methodBase = System.Reflection.MethodBase.GetCurrentMethod();

        protected bool result = false;
        protected object data = null;
        protected T req;


        public virtual void Set(T req)
        {
            this.req = req;
        }


        public abstract bool Excute();

        public virtual string GetMessage()
        {
            return BaseMessage;
        }

        public virtual object GetData()
        {
            return data;
        }

        /// <summary>
        /// 参数验证
        /// </summary>
        /// <returns></returns>
        protected virtual bool Validate()
        {
            #region 非空验证
            if (req == null)
            {
                this.BaseMessage = "没有数据";
                return false;
            }
            var listError = req.IsValid();
            if (listError.Count > 0)
            {
                this.BaseMessage = listError[0].Message;
                return false;
            }
            #endregion
            return true;
        }
    }
}
