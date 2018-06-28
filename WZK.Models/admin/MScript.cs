using System;
namespace WZK.Models.admin
{
   public class MScript
    {
        /// <summary>
        /// 脚本编号
        /// </summary>
        public System.Guid Id { get; set; }
        /// <summary>
        /// 脚本名称
        /// </summary>
        public string ScriptName { get; set; }
        /// <summary>
        /// 适配手机型号
        /// </summary>
        public Guid MobileType { get; set; }
        /// <summary>
        /// 适配手机型号名称
        /// </summary>
        public string MobileTypeName{ get; set; }
        /// <summary>
        /// 适配分辨率
        /// </summary>
        public string DPI { get; set; }
        /// <summary>
        /// 适配App名称
        /// </summary>
        public string APPName { get; set; }
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
