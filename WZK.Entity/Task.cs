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
    
    public partial class Task
    {
        public System.Guid Id { get; set; }
        public string TaskName { get; set; }
        public string TaskDesc { get; set; }
        public int TaskType { get; set; }
        public System.Guid TaskTemplateId { get; set; }
        public string TemplateName { get; set; }
        public System.DateTime StartTime { get; set; }
        public int ExecuteCycle { get; set; }
        public int ExecuteTimes { get; set; }
        public int IsComplete { get; set; }
        public System.Guid InputUser { get; set; }
        public System.DateTime InsertTime { get; set; }
        public Nullable<System.Guid> UpdateUser { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
    }
}
