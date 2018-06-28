namespace WZK.Models.admin
{
    public class PhoneModel
    {
        /// <summary>
        ///编号
        /// </summary>
        public System.Guid Id { get; set; }
        /// <summary>
        /// 型号名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 分辨率
        /// </summary>
        public string DPI { get; set; }
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
