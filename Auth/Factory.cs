using Auth.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth
{
    public class Factory<T, K>
        where T : class
        where K : class
    {
        public static IAuth<T, K> Auth(string className)
        {
            IAuth<T, K> handle = null;
            switch (className)
            {
                case "WxServerAuth":
                    handle = new Wx.WxServerAuth<T, K>();
                    break;
                //case "WxWebAuth":
                //    handle = new Wx.WxWebAuth();
                //    break;
            }
            return handle;
        }
    }
}
