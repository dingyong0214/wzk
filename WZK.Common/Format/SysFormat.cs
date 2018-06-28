using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WZK.Common.Format
{
    /// <summary>
    /// 格式化
    /// author:dingyong
    /// createTime:2015-04-21
    /// </summary>
    public static class SysFormat
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
        /// decimal数字格式化为金额
        /// (有千分位和2位小数)
        /// </summary>
        /// <param name="amount">当前金额.</param>
        /// <param name="withPrefix">是否带有前缀¥</param>
        /// <returns>System.String.</returns>
        /// <remarks>add by dingyong,2015-09-06 10:48:45 </remarks>
        public static string ToMoney(this decimal amount, bool withPrefix = true)
        {
            return (withPrefix) ? "¥ " + amount.ToString("N") : amount.ToString("N");
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
                return mobile.Substring(0, 3) + "*****" + mobile.Substring(8, 3);
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
                    return card.Substring(0, 6) + "***** *****" + card.Substring(14, 4);
                }
                else if (card.Length >= 15)
                {
                    return card.Substring(0, 6) + "*****" + card.Substring(11, 4);
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

        #region 四舍五入
        /// <summary>
        /// 四舍五入指定位数
        /// </summary>
        /// <param name="d">转换的数值</param>
        /// <param name="length">长度</param>
        /// <returns>带指定长度的数值</returns>
        /// <remarks>**** added,2015-07-19</remarks>
        public static decimal Rounding(this decimal d, int length = 2)
        {
            return Math.Round(d, length, MidpointRounding.AwayFromZero);
        }
        #endregion

        #region 获取排序号
        /// <summary>
        /// 获取排序号 第一级三位从010开始，第二级六位从010010开始....
        /// </summary>
        /// <param name="parentOrderNo">父级的排序号</param>
        /// <param name="preOrderNo">上一个排序号,没有传空，表示从头开始</param>
        /// <param name="depth">深度</param>
        /// <returns></returns>
        public static string GetOrderNo(string parentOrderNo, string preOrderNo)
        {

            string orderNo = "";
            if (string.IsNullOrWhiteSpace(preOrderNo))
            {
                if (string.IsNullOrWhiteSpace(parentOrderNo))
                {
                    orderNo = "010";
                }
                else
                {
                    orderNo = parentOrderNo + "010";
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(parentOrderNo))//一级
                {
                    orderNo = (Convert.ToInt64(preOrderNo) + 10).ToString().PadLeft(3, '0');
                }
                else
                {
                    preOrderNo = preOrderNo.Substring(preOrderNo.Length - 3, 3);

                    orderNo = (Convert.ToInt64(parentOrderNo + preOrderNo) + 10).ToString().PadLeft(parentOrderNo.Length + 3, '0');
                }
            }

            return orderNo;
        }
        #endregion

        #region 获取随机数
        private static Random random = new Random();
        /// <summary>
        /// 获取随机数
        /// </summary>
        /// <param name="length">随机数长度,最长只支持9位</param>
        /// <returns>随机数</returns>
        /// <remarks>add by dingyong,2016-08-17 17:12:06 </remarks>
        public static int GetRomdonNumber(int length)
        {
            if (length >= 9)
            {
                length = 9;
            }
            return random.Next(Convert.ToInt32(Math.Pow(10, length - 1)), Convert.ToInt32(Math.Pow(10, length)));
        }
        #endregion
    }
}
