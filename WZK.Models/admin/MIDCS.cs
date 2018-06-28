using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WZK.Models.admin
{
    public  class MIDCS
    {
        /// <summary>
        /// 编号
        /// </summary>
        public System.Guid Id { get; set; }
        /// <summary>
        /// 机房名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 机房地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 录入人
        /// </summary>
        public string InputUser { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        public System.DateTime InsertTime { get; set; }
    }
}
