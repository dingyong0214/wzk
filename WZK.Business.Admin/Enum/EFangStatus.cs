using System.ComponentModel;

namespace WZK.Business.Admin.Enum
{
    /// <summary>
    /// 房源状态
    /// </summary>
    public enum EFangStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Description("正常")]
        Normal = 1,
        /// <summary>
        /// 失效
        /// </summary>
        [Description("失效")]
        ShiXiao = 2,
        /// <summary>
        /// 已租
        /// </summary>
        [Description("已租")]
        HadRent = 3
    }
}
