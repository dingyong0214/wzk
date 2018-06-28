using WZK.Common;
using WZK.Entity;
using System;
using System.Data;
using System.Linq;
using WZK.Models.admin;

namespace WZK.Business.Admin
{
    /// <summary>
    /// 手机型号管理
    /// author:dingyong
    /// date:2017-05-13
    /// </summary>
    public class MobileModelMgt : BaseBusiness
    {
        #region 手机型号列表        
        /// <summary>
        /// 手机型号列表
        /// </summary>
        /// <param name="name">手机型号名称.</param>
        /// <param name="pageIndex">页码.</param>
        /// <returns>手机型号列表.</returns>
        /// <remarks>add by dingyong,2017-05-12 16:08:14 </remarks>
        public ResultList<PhoneModel> GetIMobileModelList(string name, int pageIndex)
        {
            ResultList<PhoneModel> info = new ResultList<PhoneModel>();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    var sql = from m in context.MobileTypes
                              join u in context.UserAdmin  
                              on m.InputUser equals u.Id into temp
                              from t in temp.DefaultIfEmpty()
                              select new PhoneModel
                              {
                                  Id = m.Id,
                                  Name = m.Name,
                                  DPI=m.DPI,
                                  InputUser = t.Name ?? t.UserName ?? "",
                                  InsertTime = m.InsertTime
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
                log.Error("WZK.Business.Admin.MobileModelMgt.GetIMobileModelList  获取手机型号列表Error：", ex);
            }
            return info;
        }
        #endregion

        #region 手机型号列表（All）        
        /// <summary>
        /// 手机型号列表（All）   
        /// </summary>
        /// <returns>手机型号列表.</returns>
        /// <remarks>add by dingyong,2017-05-12 16:08:14 </remarks>
        public ResultList<MobileTypes> GetIMobileModelListAll()
        {
            ResultList<MobileTypes> info = new ResultList<MobileTypes>();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    info.ReturnList= context.MobileTypes.OrderByDescending(i => i.InsertTime).ToList();
                    info.Status = ErrorStatus.S0001;
                }
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.MobileModelMgt.GetIMobileModelListAll  获取手机型号列表Error：", ex);
            }
            return info;
        }
        #endregion

        #region 手机型号信息        
        /// <summary>
        /// 手机型号信息 .      
        /// </summary>
        /// <param name="id">编号.</param>
        /// <returns>手机型号信息.</returns>
        /// <remarks>add by dingyong,2017-05-12 16:08:14 </remarks>
        public ResultSingle<MobileTypes> GetMobileModel(Guid id)
        {
            ResultSingle<MobileTypes> info = new ResultSingle<MobileTypes>();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    info.ReturnObject = context.MobileTypes.Find(id);
                    info.Status = ErrorStatus.S0001;
                }
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.MobileModelMgt.GetMobileModel  获取手机型号信息Error：", ex);
            }
            return info;
        }
        #endregion

        #region 新增手机型号
        /// <summary>
        /// 新增手机型号 .
        /// </summary>
        /// <param name="roleName">操作人角色名称.</param>
        /// <param name="entity">手机型号  信息.</param>
        /// <param name="operUserId">操作人编号.</param>
        /// <returns>ResultInfo.</returns>
        /// <remarks>add by dingyong,2017-05-12 16:46:54 </remarks>
        public ResultInfo AddMobileModel(string roleName, MobileTypes entity, Guid operUserId)
        {
            ResultInfo info = new ResultInfo();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    //是否重名
                    if (context.MobileTypes.Any(c => c.Name == entity.Name && c.Id != entity.Id))
                    {
                        info.Status = ErrorStatus.S4033;
                    }
                    else
                    {
                        entity.Name = entity.Name.Trim();
                        entity.DPI = entity.DPI.Trim();
                        entity.Id = Guid.NewGuid();
                        entity.UpdateTime = DateTime.Now;
                        entity.UpdateUser = operUserId;
                        entity.InputUser = operUserId;
                        context.MobileTypes.Add(entity);
                        info.Status = context.SaveChanges() > 0 ? ErrorStatus.S0001 : ErrorStatus.S0002;
                    }
                }
                var model = new OperateLog();
                model.UserId = operUserId;
                model.RoleName = roleName;
                model.MenuName = "云控管理---手机型号管理---新增---保存";
                model.OldTableName = "MobileTypes";
                model.OldBussnessId = entity.Id.ToString();
                model.OperateDesc = "新增手机型号信息";
                PublicBusiness.AddOperateLog(model);
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.MobileModelMgt.AddMobileModel  新增手机型号信息Error：", ex);
            }
            return info;
        }
        #endregion

        #region 修改手机型号
        /// <summary>
        /// 修改手机型号.
        /// </summary>
        /// <param name="roleName">操作人角色名称.</param>
        /// <param name="entity">手机型号信息.</param>
        /// <param name="operUserId">操作人编号.</param>
        /// <returns>ResultInfo.</returns>
        /// <remarks>add by dingyong,2017-05-12 16:46:54 </remarks>
        public ResultInfo ModifyMobileModel(string roleName, MobileTypes entity, Guid operUserId)
        {
            ResultInfo info = new ResultInfo();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    var types = context.MobileTypes.Find(entity.Id);
                    //是否重名
                    if (context.MobileTypes.Any(c => c.Name == entity.Name && c.Id != entity.Id))
                    {
                        info.Status = ErrorStatus.S4033;
                    }
                    else
                    {
                        types.UpdateTime = DateTime.Now;
                        types.UpdateUser = operUserId;
                        types.Name = entity.Name.Trim();
                        types.DPI = entity.DPI.Trim();
                        context.SaveChanges();
                        info.Status = ErrorStatus.S0001;
                    }
                }
                var model = new OperateLog();
                model.UserId = operUserId;
                model.RoleName = roleName;
                model.MenuName = "云控管理---手机型号管理---查看---保存";
                model.OldTableName = "MobileTypes";
                model.OldBussnessId = entity.Id.ToString();
                model.OperateDesc = "修改手机型号信息";
                PublicBusiness.AddOperateLog(model);
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.MobileModelMgt.ModifyMobileModel 修改手机型号信息Error：", ex);
            }
            return info;
        }
        #endregion

        #region 删除手机型号
        /// <summary>
        /// 删除手机型号
        /// </summary>
        /// <param name="id">手机型号Id.</param>
        /// <param name="roleName">操作人角色名称</param>
        /// <param name="operUserId">操作人</param>
        /// <returns>ResultInfo.</returns>
        /// <remarks>add by dingyong,2017-05-12 16:46:54 </remarks>
        public ResultInfo DelMobileModel(Guid id, string roleName, Guid operUserId)
        {
            ResultInfo info = new ResultInfo();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    var entity = context.MobileTypes.SingleOrDefault(c => c.Id == id);
                    if (entity != null)
                    {
                        context.MobileTypes.Remove(entity);
                    }
                    info.Status = context.SaveChanges() > 0 ? ErrorStatus.S0001 : ErrorStatus.S0002;
                }
                var model = new OperateLog();
                model.UserId = operUserId;
                model.RoleName = roleName;
                model.MenuName = "云控管理---手机型号管理---删除";
                model.OldTableName = "MobileTypes";
                model.OldBussnessId = id.ToString();
                model.OperateDesc = " 删除手机型号";
                PublicBusiness.AddOperateLog(model);
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.MobileModelMgt.DelIMobileModel 删除手机型号Error：", ex);
            }
            return info;
        }
        #endregion

    }
}
