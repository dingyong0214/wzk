using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WZK.Admin
{
    /// <summary>
    /// 字典信息维护
    /// </summary>
    public class ModifyDictInfo
    {
        /// <summary>
        /// 字典编号
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 字典名称
        /// </summary>
        [Required(ErrorMessage = "字典名称不能为空")]
        [StringLength(20, ErrorMessage = "长度不能超过20")]
        public string Name { get; set; }
        /// <summary>
        /// 英文名称
        /// </summary>
        [Required(ErrorMessage = "字典英文名称不能为空")]
        [StringLength(50, ErrorMessage = "长度不能超过50")]
        public string EnglishName { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        [Required(ErrorMessage = "排序号不能为空")]
        [StringLength(10, ErrorMessage = "长度不能超过10")]
        public string OrderNo { get; set; }
    }
}
