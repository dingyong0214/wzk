using System;
using System.Web.Mvc;
using WZK.Business.Admin;
using WZK.Common;
using WZK.Entity;
using WZK.Business.Admin.Tool;
using System.Collections.Generic;
using WZK.Admin.Models.Task;

namespace WZK.Admin.Controllers
{
    public class TaskController : BaseController
    {
        TaskMgt manage = new TaskMgt();

        #region 任务列表
        /// <summary>
        /// 任务列表
        /// </summary>
        /// <returns></returns>
        /// <remarks>author:dingyong date:2017-05-15</remarks>
        public ActionResult Index()
        {
            //任务类型
            ViewBag.TaskType = EnumTool.ToSelectList(typeof(ETaskType));
            return View();
        }

        /// <summary>
        /// 任务列表
        /// </summary>
        /// <param name="id">页码</param>
        /// <param name="name">任务名称</param>
        /// <param name="startTime">任务开始执行时间start</param>
        /// <param name="endTime">任务开始执行时间end</param>
        /// <param name="type">任务类型</param>
        /// <param name="template">模板名称</param>
        /// <returns></returns>
        ///  <remarks>author:dingyong date:2017-05-15</remarks>
        public ActionResult _Index(int id, string name, DateTime? startTime, DateTime? endTime, int type, string template)
        {
            ViewBag.List = manage.GetTaskList(name, ConvertTryParse.TryParseDateTime(startTime), ConvertTryParse.TryParseDateTime(endTime), type,template,id);
            return View();
        }
        #endregion

        #region 任务详情
        /// <summary>
        /// 任务详情
        /// </summary>
        /// <param name="id">任务编号</param>
        /// <param name="templateId">模板编号</param>
        /// <param name="tName">模板名称</param>
        /// <param name="type">模板类型</param>
        /// <returns></returns>
        /// <remarks>author:dingyong date:2017-05-15</remarks>
        public ActionResult TaskDetail(Guid? id,Guid? templateId ,string tName="",int type=1)
        {
            //任务类型
            ViewBag.LTaskType = EnumTool.ToSelectList(typeof(ETaskType),false,false);
            if (id == null)
            {
                ViewBag.name = "新增";
                if (templateId != null && templateId != Guid.Empty)
                {
                    return View(new ModifyTask() { TaskTemplateId=templateId.Value,TemplateName=tName,TaskType=type});
                }
                return View(new ModifyTask());
            }
            else
            {
                ViewBag.name = "详情";
                var info = manage.GetTask(id.Value);
                return View(ModelMapping.ChangeModel<Task, ModifyTask>(info.ReturnObject));
            }
        }
        #endregion

        #region 修改任务信息
        /// <summary>
        /// 修改任务信息
        /// </summary>
        /// <param name="model">任务信息</param>
        /// <returns></returns>
        /// <remarks>author:dingyong date:2017-05-15</remarks>
        public ActionResult ModifyTaskInfo(ModifyTask model)
        {
            if (ModelState.IsValid)
            {
                ResultInfo info = new ResultInfo();
                //新增
                if (model.Id == Guid.Empty)
                {
                    info = manage.AddTask(RoleName, ModelMapping.ChangeModel<ModifyTask, Task>(model), this.UserId);
                }
                //修改
                else
                {
                    info = manage.ModifyTask(RoleName, ModelMapping.ChangeModel<ModifyTask, Task>(model), UserId);
                }
                return Json(info);
            }
            return Json("");
        }
        #endregion

        #region  任务手机列表
        /// <summary>
        /// 任务手机列表
        /// </summary>
        /// <param name="id">任务编号</param>
        /// <returns></returns>
        /// <remarks>author:dingyong date:2017-05-15</remarks>

        public ActionResult TaskDetailList(Guid id)
        {
            //任务状态
            ViewBag.TaskState = EnumTool.ToSelectList(typeof(ETaskState));
            //手机型号
            List<SelectListItem> mtList = new List<SelectListItem>();
            var types = new MobileModelMgt().GetIMobileModelListAll();
            mtList.Add(new SelectListItem { Text = "--请选择--", Value = Guid.Empty.ToString() });
            foreach (MobileTypes mt in types.ReturnList)
            {
                mtList.Add(new SelectListItem { Text = mt.Name, Value = mt.Id.ToString() });
            }
            ViewBag.MobileTypes = mtList;
            ViewBag.taskId = id;
            return View();
        }
        /// <summary>
        /// 任务手机列表数据
        /// </summary>
       /// <param name="id">页码</param>
        /// <param name="taskId">任务编号</param>
        /// <param name="mobile">手机号码</param>
        /// <param name="mType">手机型号</param>
        /// <param name="state">任务状态</param>
        /// <returns></returns>
        /// <remarks>author:dingyong date:2017-05-15</remarks>
        public ActionResult _TaskDetailList(int id,Guid taskId, string mobile, Guid? mType, int state)
        {
            ViewBag.taskId = taskId;
            ViewBag.List = manage.GetTaskDetailList(taskId,mobile,mType,state,id);
            return View();
        }
        #endregion

        #region 新增任务手机(推送通知客户端删除任务)
        /// <summary>
        /// 新增任务手机
        /// </summary>
        /// <param name="TaskId">任务编号.</param>
        /// <param name="MobileId">手机编号</param>
        /// <returns></returns>
        /// <remarks>add by dingyong,2017-05-15 16:46:54 </remarks>
        public ActionResult AddTaskDetail(Guid TaskId, Guid MobileId)
        {
             TaskDetail model = new TaskDetail();
             model.TaskId = TaskId;
             model.MobileId = MobileId;
             ResultInfo info = manage.AddTaskDetail(this.RoleName, model,this.UserId);
             return Json(info);
        }
        #endregion

        #region 批量新增任务手机(推送通知客户端删除任务)
        /// <summary>
        /// 批量新增任务手机
        /// </summary>
        /// <param name="model">model.</param>
        /// <returns></returns>
        /// <remarks>add by dingyong,2017-05-15 16:46:54 </remarks>
        public ActionResult InsertIntoTaskDetail(ModifyTaskDetail model)
        {
            if (ModelState.IsValid)
            {
                ResultInfo info = manage.InsertIntoTaskDetail(this.RoleName, this.UserId, model.Param, model.TaskId);
                return Json(info);
            }
            return this.GetAjaxErrorInfo();
        }
        #endregion

        #region 删除任务手机(推送通知客户端删除任务)
        /// <summary>
        /// 删除任务手机
        /// </summary>
        /// <param name="Id">任务手机编号.</param>
        /// <param name="taskId">任务编号</param>
        /// <returns>ActionResult.</returns>
        /// <remarks>author:dingyong date:2017-05-15 </remarks>
        [HttpPost]
        public ActionResult DelTaskDetail(Guid id, Guid taskId)
        {
            ResultInfo info = manage.DelTaskDetail(id, RoleName, UserId,taskId);
            return Json(info);
        }
        #endregion

        #region 批量删除任务手机(推送通知客户端删除任务)
        /// <summary>
        /// 批量删除任务手机
        /// </summary>
        /// <param name="id">任务编号</param>
        /// <param name="param">任务明细编号（以多个以^分隔）</param>
        /// <returns>ActionResult.</returns>
        /// <remarks>author:dingyong date:2017-05-15 </remarks>
        [HttpPost]
        public ActionResult DelTaskDetailInfo(Guid id,string param)
        {
            ResultInfo info = manage.DelTaskDetailInfo(id,param, RoleName, UserId);
            return Json(info);
        }
        #endregion

        #region 删除所有任务手机(推送通知客户端删除任务)
        /// <summary>
        /// 删除所有任务手机
        /// </summary>
        /// <param name="id">任务编号</param>
        /// <returns>ActionResult.</returns>
        /// <remarks>author:dingyong date:2017-05-15 </remarks>
        [HttpPost]
        public ActionResult DelAllTaskDetail(Guid id)
        {
            ResultInfo info = manage.DelAllTaskDetail(id,RoleName, UserId);
            return Json(info);
        }
        #endregion

    }
}