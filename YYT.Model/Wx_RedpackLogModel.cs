namespace YYT.Model
{
    using System;
    using System.Collections.Generic;

    public class Wx_RedpackLogModel
    {
        public int Id { get; set; }
        public Nullable<int> channelUserId { get; set; }
        public Nullable<int> RedpackId { get; set; }
        public string ResultCode { get; set; }
        public string ReturnMsg { get; set; }
        public Nullable<System.DateTime> Createtime { get; set; }
    }
}
