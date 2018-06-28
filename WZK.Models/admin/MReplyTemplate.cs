using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WZK.Models.admin
{
   public class MReplyTemplate
    {
        /// <summary>
        /// 自动回复模板编号
        /// </summary>
        public System.Guid Id { get; set; }
        /// <summary>
        /// 模板名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 简要说明
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string Pic { get; set; }
        /// <summary>
        /// 图片序号
        /// </summary>
        public Nullable<int> PicIndex { get; set; }
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
