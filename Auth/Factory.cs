using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Events;
using Auth;
using YYT.Model.Auth;

namespace Auth
{
    public class Factory
    {
        private Factory()
        {

        }

        #region 用户授权 外部调用 未验证签名
        /// <summary>
        /// 微信公众号授权
        /// </summary>
        /// <returns></returns>
        public static AuthCall<WxServerAuthModel, ServerTokenAndTicketModel> WxServerAuth(WxServerAuthModel wxServerAuthModel)
        {
            return new AuthCall<WxServerAuthModel, ServerTokenAndTicketModel>();
        }

        /// <summary>
        /// 微信网页授权
        /// </summary>
        /// <param name="wxWebAuthModel"></param>
        /// <returns></returns>
        public static AuthCall<WxWebAuthModel, WxMemberModel> WxWebAuth(WxWebAuthModel wxWebAuthModel)
        {
            return new AuthCall<WxWebAuthModel, WxMemberModel>();
        }
        #endregion


        public static IAuth<T, M> Auth<T, M>()
            where T : class
            where M : class
        {
            IAuth<T, M> iAuth = null;
            if (typeof(M) == typeof(ServerTokenAndTicketModel))
            {
                iAuth = new Wx.WxServerAuth<T, M>();
            }
            else if (typeof(M) == typeof(WxMemberModel))
            {
                iAuth = new Wx.WxWebAuth<T, M>();
            }
            return iAuth;
        }
    }
}
