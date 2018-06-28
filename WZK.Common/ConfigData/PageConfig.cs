using System.Configuration;

namespace WZK.Common.ConfigData
{
    /// <summary>
    /// 分页相关配置
    /// author:lorne
    /// createTime:2015-11-19
    public class PageConfig
    {
        /// <summary>
        /// 每页显示数据条数
        /// </summary>
        public static int PageSize = int.Parse(ConfigurationManager.AppSettings["PageSize"]);
    }
}
