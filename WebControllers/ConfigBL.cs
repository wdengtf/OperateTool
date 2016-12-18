using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework;

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
            return Utility.GetConfig("wxAuthUrl");
        }

        /// <summary>
        /// 用户id
        /// </summary>
        /// <returns></returns>
        public static int AccountUserId()
        {
            return int.Parse(Utility.GetConfig("accountUserId"));
        }


        /// <summary>
        /// 跳过权限组id
        /// </summary>
        /// <returns></returns>
        public static int JumpDroitGroupId()
        {
            return int.Parse(Utility.GetConfig("JumpDroitGroupId"));
        }
    }
}
