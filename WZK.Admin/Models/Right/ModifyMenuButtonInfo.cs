using System;
using System.ComponentModel.DataAnnotations;

namespace WZK.Admin.Models.Right
{
    /// <summary>
    /// 菜单功能信息维护
    /// author:lonre
    /// date:2016-01-15
    /// </summary>
    public class ModifyMenuButtonInfo
    {
        /// <summary>
    	///编号
    	///</summary>
        public int Id { get; set; }
        /// <summary>
    	///功能名
    	///</summary>
        [Required(ErrorMessage = "功能名不能为空")]
        [StringLength(20, ErrorMessage = "功能名长度不能超过20")]
        public string Name { get; set; }
        /// <summary>
    	///菜单编号
    	///</summary>
        [Required(ErrorMessage = "菜单编号不能为空")]
        public int MenuId { get; set; }
        /// <summary>
    	///控制器
    	///</summary>
        [Required(ErrorMessage = "控制器不能为空")]
        [StringLength(128, ErrorMessage = "控制器长度不能超过128")]
        [RegularExpression("^[a-zA-Z_,]{1,128}$", ErrorMessage = "控制器输入有误")]
        public string Controller { get; set; }
        /// <summary>
    	///Action
    	///</summary>
        [Required(ErrorMessage = "Action不能为空")]
        [StringLength(128, ErrorMessage = "Action长度不能超过128")]
        [RegularExpression("^[a-zA-Z_,]{1,128}$", ErrorMessage = "Action输入有误")]
        public string Action { get; set; }
        /// <summary>
    	///排序号
    	///</summary>
        [StringLength(15, ErrorMessage = "排序号长度不能超过15")]
        [RegularExpression("^\\d{1,15}$", ErrorMessage = "排序号长度不能超过15位")]
        public string OrderNo { get; set; } = "010";

        /// <summary>
        ///排序号
        ///</summary> 
        public string AllOrderNo { get; set; }
        /// <summary>
    	///按钮类型：1-基本按钮 2-一般按钮
    	///</summary>
        public int ButtonType { get; set; } = 2;
        /// <summary>
    	///录入时间
    	///</summary>
        public System.DateTime InsertTime { get; set; } = DateTime.Now;
    }
}