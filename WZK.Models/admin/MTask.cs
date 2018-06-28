using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WZK.Models.admin
{
   public class MTask
    {
        /// <summary>
        /// 编号
        /// </summary>
        public System.Guid Id { get; set; }
        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName { get; set; }
        /// <summary>
        /// 任务描述
        /// </summary>
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
        public string TemplateName { get; set; }
        /// <summary>
        /// 任务开始执行时间
        /// </summary>
        public System.DateTime StartTime { get; set; }
        /// <summary>
        /// 录入人
        /// </summary>
        public string InputUser { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        public System.DateTime InsertTime { get; set; }
    }
}
