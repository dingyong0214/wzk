using System.Web.Mvc;
using WZK.Business;
using WZK.Business.Admin.Tool;
using WZK.Common;
using System.Linq;
using System.Collections.Generic;
using WZK.Entity;
using WZK.Business.Admin;
using System;
using WZK.Models.admin;

namespace WZK.Admin.Controllers
{
    public class HomeController : BaseController
    {

        #region 首页
        /// <summary>
        /// 任务明细
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult _Index()
        {
            ResultTwoList<MPieChart, MBarChart> ResultList = new TaskMgt().TaskStatistics();
            return Json(ResultList);
        }
        #endregion

        #region 权限提示
        /// <summary>
        /// 权限提示
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Authorize()
        {
            return View();
        }
        #endregion

    }
}