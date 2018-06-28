using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;

namespace WZK.Admin.Core
{
    /// <summary>
    /// 权限过滤器
    /// author:dingyong
    /// date:2016-07-06
    /// </summary>
    public class AuthFilter : ActionFilterAttribute
    {
        public class ActionInfo
        {
            /// <summary>
            /// Action名称
            /// </summary>
            public string Action { get; set; }

            /// <summary>
            /// 控制器名称
            /// </summary>
            public string Controller { get; set; }
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //白名单
            var whiteAction = GetWhiteAction();

            //获取用户要请求的页面
            ActionInfo context = new ActionInfo() { Action = filterContext.ActionDescriptor.ActionName, Controller = filterContext.Controller.GetType().Name.Replace("Controller", "") };
            //判断是否有权访问
            if (!(whiteAction.IndexOf(context.Controller + context.Action + ",", 0, whiteAction.Length, StringComparison.CurrentCultureIgnoreCase) >= 0 || Common.RightInfo.GetRightInfo().CanUse(context.Controller, context.Action)))
            {
                filterContext.Result = new RedirectToRouteResult("Default", new System.Web.Routing.RouteValueDictionary(new { controller = "Home", action = "Authorize" }));
            }
        }

        /// <summary>
        /// 获取白名单
        /// </summary>
        /// <returns>白名单</returns>
        /// <remarks>author:*** date:2016-07-06</remarks>
        public string GetWhiteAction()
        {
            if (HttpRuntime.Cache["WhiteAction"] == null)
            {
                StringBuilder actions = new StringBuilder();
                var assembly = Assembly.GetExecutingAssembly();//获取当前程序集

                foreach (var type in assembly.GetTypes())
                {
                    if (type.BaseType == typeof(Controller) || type.BaseType == typeof(WZK.Common.BaseController))//取出Controller
                    {
                        var methods = type.GetMethods();
                        foreach (var method in methods)
                        {
                            if (method.ReturnType == typeof(ActionResult) || (method.ReturnType.BaseType != null && method.ReturnType.BaseType == typeof(ActionResult)))//取出Action
                            {
                                //允许匿名访问
                                if (method.GetCustomAttributes(typeof(AllowAnonymousAttribute)).Count() > 0 || method.GetCustomAttributes(typeof(WZK.Business.Admin.Tool.WhiteAction)).Count() > 0 || type.GetCustomAttributes(typeof(AllowAnonymousAttribute)).Count() > 0 || type.GetCustomAttributes(typeof(WZK.Business.Admin.Tool.WhiteAction)).Count() > 0)
                                {
                                    actions.Append(type.Name.Replace("Controller", ""));
                                    actions.Append(method.Name);
                                    actions.Append(",");
                                }
                            }
                        }
                    }
                }
                HttpRuntime.Cache.Insert("whiteAction", actions.ToString(), null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(30));
            }
            return HttpRuntime.Cache["whiteAction"].ToString();
        }
    }
}