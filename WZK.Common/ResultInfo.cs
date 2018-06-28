using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace WZK.Common
{
    #region 结果状态信息
    /// <summary>
    /// 结果状态信息
    /// author:曾波
    /// createTime:2016-07-23
    /// </summary>
    public class ResultInfo
    {
        public ResultInfo(ErrorStatus status = ErrorStatus.S0000)
        {
            this.Status = status;
        }

        private ErrorStatus status;
        /// <summary>
        /// 错误状态码
        /// </summary>
        public ErrorStatus Status
        {
            get { return status; }
            set
            {
                status = value;
                if (!string.IsNullOrWhiteSpace(ErrorMsg))
                {
                    Desc = ErrorMsg;
                }
                else
                {
                    //为状态描述赋值
                    string name = Enum.GetName(typeof(ErrorStatus), value);
                    object[] objs = typeof(ErrorStatus).GetField(name).GetCustomAttributes(false);
                    if (objs.Length > 0)
                        Desc = (objs[0] as DescriptionAttribute).Description;
                    else
                        Desc = name;
                }
            }
        }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMsg { get; set; }

        /// <summary>
        /// 状态描述
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 错误状态码
        /// </summary>
        public string StatusCode { get { return this.Status.ToString(); } }

        /// <summary>
        /// 是否通过
        /// </summary>
        public bool IsPass { get { return status == ErrorStatus.S0001; } }
    }
    #endregion

    #region 返回单个实体集
    /// <summary>
    /// 返回单个实体集
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResultSingle<T> : ResultInfo
    {
        /// <summary>
        /// 单个返回数据
        /// </summary>
        public T ReturnObject { get; set; }
    }

    #region APP返回列表实体集
    /// <summary>
    /// APP返回列表实体集
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AppResultSingle<T> : ResultSingle<T>
    {
        /// <summary>
        /// 是否需要更新数据
        /// </summary>
        public bool NeedUpdate { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public string Timestamp { get; set; }
    }
    #endregion

    #endregion

    #region 返回列表实体集
    /// <summary>
    /// 返回列表实体集
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResultList<T> : ResultInfo
    {
        /// <summary>
        /// 列表返回数据
        /// </summary>
        public List<T> ReturnList { get; set; }

        /// <summary>
        /// 列表数据的总量（用于分页）
        /// </summary>
        public int Count { get; set; }
    }
    #endregion

    #region APP返回列表实体集
    /// <summary>
    /// APP返回列表实体集
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AppResultList<T> : ResultList<T>
    {
        /// <summary>
        /// 是否需要更新数据
        /// </summary>
        public bool NeedUpdate { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public string Timestamp { get; set; }
    }
    #endregion

    #region 返回两个列表实体集
    /// <summary>
    /// 返回列表实体集
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="T2"></typeparam>
    public class ResultTwoList<T, T2> : ResultInfo
    {
        /// <summary>
        /// 实体(列表)返回数据
        /// </summary>
        public T ReturnObject { get; set; }
        /// <summary>
        /// 列表返回数据
        /// </summary>
        public List<T2> ReturnList { get; set; }

        /// <summary>
        /// 列表数据的总量（用于分页）
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 是否需要更新数据
        /// </summary>
        public bool NeedUpdate { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public string Timestamp { get; set; }
    }
    #endregion

    #region 返回DataTable
    /// <summary>
    /// 返回DataTable
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResultDataTable : ResultInfo
    {
        /// <summary>
        /// DataTable返回数据
        /// </summary>
        public DataTable ReturnDataTable { get; set; }
    }
    #endregion

    #region 返回两个DataTable
    /// <summary>
    /// 返回两个DataTable
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResultTwoDataTable : ResultInfo
    {
        /// <summary>
        /// DataTable返回数据
        /// </summary>
        public DataTable One { get; set; }

        /// <summary>
        /// DataTable返回数据
        /// </summary>
        public DataTable Two { get; set; }
    }
    #endregion

    #region 返回DataTable和实体
    /// <summary>
    /// 返回DataTable和实体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResultDataTableAndModel<T> : ResultInfo
    {
        /// <summary>
        /// DataTable返回数据
        /// </summary>
        public T ReturnObject { get; set; }

        /// <summary>
        /// DataTable返回数据
        /// </summary>
        public DataTable DataTable { get; set; }
    }
    #endregion

    #region 返回DataTable和列表
    /// <summary>
    /// 返回DataTable和列表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResultDataTableAndList<T> : ResultInfo
    {
        /// <summary>
        /// 列表
        /// </summary>
        public List<T> ReturnList { get; set; }

        /// <summary>
        /// DataTable返回数据
        /// </summary>
        public DataTable DataTable { get; set; }
    }
    #endregion

    /// <summary>
    /// 状态枚举
    /// author:lorne
    /// createTime:2015-10-23
    /// </summary>
    public enum ErrorStatus
    {
        #region 全局适用状态
        /// <summary>
        /// 初始状态
        /// </summary>
        [Description("初始状态")]
        S0000,
        /// <summary>
        /// 成功
        /// </summary>
        [Description("成功")]
        S0001,
        /// <summary>
        /// 本次操作未成功
        /// </summary>
        [Description("本次操作不成功")]
        S0002,
        /// <summary>
        /// 验证码过期或已验证
        /// </summary>
        [Description("验证码过期")]
        S0003,
        /// <summary>
        /// 短信验证码错误
        /// </summary>
        [Description("短信验证码错误")]
        S0004,
        /// <summary>
        /// 当天验证码获取次数超过限制
        /// </summary>
        [Description("当天验证码获取次数超过限制")]
        S0005,
        /// <summary>
        /// 验证码发送失败
        /// </summary>
        [Description("验证码发送失败")]
        S0006,
        /// <summary>
        /// 图形验证码输入有误或已过期
        /// </summary>
        [Description("图形验证码输入有误或已过期")]
        S0007,
        /// <summary>
        /// 该用户不存在
        /// </summary>
        [Description("该用户不存在")]        
        S0008,
        /// <summary>
        /// 非法请求
        /// </summary>
        [Description("非法请求")]
        S0009,
        /// <summary>
        /// 验证码过期或已验证
        /// </summary>
        [Description("验证码已使用过")]
        S0010,
        /// <summary>
        /// 正在审核中
        /// </summary>
        [Description("正在审核中")]
        S0011,
        /// <summary>
        /// 审核未通过
        /// </summary>
        [Description("审核未通过")]
        S0012,
        /// <summary>
        /// 支付密码错误
        /// </summary>
        [Description("支付密码错误")]
        S0013,
        /// <summary>
        /// 您还未实名，请先实名
        /// </summary>
        [Description("您还未实名，请先实名")]
        S0014,
        /// <summary>
        /// 实名认证正在审核中
        /// </summary>
        [Description("实名认证正在审核中")]
        S0015,
        /// <summary>
        /// 实名认证未通过，请重新认证
        /// </summary>
        [Description("实名认证未通过，请重新认证")]
        S0016,
        /// <summary>
        /// 您还未登录
        /// </summary>
        [Description("您还未登录")]
        S0017,
        /// <summary>
        /// 审核已经审核，不能撤销
        /// </summary>
        [Description("审核已经审核，不能撤销")]
        S0018,
        /// <summary>
        /// 签名错误,请联系擦一擦技术人员
        /// </summary>
        [Description("签名错误,请联系擦一擦技术人员")]
        S0019,
        /// <summary>
        /// 图片验证码错误
        /// </summary>
        [Description("图片验证码错误")]
        S0020,
        /// <summary>
        /// 区域不存在,无法修改
        /// </summary>
        [Description("区域不存在,无法修改")]
        S0021,
        /// <summary>
        /// 联系方式已存在
        /// </summary>
        [Description("联系方式已存在")]
        S0022,
        /// <summary>
        /// 合伙人等级结算设置已存在
        /// </summary>
        [Description("合伙人等级已存在配置")]
        S0023,
        /// <summary>
        /// 分类不存在
        /// </summary>
        [Description("分类不存在")]
        S0024,
        /// <summary>
        /// 用户资金信息异常
        /// </summary>
        [Description("用户资金信息异常")]
        S0025,
        /// <summary>
        /// 充值金额输入异常
        /// </summary>
        [Description("充值金额输入异常")]
        S0026,
        /// <summary>
        /// 账户总额小于冻结金额,资金数据异常
        /// </summary>
        [Description("账户总额小于冻结金额,资金数据异常")]
        S0027,
        /// <summary>
        /// 请上传身份证正面图片
        /// </summary>
        [Description("请上传身份证正面图片")]
        S0028,
        /// <summary>
        /// 请上传身份证反面图片
        /// </summary>
        [Description("请上传身份证反面图片")]
        S0029,
        /// <summary>
        /// 请输入验证码
        /// </summary>
        [Description("请输入验证码")]
        S0030,
        #region HttpStatus
        /// <summary>
        /// 未授权
        /// </summary>
        [Description("未授权")]
        S0401,
        /// <summary>
        /// 资源不存在
        /// </summary>
        [Description("资源不存在")]
        S0404,
        /// <summary>
        /// 不支持指定的请求方式
        /// </summary>
        [Description("不支持指定的请求方式")]
        S0405,
        /// <summary>
        /// 服务器内部错误
        /// </summary>
        [Description("服务器内部错误")]
        S0500,
        /// <summary>
        /// 请确保数据格式正确
        /// </summary>
        [Description("请确保数据格式正确")]
        S0600,
        /// <summary>
        /// 余额不足,暂不支持部分抵扣
        /// </summary>
        [Description("余额不足,暂不支持部分抵扣")]
        S0700,
        #endregion

        #endregion

        #region 用户相关状态
        /// <summary>
        /// 该手机号已注册
        /// </summary>
        [Description("该手机号已注册")]
        S1000,
        /// <summary>
        /// 该手机号未注册
        /// </summary>
        [Description("该手机号未注册")]
        S1001,
        /// <summary>
        /// 推荐人手机号未注册
        /// </summary>
        [Description("推荐人手机号未注册")]
        S1002,
        /// <summary>
        /// 新密码与原密码相同
        /// </summary>
        [Description("新密码与原密码相同")]
        S1003,
        /// <summary>
        /// 原支付密码错误
        /// </summary>
        [Description("原支付密码错误")]
        S1004,
        /// <summary>
        /// 支付密码不能与登录密码相同
        /// </summary>
        [Description("支付密码不能与登录密码相同")]
        S1005,
        /// <summary>
        /// 该用户已禁用
        /// </summary>
        [Description("该用户已禁用")]
        S1006,
        /// <summary>
        /// 用户名或密码错误
        /// </summary>
        [Description("用户名或密码错误")]
        S1007,
        /// <summary>
        /// 当前密码错误
        /// </summary>
        [Description("当前密码错误")]
        S1008,
        /// <summary>
        /// 密码必须6到30位包含字母数字组合
        /// </summary>
        [Description("密码必须6到30位包含字母数字组合")]
        S1009,
        /// <summary>
        /// 用户名或密码错误
        /// </summary>
        [Description("用户名或密码错误")]
        S1010,
        /// <summary>
        /// 该身份证已使用
        /// </summary>
        [Description("该身份证已使用")]
        S1011,
        /// <summary>
        /// 身份证号码格式不正确
        /// </summary>
        [Description("身份证号码格式不正确")]
        S1012,
        /// <summary>
        /// 手机号码格式不正确
        /// </summary>
        [Description("手机号码格式不正确")]
        S1013,
        /// <summary>
        /// 该用户不存在
        /// </summary>
        [Description("该用户不存在")]
        S1014,
        /// <summary>
        /// 用户已被删除
        /// </summary>
        [Description("用户已被删除")]
        S1015,
        /// <summary>
        /// 您还未设置支付密码，请点击“忘记支付密码”设置
        /// </summary>
        [Description("您还未设置支付密码，请点击“忘记支付密码”设置")]
        S1016,
        /// <summary>
        /// 未确认身份不能投票，请重新确认身份
        /// </summary>
        [Description("未确认身份不能投票，请重新确认身份")]
        S1017,
        /// <summary>
        /// 新手机号码不能和旧手机号码相同
        /// </summary>
        [Description("新手机号码不能和旧手机号码相同")]
        S1018,
        /// <summary>
        /// 推荐人不能是自己
        /// </summary>
        [Description("推荐人不能是自己")]
        S1019,
        /// <summary>
        /// 用户已有推荐人，不能重复设置推荐人
        /// </summary>
        [Description("用户已有推荐人，不能重复设置推荐人")]
        S1020,
        /// <summary>
        /// 短信发送超出个人每日限制
        /// </summary>
        [Description("短信发送超出个人每日限制")]
        S1021,
        /// <summary>
        /// 60秒内仅能获取一次验证码
        /// </summary>
        [Description("60秒内仅能获取一次验证码")]
        S1022,
        /// <summary>
        ///WEB和APP密码错误10次以上，账户锁定
        /// </summary>
        [Description("您的账户为高风险账户,将被系统锁定24小时")]
        S1023,
        #endregion

        #region 管理后台相关状态
        /// <summary>
        /// 不存在该用户
        /// </summary>
        [Description("不存在该用户")]
        S4001,
        /// <summary>
        /// 原密码错误
        /// </summary>
        [Description("原密码错误")]
        S4002,
        /// <summary>
        /// 区域名称已存在
        /// </summary>
        [Description("区域名称已存在")]
        S4003,
        /// <summary>
        /// 该服务器不存在
        /// </summary>
        [Description("该服务器不存在")]
        S4004,
        /// <summary>
        /// 服务器名称已存在
        /// </summary>
        [Description("服务器名称已存在")]
        S4005,
        /// <summary>
        /// 该用户名已存在
        /// </summary>
        [Description("该用户名已存在")]
        S4006,
        /// <summary>
        /// 帐号已被禁用
        /// </summary>
        [Description("帐号已被禁用")]
        S4007,
        /// <summary>
        /// 该角色名已存在
        /// </summary>
        [Description("该角色名已存在")]
        S4008,
        /// <summary>
        /// 该菜单名已存在
        /// </summary>
        [Description("该菜单名已存在")]
        S4009,
        /// <summary>
        /// 该菜单英文名已存在
        /// </summary>
        [Description("该菜单英文名已存在")]
        S4010,
        /// <summary>
        /// 该菜单功能名已存在
        /// </summary>
        [Description("该菜单功能名已存在")]
        S4011,
        /// <summary>
        /// 该菜单序号已存在
        /// </summary>
        [Description("该菜单序号已存在")]
        S4012,
        /// <summary>
        /// 状态异常
        /// </summary>
        [Description("状态异常")]
        S4016,
        /// <summary>
        /// 资金不足
        /// </summary>
        [Description("资金不足")]
        S4017,
        /// <summary>
        /// 该手机号已被其他会员使用
        /// </summary>
        [Description("该手机号已被其他会员使用")]
        S4018,
        /// <summary>
        /// 该数据不存在或可能已被删除
        /// </summary>
        [Description("该数据不存在或可能已被删除")]
        S4022,
        /// <summary>
        /// 该身份证号已被其他用户使用
        /// </summary>
        [Description("该身份证号已被其他用户使用")]
        S4023,
        /// <summary>
        /// 当前系统登录密码错误
        /// </summary>
        [Description("当前系统登录密码错误")]
        S4024,

        /// <summary>
        /// 菜单按钮不存在
        /// </summary>
        [Description("菜单按钮不存在")]
        S4026,
        /// <summary>
        /// 菜单不存在
        /// </summary>
        [Description("菜单不存在")]
        S4027,
        /// <summary>
        /// 身份证信息有误
        /// </summary>
        [Description("身份证信息有误")]
        S4028,
        /// <summary>
        /// 该序号已存在
        /// </summary>
        [Description("该序号已存在")]
        S4031,
        /// <summary>
        /// 该机房名已存在
        /// </summary>
        [Description("该机房名已存在")]
        S4032,
        /// <summary>
        /// 该手机型号已存在
        /// </summary>
        [Description("该手机型号已存在")]
        S4033,
        /// <summary>
        /// 该手机号码已存在
        /// </summary>
        [Description("该手机号码已存在")]
        S4034,
        /// <summary>
        /// 没有找到您需要修改的手机
        /// </summary>
        [Description("没有找到您需要修改的手机")]
        S4035,
        /// <summary>
        /// 该模板名称已存在
        /// </summary>
        [Description("该模板名称已存在")]
        S4036,
        /// <summary>
        /// 回复序号已存在
        /// </summary>
        [Description("回复序号已存在")]
        S4037,
        /// <summary>
        /// 脚本名称已存在
        /// </summary>
        [Description("脚本名称已存在")]
        S4038,
        /// <summary>
        /// 任务开始时间与其他任务冲突，间隔时间不能低于10分钟
        /// </summary>
        [Description("任务开始时间与其他任务冲突，间隔时间不能低于10分钟")]
        S4039,
        #endregion        
    }
}
