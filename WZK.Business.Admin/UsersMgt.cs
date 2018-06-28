using WZK.Common;
using WZK.Common.Format;
using WZK.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace WZK.Business.Admin
{
    /// <summary>
    /// 用户数据后台管理
    /// author:dingyong
    /// date:2016-07-09
    /// </summary>
    public class UsersMgt : BusinessBase
    {

        #region 根据身份证号获取生日
        /// <summary>
        /// 根据身份证号获取生日
        /// </summary>
        /// <param name="identityCard"></param>
        /// <returns>生日</returns>
        /// <remarks>add by dingyong，2016-11-15</remarks>
        public DateTime? GetBirthday(string identityCard)
        {
            string birthday = "";
            if (identityCard.Length == 18)
            {
                birthday = identityCard.Substring(6, 4) + "-" + identityCard.Substring(10, 2) + "-" + identityCard.Substring(12, 2);
            }
            DateTime dt = new DateTime();
            if (DateTime.TryParse(birthday, out dt))
            {
                return dt;
            }
            return null;
        }
        #endregion

        #region 根据身份证号获取性别
        /// <summary>
        /// 根据身份证号获取性别
        /// </summary>
        /// <param name="identityCard"></param>
        /// <returns>性别</returns>
        /// <remarks>add by dingyong，2016-11-15</remarks>
        public string GetSex(string identityCard)
        {
            string sex = "";
            if (identityCard.Length == 18)
            {
                sex = identityCard.Substring(14, 3);
            }
            //性别代码为偶数是女性奇数为男性
            if (int.Parse(sex) % 2 == 0)
            {
                sex = "女";
            }
            else
            {
                sex = "男";
            }
            return sex;
        }
        #endregion

        #region 根据身份证号获取籍贯
        /// <summary>
        /// 根据身份证号获取籍贯
        /// </summary>
        /// <param name="identityCard"></param>
        /// <returns>籍贯</returns>
        /// <remarks>add by dingyong，2016-11-15</remarks>
        public string GetJiGuanName(string identityCard)
        {
            using (WZKEntities context = new WZKEntities())
            {
                //var model= context.JiGuan.FirstOrDefault(p => p.JiGuanCode == identityCard.Substring(0, 6));
               // if (model == null)
               // {
                    return "";
              //  }
               // return model.JiGuanCode + "^" + model.NativePlace;
            }
        }
        #endregion

    }
}
