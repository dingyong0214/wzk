// ==============================================
// Author	  : dingyong
// Create date: 2017-04-10 11:41:33
// Description: ESms
// Update	  : 
// ===============================================
using System.ComponentModel;

namespace WZK.Common
{
    /// <summary>
    /// 短信业务类型
    /// </summary>
    public enum ESmsBussinessType
    {
        /// <summary>
        /// 验证码登陆
        /// </summary>
        [Description("验证码登陆")]
        Register = 0,
        /// <summary>
        /// 修改登录密码
        /// </summary>
        [Description("修改登录密码")]
        UpdateLoginPassport = 1,
        /// <summary>
        /// 修改交易密码
        /// </summary>
        [Description("修改交易密码")]
        UpdateTranPassport = 2,
        /// <summary>
        /// 忘记登录密码
        /// </summary>
        [Description("忘记登录密码")]
        ForgetLoginPassport = 3,
        /// <summary>
        /// 忘记支付密码
        /// </summary>
        [Description("忘记支付密码")]
        ForgetTranPassport = 4,
        /// <summary>
        /// 验证手机号码
        /// </summary>
        [Description("验证手机号码")]
        VerifyPhoneNumber = 5,
        /// <summary>
        /// 修改手机号码
        /// </summary>
        [Description("修改手机号码")]
        UpdatePhoneNumber = 6
    }
}
