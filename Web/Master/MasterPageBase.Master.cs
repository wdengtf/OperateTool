using System;
using System.Web.SessionState;

namespace Web.Master
{
    public partial class MasterPageBase : System.Web.UI.MasterPage, IRequiresSessionState
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}