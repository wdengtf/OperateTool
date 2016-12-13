namespace YYT.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;


    public class HT_Account
    {
        public int id { get; set; }
        public string username { get; set; }
        public string pwd { get; set; }
        public Nullable<int> groupid { get; set; }
        public Nullable<int> status { get; set; }
        public Nullable<System.DateTime> createtime { get; set; }
    }
}
