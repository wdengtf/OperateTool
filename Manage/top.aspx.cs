using System;
using System.Collections.Generic;
using Framework;
using WebControllers.Admin;
using WebControllers.Handle;
using WebControllers.Model;

namespace Web.Manage
{
    public partial class top : BaseAdminPage
    {
        protected string manageDroitListStr = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (manageUserModel == null)
                Response.Write("<script>window.top.location.href= 'index.aspx ';</script>");


            List<ManageDroitModel> manageDroitList = new AdminUser().GetManageDroitModel(manageUserModel.UserDroit);
            if (manageDroitList != null && manageDroitList.Count > 0)
                manageDroitListStr = Utility.ToJson(manageDroitList);
        }
    }
}