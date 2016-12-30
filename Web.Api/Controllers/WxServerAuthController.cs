using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web.Api.Base;
using YYT.Model;
using YYT.Model.Auth;

namespace Web.Api.Controllers
{
    public class WxServerAuthController : ApiController
    {
        /// <summary>
        /// 微信公众号授权接口
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public HttpResponseMessage Post([FromBody] WxServerAuthModel req)
        {
            IBase<WxServerAuthModel, ServerTokenAndTicketModel> ibase = Auth.Factory.WxServerAuth(req);
            JsonResult re = BaseApi.MainExcute(req, ibase);
            return Request.CreateResponse(HttpStatusCode.OK, re);
        }

    }
}
