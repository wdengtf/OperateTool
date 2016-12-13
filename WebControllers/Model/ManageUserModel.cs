using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebControllers.Model
{
    public class ManageUserModel
    {
        /// <summary>
        /// 后台会员id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 后台用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 后台用户所有权限ID
        /// </summary>
        public string UserDroit { get; set; }
    }
}
