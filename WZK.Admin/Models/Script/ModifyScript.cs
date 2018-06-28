using WZK.Admin.Common;
using System;
using System.ComponentModel.DataAnnotations;
using MvcValidation.Extension;

namespace WZK.Admin.Models.Script
{
    public class ModifyScript
    {
        /// <summary>
        /// 脚本编号
        /// </summary>
        public System.Guid Id { get; set; }
        /// <summary>
        /// 脚本名称
        /// </summary>
        [Required(ErrorMessage = "脚本名称不能为空")]
        [StringLength(25, ErrorMessage = "脚本名称长度不能超过25个字符")]
        public string ScriptName { get; set; }
        /// <summary>
        /// 手机型号
        /// </summary>
        [NotEqualTo("GuidEmpty", ErrorMessage = "请选择手机型号")]
        public System.Guid MobileType { get; set; }
        /// <summary>
        /// 适配分辨率
        /// </summary>
        [Required(ErrorMessage = "适配分辨率不能为空")]
        public string DPI { get; set; }
        /// <summary>
        /// 脚本路径
        /// </summary>
        public string ScriptPath { get; set; }
        /// <summary>
        /// 适配App名称
        /// </summary>
        [Required(ErrorMessage = "请选择适配App")]
        public string APPName { get; set; }
        /// <summary>
        /// 脚本内容
        /// </summary>
        [Required(ErrorMessage = "脚本内容不能为空")]
        public string ScriptContent { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public int IsDel { get; set; } = 1;
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