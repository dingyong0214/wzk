using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WZK.Business.Admin
{
    public class MUserAdmin
    {
        /// <summary>
        ///用户编号
        ///</summary>
        public System.Guid Id { get; set; }
        /// <summary>
    	///用户名
    	///</summary>
        public string UserName { get; set; }
        /// <summary>
    	///姓名
    	///</summary>
        public string Name { get; set; }
        /// <summary>
    	///部门
    	///</summary>
        public string Department { get; set; }
        /// <summary>
    	///注册时间
    	///</summary>
        public DateTime RegisterTime { get; set; }
        /// <summary>
    	///用户状态：1-有效 2-禁用
    	///</summary>
        public int State { get; set; }
         
        /// <summary>
    	///用户拥有角色
    	///</summary>
        public string RoleName { get; set; }
        /// <summary>
    	///录入人
    	///</summary>
        public string InputUser { get; set; }

    }
}
