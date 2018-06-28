using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WZK.Admin.Common
{
    /// <summary>
    /// 手机号码验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class MobileAttribute : CustomRegularExpressionAttribute
    {
        public MobileAttribute()
            : base("^(?:13\\d|14\\d|15\\d|17\\d|18\\d)\\d{5}(\\d{3}|\\*{3})$")
        {
            this.ErrorMessage = "手机号码格式错误";
        }
        /// <summary>
        /// 验证是否存在.
        /// </summary>
        /// <value><c>true</c> if exits; otherwise, <c>false</c>.</value>
        public bool Exits { get; set; }

        /// <summary>
        /// 确定指定的值是否与有效的手机号码相匹配。
        /// </summary>
        /// <param name="value">要验证的值。</param>
        /// <returns>如果指定的值有效或 null，则为 true；否则，为 false。</returns>
        /// <remarks>add by dingyong,2015-05-20 19:49:14 </remarks>
        public override bool IsValid(object value)
        {
            if (value == null) return true;
            string postValue = (value as string).Trim();
            if (string.IsNullOrEmpty(postValue))
            {
                ErrorMessage = "手机号码不能为空";
                return false;
            }
            else if (!base.IsValid(postValue))
            {
                ErrorMessage = this.ErrorMessage == "true" ? "3000206" : "手机号码格式错误";
                return false;
            }
            return true;
        }

    }
}