using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;

namespace MvcValidation.Extension
{
    /// <summary>
    /// 验证两个属性值不能相等
    /// </summary>
    public class NotEqualToAttribute : ValidationAttribute, IClientValidatable
    {
        public string OtherProperty { get; set; }

        public NotEqualToAttribute(string otherProperty)
        {
            OtherProperty = otherProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //从验证上下文中可以获取我们想要的的属性
            var property = validationContext.ObjectType.GetProperty(OtherProperty);
            if (property == null)
            {
                return new ValidationResult(string.Format(CultureInfo.CurrentCulture, "{0} 不存在", OtherProperty));
            }

            //获取属性的值
            var otherValue = property.GetValue(validationContext.ObjectInstance, null);
            if (object.Equals(value, otherValue))
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }
            return null;
        }

        public System.Collections.Generic.IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ValidationType = "notequalto",
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName())
            };
            rule.ValidationParameters["other"] = OtherProperty;
            yield return rule;
        }
    }
}