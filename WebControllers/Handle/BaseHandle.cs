using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;
using Framework;
using Framework.Log;
using Framework.Model;
using Framework.Validate;
using WebControllers.Model;
using WebControllers.Admin;

namespace WebControllers.Handle
{
    public abstract class BaseHandle : IHttpHandler, IRequiresSessionState
    {
        private string data, sign;
        protected string message = String.Empty;
        protected string action = String.Empty;
        protected string defaultSort = "desc";//默认排序

        protected ManageUserModel manageUserModel = null;
        protected AdminUser adminUser = new AdminUser();

        protected HttpContext httpContext = null;
        public virtual void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            JsonResult re = new JsonResult();
            try
            {
                OnInit();
                httpContext = context;

                //登录验证
                if (!MemberValidate())
                {
                    re = JsonResult.FailResult(GetMessage());
                }
                else
                {
                    re = HandleProcess();
                }
            }
            catch (Exception ex)
            {
                re = JsonResult.FailResult(MsgShowConfig.EmptyFunction);
                LogService.logDebug(ex);
            }
            context.Response.Write(Utility.ToJson(re));
        }

        public abstract JsonResult HandleProcess();

        /// <summary>
        /// 参数初始化
        /// </summary>
        protected void OnInit()
        {
            try
            {
                data = Utility.RF("data");
                sign = Utility.RF("sign");
                action = Utility.RF("action");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        protected string GetData()
        {
            return this.data;
        }

        /// <summary>
        /// 计算总页数
        /// </summary>
        /// <param name="totalRecord"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        protected int GetTotalPage(int totalRecord,int pageSize)
        {
            return totalRecord > ((totalRecord / pageSize) * pageSize) ? (totalRecord / pageSize) + 1 : (totalRecord / pageSize);
        }

        /// <summary>
        /// 签名验证
        /// </summary>
        /// <returns></returns>
        protected bool SignValidate()
        {
            return true;
        }

        /// <summary>
        /// 获取提示消息
        /// </summary>
        /// <returns></returns>
        protected string GetMessage()
        {
            return this.message;
        }

        /// <summary>
        /// 参数验证
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        protected bool Validate<T>(T t)
        {
            #region 参数验证
            if (t == null)
            {
                message = "数据错误";
                return false;
            }
            List<BrokenRule> ErrorMessage = t.IsValid();
            if (ErrorMessage.Count > 0)
            {
                this.message = ErrorMessage[0].Message;
                return false;
            }
            return true;
            #endregion
        }

        /// <summary>
        /// 登录验证
        /// </summary>
        /// <returns></returns>
        private bool MemberValidate()
        {
            bool _flag = false;

            #region 登录信息设置
            if (!adminUser.CheckLogin())
            {
                this.message = MsgShowConfig.NoLogin;
                return _flag;
            }
            manageUserModel = adminUser.GetManageUserModel();
            #endregion
            _flag = true;
            return _flag;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
