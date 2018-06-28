using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WZK.Admin
{
    /// <summary>
    /// banner维护
    /// </summary>
    public class ModifyAppBannerInfo
    {
        /// <summary>
        /// banner编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// banner名称
        /// </summary>
        [Required(ErrorMessage = "请输入名称")]
        [StringLength(64, ErrorMessage = "长度不能超过64")]
        public string Name { get; set; }
        /// <summary>
        /// 图片url
        /// </summary>
        [Required(ErrorMessage = "请上传图片")]
        public string BannerImageUrl { get; set; }
        /// <summary>
        /// banner链接地址
        /// </summary>
        public string BannerPageUrl { get; set; }
        /// <summary>
        /// 是否在理财师显示
        /// </summary>
        public bool IsShowPlanner { get; set; }
        /// <summary>
        /// 是否在投资人显示
        /// </summary>
        public bool IsShowUser { get; set; }
        /// <summary>
        /// 类别：1活动培训2资讯3节日祝福
        /// </summary>
        public int BannerKind { get; set; }
        /// <summary>
        /// 业务编号
        /// </summary>
        [Required(ErrorMessage = "请选择")]
        public Guid? BusinessId { get; set; }
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNo { get; set; }

    }
}
