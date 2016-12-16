using Framework.Cookies;
using System;
using System.Web.SessionState;

namespace WebControllers.Handle
{
    public class BasePage : System.Web.UI.Page, IRequiresSessionState
    {
        protected string strMsg = "";
        protected CookieHandle cookieHandle = new CookieHandle();
        /// <summary>
        /// 后台页面公共程序 没有授权
        /// </summary>
        public BasePage() { }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }
    }
}
