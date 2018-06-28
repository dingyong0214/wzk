using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WZK.Models.api
{
    public class MTaskDetail
    {
        /// <summary>
        ///任务编号
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        ///手机号码
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 已经执行的次数
        /// </summary>
        public int Times { get; set; }
        /// <summary>
        /// 任务状态：0-新任务，1-已接受，2-开始执行，3-已完成，4-执行失败
        /// </summary>
        public int State { get; set; }
    }
}
