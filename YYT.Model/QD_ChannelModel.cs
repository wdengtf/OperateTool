namespace YYT.Model
{
    using System;
    using System.Collections.Generic;

    public class QD_ChannelModel
    {
        public int id { get; set; }
        public Nullable<int> category { get; set; }
        public string name { get; set; }
        public Nullable<int> status { get; set; }
        public string remark { get; set; }
        public Nullable<System.DateTime> Createtime { get; set; }
    }
}
