using System;
using System.Collections.Generic;
using System.Configuration;
using com.igetui.api.openservice;
using com.igetui.api.openservice.igetui;
using com.igetui.api.openservice.igetui.template;
using com.igetui.api.openservice.payload;
using WZK.Common;
using log4net;
using System.Reflection;
using System.ComponentModel;

namespace WZK.Business
{
    /// <summary>
    /// 推送
    /// </summary>
    /// <remarks>added by dingyong,2017-05-12</remarks>
    public class TuiSong : BaseBusiness
    {
        static string appId = ConfigurationManager.AppSettings["GtAppId"];
        static string appKey = ConfigurationManager.AppSettings["GtAppKey"];
        static string host = ConfigurationManager.AppSettings["GtHost"];
        static string masterSecret = ConfigurationManager.AppSettings["GtMasterSecret"];

        #region 对单个用户透传推送
        /// <summary>
        /// 对单个用户透传推送
        /// </summary>
        /// <param name="clientId">android推送Id：ClientId</param>
        /// <param name="notice">推送内容</param>
        /// <remarks>author:dingyong date:2017-05-25</remarks>
        public static void PushMessageToSingle(string clientId,NoticeMessage notice)
        {
            try
            {
                IGtPush push = new IGtPush(host, appKey, masterSecret);
                // 定义"AppMessage"类型消息对象，设置消息内容模板、发送的目标App列表、是否支持离线发送、以及离线消息有效期(单位毫秒)
                SingleMessage message = new SingleMessage();
                TransmissionTemplate template = TransmissionTemplate(notice);
                message.IsOffline = true;  // 用户当前不在线时，是否离线存储,可选
                message.OfflineExpireTime = 1000 * 3600 * 12;     // 离线有效时间，单位为毫秒，可选
                message.Data = template;
                com.igetui.api.openservice.igetui.Target target = new com.igetui.api.openservice.igetui.Target();
                target.appId = appId;
                target.clientId = clientId;
                String pushResult = push.pushMessageToSingle(message, target);
                log.Info(pushResult);
            }
            catch (Exception ex)
            {
                log.Error(ex.StackTrace);
            }
        }
        #endregion

        #region 对单个用户透传推送2
        /// <summary>
        /// 对单个用户透传推送2
        /// </summary>
        /// <param name="clientId">android推送Id：ClientId</param>
        /// <param name="notice">推送内容</param>
        /// <remarks>author:dingyong date:2017-05-25</remarks>
        public static void PushMsgToSingle(string clientId, NoticeMsg notice)
        {
            try
            {
                IGtPush push = new IGtPush(host, appKey, masterSecret);
                // 定义"AppMessage"类型消息对象，设置消息内容模板、发送的目标App列表、是否支持离线发送、以及离线消息有效期(单位毫秒)
                SingleMessage message = new SingleMessage();
                TransmissionTemplate template = TransmissionTemplates(notice);
                message.IsOffline = true;  // 用户当前不在线时，是否离线存储,可选
                message.OfflineExpireTime = 1000 * 3600 * 12;     // 离线有效时间，单位为毫秒，可选
                message.Data = template;
                com.igetui.api.openservice.igetui.Target target = new com.igetui.api.openservice.igetui.Target();
                target.appId = appId;
                target.clientId = clientId;
                String pushResult = push.pushMessageToSingle(message, target);
                log.Info(pushResult);
            }
            catch (Exception ex)
            {
                log.Error(ex.StackTrace);
            }
        }
        #endregion

        #region 对多个用户推送
        /// <summary>
        /// 对多个用户推送
        /// </summary>
        /// <param name="androidCid">android推送Id：ClientId</param>
        /// <param name="content">推送消息内容</param>
        public static void PushMessageToList(List<string> androidCid, NoticeMessage content)
        {
            try
            {
                  // 推送主类（方式1，不可与方式2共存）
                IGtPush push = new IGtPush(host, appKey, masterSecret);
                // 推送主类（方式2，不可与方式1共存）此方式可通过获取服务端地址列表判断最快域名后进行消息推送，每10分钟检查一次最快域名
                //IGtPush push = new IGtPush(host, appKey, masterSecret);
                ListMessage message = new ListMessage();

                TransmissionTemplate template = TransmissionTemplate(content);
                // 用户当前不在线时，是否离线存储,可选
                message.IsOffline = true;
                // 离线有效时间，单位为毫秒，可选
                message.OfflineExpireTime = 1000 * 3600 * 12;
                message.Data = template;
                message.PushNetWorkType = 0;        //判断是否客户端是否wifi环境下推送，1为在WIFI环境下，0为不限制网络环境。
                //设置接收者,接收者为空则对所有手机推送
                List<com.igetui.api.openservice.igetui.Target> targetList = new List<com.igetui.api.openservice.igetui.Target>();
                androidCid.ForEach(c =>
                {
                    com.igetui.api.openservice.igetui.Target target = new com.igetui.api.openservice.igetui.Target();
                    target.appId = appId;
                    target.clientId =c;
                    targetList.Add(target);
                });
                String contentId = push.getContentId(message);
                String pushResult = push.pushMessageToList(contentId, targetList);
                log.Info(pushResult);//推送结果
            }
            catch (Exception ex)
            {
                log.Error(ex.StackTrace);
            }
        }

        #endregion

        #region 透传模板动作内容
        /// <summary>
        /// 透传模板动作内容
        /// </summary>
        /// <param name="notice">透传内容</param>
        /// <returns></returns>
        /// <remarks>author:dingyong date:2017-05-25</remarks>
        public static TransmissionTemplate TransmissionTemplate(NoticeMessage notice)
        {
            TransmissionTemplate template = new TransmissionTemplate();
            template.AppId = appId;
            template.AppKey = appKey;
            //应用启动类型，1：强制应用启动 2：等待应用启动
            template.TransmissionType = "2";
            //透传内容
            string content = string.Format("{{title:\"{0}\",content:\"{1}\",payload:{{\"type\":{2},\"data\":{3}}}}}", notice.Title, notice.Content, notice.MessageType, notice.ToString());
            template.TransmissionContent = content;
            //设置通知定时展示时间，结束时间与开始时间相差需大于6分钟，消息推送后，客户端将在指定时间差内展示消息（误差6分钟）
            //String begin = DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
            //String end = DateTime.Now.AddMinutes(6).ToString("yyy-MM-dd HH:mm:ss");
            //template.setDuration(begin, end);

            return template;
        }
        #endregion

        #region 透传模板动作内容2
        /// <summary>
        /// 透传模板动作内容2
        /// </summary>
        /// <param name="notice">透传内容</param>
        /// <returns></returns>
        /// <remarks>author:dingyong date:2017-05-25</remarks>
        public static TransmissionTemplate TransmissionTemplates(NoticeMsg notice)
        {
            TransmissionTemplate template = new TransmissionTemplate();
            template.AppId = appId;
            template.AppKey = appKey;
            //应用启动类型，1：强制应用启动 2：等待应用启动
            template.TransmissionType = "2";
            //透传内容
            string content = string.Format("{{title:\"{0}\",content:\"{1}\",payload:{{\"type\":{2},\"data\":{3}}}}}", notice.Title, notice.Content, notice.MessageType, notice.ToString());
            template.TransmissionContent = content;
            //设置通知定时展示时间，结束时间与开始时间相差需大于6分钟，消息推送后，客户端将在指定时间差内展示消息（误差6分钟）
            //String begin = DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
            //String end = DateTime.Now.AddMinutes(6).ToString("yyy-MM-dd HH:mm:ss");
            //template.setDuration(begin, end);

            return template;
        }
        #endregion
    }

    #region 消息类型
    /// <summary>
    /// 消息类型 1-新增任务，2-删除任务，3-修改任务，4-修改模板
    /// </summary>
    public enum NoticeType
    {
        /// <summary>
        /// 新增任务
        /// </summary>
        [Description("新增任务")]
        AddTask = 1,
        /// <summary>
        /// 删除任务
        /// </summary>
        [Description("删除任务")]
        DelTask = 2,
        /// <summary>
        /// 修改任务
        /// </summary>
        [Description("修改任务")]
        ModifyTask = 3,
        /// <summary>
        /// 修改模板
        /// </summary>
        [Description("修改模板")]
        ModifyTemplate = 4
    }
    #endregion

    #region 通知消息（有任务栏通知，需要点击，通过click事件获取数据）
    /// <summary>
    /// 通知消息（有任务栏通知，需要点击，通过click事件获取数据）
    /// </summary>
    public class NoticeMessage
    {
        /// <summary>
        /// 消息类型 1-新增任务，2-删除任务
        /// </summary>
        public int MessageType { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; } = "微智控";
        /// <summary>
        /// 任务类型：1-执行脚本，2-自动回复，3-发朋友圈,4-定位,5-截屏上传
        /// </summary>
        public int TaskType { get; set; }
        /// <summary>
        /// 任务编号
        /// </summary>
        public Guid TaskId { get; set; }
        /// <summary>
        /// 任务模板编号
        /// </summary>
        public Guid TemplateId { get; set; }
        /// <summary>
        /// 执行周期:单位分钟  0-无周期，任务只执行1次，大于0 - n分钟执行一次
        /// </summary>
        public int Cycle { get; set; }
        /// <summary>
        /// 需要执行的次数：0-无限次，大于0-n次
        /// </summary>
        public int Times { get; set; }
        /// <summary>
        /// 任务开始执行时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 重载ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{{TaskType:\"{0}\",TaskId:\"{1}\",TemplateId:\"{2}\",Cycle:\"{3}\",Times:\"{4}\",StartTime:\"{5}\"}}", TaskType, TaskId, TemplateId, Cycle, Times, StartTime);
        }
    }
    #endregion

    #region 通知消息2（有任务栏通知，需要点击，通过click事件获取数据）
    /// <summary>
    /// 通知消息（有任务栏通知，需要点击，通过click事件获取数据）
    /// </summary>
    public class NoticeMsg
    {
        /// <summary>
        /// 消息类型 1-新增任务，2-删除任务
        /// </summary>
        public int MessageType { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; } = "微智控";
        /// <summary>
        /// 任务类型：1-执行脚本，2-自动回复，3-发朋友圈,4-定位,5-截屏上传
        /// </summary>
        public int TaskType { get; set; }
        /// <summary>
        /// 操作类型 ：1-坐标，2-菜单，3,-home键，4-返回，后退，5-滚屏
        /// </summary>
        public int OperationType { get; set; }
        /// <summary>
        /// 坐标
        /// </summary>
        public string Coordinate { get; set; }
        /// <summary>
        /// 会话Id
        /// </summary>
        public string ConnectionId { get; set; }
        /// <summary>
        /// 实时控制编号
        /// </summary>
        public Guid ControlId { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 重载ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{{TaskType:\"{0}\",Coordinate:\"{1}\",ConnectionId:\"{2}\",ControlId:\"{3}\",OperationType:\"{4}\"}}", TaskType, Coordinate, ConnectionId, ControlId, OperationType);
        }
    }
    #endregion
}
