using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace WZK.Api.IAttribute
{
    public class ModelAttribute
    {
        /// <summary>
        /// 手机号码验证
        /// </summary>
        [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
        public class MobileAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (value != null)
                {
                    var valueAsString = value.ToString();
                    const string regPattern = @"^(13[0-9]|15[0-9]|17[0-9]|18[0-9]|14[0-9])[0-9]{8}$";
                    if (!Regex.IsMatch(valueAsString, regPattern))
                    {
                        return new ValidationResult("输入的手机号码错误！");
                    }
                    else
                    {
                        return ValidationResult.Success;
                    }
                }
                return new ValidationResult("输入的手机号码错误！");
            }
        }

        /// <summary>
        /// 邮箱验证
        /// </summary>
        [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
        public class MailAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (value != null)
                {
                    var valueAsString = value.ToString();
                    const string regPattern = @"^(\w)+(\.\w+)*@(\w)+((\.\w+)+)$";
                    if (!Regex.IsMatch(valueAsString, regPattern))
                    {
                        return new ValidationResult("输入的邮箱有误！");
                    }
                    else
                    {
                        return ValidationResult.Success;
                    }
                }
                return new ValidationResult("输入的邮箱有误！");
            }
        }

        /// <summary>
        /// 密码检验
        /// </summary>
        public class PasswordAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                string regPattern = @"(?=.*\d)(?=.*[a-zA-Z]).{6,30}";

                return Regex.IsMatch(value.ToString(), regPattern) ? ValidationResult.Success :
                    new ValidationResult("密码必须6到30位包含字母数字组合");
            }
        }
    }

}