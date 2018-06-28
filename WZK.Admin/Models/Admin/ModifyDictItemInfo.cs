using System.ComponentModel.DataAnnotations;

namespace WZK.Admin
{
    /// <summary>
    /// 字典项维护
    /// </summary>
    /// <remarks>author:wcy date:2016-03-09</remarks>
    public class ModifyDictItemInfo
    {
        /// <summary>
    	///编号
    	///</summary>
        public int Id { get; set; }
        /// <summary>
    	///字典编号
    	///</summary>
        public System.Guid DictId { get; set; }
        /// <summary>
    	///字典项名称
    	///</summary>
        [Required(ErrorMessage = "请输入字典项名称")]
        [StringLength(64, ErrorMessage = "长度不能超过64")]
        public string ItemName { get; set; }
        /// <summary>
    	///字典项值
    	///</summary>
        [Required(ErrorMessage = "请输入字典项值")]
        [StringLength(64, ErrorMessage = "长度不能超过64")]
        public string ItemValue { get; set; }
        /// <summary>
    	///排序号
    	///</summary>
        [Required(ErrorMessage = "请填写排序号")]
        [StringLength(64, ErrorMessage = "长度不能超过64")]
        public string OrderNo { get; set; }
        /// <summary>
    	///备注
    	///</summary>
        [Required(ErrorMessage = "请填写备注")]
        [StringLength(64, ErrorMessage = "长度不能超过128")]
        public string Remark { get; set; }
    }
}
