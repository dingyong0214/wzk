using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WZK.Admin
{
    /// <summary>
    /// 评测管理
    /// </summary>
    /// <remarks>author:wcy date:2016-03-08</remarks>
    public class ModifyRiskModelInfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 问题
        /// </summary>
        [Required(ErrorMessage = "问题不能为空")]
        [StringLength(64, ErrorMessage = "长度不能超过64")]
        public string Question { get; set; }
        /// <summary>
    	///问题序号
    	///</summary>
        [RegularExpression(@"^\d+$", ErrorMessage = "问题序号只能是数字")]
        public int Xuhao { get; set; }
        /// <summary>
    	///模型类别
    	///</summary>
        public int Kind { get; set; }
        /// <summary>
    	///录入人
    	///</summary>
        public Nullable<System.Guid> InputUser { get; set; }
        /// <summary>
    	///录入时间
    	///</summary>
        public Nullable<System.DateTime> InsertTime { get; set; }
        /// <summary>
    	///修改人
    	///</summary>
        public Nullable<System.Guid> UpdateUser { get; set; }
        /// <summary>
    	///修改时间
    	///</summary>
        public Nullable<System.DateTime> UpdateTime { get; set; }
    }
}
