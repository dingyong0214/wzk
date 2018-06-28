using System;
using System.Web.Mvc;
using WZK.Business.Admin;
using WZK.Common;
using WZK.Entity;
using WZK.Business.Admin.Tool;
using WZK.Admin.Models.MobileModel;

namespace WZK.Admin.Controllers
{
    public class MobileModelController : BaseController
    {
        MobileModelMgt manage = new MobileModelMgt();

        #region 手机型号列表
        /// <summary>
        /// 手机型号列表
        /// </summary>
        /// <returns></returns>
        /// <remarks>author:dingyong date:2017-05-12</remarks>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 手机型号列表数据
        /// </summary>
        /// <param name="id">页码</param>
        /// <param name="name">手机型号名称</param>
        /// <returns></returns>
        public ActionResult _Index(int id, string name)
        {
            ViewBag.List = manage.GetIMobileModelList(name, id);
            return View();
        }
        #endregion

        #region 手机型号信息详情
        /// <summary>
        /// 手机型号信息详情
        /// </summary>
        /// <param name="id">手机型号编号</param>
        /// <returns></returns>
        /// <remarks>author:dingyong date:2017-05-12</remarks>
        public ActionResult MobileModelDetail(Guid? id)
        {
            if (id == null)
            {
                ViewBag.name = "新增";
                return View(new ModifyMobileModel());
            }
            else
            {
                ViewBag.name = "详情";
                var info = manage.GetMobileModel(id.Value);
                return View(ModelMapping.ChangeModel<MobileTypes, ModifyMobileModel>(info.ReturnObject));
            }
        }
        #endregion

        #region 修改手机型号信息
        /// <summary>
        /// 修改手机型号信息
        /// </summary>
        /// <param name="model">手机型号信息</param>
        /// <returns></returns>
        /// <remarks>author:dingyong date:2017-05-12</remarks>
        public ActionResult ModifyMobileModelInfo(ModifyMobileModel model)
        {
            if (ModelState.IsValid)
            {
                ResultInfo info = new ResultInfo();
                //新增
                if (model.Id == Guid.Empty)
                {
                    info = manage.AddMobileModel(RoleName, ModelMapping.ChangeModel<ModifyMobileModel, MobileTypes>(model), this.UserId);
                }
                //修改
                else
                {
                    info = manage.ModifyMobileModel(RoleName, ModelMapping.ChangeModel<ModifyMobileModel, MobileTypes>(model), UserId);
                }
                return Json(info);
            }
            return this.GetAjaxErrorInfo();
        }
        #endregion

        #region 删除手机型号
        /// <summary>
        /// 删除手机型号
        /// </summary>
        /// <param name="Id">手机型号编号id.</param>
        /// <returns>ActionResult.</returns>
        /// <remarks>author:dingyong date:2017-05-12</remarks>
        [HttpPost]
        public ActionResult DelMobileModel(Guid id)
        {
            ResultInfo info = manage.DelMobileModel(id, RoleName, UserId);
            return Json(info);
        }
        #endregion
    }
}