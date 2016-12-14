using System;
using Framework.Log;
using WebControllers.Handle;

namespace Web.Manage
{
    public partial class exit : BaseAdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!adminUser.LoginOut())
                LogService.logError("exit.aspx退出登录清除用户信息失败");

            Response.Write("<script>window.top.location.href= '/index.aspx ';</script>");
        }
    }
}