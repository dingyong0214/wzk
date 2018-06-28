using System.ComponentModel;

namespace WZK.Common
{
    /// <summary>
    /// 投票结果：0-反对 1-同意 2-弃权
    /// </summary>
    public enum EVoteResult
    {
        /// <summary>
        /// 反对
        /// </summary>
        [Description("反对")]
        Against = 0,
        /// <summary>
        /// 赞成
        /// </summary>
        [Description("赞成")]
        Agree = 1,
        /// <summary>
        /// 弃权
        /// </summary>
        [Description("弃权")]
        QiQuan = 2
    }
}
