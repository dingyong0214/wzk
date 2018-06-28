using System;
using System.Web.Mvc;
using WZK.Business.Admin;
using WZK.Common;
using WZK.Entity;
using WZK.Business.Admin.Tool;
using WZK.Admin.Models.Location;
using System.Collections.Generic;

namespace WZK.Admin.Controllers
{
    public class LocationController : BaseController
    {
        LocationMgt manage = new LocationMgt();

        #region 定位列表
        /// <summary>
        /// 定位列表
        /// </summary>
        /// <returns></returns>
        /// <remarks>add by dingyong,2017-05-18 19:42:14 </remarks>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 定位列表数据
        /// </summary>
        /// <param name="id">页码</param>
        /// <param name="title">定位标题</param>
        /// <param name="address">定位地址</param>
        /// <returns></returns>
        public ActionResult _Index(int id, string title,string address)
        {
            ViewBag.List = manage.GetLocationList(title,address,id);
            return View();
        }
        #endregion

        #region 定位列表(任务管理)
        /// <summary>
        /// 定位列表
        /// </summary>
        /// <returns></returns>
        /// <remarks>add by dingyong,2017-05-18 19:42:14 </remarks>
        public ActionResult LocationList()
        {
            return View();
        }
        /// <summary>
        /// 定位列表数据
        /// </summary>
        /// <param name="id">页码</param>
        /// <param name="title">定位标题</param>
        /// <param name="address">定位地址</param>
        /// <returns></returns>
        public ActionResult _LocationList(int id, string title, string address)
        {
            ViewBag.List = manage.GetLocationList(title, address, id);
            return View();
        }
        #endregion

        #region  定位详情
        /// <summary>
        ///  定位详情
        /// </summary>
        /// <param name="id">朋友圈模板编号</param>
        /// <returns></returns>
        /// <remarks>add by dingyong,2017-05-18 19:42:14 </remarks>
        public ActionResult LocationDetail()
        {
               ViewBag.name = "新增";
               return View(new ModifyLocation());
        }
        #endregion

        #region 修改定位信息
        /// <summary>
        /// 修改定位信息
        /// </summary>
        /// <param name="model">定位信息</param>
        /// <returns></returns>
        /// <remarks>add by dingyong,2017-05-18 19:42:14 </remarks>
        public ActionResult ModifyLocationInfo(ModifyLocation model)
        {
            if (ModelState.IsValid)
            {
                ResultInfo info = new ResultInfo();
                //新增
                info = manage.AddLocation(RoleName, ModelMapping.ChangeModel<ModifyLocation, Location>(model), this.UserId);
                return Json(info);
            }
            return this.GetAjaxErrorInfo();
        }
        #endregion

        #region 删除定位信息
        /// <summary>
        /// 删除定位信息
        /// </summary>
        /// <param name="Id">定位编号.</param>
        /// <returns>ActionResult.</returns>
        /// <remarks>add by dingyong,2017-05-18 19:42:14  </remarks>
        [HttpPost]
        public ActionResult DelLocation(Guid id)
        {
            ResultInfo info = manage.DelLocation(id, RoleName, UserId);
            return Json(info);
        }
        #endregion
    }
}