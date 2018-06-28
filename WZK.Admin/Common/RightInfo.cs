using WZK.Business.Admin;
using WZK.Business.Admin.Models;
using WZK.Common.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;

namespace WZK.Admin.Common
{
    /// <summary>
    /// 权限信息
    /// author:lorne
    /// date:2016-01-18
    /// </summary>
    public class RightInfo
    {
        private static RightInfo rightInfo = null;

        private RightInfo() { }

        public static RightInfo GetRightInfo()
        {
            if (rightInfo == null)
            {
                rightInfo = new RightInfo();
            }
            return rightInfo;
        }

        #region 缓存权限相关数据
        /// <summary>
        /// 缓存权限相关数据
        /// </summary>
        /// <remarks>author:lorne date:2016-01-18</remarks>
        public void CacheRight(Guid? id = null)
        {
            if (id == null)
            {
                id = new WZK.Common.Authentication.Impl.FormsAuthenticationService<AuthUser>().GetSiginUser().Id;
            }
            //缓存权限相关数据，过期时间30分钟（滑动过期）
            Cache cache = HttpRuntime.Cache;
            var manager = new RightMgt();
            cache.Insert(id.Value + "menus", manager.GetRightMMenus(id.Value).ReturnList, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(30));
            cache.Insert(id.Value + "simpleactions", string.Join(",", manager.GetRightActions(id.Value).ReturnList.Select(b => { return JoinCA(b.Controller, b.Action); }).ToArray()), null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(30));
        }

        private string JoinCA(string controller, string action)
        {
            var controllers = controller.Split(',');
            var actions = action.Split(',');
            StringBuilder temp = new StringBuilder();

            for (int i = 0; i < controllers.Length; i++)
            {
                temp.Append(controllers[i].Trim());
                temp.Append(actions[i].Trim());
                temp.Append(",");
            }
            return temp.ToString();
        }
        #endregion

        #region 判断是否有某按钮权限
        /// <summary>
        /// 判断是否有某项权限
        /// </summary>
        /// <param name="con">控制器</param>
        /// <param name="act">Action</param>
        /// <returns>是否有某项权限</returns>
        /// <remarks>author:lorne date:2016-01-18</remarks>
        public bool CanUse(string con, string act)
        {
            Guid UserId = new WZK.Common.Authentication.Impl.FormsAuthenticationService<AuthUser>().GetSiginUser().Id;
            if (HttpRuntime.Cache[UserId + "simpleactions"] == null)
            {
                CacheRight();
            }
            var actions = HttpRuntime.Cache[UserId + "simpleactions"].ToString();
            return actions.IndexOf(con + act + ",", 0, actions.Length, StringComparison.CurrentCultureIgnoreCase) >= 0;
        }
        #endregion

        #region 判断是否有某菜单权限
        /// <summary>
        /// 判断是否有某菜单权限
        /// </summary>
        /// <param name="con">控制器</param>
        /// <param name="act">Action</param>
        /// <returns>判断是否有某菜单权限</returns>
        /// <remarks>author:dingyong date:2016-08-10</remarks>
        public bool CanUseMenu(string con, string act)
        {
            Guid UserId = new WZK.Common.Authentication.Impl.FormsAuthenticationService<AuthUser>().GetSiginUser().Id;
            if (HttpRuntime.Cache[UserId + "menus"] == null)
            {
                CacheRight();
            }
            var mMenus = HttpRuntime.Cache[UserId + "menus"] as List<MMenus>;
            if (mMenus == null)
            {
                return false;
            }
            return mMenus.Where(p => !string.IsNullOrWhiteSpace(p.Url)).Select(p => p.Url.ToLower()).ToList().Contains(string.Format("/{0}/{1}", con.Trim('/'), act.Trim('/')).ToLower());
        }
        #endregion
    }
}