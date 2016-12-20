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
    /// <summary>
    /// 微信网页授权
    /// </summary>
    public class WxWebAuth
    {
        private const string appid = WxConfig.appid;
        private const string appSecret = WxConfig.appSecret;
        private WebUtils webUtils = new WebUtils();
        public WxWebAuth()
        { }

        /// <summary>
        /// 获取微信Code
        /// </summary>
        /// <param name="redirectUrl"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 获取微信Access_token
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 刷新token
        /// </summary>
        /// <param name="refresh_token"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="open_id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 保存微信信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
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
                    LogService.LogInfo("获取Access_token失败:" + tokenPost);
                    return wxMemberModel;
                }
                AccessTokenWebModel accessTokenModel = Utility.JsonToObject<AccessTokenWebModel>(tokenPost);

                string strUserinfo = webUtils.DoGet(GetUserInfo(accessTokenModel.access_token, accessTokenModel.openid), null);
                //strUserinfo = System.Text.Encoding.UTF8.GetString(Encoding.GetEncoding("ISO-8859-1").GetBytes(strUserinfo));

                if (!strUserinfo.Contains("openid"))
                {
                    LogService.LogInfo("获取微信用户信息失败:" + strUserinfo);
                    return wxMemberModel;
                }
                wxMemberModel = Utility.JsonToObject<WxMemberModel>(strUserinfo);
            }
            catch (Exception ex)
            {
                LogService.LogDebug(ex);
            }
            return wxMemberModel;
        }
    }
}
