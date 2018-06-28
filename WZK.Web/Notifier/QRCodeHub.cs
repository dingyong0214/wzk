using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace WZK.Web
{
    /// <summary>
    /// 扫码登陆 （SignalR集线器类 v2）
    /// </summary>
    /// <remarks>added by dingyong,2017-03-06</remarks>
    public class QRCodeHub : Hub
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

    }
}