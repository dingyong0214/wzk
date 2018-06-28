using WZK.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Net.Http;
using System.Net;
using System.Web.Http.Controllers;
using System.Net.Http.Headers;
using System.Linq;
using System.Globalization;

namespace WZK.Api.IAttribute
{
    /// <summary>
    /// 异常捕获
    /// </summary>
    /// <remarks>add by 赵小江，2015年10月26日, PM 05:55:44</remarks>
    public class LogActionFilter : ActionFilterAttribute, IExceptionFilter
    {
        protected Logger log = new Logger();
        public Task ExecuteExceptionFilterAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {
                log.Error("程序异常：" + actionExecutedContext.Request.RequestUri + "。" + actionExecutedContext.Exception.ToString(), actionExecutedContext.Exception);
                var msg = new ResultSingle<bool>() { Status = ErrorStatus.S0002, ReturnObject = false, Desc = actionExecutedContext.Exception.Message == null ? "哦唷,出错啦!" : "Exception：" + actionExecutedContext.Exception.Message };
                if (actionExecutedContext.Exception.InnerException != null)
                {
                    msg.Desc += actionExecutedContext.Exception.InnerException.Message;
                }
                actionExecutedContext.Response = new HttpResponseMessage()
                {
                    Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(msg), Encoding.UTF8, "application/json"),
                    StatusCode = HttpStatusCode.OK
                };
            });
        }
    }

    /// <summary>
    /// 国际化设置
    /// </summary>
    public class CultureFilter : ActionFilterAttribute
    {
        /// <summary>
        /// 设定区域
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            StringWithQualityHeaderValue acceptCultureHeader = actionContext.Request.Headers.AcceptLanguage.OrderByDescending(header => header.Quality).FirstOrDefault();
            if (null != acceptCultureHeader)
            {
                actionContext.Request.Properties["__CurrentCulture"] = Thread.CurrentThread.CurrentUICulture;
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(acceptCultureHeader.Value);
            }
        }
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            object culture;
            if (actionExecutedContext.Request.Properties.TryGetValue("__CurrentCulture", out culture))
            {
                Thread.CurrentThread.CurrentUICulture = (CultureInfo)culture;
            }
        }
    }
}