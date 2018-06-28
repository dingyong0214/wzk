using System.Configuration;

namespace WZK.Common.ConfigData
{
    /// <summary>
    /// 阿里云相关配置
    /// author:lorne
    /// createTime:2015-11-19
    /// </summary>
    public static class AliyunConfig
    {
        /// <summary>
        /// 阿里云图片上传Id
        /// </summary>
        public static string AliyunAccessId = ConfigurationManager.AppSettings["AliyunAccessId"];
        /// <summary>
        /// 阿里云图片上传Key
        /// </summary>
        public static string AliyunAccessKey = ConfigurationManager.AppSettings["AliyunAccessKey"];
        /// <summary>
        /// 阿里云图片上传BucketName
        /// </summary>
        public static string BucketName = ConfigurationManager.AppSettings["BucketName"];
        /// <summary>
        /// 阿里云图片上传BucketURL
        /// </summary>
        public static string BucketUrl = ConfigurationManager.AppSettings["BucketUrl"];
        /// <summary>
        /// 阿里云图片上传域名
        /// </summary>
        public static string AliyunDomain = ConfigurationManager.AppSettings["AliyunDomain"];
        /// <summary>
        /// 图片上传临时文件夹
        /// </summary>
        public static string TempUpload = System.Configuration.ConfigurationManager.AppSettings["TempUpload"];
    }
}
