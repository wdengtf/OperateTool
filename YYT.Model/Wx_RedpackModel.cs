namespace YYT.Model
{
    using System;
    using System.Collections.Generic;

    public class Wx_RedpackModel
    {
        public int Id { get; set; }
        public Nullable<int> channelUserId { get; set; }
        public Nullable<int> WxConfigId { get; set; }
        public string mch_billno { get; set; }
        public string send_name { get; set; }
        public string openid { get; set; }
        public Nullable<decimal> total_amount { get; set; }
        public Nullable<int> total_num { get; set; }
        public string amt_type { get; set; }
        public string wishing { get; set; }
        public string act_name { get; set; }
        public string remark { get; set; }
        public string scene_id { get; set; }
        public string risk_info { get; set; }
        public string consume_mch_id { get; set; }
        public Nullable<System.DateTime> Createtime { get; set; }
        public Nullable<System.DateTime> Addtime { get; set; }
        public Nullable<int> Status { get; set; }
        public string send_listid { get; set; }
        public Nullable<System.DateTime> Updatetime { get; set; }
    }
}
