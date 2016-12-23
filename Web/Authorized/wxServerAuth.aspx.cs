using Auth.Model;
using Auth.Wx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YYT.BLL;
using Framework.Log;
using YYT.Model;
using WebControllers;
using System.Linq.Expressions;
using Framework.EF;

namespace Web.Authorized
{
    /// <summary>
    /// 微信服务号授权
    /// </summary>
    public partial class wxServerAuth : System.Web.UI.Page
    {
        private Wx_ConfigBO wxConfigBo = new Wx_ConfigBO();
        private WxServerAuth<DBNull, ServerTokenAndTicketModel> wxServerAuthor = new WxServerAuth<DBNull, ServerTokenAndTicketModel>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (SaveWxConfig())
                    Response.Write("授权成功");
            }
        }

        private bool SaveWxConfig()
        {
            bool result = false;
            try
            {
                ServerTokenAndTicketModel serverTokenAndTicketModel = wxServerAuthor.GetServerTokenAndTicken();
                if (serverTokenAndTicketModel == null)
                {
                    LogService.LogError("获取公众号AccessToken和JsapiTicket失败");
                    return false;
                }
                Expression<Func<Wx_Config, bool>> where = PredicateExtensionses.True<Wx_Config>();
                int accountUserId = ConfigBL.AccountUserId();
                where = where.AndAlso(p => p.channelUserId == accountUserId);
                Wx_Config wxConfigModel = wxConfigBo.GetSingle<int>(where);
                if (wxConfigModel == null)
                {
                    LogService.LogError("微信账号不存在");
                    return false;
                }
                wxConfigModel.access_token = serverTokenAndTicketModel.access_token;
                wxConfigModel.jsapi_ticket = serverTokenAndTicketModel.ticket;
                wxConfigModel.Updatetime = DateTime.Now;
                wxConfigBo.Update(wxConfigModel);
                result = true;
            }
            catch (Exception ex)
            {
                LogService.LogDebug(ex);
            }
            return result;
        }
    }
}