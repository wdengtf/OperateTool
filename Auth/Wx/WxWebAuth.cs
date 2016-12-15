using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auth.Model;
using Framework.Log;
using Framework.Utils;
using Framework;

namespace Auth.Wx
{
    public class WxWebAuth
    {
        private const string appid = "wxb5f424ccbb74ed55";
        private const string appSecret = "419238a2732fdac9641e714f57c62013";
        private WebUtils webUtils = new WebUtils();
        public WxWebAuth()
        { }

        public static string GetCode(string redirectUrl)
        {
            string codeUrl = "";
            try
            {
                codeUrl = String.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_userinfo&state={2}#wechat_redirect", appid, redirectUrl, 1);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return codeUrl;
        }

        public static string GetAccess_token(string code)
        {
            string tokenUrl = "";
            try
            {
                tokenUrl = String.Format("https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code", appid, appSecret, code);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return tokenUrl;
        }

        public static string RefreshToken(string refresh_token)
        {
            string tokenUrl = "";
            try
            {
                tokenUrl = String.Format("https://api.weixin.qq.com/sns/oauth2/refresh_token?appid={0}&grant_type=refresh_token&refresh_token={1}", appid, refresh_token);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return tokenUrl;
        }

        public static string GetUserInfo(string access_token, string open_id)
        {
            string userinfoUrl = "";
            try
            {
                userinfoUrl = String.Format("https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}&lang=zh_CN", access_token, open_id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return userinfoUrl;
        }

        public WxMemberModel WxAuthGetUserInfo(string code)
        {
            WxMemberModel wxMemberModel = null;
            try
            {
                if (String.IsNullOrWhiteSpace(code))
                    return wxMemberModel;

                string tokenPost = webUtils.DoGet(GetAccess_token(code), null);
                if (!tokenPost.Contains("access_token"))
                {
                    LogService.logInfo("获取Access_token失败:" + tokenPost);
                    return wxMemberModel;
                }
                AccessTokenModel accessTokenModel = Utility.JsonToObject<AccessTokenModel>(tokenPost);

                string strUserinfo = webUtils.DoGet(GetUserInfo(accessTokenModel.access_token, accessTokenModel.openid), null);
                strUserinfo = System.Text.Encoding.UTF8.GetString(Encoding.GetEncoding("ISO-8859-1").GetBytes(strUserinfo));

                if (!strUserinfo.Contains("openid"))
                {
                    LogService.logInfo("获取微信用户信息失败:" + strUserinfo);
                    return wxMemberModel;
                }
                wxMemberModel = Utility.JsonToObject<WxMemberModel>(strUserinfo);
            }
            catch (Exception ex)
            {
                LogService.logDebug(ex);
            }
            return wxMemberModel;
        }
    }
}
