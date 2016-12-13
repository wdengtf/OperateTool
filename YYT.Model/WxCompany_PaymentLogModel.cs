namespace YYT.Model
{
    using System;
    using System.Collections.Generic;

    public class WxCompany_PaymentLogModel
    {
        public int Id { get; set; }
        public Nullable<int> channelUserId { get; set; }
        public Nullable<int> PaymentId { get; set; }
        public string ResultCode { get; set; }
        public string ReturnMsg { get; set; }
        public Nullable<System.DateTime> Createtime { get; set; }
    }
}
