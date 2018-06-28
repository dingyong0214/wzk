using System;
using Microsoft.AspNet.SignalR;

namespace WZK.Admin
{
    /// <summary>
    /// 消息推送
    /// </summary>
    /// <remarks>added by dingyong,2017-09-25</remarks>
    public static class Notifier
    {
        private static readonly IHubContext Context = GlobalHost.ConnectionManager.GetHubContext<RealtimeHub>();

        #region 消息推送至客户端
        /// <summary>
        /// 消息推送至客户端
        /// </summary>
        /// <param name="connectionId">会话Id</param>
        /// <param name="msg">消息内容</param>
        public static void NotifierClientMessage(string connectionId, string msg)
        {
            Context.Clients.Client(connectionId).notifierClientMessage(msg);
        }
        #endregion

        #region 通知图片上传成功
        /// <summary>
        /// 通知图片上传成功
        /// </summary>
        /// <param name="connectionId">会话Id</param>
        /// <param name="msg">消息内容</param>
        public static void NotifierClientUploadSs(string connectionId, string msg)
        {
            Context.Clients.Client(connectionId).notifierClientUploadSs(msg);
        }
        #endregion

        #region 通知图片上传失败
        /// <summary>
        /// 通知图片上传失败
        /// </summary>
        /// <param name="connectionId">会话Id</param>
        /// <param name="msg">消息内容</param>
        public static void NotifierClientUploadFail(string connectionId, string msg)
        {
            Context.Clients.Client(connectionId).notifierClientUploadFail(msg);
        }
        #endregion

        #region 发送会话Id
        /// <summary>
        /// 发送会话Id
        /// </summary>
        /// <param name="connectionId">会话Id</param>
        public static void SendConnectionId(string connectionId)
        {
            Context.Clients.Client(connectionId).sendConnectionId(connectionId);
        }
        #endregion

    }
}