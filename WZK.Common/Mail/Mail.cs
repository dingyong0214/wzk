// ==============================================
// Author	  : zj
// Create date: 2014-05-11 21:16:00
// Description: DefaultMail
// Update	  : 
// ===============================================

using System.Net;
using System.Net.Mail;
using System.Linq;
using System;

namespace WZK.Common
{
    /// <summary>
    /// 默认邮件服务
    /// </summary>
    public class Mail 
    {       
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="subject">标题</param>
        /// <param name="body">内容</param>
        /// <param name="receiveAddress">收件地址.</param>
        /// <remarks>add by zj,2014-05-11 20:22:11 </remarks>
        public static bool Send(string subject, string body, params string[] receiveAddress)
        {
            bool returnVal = false;
            try
            {
                string fromAddress = "de_shuai@qq.com";//发件人
                string displayName = "嗨了么(Hile.me)";
                string host = "smtp.qq.com";
                string userName = "de_shuai@qq.com";
                string password = "z,J.1018";
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(fromAddress, displayName);

                receiveAddress.ToList().ForEach(p => mail.To.Add(new MailAddress(p)));

                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                SmtpClient client = new SmtpClient();
                client.Host = host;
                client.Credentials = new NetworkCredential(userName, password);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;

                client.Send(mail);
                returnVal = true;
            }
            catch(Exception ex) {
                throw ex;
            }
            return returnVal;
        }
    }
}
