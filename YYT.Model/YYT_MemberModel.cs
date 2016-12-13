namespace YYT.Model
{
    using System;
    using System.Collections.Generic;

    public class YYT_MemberModel
    {
        public int Id { get; set; }
        public string out_id { get; set; }
        public Nullable<int> channelUserId { get; set; }
        public string userName { get; set; }
        public string data_type { get; set; }
        public string Mobile { get; set; }
        public string email { get; set; }
        public Nullable<int> Sex { get; set; }
        public Nullable<int> Status { get; set; }
        public string nickname { get; set; }
        public string country { get; set; }
        public string province { get; set; }
        public string city { get; set; }
        public string area { get; set; }
        public string addr { get; set; }
        public string headimgurl { get; set; }
        public string unionid { get; set; }
        public Nullable<System.DateTime> Createtime { get; set; }
        public Nullable<System.DateTime> Updatetime { get; set; }
    }
}
