using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WZK.Models.api
{
    /// <summary>
    /// 登录
    /// </summary>
    public  class MLogin
    {
        /// <summary>
        ///6位数 密码
        /// </summary>
        public int PassWord { get; set; }
        /// <summary>
        /// 客户
        /// </summary>
        public string Customer { get; set; } = "wzk51";
    }
}
