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
    
    public partial class AutoReply
    {
        public System.Guid Id { get; set; }
        public System.Guid TemplateId { get; set; }
        public string AskNo { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Pic { get; set; }
        public Nullable<int> PicIndex { get; set; }
        public int OrderNo { get; set; }
        public System.Guid InputUser { get; set; }
        public System.DateTime InsertTime { get; set; }
    }
}
