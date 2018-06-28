using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;

namespace WZK.Common
{
    /// <summary>
    /// 工具类
    /// author:lorne
    /// createTime:2015-10-20
    /// </summary>
    public class Tool
    {
        #region 获取IP
        /// <summary>
        /// 获取IP
        /// </summary>
        /// <returns>IP地址</returns>
        /// <remarks>author:lorne date:2015-10-20</remarks>
        public static string GetIP()
        {
            string ip = string.Empty;
            if (!string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"]))
                ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ip))
                ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            return ip;
        }
        #endregion

        #region 模拟POST请求
        /// <summary>
        /// 模拟POST请求
        /// </summary>
        /// <returns>响应内容</returns>
        /// <remarks>add by dingyong date:2016-08-12</remarks>
        public static string HttpPost(string url, string postDataStr, string contentType = "application/x-www-form-urlencoded", CookieContainer cookie = null, string requestEncoding = "utf-8", string reponseEncoding = "utf-8")
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = contentType;
            if (cookie != null)
            {
                request.CookieContainer = cookie;
            }
            byte[] bytes = Encoding.GetEncoding(requestEncoding).GetBytes(postDataStr);
            request.ContentLength = bytes.Length;
            Stream stream = request.GetRequestStream();
            stream.Write(bytes, 0, bytes.Length);
            stream.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding(reponseEncoding));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }
        #endregion

        #region 模拟GET请求
        /// <summary>
        /// 模拟GET请求
        /// </summary>
        /// <returns>响应内容</returns>
        /// <remarks>add by dingyong date:2016-08-12</remarks>
        public static string HttpGet(string url, string contentType = "text/html;charset=UTF-8", CookieContainer cookie = null, string requestEncoding = "utf-8", string reponseEncoding = "utf-8")
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = contentType;
            if (cookie != null)
            {
                request.CookieContainer = cookie;
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding(reponseEncoding));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }
        #endregion

        #region 获取当前IP对应的 Ip(国家-省-市)
        /// <summary>
        /// 获取当前IP对应的 Ip(国家-省-市)
        /// </summary>
        /// <returns>System.String.</returns>
        /// <remarks>add by dingyong,2016-08-18 10:20:25 </remarks>
        public static string GetIpAdress()
        {
            try
            {
                string strIp = GetRealIP();
                if (!string.IsNullOrEmpty(strIp))
                {
                    string url = "http://int.dpool.sina.com.cn/iplookup/iplookup.php";
                    string data = "format=json&ip=" + strIp;
                    var result = HttpGet(url, data);
                    string strCountry = "";
                    string strProvince = "";
                    string strCity = "";
                    if (result != "-2")
                    {
                        if (result.Split(',').Length > 1)
                        {
                            strCountry = result.Split(',')[3].Split(':')[1];
                            strCountry = strCountry.Substring(1, strCountry.Length - 2).Replace(@"\\", @"\");
                            strProvince = result.Split(',')[4].Split(':')[1];
                            strProvince = strProvince.Substring(1, strProvince.Length - 2).Replace(@"\\", @"\");
                            strCity = result.Split(',')[5].Split(':')[1];
                            strCity = strCity.Substring(1, strCity.Length - 2).Replace(@"\\", @"\");
                        }
                    }
                    return strIp + "(" + ToGB2312(strCountry) + "-" + ToGB2312(strProvince) + "-" + ToGB2312(strCity) + ")";
                }
            }
            catch (Exception ex)
            {
                return "";
            }
            return "";
        }
        #endregion

        #region 得到真实IP
        /// <summary>
        /// 得到真实IP
        /// </summary>
        /// <returns>System.String.</returns>
        /// <remarks>add by dingyong,2016-08-18 10:22:49 </remarks>
        public static string GetRealIP()
        {
            string ip;
            try
            {
                HttpRequest request = HttpContext.Current.Request;

                if (request.ServerVariables["HTTP_VIA"] != null)
                {
                    ip = request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString().Split(',')[0].Trim();
                }
                else
                {
                    ip = request.UserHostAddress;
                }
            }
            catch (Exception e)
            {
                return "";
            }

            return ip;
        }
        #endregion

        #region 将Unicode编码转换为汉字字符串
        /// <summary>
        /// 将Unicode编码转换为汉字字符串
        /// </summary>
        /// <param name="str">Unicode编码字符串</param>
        /// <returns>汉字字符串</returns>
        /// <remarks>add by dingyong,2016-08-18 10:22:49 </remarks>
        public static string ToGB2312(string str)
        {
            string r = "";
            MatchCollection mc = Regex.Matches(str, @"\\u([\w]{2})([\w]{2})", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            byte[] bts = new byte[2];
            foreach (Match m in mc)
            {
                bts[0] = (byte)int.Parse(m.Groups[2].Value, NumberStyles.HexNumber);
                bts[1] = (byte)int.Parse(m.Groups[1].Value, NumberStyles.HexNumber);
                r += Encoding.Unicode.GetString(bts);
            }
            return r;
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 提交数据
        /// </summary>
        /// <param name="url">提交数据到URL</param>
        /// <param name="postData">数据</param>
        /// <param name="contentType">内容类型</param>
        /// <returns>System.String.</returns>
        /// <remarks>add by dingyong,2016-08-18 11:13:43 </remarks>
        public static string PostData(string url, string postData, string contentType = "text/json; charset=utf-8")
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] data = encoding.GetBytes(postData);
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);

            myRequest.Method = "POST";
            myRequest.ContentType = contentType;
            myRequest.ContentLength = data.Length;
            Stream newStream = myRequest.GetRequestStream();
            newStream.Write(data, 0, data.Length);
            newStream.Close();

            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            string content = reader.ReadToEnd();
            reader.Close();
            return content;
        }
        #endregion

        #region 获取日期间隔天数
        /// <summary>
        /// 获取日期间隔天数
        /// </summary>
        /// <param name="startTime">开始时间.</param>
        /// <param name="endTime">结束时间.</param>
        /// <returns>System.Int32.</returns>
        /// <remarks>add by dingyong,2016-09-29 15:59:58 </remarks>
        public static int GetIntervalDay(DateTime startTime, DateTime endTime)
        {
            TimeSpan ts2 = endTime.Subtract(startTime);
            return ts2.Days;
        }
        #endregion

        #region 模拟Post请求 --  提交byte
        /// <summary>
        /// 模拟Post请求 --  提交byte
        /// </summary>
        /// <param name="posturl">url</param>
        /// <param name="postData">参数byte</param>
        /// <returns></returns>
        /// <remarks>add by dingyong,2016-12-12 10:22:49 </remarks>
        public static string PostSumbit(string posturl, byte[] postData)
        {
            HttpWebRequest objWebRequest = (HttpWebRequest)WebRequest.Create(posturl);
            objWebRequest.Method = "POST";
            objWebRequest.ContentType = "application/x-www-form-urlencoded";
            objWebRequest.ContentLength = postData.Length;
            Stream newStream = objWebRequest.GetRequestStream();
            // Send the data. 
            newStream.Write(postData, 0, postData.Length); //写入参数 
            newStream.Close();

            HttpWebResponse response = (HttpWebResponse)objWebRequest.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            string textResponse = sr.ReadToEnd(); // 返回的数据
            return textResponse;
        }
        #endregion

        #region Cookie操作       
        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        /// <param name="strValue">过期时间(分钟)</param>
        public static void WriteCookie(string strName, string strValue, int expires)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Value = strValue;
            if (expires > 0)
            {
                cookie.Expires = DateTime.Now.AddMinutes(expires);
            }
            HttpContext.Current.Response.AppendCookie(cookie);
        }
        /// <summary>
        /// 读cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns>cookie值</returns>
        public static string GetCookie(string strName)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null)
            {
                return HttpContext.Current.Request.Cookies[strName].Value.ToString();
            }
            return "";
        }
        /// <summary>
        /// 删除Cookie对象
        /// </summary>
        /// <param name="cookiesName">Cookie名称</param>
        public static void RemoveCookie(string cookiesName)
        {
            HttpCookie objCookie = new HttpCookie(cookiesName.Trim());
            objCookie.Expires = DateTime.Now.AddYears(-5);
            HttpContext.Current.Response.Cookies.Add(objCookie);
        }
        #endregion
    }
}
