using WZK.Admin.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace WZK.Admin.Models.AutoReply
{
    public class ModifyTemplate
    {
        /// <summary>
        /// 编号
        /// </summary>
        public System.Guid Id { get; set; }
        /// <summary>
        /// 模板名称
        /// </summary>
        [Required(ErrorMessage = "模板名称不能为空")]
        [StringLength(20, ErrorMessage = "模板名称长度不能超过20个字符")]
        public string Name { get; set; }
        /// <summary>
        /// 简要说明
        /// </summary>
        [Required(ErrorMessage = "简要说明不能为空")]
        public string Description { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string Pic { get; set; }
        /// <summary>
        /// 图片序号
        /// </summary>
        [Required(ErrorMessage = "图片序号不能为空")]
        [RegularExpression("^[+-]?\\d+\\d*$", ErrorMessage = "请输入数字,且不能为小数")]
        [Range(-1, 10000, ErrorMessage = "请输入-1-10000以内数字")]
        public Nullable<int> PicIndex { get; set; } = -1;
        /// <summary>
        /// 是否删除 0-删除，1-未删除
        /// </summary>
        public Nullable<int> IsDel { get; set; } = 1;
        /// <summary>
        /// 录入人
        /// </summary>
        public System.Guid InputUser { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        public System.DateTime InsertTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 修改人
        /// </summary>
        public Nullable<System.Guid> UpdateUser { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public Nullable<System.DateTime> UpdateTime { get; set; }
    }
}