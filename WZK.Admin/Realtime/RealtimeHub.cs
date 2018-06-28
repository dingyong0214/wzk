using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace WZK.Admin
{
    /// <summary>
    /// 设备实时控制 （SignalR集线器类 v2）
    /// </summary>
    /// <remarks>added by dingyong,2017-09-25</remarks>
    public class RealtimeHub : Hub
    {

        #region 发送普通消息
        /// <summary>
        /// 发送普通消息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="message"></param>
        public void Send(string name, string message)
        {
            //调用所有客户端addNewMessageToPage函数传递数据
            Clients.All.addNewMessageToPage(name, message);
        }
        #endregion

        #region 发送会话Id
        /// <summary>
        /// 发送会话Id
        /// </summary>
        public void SendConnectionId()
        {
            Clients.Client(Context.ConnectionId).sendConnectionId(Context.ConnectionId);
        }
        #endregion

        #region 连接连接到此集线器实例时调用
        /// <summary>
        /// 连接连接到此集线器实例时调用。
        /// </summary>
        /// <returns>A <see cref="T:System.Threading.Tasks.Task" /></returns>
        public override Task OnConnected()
        {
            //给客户端推送ConnectionId
            SendConnectionId();
            return base.OnConnected();
        }
        #endregion

        #region 当一个连接断开或由于超时调用
        /// <summary>
        ///当一个连接断开或由于超时调用
        /// </summary>
        /// <param name="stopCalled">
        /// true, 如果客户端调用停止，则关闭该连接； 
        /// false, 如果连接已丢失或超时可以通过客户端重新连接到另一个服务器。 
        /// <see cref="P:Microsoft.AspNet.SignalR.Configuration.IConfigurationManager.DisconnectTimeout" />.
        /// </param>
        /// <returns>A <see cref="T:System.Threading.Tasks.Task" /></returns>
        public override Task OnDisconnected(bool stopCalled)
        {
            //  标记离线.
            Clients.Client(Context.ConnectionId).onUserDisconnected("Disconnected");//调用客户端用户离线通知
            return base.OnDisconnected(stopCalled);
        }
        #endregion

        #region 重新连接调用
        /// <summary>
        /// 当连接重新连接
        /// </summary>
        /// <returns>A <see cref="T:System.Threading.Tasks.Task" /></returns>
        public override Task OnReconnected()
        {
            // 可以标记用户离线后重新连接,标记为在线   
            SendConnectionId();
            return base.OnReconnected();
        }
        #endregion

    }
}