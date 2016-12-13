namespace YYT.Model
{
    using System;
    using System.Collections.Generic;

    public class WxCompany_PaymentModel
    {
        public int Id { get; set; }
        public Nullable<int> channelUserId { get; set; }
        public Nullable<int> WxConfigId { get; set; }
        public string partner_trade_no { get; set; }
        public string openid { get; set; }
        public string check_name { get; set; }
        public string re_user_name { get; set; }
        public Nullable<decimal> amount { get; set; }
        public string descr { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<System.DateTime> Createtime { get; set; }
        public Nullable<System.DateTime> Addtime { get; set; }
        public string payment_no { get; set; }
        public Nullable<System.DateTime> payment_time { get; set; }
        public Nullable<System.DateTime> Updatetime { get; set; }
    }
}
