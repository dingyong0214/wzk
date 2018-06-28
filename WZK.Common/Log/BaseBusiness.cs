namespace WZK.Common
{
    /// <summary>
    /// 业务逻辑基类
    /// </summary>
    public class BaseBusiness
    {
        protected BaseBusiness() { }

        protected static Logger log = new Logger();
        public int PageSize { get { return ConfigData.PageConfig.PageSize; } }
    }
}
