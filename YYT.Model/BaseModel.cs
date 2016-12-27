using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace YYT.Model
{
    public class BaseModel
    {
        public BaseModel()
        { }

        /// <summary>
        /// 渠道签名
        /// </summary>
        [Required(ErrorMessage = "签名不能为空")]
        public string channelSign { get; set; }

        /// <summary>
        /// 渠道用户名
        /// </summary>
        [Required(ErrorMessage = "渠道用户名不能为空")]
        public string channelUser { get; set; }
    }
}
