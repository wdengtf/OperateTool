using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Payment.PayModel
{
    /// <summary>
    /// 微信红包查询对象
    /// </summary>
    public class RedPacketQueryModel
    {
        public RedPacketQueryModel()
        {
            this.postUrl = "https://api.mch.weixin.qq.com/mmpaymkttransfers/gethbinfo";//请求Url
            this.bill_type = "MCHT";//MCHT:通过商户订单号获取红包信息。
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
        /// 商户订单号（每个订单号必须唯一）
        /// 组成：mch_id+yyyymmdd+10位一天内不能重复的数字。
        /// 接口根据商户订单号支持重入，如出现超时可再调用。
        /// </summary>
        [Required(ErrorMessage = "商户订单号不能为空")]
        public string mch_billno { get; set; }

        /// <summary>
        /// 商户号
        /// </summary>
        [Required(ErrorMessage = "商户号不能为空")]
        public string mch_id { get; set; }

        /// <summary>
        /// 公众账号appid
        /// </summary>
        [Required(ErrorMessage = "公众账号appid不能为空")]
        public string appid { get; set; }

        /// <summary>
        /// 订单类型
        /// MCHT:通过商户订单号获取红包信息
        /// </summary>
        [Required(ErrorMessage = "Appid不能为空")]
        public string bill_type { get; set; }
    }
}
