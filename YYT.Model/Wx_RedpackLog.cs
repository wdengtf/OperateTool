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
    
    public partial class Wx_RedpackLog
    {
        public int Id { get; set; }
        public Nullable<int> channelUserId { get; set; }
        public Nullable<int> RedpackId { get; set; }
        public string ResultCode { get; set; }
        public string ReturnMsg { get; set; }
        public Nullable<System.DateTime> Createtime { get; set; }
    }
}
