using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Auth.Model;
using Framework;
using WebControllers.Member;

namespace Web
{
    public partial class index : BaseMemberPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //{"openid":"o_jctwMAq7vsKMUX7qsYAsbpsZJE","nickname":"é?????","sex":1,"language":"zh_CN","city":"é???μ|","province":"????μ·","country":"??-???","headimgurl":"http:\/\/wx.qlogo.cn\/mmopen\/anlqibgoIhRK9ibicCaNamjMJpdNBCUP3JR5LHUHpEphzSwGEYB8PohWaQ9BLhoJO36ssTfXhGa50HchluhDnnZr2Z5iboaqjt38\/0","privilege":[]}

        }

        protected void btnTest_Click(object sender, EventArgs e)
        {
            string strUserinfo = txtContact.Text.Trim();
            WxMemberModel wxMemberModel = Utility.JsonToObject<WxMemberModel>(strUserinfo);
            Response.Write(Utility.ToJson(wxMemberModel));
        }
    }
}