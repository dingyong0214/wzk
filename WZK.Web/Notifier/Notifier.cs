using Microsoft.AspNet.SignalR;
using System;
namespace WZK.Web
{
    /// <summary>
    /// 消息推送
    /// </summary>
    /// <remarks>added by dingyong,2017-03-06</remarks>
    public static class Notifier
    {
        private static readonly IHubContext Context = GlobalHost.ConnectionManager.GetHubContext<QRCodeHub>();

        #region 消息推送至客户端
        /// <summary>
        /// 消息推送至客户端
        /// </summary>
        /// <param name="connectionId">会话Id</param>
        /// <param name="msg">消息内容</param>
        public static void NotifierClientMessage(string msg)
        {
            Context.Clients.All.notifierClientMessage(msg);
        }
        #endregion

    }
}