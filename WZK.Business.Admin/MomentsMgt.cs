using WZK.Common;
using WZK.Entity;
using System;
using System.Data;
using System.Linq;
using WZK.Models.admin;
using System.Collections.Generic;

namespace WZK.Business.Admin
{
    /// <summary>
    ///朋友圈模板管理
    /// author:dingyong
    /// date:2017-05-19
    public class MomentsMgt : BaseBusiness
    {
        #region 朋友圈模板列表        
        /// <summary>
        /// 朋友圈模板列表
        /// </summary>
        /// <param name="title">标题.</param>
        /// <param name="pageIndex">页码.</param>
        /// <returns>朋友圈模板列表.</returns>
        /// <remarks>add by dingyong,2017-05-18 19:42:14 </remarks>
        public ResultList<MMoments> GetMomentsList(string title, int pageIndex)
        {
            ResultList<MMoments> info = new ResultList<MMoments>();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    var sql = from m in context.MomentsTemplate
                              join u in context.UserAdmin
                              on m.InputUser equals u.Id into temp
                              from t in temp.DefaultIfEmpty()
                              where m.IsDel == 1
                              select new MMoments
                              {
                                  Id = m.Id,
                                  Title=m.Title,
                                  Content=m.Content,
                                  Picture=m.Picture,
                                  InputUser = t.Name ?? t.UserName ?? "",
                                  InsertTime = m.InsertTime
                              };
                    if (!string.IsNullOrWhiteSpace(title))
                    {
                        sql = sql.Where(c => c.Title.Contains(title));
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
                log.Error("WZK.Business.Admin.MomentsMgt.GetMomentsList  朋友圈模板列表Error：", ex);
            }
            return info;
        }
        #endregion

        #region 朋友圈模板信息
        /// <summary>
        /// 朋友圈模板信息 .      
        /// </summary>
        /// <param name="id">朋友圈模板编号.</param>
        /// <returns>朋友圈模板信息.</returns>
        /// <remarks>add by dingyong,2017-05-18 19:42:14 </remarks>
        public ResultSingle<MomentsTemplate> GetMoments(Guid id)
        {
            ResultSingle<MomentsTemplate> info = new ResultSingle<MomentsTemplate>();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    info.ReturnObject = context.MomentsTemplate.Find(id);
                    info.Status = ErrorStatus.S0001;
                }
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.MomentsMgt.GetMoments  获取朋友圈模板信息Error：", ex);
            }
            return info;
        }
        #endregion

        #region 新增朋友圈模板
        /// <summary>
        /// 新增朋友圈模板
        /// </summary>
        /// <param name="roleName">操作人角色名称.</param>
        /// <param name="entity">朋友圈模板信息.</param>
        /// <param name="operUserId">操作人编号.</param>
        /// <returns>ResultInfo.</returns>
        /// <remarks>add by dingyong,2017-05-18 19:42:14 </remarks>
        public ResultInfo AddMoments(string roleName, MomentsTemplate entity, Guid operUserId)
        {
            ResultInfo info = new ResultInfo();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                        entity.Id = Guid.NewGuid();
                        entity.UpdateTime = DateTime.Now;
                        entity.UpdateUser = operUserId;
                        entity.InputUser = operUserId;
                        context.MomentsTemplate.Add(entity);
                        info.Status = context.SaveChanges() > 0 ? ErrorStatus.S0001 : ErrorStatus.S0002;
                }
                var model = new OperateLog();
                model.UserId = operUserId;
                model.RoleName = roleName;
                model.MenuName = "云控管理---朋友圈模板---新增---保存";
                model.OldTableName = "MomentsTemplate";
                model.OldBussnessId = entity.Id.ToString();
                model.OperateDesc = "新增朋友圈模板";
                PublicBusiness.AddOperateLog(model);
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.MomentsMgt.AddMoments  新增朋友圈模板Error：", ex);
            }
            return info;
        }
        #endregion

        #region 修改朋友圈模板信息
        /// <summary>
        /// 修改朋友圈模板信息.
        /// </summary>
        /// <param name="roleName">操作人角色名称.</param>
        /// <param name="entity">朋友圈模板信息.</param>
        /// <param name="operUserId">操作人编号.</param>
        /// <returns>ResultInfo.</returns>
        /// <remarks>add by dingyong,2017-05-18 19:42:14  </remarks>
        public ResultInfo ModifyMoments(string roleName, MomentsTemplate entity, Guid operUserId)
        {
            ResultInfo info = new ResultInfo();
            try
            {
                var templateId = Guid.Empty;
                using (WZKEntities context = new WZKEntities())
                {
                        var moment = context.MomentsTemplate.Find(entity.Id);
                        moment.Title = entity.Title;
                        moment.Content = entity.Content;
                        moment.Picture = entity.Picture;
                        moment.UpdateTime = DateTime.Now;
                        moment.UpdateUser = operUserId;
                        context.SaveChanges();
                        info.Status = ErrorStatus.S0001;
                        templateId = context.Task.Where(t => t.TaskTemplateId == entity.Id).Select(t => t.Id).FirstOrDefault();
                }
                if (info.IsPass)
                {
                    //推送修改模板消息
                    PublicBusiness.PushMessageToList(templateId, new List<Guid>(), (int)NoticeType.ModifyTemplate);
                }
                var model = new OperateLog();
                model.UserId = operUserId;
                model.RoleName = roleName;
                model.MenuName = "云控管理---朋友圈模板---查看---保存";
                model.OldTableName = "MomentsTemplate";
                model.OldBussnessId = entity.Id.ToString();
                model.OperateDesc = "修改朋友圈模板信息";
                PublicBusiness.AddOperateLog(model);
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.MomentsMgt.ModifyMoments 修改朋友圈模板信息Error：", ex);
            }
            return info;
        }
        #endregion

        #region 删除朋友圈模板信息(软删除)
        /// <summary>
        /// 删除朋友圈模板信息
        /// </summary>
        /// <param name="id">朋友圈模板Id.</param>
        /// <param name="roleName">操作人角色名称</param>
        /// <param name="operUserId">操作人</param>
        /// <returns>ResultInfo.</returns>
        /// <remarks>add by dingyong,2017-05-18 19:42:14 </remarks>
        public ResultInfo DelMoments(Guid id, string roleName, Guid operUserId)
        {
            ResultInfo info = new ResultInfo();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    var entity = context.MomentsTemplate.SingleOrDefault(c => c.Id == id);
                    if (entity != null)
                    {
                        entity.IsDel = 0;
                    }
                    info.Status = context.SaveChanges() > 0 ? ErrorStatus.S0001 : ErrorStatus.S0002;
                }
                var model = new OperateLog();
                model.UserId = operUserId;
                model.RoleName = roleName;
                model.MenuName = "云控管理---朋友圈模板---删除";
                model.OldTableName = "MomentsTemplate";
                model.OldBussnessId = id.ToString();
                model.OperateDesc = "删除朋友圈模板信息";
                PublicBusiness.AddOperateLog(model);
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.MomentsMgt.DelMoments 删除朋友圈模板信息Error：", ex);
            }
            return info;
        }
        #endregion
    }
}
