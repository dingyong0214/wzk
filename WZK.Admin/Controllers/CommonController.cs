using WZK.Business.Admin;
using WZK.Common;
using WZK.Common.Authentication;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Collections.Generic;

namespace WZK.Admin.Controllers
{
    /// <summary>
    /// 共用控制器
    /// author:dingyong
    /// date:2017-05-12
    /// </summary>
    [AllowAnonymous]
    public class CommonController : Controller
    {
        #region 图形验证码
        /// <summary>
        /// 图形验证码
        /// </summary>
        /// <returns></returns>
        /// <remarks>author:dingyong date:2017-05-12</remarks>
        [AllowAnonymous]
        public FileResult VerifyCode()
        {
            var cap = new WZK.Common.CreateCaptcha().CreateImage();
            return File(cap, "image/jpeg");
        }
        #endregion

        #region 图片上传
        /// <summary>
        /// 图片上传
        /// </summary>
        /// <returns></returns>
        /// <remarks>author:dingyong date:2017-05-12</remarks>
        public ActionResult UploadImg()
        {
            WebImage pic = ImgHandler.Instance.GetImageFromRequest();
            string typeStr = Request["type"];
            int type = 0;
            if (!int.TryParse(typeStr, out type))
            {
                type = 0;
            }
            if (pic != null)
            {
                string path = ImgHandler.Instance.UploadImg(pic, type);
                return Json(new { url = path });
            }
            return Json(new { url = "" });
        }
        #endregion

        #region 多张图片上传
        /// <summary>
        /// 多张图片上传
        /// </summary>
        /// <returns></returns>
        /// <remarks>author:dingyong date:2017-05-12</remarks>
        public ActionResult UploadMuchImg()
        {
            List<WebImage> picList = ImgHandler.Instance.GetMuchImageFromRequest();
            string typeStr = Request["type"];
            int type = 0;
            if (!int.TryParse(typeStr, out type))
            {
                type = 0;
            }
            if (picList != null)
            {
                string path = "";
                foreach (var item in picList)
                {
                    path += ImgHandler.Instance.UploadImg(item, type)+",";
                }
                return Json(new { url = path.Substring(0,path.Length-1) });
            }
            return Json(new { url = "" });
        }
        #endregion
    }
}