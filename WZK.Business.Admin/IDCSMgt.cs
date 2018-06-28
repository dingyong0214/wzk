using WZK.Common;
using WZK.Entity;
using System;
using System.Data;
using System.Linq;
using WZK.Models.admin;

namespace WZK.Business.Admin
{
    /// <summary>
    /// 机房数据后台管理
    /// author:dingyong
    /// date:2017-05-12
    /// </summary>
    public class IDCSMgt : BaseBusiness
    {
        #region 机房列表        
        /// <summary>
        /// 机房列表
        /// </summary>
        /// <param name="name">机房名/地址.</param>
        /// <param name="pageIndex">页码.</param>
        /// <returns>机房列表.</returns>
        /// <remarks>add by dingyong,2017-05-12 16:08:14 </remarks>
        public ResultList<MIDCS> GetIDCSList( string name, int pageIndex)
        {
            ResultList<MIDCS> info = new ResultList<MIDCS>();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    var sql = from m in context.IDCS join u in context.UserAdmin
                              on m.InputUser equals u.Id into temp
                              from t in temp.DefaultIfEmpty()
                              select new MIDCS
                              {
                                  Id=m.Id,
                                  Name=m.Name,
                                  Address=m.Address,
                                  InputUser= t.Name ?? t.UserName ?? "",
                                  InsertTime=m.InsertTime
                              };
                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        sql = sql.Where(c => c.Name.Contains(name) || c.Address.Contains(name));
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
                log.Error("WZK.Business.Admin.IDCSMgt.GetIDCSList  获取机房列表Error：", ex);
            }
            return info;
        }
        #endregion

        #region 机房列表（All）
        /// <summary>
        /// 机房列表所有数据
        /// </summary>
        /// <returns></returns>
        /// <remarks>add by dingyong,2017-05-12 16:08:14 </remarks>
        public ResultList<IDCS> GetIDCSListAll()
        {
            ResultList<IDCS> info = new ResultList<IDCS>();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    info.ReturnList =context.IDCS.OrderByDescending(i => i.InsertTime).ToList();
                    info.Status = ErrorStatus.S0001;
                }
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.IDCSMgt.GetIDCSListAll  获取机房列表Error：", ex);
            }
            return info;
        }

        #endregion

        #region 机房信息        
        /// <summary>
        /// 机房信息 .      
        /// </summary>
        /// <param name="id">机房编号.</param>
        /// <returns>机房信息.</returns>
        /// <remarks>add by dingyong,2017-05-12 16:08:14 </remarks>
        public ResultSingle<IDCS> GetIDCS(Guid id)
        {
            ResultSingle<IDCS> info = new ResultSingle<IDCS>();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    info.ReturnObject = context.IDCS.Find(id);
                    info.Status = ErrorStatus.S0001;
                }
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.IDCSMgt.GetIDCS  获取机房信息Error：", ex);
            }
            return info;
        }
        #endregion

        #region 新增机房     
        /// <summary>
        /// 新增机房 .
        /// </summary>
        /// <param name="roleName">操作人角色名称.</param>
        /// <param name="entity">机房  信息.</param>
        /// <param name="operUserId">操作人编号.</param>
        /// <returns>ResultInfo.</returns>
        /// <remarks>add by dingyong,2017-05-12 16:46:54 </remarks>
        public ResultInfo AddIDCS(string roleName, IDCS entity, Guid operUserId)
        {
            ResultInfo info = new ResultInfo();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    //是否重名
                    if (context.IDCS.Any(c => c.Name == entity.Name&&c.Id!=entity.Id))
                    {
                        info.Status = ErrorStatus.S4032;
                    }
                    else
                    {
                        entity.Id = Guid.NewGuid();
                        entity.UpdateTime = DateTime.Now;
                        entity.UpdateUser = operUserId;
                        entity.InputUser = operUserId;
                        context.IDCS.Add(entity);
                        info.Status = context.SaveChanges() > 0 ? ErrorStatus.S0001 : ErrorStatus.S0002;
                    }
                }
                var model = new OperateLog();
                model.UserId = operUserId;
                model.RoleName = roleName;
                model.MenuName = "云控管理---机房管理---新增---保存";
                model.OldTableName = "IDCS";
                model.OldBussnessId = entity.Id.ToString();
                model.OperateDesc = "新增机房信息";
                PublicBusiness.AddOperateLog(model);
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.IDCSMgt.AddIDCS  新增机房信息Error：", ex);
            }
            return info;
        }
        #endregion

        #region 修改机房信息        
        /// <summary>
        /// 修改机房信息.
        /// </summary>
        /// <param name="roleName">操作人角色名称.</param>
        /// <param name="entity">机房信息.</param>
        /// <param name="operUserId">操作人编号.</param>
        /// <returns>ResultInfo.</returns>
        /// <remarks>add by dingyong,2017-05-12 16:46:54 </remarks>
        public ResultInfo ModifyIDCS(string roleName, IDCS entity, Guid operUserId)
        {
            ResultInfo info = new ResultInfo();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    var Idcs = context.IDCS.Find(entity.Id);
                    //是否重名
                    if (context.IDCS.Any(c => c.Name == entity.Name && c.Id != entity.Id))
                    {
                        info.Status = ErrorStatus.S4032;
                    }
                    else
                    {
                        Idcs.UpdateTime = DateTime.Now;
                        Idcs.UpdateUser = operUserId;
                        Idcs.Name = entity.Name;
                        Idcs.Address = entity.Address;
                        context.SaveChanges();
                        info.Status = ErrorStatus.S0001;
                    }
                }
                var model = new OperateLog();
                model.UserId = operUserId;
                model.RoleName = roleName;
                model.MenuName = "云控管理---机房管理---查看---保存";
                model.OldTableName = "IDCS";
                model.OldBussnessId = entity.Id.ToString();
                model.OperateDesc = "修改机房信息";
                PublicBusiness.AddOperateLog(model);
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.IDCSMgt.ModifyIDCS 修改机房信息Error：", ex);
            }
            return info;
        }
        #endregion

        #region 删除机房
        /// <summary>
        /// 删除机房
        /// </summary>
        /// <param name="id">机房Id.</param>
        /// <param name="roleName">操作人角色名称</param>
        /// <param name="operUserId">操作人</param>
        /// <returns>ResultInfo.</returns>
        /// <remarks>add by dingyong,2017-05-12 16:46:54 </remarks>
        public ResultInfo DelIDCS(Guid id, string roleName, Guid operUserId)
        {
            ResultInfo info = new ResultInfo();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    var entity = context.IDCS.SingleOrDefault(c => c.Id == id);
                    if (entity != null)
                    {
                        context.IDCS.Remove(entity);
                    }
                    info.Status = context.SaveChanges() > 0 ? ErrorStatus.S0001 : ErrorStatus.S0002;
                }
                var model = new OperateLog();
                model.UserId = operUserId;
                model.RoleName = roleName;
                model.MenuName = "云控管理---机房管理---删除";
                model.OldTableName = "IDCS";
                model.OldBussnessId = id.ToString();
                model.OperateDesc = "删除机房信息";
                PublicBusiness.AddOperateLog(model);
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.IDCSMgt.DelIDCS 删除机房信息Error：", ex);
            }
            return info;
        }
        #endregion

    }
}
