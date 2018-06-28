using System;
using System.ComponentModel.DataAnnotations;

namespace WZK.Admin.Models.Right
{
    /// <summary>
    /// 菜单信息维护
    /// author:lonre
    /// date:2016-01-15
    /// </summary>
    public class ModifyMenuInfo
    {
        /// <summary>
    	///编号
    	///</summary>
        public int Id { get; set; }
        /// <summary>
    	///父级菜单
    	///</summary>
        public string ParentId { get; set; } = "0";
        /// <summary>
    	///菜单名字
    	///</summary>
        [Required(ErrorMessage = "菜单名不能为空")]
        [StringLength(20, ErrorMessage = "菜单名长度不能超过20")]
        public string Name { get; set; }
        /// <summary>
    	///菜单英文名
    	///</summary>
        [Required(ErrorMessage = "菜单英文名不能为空")]
        [StringLength(20, ErrorMessage = "菜单英文名长度不能超过20")]
        [RegularExpression("^[a-zA-Z_0-9]{1,20}.*?$", ErrorMessage = "菜单英文名输入有误")]
        public string EnglishName { get; set; }
        /// <summary>
    	///菜单Url
    	///</summary>
        [StringLength(100, ErrorMessage = "菜单Url长度不能超过100")]
        [RegularExpression("^[a-zA-Z_:./]{2,100}$", ErrorMessage = "菜单Url输入有误")]
        public string Url { get; set; }
        /// <summary>
    	///排序号
    	///</summary>
        [RegularExpression("^\\d{1,3}$", ErrorMessage = "排序号输入有误")]
        [MaxLength(3,ErrorMessage ="排序号只限三位")]
        public string OrderNo { get; set; } = "0";
        /// <summary>
        ///排序号
        ///</summary> 
        public string AllOrderNo { get; set; }
        /// <summary>
    	///录入时间
    	///</summary>
        public System.DateTime InsertTime { get; set; } = DateTime.Now;

        /// <summary>
        ///菜单类型：0-主菜单 1-子菜单
        ///</summary>
        public int MenuType { get; set; }
    }
}