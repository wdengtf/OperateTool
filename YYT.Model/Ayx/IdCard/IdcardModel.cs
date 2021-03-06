﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YYT.Model.Ayx.IdCard
{
    public class IdcardModel:BaseModel 
    {
        public IdcardModel()
        {
            this.postUrl = AyxConfig.postIdCardUrl;
            this.appkey = AyxConfig.appkey;
            this.output = AyxConfig.output;
        }

        /// <summary>
        /// 请求Url
        /// </summary>
        [Required(ErrorMessage = "请求Url不能为空")]
        public string postUrl { get; set; }

        /// <summary>
        /// appkey
        /// </summary>
        [Required(ErrorMessage = "appkey不能为空")]
        public string appkey { get; set; }

        /// <summary>
        /// 姓名  需进行urlEncode编码
        /// </summary>
        [Required(ErrorMessage = "姓名不能为空")]
        public string name { get; set; }

        /// <summary>
        /// 身份证号码
        /// </summary>
        [Required(ErrorMessage = "身份证号码不能为空")]
        public string cardno { get; set; }

        /// <summary>
        /// 输出格式：json/xml，默认json
        /// </summary>
        public string output { get; set; }
    }
}
