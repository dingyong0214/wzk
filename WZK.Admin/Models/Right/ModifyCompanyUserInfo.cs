using WZK.Admin.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace WZK.Admin.Models.Right
{
    /// <summary>
    /// 企业操作员信息维护
    /// author:lonre
    /// date:2016-01-21
    /// </summary>
    public class ModifyCompanyUserInfo
    {
        /// <summary>
    	///编号
    	///</summary>
        public System.Guid Id { get; set; }
        /// <summary>
    	///企业编号
    	///</summary>
        public System.Guid CompanyId { get; set; }
        /// <summary>
    	///企业名称
    	///</summary>
        public string CompanyName { get; set; }
        /// <summary>
    	///姓名
    	///</summary>
        [StringLength(10, ErrorMessage = "姓名长度不能超过10")]
        public string Name { get; set; }
        /// <summary>
    	///手机号码
    	///</summary>
        [Required(ErrorMessage = "手机号码")]
        [Mobile(ErrorMessage = "手机号码输入错误")]
        public string Mobile { get; set; }
        /// <summary>
    	///状态：1-有效 2-禁用
    	///</summary>
        public int State { get; set; }
        /// <summary>
    	///备注
    	///</summary>
        [StringLength(100, ErrorMessage = "备注长度不能超过100")]
        public string Remark { get; set; }
    }
}