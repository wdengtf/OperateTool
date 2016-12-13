using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebControllers.Model
{
    /// <summary>
    /// 操作权限对象
    /// </summary>
    public class ManageDroitModel
    {
        /// <summary>
        /// 栏目ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 栏目名称
        /// </summary>
        public string Rs_title { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        public string Droit { get; set; }

        /// <summary>
        /// 父ID
        /// </summary>
        public int Pid { get; set; }

        /// <summary>
        /// URL
        /// </summary>
        public string Url { get; set; }
    }
}
