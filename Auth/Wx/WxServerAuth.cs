using Auth.Model;
using Events;
using Framework;
using Framework.Log;
using Framework.Model;
using Framework.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Wx
{
    /// <summary>
    /// 微信公众号授权
    /// </summary>
    public class WxServerAuth
    {
        private MethodBase methodBase = System.Reflection.MethodBase.GetCurrentMethod();
        protected string ClassName = "微信公众号授权";

        private const string appid = WxConfig.appid;
        private const string appSecret = WxConfig.appSecret;
        private WebUtils webUtils = new WebUtils();
        public WxServerAuth()
        { }

        /// <summary>
        /// 获取access token
        /// </summary>
        /// <param name="redirectUrl"></param>
        /// <returns></returns>
        private static string GetAccess_token()
        {
            string accessTokenUrl = "";
            try
            {
                accessTokenUrl = String.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", appid, appSecret);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return accessTokenUrl;
        }

        /// <summary>
        /// 根据access_token 获取jsapi_ticket
        /// </summary>
        /// <param name="access_token"></param>
        /// <returns></returns>
        private static string GetJsapiTicket(string access_token)
        {
            string jsapiTicketUrl = "";
            try
            {
                jsapiTicketUrl = String.Format("https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={0}&type=jsapi", access_token);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return jsapiTicketUrl;
        }

        /// <summary>
        /// 获取公众号AccessToken和JsapiTicket
        /// </summary>
        /// <returns></returns>
        public ServerTokenAndTicketModel GetServerTokenAndTicken()
        {
            ServerTokenAndTicketModel serverTicketModel = null;
            //OperationFilePath = methodBase.DeclaringType.FullName + "." + methodBase.Name;
            //OperationName = "获取" + ClassName + "数据";
            try
            {
                //BaseEvent(EventEnum.OnBegin);

                string accessToken = webUtils.DoGet(GetAccess_token(),null);
                if (!accessToken.Contains("access_token"))
                {
                    LogService.LogInfo("获取Access_token失败:" + accessToken);
                    return serverTicketModel;
                }
                AccessTokenServerModel accessTokenModel = Utility.JsonToObject<AccessTokenServerModel>(accessToken);
                string jsapiTicket = webUtils.DoGet(GetJsapiTicket(accessTokenModel.access_token), null);
                if (!jsapiTicket.Contains("ticket"))
                {
                    LogService.LogInfo("获取jsapi_ticket信息失败:" + jsapiTicket);
                    return serverTicketModel;
                }
                serverTicketModel = Utility.JsonToObject<ServerTokenAndTicketModel>(jsapiTicket);
                //BaseEvent(EventEnum.OnSuccess);
            }
            catch (Exception ex)
            {
                LogService.LogDebug(ex);
                //Description = ex.ToString();
                //BaseEvent(EventEnum.OnException);
            }
            //BaseEvent(EventEnum.OnCompelete);
            return serverTicketModel;
        }

    }
}
