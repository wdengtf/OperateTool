using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace YYT.Model.Pay
{
    public class CompanyPaymentQueryModel
    {
        public CompanyPaymentQueryModel()
        {
            this.postUrl = "https://api.mch.weixin.qq.com/mmpaymkttransfers/gettransferinfo";
        }

        /// <summary>
        /// 渠道用户名
        /// </summary>
        [Required(ErrorMessage = "渠道用户名不能为空")]
        public string channelUser { get; set; }

        /// <summary>
        /// 用户密钥
        /// </summary>
        [Required(ErrorMessage = "用户密钥不能为空")]
        public string key { get; set; }

        /// <summary>
        /// 请求Url
        /// </summary>
        [Required(ErrorMessage = "请求Url不能为空")]
        public string postUrl { get; set; }

        /// <summary>
        /// 随机字符串
        /// </summary>
        [Required(ErrorMessage = "随机字符串不能为空")]
        public string nonce_str { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        [Required(ErrorMessage = "签名不能为空")]
        public string sign { get; set; }


        /// <summary>
        /// 商户订单号
        /// </summary>
        [Required(ErrorMessage = "商户订单号不能为空")]
        public string partner_trade_no { get; set; }

        /// <summary>
        /// 商户号
        /// </summary>
        [Required(ErrorMessage = "商户号不能为空")]
        public string mch_id { get; set; }

        /// <summary>
        /// 公众账号appid
        /// </summary>
        [Required(ErrorMessage = "商户号的appid不能为空")]
        public string appid { get; set; }
    }
}
