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


        /// <summary>
        /// 跳过权限组id
        /// </summary>
        /// <returns></returns>
        public static int JumpDroitGroupId()
        {
            return int.Parse(GetConfig("JumpDroitGroupId"));
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
