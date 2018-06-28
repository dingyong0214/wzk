using System;
using System.Web;
using System.Web.Security;

namespace WZK.Common.Authentication.Impl
{
    /// <summary>
    /// Form验证.
    /// </summary>
    public class FormsAuthenticationService<T> : IAuthenticationService<T> where T : IAuthUser, new()
    {
        private readonly HttpContext _httpContext;
        private readonly TimeSpan _expirationTimeSpan;

        /// <summary>
        /// Form验证.
        /// </summary>
        public FormsAuthenticationService()
        {
            _httpContext = HttpContext.Current;
            _expirationTimeSpan = FormsAuthentication.Timeout;
        }

        /// <summary>
        /// 是否登陆
        /// </summary>
        /// <value><c>true</c> if this instance is login; otherwise, <c>false</c>.</value>
        /// <exception cref="System.NotImplementedException"></exception>
        public bool IsLogin
        {
            get
            {
                return _httpContext != null &&
                       _httpContext.Request.IsAuthenticated &&
                       _httpContext.User.Identity is FormsIdentity;
            }
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="createPersistentCookie">if set to <c>true</c> [create persistent cookie].</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void SignIn(IAuthUser user, bool createPersistentCookie)
        {
            var now = DateTime.UtcNow.ToLocalTime();

            var ticket = new FormsAuthenticationTicket(
                1 /*version*/,
                user.Id.ToString(),
                now,
                now.Add(_expirationTimeSpan),
                createPersistentCookie,
                user.UserData,
                FormsAuthentication.FormsCookiePath);

            var encryptedTicket = FormsAuthentication.Encrypt(ticket);

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
            {
                HttpOnly = true
            };

            if (ticket.IsPersistent)
            {
                cookie.Expires = ticket.Expiration;
            }

            cookie.Secure = FormsAuthentication.RequireSSL;
            cookie.Path = FormsAuthentication.FormsCookiePath;
            if (FormsAuthentication.CookieDomain != null)
            {
                cookie.Domain = FormsAuthentication.CookieDomain;
            }

            _httpContext.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void SignOut()
        {
            foreach (var name in _httpContext.Request.Cookies.AllKeys)
            {
                HttpCookie expiredCookie = new HttpCookie(name)
                {
                    Domain = FormsAuthentication.CookieDomain,
                    Expires = DateTime.Now.AddDays(-1)
                };
                _httpContext.Response.Cookies.Add(expiredCookie);
            }

            FormsAuthentication.SignOut();
        }

        /// <summary>
        /// 获取登录用户.
        /// </summary>
        /// <returns>IUser.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public T GetSiginUser()
        {
            T user = new T();

            if (!this.IsLogin)
            {
                return (T)user.GetUser(Guid.Empty.ToString(), string.Empty);
            }
            var formsIdentity = (FormsIdentity)_httpContext.User.Identity;

            user = (T)user.GetUser(_httpContext.User.Identity.Name, formsIdentity.Ticket.UserData);

            return user;
        }
    }
}
