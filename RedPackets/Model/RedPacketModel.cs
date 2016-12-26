using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Wx.RedPackets.Model
{
    public class RedPacketModel
    {
        public RedPacketModel()
        {
            this.postUrl = "https://api.mch.weixin.qq.com/mmpaymkttransfers/sendredpack";
        }

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
        public string wxappid { get; set; }

        /// <summary>
        /// 商户名称
        /// </summary>
        [Required(ErrorMessage = "商户名称不能为空")]
        public string send_name { get; set; }

        /// <summary>
        /// 用户openid
        /// </summary>
        [Required(ErrorMessage = "用户openid不能为空")]
        public string re_openid { get; set; }

        /// <summary>
        /// 付款金额
        /// </summary>
        [Required(ErrorMessage = "付款金额不能为0")]
        public int total_amount { get; set; }

        /// <summary>
        /// 红包发放总人数
        /// </summary>
        [Required(ErrorMessage = "红包发放总人数不能为0")]
        public int total_num { get; set; }

        /// <summary>
        /// 红包金额设置方式   发放裂变红包需要传
        /// ALL_RAND—全部随机,商户指定总金额和红包发放总人数，由微信支付随机计算出各红包金额
        /// </summary>
        public string amt_type { get; set; }

        /// <summary>
        /// 红包祝福语
        /// </summary>
        [Required(ErrorMessage = "红包祝福语不能为空")]
        public string wishing { get; set; }

        /// <summary>
        /// Ip地址
        /// </summary>
        [Required(ErrorMessage = "Ip地址不能为空")]
        public string client_ip { get; set; }

        /// <summary>
        /// 活动名称
        /// </summary>
        [Required(ErrorMessage = "活动名称不能为空")]
        public string act_name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Required(ErrorMessage = "备注不能为空")]
        public string remark { get; set; }

        /// <summary>
        ///场景id 发放红包使用场景，红包金额大于200时必传
        //PRODUCT_1:商品促销
        //PRODUCT_2:抽奖
        //PRODUCT_3:虚拟物品兑奖 
        //PRODUCT_4:企业内部福利
        //PRODUCT_5:渠道分润
        //PRODUCT_6:保险回馈
        //PRODUCT_7:彩票派奖
        //PRODUCT_8:税务刮奖
        /// </summary>
        public string scene_id { get; set; }

        /// <summary>
        /// 活动信息
        /// </summary>
        public string risk_info { get; set; }

        /// <summary>
        /// 资金授权商户号 服务商替特约商户发放时使用
        /// </summary>
        public string consume_mch_id { get; set; }

    }
}
