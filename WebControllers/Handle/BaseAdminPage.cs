using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.SessionState;
using WebControllers.Admin;
using WebControllers.Model;

namespace WebControllers.Handle
{
    public class BaseAdminPage : System.Web.UI.Page, IRequiresSessionState
    {
        protected string strMsg = "";
        protected ManageUserModel manageUserModel = null;
        protected AdminUser adminUser = new AdminUser();

        /// <summary>
        /// 后台页面公共程序
        /// </summary>
        public BaseAdminPage() { }

        protected override void OnInit(EventArgs e)
        {
            adminUser.CheckLoginJump();
            manageUserModel = adminUser.GetManageUserModel();
            base.OnInit(e);
        }
    }
}
