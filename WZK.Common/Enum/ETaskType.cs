using System.ComponentModel;

namespace WZK.Common
{
    /// <summary>
    /// 任务类型： 1-执行脚本，2-自动回复，3-发朋友圈,4-定位
    /// author:dingyong
    /// createTime:2017-05-18
    /// </summary>
    public enum ETaskType
    {
        /// <summary>
        /// 执行脚本
        /// </summary>
        [Description("执行脚本")]
        Script = 1,
        /// <summary>
        /// 自动回复
        /// </summary>
        [Description("自动回复")]
        Reply = 2,
        /// <summary>
        /// 发朋友圈
        /// </summary>
        [Description("发朋友圈")]
        Moments = 3,
        /// <summary>
        /// 定位
        /// </summary>
        [Description("定位")]
        Location = 4
    }
}
