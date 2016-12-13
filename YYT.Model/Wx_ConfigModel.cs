namespace YYT.Model
{
    using System;
    using System.Collections.Generic;

    public class Wx_ConfigModel
    {
        public int Id { get; set; }
        public Nullable<int> channelUserId { get; set; }
        public string wxappid { get; set; }
        public string mch_id { get; set; }
        public string wxkey { get; set; }
        public Nullable<System.DateTime> Createtime { get; set; }
    }
}
