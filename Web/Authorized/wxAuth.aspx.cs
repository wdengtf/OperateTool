using System;
using System.Web.UI;
using Auth.Model;
using Auth.Wx;
using Framework;
using Framework.Log;
using Framework.Model;
using YYT.BLL;
using YYT.Model;

namespace Web.Authorized
{
    public partial class wxAuth : System.Web.UI.Page
    {
        private string strCode = "";
        private YYT_MemberBO memberBo = new YYT_MemberBO();
        protected void Page_Load(object sender, EventArgs e)
        {
            strCode = Utility.RF("code");
            if (!Page.IsPostBack)
            {
                if (!String.IsNullOrWhiteSpace(strCode))
                {
                    WxMemberModel wxMemberModel = new WxWebAuth().WxAuthGetUserInfo(strCode);
                    if (wxMemberModel == null)
                    {
                        Response.Redirect("404.html");
                        return;
                    }

                    MemberBaseModel memberBaseModel = new MemberBaseModel();
                    memberBaseModel.data_type = "WX";
                    memberBaseModel.out_id = wxMemberModel.openid;
                    memberBaseModel.nickname = wxMemberModel.nickname;
                    memberBaseModel.mobile = "";
                    new WebControllers.Member.YYT_Member().SaveMemberCookies(memberBaseModel);

                    YYT_Member member = new YYT_Member();
                    member.data_type = "WX";
                    member.out_id = wxMemberModel.openid;
                    member.nickname = wxMemberModel.nickname;
                    member.Sex = int.Parse(wxMemberModel.sex);
                    member.country = wxMemberModel.country;
                    member.province = wxMemberModel.province;
                    member.city = wxMemberModel.city;
                    member.headimgurl = wxMemberModel.headimgurl;
                    member.unionid = wxMemberModel.unionid;

                    member.area = "";
                    member.addr = "";
                    member.channelUserId = 1;
                    member.userName = "";
                    member.Mobile = "";
                    member.email = "";
                    if (memberBo.Add(member) > 0)
                        Response.Redirect("index.aspx");
                    else
                    {
                        Response.Redirect("404.html");
                        return;
                    }
                }
            }
        }
    }
}