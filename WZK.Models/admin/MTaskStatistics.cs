using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WZK.Models.admin
{
    public class MTaskStatistics
    {
        /// <summary>
        /// 任务类型
        /// </summary>
        public int TaskType { get; set; }
        /// <summary>
        /// 执行该任务手机数量
        /// </summary>
        public int MobileNum { get; set; }
        /// <summary>
        /// 新任务Num
        /// </summary>
        public int NewTaskNum { get; set; }
        /// <summary>
        /// 已接受的任务Num
        /// </summary>
        public int AcceptNum { get; set; }
        /// <summary>
        /// 开始执行Num
        /// </summary>
        public int ExecuteNum { get; set; }
        /// <summary>
        /// 已完成Num
        /// </summary>
        public int CompleteNum { get; set; }
        /// <summary>
        /// 执行失败Num
        /// </summary>
        public int FailNum { get; set; }

    }
}
