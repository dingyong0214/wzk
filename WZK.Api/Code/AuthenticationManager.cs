using System;
using System.Web;
using System.Web.Security;

namespace WZK.Api.Code
{
    /// <summary>
    /// 认证管理
    /// </summary>
    public class AuthenticationManager
    {
        /// <summary>
        /// 创建授权
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <remarks>author:zengbo date:2015-10-22</remarks>
        public static void CreateAuth(Guid id)
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(2, id.ToString(), DateTime.Now, DateTime.Now.Add(FormsAuthentication.Timeout), true, "");

            string cookieValue = FormsAuthentication.Encrypt(ticket);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookieValue);
            cookie.HttpOnly = true;
            cookie.Secure = FormsAuthentication.RequireSSL;
            cookie.Domain = FormsAuthentication.CookieDomain;
            cookie.Path = FormsAuthentication.FormsCookiePath;
            cookie.Expires = DateTime.Now.Add(FormsAuthentication.Timeout);

            HttpContext context = HttpContext.Current;
            if (context == null)
                throw new InvalidOperationException();

            context.Response.Cookies.Remove(cookie.Name);
            context.Response.Cookies.Add(cookie);
        }
    }
}