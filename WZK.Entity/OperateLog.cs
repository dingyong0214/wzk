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
    
    public partial class OperateLog
    {
        public System.Guid Id { get; set; }
        public System.Guid UserId { get; set; }
        public string RoleName { get; set; }
        public string MenuName { get; set; }
        public string OperateDesc { get; set; }
        public string IP { get; set; }
        public System.DateTime OperateTime { get; set; }
        public string OldBussnessId { get; set; }
        public string OldTableName { get; set; }
    }
}
