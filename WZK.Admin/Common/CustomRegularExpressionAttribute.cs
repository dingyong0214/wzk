using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WZK.Admin.Common
{
    /// <summary>
    /// Class CluboRegularExpressionAttribute.
    /// </summary>
    public class CustomRegularExpressionAttribute : RegularExpressionAttribute, IClientValidatable
    {
        public CustomRegularExpressionAttribute(string pattern)
            : base(pattern)
        {
        }

        /// <summary>
        /// 在类中实现时，返回该类的客户端验证规则。
        /// </summary>
        /// <param name="metadata">模型元数据。</param>
        /// <param name="context">控制器上下文。</param>
        /// <returns>此验证程序的客户端验证规则。</returns>
        /// <remarks>add by dingyong,2015-09-14 10:48:25 </remarks>
        public System.Collections.Generic.IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            ModelClientValidationRule validationRule = new ModelClientValidationRule
            {
                ValidationType = "regex",
                ErrorMessage = this.ErrorMessage
            };
            validationRule.ValidationParameters.Add("pattern", this.Pattern);
            yield return validationRule;
        }
    }
}
