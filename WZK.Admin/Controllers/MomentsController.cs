using System;
using System.Web.Mvc;
using WZK.Business.Admin;
using WZK.Common;
using WZK.Entity;
using WZK.Business.Admin.Tool;
using WZK.Admin.Models.Moments;
using System.Collections.Generic;

namespace WZK.Admin.Controllers
{
    public class MomentsController : BaseController
    {
        MomentsMgt manage = new MomentsMgt();

        #region 朋友圈模板列表
        /// <summary>
        /// 朋友圈模板列表
        /// </summary>
        /// <returns></returns>
        /// <remarks>add by dingyong,2017-05-18 19:42:14 </remarks>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 朋友圈模板列表数据
        /// </summary>
        /// <param name="id">页码</param>
        /// <param name="title">标题</param>
        /// <returns></returns>
        public ActionResult _Index(int id, string title)
        {
            ViewBag.List = manage.GetMomentsList(title, id);
            return View();
        }
        #endregion

        #region 朋友圈模板列表(任务管理)
        /// <summary>
        /// 朋友圈模板列表
        /// </summary>
        /// <returns></returns>
        /// <remarks>add by dingyong,2017-05-18 19:42:14 </remarks>
        public ActionResult MomentsList()
        {
            return View();
        }
        /// <summary>
        /// 朋友圈模板列表数据
        /// </summary>
        /// <param name="id">页码</param>
        /// <param name="title">标题</param>
        /// <returns></returns>
        public ActionResult _MomentsList(int id, string title)
        {
            ViewBag.List = manage.GetMomentsList(title, id);
            return View();
        }
        #endregion

        #region  朋友圈模板详情
        /// <summary>
        ///  朋友圈模板详情
        /// </summary>
        /// <param name="id">朋友圈模板编号</param>
        /// <returns></returns>
        /// <remarks>add by dingyong,2017-05-18 19:42:14 </remarks>
        public ActionResult MomentsDetail(Guid? id)
        {
            if (id == null)
            {
                ViewBag.name = "新增";
                return View(new ModifyMoments());
            }
            else
            {
                ViewBag.name = "详情";
                var info = manage.GetMoments(id.Value);
                return View(ModelMapping.ChangeModel<MomentsTemplate, ModifyMoments>(info.ReturnObject));
            }
        }
        #endregion

        #region 修改朋友圈模板信息
        /// <summary>
        /// 修改朋友圈模板信息
        /// </summary>
        /// <param name="model">朋友圈模板信息</param>
        /// <returns></returns>
        /// <remarks>add by dingyong,2017-05-18 19:42:14 </remarks>
        public ActionResult ModifyMomentsInfo(ModifyMoments model)
        {
            if (ModelState.IsValid)
            {
                ResultInfo info = new ResultInfo();
                //新增
                if (model.Id == Guid.Empty)
                {
                    info = manage.AddMoments(RoleName, ModelMapping.ChangeModel<ModifyMoments, MomentsTemplate>(model), this.UserId);
                }
                //修改
                else
                {
                    info = manage.ModifyMoments(RoleName, ModelMapping.ChangeModel<ModifyMoments, MomentsTemplate>(model), UserId);
                }
                return Json(info);
            }
            return this.GetAjaxErrorInfo();
        }
        #endregion

        #region 删除朋友圈模板信息
        /// <summary>
        /// 删除朋友圈模板信息
        /// </summary>
        /// <param name="Id">朋友圈模板编号.</param>
        /// <returns>ActionResult.</returns>
        /// <remarks>add by dingyong,2017-05-18 19:42:14  </remarks>
        [HttpPost]
        public ActionResult DelMoments(Guid id)
        {
            ResultInfo info = manage.DelMoments(id, RoleName, UserId);
            return Json(info);
        }
        #endregion
    }
}