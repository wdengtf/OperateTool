//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace YYT.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class QD_ChannelUser
    {
        public int id { get; set; }
        public Nullable<int> channel_id { get; set; }
        public string user_name { get; set; }
        public string user_key { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<System.DateTime> start_time { get; set; }
        public Nullable<System.DateTime> end_time { get; set; }
        public Nullable<System.DateTime> Createtime { get; set; }
        public Nullable<int> validate_ip { get; set; }
    }
}
