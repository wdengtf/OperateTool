using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.SessionState;
using Auth.Wx;
using Framework.Model;

namespace WebControllers.Member
{
    /// <summary>
    /// 前台授权页面
    /// </summary>
    public class BaseMemberPage : System.Web.UI.Page, IRequiresSessionState
    {
        protected YYT_Member member = new YYT_Member();
        protected MemberBaseModel memberBaseModel = null;

        public BaseMemberPage()
        {
            memberBaseModel = member.GetMemberCookies();
            //登录验证
            if (!member.MemberValidate(memberBaseModel))
            {
                System.Web.HttpContext.Current.Response.Redirect(WxConfig.GetCode(ConfigBL.WxAuthUrl()));
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }
    }
}
