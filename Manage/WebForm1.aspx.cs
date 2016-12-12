using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YYT.Model;
using YYT.BLL;

namespace Manage
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            YYT_Member model = new YYT_Member();
            YYT_MemberBO service = YYT_MemberBO.GetService;
            service.YYT_Member.Add(model);
            service.SaveChanges();

            using (var db = new YYT_DBEntities())
            {

                db.SaveChanges();
            }
        }
    }
}