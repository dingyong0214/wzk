using System;
using System.ComponentModel.DataAnnotations;

namespace WZK.Admin.Models.Right
{
    /// <summary>
    /// 角色信息维护
    /// author:lonre
    /// date:2016-01-14
    /// </summary>
    public class ModifyRoleInfo
    {
        /// <summary>
        ///角色编号
        ///</summary>
        public int RoleID { get; set; }
        /// <summary>
    	///企业编号
    	///</summary>
        public System.Guid CompanyId { get; set; }
        /// <summary>
    	///企业名称
    	///</summary>
        public string CompanyName { get; set; }
        /// <summary>
    	///角色名
    	///</summary>
        [Required(ErrorMessage = "角色名不能为空")]
        [StringLength(10, ErrorMessage = "角色名长度不能超过10")]
        public string RoleName { get; set; }
        /// <summary>
    	///录入时间
    	///</summary>
        public System.DateTime InsertTime { get; set; } = DateTime.Now;
    }
}