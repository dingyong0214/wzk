using WZK.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WZK.Web.Models
{
    /// <summary>
    /// 登录视图模型
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// 手机号码
        /// </summary> 
        [Display(Name = "手机号码")]
        [Required(ErrorMessage = "请输入{0}")]
        [Phone(ErrorMessage = "{0}格式不正确")]
        [MaxLength(11)]
        public string Mobile { get; set; } 
    }

}