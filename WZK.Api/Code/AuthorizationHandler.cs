using System;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Web;
using WZK.Business;
using WZK.Common;

namespace WZK.Api.Code
{
    public class AuthorizationHandler : DelegatingHandler
    {
        Token m = new Token();

        #region 验证登录
        /// <summary>
        /// 验证登录
        /// </summary>
        /// <param name="request">请求</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //获取请求头中的userId，token信息
            string userId = null;
            var token = Enumerable.Empty<string>();
            bool existToken = request.Headers.TryGetValues("Token", out token);
            if (existToken && m.IsLogin(token.FirstOrDefault(), out userId))
            {
                if (!string.IsNullOrEmpty(userId))
                {
                    SetLoginInfo(userId);
                }
            }
            return base.SendAsync(request, cancellationToken);
        }
        #endregion

        #region 设置登录信息
        /// <summary>
        /// 设置登录信息
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <remarks>author:zengbo date:2015-10-26</remarks>
        private void SetLoginInfo(string userId)
        {
            GenericIdentity identity = new GenericIdentity(userId);
            GenericPrincipal loginInfo = new GenericPrincipal(identity, new string[] { "User" });
            Thread.CurrentPrincipal = loginInfo;
            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = loginInfo;
            }
        } 
        #endregion
    }
}