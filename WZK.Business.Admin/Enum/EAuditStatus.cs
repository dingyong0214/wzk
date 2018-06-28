using System.ComponentModel;

namespace WZK.Business.Admin.Enum
{
    /// <summary>
    /// 认证状态
    /// author:dingyong
    /// createTime:2016-07-07
    /// </summary>
    public enum EAuditStatus
    {
        /// <summary>
        /// 未审核
        /// </summary>
        [Description("未审核")]
        NotAudit = 0,
        /// <summary>
        /// 通过
        /// </summary>
        [Description("通过")]
        Pass = 1,
        /// <summary>
        /// 拒绝
        /// </summary>
        [Description("拒绝")]
        NotPass = 2
    }
}
