using WZK.Admin.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace WZK.Admin.Models.Right
{
    /// <summary>
    /// 用户信息维护
    /// author:lonre
    /// date:2016-01-14
    /// </summary>
    public class ModifyUserInfo
    {
        /// <summary>
        ///用户编号
        ///</summary>
        public Guid Id { get; set; }
        /// <summary>
    	///用户名
    	///</summary>
        [Required(ErrorMessage = "用户名不能为空")]
        [StringLength(20, ErrorMessage = "用户名长度不能超过20个字符")]
        public string UserName { get; set; }

        /// <summary>
        ///密码
        ///</summary>
        [Required(ErrorMessage = "密码不能为空")]
        [StringLength(64, ErrorMessage = "密码长度不能超过64个字符")]
        public string Password { get; set; }

        /// <summary>
    	///头像
    	///</summary>
        public string Photo { get; set; }
        /// <summary>
    	///姓名
    	///</summary>
        [Required(ErrorMessage = "姓名不能为空")]
        [StringLength(5, ErrorMessage = "姓名长度不能超过5个字符")]
        public string Name { get; set; }
        /// <summary>
    	///身份证号码
    	///</summary>
        [RegularExpression(@"^(\d{15}$|^\d{18}$|^\d{17}(\d|X|x))$", ErrorMessage = "身份证号码不正确")]
        public string IdCard { get; set; }
        /// <summary>
    	///部门
    	///</summary>
        [Required(ErrorMessage = "请选择部门")]
        [StringLength(10, ErrorMessage = "部门长度不能超过10个字符")]
        public string Department { get; set; }
        /// <summary>
    	///手机
    	///</summary>
        [Mobile(ErrorMessage = "手机号输入错误")]
        public string Mobile { get; set; }
        /// <summary>
    	///联系电话
    	///</summary>
        [RegularExpression(@"^(1[3|4|5|8|7][0-9]\d{8})|((0\d{2}-?\d{8})|(0\d{3}-?\d{7})|(0\d2-?\d{8})|(0\d3-?\d{7}))$", ErrorMessage = "联系电话输入错误")]
        public string Telephone { get; set; }
        /// <summary>
    	///邮箱
    	///</summary>
        [EmailAddress(ErrorMessage = "邮箱输入有误")]
        [StringLength(50, ErrorMessage = "邮箱长度不能超过50个字符")]
        public string Mail { get; set; }
        /// <summary>
    	///注册时间
    	///</summary>
        public DateTime RegisterTime { get; set; } = DateTime.Now;
        /// <summary>
    	///用户状态：1-有效 2-禁用
    	///</summary>
        public int State { get; set; }
    }
}