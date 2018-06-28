using System.Configuration;

namespace WZK.Common.ConfigData
{
    /// <summary>
    /// 短信、语音相关配置
    /// author:lorne
    /// createTime:2015-11-19
    /// </summary>
    public class SMSConfig
    {
        /// <summary>
        /// 短信、语音接口地址
        /// </summary>
        public static string Host = ConfigurationManager.AppSettings["Host"];
        /// <summary>
        /// 短信、语音接口端口
        /// </summary>
        public static string Port = ConfigurationManager.AppSettings["Port"];
        /// <summary>
        /// 短信、语音帐号Id
        /// </summary>
        public static string AccountId = ConfigurationManager.AppSettings["AccountId"];
        /// <summary>
        /// 短信、语音帐号Token
        /// </summary>
        public static string AccountToken = ConfigurationManager.AppSettings["AccountToken"];
        /// <summary>
        /// 短信AppId
        /// </summary>
        public static string SMSAppId = ConfigurationManager.AppSettings["SMSAppId"];
        /// <summary>
        /// 短信模板Id
        /// </summary>
        public static string TempId = ConfigurationManager.AppSettings["TempId"];
        /// <summary>
        /// 是否启用短信、语音
        /// </summary>
        public static string MsgIsOpen = ConfigurationManager.AppSettings["MsgIsOpen"];
        /// <summary>
        /// 是否使用默认验证码
        /// </summary>
        public static string IsDefaultCode = ConfigurationManager.AppSettings["IsDefaultCode"];
        /// <summary>
        /// 默认验证码
        /// </summary>
        public static string DefaultCode = ConfigurationManager.AppSettings["DefaultCode"];
        /// <summary>
        /// 验证码有效时长
        /// </summary>
        public static string ValidTimeLen = ConfigurationManager.AppSettings["ValidTimeLen"];
        /// <summary>
        /// 当天同一业务类型的验证限制次数
        /// </summary>
        public static int ValidLimit = int.Parse(ConfigurationManager.AppSettings["ValidLimit"]);
    }
}
