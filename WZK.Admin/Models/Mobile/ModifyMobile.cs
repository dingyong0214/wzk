using MvcValidation.Extension;
using System;
using System.ComponentModel.DataAnnotations;
using WZK.Admin.Common;

namespace WZK.Admin.Models.Mobile
{
    public class ModifyMobile
    {
        /// <summary>
        /// 手机编号
        /// </summary>
        public System.Guid Id { get; set; }
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
        public System.Guid MobileType { get; set; }
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
        /// 手机号码
        /// </summary>
        [Display(Name = "手机号码")]
        [Required(ErrorMessage = "{0}不能为空")]
        [Mobile(ErrorMessage = "手机号格式不正确")]
        public string MobileNo { get; set; }
        /// <summary>
        /// Mac地址
        /// </summary>
        public string Mac { get; set; }
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
        /// android推送clientId
        /// </summary>
        public string AndroidClientId { get; set; }
        /// <summary>
        /// 是否激活 0-未激活，1-已激活
        /// </summary>
        public int IsActive { get; set; } = 0;
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
        /// <summary>
        /// 验证 Guid.Empty
        /// </summary>
        public Guid GuidEmpty { get; set; } = Guid.Empty;
    }
}