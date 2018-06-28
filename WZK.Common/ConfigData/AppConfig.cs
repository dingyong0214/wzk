using System.Configuration;

namespace WZK.Common.ConfigData
{
    /// <summary>
    /// app相关配置
    /// author:lorne
    /// createTime:2015-11-19
    /// </summary>
    public class AppConfig
    {
        /// <summary>
        /// 更新版本号
        /// </summary>
        public static string Vcode = ConfigurationManager.AppSettings["vcode"];
        /// <summary>
        /// apk地址
        /// </summary>
        public static string ApkUrl = ConfigurationManager.AppSettings["apkUrl"];

        /// <summary>
        /// wzk更新版本号
        /// </summary>
        public static string WzkVcode = ConfigurationManager.AppSettings["wzkcode"];
        /// <summary>
        /// wzk apk地址
        /// </summary>
        public static string WzkApkUrl = ConfigurationManager.AppSettings["wzkUrl"];
    }
}
