using System;
using System.ComponentModel.DataAnnotations;

namespace WZK.Admin.Models.Moments
{
    public class ModifyMoments
    {
        /// <summary>
        /// 编号
        /// </summary>
        public System.Guid Id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        [Required(ErrorMessage = "标题不能为空")]
        [StringLength(25, ErrorMessage = "标题长度不能超过25个字符")]
        public string Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        [Required(ErrorMessage = "内容不能为空")]
       [StringLength(800, ErrorMessage = "机房名称长度不能超过800个字符")]
        public string Content { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string Picture { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public int IsDel { get; set; } = 1;
        public System.Guid InputUser { get; set; }
        public System.DateTime InsertTime { get; set; } = DateTime.Now;
        public Nullable<System.Guid> UpdateUser { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
    }
}