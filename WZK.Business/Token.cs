using System;
using System.Linq;
using System.Data;
using WZK.Entity;
using WZK.Common;
using System.Configuration;

namespace WZK.Business
{
    /// <summary>
    /// 验证token
    /// author:dingyong
    /// date:2017-05-24
    /// </summary>
    public class Token : BaseBusiness
    {
        #region 验证是否登录
        /// <summary>
        /// 验证是否登录
        /// </summary>
        /// <param name="token"></param>
        /// <param name="userId">手机编号</param>
        /// <returns></returns>
        /// <remarks>added by dingyong date:2017-05-24</remarks>
        public bool IsLogin(string token, out string userId)
        {
            bool returnVal = false;
            userId = string.Empty;
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    var authorize = context.AppAuthorize.Where(p => p.Token == token && p.State==1).FirstOrDefault();
                    if (authorize != null)
                    {
                       returnVal = true;
                       userId = authorize.Id.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                returnVal = false;
            }
            return returnVal;
        }
        #endregion
    }
}
