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
    
    public partial class Mobile
    {
        public System.Guid Id { get; set; }
        public string MobileName { get; set; }
        public System.Guid MobileType { get; set; }
        public string MobileTypeName { get; set; }
        public string DPI { get; set; }
        public string MobileNo { get; set; }
        public string Mac { get; set; }
        public System.Guid IDC { get; set; }
        public string Position { get; set; }
        public string AndroidClientId { get; set; }
        public int IsActive { get; set; }
        public System.Guid InputUser { get; set; }
        public System.DateTime InsertTime { get; set; }
        public Nullable<System.Guid> UpdateUser { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
    }
}