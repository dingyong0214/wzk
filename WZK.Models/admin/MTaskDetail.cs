using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WZK.Models.admin
{
   public class MTaskDetail
    {
        /// <summary>
        /// 编号
        /// </summary>
        public System.Guid Id { get; set; }
        /// <summary>
        /// 任务编号
        /// </summary>
        public System.Guid TaskId { get; set; }
        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName { get; set; }
        /// <summary>
        /// 手机编号
        /// </summary>
        public System.Guid MobileId { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string MobileNo { get; set; }
        /// <summary>
        /// 手机型号
        /// </summary>
        public Guid MobileType { get; set; }
        /// <summary>
        /// 手机型号名称
        /// </summary>
        public string MobileTypeName { get; set; }
        /// <summary>
        /// 手机分辨率
        /// </summary>
        public string DPI { get; set; }
        /// <summary>
        /// 已经执行的次数：默认0
        /// </summary>
        public int ExecutedTimes { get; set; }
        /// <summary>
        /// 执行结束时间
        /// </summary>
        public Nullable<System.DateTime> EndTime { get; set; }
        /// <summary>
        /// 状态：0-新任务，1-已接受，2-开始执行，3-已完成，4-执行失败，5-已取消
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 录入人
        /// </summary>
        public string  InputUser { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        public System.DateTime InsertTime { get; set; }
    }
}
