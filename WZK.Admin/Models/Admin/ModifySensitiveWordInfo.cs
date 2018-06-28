using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WZK.Admin
{
    public class ModifySensitiveWordInfo
    {

        /// <summary>
        ///编号
        ///</summary>
        public System.Guid Id { get; set; }
        /// <summary>
    	///需要替换的敏感词
    	///</summary>
        [Required(ErrorMessage ="请输入敏感词")]
        [StringLength(125, ErrorMessage = "长度不能超过125")]
        public string Word { get; set; }
    }
}
