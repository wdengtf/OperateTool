using Auth.Model;
using Framework;
using Framework.EF;
using Framework.Log;
using Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using WebControllers;
using YYT.BLL;
using YYT.Model;
using WebControllers.Member;

namespace Web.Data
{
    /// <summary>
    /// Wechat 的摘要说明
    /// </summary>
    public class Wechat : BaseHandle
    {
        private Wx_ConfigBO wxConfigBo = new Wx_ConfigBO();
        public override JsonResult HandleProcess()
        {
            JsonResult re = new JsonResult();
            switch (action)
            {
                case "wxConfig":
                    re = WxConfig();
                    break;
            }
            return re;
        }

        private JsonResult WxConfig()
        {
            JsonResult re = new JsonResult();
            try
            {
                Expression<Func<Wx_Config, bool>> where = PredicateExtensionses.True<Wx_Config>();
                int accountUserId = ConfigBL.AccountUserId();
                where = where.AndAlso(p => p.channelUserId == accountUserId);
                Wx_Config wxConfigModel = wxConfigBo.GetSingle<int>(where);

                WxCommonModel wxCommonModel = new WxCommonModel();
                wxCommonModel.jsapi_ticket = wxConfigModel.jsapi_ticket;

                string strUrl = Utility.RF("url").Split('#')[0];
                wxCommonModel.signature = Auth.Wx.WxConfig.GetConfigSign(wxCommonModel.jsapi_ticket, wxCommonModel.timestamp, wxCommonModel.nonceStr, strUrl).ToLower();
                re = JsonResult.SuccessResult(wxCommonModel);
            }
            catch (Exception ex)
            {
                re = JsonResult.FailResult(MsgShowConfig.Exception);
                LogService.LogDebug(ex);
            }
            return re;
        }
    }
}