using Framework.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace Web
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            LogService.logInfo("第一个人访问时间：" + DateTime.Now.ToString());
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            LogService.logInfo("网站关闭,或重启时时间：" + DateTime.Now.ToString());
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            //在出现未处理的错误时运行的代码
            Exception ex = Server.GetLastError().GetBaseException();
            StringBuilder strErr = new StringBuilder(1024);
            strErr.Append("\r\n--------------------------------------------------------------------------------------------------");
            strErr.Append("\r\n" + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss"));
            strErr.Append("\r\n.客户信息：");

            string ip = "";
            if (Request.ServerVariables.Get("HTTP_X_FORWARDED_FOR") != null)
            {
                ip = Request.ServerVariables.Get("HTTP_X_FORWARDED_FOR").ToString().Trim();
            }
            else
            {
                ip = Request.ServerVariables.Get("Remote_Addr").ToString().Trim();
            }
            strErr.Append("\r\n\tIp:" + ip);
            strErr.Append("\r\n\t浏览器:" + Request.Browser.Browser.ToString());
            strErr.Append("\r\n\t浏览器版本:" + Request.Browser.MajorVersion.ToString());
            strErr.Append("\r\n\t操作系统:" + Request.Browser.Platform.ToString());
            strErr.Append("\r\n.错误信息：");
            if (Request.UrlReferrer != null)
                strErr.Append("\r\n\t来源页面：" + Request.UrlReferrer.ToString());
            strErr.Append("\r\n\t页面：" + Request.Url.ToString());
            strErr.Append("\r\n\t错误信息：" + ex.Message);
            strErr.Append("\r\n\t错误源：" + ex.Source);
            strErr.Append("\r\n\t异常方法：" + ex.TargetSite);
            strErr.Append("\r\n\t堆栈信息：" + ex.StackTrace);
            strErr.Append("\r\n--------------------------------------------------------------------------------------------------");

            //处理完及时清理异常 
            Server.ClearError();
            //跳转至出错页面 
            LogService.logFatal(strErr.ToString());

            return;
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}