using System;
using System.ComponentModel.DataAnnotations;

namespace WZK.Admin.Models.MobileModel
{
    public class ModifyMobileModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        public System.Guid Id { get; set; }
        /// <summary>
        /// 手机型号名
        /// </summary>
        [Required(ErrorMessage = "机房名称不能为空")]
        [StringLength(20, ErrorMessage = "机房名称长度不能超过20个字符")]
        public string Name { get; set; }
        /// <summary>
        /// 分辨率
        /// </summary>
        [Required(ErrorMessage = "分辨率不能为空")]
        [StringLength(20, ErrorMessage = "分辨率长度不能超过20个字符")]
        public string DPI { get; set; }
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
        /// 修改时间
        /// </summary>
        public Nullable<System.DateTime> UpdateTime { get; set; }
    }
}