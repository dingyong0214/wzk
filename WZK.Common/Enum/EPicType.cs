using System.ComponentModel;

namespace CA.Common
{
    public enum EPicType
    {
        /// <summary>
        /// 用户头像
        /// </summary>
        [Description("用户头像")]
        Avatar = 1,
        /// <summary>
        /// 身份认证图片
        /// </summary>
        [Description("身份认证图片")]
        Cert = 2,
        /// <summary>
        /// 商品图片
        /// </summary>
        [Description("商品图片")]
        Product = 3,
        /// <summary>
        /// 分类图片
        /// </summary>
        [Description("分类图片")]
        Category = 4,
        /// <summary>
        /// 用户图片
        /// </summary>
        [Description("用户图片")]
        User = 5
    }
}
