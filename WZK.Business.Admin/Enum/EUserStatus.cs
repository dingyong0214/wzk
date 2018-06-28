using System.ComponentModel;

namespace WZK.Business.Admin.Enum
{
    /// <summary>
    /// 用户状态
    /// author:lorne
    /// createTime:2015-11-25
    /// </summary>
    public enum EUserStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Description("正常")]
        Vaild = 1,
        /// <summary>
        /// 禁用
        /// </summary>
        [Description("禁用")]
        Disable = 2,
        /// <summary>
        /// 删除
        /// </summary>
        [Description("删除")]
        Del = 3,
        /// <summary>
        /// 未交费
        /// </summary>
        [Description("未交费")]
        Notpay = 4
    }

    /// <summary>
    /// 统计时间类型  
    /// </summary>
    public enum EStatictisDateType
    {
        /// <summary>
        /// 天
        /// </summary>
        [Description("天")]
        Day = 1,
        /// <summary>
        /// 周
        /// </summary>
        [Description("周")]
        Week = 2,
        /// <summary>
        /// 月
        /// </summary>
        [Description("月")]
        Month = 3,
        /// <summary>
        /// 年
        /// </summary>
        [Description("年")]
        Year = 4
    }

    /// <summary>
    /// 城市合伙人申请状态
    /// author:dingyong
    /// createTime:2016-12-30
    /// </summary>
    public enum EPartnerApplyStatus
    {
        /// <summary>
        /// 未确认
        /// </summary>
        [Description("未确认")]
        Vaild = 0,
        /// <summary>
        /// 已回访
        /// </summary>
        [Description("已回访")]
        Callback = 1,
        /// <summary>
        /// 已确认
        /// </summary>
        [Description("已确认")]
        IsPass = 2
    }

    /// <summary>
    /// 城市合伙人状态
    /// author:dingyong
    /// createTime:2017-01-13
    /// </summary>
    public enum EPartnerStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Description("正常")]
        Normal = 1,
        /// <summary>
        /// 禁用
        /// </summary>
        [Description("禁用")]
        Disable = 2
    }

    /// <summary>
    /// 城市合伙人费用结算状态
    /// author:dingyong
    /// createTime:2017-01-17
    /// </summary>
    public enum EPartnerIncomeStatus
    {
        /// <summary>
        /// 未结算
        /// </summary>
        [Description("未结算")]
        WeiJieSuan = 0,
        /// <summary>
        /// 已结算
        /// </summary>
        [Description("已结算")]
        YiJieSuan = 1
    }

    /// <summary>
    /// 城市合伙人等级
    /// author:dingyong
    /// createTime:2017-01-13
    /// </summary>
    public enum EPartnerLevel
    {
        /// <summary>
        /// C1
        /// </summary>
        [Description("C1")]
        C1 = 1,
        /// <summary>
        /// C2
        /// </summary>
        [Description("C2")]
        C2 = 2,
        /// <summary>
        /// C3
        /// </summary>
        [Description("C3")]
        C3 = 3,
        /// <summary>
        /// C4
        /// </summary>
        [Description("C4")]
        C4 = 4
    }

    /// <summary>
    /// 赚钱方法
    /// author:dingyong
    /// createTime:2016-12-30
    /// </summary>
    public enum EEarnMethod
    {
        /// <summary>
        /// 推荐
        /// </summary>
        [Description("推荐")]
        TuiJian = 0,
        /// <summary>
        /// 成为合伙人
        /// </summary>
        [Description("成为合伙人")]
        HeHuoRen = 1
    }
}
