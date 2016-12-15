using Framework;
using Framework.Log;
using Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;

namespace WebControllers.Member
{
    public abstract class BaseHandle : IHttpHandler, IRequiresSessionState
    {
        protected string message = String.Empty;
        protected string action = String.Empty;
        protected string sortField = "Id";//默认排序字段
        protected HttpContext httpContext = null;
        protected YYT_Member member = new YYT_Member();
        protected MemberBaseModel memberBaseModel = null;

        public virtual void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            JsonResult re = new JsonResult();
            try
            {
                //登录验证
                memberBaseModel = member.GetMemberCookies();
                if (!member.MemberValidate(memberBaseModel))
                {
                    re = JsonResult.FailResult(MsgShowConfig.NoLogin);
                }
                else
                {
                    OnInit();
                    httpContext = context;
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
                action = Utility.RF("action");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// 获取提示消息
        /// </summary>
        /// <returns></returns>
        protected string GetMessage()
        {
            return this.message;
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
