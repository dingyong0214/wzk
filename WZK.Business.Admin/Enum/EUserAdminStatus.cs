using System.ComponentModel;

namespace WZK.Business.Admin.Enum
{
    /// <summary>
    /// 后台用户状态
    /// author:lorne
    /// date:2016-01-14
    /// </summary>
    public enum EUserAdminStatus
    {
        /// <summary>
        /// 有效
        /// </summary>
        [Description("有效")]
        Vaild = 1,
        /// <summary>
        /// 禁用
        /// </summary>
        [Description("禁用")]
        Invaild = 2,
        /// <summary>
        /// 删除
        /// </summary>
        [Description("删除")]
        Del = 3
    }
}
