namespace YYT.Model
{
    using System;
    using System.Collections.Generic;

    public class Luck_ActivityModel
    {
        public int Id { get; set; }
        public Nullable<int> channelUserId { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> Startdate { get; set; }
        public Nullable<System.DateTime> Enddate { get; set; }
        public Nullable<int> Rules { get; set; }
        public string Img { get; set; }
        public string Url { get; set; }
        public Nullable<int> Status { get; set; }
        public string Introduction { get; set; }
        public string descr { get; set; }
        public Nullable<System.DateTime> Createtime { get; set; }
    }
}
