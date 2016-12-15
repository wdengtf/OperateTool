using System;
using System.Web.UI;
using Auth.Model;
using Auth.Wx;
using Framework;
using Framework.Log;

namespace Web.Authorized
{
    public partial class wxAuth : System.Web.UI.Page
    {
        private string strCode = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            strCode = Utility.RF("code");
            if (!Page.IsPostBack)
            {
                if (!String.IsNullOrWhiteSpace(strCode))
                {
                    WxMemberModel wxMemberModel = new WxWebAuth().WxAuthGetUserInfo(strCode);
                    Response.Write(Utility.ToJson(wxMemberModel));
                }
            }
        }
    }
}