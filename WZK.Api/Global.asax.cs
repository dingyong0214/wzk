using System;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;

namespace WZK.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// 需处理的http状态
        /// </summary>
        static int[] httpStatus = { 401, 404, 405, 500 };
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        /// <summary>
        /// 出现错误时重写错误信息
        /// </summary>
        /// <remarks>author:zengbo date:2015-10-26</remarks>
        protected void Application_EndRequest()
        {
            var response = HttpContext.Current.Response;

            if (httpStatus.Any(s => s == response.StatusCode))
            {
                response.ContentType = "application/json;charset=utf-8";
                response.ClearContent();
                byte[] bytes = null;
                Common.ErrorStatus status = (Common.ErrorStatus)Enum.Parse(typeof(Common.ErrorStatus), "S0" + response.StatusCode);
                if (response.StatusCode == 401)
                {
                    status = Common.ErrorStatus.S0017;
                    response.StatusCode = 200;
                }
                bytes = Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(new Common.ResultInfo(status)));
                response.BinaryWrite(bytes);
            }
        }

        public override void Init()
        {
            this.PostAuthenticateRequest += (sender, e) => HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
            base.Init();
        }
    }
}
