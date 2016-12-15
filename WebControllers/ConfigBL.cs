using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebControllers
{
    public class ConfigBL
    {
        /// <summary>
        /// 微信授权Url
        /// </summary>
        /// <returns></returns>
        public static string WxAuthUrl()
        {
            return GetConfig("wxAuthUrl");
        }


        private static string GetConfig(string strName)
        {
            string str = "";
            try
            {
                if (String.IsNullOrEmpty(strName))
                    return str;

                str = ConfigurationManager.AppSettings[strName].ToString();
            }
            catch { str = ""; }
            return str;
        }
    }
}
