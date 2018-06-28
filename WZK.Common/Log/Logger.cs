using System;
using System.IO;
using System.Web;
using log4net;
using log4net.Config;
using log4net.Appender;
using log4net.Layout;

namespace WZK.Common
{
    /// <summary>
    /// 日志操作管理类
    /// </summary>
    public class Logger
    { 
        private readonly ILog log;

        #region 构造函数

        public Logger()
            : this("Default")
        {

        }

        public Logger(string name, bool isProfile = false)
        {
            string basePath = HttpContext.Current != null
                ? AppDomain.CurrentDomain.SetupInformation.PrivateBinPath
                : AppDomain.CurrentDomain.BaseDirectory;

            string configFile = Path.Combine(basePath, "Config", "log4net.config");
            if (!File.Exists(configFile))
            {

                if (!isProfile)
                {
                    //默认设置
                    RollingFileAppender appender = new RollingFileAppender
                    {
                        Name = "Default",
                        File = "log\\log.log",
                        AppendToFile = true,
                        RollingStyle = RollingFileAppender.RollingMode.Date,
                        DatePattern = "yyyyMMdd\".log\"",
                        MaxSizeRollBackups = 10
                    };

                    PatternLayout layout = new PatternLayout("时间:%d{yyyy-MM-dd HH:mm:ss.fff} %n级别:%M %n信息:%m%n");
                    appender.Layout = layout;
                    BasicConfigurator.Configure(appender);
                    appender.ActivateOptions();
                }
                else
                {
                    throw new Exception("请检查日志配置文件是否存在");
                }
            }
            else
            {
                XmlConfigurator.ConfigureAndWatch(new FileInfo(configFile));
            }
            log = LogManager.GetLogger(name);
        }

        #endregion

        #region 公共方法

        /// <summary>
        ///     输出<see cref="LogLevel.Debug" />级别的日志
        /// </summary>
        public void Debug(object message, Exception exception = null)
        {
            if (exception == null)
            {
                log.Debug(message);
            }
            else
            {
                log.Debug(message, exception);
            }
        }

        /// <summary>
        ///     输出<see cref="LogLevel.Debug" />级别的日志
        /// </summary>
        public void DebugFormat(string format, params object[] args)
        {
            log.DebugFormat(format, args);
        }

        /// <summary>
        ///     输出<see cref="LogLevel.Info" />级别的日志
        /// </summary>
        public void Info(object message, Exception exception = null)
        {
            if (exception == null)
            {
                log.Info(message);
            }
            else
            {
                log.Info(message, exception);
            }
        }

        /// <summary>
        ///     输出<see cref="LogLevel.Info" />级别的日志
        /// </summary>
        public void InfoFormat(string format, params object[] args)
        {
            log.InfoFormat(format, args);
        }

        /// <summary>
        ///     输出<see cref="LogLevel.Warn" />级别的日志
        /// </summary>
        public void Warn(object message, Exception exception = null)
        {
            if (exception == null)
            {
                log.Warn(message);
            }
            else
            {
                log.Warn(message, exception);
            }
        }

        /// <summary>
        ///     输出<see cref="LogLevel.Warn" />级别的日志
        /// </summary>
        public void WarnFormat(string format, params object[] args)
        {
            log.WarnFormat(format, args);
        }

        /// <summary>
        ///     输出<see cref="LogLevel.Error" />级别的日志
        /// </summary>
        public void Error(object message, Exception exception = null)
        {
            if (exception == null)
            {
                log.Error(message);
            }
            else
            {
                log.Error(message, exception);
            }
        }

        /// <summary>
        ///     输出<see cref="LogLevel.Error" />级别的日志
        /// </summary>
        public void ErrorFormat(string format, params object[] args)
        {
            log.ErrorFormat(format, args);
        }

        /// <summary>
        ///     输出<see cref="LogLevel.Fatal" />级别的日志
        /// </summary>
        public void Fatal(object message, Exception exception = null)
        {
            if (exception == null)
            {
                log.Fatal(message);
            }
            else
            {
                log.Fatal(message, exception);
            }
        }

        /// <summary>
        ///     输出<see cref="LogLevel.Fatal" />级别的日志
        /// </summary>
        public void FatalFormat(string format, params object[] args)
        {
            log.FatalFormat(format, args);
        }
         

        /// <summary>
        /// 获取错误详细信息
        /// </summary>
        /// <param name="ex">异常</param>
        /// <returns>异常信息</returns>
        /// <remarks>add by ****,2015-07-28 13:53:07 </remarks>
        public static string GetErrMsg(Exception ex)
        {
            string errMessage = "";
            for (Exception tempException = ex; tempException != null; tempException = tempException.InnerException)
            {
                errMessage += tempException.Message + Environment.NewLine + Environment.NewLine;
            }
            errMessage += ex.ToString();
            return errMessage;
        }
        #endregion
    }
}