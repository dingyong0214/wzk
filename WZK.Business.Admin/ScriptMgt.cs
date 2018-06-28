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
    ///脚本管理
    /// author:dingyong
    /// date:2017-05-18
    /// </summary>
    public class ScriptMgt : BaseBusiness
    {
        #region 脚本列表        
        /// <summary>
        /// 脚本列表
        /// </summary>
        /// <param name="name">脚本名称.</param>
        /// <param name="app">适配app</param>
        /// <param name="dpi">适配分辨率</param>
        /// <param name="mType">适配手机型号</param>
        /// <param name="pageIndex">页码.</param>
        /// <returns>脚本列表.</returns>
        /// <remarks>add by dingyong,2017-05-18 19:42:14 </remarks>
        public ResultList<MScript> GetScriptList(string name,string app,string dpi, Guid? mType,int pageIndex)
        {
            ResultList<MScript> info = new ResultList<MScript>();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    var sql = from m in context.Script
                              join mt in context.MobileTypes
                              on m.MobileType equals mt.Id
                              join u in context.UserAdmin
                              on m.InputUser equals u.Id into temp
                              from t in temp.DefaultIfEmpty()
                              where m.IsDel==1
                              select new MScript
                              {
                                  Id = m.Id,
                                  ScriptName = m.ScriptName,
                                  MobileType = m.MobileType,
                                  MobileTypeName = mt.Name,
                                  DPI =m.DPI,
                                  APPName=m.APPName,
                                  InputUser = t.Name ?? t.UserName ?? "",
                                  InsertTime = m.InsertTime
                              };
                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        sql = sql.Where(c => c.ScriptName.Contains(name));
                    }
                    if (!string.IsNullOrWhiteSpace(app))
                    {
                        sql = sql.Where(c => c.APPName.Contains(app));
                    }
                    if (!string.IsNullOrWhiteSpace(dpi))
                    {
                        sql = sql.Where(c => c.DPI.Contains(dpi));
                    }
                    if (mType != null && mType != Guid.Empty)
                    {
                        sql = sql.Where(c => c.MobileType==mType.Value);
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
                log.Error("WZK.Business.Admin.ScriptMgt.GetIScriptList  获取脚本列表Error：", ex);
            }
            return info;
        }
        #endregion

        #region 脚本信息
        /// <summary>
        /// 脚本信息 .      
        /// </summary>
        /// <param name="id">脚本编号.</param>
        /// <returns>脚本信息.</returns>
        /// <remarks>add by dingyong,2017-05-18 19:42:14 </remarks>
        public ResultSingle<Script> GetScript(Guid id)
        {
            ResultSingle<Script> info = new ResultSingle<Script>();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    info.ReturnObject = context.Script.Find(id);
                    info.Status = ErrorStatus.S0001;
                }
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.ScriptMgt.GetScript  获取脚本信息Error：", ex);
            }
            return info;
        }
        #endregion

        #region 新增脚本信息
        /// <summary>
        /// 新增脚本信息 .
        /// </summary>
        /// <param name="roleName">操作人角色名称.</param>
        /// <param name="entity">脚本信息.</param>
        /// <param name="operUserId">操作人编号.</param>
        /// <returns>ResultInfo.</returns>
        /// <remarks>add by dingyong,2017-05-18 19:42:14 </remarks>
        public ResultInfo AddScript(string roleName, Script entity, Guid operUserId)
        {
            ResultInfo info = new ResultInfo();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    //是否重名
                    if (context.Script.Any(c => c.ScriptName == entity.ScriptName && c.Id != entity.Id))
                    {
                        info.Status = ErrorStatus.S4038;
                    }
                    else
                    {
                        entity.Id = Guid.NewGuid();
                        entity.UpdateTime = DateTime.Now;
                        entity.UpdateUser = operUserId;
                        entity.InputUser = operUserId;
                        context.Script.Add(entity);
                        info.Status = context.SaveChanges() > 0 ? ErrorStatus.S0001 : ErrorStatus.S0002;
                    }
                }
                var model = new OperateLog();
                model.UserId = operUserId;
                model.RoleName = roleName;
                model.MenuName = "云控管理---脚本管理---新增---保存";
                model.OldTableName = "Script";
                model.OldBussnessId = entity.Id.ToString();
                model.OperateDesc = "新增脚本信息";
                PublicBusiness.AddOperateLog(model);
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.ScriptMgt.AddScript  新增脚本信息Error：", ex);
            }
            return info;
        }
        #endregion

        #region 修改脚本信息       
        /// <summary>
        /// 修改脚本信息.
        /// </summary>
        /// <param name="roleName">操作人角色名称.</param>
        /// <param name="entity">脚本信息.</param>
        /// <param name="operUserId">操作人编号.</param>
        /// <returns>ResultInfo.</returns>
        /// <remarks>add by dingyong,2017-05-18 19:42:14  </remarks>
        public ResultInfo ModifyScript(string roleName, Script entity, Guid operUserId)
        {
            ResultInfo info = new ResultInfo();
            try
            {
                var templateId = Guid.Empty;
                using (WZKEntities context = new WZKEntities())
                {
                    var script = context.Script.Find(entity.Id);
                    //是否重名
                    if (context.Script.Any(c => c.ScriptName == entity.ScriptName && c.Id != entity.Id))
                    {
                        info.Status = ErrorStatus.S4038;
                    }
                    else
                    {
                        script.UpdateTime = DateTime.Now;
                        script.UpdateUser = operUserId;
                        script.MobileType = entity.MobileType;
                        script.DPI = entity.DPI;
                        script.APPName = entity.APPName;
                        script.ScriptName = entity.ScriptName;
                        script.ScriptContent = entity.ScriptContent;
                        context.SaveChanges();
                        info.Status = ErrorStatus.S0001;
                        templateId = context.Task.Where(t => t.TaskTemplateId == entity.Id).Select(t => t.Id).FirstOrDefault();
                    }
                }
                if (info.IsPass)
                {
                    //推送修改模板消息
                    PublicBusiness.PushMessageToList(templateId, new List<Guid>(), (int)NoticeType.ModifyTemplate);
                }
                var model = new OperateLog();
                model.UserId = operUserId;
                model.RoleName = roleName;
                model.MenuName = "云控管理---脚本管理---查看---保存";
                model.OldTableName = "Script";
                model.OldBussnessId = entity.Id.ToString();
                model.OperateDesc = "修改脚本信息";
                PublicBusiness.AddOperateLog(model);
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.ScriptMgt.ModifyScript 修改脚本信息Error：", ex);
            }
            return info;
        }
        #endregion

        #region 删除脚本信息(软删除)
        /// <summary>
        /// 删除脚本信息
        /// </summary>
        /// <param name="id">脚本Id.</param>
        /// <param name="roleName">操作人角色名称</param>
        /// <param name="operUserId">操作人</param>
        /// <returns>ResultInfo.</returns>
        /// <remarks>add by dingyong,2017-05-18 19:42:14 </remarks>
        public ResultInfo DelScript(Guid id, string roleName, Guid operUserId)
        {
            ResultInfo info = new ResultInfo();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    var entity = context.Script.SingleOrDefault(c => c.Id == id);
                    if (entity != null)
                    {
                        entity.IsDel = 0;
                    }
                    info.Status = context.SaveChanges() > 0 ? ErrorStatus.S0001 : ErrorStatus.S0002;
                }
                var model = new OperateLog();
                model.UserId = operUserId;
                model.RoleName = roleName;
                model.MenuName = "云控管理---脚本管理---删除";
                model.OldTableName = "Script";
                model.OldBussnessId = id.ToString();
                model.OperateDesc = "删除脚本信息";
                PublicBusiness.AddOperateLog(model);
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.ScriptMgt.DelScript 删除脚本信息Error：", ex);
            }
            return info;
        }
        #endregion
    }
}
