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
    /// 手机管理
    /// author:dingyong
    /// date:2017-05-15
    /// </summary>
    public class MobileMgt:BaseBusiness
    {
        #region 手机列表       
        /// <summary>
        /// 手机列表
        /// </summary>
        /// <param name="mobile">手机号码</param>
        /// <param name="mobileModel">手机型号</param>
        /// <param name="iDC">所在机房</param>
        /// <param name="isActive">是否激活</param>
        /// <param name="pageIndex">页码</param>
        /// <returns>手机列表</returns>
        /// <remarks>add by dingyong,2017-05-15 16:08:14 </remarks>
        public ResultList<MMobile> GetMobileList(string mobile,Guid? mobileModel,Guid? iDC,int isActive, int pageIndex)
        {
            ResultList<MMobile> info = new ResultList<MMobile>();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    var sql = from m in context.Mobile
                              join d in context.IDCS
                              on m.IDC equals d.Id
                              join u in context.UserAdmin
                              on m.InputUser equals u.Id into temp
                              from t in temp.DefaultIfEmpty()
                              select new MMobile
                              {
                                  Id = m.Id,
                                  MobileName=m.MobileName,
                                  MobileType=m.MobileType,
                                  MobileTypeName=m.MobileTypeName,
                                  DPI=m.DPI,
                                  MobileNo=m.MobileNo,
                                  IDCId=d.Id,
                                  IDC=d.Name,
                                  IsActive = m.IsActive,
                                  InputUser =t.Name?? t.UserName ?? "",
                                  InsertTime = m.InsertTime
                              };
                    if (!string.IsNullOrWhiteSpace(mobile))
                    {
                        sql = sql.Where(c => c.MobileNo.Contains(mobile));
                    }
                    if (mobileModel != null && mobileModel != Guid.Empty)
                    {
                        sql = sql.Where(c => c.MobileType==mobileModel);
                    }
                    if (iDC != null && iDC != Guid.Empty)
                    {
                        sql = sql.Where(c => c.IDCId == iDC);
                    }
                    if (isActive != -1)
                    {
                        sql = sql.Where(c => c.IsActive == isActive);
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
                log.Error("WZK.Business.Admin.MobileMgt.GetIMobileList  获取手机列表Error：", ex);
            }
            return info;
        }
        #endregion

        #region 手机列表(任务指定,筛选掉已指定的手机)      
        /// <summary>
        /// 手机列表(手机必须是激活状态)
        /// </summary>
        /// <param name="taskId">任务编号</param>
        /// <param name="mobile">手机号码</param>
        /// <param name="mobileModel">手机型号</param>
        /// <param name="iDC">所在机房</param>
        /// <param name="pageIndex">页码</param>
        /// <returns>手机列表</returns>
        /// <remarks>add by dingyong,2017-05-15 16:08:14 </remarks>
        public ResultList<MTaskMobile> GetMobileList(Guid taskId,string mobile, Guid? mobileModel, Guid? iDC, int pageIndex)
        {
            ResultList<MTaskMobile> info = new ResultList<MTaskMobile>();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    var sql = from m in context.Mobile
                              join d in context.IDCS
                              on m.IDC equals d.Id
                              join u in context.TaskDetail.Where(t=>t.TaskId==taskId)
                              on m.Id equals u.MobileId into temp
                              from t in temp.DefaultIfEmpty()
                              where m.IsActive==1 && t.MobileId==null
                              select new MTaskMobile
                              {
                                  Id = m.Id,
                                  MobileName = m.MobileName,
                                  MobileType = m.MobileType,
                                  MobileTypeName = m.MobileTypeName,
                                  DPI = m.DPI,
                                  MobileNo = m.MobileNo,
                                  IDCId = d.Id,
                                  IDC = d.Name,
                                  InsertTime=m.InsertTime
                              };
                    if (!string.IsNullOrWhiteSpace(mobile))
                    {
                        sql = sql.Where(c => c.MobileNo.Contains(mobile));
                    }
                    if (mobileModel != null && mobileModel != Guid.Empty)
                    {
                        sql = sql.Where(c => c.MobileType == mobileModel);
                    }
                    if (iDC != null && iDC != Guid.Empty)
                    {
                        sql = sql.Where(c => c.IDCId == iDC);
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
                log.Error("WZK.Business.Admin.MobileMgt.GetIMobileList  获取手机列表Error：", ex);
            }
            return info;
        }
        #endregion

        #region 手机信息        
        /// <summary>
        /// 手机信息 .      
        /// </summary>
        /// <param name="id">编号.</param>
        /// <returns>手机信息.</returns>
        /// <remarks>add by dingyong,2017-05-15 16:08:14 </remarks>
        public ResultSingle<Mobile> GetMobile(Guid id)
        {
            ResultSingle<Mobile> info = new ResultSingle<Mobile>();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    info.ReturnObject = context.Mobile.Find(id);
                    info.Status = ErrorStatus.S0001;
                }
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.MobileMgt.GetMobile  获取手机信息Error：", ex);
            }
            return info;
        }
        #endregion

        #region 新增手机
        /// <summary>
        /// 新增手机 .
        /// </summary>
        /// <param name="roleName">操作人角色名称.</param>
        /// <param name="entity">手机 信息.</param>
        /// <param name="operUserId">操作人编号.</param>
        /// <returns>ResultInfo.</returns>
        /// <remarks>add by dingyong,2017-05-15 16:46:54 </remarks>
        public ResultInfo AddMobile(string roleName, Mobile entity, Guid operUserId)
        {
            ResultInfo info = new ResultInfo();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    //手机号是否存在
                    if (context.Mobile.Any(c => c.MobileNo == entity.MobileNo && c.Id != entity.Id))
                    {
                        info.Status = ErrorStatus.S4034;
                    }
                    else
                    {
                        entity.Id = Guid.NewGuid();
                        entity.UpdateTime = DateTime.Now;
                        entity.UpdateUser = operUserId;
                        entity.InputUser = operUserId;
                        context.Mobile.Add(entity);
                        info.Status = context.SaveChanges() > 0 ? ErrorStatus.S0001 : ErrorStatus.S0002;
                    }
                }
                var model = new OperateLog();
                model.UserId = operUserId;
                model.RoleName = roleName;
                model.MenuName = "云控管理---手机管理---新增---保存";
                model.OldTableName = "Mobile";
                model.OldBussnessId = entity.Id.ToString();
                model.OperateDesc = "新增手机信息";
                PublicBusiness.AddOperateLog(model);
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.MobileMgt.AddMobile  新增手机信息Error：", ex);
            }
            return info;
        }
        #endregion

        #region 批量新增手机信息
        /// <summary>
        /// 批量新增手机信息
        /// </summary>
        /// <param name="roleName">操作人角色名称</param>
        /// <param name="operUserId">操作人编号</param>
        /// <param name="param">手机号 信息 （以多个以^分隔）</param>
        /// <param name="mName">手机名称</param>
        /// <param name="mType">手机型号</param>
        /// <param name="mtName">手机型号名称</param>
        /// <param name="dpi">分辨率</param>
        /// <param name="idc">所在机房</param>
        /// <param name="postion">所在位置</param>
        /// <param name="isActive">是否激活 0-未激活，1-已激活</param>
        /// <returns></returns>
        /// <remarks>add by dingyong,2017-05-15 16:46:54 </remarks>
        public ResultInfo InsertIntoMobile(string roleName, Guid operUserId ,string param,string mName,Guid mType,string mtName,string dpi, Guid idc,string position,int isActive)
        {
            ResultInfo info = new ResultInfo();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    var paraArr = param.Split('^');
                    List<Mobile> list = new List<Mobile>();
                    var i = 1;
                    foreach (var item in paraArr)
                    {
                        //手机号是否存在&&简单验证手机号（后面如果需要用正则）
                        if (!context.Mobile.Any(c => c.MobileNo == item)&&item.Length==11)
                        {
                            Mobile entity = new Mobile()
                            {
                                Id = Guid.NewGuid(),
                                MobileName = mName + i,
                                MobileType = mType,
                                MobileTypeName = mtName,
                                DPI = dpi,
                                MobileNo = item,
                                IDC = idc,
                                Position = position,
                                IsActive = isActive,
                                InputUser = operUserId,
                                InsertTime = DateTime.Now,
                                UpdateUser=operUserId,
                                UpdateTime=DateTime.Now
                            };
                            list.Add(entity);
                            i++;
                        }
                    }
                    if (context.Database.Connection.State != ConnectionState.Open)
                    {
                        context.Database.Connection.Open(); //打开Connection连接    
                    }
                    //调用BulkInsert方法,将entitys集合数据批量插入到数据库的Mobile表中    
                    EF_Batch.Instance.BulkInsert<Mobile>((SqlConnection)context.Database.Connection, "Mobile", list);
                    if (context.Database.Connection.State != ConnectionState.Closed)
                    {
                        context.Database.Connection.Close(); //关闭Connection连接    
                    }
                    info.Status = ErrorStatus.S0001;
                }
                var model = new OperateLog();
                model.UserId = operUserId;
                model.RoleName = roleName;
                model.MenuName = "云控管理---手机管理---批量新增---保存";
                model.OldTableName = "Mobile";
                model.OldBussnessId = mType.ToString();
                model.OperateDesc = "批量新增手机信息";
                PublicBusiness.AddOperateLog(model);
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.MobileMgt.InsertIntoMobile  批量新增手机信息Error：", ex);
            }
            return info;
        }
        #endregion

        #region 修改手机信息
        /// <summary>
        /// 修改手机信息.
        /// </summary>
        /// <param name="roleName">操作人角色名称.</param>
        /// <param name="entity">修改手机信息.</param>
        /// <param name="operUserId">操作人编号.</param>
        /// <returns>ResultInfo.</returns>
        /// <remarks>add by dingyong,2017-05-15 16:46:54 </remarks>
        public ResultInfo ModifyMobile(string roleName, Mobile entity, Guid operUserId)
        {
            ResultInfo info = new ResultInfo();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    var mobile = context.Mobile.Find(entity.Id);
                    //手机号是否存在
                    if (context.Mobile.Any(c => c.MobileNo == entity.MobileNo && c.Id != entity.Id))
                    {
                        info.Status = ErrorStatus.S4034;
                    }
                    else
                    {
                        mobile.UpdateTime = DateTime.Now;
                        mobile.UpdateUser = operUserId;
                        mobile.MobileName = entity.MobileName;
                        mobile.MobileNo = entity.MobileNo;
                        mobile.MobileType = entity.MobileType;
                        mobile.MobileTypeName = entity.MobileTypeName;
                        mobile.DPI =  entity.DPI;
                        mobile.IsActive = entity.IsActive;
                        mobile.Position = entity.Position;
                        mobile.Mac = entity.Mac;
                        mobile.IDC = entity.IDC;
                        context.SaveChanges();
                        info.Status = ErrorStatus.S0001;
                    }
                }
                var model = new OperateLog();
                model.UserId = operUserId;
                model.RoleName = roleName;
                model.MenuName = "云控管理---手机管理---查看---保存";
                model.OldTableName = "Mobile";
                model.OldBussnessId = entity.Id.ToString();
                model.OperateDesc = "修改手机信息";
                PublicBusiness.AddOperateLog(model);
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.MobileMgt.ModifyMobile 修改手机信息Error：", ex);
            }
            return info;
        }
        #endregion

        #region 批量修改手机信息
        /// <summary>
        /// 批量修改手机信息
        /// </summary>
        /// <param name="roleName">操作人角色名称</param>
        /// <param name="operUserId">操作人编号</param>
        /// <param name="param">手机编号（以多个以^分隔）</param>
        /// <param name="currType">当前手机型号</param>
        /// <param name="mType">修改后手机型号</param>
        /// <param name="mtName">手机型号名称</param>
        /// <param name="dpi">分辨率</param>
        /// <param name="currIDC">当前所在机房</param>
        /// <param name="idc">修改后所在机房</param>
        /// <param name="postion">所在位置</param>
        /// <param name="isActive">是否激活 0-未激活,1-已激活</param>
        /// <returns></returns>
        /// <remarks>add by dingyong,2017-05-15 16:08:14 </remarks>
        public ResultInfo UpdateMobileInfo(string roleName, Guid operUserId, string param,Guid? currType, Guid? mType,string mtName,string dpi, Guid? currIDC, Guid? idc, string position, int isActive)
        {
            ResultInfo info = new ResultInfo();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    var status = 0;
                    var count = 1;
                    if (!string.IsNullOrWhiteSpace(param))
                    {
                        //修改选中手机
                        var paraArr = param.Split('^');
                        List<Guid> list = new List<Guid>();
                        foreach (var item in paraArr)
                        {
                            list.Add(Guid.Parse(item));
                        }
                        status= context.Mobile.Where(c => list.Contains(c.Id)).Update(t=>new Mobile { MobileType =(mType==null||mType==Guid.Empty)? t.MobileType:mType.Value, MobileTypeName= (mType == null || mType == Guid.Empty)?t.MobileTypeName:mtName, DPI = (mType == null || mType == Guid.Empty) ? t.DPI : dpi,IDC =(idc==null||idc==Guid.Empty)?t.IDC:idc.Value, Position = position == string.Empty ? t.Position : position, IsActive = isActive==-1?t.IsActive:isActive});
                    }
                    else
                    {
                        //修改所有手机
                        var sql = context.Mobile.Where(c=>1==1);
                        if (currType != null && currType != Guid.Empty)
                        {
                            sql = sql.Where(c => c.MobileType == currType);
                        }
                        if (currIDC != null && currIDC != Guid.Empty)
                        {
                            sql = sql.Where(c => c.IDC == currIDC);
                        }
                        count = sql.Count();
                        status = sql.Update(t => new Mobile { MobileType = (mType == null || mType == Guid.Empty) ? t.MobileType : mType.Value, MobileTypeName = (mType == null || mType == Guid.Empty) ? t.MobileTypeName : mtName, DPI = (mType == null || mType == Guid.Empty) ? t.DPI : dpi, IDC = (idc == null || idc == Guid.Empty) ? t.IDC : idc.Value, Position = position ==string.Empty? t.Position:position, IsActive = isActive == -1 ? t.IsActive : isActive });
                    }
                    info.Status = status>0? ErrorStatus.S0001: (count>0? ErrorStatus.S0002:ErrorStatus.S4035);
                }
                var model = new OperateLog();
                model.UserId = operUserId;
                model.RoleName = roleName;
                model.MenuName = "云控管理---手机管理---批量修改";
                model.OldTableName = "Mobile";
                model.OldBussnessId = operUserId.ToString();
                model.OperateDesc = "批量修改手机信息";
                PublicBusiness.AddOperateLog(model);
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.MobileMgt.UpdateMobileInfo 批量修改手机信息Error：", ex);
            }
            return info;
        }
        #endregion

        #region 删除手机
        /// <summary>
        /// 删除手机
        /// </summary>
        /// <param name="id">手机编号.</param>
        /// <param name="roleName">操作人角色名称</param>
        /// <param name="operUserId">操作人</param>
        /// <returns>ResultInfo.</returns>
        /// <remarks>add by dingyong,2017-05-15 16:46:54 </remarks>
        public ResultInfo DelMobile(Guid id, string roleName, Guid operUserId)
        {
            ResultInfo info = new ResultInfo();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    var entity = context.Mobile.SingleOrDefault(c => c.Id == id);
                    if (entity != null)
                    {
                        context.Mobile.Remove(entity);
                    }
                    info.Status = context.SaveChanges() > 0 ? ErrorStatus.S0001 : ErrorStatus.S0002;
                }
                var model = new OperateLog();
                model.UserId = operUserId;
                model.RoleName = roleName;
                model.MenuName = "云控管理---手机管理---删除";
                model.OldTableName = "Mobile";
                model.OldBussnessId = id.ToString();
                model.OperateDesc = " 删除手机";
                PublicBusiness.AddOperateLog(model);
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.MobileMgt.DelMobile 删除手机Error：", ex);
            }
            return info;
        }
        #endregion

        #region 批量删除手机
        /// <summary>
        /// 批量删除手机
        /// </summary>
        /// <param name="param">手机编号（以多个以^分隔）</param>
        /// <param name="roleName">操作人角色名称</param>
        /// <param name="operUserId">操作人编号</param>
        /// <returns></returns>
        /// <remarks>add by dingyong,2017-05-15 16:08:14 </remarks>
        public ResultInfo DelMobileInfo(string param, string roleName, Guid operUserId)
        {
            ResultInfo info = new ResultInfo();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    //删除选中手机
                    var paraArr = param.Split('^');
                    List<Guid> list = new List<Guid>();
                    foreach (var item in paraArr)
                    {
                        list.Add(Guid.Parse(item));
                    }
                    info.Status = context.Mobile.Where(t => list.Contains(t.Id)).Delete() > 0 ? ErrorStatus.S0001 : ErrorStatus.S0002;
                }
                var model = new OperateLog();
                model.UserId = operUserId;
                model.RoleName = roleName;
                model.MenuName = "云控管理---手机管理---批量删除";
                model.OldTableName = "Mobile";
                model.OldBussnessId = operUserId.ToString();
                model.OperateDesc = "批量删除手机";
                PublicBusiness.AddOperateLog(model);
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.MobileMgt.DelMobileInfo 批量删除手机Error：", ex);
            }
            return info;
        }
        #endregion

        #region 初始化实时手机信息
        /// <summary>
        /// 初始化实时手机信息
        /// </summary>
        /// <param name="mobileId">手机编号</param>
        /// <returns></returns>
        public Tuple<Guid,Guid> initRealControl(Guid mobileId,Guid operUserId, string roleName)
        {
            Tuple<Guid, Guid> info = new Tuple<Guid, Guid>(Guid.Empty,Guid.Empty);
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    var entity = context.RealtimeControl.Where(m=>m.MobileId== mobileId).FirstOrDefault();
                    if (entity != null)
                    {
                        entity.Flag = Guid.NewGuid();
                        entity.IsUsed = 0;
                        entity.Shootme = "";
                        entity.InputUser = operUserId;
                        entity.InsertTime = DateTime.Now;
                        info= new Tuple<Guid, Guid>(entity.Id,entity.Flag.Value);
                    }
                    else
                    {
                        var control = new RealtimeControl();
                        control.Id = Guid.NewGuid();
                        control.MobileId = mobileId;
                        control.Flag = Guid.NewGuid();
                        control.IsUsed = 0;
                        control.Shootme = "";
                        control.InputUser = operUserId;
                        control.InsertTime = DateTime.Now;
                        info = new Tuple<Guid, Guid>(control.Id, control.Flag.Value);
                        context.RealtimeControl.Add(control);
                    }
                  context.SaveChanges();
                }
                var model = new OperateLog();
                model.UserId = operUserId;
                model.RoleName = roleName;
                model.MenuName = "云控管理---手机管理---实时控制";
                model.OldTableName = "RealtimeControl";
                model.OldBussnessId = mobileId.ToString();
                model.OperateDesc = "初始化实时手机信息";
                PublicBusiness.AddOperateLog(model);
            }
            catch (Exception ex)
            {
                info = new Tuple<Guid, Guid>(Guid.Empty, Guid.Empty);
                log.Error("WZK.Business.Admin.MobileMgt.ModifyMobile 修改手机信息Error：", ex);
            }
            return info;
        }
        #endregion

        #region 获取实时手机信息
        /// <summary>
        /// 获取实时手机信息 .      
        /// </summary>
        /// <param name="id">编号.</param>
        /// <returns>实时手机信息.</returns>
        /// <remarks>add by dingyong,2017-09-27 16:08:14 </remarks>
        public ResultSingle<RealtimeControl> GetRealtimeControl(Guid id)
        {
            ResultSingle<RealtimeControl> info = new ResultSingle<RealtimeControl>();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    info.ReturnObject = context.RealtimeControl.Find(id);
                    info.Status = ErrorStatus.S0001;
                }
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.MobileMgt.GetRealtimeControl  获取实时手机信息Error：", ex);
            }
            return info;
        }

        #endregion

    }
}
