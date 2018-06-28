using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WZK.Business.Admin
{
    public class Mmenus
    {
        /// <summary>
    	///编号
    	///</summary>
        public int Id { get; set; }
        /// <summary>
    	///父级菜单
    	///</summary>
        public Nullable<int> ParentId { get; set; }
        /// <summary>
    	///菜单名字
    	///</summary>
        public string Name { get; set; }
        /// <summary>
    	///菜单英文名
    	///</summary>
        public string EnglishName { get; set; }
        /// <summary>
    	///菜单Url
    	///</summary>
        public string Url { get; set; }
        /// <summary>
    	///排序号
    	///</summary>
        public string OrderNo { get; set; }
        /// <summary>
    	///录入人
    	///</summary>
        public string InputUser { get; set; }
        /// <summary>
    	///入库时间
    	///</summary>
        public System.DateTime InsertTime { get; set; }
        /// <summary>
    	///修改人
    	///</summary>
        public Nullable<System.Guid> UpdateUser { get; set; }
        /// <summary>
    	///更新时间
    	///</summary>
        public Nullable<System.DateTime> UpdateTime { get; set; }
    }
}
