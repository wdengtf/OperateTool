using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Payment.PayModel
{
    /// <summary>
    /// 微信企业付款
    /// </summary>
    public class CompanyPaymentModel
    {
        public CompanyPaymentModel()
        {
            this.postUrl = "https://api.mch.weixin.qq.com/mmpaymkttransfers/promotion/transfers";
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
        /// 公众账号appid
        /// </summary>
        [Required(ErrorMessage = "公众账号appid不能为空")]
        public string mch_appid { get; set; }

        /// <summary>
        /// 商户号
        /// </summary>
        [Required(ErrorMessage = "商户号不能为空")]
        public string mchid { get; set; }

        /// <summary>
        /// 设备号
        /// </summary>
        public string device_info { get; set; }

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
        /// 用户openid
        /// </summary>
        [Required(ErrorMessage = "用户openid不能为空")]
        public string openid { get; set; }

        /// <summary>
        /// 校验用户姓名选项 
        /// NO_CHECK：不校验真实姓名 
        /// FORCE_CHECK：强校验真实姓名（未实名认证的用户会校验失败，无法转账） 
        /// OPTION_CHECK：针对已实名认证的用户才校验真实姓名（未实名认证用户不校验，可以转账成功）
        /// </summary>
        [Required(ErrorMessage = "校验用户姓名选项不能为空")]
        public string check_name { get; set; }

        /// <summary>
        /// 收款用户姓名
        /// </summary>
        public string re_user_name { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        [Required(ErrorMessage = "金额不能为0")]
        public int amount { get; set; }

        /// <summary>
        /// 企业付款描述信息
        /// </summary>
        [Required(ErrorMessage = "企业付款描述信息不能为空")]
        public string desc { get; set; }

        /// <summary>
        /// Ip地址
        /// </summary>
        [Required(ErrorMessage = "Ip地址不能为空")]
        public string spbill_create_ip { get; set; }
    }
}
