using MvcValidation.Extension;
using System;
using System.ComponentModel.DataAnnotations;

namespace WZK.Admin.Models.Task
{
    public class ModifyTaskDetail
    {
        /// <summary>
        /// 手机编号 信息 （以多个以^分隔）
        /// </summary>
        [Required(ErrorMessage = "请选择任务手机")]
        public string Param { get; set; }
        /// <summary>
        /// 任务编号
        /// </summary>
        [NotEqualTo("GuidEmpty", ErrorMessage = "任务信息错误")]
        public Guid TaskId { get; set; }
        /// <summary>
        /// 验证 Guid.Empty
        /// </summary>
        public Guid GuidEmpty { get; set; } = Guid.Empty;
    }
}