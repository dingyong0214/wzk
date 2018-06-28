using System;

namespace WZK.Business.Admin.Models
{
    /// <summary>
    /// 菜单功能
    /// author:lorne
    /// date:2016-01-15
    /// </summary>
    public class MMenuButton
    {
        /// <summary>
    	///编号
    	///</summary>
        public int Id { get; set; }
        /// <summary>
    	///功能名字
    	///</summary>
        public string Name { get; set; }
        /// <summary>
    	///菜单编号
    	///</summary>
        public int MenuId { get; set; }
        /// <summary>
    	///菜单名称
    	///</summary>
        public string MenuName { get; set; }
        /// <summary>
    	///控制器
    	///</summary>
        public string Controller { get; set; }
        /// <summary>
    	///Action
    	///</summary>
        public string Action { get; set; }
        /// <summary>
    	///排序号
    	///</summary>
        public string OrderNo { get; set; }
        /// <summary>
    	///按钮类型：1-基本按钮 2-一般按钮
    	///</summary>
        public Nullable<int> ButtonType { get; set; }
        /// <summary>
    	///入库时间
    	///</summary>
        public System.DateTime InsertTime { get; set; }
        /// <summary>
    	///录入人
    	///</summary>
        public string InputUser { get; set; }
    }
}
