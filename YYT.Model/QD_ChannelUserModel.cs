namespace YYT.Model
{
    using System;
    using System.Collections.Generic;

    public class QD_ChannelUserModel
    {
        public int id { get; set; }
        public Nullable<int> channel_id { get; set; }
        public string user_name { get; set; }
        public string user_key { get; set; }
        public Nullable<int> Status { get; set; }
        public string start_time { get; set; }
        public string end_time { get; set; }
        public Nullable<System.DateTime> Createtime { get; set; }
        public Nullable<int> validate_ip { get; set; }
    }
}
