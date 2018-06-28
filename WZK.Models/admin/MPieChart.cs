using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WZK.Models.admin
{
    //0-任务总数 1.执行脚本 2.自动回复 3.发朋友圈 4.定位
   public class MPieChart
    {
        /// <summary>
        /// 任务总数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 执行脚本数量
        /// </summary>
        public int ScriptNum { get; set; }
        /// <summary>
        /// 自动回复Num
        /// </summary>
        public int AutoReplyNum { get; set; }
        /// <summary>
        /// 发朋友圈 Num
        /// </summary>
        public int MomentsNum { get; set; }
        /// <summary>
        /// 定位Num
        /// </summary>
        public int LocationNum { get; set; }
    }
}
