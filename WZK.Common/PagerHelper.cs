using System.Collections.Specialized;
using System.Text;
using System.Web.Routing;

namespace System.Web.Mvc.Html
{
    /// <summary>
    /// 分页组件.
    /// </summary>
    public static class PagerHelper
    {
        private const string _Key = "id",
            nullHerf = "javascript:;",
            disabled = "disabled=\"disabled\"";

        /// <summary>
        /// Shows the page held.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="totalCount">The total count.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>HtmlString.</returns>
        /// <remarks>add by ****,2015-05-29 21:40:54 </remarks>
        public static HtmlString ShowPageHead(this HtmlHelper helper, int totalCount, int pageSize = 10)
        {
            int pageCount = Math.Max((totalCount + pageSize - 1) / pageSize, 1), //总页数
                showPage =
                !int.TryParse(helper.ViewContext.RouteData.Values[_Key] as string, out showPage) ? 1 :
                showPage > pageCount ? 1 :
                showPage; //当前显示页

            StringBuilder sb = new StringBuilder("<label class=\"fr\">");
            sb.Append("<label class=\"fr\">");
            sb.AppendFormat("<span>{0}/{1}</span>", showPage, pageCount);
            sb.AppendFormat("<a href=\"{0}\" class=\"pre\" {1}>上一页</a>", showPage - 1 > 0 ? helper.GetURL(showPage - 1) : nullHerf, showPage - 1 > 0 ? "" : disabled);
            sb.AppendFormat("<a href=\"{0}\" class=\"next\" {1}>下一页</a>", showPage + 1 <= pageCount ? helper.GetURL(showPage + 1) : nullHerf, showPage + 1 <= pageCount ? "" : disabled);
            sb.Append("</label>");

            return new HtmlString(sb.ToString());
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="totalCount">数据总数</param>
        /// <param name="pageSize">每页显示数量</param>
        /// <param name="max">最大显示页码</param>
       /// <param name="itemArea">显示区域</param>
        /// <returns>分页html</returns>
        /// <remarks>add by dingyong,2017-02-20 16:20:44 </remarks>
        public static HtmlString ShowPageNavigate(this HtmlHelper helper, int totalCount, int pageSize = 10, int max = 10,string itemArea = "container")
        {

            int pageCount = Math.Max((totalCount + pageSize - 1) / pageSize, 1), //总页数
                showPage =
                !int.TryParse(helper.ViewContext.RouteData.Values[_Key] as string, out showPage) ? 1 :
                showPage > pageCount ? 1 :
                showPage,  //当前显示页
                half = (int)Math.Ceiling(max / 2m);//前后页码

            StringBuilder output = new StringBuilder();//输出

            TagBuilder divTag = new TagBuilder("div");
            divTag.MergeAttribute("class", "page");//样式
            divTag.MergeAttribute("id", "page");//ID
            divTag.MergeAttribute("data-itemarea", itemArea);//显示区域
            divTag.MergeAttribute("data-action", helper.GetURL(0));//请求地址
            divTag.MergeAttribute("data-key", _Key);//key
            divTag.MergeAttribute("data-max", pageCount.ToString());//总页数

            output.Append(divTag.ToString(TagRenderMode.StartTag));

            #region 上一页 第一页
            //当前页大于一页显示上一个页面
            if (showPage > 1)
            { 

                TagBuilder prevTag = new TagBuilder("a");
                prevTag.MergeAttribute("href", helper.GetURL(showPage - 1));
                prevTag.MergeAttribute("class", "prev");
                prevTag.InnerHtml = "<";
                 
                output.Append(prevTag.ToString());
            }
            #endregion

            #region 分页页码显示
            if (pageCount <= max)//当页面总页数（pageCount）小于分页最大选项（Max）时
            {
                for (int i = 1; i <= pageCount; i++)
                {
                    TagBuilder aTag = new TagBuilder("a");
                    aTag.MergeAttribute("href", helper.GetURL(i));
                    aTag.MergeAttribute("class", i == showPage ? "current" : "num");
                    aTag.InnerHtml = i.ToString();

                    output.Append(aTag.ToString());
                }
            }
            else if (showPage - half > 0 && showPage + half < pageCount)//前后都有
            {
                for (int i = showPage - half; i <= showPage + half; i++)
                {
                    TagBuilder aTag = new TagBuilder("a");
                    aTag.MergeAttribute("href", helper.GetURL(i));
                    aTag.MergeAttribute("class", i == showPage ? "current" : "num");
                    aTag.InnerHtml = i.ToString();

                    output.Append(aTag.ToString());
                }
            }
            else if (showPage - half <= 0)//前面不够 ，后面多余
            {
                for (int i = 1; i <= max; i++)
                {
                    TagBuilder aTag = new TagBuilder("a");
                    aTag.MergeAttribute("href", helper.GetURL(i));
                    aTag.MergeAttribute("class", i == showPage ? "current" : "num");
                    aTag.InnerHtml = i.ToString();

                    output.Append(aTag.ToString());
                }
            }
            else if (showPage + half >= pageCount)//前面多余，后面不足
            {
                for (int i = pageCount - max; i <= pageCount; i++)
                {
                    TagBuilder aTag = new TagBuilder("a");
                    aTag.MergeAttribute("href", helper.GetURL(i));
                    aTag.MergeAttribute("class", i == showPage ? "current" : "num");
                    aTag.InnerHtml = i.ToString();

                    output.Append(aTag.ToString());
                }
            }
            #endregion

            #region 下一页 最后一页
            if (showPage < pageCount)//下一页
            {
                TagBuilder nextTag = new TagBuilder("a");
                nextTag.MergeAttribute("href", helper.GetURL(showPage + 1));
                nextTag.MergeAttribute("class", "next"); 
                nextTag.InnerHtml = ">";
                output.Append(nextTag.ToString()); 
            }
            #endregion

            #region 跳转选项
            TagBuilder spanTag = new TagBuilder("span");
            spanTag.InnerHtml = "转到第";
            output.Append(spanTag.ToString());

            TagBuilder inputTag = new TagBuilder("input");
            inputTag.MergeAttribute("type", "text");
            //add by ****,2015-7-21 非数字字符屏蔽输入
            inputTag.MergeAttribute("maxLength", "10");
            inputTag.Attributes.Add("onkeyup", "this.value=this.value.replace(/\\D+/,'')");
            inputTag.Attributes.Add("onpaste", "this.value=this.value.replace(/\\D+/,'')");
            output.Append(inputTag.ToString(TagRenderMode.SelfClosing));
            spanTag = new TagBuilder("span");
            spanTag.InnerHtml = "页";
            output.Append(spanTag.ToString());
            TagBuilder Go = new TagBuilder("a");
            Go.MergeAttribute("href", "javascript:;");
            Go.MergeAttribute("class", "go");
            Go.InnerHtml = "Go";
            output.Append(Go.ToString());
            spanTag = new TagBuilder("span");
            spanTag.InnerHtml = "共"+ totalCount + "条数据";
            output.Append(spanTag.ToString());
            #endregion

            output.Append(divTag.ToString(TagRenderMode.EndTag));
            return new HtmlString(output.ToString());
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="totalCount">数据总数</param>
        /// <param name="pageSize">每页显示数量</param>
        /// <param name="max">最大显示页码</param>
        /// <returns>分页html</returns>
        /// <remarks>add by dingyong,2017-02-16 16:51:32 </remarks>
        public static HtmlString PaginationNavigate(this HtmlHelper helper, int totalCount, int pageSize = 10, int max = 10)
        {

            int pageCount = Math.Max((totalCount + pageSize - 1) / pageSize, 1), //总页数
                showPage =
                !int.TryParse(helper.ViewContext.RouteData.Values[_Key] as string, out showPage) ? 1 :
                showPage > pageCount ? 1 :
                showPage,  //当前显示页
                half = (int)Math.Ceiling(max / 2m);//前后页码

            StringBuilder output = new StringBuilder();//输出
            TagBuilder divTag = new TagBuilder("div");
            divTag.MergeAttribute("class", "web_page");//样式
            divTag.MergeAttribute("id", "page");//ID
            divTag.MergeAttribute("data-action", helper.GetURL(0));//请求地址
            divTag.MergeAttribute("data-key", _Key);//key
            divTag.MergeAttribute("data-max", pageCount.ToString());//总页数

            output.Append(divTag.ToString(TagRenderMode.StartTag));

            #region 上一页 第一页
            //当前页大于一页显示上一个页面
            if (showPage > 1)
            {
                TagBuilder firstTag = new TagBuilder("div");
                firstTag.MergeAttribute("class", "page_prev");
                firstTag.InnerHtml = "<a href = \""+ helper.GetURL(showPage - 1) + "\" title=\"上一页\">上一页</a>";
                output.Append(firstTag.ToString());

                TagBuilder prevTag = new TagBuilder("div");
                prevTag.MergeAttribute("class", "page_first");
                prevTag.InnerHtml = "<a href = \"" + helper.GetURL(1) + "\" title=\"第一页\">&nbsp;</a>";
                output.Append(prevTag.ToString());

            }
            #endregion

            #region 下一页 最后一页
            if (showPage < pageCount)//下一页
            {
                output.Append("<div class=\"page_next\"><a href =\""+ helper.GetURL(showPage + 1) + "\" title=\"下一页\">下一页</a></div>");
                output.Append("<div class=\"page_last\"><a href = \"" + helper.GetURL(pageCount) + "\" title=\"最后一页\">&nbsp;</a></div>");
            }
            #endregion

            #region 分页页码显示
            output.Append("<ul>");
            if (pageCount <= max)//当页面总页数（pageCount）小于分页最大选项（Max）时
            {
                for (int i = 1; i <= pageCount; i++)
                {
                    TagBuilder liTag = new TagBuilder("li");
                    liTag.MergeAttribute("class", i == showPage ? "selected" : "");
                    liTag.InnerHtml = "<a href = \""+ helper.GetURL(i) + "\" > "+i+" </ a >";
                    output.Append(liTag.ToString());
                }
            }
            else if (showPage - half > 0 && showPage + half < pageCount)//前后都有
            {
                for (int i = showPage - half; i <= showPage + half; i++)
                {
                    TagBuilder liTag = new TagBuilder("li");
                    liTag.MergeAttribute("class", i == showPage ? "selected" : "");
                    liTag.InnerHtml = "<a href = \"" + helper.GetURL(i) + "\" > " + i + " </ a >";
                    output.Append(liTag.ToString());
                }
            }
            else if (showPage - half <= 0)//前面不够 ，后面多余
            {
                for (int i = 1; i <= max; i++)
                {
                    TagBuilder liTag = new TagBuilder("li");
                    liTag.MergeAttribute("class", i == showPage ? "selected" : "");
                    liTag.InnerHtml = "<a href = \"" + helper.GetURL(i) + "\" > " + i + " </ a >";
                    output.Append(liTag.ToString());
                }
            }
            else if (showPage + half >= pageCount)//前面多余，后面不足
            {
                for (int i = pageCount - max; i <= pageCount; i++)
                {
                    TagBuilder liTag = new TagBuilder("li");
                    liTag.MergeAttribute("class", i == showPage ? "selected" : "");
                    liTag.InnerHtml = "<a href = \"" + helper.GetURL(i) + "\" > " + i + " </ a >";

                    output.Append(liTag.ToString());
                }
            }
            output.Append("</ul>");
            #endregion

            output.Append(divTag.ToString(TagRenderMode.EndTag));
            return new HtmlString(output.ToString());
        }

        /// <summary>
        /// Gets the URL.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <returns>System.String.</returns>
        /// <remarks>add by ****,2015-05-23 22:38:27 </remarks>
        private static string GetURL(this HtmlHelper helper, int pageIndex)
        {
            ViewContext viewContext = helper.ViewContext;
            RouteValueDictionary routeValues = new RouteValueDictionary(viewContext.RouteData.Values);

            if (pageIndex != 0)
            {
                if (!routeValues.ContainsKey(_Key))
                {
                    routeValues.Add(_Key, pageIndex);
                }
                else
                {
                    int index = 0;
                    int.TryParse(routeValues[_Key].ToString(), out index);
                    index = index == 0 ? 1 : index; 
                    routeValues[_Key] = pageIndex;
                }
            }
            else
            {
                if (!routeValues.ContainsKey(_Key))
                {
                    routeValues.Add(_Key, 1);
                }
                else
                {
                    routeValues[_Key] = 1;
                }
            }
            string url = UrlHelper.GenerateUrl(
                null,
                routeValues["action"].ToString(),
                routeValues["controller"].ToString(),
                routeValues,
                helper.RouteCollection,
                viewContext.RequestContext,
                false);


            NameValueCollection queryString = viewContext.RequestContext.HttpContext.Request.QueryString;

            if (queryString.Count > 0)
                url = url + "?" + viewContext.RequestContext.HttpContext.Request.QueryString.ToString();

            return url;
        }
    }
}
