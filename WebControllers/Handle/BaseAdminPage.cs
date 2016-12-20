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
        protected string defaultSort = "desc";//默认排序
        protected int jumpDroitGroupId = 0;

        /// <summary>
        /// 后台页面公共程序 有授权
        /// </summary>
        public BaseAdminPage() { }

        protected override void OnInit(EventArgs e)
        {
            adminUser.CheckLoginJump();
            manageUserModel = adminUser.GetManageUserModel();
            jumpDroitGroupId = ConfigBL.JumpDroitGroupId();
            base.OnInit(e);
        }
    }
}
