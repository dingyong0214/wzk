using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WZK.Models.api
{
   public  class MMobile
    {
        /// <summary>
        ///手机号码
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// Mac地址
        /// </summary>
        public string Mac { get; set; }
        /// <summary>
        /// android 个推Id
        /// </summary>
        public string ClientId { get; set; }
    }
}
