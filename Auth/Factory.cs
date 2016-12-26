using Auth.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Events;
using Auth;

namespace Auth
{
    public class Factory
    {
        private Factory()
        {

        }

        public static IAuth<T, M> Auth<T, M>() where T : class where M : class
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

        //public static IAuth<DBNull, ServerTokenAndTicketModel> WxServerAuth()
        //{
        //    return new Wx.WxServerAuth();
        //}

        //public static IAuth<String, WxMemberModel> WxWebAuth()
        //{
        //    return new Wx.WxWebAuth();
        //}
    }
}
