using System;
using System.Web.Mvc;
using WZK.Business.Admin;
using WZK.Common;
using WZK.Entity;
using WZK.Business.Admin.Tool;
using WZK.Admin.Models.AutoReply;
using System.Collections.Generic;

namespace WZK.Admin.Controllers
{
    public class AutoReplyController : BaseController
    {
        AutoReplyMgt manage = new AutoReplyMgt();

        #region  自动回复模板列表
        /// <summary>
        /// 自动回复模板列表
        /// </summary>
        /// <returns></returns>
        /// <remarks>author:dingyong date:2017-05-15</remarks>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 自动回复模板列表数据
        /// </summary>
        /// <param name="id">页码</param>
        /// <param name="name">模板名称</param>
        /// <returns></returns>
        /// <remarks>author:dingyong date:2017-05-15</remarks>
        public ActionResult _Index(int id, string name)
        {
            ViewBag.List = manage.GetTemplateList(name, id);
            return View();
        }
        #endregion

        #region  自动回复模板列表(任务管理)
        /// <summary>
        /// 自动回复模板列表
        /// </summary>
        /// <returns></returns>
        /// <remarks>author:dingyong date:2017-05-15</remarks>
        public ActionResult ReplyList()
        {
            return View();
        }
        /// <summary>
        /// 自动回复模板列表数据
        /// </summary>
        /// <param name="id">页码</param>
        /// <param name="name">模板名称</param>
        /// <returns></returns>
        /// <remarks>author:dingyong date:2017-05-15</remarks>
        public ActionResult _ReplyList(int id, string name)
        {
            ViewBag.List = manage.GetTemplateList(name, id);
            return View();
        }
        #endregion

        #region 自动回复模板详情
        /// <summary>
        /// 自动回复模板详情
        /// </summary>
        /// <param name="id">自动回复模板编号</param>
        /// <returns></returns>
        /// <remarks>author:dingyong date:2017-05-15</remarks>
        public ActionResult TemplateDetail(Guid? id)
        {
            if (id == null)
            {
                ViewBag.name = "新增";
                return View(new ModifyTemplate());
            }
            else
            {
                ViewBag.name = "详情";
                var info = manage.GetTemplate(id.Value);
                return View(ModelMapping.ChangeModel<Entity.AutoReplyTemplate, ModifyTemplate>(info.ReturnObject));
            }
        }
        #endregion

        #region 修改自动回复模板信息
        /// <summary>
        /// 修改自动回复模板信息
        /// </summary>
        /// <param name="model">模板信息</param>
        /// <returns></returns>
        /// <remarks>author:dingyong date:2017-05-15</remarks>
        public ActionResult ModifyTemplate(ModifyTemplate model)
        {
            if (ModelState.IsValid)
            {
                ResultInfo info = new ResultInfo();
                //新增
                if (model.Id == Guid.Empty)
                {
                    info = manage.AddTemplate(RoleName, ModelMapping.ChangeModel<ModifyTemplate, Entity.AutoReplyTemplate>(model), this.UserId);
                }
                //修改
                else
                {
                    info = manage.ModifyTemplate(RoleName, ModelMapping.ChangeModel<ModifyTemplate, Entity.AutoReplyTemplate>(model), UserId);
                }
                return Json(info);
            }
            return this.GetAjaxErrorInfo();
        }
        #endregion

        #region 删除自动回复模板信息
        /// <summary>
        /// 删除自动回复模板信息
        /// </summary>
        /// <param name="Id">模板编号.</param>
        /// <returns>ActionResult.</returns>
        /// <remarks>author:dingyong date:2017-05-15 </remarks>
        [HttpPost]
        public ActionResult DelTemplate(Guid id)
        {
            ResultInfo info = manage.DelTemplate(id, RoleName, UserId);
            return Json(info);
        }
        #endregion

        #region  自动回复答案列表
        /// <summary>
        /// 自动回复答案列表
        /// </summary>
        /// <param name="id">模板编号</param>
        /// <returns></returns>
        /// <remarks>author:dingyong date:2017-05-15</remarks>
        public ActionResult AnswerList(Guid id)
        {
            ViewBag.templateId = id;
            return View();
        }
        /// <summary>
        /// 自动回复模板列表数据
        /// </summary>
        /// <param name="templateId">页码</param>
        /// <param name="name">问题</param>
        /// 
        /// <returns></returns>
        /// <remarks>author:dingyong date:2017-05-15</remarks>
        public ActionResult _AnswerList(int id, Guid templateId, string name)
        {
            ViewBag.templateId = templateId;
            ViewBag.List = manage.GetAnswerList(templateId, name,id);
            return View();
        }
        #endregion

        #region 自动回复答案详情
        /// <summary>
        /// 自动回复答案详情
        /// </summary>
        /// <param name="id">自动回复编号</param>
        /// <param name="templateId">模板编号</param>
        /// <returns></returns>
        /// <remarks>author:dingyong date:2017-05-15</remarks>
        public ActionResult ReplyDetail(Guid? id,Guid templateId)
        {
            if (id == null)
            {
                ViewBag.name = "新增";
                return View(new ModifyReplyInfo() { TemplateId=templateId});
            }
            else
            {
                ViewBag.name = "详情";
                var info = manage.GetReplyInfo(id.Value);
                return View(ModelMapping.ChangeModel<Entity.AutoReply, ModifyReplyInfo>(info.ReturnObject));
            }
        }
        #endregion

        #region 修改自动回复信息
        /// <summary>
        /// 修改自动回复信息
        /// </summary>
        /// <param name="model">自动回复信息</param>
        /// <returns></returns>
        /// <remarks>author:dingyong date:2017-05-15</remarks>
        public ActionResult ModifyReply(ModifyReplyInfo model)
        {
            if (ModelState.IsValid)
            {
                ResultInfo info = new ResultInfo();
                //新增
                if (model.Id == Guid.Empty)
                {
                    info = manage.AddReplyInfo(RoleName, ModelMapping.ChangeModel<ModifyReplyInfo, Entity.AutoReply>(model), this.UserId);
                }
                //修改
                else
                {
                    info = manage.ModifyReplyInfo(RoleName, ModelMapping.ChangeModel<ModifyReplyInfo, Entity.AutoReply>(model), UserId);
                }
                return Json(info);
            }
            return this.GetAjaxErrorInfo();
        }
        #endregion

        #region 删除自动回复信息
        /// <summary>
        /// 删除自动回复信息
        /// </summary>
        /// <param name="Id">自动回复.</param>
        /// <returns>ActionResult.</returns>
        /// <remarks>author:dingyong date:2017-05-15 </remarks>
        [HttpPost]
        public ActionResult DelReply(Guid id)
        {
            ResultInfo info = manage.DelReplyInfo(id, RoleName, UserId);
            return Json(info);
        }
        #endregion
    }
}