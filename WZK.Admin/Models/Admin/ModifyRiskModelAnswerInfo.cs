using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WZK.Admin
{
    /// <summary>
    /// 评测管理答案
    /// </summary>
    /// <remarks>author:wcy date:2016-03-08</remarks>
    public class ModifyRiskModelAnswerInfo
    {

        /// <summary>
        ///编号
        ///</summary>
        public System.Guid Id { get; set; }
        /// <summary>
        /// 模型类型
        /// </summary>
        public int Kind { get; set; }
        /// <summary>
    	///问题编号
    	///</summary>
        public System.Guid QuestionId { get; set; }
        /// <summary>
    	///答案序号
    	///</summary>
        [Required(ErrorMessage = "请填写序号")]
        [RegularExpression(@"^\d+$", ErrorMessage = "答案序号只能是数字")]
        public int Xuhao { get; set; }
        /// <summary>
    	///答案字母编号
    	///</summary>
        [Required(ErrorMessage = "请填写字母编号")]
        [RegularExpression(@"^[A-Z]$", ErrorMessage = "答案字母编号为英文字母ABCD等")]
        public string ZimuNo { get; set; }
        /// <summary>
    	///答案描述
    	///</summary>
        [Required(ErrorMessage = "描述不能为空")]
        [StringLength(128, ErrorMessage = "长度不能超过128")]
        public string AnswerDesc { get; set; }
        /// <summary>
    	///分值
    	///</summary>
        [Required(ErrorMessage = "请填写分值")]
        [RegularExpression(@"^\d+$", ErrorMessage = "分值只能是数字")]
        public int Score { get; set; }
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
