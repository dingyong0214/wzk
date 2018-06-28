using System;
using System.ComponentModel.DataAnnotations;

namespace WZK.Admin
{
    /// <summary>
    /// 重置密码
    /// author:dingyong
    /// createTime:2016-07-05
    /// </summary>
    public class ResetPwd
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [Required(ErrorMessage = "用户ID不能为空")]
        public Guid Id { get; set; }
        /// <summary>
        /// 当前登录运营系统密码
        /// </summary>
        [Required(ErrorMessage = "当前系统登录密码不能为空")]
        public string LoginPwd { get; set; }
        /// <summary>
        /// 新密码
        /// </summary>
        [Required(ErrorMessage = "请输入新密码")]
        [RegularExpression(@"^(?!\d+$)(?![A-Za-z]+$)[\x21-\x7e]{6,32}$", ErrorMessage = "密码为6-32个英文字母、数字或符号，不能是纯数字或纯字母")]
        public string NewPwd { get; set; }
        /// <summary>
        /// 确认新密码
        /// </summary>
        [Compare("NewPwd",ErrorMessage = "两次密码不一致")]
        public string ConNewPwd { get; set; }
    }

    public class ResetOrder
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [Required(ErrorMessage = "用户ID不能为空")]
        public Guid Id { get; set; }
        /// <summary>
        /// 新序号
        /// </summary>
        [Required(ErrorMessage = "请输入新序号")]
        [RegularExpression(@"[0-9]{1,4}", ErrorMessage = "序号大于0，最大9999")]
        public int NewOrder { get; set; }
    }
}