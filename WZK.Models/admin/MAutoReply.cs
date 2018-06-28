using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WZK.Models.admin
{
   public class MAutoReply
    {
        /// <summary>
        /// 自动回复编号
        /// </summary>
        public System.Guid Id { get; set; }
        /// <summary>
        /// 所属模板
        /// </summary>
        public string TemplateName { get; set; }
        /// <summary>
        /// 回复序号
        /// </summary>
        public string AskNo { get; set; }
        /// <summary>
        /// 问题
        /// </summary>
        public string Question { get; set; }
        /// <summary>
        /// 答案
        /// </summary>
        public string Answer { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string Pic { get; set; }
        /// <summary>
        /// 图片序号
        /// </summary>
        public Nullable<int> PicIndex { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNo { get; set; }
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
