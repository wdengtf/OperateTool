using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Validate;
using Events;
using System.Reflection;

namespace Payment
{
    public abstract class PayBase<T> : EventBase, IPay<T> where T : class
    {
        protected MethodBase methodBase = System.Reflection.MethodBase.GetCurrentMethod();

        protected string message = "";
        protected bool result = false;
        protected object data = null;
        protected T req;

        public virtual void Set(T req)
        {
            this.req = req;
        }


        public abstract void Excute();

        public virtual string GetMessage()
        {
            return message;
        }

        public virtual bool GetResult()
        {
            return result;
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
                this.message = "没有数据";
                return false;
            }
            var listError = req.IsValid();
            if (listError.Count > 0)
            {
                this.message = listError[0].Message;
                return false;
            }
            #endregion
            return true;
        }
    }
}
