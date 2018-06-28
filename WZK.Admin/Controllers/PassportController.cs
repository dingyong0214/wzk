using WZK.Admin.Common;
using WZK.Business.Admin;
using WZK.Common;
using WZK.Common.Authentication;
using System;
using System.Web.Mvc;

namespace WZK.Admin.Controllers
{
    /// <summary>
    /// 登录
    /// author:dingyong
    /// date:2016-07-01
    /// </summary>
    [AllowAnonymous]
    public class PassportController : BaseController
    {
        #region 登录页面
        /// <summary>
        /// 登录页面
        /// </summary>
        /// <returns></returns>      
        public ActionResult Login()
        {
            ViewBag.ReturnUrl = Request.QueryString["ReturnUrl"] ?? "/";
            return View();
        }
        #endregion

        #region 登录
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">登录密码</param>
        /// <returns>JsonResult.</returns>
        /// <remarks>add by 谢四仁,2016-07-19 16:15:52 </remarks>
        [HttpPost]
        public JsonResult Login(string userName, string password)
        {
            var info = new AdminUserMgt().GetUser(userName, password);

            if (info.IsPass)
            {
                SignIn(info.ReturnObject.Id, info.ReturnObject.UserName, info.ReturnObject.Mobile);
                RightInfo.GetRightInfo().CacheRight(info.ReturnObject.Id);
            }          

            return Json(new ResultSingle<dynamic>()
            {
                Status = info.Status,
                ReturnObject = (info.ReturnObject == null ? null : new
                {
                    info.ReturnObject.Id,
                    info.ReturnObject.Name
                })
            });
        }


        /// <summary>
        /// 登录.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="mobile">The mobile.</param>
        /// <remarks>add by dingyong,2017-02-15 15:43:55 </remarks>
        [NonAction]
        private void SignIn(Guid id, string name, string mobile)
        {
            var role = string.Empty;
            if (name == null)
            {
                name = string.Empty;
            }
            if (!string.IsNullOrEmpty(id.ToString()))
            {
                role = new WZK.Business.Admin.AdminUserMgt().GetUserRoleName(id);
            }
            new WZK.Common.Authentication.Impl.FormsAuthenticationService<AuthUser>().SignIn(new AuthUser(id, role, name, mobile), false);
        }

        #endregion

        #region 退出登录
        public ActionResult Logout()
        {
            new WZK.Common.Authentication.Impl.FormsAuthenticationService<AuthUser>().SignOut();
            return RedirectToAction("Login");
        }
        #endregion

    }
}