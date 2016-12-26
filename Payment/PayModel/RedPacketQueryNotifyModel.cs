using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.PayModel
{
    /// <summary>
    /// 微信红包查询成功返回
    /// </summary>
    public class RedPacketQueryNotifyModel
    {
        public RedPacketQueryNotifyModel()
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
        /// 红包单号 使用API发放现金红包时返回的红包单号
        /// </summary>
        public string detail_id { get; set; }

        /// <summary>
        /// 红包状态
        /// SENDING:发放中 
        /// SENT:已发放待领取 
        /// FAILED：发放失败 
        /// RECEIVED:已领取 
        /// RFUND_ING:退款中 
        /// REFUND:已退款
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// 发放类型
        /// API:通过API接口发放 
        /// UPLOAD:通过上传文件方式发放 
        /// ACTIVITY:通过活动方式发放
        /// </summary>
        public string send_type { get; set; }

        /// <summary>
        /// 红包类型
        /// GROUP:裂变红包 
        /// NORMAL:普通红包
        /// </summary>
        public string hb_type { get; set; }

        /// <summary>
        /// 红包个数
        /// </summary>
        public int total_num { get; set; }

        /// <summary>
        /// 红包金额
        /// </summary>
        public int total_amount { get; set; }

        /// <summary>
        /// 失败原因
        /// </summary>
        public string reason { get; set; }

        /// <summary>
        /// 红包发送时间
        /// </summary>
        public string send_time { get; set; }

        /// <summary>
        /// 红包退款时间
        /// </summary>
        public string refund_time { get; set; }

        /// <summary>
        /// 红包退款金额
        /// </summary>
        public int refund_amount { get; set; }
        /// <summary>
        /// 祝福语
        /// </summary>
        public string wishing { get; set; }

        /// <summary>
        /// 活动描述
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 活动名称
        /// </summary>
        public string act_name { get; set; }

        /// <summary>
        /// 裂变红包领取列表
        /// </summary>
        public List<HblbModel> hblist { get; set; }

        /// <summary>
        /// 领取红包的Openid
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public int amount { get; set; }

        /// <summary>
        /// 接收时间
        /// </summary>
        public string rcv_time { get; set; }

    }

    /// <summary>
    /// 裂变红包对象
    /// </summary>
    public class HblbModel
    {
        public string openid { get; set; }

        public int amount { get; set; }

        public string rcv_time { get; set; }
    }
}
