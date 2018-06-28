using WZK.Common;
using WZK.Entity;
using System;
using System.Data;
using System.Linq;
using WZK.Models.admin;

namespace WZK.Business.Admin
{
    /// <summary>
    ///定位管理
    /// author:dingyong
    /// date:2017-05-19
    public class LocationMgt : BaseBusiness
    {
        #region 定位列表        
        /// <summary>
        /// 定位列表
        /// </summary>
        /// <param name="title">定位标题</param>
        /// <param name="address">定位地址.</param>
        /// <param name="pageIndex">页码.</param>
        /// <returns>定位列表.</returns>
        /// <remarks>add by dingyong,2017-05-18 19:42:14 </remarks>
        public ResultList<MLocation> GetLocationList(string title,string address, int pageIndex)
        {
            ResultList<MLocation> info = new ResultList<MLocation>();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    var sql = from m in context.Location
                              join u in context.UserAdmin
                              on m.InputUser equals u.Id into temp
                              from t in temp.DefaultIfEmpty()
                              where m.IsDel == 1
                              select new MLocation
                              {
                                  Id = m.Id,
                                  Title = m.Title,
                                  Longitude = m.Longitude,
                                  Latitude = m.Latitude,
                                  LatAndLong = m.LatAndLong,
                                  Address = m.Address,
                                  InputUser = t.Name ?? t.UserName ?? "",
                                  InsertTime = m.InsertTime
                              };
                    if (!string.IsNullOrWhiteSpace(title))
                    {
                        sql = sql.Where(c => c.Title.Contains(title));
                    }
                    if (!string.IsNullOrWhiteSpace(address))
                    {
                        sql = sql.Where(c => c.Address.Contains(address));
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
                log.Error("WZK.Business.Admin.LocationMgt.GetLocationList  获取定位列表Error：", ex);
            }
            return info;
        }
        #endregion

        #region 新增定位信息
        /// <summary>
        /// 新增定位信息
        /// </summary>
        /// <param name="roleName">操作人角色名称.</param>
        /// <param name="entity">定位信息.</param>
        /// <param name="operUserId">操作人编号.</param>
        /// <returns>ResultInfo.</returns>
        /// <remarks>add by dingyong,2017-05-18 19:42:14 </remarks>
        public ResultInfo AddLocation(string roleName, Location entity, Guid operUserId)
        {
            ResultInfo info = new ResultInfo();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    entity.Id = Guid.NewGuid();
                    entity.InputUser = operUserId;
                    context.Location.Add(entity);
                    info.Status = context.SaveChanges() > 0 ? ErrorStatus.S0001 : ErrorStatus.S0002;
                }
                var model = new OperateLog();
                model.UserId = operUserId;
                model.RoleName = roleName;
                model.MenuName = "云控管理---定位管理---新增---保存";
                model.OldTableName = "MomentsTemplate";
                model.OldBussnessId = entity.Id.ToString();
                model.OperateDesc = "新增定位信息";
                PublicBusiness.AddOperateLog(model);
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.LocationMgt.AddLocation  新增定位信息Error：", ex);
            }
            return info;
        }
        #endregion

        #region 删除定位信息(软删除)
        /// <summary>
        /// 删除定位信息
        /// </summary>
        /// <param name="id">定位Id.</param>
        /// <param name="roleName">操作人角色名称</param>
        /// <param name="operUserId">操作人</param>
        /// <returns>ResultInfo.</returns>
        /// <remarks>add by dingyong,2017-05-18 19:42:14 </remarks>
        public ResultInfo DelLocation(Guid id, string roleName, Guid operUserId)
        {
            ResultInfo info = new ResultInfo();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    var entity = context.Location.SingleOrDefault(c => c.Id == id);
                    if (entity != null)
                    {
                        entity.IsDel = 0;
                    }
                    info.Status = context.SaveChanges() > 0 ? ErrorStatus.S0001 : ErrorStatus.S0002;
                }
                var model = new OperateLog();
                model.UserId = operUserId;
                model.RoleName = roleName;
                model.MenuName = "云控管理---定位管理---删除";
                model.OldTableName = "Location";
                model.OldBussnessId = id.ToString();
                model.OperateDesc = "删除定位信息";
                PublicBusiness.AddOperateLog(model);
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.LocationMgt.DelLocation 删除定位信息Error：", ex);
            }
            return info;
        }
        #endregion
    }
}
