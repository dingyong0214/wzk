using System;
using System.ComponentModel.DataAnnotations;

namespace WZK.Admin.Models.Task
{
    public class ModifyTask
    {
        /// <summary>
        /// 编号
        /// </summary>
        public System.Guid Id { get; set; }
        /// <summary>
        /// 任务名称
        /// </summary>
        [Display(Name = "任务名称")]
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(25, ErrorMessage = "{0}长度不能超过25个字符")]
        public string TaskName { get; set; }
        /// <summary>
        /// 任务描述
        /// </summary>
        [Display(Name = "任务描述")]
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(250, ErrorMessage = "{0}长度不能超过250个字符")]
        public string TaskDesc { get; set; }
        /// <summary>
        /// 任务类型
        /// </summary>
        public int TaskType { get; set; }
        /// <summary>
        /// 任务模板编号
        /// </summary>
        public System.Guid TaskTemplateId { get; set; }
        /// <summary>
        /// 任务模板名称
        /// </summary>
        [Required(ErrorMessage = "请选择任务模板")]
        public string TemplateName { get; set; }
        /// <summary>
        /// 任务开始时间
        /// </summary>
        [Display(Name ="任务开始时间")]
        [Required(ErrorMessage = "{0}不能为空")]
        
        public System.DateTime StartTime { get; set; } = DateTime.Now.AddMinutes(30);
        /// <summary>
        /// 执行周期:单位分钟  0-无周期，任务只执行1次，大于0 - n分钟执行一次
        /// </summary>
        [Required(ErrorMessage = "执行周期不能为空")]
        [RegularExpression("^\\d*$", ErrorMessage = "请输入数字,且不能为小数或负数")]
        [Range(0, 10080, ErrorMessage = "请输入0-10080以内数字")]
        public int ExecuteCycle { get; set; } = 0;
        /// <summary>
        /// 需要执行的次数：0-无限次，大于0-n次
        /// </summary>
        [Required(ErrorMessage = "需要执行的次数不能为空")]
        [RegularExpression("^\\d*$", ErrorMessage = "请输入数字,且不能为小数或负数")]
        [Range(0, 10000, ErrorMessage = "请输入0-10000以内数字")]
        public int ExecuteTimes { get; set; } = 1;
        /// <summary>
        /// 是否完成：0-未完成，1-已完成
        /// </summary>
        public int IsComplete { get; set; } = 0;
        /// <summary>
        /// 录入人
        /// </summary>
        public System.Guid InputUser { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        public System.DateTime InsertTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 修改人
        /// </summary>
        public Nullable<System.Guid> UpdateUser { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public Nullable<System.DateTime> UpdateTime { get; set; }
    }
}