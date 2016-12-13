namespace YYT.Model
{
    using System;
    using System.Collections.Generic;

    public class Luck_ActivityPrizeModel
    {
        public int Id { get; set; }
        public Nullable<int> channelUserId { get; set; }
        public string name { get; set; }
        public Nullable<int> sortid { get; set; }
        public Nullable<decimal> price { get; set; }
        public Nullable<int> PrizeType { get; set; }
        public string PrizeLevel { get; set; }
        public Nullable<int> num { get; set; }
        public Nullable<int> winNum { get; set; }
        public string PrizeUrl { get; set; }
        public string PrizeImg { get; set; }
        public Nullable<int> Status { get; set; }
        public string Introduction { get; set; }
        public string descr { get; set; }
        public Nullable<System.DateTime> Createtime { get; set; }
        public Nullable<int> Position { get; set; }
        public string PositionImg { get; set; }
    }
}
