using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WZK.Common.ConfigData
{
    /// <summary>
    /// 邮件相关配置
    /// author:lorne
    /// date:2016-01-14
    /// </summary>
    public class EmailConfig
    {
        /// <summary>
        /// 邮件服务器
        /// </summary>
        public static string Host = ConfigurationManager.AppSettings["EmailHost"];
        /// <summary>
        /// 发送邮件的地址
        /// </summary>
        public static string Email = ConfigurationManager.AppSettings["EmailAddress"];
        /// <summary>
        /// 发送邮件的密码
        /// </summary>
        public static string Pwd = ConfigurationManager.AppSettings["EmailPwd"];
    }
}
