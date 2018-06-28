using System;
using System.Web.Mvc;
using WZK.Business.Admin;
using WZK.Common;
using WZK.Entity;
using WZK.Business.Admin.Tool;
using WZK.Admin.Models.IDCS;

namespace WZK.Admin.Controllers
{
    public class IDCSController : BaseController
    {
        IDCSMgt manage = new IDCSMgt();

        #region 机房列表
        /// <summary>
        /// 机房列表
        /// </summary>
        /// <returns></returns>
        /// <remarks>author:dingyong date:2017-05-12</remarks>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 机房列表数据
        /// </summary>
        /// <param name="id">页码</param>
        /// <param name="name">机房名称</param>
        /// <returns></returns>
        public ActionResult _Index(int id,string name)
        {
            ViewBag.List = manage.GetIDCSList(name, id);
            return View();
        }
        #endregion

        #region 机房信息详情
        /// <summary>
        /// 机房信息详情
        /// </summary>
        /// <param name="id">机房编号</param>
        /// <returns></returns>
        /// <remarks>author:dingyong date:2017-05-12</remarks>
        public ActionResult IDCDetail(Guid? id)
        {
            if (id == null)
            {
                ViewBag.name = "新增";
                return View(new ModifyIDCInfo());
            }
            else
            {
                ViewBag.name = "详情";
                var info = manage.GetIDCS(id.Value);
                return View(ModelMapping.ChangeModel<IDCS, ModifyIDCInfo>(info.ReturnObject));
            }
        }
        #endregion

        #region 修改机房信息
        /// <summary>
        /// 修改机房信息
        /// </summary>
        /// <param name="model">机房信息</param>
        /// <returns></returns>
        /// <remarks>author:dingyong date:2016-07-05</remarks>
        public ActionResult ModifyIDCInfo(ModifyIDCInfo model)
        {
            if (ModelState.IsValid)
            {
                ResultInfo info = new ResultInfo();
                //新增
                if (model.Id == Guid.Empty)
                {
                    info = manage.AddIDCS(RoleName,ModelMapping.ChangeModel<ModifyIDCInfo, IDCS>(model),this.UserId);
                }
                //修改
                else
                {
                    info = manage.ModifyIDCS(RoleName,ModelMapping.ChangeModel<ModifyIDCInfo, IDCS>(model), UserId);
                }
                return Json(info);
            }
            return this.GetAjaxErrorInfo();
        }
        #endregion

        #region 删除机房
        /// <summary>
        /// 删除机房
        /// </summary>
        /// <param name="Id">机房编号.</param>
        /// <returns>ActionResult.</returns>
        /// <remarks>add by dingyong,2016-09-13 15:17:45 </remarks>
        [HttpPost]
        public ActionResult DeleteIDC(Guid id)
        {
            ResultInfo info = manage.DelIDCS(id, RoleName, UserId);
            return Json(info);
        }
        #endregion

    }
}