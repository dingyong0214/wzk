using System;

namespace WZK.Common
{
    /// <summary>
    /// 字符格式化
    /// </summary>
    public static class ZnFormat
    {
        /// <summary>
        /// 格式化为短名字
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="length">The length.</param>
        /// <returns>System.String.</returns>
        public static string ToShortName(this string userName, int length = 10)
        {
            userName = userName ?? string.Empty;
            if (userName.Length < length)
            {
                return userName;
            }
            return userName.Substring(0, length) + "**";
        }

        public static string FormatLoginName(this string loginName)
        {
            int length = loginName.Length;
            return loginName.Substring(0, 1) + "***" + loginName.Substring(length - 1, 1);
        }

        /// <summary>
        /// 格式化帐户显示
        /// </summary>
        /// <param name="card"></param>
        /// <param name="userName"></param>
        /// <param name="openBankName"></param>
        /// <returns></returns>
        public static string BankCard(string card, string userName, string openBankName)
        {
            return string.Format("{0} {1} {2}", userName, openBankName, " **** **** **** " + card.Substring(card.Length - 4));
        }

        /// <summary>
        /// 金额字符串转换decimal?
        /// </summary>
        /// <param name="_money">金额</param>
        /// <returns>例:700000.00或null</returns>
        public static decimal? Money(this string _money)
        {
            if (_money == "")
                return null;
            _money = _money.Replace(",", "");
            return Convert.ToDecimal(_money);
        }

        /// <summary>
        /// 获取路径
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static string GetSrc(string src, string version)
        {
            return string.Format("{0}{1}v={2}", src, src.IndexOf("?") == -1 ? "?" : "&", version);
        }

        #region 获取流水号
        /// <summary>
        /// 获取流水号
        /// </summary>
        /// <returns></returns>
        public static string GetSeqId()
        {
            return DateTime.Now.ToString("yyyyMMddhhmmfffff");
        }
        #endregion

        #region 格式化手机号
        /// <summary>
        /// 格式化手机号
        /// </summary>
        /// <param name="mobile">手机号</param>
        /// <returns>System.String.</returns>
        public static string FormatMobile(this string mobile)
        {
            if (!string.IsNullOrEmpty(mobile) && mobile.Length >= 11)
            {
                return mobile.Substring(0, 3) + "****" + mobile.Substring(7, 4);
            }
            return mobile;
        }

        #region 格式化身份证
        /// <summary>
        /// Formats the mobile.
        /// </summary>
        /// <param name="mobile">The mobile.</param>
        /// <returns>System.String.</returns>
        /// <remarks>add by dingyong,2015-06-25 11:10:33 </remarks>
        public static string FormatIdCard(this string card)
        {

            if (!string.IsNullOrEmpty(card))
            {
                if (card.Length >= 18)
                {
                    return card.Substring(0, 4) + "******* *****" + card.Substring(14, 4);
                }
                else if (card.Length >= 15)
                {
                    return card.Substring(0, 4) + "*******" + card.Substring(11, 4);
                }
            }
            return card;
        }
        #endregion

        #endregion

        #region 取用户Guid中间八位
        /// <summary>
        /// 取用户Guid中间八位.
        /// </summary>
        /// <param name="userId">用户Id.</param>
        /// <returns>System.String.</returns>
        public static string FormatUserId(this Guid userId)
        {
            string formatUser = userId.ToString("N");
            return formatUser.Substring(12, 8);
        }
        #endregion


        #region 格式化开始时间和结束时间
        /// <summary>
        /// 格式化开始时间和结束时间
        /// </summary>
        /// <param name="startTime">开始时间.</param>
        /// <param name="endTime">结束时间.</param>
        /// <remarks>add by dingyong,2016-09-29 15:36:23 </remarks>
        public static void Format(ref DateTime? startTime, ref DateTime? endTime)
        {
            if (startTime.HasValue &&endTime.HasValue)
            {
                startTime =DateTime.Parse(startTime.Value.ToShortDateString());
                endTime = endTime.Value.AddDays(1).AddMilliseconds(-1);
            } 
        }
        #endregion
    }
}
