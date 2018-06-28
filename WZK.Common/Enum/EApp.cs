using System.ComponentModel;

namespace WZK.Common
{
    /// <summary>
    /// 脚本适配App
    /// author:dingyong
    /// createTime:2017-05-18
    /// </summary>
    public enum EApp
    {
        /// <summary>
        /// 微信
        /// </summary>
        [Description("微信")]
        WeChat = 1,
        /// <summary>
        /// QQ
        /// </summary>
        [Description("QQ")]
        QQ = 2,
        /// <summary>
        /// 微博
        /// </summary>
        [Description("微博")]
        WeiBo = 3,
        /// <summary>
        /// 知乎
        /// </summary>
        [Description("知乎")]
        ZhiHu = 4
    }
}
