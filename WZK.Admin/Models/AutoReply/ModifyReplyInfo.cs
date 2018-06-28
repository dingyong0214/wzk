using System;
using System.ComponentModel.DataAnnotations;

namespace WZK.Admin.Models.AutoReply
{
    public class ModifyReplyInfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        public System.Guid Id { get; set; }
        /// <summary>
        /// 模板编号
        /// </summary>
        public System.Guid TemplateId { get; set; }
        /// <summary>
        /// 回复序号
        /// </summary>
        [Required(ErrorMessage = "回复序号不能为空")]
        public string AskNo { get; set; }
        /// <summary>
        /// 问题
        /// </summary>
        [Required(ErrorMessage = "问题不能为空")]
        [StringLength(25, ErrorMessage = "问题长度不能超过25个字符")]
        public string Question { get; set; }
        /// <summary>
        /// 回答
        /// </summary>
        [Required(ErrorMessage = "回答不能为空")]
        [StringLength(500, ErrorMessage = "回答长度不能超过500个字符")]
        public string Answer { get; set; }
        /// <summary>
        /// 回复图片
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
        /// 排序号
        /// </summary>
        [Required(ErrorMessage = "排序号不能为空")]
        [RegularExpression("^[+-]?\\d+\\d*$", ErrorMessage = "请输入数字,且不能为小数")]
        [Range(1,10000, ErrorMessage = "请输入1-10000以内数字")]
        public int OrderNo { get; set; } = 1;
        /// <summary>
        /// 录入人
        /// </summary>
        public System.Guid InputUser { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        public System.DateTime InsertTime { get; set; } = DateTime.Now;
    }
}