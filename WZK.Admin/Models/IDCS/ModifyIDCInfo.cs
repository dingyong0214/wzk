using WZK.Admin.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace WZK.Admin.Models.IDCS
{
    public class ModifyIDCInfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        public System.Guid Id { get; set; }
        /// <summary>
        /// 机房名称
        /// </summary>
        [Required(ErrorMessage = "机房名称不能为空")]
        [StringLength(20, ErrorMessage = "机房名称长度不能超过20个字符")]
        public string Name { get; set; }
        /// <summary>
        /// 机房地址
        /// </summary>
        [Required(ErrorMessage = "机房地址不能为空")]
        [StringLength(120, ErrorMessage = "机房地址长度不能超过120个字符")]
        public string Address { get; set; }
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