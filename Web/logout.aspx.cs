using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebControllers.Member;

namespace Web
{
    public partial class logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (new YYT_Member().LoginOut())
            {
                Response.Redirect("/index.aspx");
            }
        }
    }
}