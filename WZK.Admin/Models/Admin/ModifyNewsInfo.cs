using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WZK.Admin
{
    /// <summary>
    /// 新闻维护类
    /// Author:wcy date:2016-03-17
    /// </summary>
    public class ModifyNewsInfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        [Required(ErrorMessage = "请输入新闻标题")]
        public string Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        [Required(ErrorMessage = "请输入资讯内容")]
        public string Content { get; set; }
    }
}
