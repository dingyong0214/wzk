using System.Configuration;

namespace WZK.Common.ConfigData
{
    /// <summary>
    /// 支付相关配置
    /// author:dingyong
    /// createTime:2016-08-02
    public class PayConfig
    {
        /// <summary>
        /// 招行一网通费率标志 0-百分比 1-固定金额  
        /// </summary>
        public static int OneNetFeeRateFlag
        {
            get
            {
                if (ConfigurationManager.AppSettings["OneNetFeeRateFlag"] == null)
                {
                    return 0;
                }
                return int.Parse(ConfigurationManager.AppSettings["OneNetFeeRateFlag"]);
            }
        }

        /// <summary>
        /// 招行一网通固定金额/费率(存储数据都是乘以100后值)
        /// </summary>
        public static decimal OneNetFeeRate
        {
            get
            {
                if (ConfigurationManager.AppSettings["OneNetFeeRate"] == null)
                {
                    return 0;
                }
                return decimal.Parse(ConfigurationManager.AppSettings["OneNetFeeRate"]);
            }
        }

        /// <summary>
        /// 招行一网通网站支付参数字典名称
        /// </summary>
        public static string OneNetPayDicParamKey
        {
            get
            {
                if (ConfigurationManager.AppSettings["OneNetPayDicParamKey"] == null)
                {
                    return "";
                }
                return ConfigurationManager.AppSettings["OneNetPayDicParamKey"].ToString();
            }
        }

        /// <summary>
        /// 微信费率标志 0-百分比 1-固定金额  
        /// </summary>
        public static int WeiXinFeeRateFlag
        {
            get
            {
                if (ConfigurationManager.AppSettings["WeiXinFeeRateFlag"] == null)
                {
                    return 0;
                }
                return int.Parse(ConfigurationManager.AppSettings["WeiXinFeeRateFlag"]);
            }
        }

        /// <summary>
        /// 微信固定金额/费率(存储数据都是乘以100后值)
        /// </summary>
        public static decimal WeiXinFeeRate
        {
            get
            {
                if (ConfigurationManager.AppSettings["WeiXinFeeRate"] == null)
                {
                    return 0;
                }
                return decimal.Parse(ConfigurationManager.AppSettings["WeiXinFeeRate"]);
            }
        }

        /// <summary>
        /// 支付宝费率标志 0-百分比 1-固定金额  
        /// </summary>
        public static int AliPayFeeRateFlag
        {
            get
            {
                if (ConfigurationManager.AppSettings["AliPayFeeRateFlag"] == null)
                {
                    return 0;
                }
                return int.Parse(ConfigurationManager.AppSettings["AliPayFeeRateFlag"]);
            }
        }

        /// <summary>
        /// 支付宝固定金额/费率(存储数据都是乘以100后值)
        /// </summary>
        public static decimal AliPayFeeRate
        {
            get
            {
                if (ConfigurationManager.AppSettings["AliPayFeeRate"] == null)
                {
                    return 0;
                }
                return decimal.Parse(ConfigurationManager.AppSettings["AliPayFeeRate"]);
            }
        }
    }
}
