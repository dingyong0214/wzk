// ==============================================
// Author	  : dingyong
// Create date: 2016-07-08 15:50:19
// Description:  公共业务处理类
// Update	  : 
// ===============================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;
using System.Globalization;
using WZK.Entity;
using System.Threading.Tasks;
using WZK.Common;

namespace WZK.Business.Admin
{
    public class PublicBusiness
    {
        #region 添加操作日志
        /// <summary>
        /// 添加操作日志
        /// </summary>
        /// <param name="entity"></param>
        public static void AddOperateLog(OperateLog entity)
        {
            using (WZKEntities context = new WZKEntities())
            {
                entity.Id = Guid.NewGuid();
                entity.IP = GetIpAdress();
                entity.OperateTime = DateTime.Now;
                context.OperateLog.Add(entity);
                context.SaveChanges();
            }
        }
        #endregion

        #region 获取当前IP对应的 Ip(国家-省-市)
        /// <summary>
        /// 获取当前IP对应的 Ip(国家-省-市)
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static string GetIpAdress()
        {
            string strIp = GetRealIP();
            string url = string.Empty;
            string data = "format=json&ip=" + strIp;
            string result = string.Empty;
            try
            {
                url = "http://int.dpool.sina.com.cn/iplookup/iplookup.php";
                result = HttpGet(url, data);

                string strCountry = "";
                string strProvince = "";
                string strCity = "";
                if (result != "-2")
                {
                    if (result.Split(',').Length > 1)
                    {
                        strCountry = result.Split(',')[3].Split(':')[1];
                        strCountry = strCountry.Substring(1, strCountry.Length - 2).Replace(@"\\", @"\");
                        strProvince = result.Split(',')[4].Split(':')[1];
                        strProvince = strProvince.Substring(1, strProvince.Length - 2).Replace(@"\\", @"\");
                        strCity = result.Split(',')[5].Split(':')[1];
                        strCity = strCity.Substring(1, strCity.Length - 2).Replace(@"\\", @"\");
                    }
                }
                return strIp + "(" + ToGB2312(strCountry) + "-" + ToGB2312(strProvince) + "-" + ToGB2312(strCity) + ")";
            }
            catch
            {
                url = string.Empty;
                result = string.Empty;
            }
            return string.Empty;
        }
        #endregion

        #region 获取IP地址
        /// <summary>
        /// 获取IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetRealIP()
        {
            string ip;
            try
            {
                HttpRequest request = HttpContext.Current.Request;

                if (request.ServerVariables["HTTP_VIA"] != null)
                {
                    ip = request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString().Split(',')[0].Trim();
                }
                else
                {
                    ip = request.UserHostAddress;
                }
            }
            catch
            {
                return "";
            }

            return ip;
        }
        #endregion

        #region POST请求与获取结果
        /// <summary>
        /// POST请求与获取结果
        /// </summary>
        /// <param name="Url">URL</param>
        /// <param name="postDataStr">提交数据字符串</param>
        /// <param name="headers">请求头</param>
        /// <returns></returns>
        public static string HttpPost(string Url, string postDataStr, Dictionary<string, string> headers)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postDataStr.Length;

            if (headers != null)
                foreach (var item in headers)
                    request.Headers.Add(item.Key, item.Value);

            StreamWriter writer = new StreamWriter(request.GetRequestStream(), Encoding.ASCII);
            writer.Write(postDataStr);
            writer.Flush();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string encoding = response.ContentEncoding;
            if (encoding == null || encoding.Length < 1)
            {
                encoding = "UTF-8"; //默认编码   
            }
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
            string retString = reader.ReadToEnd();
            return retString;
        }
        #endregion

        #region GET请求与获取结果
        /// <summary>
        /// GET请求与获取结果
        /// </summary>
        /// <param name="Url">URL</param>
        /// <param name="postDataStr">提交数据字符串</param>
        /// <returns></returns>
        public static string HttpGet(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }
        #endregion

        #region 将Unicode编码转换为汉字字符串
        /// <summary>
        /// 将Unicode编码转换为汉字字符串
        /// </summary>
        /// <param name="str">Unicode编码字符串</param>
        /// <returns>汉字字符串</returns>
        public static string ToGB2312(string str)
        {
            string r = "";
            MatchCollection mc = Regex.Matches(str, @"\\u([\w]{2})([\w]{2})", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            byte[] bts = new byte[2];
            foreach (Match m in mc)
            {
                bts[0] = (byte)int.Parse(m.Groups[2].Value, NumberStyles.HexNumber);
                bts[1] = (byte)int.Parse(m.Groups[1].Value, NumberStyles.HexNumber);
                r += Encoding.Unicode.GetString(bts);
            }
            return r;
        }
        #endregion

        #region 验证当前运营系统登录密码        
        /// <summary>
        /// 验证当前运营系统登录密码 .
        /// </summary>
        /// <param name="userId">用户编号.</param>
        /// <param name="loginPwd">当前登录密码.</param>
        /// <returns><c>true</c> 相等, <c>false</c>不等.</returns>
        /// <remarks>add by dingyong,2016-07-14 19:42:53 </remarks>
        public static bool LoginPwd(Guid userId, string loginPwd)
        {
            using (WZKEntities db = new WZKEntities())
            {
                var equals = false;
                var user = db.UserAdmin.Find(userId);
                if (user != null)
                {
                    string key1 = userId.ToString().Split('-')[2];
                    string key2 = userId.ToString().Split('-')[3];
                    loginPwd = Encrypt.EncryptDES(loginPwd, key1 + key2);
                    if (loginPwd.Equals(user.Password))
                    {
                        equals = true;
                    }
                }
                return equals;
            }
        }
        #endregion

        #region 任务推送

        /// <summary>
        /// 对多台手机进行推送
        /// </summary>
        /// <param name="taskId">任务编号</param>
        /// <param name="Ids">任务明细编号信息</param>
        /// <param name="msgType">消息类型 1-新增任务，2-删除任务,3-修改任务，4-修改模板</param>
        public static void PushMessageToList(Guid? taskId,List<Guid> Ids, int msgType = 1)
        {
            using (WZKEntities context = new WZKEntities())
            {
                //推送任务
                var sql = from td in context.TaskDetail
                           join t in context.Task
                           on td.TaskId equals t.Id
                           join m in context.Mobile
                           on td.MobileId equals m.Id
                           select new
                           {
                               TaskDetailId=td.Id,
                               TaskId = td.TaskId,
                               TaskTemplateId = t.TaskTemplateId,
                               TaskType = t.TaskType,
                               ExecuteCycle = t.ExecuteCycle,
                               ExecuteTimes = t.ExecuteTimes,
                               StartTime = t.StartTime,
                               AndroidClientId = m.AndroidClientId
                           };
                if (taskId != null && Ids.Count == 0)
                     sql= sql.Where(t =>t.TaskId==taskId);
                else
                    sql = sql.Where(t => Ids.Contains(t.TaskDetailId));
                List<string> clientIdList = new List<string>();
                var result = sql.ToList();
                result.ForEach(d =>
                {
                    clientIdList.Add(d.AndroidClientId);
                });
                if (clientIdList != null && clientIdList.Count > 0)
                {
                    NoticeMessage notice = new NoticeMessage();
                    notice.MessageType = msgType;
                    notice.Content = EnumTool.GetDesc((ETaskType)result[0].TaskType);
                    notice.TaskId = result[0].TaskId;
                    notice.TaskType = result[0].TaskType;
                    notice.Cycle = result[0].ExecuteCycle;
                    notice.Times = result[0].ExecuteTimes;
                    notice.StartTime = result[0].StartTime;
                    notice.TemplateId = result[0].TaskTemplateId;
                    TuiSong.PushMessageToList(clientIdList, notice);
                }
            }
        }

        /// <summary>
        /// 对单个手机推送
        /// </summary>
        /// <param name="Id">任务明细编号</param>
        /// <param name="msgType">消息类型 1-新增任务，2-删除任务,3-修改任务，4-修改模板</param>
        public static void PushMessageToSingle(Guid Id,int msgType=1)
        {
            using (WZKEntities context = new WZKEntities())
            {
                var sql = (from td in context.TaskDetail
                           join t in context.Task
                           on td.TaskId equals t.Id
                           join m in context.Mobile
                           on td.MobileId equals m.Id
                           where td.Id == Id
                           select new
                           {
                               TaskId = td.TaskId,
                               TaskTemplateId = t.TaskTemplateId,
                               TaskType = t.TaskType,
                               ExecuteCycle = t.ExecuteCycle,
                               ExecuteTimes = t.ExecuteTimes,
                               StartTime = t.StartTime,
                               AndroidClientId = m.AndroidClientId
                           }).FirstOrDefault();
                if (sql != null && !string.IsNullOrWhiteSpace(sql.AndroidClientId))
                {
                    NoticeMessage notice = new NoticeMessage();
                    notice.MessageType = msgType;
                    notice.Content = EnumTool.GetDesc((ETaskType)sql.TaskType);
                    notice.TaskType = sql.TaskType;
                    notice.TaskId = sql.TaskId;
                    notice.Cycle = sql.ExecuteCycle;
                    notice.Times = sql.ExecuteTimes;
                    notice.StartTime = sql.StartTime;
                    notice.TemplateId = sql.TaskTemplateId;
                    TuiSong.PushMessageToSingle(sql.AndroidClientId, notice);
                }
            }
        }

        /// <summary>
        /// 对单个手机推送
        /// </summary>
        /// <param name="coordinate">坐标</param>
        /// <param name="connectionId">会话Id</param>
        /// <param name="pushId">推送标识clientId</param>
        /// <param name="controlId">实时控制编号</param>
        /// <param name="flag">是否在其他地方控制，连接断开</param>
        /// <param name="msgType">消息类型 1-新增任务，2-删除任务,3-修改任务，4-修改模板</param>
        /// <param name="operationType">操作类型 1-坐标，2-菜单，3,-home键，4-返回，后退，5-滚屏</param>
        /// <returns></returns>
        public static Tuple<bool,int> PushMsgToSingle(string coordinate, string connectionId, string pushId, Guid controlId,Guid flag, int msgType = 1,int operationType=1)
        {
            //bool-是否推送成功，int-0:正常，1：设备在其他地方被控制，2：数据初始化失败
            Tuple<bool, int> info;
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    var model = context.RealtimeControl.Find(controlId);
                    if (model != null)
                    {
                        if (model.Flag==flag)
                        {
                            info = new Tuple<bool, int>(true, 0);
                            NoticeMsg notice = new NoticeMsg();
                            notice.MessageType = msgType;
                            notice.Content = "实时操作，截屏上传";
                            notice.TaskType = 5;
                            notice.Coordinate = coordinate;
                            notice.ConnectionId = connectionId;
                            notice.ControlId = controlId;
                            notice.OperationType = operationType;
                            TuiSong.PushMsgToSingle(pushId, notice);
                        }
                        else
                        {
                            info = new Tuple<bool, int>(false, 1);
                        }
                    }
                    else
                    {
                        info = new Tuple<bool, int>(false, 2);
                    }
                }
            }
            catch
            {
                info = new Tuple<bool, int>(false, 0);
            }
            return info;
        }
        #endregion

    }
}
