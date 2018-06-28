
using System.ComponentModel;

namespace WZK.Common
{
    #region 性别
    /// <summary>
    /// 性别
    /// </summary>
    /// <remarks>added by dingyong 2016-08-23</remarks>
    public enum ESex
    {
        /// <summary>
        /// 未知
        /// </summary>
        [Description("未知")]
        None = 0,

        /// <summary>
        /// 男
        /// </summary>
        [Description("男")]
        Men = 1,
        /// <summary>
        /// 女
        /// </summary>
        [Description("女")]
        Women = 2
    }
    #endregion

    #region 用户状态
    /// <summary>
    /// 用户状态
    /// </summary>
    public enum EUserState
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Description("正常")]
        normal = 1,
        /// <summary>
        /// 禁用
        /// </summary>
        [Description("禁用")]
        Disable = 2,
        /// <summary>
        /// 删除
        /// </summary>
        [Description("删除")]
        Delete = 3
    }
    #endregion

    #region 扫码推荐标识
    /// <summary>
    /// 扫码推荐标识枚举
    /// </summary>
    public enum EUserKey
    {
        /// <summary>
        /// 手机号
        /// </summary>
        [Description("手机号")]
        Mobile = 0,
        /// <summary>
        /// 微信开放ID
        /// </summary>
        [Description("微信开放ID")]
        WeChatOpenID = 1,
    }
    #endregion    
}
