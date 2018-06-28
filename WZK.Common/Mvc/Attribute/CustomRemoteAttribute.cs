// ==============================================
// Author	  : dingyong
// Create date: 2016-07-13 14:54:23
// Description: CustomRemoteAttribute
// Update	  : 
// ===============================================
using System.Web.Mvc;

namespace WZK.Common
{
    /// <summary>
    /// 提供使用 jQuery 验证插件远程以及后台验证程序的特性e.
    /// </summary>
    public class CustomRemoteAttribute : RemoteAttribute
    {
        /// <summary>
        /// 使用指定的操作方法名称和控制器名称来初始化 <see cref="T:System.Web.Mvc.RemoteAttribute" /> 类的新实例。
        /// </summary>
        /// <param name="action">操作方法的名称。</param>
        /// <param name="controller">控制器的名称。</param>
        public CustomRemoteAttribute(string action, string controller)
            : base(action, controller)
        {
            Action = action;
            Controller = controller;
        }
        /// <summary>
        /// 操作方法的名称.
        /// </summary>
        /// <value>The action.</value>
        public string Action { get; set; }
        /// <summary>
        /// 控制器的名称.
        /// </summary>
        /// <value>The controller.</value>
        public string Controller { get; set; }
    }
}
