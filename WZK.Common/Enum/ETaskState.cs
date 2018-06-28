using System.ComponentModel;

namespace WZK.Common
{
    /// <summary>
    /// 状态：0-新任务，1-已接受，2-开始执行，3-已完成，4-执行失败
    /// author:dingyong
    /// createTime:2017-05-18
    /// </summary>
    public enum ETaskState
    {
        /// <summary>
        /// 新任务
        /// </summary>
        [Description("新任务")]
        NewTask = 0,
        /// <summary>
        /// 已接受
        /// </summary>
        [Description("已接受")]
        Accept = 1,
        /// <summary>
        /// 开始执行
        /// </summary>
        [Description("开始执行")]
        Execute = 2,
        /// <summary>
        /// 已完成
        /// </summary>
        [Description("已完成")]
        Complete = 3,
        /// <summary>
        /// 执行失败
        /// </summary>
        [Description("执行失败")]
        Fail = 4
    }
}
