using System;
using System.Web.UI;
using Auth.Model;
using Auth.Wx;
using Framework;
using Framework.Log;
using Framework.Model;
using YYT.BLL;
using YYT.Model;
using System.Linq.Expressions;
using Framework.EF;
using WebControllers;

namespace Web.Authorized
{
    /// <summary>
    /// 微信网页授权
    /// </summary>
    public partial class wxAuth : System.Web.UI.Page
    {
        private string strCode = "";
        private YYT_MemberBO memberBo = new YYT_MemberBO();
        private WxWebAuth wxWebAuth = new WxWebAuth();
        protected void Page_Load(object sender, EventArgs e)
        {
            strCode = Utility.RF("code");
            if (!Page.IsPostBack)
            {
                if (!String.IsNullOrWhiteSpace(strCode))
                {
                    WxMemberModel wxMemberModel = wxWebAuth.WxAuthGetUserInfo(strCode);
                    if (wxMemberModel == null)
                    {
                        Response.Redirect("/404.html");
                        return;
                    }

                    MemberBaseModel memberBaseModel = new MemberBaseModel();
                    memberBaseModel.data_type = "WX";
                    memberBaseModel.out_id = wxMemberModel.openid;
                    memberBaseModel.nickname = wxMemberModel.nickname;
                    memberBaseModel.mobile = "";
                    new WebControllers.Member.YYT_Member().SaveMemberCookies(memberBaseModel);

                    Expression<Func<YYT_Member, bool>> where = PredicateExtensionses.True<YYT_Member>();
                    where = where.AndAlso(p => p.out_id.Contains(wxMemberModel.openid));
                    YYT_Member dbMemberModel = memberBo.GetSingle<int>(where);
                    if (dbMemberModel == null)
                    {
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
                        member.channelUserId = ConfigBL.AccountUserId(); ;
                        member.userName = "";
                        member.Mobile = "";
                        member.email = "";
                        member.Createtime = DateTime.Now;
                        member.Updatetime = DateTime.Now;
                        member.Status = (int)StatusEnmu.Normal;
                        memberBo.Add(member);
                    }
                    Response.Redirect("/index.aspx");
                }
            }
        }
    }
}