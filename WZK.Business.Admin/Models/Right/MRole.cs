using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WZK.Business.Admin
{
    /// <summary>
    /// 用户角色.
    /// author:dingyong
    /// date:2016-07-14
    /// </summary>
    public class MRole
    {
         /// <summary>
    	///编号
    	///</summary>
        public int RoleID { get; set; }
        /// <summary>
    	///角色名称
    	///</summary>
        public string RoleName { get; set; }
        /// <summary>
    	///录入人
    	///</summary>
        public string InputUser { get; set; }
        /// <summary>
    	///入库时间
    	///</summary>
        public System.DateTime InsertTime { get; set; }   
    }
}
