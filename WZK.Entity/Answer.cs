//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace CA.DAL
{
    using System;
    using System.Collections.Generic;
    
    /// <summary>
    ///问题回答
    ///</summary>
    public partial class Answer
    {
        /// <summary>
    	///编号
    	///</summary>
        public System.Guid Id { get; set; }
        /// <summary>
    	///问题编号
    	///</summary>
        public System.Guid QuestionId { get; set; }
        /// <summary>
    	///用户编号
    	///</summary>
        public System.Guid UserId { get; set; }
        /// <summary>
    	///回复谁
    	///</summary>
        public Nullable<System.Guid> ToUserId { get; set; }
        /// <summary>
    	///回答内容
    	///</summary>
        public string Content { get; set; }
        /// <summary>
    	///状态：1-未读 2-已读
    	///</summary>
        public int State { get; set; }
        /// <summary>
    	///阅读时间
    	///</summary>
        public Nullable<System.DateTime> ReadTime { get; set; }
        /// <summary>
    	///入库时间
    	///</summary>
        public System.DateTime InsertTime { get; set; }
        /// <summary>
    	///更新时间
    	///</summary>
        public Nullable<System.DateTime> UpdateTime { get; set; }
    }
}
