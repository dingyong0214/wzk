using WZK.Common;
using WZK.Entity;
using System;
using System.Data;
using System.Linq;
using WZK.Models.admin;
using System.Collections.Generic;
using System.Data.SqlClient;
using EntityFramework.Extensions;

namespace WZK.Business.Admin
{
    /// <summary>
    /// 自动回复管理
    /// author:dingyong
    /// date:2017-05-17
    /// </summary>
   public class AutoReplyMgt : BaseBusiness
    {
        #region 自动回复模板列表       
        /// <summary>
        /// 自动回复模板列表
        /// </summary>
        /// <param name="name">模板名称</param>
        /// <param name="pageIndex">页码</param>
        /// <returns>模板列表</returns>
        /// <remarks>add by dingyong,2017-05-15 16:08:14 </remarks>
        public ResultList<MReplyTemplate> GetTemplateList(string name, int pageIndex)
        {
            ResultList<MReplyTemplate> info = new ResultList<MReplyTemplate>();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    var sql = from a in context.AutoReplyTemplate
                              join u in context.UserAdmin
                              on a.InputUser equals u.Id into temp
                              from t in temp.DefaultIfEmpty()
                              where a.IsDel == 1
                              select new MReplyTemplate
                              {
                                  Id = a.Id,
                                  Name = a.Name,
                                  Description = a.Description,
                                  Pic = a.Pic,
                                  PicIndex = a.PicIndex,
                                  InputUser = t.Name ?? t.UserName ?? "",
                                  InsertTime=a.InsertTime
                              };
                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        sql = sql.Where(c => c.Name.Contains(name));
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
                log.Error("WZK.Business.Admin.AutoReplyMgt.GetTemplateList  获取自动回复模板列表Error：", ex);
            }
            return info;
        }
        #endregion

        #region 自动回复模板信息        
        /// <summary>
        /// 自动回复模板信息 .      
        /// </summary>
        /// <param name="id">编号.</param>
        /// <returns>自动回复模板信息.</returns>
        /// <remarks>add by dingyong,2017-05-15 16:08:14 </remarks>
        public ResultSingle<AutoReplyTemplate> GetTemplate(Guid id)
        {
            ResultSingle<AutoReplyTemplate> info = new ResultSingle<AutoReplyTemplate>();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    info.ReturnObject = context.AutoReplyTemplate.Find(id);
                    info.Status = ErrorStatus.S0001;
                }
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.AutoReplyMgt.GetTemplate  获取自动回复模板信息Error：", ex);
            }
            return info;
        }
        #endregion

        #region 新增自动回复模板
        /// <summary>
        /// 新增自动回复模板 .
        /// </summary>
        /// <param name="roleName">操作人角色名称.</param>
        /// <param name="entity">自动回复模板 信息.</param>
        /// <param name="operUserId">操作人编号.</param>
        /// <returns>ResultInfo.</returns>
        /// <remarks>add by dingyong,2017-05-15 16:46:54 </remarks>
        public ResultInfo AddTemplate(string roleName, AutoReplyTemplate entity, Guid operUserId)
        {
            ResultInfo info = new ResultInfo();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    //模板名称否存在
                    if (context.AutoReplyTemplate.Any(c => c.Name == entity.Name && c.Id != entity.Id && c.IsDel==1))
                    {
                        info.Status = ErrorStatus.S4036;
                    }
                    else
                    {
                        entity.Id = Guid.NewGuid();
                        entity.UpdateTime = DateTime.Now;
                        entity.UpdateUser = operUserId;
                        entity.InputUser = operUserId;
                        context.AutoReplyTemplate.Add(entity);
                        info.Status = context.SaveChanges() > 0 ? ErrorStatus.S0001 : ErrorStatus.S0002;
                    }
                }
                var model = new OperateLog();
                model.UserId = operUserId;
                model.RoleName = roleName;
                model.MenuName = "云控管理---自动回复---新增---保存";
                model.OldTableName = "AutoReplyTemplate";
                model.OldBussnessId = entity.Id.ToString();
                model.OperateDesc = "新增自动回复模板信息";
                PublicBusiness.AddOperateLog(model);
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.AutoReplyMgt.AddTemplate  新增自动回复模板信息Error：", ex);
            }
            return info;
        }
        #endregion

        #region 修改自动回复模板信息
        /// <summary>
        /// 修改自动回复模板信息.
        /// </summary>
        /// <param name="roleName">操作人角色名称.</param>
        /// <param name="entity">修改自动回复模板信息.</param>
        /// <param name="operUserId">操作人编号.</param>
        /// <returns>ResultInfo.</returns>
        /// <remarks>add by dingyong,2017-05-15 16:46:54 </remarks>
        public ResultInfo ModifyTemplate(string roleName, AutoReplyTemplate entity, Guid operUserId)
        {
            ResultInfo info = new ResultInfo();
            try
            {
                var templateId = Guid.Empty;
                using (WZKEntities context = new WZKEntities())
                {
                    var template = context.AutoReplyTemplate.Find(entity.Id);
                    //模板名称否存在
                    if (context.AutoReplyTemplate.Any(c => c.Name == entity.Name && c.Id != entity.Id && c.IsDel==1))
                    {
                        info.Status = ErrorStatus.S4036;
                    }
                    else
                    {
                        template.UpdateTime = DateTime.Now;
                        template.UpdateUser = operUserId;
                        template.Name = entity.Name;
                        template.Description = entity.Description;
                        template.Pic = entity.Pic;
                        template.PicIndex = entity.PicIndex;
                        context.SaveChanges();
                        info.Status = ErrorStatus.S0001;
                    }
                    templateId = context.Task.Where(t => t.TaskTemplateId == entity.Id).Select(t=>t.Id).FirstOrDefault();
                }
                if (info.IsPass)
                {
                    //推送修改模板消息
                    PublicBusiness.PushMessageToList(templateId, new List<Guid>(), (int)NoticeType.ModifyTemplate);
                }
                var model = new OperateLog();
                model.UserId = operUserId;
                model.RoleName = roleName;
                model.MenuName = "云控管理---自动回复---查看---保存";
                model.OldTableName = "AutoReplyTemplate";
                model.OldBussnessId = entity.Id.ToString();
                model.OperateDesc = "修改自动回复模板信息";
                PublicBusiness.AddOperateLog(model);
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.AutoReplyMgt.ModifyTemplate 修改自动回复模板信息Error：", ex);
            }
            return info;
        }
        #endregion

        #region 删除自动回复模板
        /// <summary>
        /// 删除自动回复模板
        /// </summary>
        /// <param name="id">自动回复模板编号.</param>
        /// <param name="roleName">操作人角色名称</param>
        /// <param name="operUserId">操作人</param>
        /// <returns>ResultInfo.</returns>
        /// <remarks>add by dingyong,2017-05-15 16:46:54 </remarks>
        public ResultInfo DelTemplate(Guid id, string roleName, Guid operUserId)
        {
            ResultInfo info = new ResultInfo();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    var entity = context.AutoReplyTemplate.SingleOrDefault(c => c.Id == id);
                    if (entity != null)
                    {
                        entity.IsDel = 0;
                    }
                    info.Status = context.SaveChanges() > 0 ? ErrorStatus.S0001 : ErrorStatus.S0002;
                }
                var model = new OperateLog();
                model.UserId = operUserId;
                model.RoleName = roleName;
                model.MenuName = "云控管理---自动回复---删除";
                model.OldTableName = "AutoReplyTemplate";
                model.OldBussnessId = id.ToString();
                model.OperateDesc = "删除自动回复模板";
                PublicBusiness.AddOperateLog(model);
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.AutoReplyMgt.DelTemplate 删除自动回复模板Error：", ex);
            }
            return info;
        }
        #endregion

        #region 自动回复答案列表       
        /// <summary>
        /// 自动回复答案列表
        /// </summary>
        /// <param name="templateId">自动回复模板编号</param>
        /// <param name="name">问题</param>
        /// <param name="pageIndex">页码</param>
        /// <returns>自动回复答案列表</returns>
        /// <remarks>add by dingyong,2017-05-15 16:08:14 </remarks>
        public ResultList<MAutoReply> GetAnswerList(Guid templateId, string name, int pageIndex)
        {
            ResultList<MAutoReply> info = new ResultList<MAutoReply>();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    var sql = from a in context.AutoReply
                              join r in context.AutoReplyTemplate
                              on a.TemplateId equals r.Id
                              join u in context.UserAdmin
                              on a.InputUser equals u.Id into temp
                              from t in temp.DefaultIfEmpty()
                              where a.TemplateId == templateId
                              select new MAutoReply
                              {
                                  Id = a.Id,
                                  TemplateName = r.Name,
                                  AskNo = a.AskNo,
                                  Question = a.Question,
                                  Answer = a.Answer,
                                  Pic = a.Pic,
                                  PicIndex = a.PicIndex,
                                  OrderNo = a.OrderNo,
                                  InputUser = t.Name ?? t.UserName ?? "",
                                  InsertTime=a.InsertTime
                              };
                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        sql = sql.Where(c => c.Question.Contains(name));
                    }
                    info.Count = sql.Count();
                    info.ReturnList = sql.OrderBy(i => i.OrderNo).Skip((pageIndex - 1) * PageSize).Take(PageSize).ToList();
                    if (info.ReturnList.Count() == 0 && pageIndex > 1)
                        info.ReturnList = sql.OrderBy(i => i.OrderNo).Skip((pageIndex - 2) * PageSize).Take(PageSize).ToList();
                    info.Status = ErrorStatus.S0001;
                }
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.AutoReplyMgt.GetAnswerList  获取自动回复答案列表Error：", ex);
            }
            return info;
        }
        #endregion

        #region 自动回复信息        
        /// <summary>
        /// 自动回复信息 .      
        /// </summary>
        /// <param name="id">编号.</param>
        /// <returns>自动回复信息.</returns>
        /// <remarks>add by dingyong,2017-05-15 16:08:14 </remarks>
        public ResultSingle<AutoReply> GetReplyInfo(Guid id)
        {
            ResultSingle<AutoReply> info = new ResultSingle<AutoReply>();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    info.ReturnObject = context.AutoReply.Find(id);
                    info.Status = ErrorStatus.S0001;
                }
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.AutoReplyMgt.GetReplyInfo  获取自动回复信息Error：", ex);
            }
            return info;
        }
        #endregion

        #region 新增自动回复答案
        /// <summary>
        /// 新增自动回复答案 .
        /// </summary>
        /// <param name="roleName">操作人角色名称.</param>
        /// <param name="entity">自动回复问题答案 信息.</param>
        /// <param name="operUserId">操作人编号.</param>
        /// <returns>ResultInfo.</returns>
        /// <remarks>add by dingyong,2017-05-15 16:46:54 </remarks>
        public ResultInfo AddReplyInfo(string roleName, AutoReply entity, Guid operUserId)
        {
            ResultInfo info = new ResultInfo();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    //回复序号否存在
                    if (context.AutoReply.Any(c => c.AskNo == entity.AskNo && c.Id != entity.Id && c.TemplateId == entity.TemplateId))
                    {
                        info.Status = ErrorStatus.S4037;
                    }
                    else
                    {
                        entity.Id = Guid.NewGuid();
                        entity.InputUser = operUserId;
                        context.AutoReply.Add(entity);
                        info.Status = context.SaveChanges() > 0 ? ErrorStatus.S0001 : ErrorStatus.S0002;
                    }
                }
                var model = new OperateLog();
                model.UserId = operUserId;
                model.RoleName = roleName;
                model.MenuName = "云控管理---自动回复---设置回复---新增---保存";
                model.OldTableName = "AutoReply";
                model.OldBussnessId = entity.Id.ToString();
                model.OperateDesc = "新增自动回复问题答案";
                PublicBusiness.AddOperateLog(model);
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.AutoReplyMgt.AddReplyInfo 新增自动回复问题答案Error：", ex);
            }
            return info;
        }
        #endregion

        #region 修改自动回复答案
        /// <summary>
        /// 修改自动回复答案
        /// </summary>
        /// <param name="roleName">操作人角色名称.</param>
        /// <param name="entity">自动回复问题答案.</param>
        /// <param name="operUserId">操作人编号.</param>
        /// <returns>ResultInfo.</returns>
        /// <remarks>add by dingyong,2017-05-15 16:46:54 </remarks>
        public ResultInfo ModifyReplyInfo(string roleName, AutoReply entity, Guid operUserId)
        {
            ResultInfo info = new ResultInfo();
            try
            {
                var templateId = Guid.Empty;
                using (WZKEntities context = new WZKEntities())
                {
                    var template = context.AutoReply.Find(entity.Id);
                    //回复序号否存在
                    if (context.AutoReply.Any(c => c.AskNo == entity.AskNo && c.Id != entity.Id && c.TemplateId == entity.TemplateId))
                    {
                        info.Status = ErrorStatus.S4037;
                    }
                    else
                    {
                        template.AskNo = entity.AskNo;
                        template.Question = entity.Question;
                        template.Answer = entity.Answer;
                        template.Pic = entity.Pic;
                        template.PicIndex = entity.PicIndex;
                        template.OrderNo = entity.OrderNo;
                        context.SaveChanges();
                        info.Status = ErrorStatus.S0001;
                    }
                    templateId = context.Task.Where(t => t.TaskTemplateId == entity.TemplateId).Select(t => t.Id).FirstOrDefault();
                }
                if (info.IsPass)
                {
                    //推送修改模板消息
                    PublicBusiness.PushMessageToList(templateId, new List<Guid>(), (int)NoticeType.ModifyTemplate);
                }
                var model = new OperateLog();
                model.UserId = operUserId;
                model.RoleName = roleName;
                model.MenuName = "云控管理---自动回复---设置回复---查看---保存";
                model.OldTableName = "AutoReply";
                model.OldBussnessId = entity.Id.ToString();
                model.OperateDesc = "修改自动回复问题答案";
                PublicBusiness.AddOperateLog(model);
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.AutoReplyMgt.ModifyReplyInfo 修改自动回复问题答案Error：", ex);
            }
            return info;
        }
        #endregion

        #region 删除自动回复答案
        /// <summary>
        /// 删除自动回复答案
        /// </summary>
        /// <param name="id">自动回复编号.</param>
        /// <param name="roleName">操作人角色名称</param>
        /// <param name="operUserId">操作人</param>
        /// <returns>ResultInfo.</returns>
        /// <remarks>add by dingyong,2017-05-15 16:46:54 </remarks>
        public ResultInfo DelReplyInfo(Guid id, string roleName, Guid operUserId)
        {
            ResultInfo info = new ResultInfo();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    var entity = context.AutoReply.SingleOrDefault(c => c.Id == id);
                    if (entity != null)
                    {
                        context.AutoReply.Remove(entity);
                    }
                    info.Status = context.SaveChanges() > 0 ? ErrorStatus.S0001 : ErrorStatus.S0002;
                }
                var model = new OperateLog();
                model.UserId = operUserId;
                model.RoleName = roleName;
                model.MenuName = "云控管理---自动回复---设置回复---删除";
                model.OldTableName = "AutoReply";
                model.OldBussnessId = id.ToString();
                model.OperateDesc = "删除自动回复问题答案";
                PublicBusiness.AddOperateLog(model);
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.AutoReplyMgt.DelReplyInfo 删除自动回复答案Error：", ex);
            }
            return info;
        }
        #endregion
    }
}
