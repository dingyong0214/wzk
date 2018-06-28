using WZK.Common;
using WZK.Entity;
using System;
using System.Data;
using System.Linq;
using WZK.Models.admin;
using System.Collections.Generic;
using EntityFramework.Extensions;
using System.Data.SqlClient;
using System.Data.Entity;

namespace WZK.Business.Admin
{
    /// <summary>
    ///任务管理
    /// author:dingyong
    /// date:2017-05-19
   public class TaskMgt : BaseBusiness
    {
        #region 任务列表        
        /// <summary>
        /// 任务列表
        /// </summary>
        /// <param name="name">任务名称</param>
        /// <param name="startTime">开始执行时间起(时间段)</param>
        /// <param name="endTime">开始执行时间止(时间段)</param>
        /// <param name="type">任务类型.</param>
        /// <param name="template">模板名称</param>
        /// <param name="pageIndex">页码.</param>
        /// <returns>任务列表.</returns>
        /// <remarks>add by dingyong,2017-05-18 19:42:14 </remarks>
        public ResultList<MTask> GetTaskList(string name, DateTime? startTime, DateTime? endTime, int type, string template,int pageIndex)
        {
            ResultList<MTask> info = new ResultList<MTask>();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    var sql = from m in context.Task
                              join u in context.UserAdmin
                              on m.InputUser equals u.Id into temp
                              from t in temp.DefaultIfEmpty()
                              select new MTask
                              {
                                  Id = m.Id,
                                  TaskName = m.TaskName,
                                  TaskDesc = m.TaskDesc,
                                  TaskType=m.TaskType,
                                  TaskTemplateId=m.TaskTemplateId,
                                  TemplateName=m.TemplateName,
                                  StartTime=m.StartTime,
                                  InputUser = t.Name ?? t.UserName ?? "",
                                  InsertTime = m.InsertTime
                              };
                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        sql = sql.Where(c => c.TaskName.Contains(name));
                    }
                    if (startTime != null)
                    {
                        sql = sql.Where(c => c.StartTime>=startTime);
                    }
                    if (endTime != null)
                    {
                        sql = sql.Where(c => c.StartTime <= endTime);
                    }
                    if (type != -99)
                    {
                        sql = sql.Where(c => c.TaskType == type);
                    }
                    if (!string.IsNullOrWhiteSpace(template))
                    {
                        sql = sql.Where(c => c.TemplateName.Contains(template));
                    }
                    info.Count = sql.Count();
                    info.ReturnList = sql.OrderByDescending(i => i.InsertTime).Skip((pageIndex - 1) * PageSize).Take(PageSize).ToList();
                    if (info.ReturnList.Count() == 0 && pageIndex > 1)
                        info.ReturnList = sql.OrderByDescending(i => i.InsertTime).Skip((pageIndex - 2) * PageSize).Take(PageSize).ToList();

                    info.Status = ErrorStatus.S0001;
                }
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.TaskMgt.GetTaskList  获取任务列表Error：", ex);
            }
            return info;
        }
        #endregion

        #region 任务信息
        /// <summary>
        /// 任务信息 .      
        /// </summary>
        /// <param name="id">任务编号.</param>
        /// <returns>任务信息.</returns>
        /// <remarks>add by dingyong,2017-05-18 19:42:14 </remarks>
        public ResultSingle<Task> GetTask(Guid id)
        {
            ResultSingle<Task> info = new ResultSingle<Task>();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    info.ReturnObject = context.Task.Find(id);
                    info.Status = ErrorStatus.S0001;
                }
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.TaskMgt.GetTask  获取任务信息Error：", ex);
            }
            return info;
        }
        #endregion

        #region 新增任务
        /// <summary>
        /// 新增任务
        /// </summary>
        /// <param name="roleName">操作人角色名称.</param>
        /// <param name="entity">任务信息.</param>
        /// <param name="operUserId">操作人编号.</param>
        /// <returns>ResultInfo.</returns>
        /// <remarks>add by dingyong,2017-05-18 19:42:14 </remarks>
        public ResultInfo AddTask(string roleName, Task entity, Guid operUserId)
        {
            ResultInfo info = new ResultInfo();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    //判断10分钟之内是否有执行的任务(同一天内的任务)
                    if (context.Task.Any(t => DbFunctions.DiffMinutes(t.StartTime, entity.StartTime) < 10 && DbFunctions.DiffMinutes(t.StartTime, entity.StartTime) > -10&& DbFunctions.DiffDays(t.StartTime, entity.StartTime)==0))
                    {
                        info.Status = ErrorStatus.S4039;
                    }
                    else
                    {
                        entity.Id = Guid.NewGuid();
                        entity.UpdateTime = DateTime.Now;
                        entity.UpdateUser = operUserId;
                        entity.InputUser = operUserId;
                        context.Task.Add(entity);
                        info.Status = context.SaveChanges() > 0 ? ErrorStatus.S0001 : ErrorStatus.S0002;
                    }
                }
                var model = new OperateLog();
                model.UserId = operUserId;
                model.RoleName = roleName;
                model.MenuName = "云控管理---任务管理---新增---保存";
                model.OldTableName = "Task";
                model.OldBussnessId = entity.Id.ToString();
                model.OperateDesc = "新增任务";
                PublicBusiness.AddOperateLog(model);
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.TaskMgt.AddTask 新增任务Error：", ex);
            }
            return info;
        }
        #endregion

        #region 修改任务信息
        /// <summary>
        /// 修改任务信息.
        /// </summary>
        /// <param name="roleName">操作人角色名称.</param>
        /// <param name="entity">任务信息.</param>
        /// <param name="operUserId">操作人编号.</param>
        /// <returns>ResultInfo.</returns>
        /// <remarks>add by dingyong,2017-05-18 19:42:14  </remarks>
        public ResultInfo ModifyTask(string roleName, Task entity, Guid operUserId)
        {
            ResultInfo info = new ResultInfo();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    //判断10分钟之内是否有执行的任务
                    if (context.Task.Any(t => DbFunctions.DiffMinutes(t.StartTime, entity.StartTime) < 10 && DbFunctions.DiffMinutes(t.StartTime, entity.StartTime) > -10 && t.Id!=entity.Id && DbFunctions.DiffDays(t.StartTime, entity.StartTime) == 0))
                    {
                        info.Status = ErrorStatus.S4039;
                    }
                    else
                    {
                        var task = context.Task.Find(entity.Id);
                        task.TaskName = entity.TaskName;
                        task.TaskDesc = entity.TaskDesc;
                        task.TaskType = entity.TaskType;
                        task.TaskTemplateId = entity.TaskTemplateId;
                        task.TemplateName = entity.TemplateName;
                        task.StartTime = entity.StartTime;
                        task.ExecuteCycle = entity.ExecuteCycle;
                        task.ExecuteTimes = entity.ExecuteTimes;
                        task.ExecuteTimes = entity.ExecuteTimes;
                        task.UpdateTime = DateTime.Now;
                        task.UpdateUser = operUserId;
                        context.SaveChanges();
                        info.Status = ErrorStatus.S0001;
                    }
                }
                if (info.IsPass)
                {
                    //推送修改任务消息
                    PublicBusiness.PushMessageToList(entity.Id, new List<Guid>(),(int)NoticeType.ModifyTask);
                }
                var model = new OperateLog();
                model.UserId = operUserId;
                model.RoleName = roleName;
                model.MenuName = "云控管理---任务管理---查看---保存";
                model.OldTableName = "MomentsTemplate";
                model.OldBussnessId = entity.Id.ToString();
                model.OperateDesc = "修改任务信息";
                PublicBusiness.AddOperateLog(model);
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.TaskMgt.ModifyTask 修改任务信息Error：", ex);
            }
            return info;
        }
        #endregion

        #region 任务手机列表        
        /// <summary>
        /// 任务手机列表
        /// </summary>
        /// <param name="taskId">任务编号</param>
        /// <param name="mobile">手机号码</param>
        /// <param name="mType">手机型号</param>
        /// <param name="state">任务状态</param>
        /// <param name="pageIndex">页码.</param>
        /// <returns>任务列表.</returns>
        /// <remarks>add by dingyong,2017-05-18 19:42:14 </remarks>
        public ResultList<MTaskDetail> GetTaskDetailList(Guid taskId,string mobile, Guid? mType, int state,int pageIndex)
        {
            ResultList<MTaskDetail> info = new ResultList<MTaskDetail>();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    var sql = from td in context.TaskDetail
                              join m in context.Mobile
                              on td.MobileId equals m.Id
                              join t in context.Task
                              on td.TaskId equals t.Id
                              join u in context.UserAdmin
                              on td.InputUser equals u.Id into temp
                              from tt in temp.DefaultIfEmpty()
                              where td.TaskId== taskId
                              select new MTaskDetail
                              {
                                  Id = td.Id,
                                  TaskId = td.TaskId,
                                  TaskName = t.TaskName,
                                  MobileId = td.MobileId,
                                  MobileNo = m.MobileNo,
                                  MobileType=m.MobileType,
                                  MobileTypeName = m.MobileTypeName,
                                  DPI = m.DPI,
                                  ExecutedTimes=td.ExecutedTimes,
                                  EndTime=td.EndTime,
                                  State=td.State,
                                  InputUser = tt.Name ?? tt.UserName ?? "",
                                  InsertTime = td.InsertTime
                              };
                    if (!string.IsNullOrWhiteSpace(mobile))
                    {
                        sql = sql.Where(c => c.MobileNo.Contains(mobile));
                    }
                    if (mType != null&& mType!=Guid.Empty)
                    {
                        sql = sql.Where(c => c.MobileType == mType);
                    }
                    if (state != -99)
                    {
                        sql = sql.Where(c => c.State == state);
                    }
                    info.Count = sql.Count();
                    info.ReturnList = sql.OrderByDescending(i => i.InsertTime).Skip((pageIndex - 1) * PageSize).Take(PageSize).ToList();
                    if (info.ReturnList.Count() == 0 && pageIndex > 1)
                        info.ReturnList = sql.OrderByDescending(i => i.InsertTime).Skip((pageIndex - 2) * PageSize).Take(PageSize).ToList();
                    info.Status = ErrorStatus.S0001;
                }
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.TaskMgt.GetTaskDetailList  获取任务手机列表Error：", ex);
            }
            return info;
        }
        #endregion

        #region 新增任务手机(推送通知客户端执行任务)
        /// <summary>
        /// 新增任务手机
        /// </summary>
        /// <param name="roleName">操作人角色名称.</param>
        /// <param name="entity">任务手机信息.</param>
        /// <param name="operUserId">操作人编号.</param>
        /// <returns>ResultInfo.</returns>
        /// <remarks>add by dingyong,2017-05-18 19:42:14 </remarks>
        public ResultInfo AddTaskDetail(string roleName, TaskDetail entity, Guid operUserId)
        {
            ResultInfo info = new ResultInfo();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    entity.Id = Guid.NewGuid();
                    entity.ExecutedTimes = 0;
                    entity.State = 0;
                    entity.InsertTime = DateTime.Now;
                    entity.InputUser = operUserId;
                    context.TaskDetail.Add(entity);
                    info.Status = context.SaveChanges() > 0 ? ErrorStatus.S0001 : ErrorStatus.S0002;
                }
                if (info.IsPass)
                {
                    //推送新增任务消息
                    PublicBusiness.PushMessageToSingle(entity.Id, (int)NoticeType.AddTask);
                }
                var model = new OperateLog();
                model.UserId = operUserId;
                model.RoleName = roleName;
                model.MenuName = "云控管理---任务管理---指定手机---新增---保存";
                model.OldTableName = "TaskDetail";
                model.OldBussnessId = entity.Id.ToString();
                model.OperateDesc = "新增任务手机";
                PublicBusiness.AddOperateLog(model);
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.TaskMgt.AddTaskDetail  新增任务手机Error：", ex);
            }
            return info;
        }
        #endregion

        #region 批量新增任务手机(推送通知客户端执行任务)
        /// <summary>
        /// 批量新增任务手机
        /// </summary>
        /// <param name="roleName">操作人角色名称</param>
        /// <param name="operUserId">操作人编号</param>
        /// <param name="param">手机编号 信息 （以多个以^分隔）</param>
        /// <param name="taskId">任务编号</param>
        /// <returns></returns>
        /// <remarks>add by dingyong,2017-05-15 16:46:54 </remarks>
        public ResultInfo InsertIntoTaskDetail(string roleName, Guid operUserId, string param,Guid taskId)
        {
            ResultInfo info = new ResultInfo();
            try
            {
                List<Guid> taskDetailId = new List<Guid>();
                using (WZKEntities context = new WZKEntities())
                {
                    var paraArr = param.Split('^');
                    List<TaskDetail> list = new List<TaskDetail>();
                    foreach (var item in paraArr)
                    {
                        var tt = Guid.Parse(item);
                        //手机是否存在
                        if (!context.TaskDetail.Any(c => c.MobileId == tt&&c.TaskId==taskId))
                        {
                            TaskDetail entity = new TaskDetail()
                            {
                                Id = Guid.NewGuid(),
                                TaskId = taskId,
                                MobileId = tt,
                                ExecutedTimes = 0,
                                State=0,
                                InputUser = operUserId,
                                InsertTime = DateTime.Now,
                            };
                            taskDetailId.Add(entity.Id);
                            list.Add(entity);
                        }
                    }
                    if (context.Database.Connection.State != ConnectionState.Open)
                    {
                        context.Database.Connection.Open(); //打开Connection连接    
                    }
                    //调用BulkInsert方法,将entitys集合数据批量插入到数据库的TaskDetail表中    
                    EF_Batch.Instance.BulkInsert<TaskDetail>((SqlConnection)context.Database.Connection, "TaskDetail", list);
                    if (context.Database.Connection.State != ConnectionState.Closed)
                    {
                        context.Database.Connection.Close(); //关闭Connection连接    
                    }
                    info.Status = ErrorStatus.S0001;
                }
                if (info.IsPass)
                {
                    //推送新增任务消息
                    PublicBusiness.PushMessageToList(null,taskDetailId, (int)NoticeType.AddTask);
                }
                var model = new OperateLog();
                model.UserId = operUserId;
                model.RoleName = roleName;
                model.MenuName = "云控管理---任务管理---指定手机---批量新增---保存";
                model.OldTableName = "TaskDetail";
                model.OldBussnessId = taskId.ToString();
                model.OperateDesc = "批量新增任务手机";
                PublicBusiness.AddOperateLog(model);
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.TaskMgt.InsertIntoTaskDetail 批量新增任务手机Error：", ex);
            }
            return info;
        }
        #endregion

        #region 删除任务手机(推送通知客户端删除任务)
        /// <summary>
        /// 删除任务手机
        /// </summary>
        /// <param name="id">任务明细编号.</param>
        /// <param name="roleName">操作人角色名称</param>
        /// <param name="operUserId">操作人</param>
        /// <param name="taskId">任务编号</param>
        /// <returns>ResultInfo.</returns>
        /// <remarks>add by dingyong,2017-05-15 16:46:54 </remarks>
        public ResultInfo DelTaskDetail(Guid id, string roleName, Guid operUserId, Guid taskId)
        {
            ResultInfo info = new ResultInfo();
            try
            {
                //推送删除任务消息
                PublicBusiness.PushMessageToSingle(id, (int)NoticeType.DelTask);
                using (WZKEntities context = new WZKEntities())
                {
                    var entity = context.TaskDetail.SingleOrDefault(c => c.Id == id && c.TaskId== taskId);
                    if (entity != null)
                    {
                        context.TaskDetail.Remove(entity);
                    }
                    info.Status = context.SaveChanges() > 0 ? ErrorStatus.S0001 : ErrorStatus.S0002;
                }
                var model = new OperateLog();
                model.UserId = operUserId;
                model.RoleName = roleName;
                model.MenuName = "云控管理---任务管理---指定手机---删除";
                model.OldTableName = "TaskDetail";
                model.OldBussnessId = id.ToString();
                model.OperateDesc = " 删除任务手机";
                PublicBusiness.AddOperateLog(model);
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.TaskMgt.DelTaskDetail 删除任务手机Error：", ex);
            }
            return info;
        }
        #endregion

        #region 批量删除任务手机(推送通知客户端删除任务)
        /// <summary>
        /// 批量删除任务手机
        /// </summary>
        /// <param name="taskId">任务编号</param>
        /// <param name="param">任务明细编号（以多个以^分隔）</param>
        /// <param name="roleName">操作人角色名称</param>
        /// <param name="operUserId">操作人编号</param>
        /// <returns></returns>
        /// <remarks>add by dingyong,2017-05-15 16:08:14 </remarks>
        public ResultInfo DelTaskDetailInfo(Guid taskId,string param, string roleName, Guid operUserId)
        {
            ResultInfo info = new ResultInfo();
            try
            {
                var paraArr = param.Split('^');
                List<Guid> list = new List<Guid>();
                foreach (var item in paraArr)
                {
                    list.Add(Guid.Parse(item));
                }
                //推送删除任务消息
                PublicBusiness.PushMessageToList(null,list, (int)NoticeType.DelTask);
                using (WZKEntities context = new WZKEntities())
                {
                    //删除选中任务手机
                    info.Status = context.TaskDetail.Where(t =>t.TaskId==taskId && list.Contains(t.Id)).Delete() > 0 ? ErrorStatus.S0001 : ErrorStatus.S0002;
                }
                var model = new OperateLog();
                model.UserId = operUserId;
                model.RoleName = roleName;
                model.MenuName = "云控管理---任务管理---指定手机---批量删除";
                model.OldTableName = "TaskDetail";
                model.OldBussnessId = operUserId.ToString();
                model.OperateDesc = "批量删除任务手机";
                PublicBusiness.AddOperateLog(model);
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.TaskMgt.DelTaskDetailInfo 批量删除任务手机Error：", ex);
            }
            return info;
        }
        #endregion

        #region 删除所有任务手机(推送通知客户端删除任务)
        /// <summary>
        /// 删除所有任务手机
        /// </summary>
        /// <param name="taskId">任务编号</param>
        /// <param name="roleName">操作人角色名称</param>
        /// <param name="operUserId">操作人编号</param>
        /// <returns></returns>
        /// <remarks>add by dingyong,2017-05-15 16:08:14 </remarks>
        public ResultInfo DelAllTaskDetail(Guid taskId, string roleName, Guid operUserId)
        {
            ResultInfo info = new ResultInfo();
            try
            {
                //推送删除任务消息
                PublicBusiness.PushMessageToList(taskId, new List<Guid>(), (int)NoticeType.DelTask);
                using (WZKEntities context = new WZKEntities())
                {
                    info.Status = context.TaskDetail.Where(t => t.TaskId == taskId).Delete() > 0 ? ErrorStatus.S0001 : ErrorStatus.S0002;
                }
                var model = new OperateLog();
                model.UserId = operUserId;
                model.RoleName = roleName;
                model.MenuName = "云控管理---任务管理---指定手机---全部删除";
                model.OldTableName = "TaskDetail";
                model.OldBussnessId = operUserId.ToString();
                model.OperateDesc = "删除所有任务手机";
                PublicBusiness.AddOperateLog(model);
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.TaskMgt.DelAllTaskDetail 删除所有任务手机Error：", ex);
            }
            return info;
        }
        #endregion

        #region 任务统计
        /// <summary>
        /// 任务统计
        /// </summary>
        /// <returns></returns>
        /// <remarks>add by dingyong,2017-05-27 10:08:14 </remarks>
        public ResultTwoList<MPieChart, MBarChart> TaskStatistics()
        {
            ResultTwoList<MPieChart, MBarChart> info = new ResultTwoList<MPieChart, MBarChart>();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    var sql = (from temp in (from t in context.Task
                                            join td in context.TaskDetail
                                            on t.Id equals td.TaskId into temp
                                             from tt in temp.DefaultIfEmpty()
                                             orderby t.TaskType
                                            select new
                                            {
                                                Id = t.Id,
                                                TaskType = t.TaskType,
                                                State = tt.State,
                                                StartTime = t.StartTime
                                            })
                              group temp by new { temp.Id } into result
                              select new
                              {
                                  TaskType = result.FirstOrDefault().TaskType,
                                  // 新任务
                                  NewTaskNum=result.Where(m=>m.State==0).Count(),
                                  //已接受
                                  AcceptNum = result.Where(m => m.State == 1).Count(),
                                  //开始执行
                                  ExecuteNum = result.Where(m => m.State == 2).Count(),
                                  //已完成
                                  CompleteNum = result.Where(m => m.State == 3).Count(),
                                  //执行失败
                                  FailNum = result.Where(m => m.State == 4).Count(),
                              }).ToList();
                    MPieChart model = new MPieChart()
                    {
                        //1.执行脚本 2.自动回复 3.发朋友圈 4.定位
                        TotalCount=sql.Count(),
                        ScriptNum=sql.Where(t=>t.TaskType==1).Count(),
                        AutoReplyNum=sql.Where(t=>t.TaskType==2).Count(),
                        MomentsNum=sql.Where(t=>t.TaskType==3).Count(),
                        LocationNum=sql.Where(t=>t.TaskType==4).Count()
                    };

                    List<MBarChart> bar = new List<MBarChart>();
                    for(var i = 0; i < 4; i++)
                    {
                        MBarChart Mbar = new MBarChart();
                        Mbar.TaskType = i+1;
                        Mbar.NewTaskNum = sql.Where(t => t.TaskType == Mbar.TaskType).Sum(t => t.NewTaskNum);
                        Mbar.AcceptNum = sql.Where(t => t.TaskType == Mbar.TaskType).Sum(t => t.AcceptNum);
                        Mbar.ExecuteNum = sql.Where(t => t.TaskType == Mbar.TaskType).Sum(t => t.ExecuteNum);
                        Mbar.CompleteNum = sql.Where(t => t.TaskType == Mbar.TaskType).Sum(t => t.CompleteNum);
                        Mbar.FailNum = sql.Where(t => t.TaskType == Mbar.TaskType).Sum(t => t.FailNum);
                        bar.Add(Mbar);
                    }
                    info.ReturnObject = model;
                    info.ReturnList = bar;
                    info.Status = ErrorStatus.S0001;
                }
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.TaskMgt.TaskStatistics 任务统计Error：", ex);
            }
            return info;
        }
        #endregion

    }
}
