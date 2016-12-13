namespace YYT.Model
{
    using System;
    using System.Collections.Generic;

    public class QD_ChannelLogModel
    {
        public long id { get; set; }
        public string channelName { get; set; }
        public string @interface { get; set; }
        public Nullable<int> status { get; set; }
        public string failType { get; set; }
        public string failMessage { get; set; }
        public string RawData { get; set; }
        public string ip { get; set; }
        public Nullable<System.DateTime> Createtime { get; set; }
        public Nullable<System.DateTime> Addtime { get; set; }
    }
}
