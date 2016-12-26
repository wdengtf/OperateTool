using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.PayModel
{
    public class CompanyPaymentQueryNotifyModel
    {
        public CompanyPaymentQueryNotifyModel()
        {

        }

        /// <summary>
        /// 返回状态码
        /// </summary>
        public string return_code { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        public string return_msg { get; set; }

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
        /// 商户单号
        /// </summary>
        public string partner_trade_no { get; set; }
        /// <summary>
        /// 商户号
        /// </summary>
        public string mch_id { get; set; }

        /// <summary>
        /// 付款单号调用企业付款API时，微信系统内部产生的单号
        /// </summary>
        public string detail_id { get; set; }
        /// <summary>
        /// 转账状态
        /// SUCCESS:转账成功
        /// FAILED:转账失败
        /// PROCESSING:处理中
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// 失败原因
        /// </summary>
        public string reason { get; set; }
        /// <summary>
        /// 收款用户openid
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 收款用户姓名
        /// </summary>
        public string transfer_name { get; set; }
        /// <summary>
        /// 付款金额
        /// </summary>
        public int payment_amount { get; set; }
        /// <summary>
        /// 转账时间
        /// </summary>
        public string transfer_time { get; set; }
        /// <summary>
        /// 付款描述
        /// </summary>
        public string desc { get; set; }
    }
}
