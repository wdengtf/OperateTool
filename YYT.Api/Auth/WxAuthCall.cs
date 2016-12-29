
using Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YYT.Model.Auth;

namespace YYT.Api.Auth
{

    /// <summary>
    /// 微信授权
    /// </summary>
    public class WxAuthCall
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public WxAuthCall()
        {
        }

        /// <summary>
        /// 微信公众号授权
        /// </summary>
        /// <returns></returns>
        public static AuthCall<WxServerAuthModel, ServerTokenAndTicketModel> WxServerAuth(WxServerAuthModel wxServerAuthModel)
        {
            AuthCall<WxServerAuthModel, ServerTokenAndTicketModel> authCall = new AuthCall<WxServerAuthModel, ServerTokenAndTicketModel>();

            return authCall;

            //authCall.Set(wxServerAuthModel);
            //ServerTokenAndTicketModel serverTokenAndTicketModel = authCall.Auth();
            //if (authCall.GetResultState())
            //    return serverTokenAndTicketModel;
            //else
            //    return null;
        }

    }
}
