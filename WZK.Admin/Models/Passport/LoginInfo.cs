using System.ComponentModel.DataAnnotations;

namespace WZK.Admin.Models.Passport
{
    /// <summary>
    /// 登录信息
    /// author:lorne
    /// date:2016-03-01
    /// </summary>
    public class LoginInfo
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessage = "请填写用户名")]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "请填写密码")]
        public string PassWord { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        [Required(ErrorMessage = "请填写验证码")]
        public string Code { get; set; }
    }
}