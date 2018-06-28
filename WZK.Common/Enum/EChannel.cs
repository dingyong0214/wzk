using System.ComponentModel;

namespace WZK.Common
{
    #region 数据来源渠道
    /// <summary>
    /// 数据来源渠道枚举
    /// 来源渠道：0-网站 1-微信 2-安卓 3-IOS 4-触屏版 5-系统赠送
    /// </summary>
    public enum EChannel
    {
        /// <summary>
        /// 网站
        /// </summary>
        [Description("网站")]
        Site = 0,
        /// <summary>
        /// 微信
        /// </summary>
        [Description("微信")]
        WeChat = 1,
        /// <summary>
        /// 安卓
        /// </summary>
        [Description("安卓")]
        Android = 2,
        /// <summary>
        /// IOS
        /// </summary>
        [Description("IOS")]
        IOS = 3,
        /// <summary>
        /// 触屏版
        /// </summary>
        [Description("触屏版")]
        Mobile = 4,
        /// <summary>
        /// 管理后台
        /// </summary>
        [Description("管理后台")]
        Manage = 5,
        /// <summary>
        /// 支付宝
        /// </summary>
        [Description("支付宝")]
        Alipay = 6
    }
    #endregion    
}
