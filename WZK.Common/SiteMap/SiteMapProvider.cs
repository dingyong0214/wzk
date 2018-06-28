// ==============================================
// Author	  : dingyong
// Create date: 2015-07-24 09:00:31
// Description: SiteMapProvider
// Update	  : 
// ===============================================
using System.Collections.Generic;
using System.Web;
using System.Configuration;
using System.Collections.Specialized;
using System.Xml;

namespace WZK.Common
{
    public class CMSiteMapProvider : XmlSiteMapProvider
    {
        private string _virtualPath = string.Empty;
        private ConfigXmlDocument _document = new ConfigXmlDocument();
        /// <summary>
        /// 初始化类对象。
        /// </summary>
        /// <param name="name">提供程序的标识名称</param>
        /// <param name="attributes">名称/值对集合</param>
        public override void Initialize(string name, NameValueCollection attributes)
        {
            _virtualPath = attributes["siteMapFile"];
            _document.Load(HttpContext.Current.Server.MapPath(_virtualPath)); 
            base.Initialize(name, attributes);
        }

        /// <summary>
        /// 获取主菜单
        /// </summary>
        /// <returns></returns>
        public List<SiteMapNode> getMenu()
        {
            List<SiteMapNode> maplist = new List<SiteMapNode>();
            XmlNodeList list = _document["siteMap"].ChildNodes;
            foreach (XmlNode node in list)
            {
                string title = node["title"].Value.Trim();
                string css = node["class"].Value.Trim();

                SiteMapNode mapnode = new SiteMapNode();
                mapnode.Title = title;
                mapnode.Class = css;

                maplist.Add(mapnode);
            }

            return maplist;
        }

        /// <summary>
        /// 获取当前站点地图的默认 WebCMS.WebCommon.CMSiteMapProvider 对象。
        /// </summary>
        public static CMSiteMapProvider SiteMapProvider
        {
            get { return (CMSiteMapProvider)SiteMap.Provider; }
        }
    }

    /// <summary>
    /// 节点类
    /// </summary>
    public class SiteMapNode
    {
        private string _title;
        private string _class;
        private string _menukey;
        private string _url;

        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }

        /// <summary>
        /// 样式名称
        /// </summary>
        public string Class
        {
            set { _class = value; }
            get { return _class; }
        }

        /// <summary>
        /// 菜单Key
        /// </summary>
        public string MenuKey
        {
            set { _menukey = value; }
            get { return _menukey; }
        }

        /// <summary>
        /// Url地址
        /// </summary>
        public string Url
        {
            set { _url = value; }
            get { return _url; }
        }
    }
}
