using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WZK.Models.admin
{
    public  class MMobile
    {
        /// <summary>
        /// 手机编号
        /// </summary>
        public System.Guid Id { get; set; }
        /// <summary>
        /// 手机名称
        /// </summary>
        public string MobileName { get; set; }
        /// <summary>
        /// 手机型号
        /// </summary>
        public Guid MobileType { get; set; }
        /// <summary>
        /// 手机型号名称
        /// </summary>
        public string MobileTypeName { get; set; }
        /// <summary>
        /// 手机分辨率
        /// </summary>
        public string DPI { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string MobileNo { get; set; }
        /// <summary>
        /// 所在机房编号
        /// </summary>
        public Guid IDCId { get; set; }
        /// <summary>
        /// 所在机房
        /// </summary>
        public string IDC { get; set; }
        /// <summary>
        /// 是否激活 0-未激活，1-已激活
        /// </summary>
        public int IsActive { get; set; }
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
