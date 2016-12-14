using System;
using Framework;
using WebControllers.Handle;

namespace Web.Manage
{
    public partial class left : BaseAdminPage
    {
        protected int pid = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            pid = Utility.FNumeric("pid");
        }
    }
}