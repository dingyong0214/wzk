using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WZK.Common
{
    /// <summary>
    /// 类型转换处理
    /// </summary>
    /// <remarks>author:wcy date:2016-03-11</remarks>
    public class ConvertTryParse
    {
        /// <summary>
        /// object 转 decimal类型
        /// </summary>
        /// <param name="str">值</param>
        /// <returns>异常数据返回0</returns>
        /// <remarks>author:wcy date:2016-03-11</remarks>
        public static decimal TryParseDecimal(object str)
        {
            decimal num = 0;
            if (str != null && !(str is DBNull))
            {
                decimal.TryParse(str.ToString(), out num);
            }
            return num;
        }
        /// <summary>
        /// object 转 double 类型
        /// </summary>
        /// <param name="str">值</param>
        /// <returns>异常数据返回0</returns>
        /// <remarks>author:wcy date:2016-03-11</remarks>
        public static double TryParseDouble(object str)
        {
            double num = 0;
            if (str != null && !(str is DBNull))
            {
                double.TryParse(str.ToString(), out num);
            }
            return num;
        }
        /// <summary>
        /// object 转 int 类型
        /// </summary>
        /// <param name="str">值</param>
        /// <returns>异常数据返回0</returns>
        /// <remarks>author:wcy date:2016-03-11</remarks>
        public static int TryParseInt(object str)
        {
            int num = 0;
            if (str != null && !(str is DBNull))
            {
                int.TryParse(str.ToString(), out num);
            }
            return num;
        }
        /// <summary>
        /// object 转 DateTime 类型
        /// </summary>
        /// <param name="str">值</param>
        /// <returns>异常数据返回null</returns>
        /// <remarks>author:wcy date:2016-03-11</remarks>
        public static DateTime? TryParseDateTime(object str)
        {
            DateTime? time = null;
            if (str != null && !(str is DBNull))
            {
                DateTime t = DateTime.Now;
                if (DateTime.TryParse(str.ToString(), out t))
                {
                    time = t;
                }
            }
            return time;
        }
        /// <summary>
        /// object 转 bool 类型
        /// </summary>
        /// <param name="str">值</param>
        /// <returns>异常数据返回false</returns>
        /// <remarks>author:wcy date:2016-03-11</remarks>
        public static bool TryParseBool(object str)
        {
            bool num = false;
            if (str != null && !(str is DBNull))
            {
                bool.TryParse(str.ToString(), out num);
            }
            return num;
        }
        /// <summary>
        /// object 转 guid
        /// </summary>
        /// <param name="str">值</param>
        /// <returns>异常数据返回null</returns>
        /// <remarks>author:wcy date:2016-03-25</remarks>
        public static Guid? TryParseGuid(object str)
        {
            Guid id = Guid.Empty;
            if (str != null && !(str is DBNull))
            {
                Guid.TryParse(str.ToString(), out id);
                return id;
            }
            return null;
        }

        /// <summary>
        /// String转Guid数组
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>Guid[].</returns>
        /// <remarks>add by dingyong,2016-07-11 17:01:44 </remarks>
        public static Guid[] TryParseGuidArray(string str)
        {
            try
            {
                var array = str.Split(new char[] { '，', ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (array.Length > 0)
                {
                    Guid[] result = new Guid[array.Length];
                    Guid n;
                    Guid empty = Guid.Empty;
                    for (int i = 0; i < array.Length; i++)
                    {
                        if (Guid.TryParse(array[i], out n) &&!empty.Equals(n))
                        {
                            result[i] = n;
                        }
                    }
                    return result;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// String转Guid集合
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>Guid[].</returns>
        /// <remarks>add by dingyong,2016-07-11 17:01:44 </remarks>
        public static List<Guid> TryParseGuidList(string str)
        {
            try
            {
                var array = str.Split(new char[] { '，', ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (array.Length > 0)
                {
                    List<Guid> result = new List<Guid>();
                    Guid n;
                    Guid empty = Guid.Empty;
                    for (int i = 0; i < array.Length; i++)
                    {
                        if (Guid.TryParse(array[i], out n) && !empty.Equals(n))
                        {
                            result.Add(n);
                        }
                    }
                    return result;
                } 
            }
            catch
            { 
            }
            return null;
        }

        /// <summary>
        /// String转Decimal数组
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>Guid[].</returns>
        /// <remarks>add by dingyong,2016-07-11 17:01:44 </remarks>
        public static decimal[] TryParseDecimalArray(string str)
        {
            try
            {
                var array = str.Replace("，", ",").Split(',');
                array = array.Where(c => !string.IsNullOrWhiteSpace(c)).ToArray();
                if (array.Length > 0)
                {
                    decimal[] result = new decimal[array.Length];
                    decimal n;
                    for (int i = 0; i < array.Length; i++)
                    {
                        if (decimal.TryParse(array[i], out n))
                        {
                            result[i] = n;
                        }
                    }
                    return result;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// String转Int数组
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>Guid[].</returns>
        /// <remarks>add by dingyong,2016-07-11 17:01:44 </remarks>
        public static int[] TryParseIntArray(string str)
        {
            try
            {
                var array = str.Replace("，", ",").Split(',');
                array = array.Where(c => !string.IsNullOrWhiteSpace(c)).ToArray();
                if (array.Length > 0)
                {
                    int[] result = new int[array.Length];
                    int n;
                    for (int i = 0; i < array.Length; i++)
                    {
                        if (int.TryParse(array[i], out n))
                        {
                            result[i] = n;
                        }
                    }
                    return result;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// String转Date数组
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>Guid[].</returns>
        /// <remarks>add by dingyong,2016-07-11 17:01:44 </remarks>
        public static DateTime[] TryParseDateTimeArray(string str)
        {
            try
            {
                var array = str.Replace("，", ",").Split(',');
                array = array.Where(c => !string.IsNullOrWhiteSpace(c)).ToArray();
                if (array.Length > 0)
                {
                    DateTime[] result = new DateTime[array.Length];
                    DateTime n;
                    for (int i = 0; i < array.Length; i++)
                    {
                        if (DateTime.TryParse(array[i], out n))
                        {
                            result[i] = n;
                        }
                    }
                    return result;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// String转String数组
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>Guid[].</returns>
        /// <remarks>add by dingyong,2016-07-11 17:01:44 </remarks>
        public static string[] TryParseStringArray(string str)
        {
            try
            {
                var array = str.Replace("，", ",").Split(',');
                return array.Where(c => !string.IsNullOrWhiteSpace(c)).ToArray();
            }
            catch
            {
                return null;
            }
        }

        private static string[] arrWeek = { "周日", "周一", "周二", "周三", "周四", "周五", "周六", };
        /// <summary>
        /// 转换英文星期为中文格式
        /// </summary>
        /// <param name="week">The week.</param>
        /// <returns>System.String.</returns>
        /// <remarks>add by dingyong,2016-09-06 17:20:28 </remarks>
        public static string ConvertWeekToChinese(DayOfWeek week)
        {
            return arrWeek[(int)week];
        }

        #region PUBLIC
        /// <summary>
        /// 反序列化商品属性
        /// </summary>
        /// <param name="skuAttribute">The sku attribute.</param>
        /// <returns>System.String.</returns>
        /// <remarks>add by dingyong,2016-07-22 15:16:28 </remarks>
        public static string DeserializeSKUAttribute(string skuAttribute)
        {
            try
            {
                List<SkuAttribute> attrList = JsonConvert.DeserializeObject<List<SkuAttribute>>(skuAttribute);
                string[] attr = attrList.Select(c => c.AttrName + "：" + c.AttrVal).ToArray();
                if (attr.Length > 0)
                {
                    skuAttribute = string.Join("；", attr);
                }
            }
            catch
            {
            }
            return skuAttribute;
        }
        #endregion

        /// <summary>
        /// Html转换为字符串
        /// </summary>
        /// <param name="html">Html内容</param>
        /// <returns>System.String.</returns>
        /// <remarks>add by dingyong,2017-03-04 16:21:11 </remarks>
        public static string HtmlConvertToString(string html)
        {
            System.Text.RegularExpressions.Regex regex1 = new System.Text.RegularExpressions.Regex(@"<script[\s\S]+</script *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex2 = new System.Text.RegularExpressions.Regex(@" href *= *[\s\S]*script *:", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex3 = new System.Text.RegularExpressions.Regex(@" no[\s\S]*=", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex4 = new System.Text.RegularExpressions.Regex(@"<iframe[\s\S]+</iframe *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex5 = new System.Text.RegularExpressions.Regex(@"<frameset[\s\S]+</frameset *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex6 = new System.Text.RegularExpressions.Regex(@"\<img[^\>]+\>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex7 = new System.Text.RegularExpressions.Regex(@"</p>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex8 = new System.Text.RegularExpressions.Regex(@"<p>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex9 = new System.Text.RegularExpressions.Regex(@"<[^>]*>", System.Text.RegularExpressions.RegexOptions.IgnoreCase); 
            html = regex1.Replace(html, ""); //过滤<script></script>标记
            html = regex2.Replace(html, ""); //过滤href=javascript: (<A>) 属性
            html = regex3.Replace(html, " _disibledevent="); //过滤其它控件的on...事件
            html = regex4.Replace(html, ""); //过滤iframe
            html = regex5.Replace(html, ""); //过滤frameset
            html = regex6.Replace(html, ""); //过滤frameset
            html = regex7.Replace(html, ""); //过滤frameset
            html = regex8.Replace(html, ""); //过滤frameset
            html = regex9.Replace(html, "");
            html = html.Replace(" ", "");
            html = html.Replace("</strong>", "");
            html = html.Replace("<strong>", "");
            html = html.Replace("&nbsp;", " ");
            return html;
        }
    }

    /// <summary>
    /// Class SkuAttribute.
    /// </summary>
    public class SkuAttribute
    {
        public string AttrName { get; set; }
        public string AttrVal { get; set; }
    }
}
