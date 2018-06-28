using System;
using System.Web.Mvc;
using WZK.Business.Admin;
using WZK.Common;
using WZK.Entity;
using WZK.Business.Admin.Tool;
using WZK.Admin.Models.Script;
using System.Collections.Generic;

namespace WZK.Admin.Controllers
{
    public class ScriptController : BaseController
    {
        ScriptMgt manage = new ScriptMgt();

        #region 脚本列表
        /// <summary>
        /// 脚本列表
        /// </summary>
        /// <returns></returns>
        /// <remarks>add by dingyong,2017-05-18 19:42:14 </remarks>
        public ActionResult Index()
        {
            //适配App
            ViewBag.AppList = EnumTool.ToSelectList(typeof(EApp));
            List<SelectListItem> mtList = new List<SelectListItem>();
            List<SelectListItem> dpiList = new List<SelectListItem>();
            var types = new MobileModelMgt().GetIMobileModelListAll();
            mtList.Add(new SelectListItem { Text = "--请选择--", Value = Guid.Empty.ToString() });
            dpiList.Add(new SelectListItem { Text = "--请选择--", Value = ""});
            foreach (MobileTypes mt in types.ReturnList)
            {
                mtList.Add(new SelectListItem { Text = mt.Name, Value = mt.Id.ToString() });
                dpiList.Add(new SelectListItem { Text = mt.DPI, Value = mt.DPI });
            }
            //手机型号
            ViewBag.MobileTypes = mtList;
            //手机分辨率
            ViewBag.DpiList = dpiList;
            return View();
        }
        /// <summary>
        /// 脚本列表数据
        /// </summary>
        /// <param name="id">页码</param>
        /// <param name="name">脚本名称</param>
        /// <param name="app">适配App</param>
        /// <param name="dpi">适配分辨率率</param>
        /// <param name="mType">适配手机型号</param>
        /// <returns></returns>
        public ActionResult _Index(int id, string name, string app, string dpi, Guid mType)
        {
            ViewBag.List = manage.GetScriptList(name,app,dpi,mType,id);
            return View();
        }
        #endregion

        #region 脚本列表(任务管理)
        /// <summary>
        /// 脚本列表
        /// </summary>
        /// <returns></returns>
        /// <remarks>add by dingyong,2017-05-18 19:42:14 </remarks>
        public ActionResult ScriptList()
        {
            //适配App
            ViewBag.AppList = EnumTool.ToSelectList(typeof(EApp));
            List<SelectListItem> mtList = new List<SelectListItem>();
            List<SelectListItem> dpiList = new List<SelectListItem>();
            var types = new MobileModelMgt().GetIMobileModelListAll();
            mtList.Add(new SelectListItem { Text = "--请选择--", Value = Guid.Empty.ToString() });
            dpiList.Add(new SelectListItem { Text = "--请选择--", Value = "" });
            foreach (MobileTypes mt in types.ReturnList)
            {
                mtList.Add(new SelectListItem { Text = mt.Name, Value = mt.Id.ToString() });
                dpiList.Add(new SelectListItem { Text = mt.DPI, Value = mt.DPI });
            }
            //手机型号
            ViewBag.MobileTypes = mtList;
            //手机分辨率
            ViewBag.DpiList = dpiList;
            return View();
        }
        /// <summary>
        /// 脚本列表数据
        /// </summary>
        /// <param name="id">页码</param>
        /// <param name="name">脚本名称</param>
        /// <param name="app">适配App</param>
        /// <param name="dpi">适配分辨率率</param>
        /// <param name="mType">适配手机型号</param>
        /// <returns></returns>
        public ActionResult _ScriptList(int id, string name, string app, string dpi, Guid mType)
        {
            ViewBag.List = manage.GetScriptList(name, app, dpi, mType, id);
            return View();
        }
        #endregion

        #region 脚本信息详情
        /// <summary>
        /// 脚本信息详情
        /// </summary>
        /// <param name="id">脚本编号</param>
        /// <returns></returns>
        /// <remarks>add by dingyong,2017-05-18 19:42:14 </remarks>
        public ActionResult ScriptDetail(Guid? id)
        {
            List<SelectListItem> AppList = new List<SelectListItem>();
            Array _enumValues = System.Enum.GetValues(typeof(EApp));
            AppList.Add(new SelectListItem { Text ="--请选择--", Value = "" });
            foreach (object value in _enumValues)
            {
                AppList.Add(new SelectListItem
                {
                    Text =EnumTool.GetDesc((System.Enum)value),
                    Value = EnumTool.GetDesc((System.Enum)value)
                });
            }
            //适配App
            ViewBag.AppList = AppList;
            List<SelectListItem> mtList = new List<SelectListItem>();
            var types = new MobileModelMgt().GetIMobileModelListAll();
            mtList.Add(new SelectListItem { Text = "--请选择--", Value = Guid.Empty.ToString() });
            foreach (MobileTypes mt in types.ReturnList)
            {
                mtList.Add(new SelectListItem { Text = mt.Name + "(" + mt.DPI + ")", Value = mt.Id.ToString() });
            }
            //手机型号
            ViewBag.MobileTypes = mtList;
            if (id == null)
            {
                ViewBag.name = "新增";
                return View(new ModifyScript());
            }
            else
            {
                ViewBag.name = "详情";
                var info = manage.GetScript(id.Value);
                return View(ModelMapping.ChangeModel<Script, ModifyScript>(info.ReturnObject));
            }
        }
        #endregion

        #region 修改脚本信息
        /// <summary>
        /// 修改脚本信息
        /// </summary>
        /// <param name="model">脚本信息</param>
        /// <returns></returns>
        /// <remarks>add by dingyong,2017-05-18 19:42:14 </remarks>
        public ActionResult ModifyScriptInfo(ModifyScript model)
        {
            if (ModelState.IsValid)
            {
                ResultInfo info = new ResultInfo();
                //新增
                if (model.Id == Guid.Empty)
                {
                    info = manage.AddScript(RoleName, ModelMapping.ChangeModel<ModifyScript, Script>(model), this.UserId);
                }
                //修改
                else
                {
                    info = manage.ModifyScript(RoleName, ModelMapping.ChangeModel<ModifyScript, Script>(model), UserId);
                }
                return Json(info);
            }
            return this.GetAjaxErrorInfo();
        }
        #endregion

        #region 删除脚本
        /// <summary>
        /// 删除脚本
        /// </summary>
        /// <param name="Id">脚本编号.</param>
        /// <returns>ActionResult.</returns>
        /// <remarks>add by dingyong,2017-05-18 19:42:14  </remarks>
        [HttpPost]
        public ActionResult DelScript(Guid id)
        {
            ResultInfo info = manage.DelScript(id, RoleName, UserId);
            return Json(info);
        }
        #endregion
    }
}