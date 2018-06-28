using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WZK.Business.Admin
{
    public class MDict
    {
        /// <summary>
    	///编号
    	///</summary>
        public System.Guid Id { get; set; }
        /// <summary>
    	///字典名称
    	///</summary>
        public string Name { get; set; }
        /// <summary>
    	///字典英文名称
    	///</summary>
        public string EnglishName { get; set; }
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
        public string UpdateUser { get; set; }
        /// <summary>
    	///修改时间
    	///</summary>
        public Nullable<System.DateTime> UpdateTime { get; set; }
    }
}
