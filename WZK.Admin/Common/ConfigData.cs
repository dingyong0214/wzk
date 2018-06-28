namespace WZK.Admin.Common
{
    /// <summary>
    /// 配置信息
    /// author:lorne
    /// createTime:2015-12-03
    /// </summary>
    public static class ConfigData
    {
        /// <summary>
        /// 图片上传临时文件夹
        /// </summary>
        public static string TempUpload = System.Configuration.ConfigurationManager.AppSettings["TempUpload"]; 
    }
}