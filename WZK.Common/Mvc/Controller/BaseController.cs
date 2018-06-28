using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using WZK.Common.Authentication;
using WZK.Common.Authentication.Impl;

namespace WZK.Common
{
    /// <summary>
    /// 基础
    /// author:lorne
    /// date:2016-03-01
    /// </summary>
    [Authorize]
    public class BaseController : Controller
    {
        /// <summary>
        /// 当前登录用户
        /// </summary>
        protected AuthUser SignInUser { get { return new FormsAuthenticationService<AuthUser>().GetSiginUser(); } }

        /// <summary>
        /// 是否已登录   
        /// </summary>
        protected bool IsLogin { get { return SignInUser.Id != Guid.Empty; } }

        /// <summary>
        /// 当前用户ID
        /// </summary>
        protected Guid UserId { get { return SignInUser.Id; } }
        /// <summary>
        /// 当前用户角色
        /// </summary>
        protected string RoleName { get { return SignInUser.RoleName; } }


        /// <summary>
        /// 重写Json方法
        /// </summary>
        /// <param name="data">要序列化的 JavaScript 对象图</param>
        /// <param name="contentType">内容类型（MIME 类型）</param>
        /// <param name="contentEncoding">内容编码</param>
        /// <param name="behavior">JSON 请求行为</param>
        /// <returns>将指定对象序列化为 JSON 格式的结果对象</returns>
        /// <remarks>author:lorne date:2015-11-21</remarks>
        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new NewJsonResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
        }

        /// <summary>
        /// 获取ModelValid信息
        /// </summary>
        /// <returns></returns>
        /// <remarks>author:lorne date:2015-12-04</remarks>
        protected string GetErrors()
        {
            StringBuilder msg = new StringBuilder();
            var errorList = ModelState.Values.Where(v => v.Errors.Count > 0).ToList();
            foreach (var item in errorList)
            {
                foreach (var error in item.Errors)
                {
                    msg.AppendLine(error.ErrorMessage);
                }
            }
            return msg.ToString();
        }

        /// <summary>
        /// 获取ModelValid对应错误信息.
        /// </summary>
        /// <returns>JsonResult.</returns>
        /// <remarks>add by dingyong,2016-07-07 19:58:37 </remarks>
        protected JsonResult GetAjaxErrorInfo()
        {
            string errorMsg = null,
                   errorKey = null;

            foreach (var item in ModelState)
            {
                foreach (var error in item.Value.Errors)
                {
                    errorMsg = error.ErrorMessage;
                    errorKey = item.Key;
                }
            } 
            return Json(new { IsPass = false, errorMsg, errorKey });
        }

        /// <summary>
        ///cookie名称
        /// </summary>
        protected static string CookieDeliveryAreaCode = "#deliveryAreaCode#";
        /// <summary>
        /// 获取用户选择的区域
        /// </summary>
        /// <returns>区域编码</returns>
        /// <remarks>add by dingyong,2017-02-16 09:23:18 </remarks>
        protected int GetCurrentAreaCode()
        {
            //判断Cookie中是否存在
            string deliveryAreaCode = Tool.GetCookie(CookieDeliveryAreaCode);
            int areaCode = 440304;
            //cookie不存在，默认返回深圳市区域
            if (string.IsNullOrWhiteSpace(deliveryAreaCode))
            {
                return areaCode;
            }
            areaCode = ConvertTryParse.TryParseInt(deliveryAreaCode);
            if (areaCode <= 0)
            {
                areaCode = 440304;
            }
            return areaCode;
        }
    }

    /// <summary>
    /// 自定义JsonResult
    /// </summary>
    public class NewJsonResult : JsonResult
    {
        /// <summary>
        /// Newtonsoft.json设置项
        /// </summary>
        public JsonSerializerSettings Settings { get; private set; }

        public NewJsonResult()
        {
            Settings = new JsonSerializerSettings
            {
                DateFormatString = "yyyy-MM-dd HH:mm:ss"
            };
        }

        /// <summary>
        /// 重写ExecuteResult
        /// </summary>
        /// <param name="context"></param>
        /// <remarks>author:lorne date:2015-11-21</remarks>
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            if (this.JsonRequestBehavior == JsonRequestBehavior.DenyGet && string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("JSON GET is not allowed");

            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = string.IsNullOrEmpty(this.ContentType) ? "application/json" : this.ContentType;

            if (this.ContentEncoding != null)
                response.ContentEncoding = this.ContentEncoding;
            if (this.Data == null)
                return;

            var scriptSerializer = JsonSerializer.Create(this.Settings);

            using (var sw = new StringWriter())
            {
                scriptSerializer.Serialize(sw, this.Data);
                response.Write(sw.ToString());
            }
        }
    }
}