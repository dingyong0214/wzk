using MvcValidation.Extension;
using System;
using System.ComponentModel.DataAnnotations;

namespace WZK.Admin.Models.Mobile
{
    public class BatchMobileInfo
    {
        /// <summary>
        /// 手机号 信息 （以多个以^分隔）
        /// </summary>
        [Display(Name = "手机号")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string Param { get; set; }
        /// <summary>
        /// 手机名称
        /// </summary>
        [Display(Name = "手机名称")]
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(25, ErrorMessage = "{0}长度不能超过25个字符")]
        public string MobileName { get; set; }
        /// <summary>
        /// 手机型号
        /// </summary>
        [NotEqualTo("GuidEmpty", ErrorMessage = "请选择手机型号")]
        public Guid MobileType { get; set; }
        /// <summary>
        /// 手机型号名称
        /// </summary>
        [Display(Name = "手机型号名称")]
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(25, ErrorMessage = "{0}长度不能超过25个字符")]
        public string MobileTypeName { get; set; }
        /// <summary>
        /// 分辨率
        /// </summary>
        [Display(Name = "分辨率")]
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(25, ErrorMessage = "{0}长度不能超过25个字符")]
        public string DPI { get; set; }
        /// <summary>
        /// 所在机房
        /// </summary>
        [NotEqualTo("GuidEmpty", ErrorMessage = "请选择所在机房")]
        public System.Guid IDC { get; set; }
        /// <summary>
        /// 所在位置
        /// </summary>
        [Display(Name = "所在位置")]
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(200, ErrorMessage = "{0}长度不能超过200个字符")]
        public string Position { get; set; }
        /// <summary>
        /// 是否激活 0-未激活，1-已激活
        /// </summary>
        public int IsActive { get; set; } = 0;
        /// <summary>
        /// 验证 Guid.Empty
        /// </summary>
        public Guid GuidEmpty { get; set; } = Guid.Empty;
    }
}