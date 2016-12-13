namespace YYT.Model
{
    using System;
    using System.Collections.Generic;

    public class Luck_ActivityJackpotModel
    {
        public int id { get; set; }
        public Nullable<int> channelUserId { get; set; }
        public Nullable<int> ActivityId { get; set; }
        public Nullable<int> PrizeId { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<System.DateTime> Createtime { get; set; }
        public string Ip { get; set; }
        public string data_type { get; set; }
        public string out_id { get; set; }
        public Nullable<System.DateTime> Updatetime { get; set; }
        public Nullable<System.DateTime> UpdateAddtime { get; set; }
    }
}
