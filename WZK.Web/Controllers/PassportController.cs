// ==============================================
// Author	  : dingyong
// Create date: 2017-04-21 14:13:03
// Description: 登录控制器
// Update	  : 
// ===============================================
using WZK.Common;
using WZK.Common.Authentication;
using System.Web.Mvc;
using WZK.Web.Models;
using WZK.Entity;
using WZK.Models;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Collections.Specialized;
using WZK.Business;
using System.Web;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace WZK.Web.Controllers
{
    [AllowAnonymous]
    public class PassportController : BaseController
    {
        #region 登录

        #region 视图
        /// <summary>
        /// 页面
        /// </summary>
        /// <returns>ActionResult.</returns>
        /// <remarks>add by dingyong,2017-04-21 15:27:01 </remarks>
        [HttpGet]
        public ActionResult Login()
        {
            if (base.IsLogin)
            {
                //return RedirectToAction("Index", "Home");
            }
            ViewBag.ReturnUrl = Request.QueryString["ReturnUrl"] ?? "/";
            return View();
        }
        #endregion

        #region 提交
        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="param">登录视图.</param>
        /// <returns>JsonResult.</returns>
        /// <remarks>add by dingyong,2017-04-21 16:56:37 </remarks>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult Login(LoginViewModel param)
        {
            var info = new ResultSingle<Member>() { Status = ErrorStatus.S0002 };
            if (ModelState.IsValid)
            {
                info = new WZK.Business.User().Login(param.Mobile);
                if (info.IsPass)
                {
                    new WZK.Common.Authentication.Impl.FormsAuthenticationService<AuthUser>().SignIn(new AuthUser(info.ReturnObject.Id, "", info.ReturnObject.Name, info.ReturnObject.Mobile), true);
                }
            }
            else
            {
                info.Desc = this.GetErrors();
            }
            return Json(new ResultSingle<dynamic>()
            {
                Status = info.Status,
                Desc = info.Desc
            });
        }
        #endregion

        #endregion

        #region 注销
        /// <summary>
        /// Logouts this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        /// <remarks>add by dingyong,2017-04-21 16:26:19 </remarks>
        public ActionResult Logout()
        {
            new WZK.Common.Authentication.Impl.FormsAuthenticationService<AuthUser>().SignOut();
            return RedirectToAction("Login");
        }
        #endregion

        #region 登陆二维码页面
        /// <summary>
        /// 登陆二维码
        /// </summary>
        /// <param name="id">url</param>
        /// <returns>ActionResult.</returns>
        public ActionResult LoginCode()
        {
            string url = Encrypt.EncryptDES(ConfigurationManager.AppSettings["LoginUrl"], "dingyong");
            ViewBag.ImgUrl = "/Passport/QdCode/"+ url;
            return View();
        }
        #endregion

        #region 返回登陆二维码
        /// <summary>
        /// 返回登陆二维码
        /// </summary>
        /// <param name="id">url</param>
        /// <returns>ActionResult.</returns>
        public ActionResult QdCode(string id)
        {
            byte[] bytes = null;
            if (!string.IsNullOrEmpty(id))
            {
                string url = Encrypt.DecryptDES(id, "dingyong");
                Bitmap bitmap = QRCodeHelper.CreateQRCodeWithLogo(url);
                try
                {
                    //保存图片数据
                    MemoryStream stream = new MemoryStream();
                    bitmap.Save(stream, ImageFormat.Png);
                    //输出图片流
                    bytes = stream.ToArray();
                }
                finally
                {
                    bitmap.Dispose();
                }
            }
            return File(bytes, @"image/png");  //返回支付二维码url(有效期30分钟)
        }
        #endregion

        #region 账号退出
        /// <summary>
        /// 账号退出
        /// </summary>
        /// <param name="id">url</param>
        /// <returns>ActionResult.</returns>
        public ActionResult LoginOut()
        {
            string url = Encrypt.EncryptDES("/Passport/Logout", "dingyong");
            ViewBag.ImgUrl = "/Passport/QdCode/" + url;
            return View();
        }
        #endregion
        
    }
}