
namespace YYT.Model
{
    using System;
    using System.Collections.Generic;

    public class QD_ChanneWhilteModel
    {
        public int id { get; set; }
        public Nullable<int> channel_id { get; set; }
        public Nullable<int> user_id { get; set; }
        public string ip { get; set; }
        public Nullable<System.DateTime> Createtime { get; set; }
    }
}
