using WZK.Api.Code;
using WZK.Api.IAttribute;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WZK.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务 开启跨域支持
            EnableCrossSiteRequests(config);
            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //注册授权过滤器
            config.MessageHandlers.Add(new AuthorizationHandler());
            //语言国际化支持
            //config.Filters.Add(new CultureFilter());
            //注册异常处理过滤器
            config.Filters.Add(new LogActionFilter());
        }
        public static void EnableCrossSiteRequests(HttpConfiguration config)
        {
            var cors = new EnableCorsAttribute(origins: "*", headers: "*", methods: "*");
            config.EnableCors(cors);
        }
    }
}
