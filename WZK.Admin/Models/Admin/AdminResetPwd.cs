using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WZK.Admin
{
    /// <summary>
    /// 修改后台用户密码
    /// </summary>
    public class AdminResetPwd 
    {
        /// <summary>
        /// 原密码
        /// </summary>
        [Required(ErrorMessage = "请输入原密码")]
        public string Password { get; set; }
        /// <summary>
        /// 新密码
        /// </summary>
        [Required(ErrorMessage = "请输入新密码")]
        [RegularExpression(@"^(?!\d+$)(?![A-Za-z]+$)[\x21-\x7e]{6,32}$", ErrorMessage = "密码为6-32个英文字母、数字或符号，不能是纯数字或纯字母")]
        public string NewPwd { get; set; }
        /// <summary>
        /// 确认新密码
        /// </summary>
        [Compare("NewPwd", ErrorMessage = "两次密码不一致")]
        public string ConNewPwd { get; set; }
    }
}
