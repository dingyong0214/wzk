using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WZK.Admin
{
    /// <summary>
    /// 活动培训维护类
    /// </summary>
    /// <remarks>author:wcy date:2016-03-10</remarks>
    public class ModifyCourseInfo
    {

        /// <summary>
        ///编号
        ///</summary>
        public System.Guid Id { get; set; }
        /// <summary>
    	///名称
    	///</summary>
        [Required(ErrorMessage = "请输入名称")]
        [StringLength(32, ErrorMessage = "名称长度不能超过32")]
        public string Name { get; set; }
        /// <summary>
    	///报名开始日期
    	///</summary>
        [Required(ErrorMessage = "请选择报名开始日期")]
        public System.DateTime BaomingDateStart { get; set; } = DateTime.Now;
        /// <summary>
    	///报名结束日期
    	///</summary>
        [Required(ErrorMessage = "请选择报名结束日期")]
        public System.DateTime BaomingDateEnd { get; set; } = DateTime.Now;
        /// <summary>
    	///课程考级开始日期
    	///</summary>
        [Required(ErrorMessage = "请选择考级开始日期")]
        public System.DateTime DateSart { get; set; } = DateTime.Now;
        /// <summary>
    	///课程考级结束日期
    	///</summary>
        [Required(ErrorMessage = "请选择考级结束日期")]
        public System.DateTime DateEnd { get; set; } = DateTime.Now;
        /// <summary>
    	///费用
    	///</summary>
        [Required(ErrorMessage = "请输入费用")]
        [RegularExpression(@"^(0|[1-9][0-9]*)(.[0-9]{1,2})?$", ErrorMessage = "费用不能为负数")]
        public decimal Amount { get; set; }
        /// <summary>
    	///课程考级地点
    	///</summary>
        public string Address { get; set; }
        /// <summary>
    	///课程考级标志：1-培训 2-考级
    	///</summary>
        public int Flag { get; set; }
        /// <summary>
    	///介绍
    	///</summary>
       // [Required(ErrorMessage = "请输入介绍")]
        public string Description { get; set; }

    }
}
