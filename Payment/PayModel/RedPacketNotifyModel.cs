using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.PayModel
{
    /// <summary>
    /// 微信红包发送成功返回
    /// </summary>
    public class RedPacketNotifyModel
    {
        /// <summary>
        /// 返回状态码
        /// </summary>
        public string return_code { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        public string return_msg { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }

       /// <summary>
        /// 业务结果
       /// </summary>

        public string result_code { get; set; }

        /// <summary>
        /// 错误代码
        /// </summary>
        public string err_code { get; set; }

        /// <summary>
        /// 错误代码描述
        /// </summary>
        public string err_code_des { get; set; }

        /// <summary>
        /// 商户订单号
        /// </summary>
        public string mch_billno { get; set; }

        /// <summary>
        /// 商户号
        /// </summary>
        public string mch_id { get; set; }

        /// <summary>
        /// 公众账号appid
        /// </summary>
        public string wxappid { get; set; }

        /// <summary>
        /// 用户openid
        /// </summary>
        public string re_openid { get; set; }

        /// <summary>
        /// 付款金额
        /// </summary>
        public int total_amount { get; set; }

        /// <summary>
        /// 微信单号
        /// </summary>
        public string send_listid { get; set; }

    }
}
