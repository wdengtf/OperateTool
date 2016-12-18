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

namespace Web.Authorized
{
    /// <summary>
    /// 微信服务号授权
    /// </summary>
    public partial class wxServerAuth : System.Web.UI.Page
    {
        private Wx_ConfigBO wxConfigBo = new Wx_ConfigBO();
        private WxServerAuth wxServerAuthor = new WxServerAuth();
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
                Wx_Config wxConfigModel = wxConfigBo.Find(ConfigBL.AccountUserId());
                if (wxConfigModel == null)
                {
                    LogService.LogError("微信账号不存在");
                    return false;
                }
                wxConfigModel.access_token = serverTokenAndTicketModel.access_token;
                wxConfigModel.jsapi_ticket = serverTokenAndTicketModel.ticket;
                wxConfigModel.Updatetime = DateTime.Now;
                wxConfigBo.Update(wxConfigModel);
            }
            catch (Exception ex)
            {
                LogService.LogDebug(ex);
            }
            return result;
        }
    }
}