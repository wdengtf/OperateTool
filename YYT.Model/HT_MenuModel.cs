namespace YYT.Model
{
    using System;
    using System.Collections.Generic;

    public class HT_Menu
    {
        public int id { get; set; }
        public string Title { get; set; }
        public Nullable<int> isMenu { get; set; }
        public string Droit { get; set; }
        public string Url { get; set; }
        public Nullable<int> SortId { get; set; }
        public Nullable<int> Pid { get; set; }
        public Nullable<int> status { get; set; }
        public Nullable<System.DateTime> createtime { get; set; }
    }
}