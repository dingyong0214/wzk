//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace WZK.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class RealtimeControl
    {
        public System.Guid Id { get; set; }
        public System.Guid MobileId { get; set; }
        public string Shootme { get; set; }
        public Nullable<int> IsUsed { get; set; }
        public Nullable<System.Guid> Flag { get; set; }
        public System.Guid InputUser { get; set; }
        public System.DateTime InsertTime { get; set; }
    }
}