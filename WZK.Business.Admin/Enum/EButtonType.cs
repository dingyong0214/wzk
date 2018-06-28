using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WZK.Business.Admin.Enum
{
    /// <summary>
    /// 功能按钮类型
    /// </summary>
    public enum EButtonType
    {
        /// <summary>
        /// 基本按钮
        /// </summary>
        [Description("基本按钮")]
        Base = 1,
        /// <summary>
        /// 一般按钮
        /// </summary>
        [Description("一般按钮")]
        Normal = 2
    }
}
