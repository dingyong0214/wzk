using System;
using System.ComponentModel.DataAnnotations;

namespace WZK.Admin.Models.Location
{
    public class ModifyLocation
    {
        /// <summary>
        /// 编号
        /// </summary>
        public System.Guid Id { get; set; }
        /// <summary>
        /// 定位标题
        /// </summary>
        [Required(ErrorMessage = "定位标题不能为空")]
        [StringLength(25, ErrorMessage = "定位标题长度不能超过25个字符")]
        public string Title { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        [Required(ErrorMessage = "请获取定位信息")]
        public string Longitude { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        [Required(ErrorMessage = "请获取定位信息")]
        public string Latitude { get; set; }
        /// <summary>
        /// 经纬度（多个使用）
        /// </summary>
        public string LatAndLong { get; set; }
        /// <summary>
        /// 定位地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public int IsDel { get; set; } = 1;
        /// <summary>
        /// 录入人
        /// </summary>
        public System.Guid InputUser { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        public System.DateTime InsertTime { get; set; } = DateTime.Now;
    }
}