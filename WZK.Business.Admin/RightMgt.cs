using EntityFramework.Extensions;
using WZK.Common;
using WZK.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using WZK.Business.Admin.Models;
using System.Data;

namespace WZK.Business.Admin
{
    /// <summary>
    /// 权限管理
    /// author:dingyong
    /// date:2016-07-08 16:08:14
    /// </summary>
    public class RightMgt : BusinessBase
    {
        #region 角色列表        
        /// <summary>
        /// 角色列表
        /// </summary>
        /// <param name="userId">操作人用户编号.</param>
        /// <param name="roleName">操作人角色名称.</param>
        /// <param name="name">角色名.</param>
        /// <param name="pageIndex">页码.</param>
        /// <param name="getAll">if set to <c>true</c> [get all].</param>
        /// <returns>角色列表.</returns>
        /// <remarks>add by dingyong,2016-07-08 16:08:14 </remarks>
        public ResultList<MRole> GetRoleList(Guid userId, string roleName, string name, int pageIndex, bool getAll = false)
        {
            ResultList<MRole> info = new ResultList<MRole>();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    var sql = from u in context.Role
                              join a in context.UserAdmin on u.InputUser equals a.Id
                              where (name == null || name == "") ? true : u.RoleName.Contains(name)
                              select new MRole
                              {
                                  RoleID = u.RoleID,
                                  RoleName = u.RoleName,
                                  InputUser = a.Name ?? a.UserName ?? "",
                                  InsertTime = u.InsertTime,
                              };

                    info.Count = sql.Count();
                    if (getAll)
                    {
                        info.ReturnList = sql.OrderByDescending(i => i.InsertTime).ToList();
                    }
                    else
                    {
                        info.ReturnList = sql.OrderByDescending(i => i.InsertTime).Skip((pageIndex - 1) * PageSize).Take(PageSize).ToList();
                        if (info.ReturnList.Count() == 0 && pageIndex > 1)
                            info.ReturnList = sql.OrderByDescending(i => i.InsertTime).Skip((pageIndex - 2) * PageSize).Take(PageSize).ToList();
                    }
                    info.Status = ErrorStatus.S0001;
                }
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.RightMgt.GetRoleList  获取角色列表Error：", ex);
            }
            return info;
        }
        #endregion

        #region 角色信息        
        /// <summary>
        /// 角色信息 .      
        /// </summary>
        /// <param name="userId">操作人用户编号.</param>
        /// <param name="roleName">操作人角色名称.</param>
        /// <param name="id">角色编号.</param>
        /// <returns>角色信息.</returns>
        /// <remarks>add by dingyong,2016-07-08 16:11:47 </remarks>
        public ResultSingle<Role> GetRole(Guid userId, string roleName, int id)
        {
            ResultSingle<Role> info = new ResultSingle<Role>();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    info.ReturnObject = context.Role.Find(id);
                    info.Status = ErrorStatus.S0001;
                }
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.RightMgt.GetRole  获取角色信息Error：", ex);
            }
            return info;
        }
        #endregion

        #region 新增角色        
        /// <summary>
        /// 新增角色 .
        /// </summary>
        /// <param name="roleName">操作人角色名称.</param>
        /// <param name="entity">角色信息.</param>
        /// <param name="operUserId">操作人编号.</param>
        /// <returns>ResultInfo.</returns>
        /// <remarks>add by dingyong,2016-07-08 16:46:54 </remarks>
        public ResultInfo AddRole(string roleName, Role entity, Guid operUserId)
        {
            ResultInfo info = new ResultInfo();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    //是否重名
                    if (context.Role.Any(c => c.RoleName == entity.RoleName))
                    {
                        info.Status = ErrorStatus.S4008;
                    }
                    else
                    {
                        entity.UpdateTime = DateTime.Now;
                        entity.UpdateUser = operUserId;
                        entity.InputUser = operUserId;
                        entity.OrderNo = "";
                        context.Role.Add(entity);
                        info.Status = context.SaveChanges() > 0 ? ErrorStatus.S0001 : ErrorStatus.S0002;
                    }
                }
                var model = new OperateLog();
                model.UserId = operUserId;
                model.RoleName = roleName;
                model.MenuName = "权限管理---角色管理---新增---保存";
                model.OldTableName = "Role";
                model.OldBussnessId = entity.RoleID.ToString();
                model.OperateDesc = "新增角色信息";
                PublicBusiness.AddOperateLog(model);
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.RightMgt.AddRole  新增角色信息Error：", ex);
            }
            return info;
        }
        #endregion

        #region 修改角色信息        
        /// <summary>
        /// 修改角色信息.
        /// </summary>
        /// <param name="roleName">操作人角色名称.</param>
        /// <param name="entity">角色信息.</param>
        /// <param name="operUserId">操作人编号.</param>
        /// <returns>ResultInfo.</returns>
        /// <remarks>add by dingyong,2016-07-08 17:38:27 </remarks>
        public ResultInfo ModifyRole(string roleName, Role entity, Guid operUserId)
        {
            ResultInfo info = new ResultInfo();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    var role = context.Role.Find(entity.RoleID);
                    //是否重名
                    if (context.Role.Any(c => c.RoleName == entity.RoleName && c.RoleID != entity.RoleID))
                    {
                        info.Status = ErrorStatus.S4008;
                    }
                    else
                    {
                        role.UpdateTime = DateTime.Now;
                        role.UpdateUser = operUserId;
                        role.RoleName = entity.RoleName;

                        context.SaveChanges();
                        info.Status = ErrorStatus.S0001;
                    }
                }
                var model = new OperateLog();
                model.UserId = operUserId;
                model.RoleName = roleName;
                model.MenuName = "权限管理---角色管理---查看---保存";
                model.OldTableName = "Role";
                model.OldBussnessId = entity.RoleID.ToString();
                model.OperateDesc = "修改角色信息";
                PublicBusiness.AddOperateLog(model);
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.RightMgt.ModifyRole  修改角色信息Error：", ex);
            }
            return info;
        }
        #endregion

        #region 删除角色
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleId">角色Id.</param>
        /// <param name="roleName">操作人角色名称</param>
        /// <param name="operUserId">操作人</param>
        /// <returns>ResultInfo.</returns>
        /// <remarks>add by dingyong,2016-09-13 18:47:55 </remarks>
        public ResultInfo DelRole(int roleId, string roleName, Guid operUserId)
        {
            ResultInfo info = new ResultInfo();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    var role = context.Role.SingleOrDefault(c => c.RoleID == roleId);
                    if (role != null)
                    {
                        context.Role.Remove(role);
                    }

                    //移除用户角色
                    string withStr = "," + role.RoleID.ToString() + ",";
                    var roles = context.UserAdmin.Where(c => c.RoleId.Contains(withStr)).ToList();
                    foreach (var item in roles)
                    {
                        item.RoleId = item.RoleId.Replace(withStr, "");
                    }

                    info.Status = context.SaveChanges() > 0 ? ErrorStatus.S0001 : ErrorStatus.S0002;
                }
                var model = new OperateLog();
                model.UserId = operUserId;
                model.RoleName = roleName;
                model.MenuName = "权限管理---角色管理---删除";
                model.OldTableName = "Role";
                model.OldBussnessId = roleId.ToString();
                model.OperateDesc = "删除角色信息";
                PublicBusiness.AddOperateLog(model);
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.RightMgt.DelRole 删除角色信息Error：", ex);
            }
            return info;
        }
        #endregion

        #region 菜单列表       
        /// <summary>
        /// 菜单列表.
        /// </summary>
        /// <param name="userId">操作者编号.</param>
        /// <param name="roleName">操作者角色名称.</param>
        /// <param name="name">菜单名.</param>
        /// <param name="pageIndex">页码.</param>
        /// <param name="getAll">是否获取所有.</param>
        /// <param name="parentId">父级Id.</param>
        /// <returns>菜单列表.</returns>
        /// <remarks>add by dingyong,2016-09-14 13:41:36 </remarks>
        public ResultList<Mmenus> GetMenuList(Guid userId, string roleName, string name, int pageIndex, bool getAll = false, int parentId = 0)
        {
            ResultList<Mmenus> info = new ResultList<Mmenus>();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    var sql = from u in context.Menus
                              join a in context.UserAdmin
                              on u.InputUser equals a.Id into temp
                              from t in temp.DefaultIfEmpty()
                              select new Mmenus
                              {
                                  Id = u.Id,
                                  ParentId = u.ParentId,
                                  Name = u.Name,
                                  EnglishName = u.EnglishName,
                                  Url = u.Url,
                                  OrderNo = u.OrderNo,
                                  InputUser = t.Name ?? t.UserName ?? "",
                                  InsertTime = u.InsertTime,
                                  UpdateUser = u.UpdateUser,
                                  UpdateTime = u.UpdateTime,
                              };
                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        sql = sql.Where(t => t.Name.Contains(name));
                    }
                    if (parentId > 0)
                    {
                        sql = sql.Where(c => c.ParentId == parentId);
                    }
                    info.Count = sql.Count();
                    if (getAll)
                    {
                        info.ReturnList = sql.OrderBy(t => t.OrderNo).ToList();
                    }
                    else
                    {
                        info.ReturnList = sql.OrderBy(i => i.OrderNo).Skip((pageIndex - 1) * PageSize).Take(PageSize).ToList();
                        if (info.ReturnList.Count == 0 && pageIndex > 1)
                            info.ReturnList = sql.OrderBy(i => i.OrderNo).Skip((pageIndex - 2) * PageSize).Take(PageSize).ToList();
                    }

                    info.Status = ErrorStatus.S0001;
                }
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.RightMgt.GetMenuList  菜单列表Error：", ex);
            }
            return info;
        }
        #endregion

        #region 菜单信息
        /// <summary>
        /// 菜单信息.
        /// </summary>
        /// <param name="userId">操作人编号.</param>
        /// <param name="roleName">操作人角色名称.</param>
        /// <param name="id">菜单编号.</param> 
        /// <returns>菜单信息.</returns>
        /// <remarks>add by dingyong,2016-11-28 15:04:00 </remarks>
        public ResultSingle<Menus> GetMenu(Guid userId, string roleName, int id)
        {
            ResultSingle<Menus> info = new ResultSingle<Menus>();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    info.ReturnObject = context.Menus.Find(id);
                    info.Status = ErrorStatus.S0001;
                }

            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.RightMgt.GetMenu  获取菜单信息Error：", ex);
            }
            return info;
        }
        #endregion

        #region 查询子级菜单列表       
        /// <summary>
        ///查询子级菜单列表  .
        /// </summary>
        /// <param name="parentId">父级id.</param>
        /// <returns>查询子级菜单列表  .</returns>
        /// <remarks>add by dingyong,2016-07-08 18:06:08 </remarks>
        public ResultList<Menus> GetMenuList( int parentId)
        {
            ResultList<Menus> info = new ResultList<Menus>();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    var sql = from u in context.Menus
                              where u.ParentId == parentId
                              select u;

                    info.Count = sql.Count();
                    info.ReturnList = sql.OrderBy(i => i.OrderNo).ToList();
                    info.Status = ErrorStatus.S0001;
                }
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.RightMgt.GetMenuList 查询子级菜单列表Error：", ex);
            }
            return info;
        }
        #endregion

        #region 查询同一级别的菜单数据        
        /// <summary>
        /// 查询同一级别的菜单数据.
        /// </summary>
        /// <param name="userId">操作人编号.</param>
        /// <param name="roleName">操作人角色.</param>
        /// <param name="id">菜单id.</param>
        /// <returns>同一级别的菜单数据.</returns>
        /// <remarks>add by dingyong,2016-07-08 18:09:00 </remarks>
        public ResultList<Menus> GetSiblingMenuList(Guid userId, string roleName, int id)
        {
            ResultList<Menus> info = new ResultList<Menus>();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    var sql = from u in context.Menus
                              join u1 in context.Menus on u.ParentId equals u1.ParentId
                              where u1.Id == id
                              select u;

                    info.Count = sql.Count();
                    info.ReturnList = sql.OrderBy(i => i.OrderNo).ToList();
                    info.Status = ErrorStatus.S0001;
                }
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.RightMgt.GetSiblingMenuList 查询同一级别的菜单数据Error：", ex);
            }
            return info;
        }
        #endregion

        #region 新增菜单        
        /// <summary>
        /// 新增菜单.
        /// </summary>
        /// <param name="roleName">操作者角色名称.</param>
        /// <param name="entity">菜单信息.</param>
        /// <param name="operUserId">操作者编号.</param>
        /// <returns>ResultInfo.</returns>
        /// <remarks>add by dingyong,2016-07-08 18:20:46 </remarks>
        public ResultInfo AddMenu(string roleName, Menus entity, Guid operUserId)
        {
            ResultInfo info = new ResultInfo();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    //是否重名
                    if (context.Menus.Any(c => c.Name == entity.Name && c.ParentId == entity.ParentId))
                    {
                        info.Status = ErrorStatus.S4009;
                    }
                    else if (context.Menus.Any(c => c.EnglishName == entity.EnglishName && c.ParentId == entity.ParentId))
                    {
                        info.Status = ErrorStatus.S4010;
                    }
                    else if (context.Menus.Any(c => c.OrderNo == entity.OrderNo && c.ParentId == entity.ParentId))
                    {
                        info.Status = ErrorStatus.S4012;
                    }
                    else
                    {
                        entity.UpdateTime = DateTime.Now;
                        entity.UpdateUser = operUserId;
                        entity.InputUser = operUserId;
                        #region 计算OrderNo
                        if (string.IsNullOrWhiteSpace(entity.OrderNo))
                        {
                            var maxOrderNo = context.Menus.Where(m => m.ParentId == entity.ParentId).Max(m => m.OrderNo);
                            if (maxOrderNo == null)
                            {
                                if (entity.ParentId == 0)
                                {
                                    maxOrderNo = "010";
                                }
                                else
                                {
                                    maxOrderNo = context.Menus.Single(m => m.Id == entity.ParentId).OrderNo + "010";
                                }
                            }
                            else
                            {
                                maxOrderNo = maxOrderNo.Substring(0, maxOrderNo.Length - 3) + (Convert.ToInt32(maxOrderNo.Substring(maxOrderNo.Length - 3, 3)) + 10).ToString().PadLeft(3, '0');
                            }
                            entity.OrderNo = maxOrderNo;
                        }
                        else
                        {
                            if (entity.ParentId != 0)
                            {
                                var parent = context.Menus.Single(c => c.Id == entity.ParentId);
                                if (parent != null)
                                {
                                    entity.OrderNo = parent.OrderNo + entity.OrderNo;
                                }
                            }
                        }
                        #endregion
                        context.Menus.Add(entity);
                        info.Status = context.SaveChanges() > 0 ? ErrorStatus.S0001 : ErrorStatus.S0002;
                    }
                }
                var model = new OperateLog();
                model.UserId = operUserId;
                model.RoleName = roleName;
                model.MenuName = "权限管理---菜单管理---新增---保存";
                model.OldTableName = "Menus";
                model.OldBussnessId = entity.Id.ToString();
                model.OperateDesc = "新增菜单";
                PublicBusiness.AddOperateLog(model);
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.RightMgt.AddMenu 新增菜单Error：", ex);
            }
            return info;
        }
        #endregion

        #region 修改菜单信息       
        /// <summary>
        /// 修改菜单信息
        /// </summary>
        /// <param name="roleName">操作人角色名称.</param>
        /// <param name="entity">菜单信息.</param>
        /// <param name="operUserId">操作人编号.</param>
        /// <returns>ResultInfo.</returns>
        /// <remarks>add by dingyong,2016-07-08 18:26:23 </remarks>
        public ResultInfo ModifyMenu(string roleName, Menus entity, Guid operUserId)
        {
            ResultInfo info = new ResultInfo();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    bool isUpdateChild = false;
                    string oldCode = string.Empty;
                    var menu = context.Menus.Find(entity.Id);
                    //是否重名
                    if (context.Menus.Any(c => c.Name == entity.Name && c.ParentId == entity.ParentId && c.Id != entity.Id))
                    {
                        info.Status = ErrorStatus.S4009;
                    }
                    else if (context.Menus.Any(c => c.EnglishName == entity.EnglishName && c.ParentId == entity.ParentId && c.Id != entity.Id))
                    {
                        info.Status = ErrorStatus.S4010;
                    }
                    else if (context.Menus.Any(c => c.OrderNo == entity.OrderNo && c.ParentId == entity.ParentId && c.Id != entity.Id))
                    {
                        info.Status = ErrorStatus.S4012;
                    }
                    else
                    {
                        //是否修改了父级
                        if (entity.ParentId != menu.ParentId)
                        {
                            #region 计算OrderNo  
                            var maxOrderNo = context.Menus.Where(m => m.ParentId == entity.ParentId).Max(m => m.OrderNo);
                            if (maxOrderNo == null)
                            {
                                if (entity.ParentId == 0)
                                {
                                    maxOrderNo = "010";
                                }
                                else
                                {
                                    maxOrderNo = context.Menus.Single(m => m.Id == entity.ParentId).OrderNo + "010";
                                }
                            }
                            else
                            {
                                maxOrderNo = maxOrderNo.Substring(0, maxOrderNo.Length - 3) + (Convert.ToInt32(maxOrderNo.Substring(maxOrderNo.Length - 3, 3)) + 10).ToString().PadLeft(3, '0');
                            }
                            #endregion
                            entity.OrderNo = maxOrderNo;
                        }
                        //排序号是否有变
                        if (entity.OrderNo != menu.OrderNo)
                        {
                            oldCode = menu.OrderNo;

                            menu.OrderNo = entity.OrderNo;
                            isUpdateChild = true;
                        }

                        menu.UpdateTime = DateTime.Now;
                        menu.UpdateUser = operUserId;
                        menu.Name = entity.Name;
                        menu.EnglishName = entity.EnglishName;
                        menu.ParentId = entity.ParentId;
                        menu.Url = entity.Url;
                        menu.MenuType = entity.MenuType;
                        info.Status = context.SaveChanges() > 0 ? ErrorStatus.S0001 : ErrorStatus.S0002;
                        if (info.IsPass && isUpdateChild)
                        {
                            //修改子项排序号
                            context.Menus.Where(m => m.ParentId == entity.Id)
                                   .Update(m => new Menus() { OrderNo = m.OrderNo.Replace(oldCode, menu.OrderNo) });

                            context.MenuButton.Where(c => c.MenuId == entity.Id)
                                   .Update(m => new MenuButton() { OrderNo = m.OrderNo.Replace(oldCode, menu.OrderNo) });
                        }
                    }
                }
                var model = new OperateLog();
                model.UserId = operUserId;
                model.RoleName = roleName;
                model.MenuName = "权限管理---菜单管理---查看---保存";
                model.OldTableName = "Menus";
                model.OldBussnessId = entity.Id.ToString();
                model.OperateDesc = "修改菜单";
                PublicBusiness.AddOperateLog(model);
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.RightMgt.ModifyMenu 修改菜单信息Error：", ex);
            }
            return info;
        }
        #endregion

        #region 删除菜单
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="menuId">菜单Id.</param>
        /// <param name="roleName">操作人角色名称</param>
        /// <param name="operUserId">操作人</param>
        /// <returns>ResultInfo.</returns>
        /// <remarks>add by dingyong,2016-09-13 15:02:14 </remarks>
        public ResultInfo DelMenu(int menuId, string roleName, Guid operUserId)
        {
            ResultInfo info = new ResultInfo();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    var menu = context.Menus.SingleOrDefault(c => c.Id == menuId);
                    if (menu != null)
                    {
                        context.Menus.Remove(menu);
                    }

                    var menuButtons = context.MenuButton.Where(c => c.MenuId == menuId).ToList();
                    string withStr = "";
                    foreach (var button in menuButtons)
                    {
                        //移除菜单按钮
                        context.MenuButton.Remove(button);

                        //移除用户权限
                        withStr = "," + button.Id.ToString() + ",";
                        var roles = context.Role.Where(c => c.ButtonIds.Contains(withStr)).ToList();
                        foreach (var item in roles)
                        {
                            item.ButtonIds = item.ButtonIds.Replace(withStr, ",");
                        }
                    }

                    info.Status = context.SaveChanges() > 0 ? ErrorStatus.S0001 : ErrorStatus.S0002;

                    if (info.IsPass)
                    {
                        //移除子菜单
                        context.Menus.Where(c => c.ParentId == menuId).Delete(); //未加入事物中，所以放到最后执行
                    }
                }
                var model = new OperateLog();
                model.UserId = operUserId;
                model.RoleName = roleName;
                model.MenuName = "权限管理---菜单管理---删除";
                model.OldTableName = "Menus";
                model.OldBussnessId = menuId.ToString();
                model.OperateDesc = "删除菜单";
                PublicBusiness.AddOperateLog(model);
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.RightMgt.DelMenu 删除菜单Error：", ex);
            }
            return info;
        }
        #endregion

        #region 获取排序
        /// <summary>
        /// 获取排序
        /// </summary>
        /// <param name="menuId">菜单Id.</param>
        /// <param name="type">类型 1.菜单按钮；2-菜单</param>
        /// <returns>ResultSingle&lt;System.String&gt;.</returns>
        /// <remarks>add by dingyong,2016-09-13 08:32:40 </remarks>
        public ResultSingle<string> GetOrderNo(int menuId, int type)
        {
            ResultSingle<string> info = new ResultSingle<string>();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    Menus menus = null;
                    int maxOrderNo = 0;
                    switch (type)
                    {
                        case 1:
                            menus = context.Menus.SingleOrDefault(c => c.Id == menuId);
                            if (menus == null)
                            {
                                info.Status = ErrorStatus.S4027;
                                return info;
                            }
                            var maxMenuButton = context.MenuButton.Where(c => c.MenuId == menuId).OrderByDescending(c => c.OrderNo).FirstOrDefault();
                            if (maxMenuButton == null)
                                info.ReturnObject = menus.OrderNo + "010";
                            else
                            {
                                if (maxMenuButton.OrderNo.Length > 3)
                                    maxMenuButton.OrderNo = maxMenuButton.OrderNo.Substring(maxMenuButton.OrderNo.Length - 3, 3);

                                maxOrderNo = int.Parse(maxMenuButton.OrderNo);
                                info.ReturnObject = menus.OrderNo + (maxOrderNo + 10).ToString().PadLeft(3, '0');
                            }
                            break;
                        case 2:


                            if (menuId != 0)
                            {
                                menus = context.Menus.SingleOrDefault(c => c.Id == menuId);
                                if (menus == null)
                                {
                                    info.Status = ErrorStatus.S4027;
                                    return info;
                                }
                            }
                            var maxMenu = context.Menus.Where(c => c.ParentId == menuId).OrderByDescending(c => c.OrderNo).FirstOrDefault();
                            if (maxMenu == null)
                                info.ReturnObject = menus.OrderNo + "010";
                            else
                            {
                                if (maxMenu.OrderNo.Length > 3)
                                    maxMenu.OrderNo = maxMenu.OrderNo.Substring(maxMenu.OrderNo.Length - 3, 3);

                                maxOrderNo = int.Parse(maxMenu.OrderNo);
                                if (menuId != 0)
                                {
                                    if (maxMenu.Id!= menuId)
                                    {
                                        info.ReturnObject = menus.OrderNo + (maxOrderNo + 10).ToString().PadLeft(3, '0');
                                    }
                                    else
                                    {
                                        info.ReturnObject = maxMenu.OrderNo;
                                    }

                                } 
                                else
                                    info.ReturnObject = (maxOrderNo + 10).ToString().PadLeft(3, '0');

                            }
                            break;
                    }

                    info.Status = ErrorStatus.S0001;
                }

            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.RightMgt.GetOrderNo 获取排序Error：", ex);
            }
            return info;
        }
        #endregion

        #region 菜单按钮功能列表
        /// <summary>
        /// 菜单按钮功能列表
        /// </summary>
        /// <param name="menuId">菜单ID.</param>
        /// <param name="name">功能名</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="getAll">得到全部按钮</param>
        /// <returns>功能列表</returns>
        /// <remarks>add by dingyong,2016-11-28 17:23:36 </remarks>
        public ResultList<MMenuButton> GetMenuButtonList( int menuId, string name, int pageIndex, bool getAll = false)
        {
            ResultList<MMenuButton> info = new ResultList<MMenuButton>();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    var sql = from b in context.MenuButton
                              join m in context.Menus on b.MenuId equals m.Id
                              join a in context.UserAdmin on b.InputUser equals a.Id into temp
                              from t in temp.DefaultIfEmpty()
                              where ((name == null || name == "") ? true : b.Name.Contains(name))
                              select new MMenuButton
                              {
                                  Action = b.Action,
                                  Controller = b.Controller,
                                  Id = b.Id,
                                  InsertTime = b.InsertTime,
                                  MenuId = m.Id,
                                  MenuName = m.Name,
                                  Name = b.Name,
                                  OrderNo = b.OrderNo,
                                  InputUser = t.Name ?? t.UserName ?? "",
                                  ButtonType = b.ButtonType
                              };
                    if (menuId > 0)
                    {
                        sql = sql.Where(c => c.MenuId == menuId);
                    }

                    info.Count = sql.Count();
                    if (getAll)
                    {
                        info.ReturnList = sql.OrderBy(t => t.OrderNo).ToList();
                    }
                    else
                    {
                        info.ReturnList = sql.OrderBy(i => i.OrderNo).Skip((pageIndex - 1) * PageSize).Take(PageSize).ToList();
                        if (info.ReturnList.Count == 0 && pageIndex > 1)
                            info.ReturnList = sql.OrderBy(i => i.OrderNo).Skip((pageIndex - 1) * PageSize).Take(PageSize).ToList();
                    }
                    info.Status = ErrorStatus.S0001;
                }
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.RightMgt.GetMenuButtonList 获取功能列表Error：", ex);
            }
            return info;
        }
        #endregion

        #region 菜单按钮信息
        /// <summary>
        /// 菜单按钮信息
        /// </summary>
        /// <param name="userId">操作者编号</param>
        /// <param name="roleName">操作者角色名称</param>
        /// <param name="id">功能ID</param>
        /// <returns>功能信息</returns>
        /// <remarks>author:dingyong date:2016-07-08</remarks>
        public ResultSingle<MMenuButton> GetMenuButton(Guid userId, string roleName, int id)
        {
            ResultSingle<MMenuButton> info = new ResultSingle<MMenuButton>();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    info.ReturnObject = (from b in context.MenuButton
                                         join m in context.Menus on b.MenuId equals m.Id
                                         where b.Id == id
                                         select new MMenuButton
                                         {
                                             Action = b.Action,
                                             Controller = b.Controller,
                                             Id = b.Id,
                                             InsertTime = b.InsertTime,
                                             MenuId = m.Id,
                                             MenuName = m.Name,
                                             Name = b.Name,
                                             OrderNo = b.OrderNo,
                                             ButtonType = b.ButtonType
                                         }).SingleOrDefault();
                    info.Status = ErrorStatus.S0001;
                }
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.RightMgt.GetMenuButton 获取功能信息Error：", ex);
            }

            return info;
        }
        #endregion

        #region 新增菜单按钮
        /// <summary>
        /// 新增菜单按钮
        /// </summary>
        /// <param name="roleName">操作人角色名称</param>
        /// <param name="entity">按钮信息</param>
        /// <param name="operUserId">操作人</param>
        /// <returns></returns>
        /// <remarks>author:dingyong date:2016-07-08</remarks>
        public ResultInfo AddMenuButton(string roleName, MenuButton entity, Guid operUserId)
        {
            ResultInfo info = new ResultInfo();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    //是否重名
                    if (context.MenuButton.Any(c => (c.Name == entity.Name || (c.Controller == entity.Controller && c.Action == entity.Action)) && c.MenuId == entity.MenuId))
                    {
                        info.Status = ErrorStatus.S4011;
                    }
                    else
                    {
                        var menu = context.Menus.Find(entity.MenuId);
                        if (menu != null)
                            entity.OrderNo = menu.OrderNo + entity.OrderNo;
                        else
                            entity.OrderNo = entity.OrderNo;

                        entity.UpdateTime = DateTime.Now;
                        entity.UpdateUser = operUserId;
                        entity.InputUser = operUserId;
                        context.MenuButton.Add(entity);
                        info.Status = context.SaveChanges() > 0 ? ErrorStatus.S0001 : ErrorStatus.S0002;
                    }
                }
                var model = new OperateLog();
                model.UserId = operUserId;
                model.RoleName = roleName;
                model.MenuName = "权限管理---菜单按钮管理---新增---保存";
                model.OldTableName = "MenuButton";
                model.OldBussnessId = entity.Id.ToString();
                model.OperateDesc = "新增功能信息";
                PublicBusiness.AddOperateLog(model);
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.RightMgt.AddMenuButton 新增功能Error：", ex);
            }
            return info;
        }
        #endregion

        #region 删除菜单按钮
        /// <summary>
        /// 删除菜单按钮
        /// </summary>
        /// <param name="menuButtonId">菜单按钮Id.</param>
        /// <param name="roleName">操作人角色名称</param>
        /// <param name="operUserId">操作人</param>
        /// <returns>ResultInfo.</returns>
        /// <remarks>add by dingyong,2016-09-13 11:39:42 </remarks>
        public ResultInfo DelMenuButton(int menuButtonId, string roleName, Guid operUserId)
        {
            ResultInfo info = new ResultInfo();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    var menuButton = context.MenuButton.SingleOrDefault(c => c.Id == menuButtonId);
                    if (menuButton != null)
                    {
                        context.MenuButton.Remove(menuButton);
                    }

                    //移除用户权限
                    string withStr = "," + menuButton.Id.ToString() + ",";
                    var roles = context.Role.Where(c => c.ButtonIds.Contains(withStr)).ToList();

                    foreach (var item in roles)
                    {
                        item.ButtonIds = item.ButtonIds.Replace(withStr, ",");
                    }

                    info.Status = context.SaveChanges() > 0 ? ErrorStatus.S0001 : ErrorStatus.S0002;
                }
                var model = new OperateLog();
                model.UserId = operUserId;
                model.RoleName = roleName;
                model.MenuName = "权限管理---菜单按钮管理---删除";
                model.OldTableName = "MenuButton";
                model.OldBussnessId = menuButtonId.ToString();
                model.OperateDesc = "删除菜单按钮";
                PublicBusiness.AddOperateLog(model);
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.RightMgt.DelMenuButton 删除菜单按钮Error：", ex);
            }
            return info;
        }
        #endregion

        #region 修改功能信息
        /// <summary>
        /// 修改功能信息
        /// </summary>
        /// <param name="roleName">操作人角色名称</param>
        /// <param name="entity">功能信息</param>
        /// <param name="operUserId">操作人</param>
        /// <returns></returns>
        /// <remarks>author:dingyong date:2016-07-08</remarks>
        public ResultInfo ModifyMenuButton(string roleName, MenuButton entity, Guid operUserId)
        {
            ResultInfo info = new ResultInfo();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    var MenuButton = context.MenuButton.Find(entity.Id);
                    //是否重名
                    if (context.MenuButton.Any(c => (c.Name == entity.Name || (c.Controller == entity.Controller && c.Action == entity.Action)) && c.MenuId == entity.MenuId && c.Id != entity.Id))
                    {
                        info.Status = ErrorStatus.S4011;
                    }
                    else
                    {
                        var menu = context.Menus.Find(MenuButton.MenuId);
                        if (menu != null)
                        {
                            MenuButton.OrderNo = menu.OrderNo + entity.OrderNo;
                            if (context.MenuButton.Any(c => c.OrderNo == MenuButton.OrderNo && c.MenuId == entity.MenuId && c.Id != entity.Id))
                            {
                                info.Status = ErrorStatus.S4012;
                                return info;
                            }
                        }
                        else
                            MenuButton.OrderNo = entity.OrderNo;

                        MenuButton.UpdateTime = DateTime.Now;
                        MenuButton.UpdateUser = operUserId;
                        MenuButton.Name = entity.Name;
                        MenuButton.Controller = entity.Controller;

                        MenuButton.Action = entity.Action;
                        MenuButton.MenuId = entity.MenuId;
                        MenuButton.ButtonType = entity.ButtonType;
                        context.SaveChanges();
                        info.Status = ErrorStatus.S0001;
                    }
                }
                var model = new OperateLog();
                model.UserId = operUserId;
                model.RoleName = roleName;
                model.MenuName = "权限管理---菜单按钮管理---查看---保存";
                model.OldTableName = "MenuButton";
                model.OldBussnessId = entity.Id.ToString();
                model.OperateDesc = "修改功能信息";
                PublicBusiness.AddOperateLog(model);
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.RightMgt.ModifyMenuButton 修改功能信息Error：", ex);
            }
            return info;
        }
        #endregion

        #region 设置角色功能
        /// <summary>
        /// 设置角色功能
        /// </summary>
        /// <param name="roleName">操作人角色名称</param>
        /// <param name="roleId">角色ID</param>
        /// <param name="actions">功能集</param>
        /// <param name="operUserId">操作人</param>
        /// <returns></returns>
        /// <remarks>author:dingyong date:2016-07-08</remarks>
        public ResultInfo SetRoleRight(string roleName, int roleId, string actions, Guid operUserId)
        {
            ResultInfo info = new ResultInfo();
            try
            {
                if (!actions.StartsWith(","))
                    actions = "," + actions;
                if (!actions.EndsWith(","))
                    actions = actions + ",";

                using (WZKEntities context = new WZKEntities())
                {
                    var role = context.Role.Find(roleId);
                    role.ButtonIds = actions;
                    role.UpdateTime = DateTime.Now;
                    role.UpdateUser = operUserId;
                    info.Status = context.SaveChanges() > 0 ? ErrorStatus.S0001 : ErrorStatus.S0002;
                }
                var model = new OperateLog();
                model.UserId = operUserId;
                model.RoleName = roleName;
                model.MenuName = "权限管理---角色管理---保存";
                model.OldTableName = "Role";
                model.OldBussnessId = roleId.ToString();
                model.OperateDesc = "设置角色功能";
                PublicBusiness.AddOperateLog(model);
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.RightMgt.SetRoleRight 设置角色功能Error：", ex);
            }
            return info;
        }
        #endregion

        #region 设置用户角色
        /// <summary>
        /// 设置用户角色
        /// </summary>
        /// <param name="roleName">操作人角色名称</param>
        /// <param name="userId">用户ID</param>
        /// <param name="roles">角色ID集</param>
        /// <param name="operUserId">操作人</param>
        /// <returns></returns>
        /// <remarks>author:dingyong date:2016-07-08</remarks>
        public ResultInfo SetUserRole(string roleName, Guid userId, string roles, Guid operUserId)
        {
            ResultInfo info = new ResultInfo();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    var user = context.UserAdmin.Find(userId);
                    if (user != null)
                    {
                        if (!string.IsNullOrWhiteSpace(roles))
                        {
                            if (!roles.StartsWith(","))
                                roles = "," + roles;
                            if (!roles.EndsWith(","))
                                roles += ",";
                        }
                        else
                        {
                            roles = "";
                        }
                        user.RoleId = roles;
                        user.UpdateTime = DateTime.Now;
                        user.UpdateUser = operUserId;
                        info.Status = context.SaveChanges() > 0 ? ErrorStatus.S0001 : ErrorStatus.S0002;
                    }
                }
                var model = new OperateLog();
                model.UserId = operUserId;
                model.RoleName = roleName;
                model.MenuName = "权限管理---角色管理---保存";
                model.OldTableName = "UserAdmin";
                model.OldBussnessId = userId.ToString();
                model.OperateDesc = "设置用户角色";
                PublicBusiness.AddOperateLog(model);
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.RightMgt.SetUserRole 设置用户角色Error：", ex);
            }
            return info;
        }
        #endregion

        #region 获取用户的所有可用的功能
        /// <summary>
        /// 获取用户的所有可用的功能
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        /// <remarks>author:dingyong date:2016-07-08</remarks>
        public ResultList<MenuButton> GetRightActions(Guid userId)
        {
            ResultList<MenuButton> info = new ResultList<MenuButton>();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    var user = new AdminUserMgt().GetUserAdmin(userId);
                    var roles = user.ReturnObject.RoleId.Split(',');
                    var buttons = context.Role.Where(r => roles.Contains(r.RoleID.ToString()) && (r.ButtonIds != null && r.ButtonIds != "")).Select(b => b.ButtonIds).ToList();
                    var temp = new List<int>();
                    foreach (var item in buttons)
                    {
                        temp.AddRange(item.Trim(',').Split(',').Select(t => Convert.ToInt32(t)));
                    }
                    temp = temp.Distinct().ToList();
                    info.ReturnList = context.MenuButton.Where(b => temp.Contains(b.Id)).ToList();
                    info.Status = ErrorStatus.S0001;
                }
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.RightMgt.GetRightActions 获取用户的所有可用的功能Error：", ex);
            }
            return info;
        }
        /// <summary>
        /// 内部调用获取用户的有权限的按钮列表
        /// </summary>
        /// <param name="userId">管理后台用户编号</param>
        /// <param name="context"></param>
        /// <returns></returns>
        public List<MenuButton> GetRightActions(Guid userId, WZKEntities context)
        {
            try
            {
                List<MenuButton> info = new List<MenuButton>();
                var roles = new AdminUserMgt().GetUserAdmin(userId).ReturnObject.RoleId.Split(',');
                var buttons = context.Role.Where(r => roles.Contains(r.RoleID.ToString()) && (r.ButtonIds != null && r.ButtonIds != "")).Select(b => b.ButtonIds).ToList();
                var temp = new List<int>();
                foreach (var item in buttons)
                {
                    temp.AddRange(item.Trim(',').Split(',').Select(t => Convert.ToInt32(t)));
                }
                temp = temp.Distinct().ToList();
                return context.MenuButton.Where(b => temp.Contains(b.Id)).ToList();
            }
            catch (Exception ex)
            {
                log.Error("WZK.Business.Admin.RightMgt.GetRightActions 内部调用获取用户的有权限的按钮列表Error：", ex);
                return null;
            }
        }
        #endregion

        #region 获取用户的所有可用菜单
        /// <summary>
        /// 获取用户的所有可用菜单
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        /// <remarks>author:dingyong date:2016-07-08</remarks>
        public ResultList<Menus> GetRightMenus(Guid userId)
        {
            ResultList<Menus> info = new ResultList<Menus>();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    info.ReturnList = new List<Menus>();
                    var menus = GetRightActions(userId, context).Select(a => a.MenuId);

                    var temp = context.Menus.Where(b => menus.Contains(b.Id)).ToList();
                    info.ReturnList.AddRange(temp);
                    while (temp.Any(m => m.ParentId != 0))
                    {
                        menus = temp.Select(a => Convert.ToInt32(a.ParentId));
                        temp = context.Menus.Where(b => menus.Contains(b.Id)).ToList();
                        info.ReturnList.AddRange(temp);
                    }
                    info.ReturnList = info.ReturnList.OrderBy(i => i.OrderNo).ToList();
                    info.Status = ErrorStatus.S0001;
                }
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.RightMgt.GetRightMenus 获取用户的所有可用菜单Error：", ex);
            }
            return info;
        }
        /// <summary>
        /// 获取管理用户可用菜单列表（包含菜单归属的控制器名和Action）
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ResultList<MMenus> GetRightMMenus(Guid userId)
        {
            ResultList<MMenus> info = new ResultList<MMenus>();
            Dictionary<int, string> dicMenuControllorAction = new Dictionary<int, string>();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    List<Menus> mmenusList = context.Menus.ToList();  //获取所有菜单列表
                    info.ReturnList = new List<MMenus>();
                    var menuBottoms = GetRightActions(userId, context);    //获取用户能操作的所有按钮信息
                    List<int> menus = new List<int>();
                    foreach (var item in menuBottoms)
                    {
                        string[] arrController = item.Controller.Split(new char[] { ',', '，' }, StringSplitOptions.RemoveEmptyEntries);
                        string[] arrAction = item.Action.Split(new char[] { ',', '，' }, StringSplitOptions.RemoveEmptyEntries);
                        for (int i = 0; i < arrController.Length; i++)
                        {
                            //遍历权限按钮,把按钮的操作url存起来
                            string val = string.Format("{0}/{1},", arrController[i], arrAction[i]);
                            if ("index".Equals(item.Action, StringComparison.CurrentCultureIgnoreCase))
                            {
                                val += string.Format("{0},", arrController[i]);
                            }
                            val = val.ToLower();
                            //每个按钮对应的url,存储到dicMenuControllorAction里(出现一个多个按钮对应一个url的时候,url里的val=a,b,c)
                            if (dicMenuControllorAction.ContainsKey(item.MenuId))
                            {
                                if (!dicMenuControllorAction[item.MenuId].Contains(val))
                                {
                                    dicMenuControllorAction[item.MenuId] += val;
                                }
                            }
                            else
                            {
                                dicMenuControllorAction.Add(item.MenuId, val);
                                menus.Add(item.MenuId);    //添加url对应的menuId
                            }
                        }
                    }
                    var temp = mmenusList.Where(b => menus.Contains(b.Id)).ToList();
                    foreach (var tempModel in temp)
                    {
                        //遍历当前分类级别添加进去
                        var model = mmenusList.FirstOrDefault(p => p.Id == tempModel.Id);
                        var MMenus = new MMenus(model);
                        if (dicMenuControllorAction.ContainsKey(model.Id))
                        {
                            MMenus.ControllerAction = dicMenuControllorAction[model.Id];
                        }
                        if (!info.ReturnList.Select(p => p.Id).Contains(MMenus.Id))
                        {
                            info.ReturnList.Add(MMenus);
                        }
                        while (model.ParentId != 0)
                        {
                            int mmId = model.Id;
                            model = mmenusList.FirstOrDefault(p => p.Id == model.ParentId.Value);
                            var parentMenus = new MMenus(model);
                            if (dicMenuControllorAction.ContainsKey(mmId))
                            {
                                parentMenus.ControllerAction = dicMenuControllorAction[mmId];
                            }
                            if (!info.ReturnList.Select(p => p.Id).Contains(parentMenus.Id))
                            {
                                info.ReturnList.Add(parentMenus);
                            }
                        }
                    }
                    info.ReturnList = info.ReturnList.OrderBy(i => i.OrderNo).ToList();
                    info.Status = ErrorStatus.S0001;
                }
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.RightMgt.GetRightMMenus 获取管理用户可用菜单列表（包含菜单归属的控制器名和Action）Error：", ex);
            }
            return info;
        }
        #endregion        

        #region 整理菜单
        /// <summary>
        /// 整理菜单
        /// </summary>
        /// <returns>ResultInfo.</returns>
        /// <remarks>add by dingyong,2017-03-10 11:25:40 </remarks>
        public ResultInfo FinishMenu(int parentId)
        {
            ResultInfo info = new ResultInfo() { Status = ErrorStatus.S0001 };
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    int curMaxOrderNo = 0, //当前最大排序号
                        curMaxOrderNoButton = 0;//当前按钮最大排序号

                    Menus parentMenu = context.Menus.FirstOrDefault(c => c.Id == parentId);
                    List<Menus> childMenus = context.Menus.Where(c => c.ParentId == parentId).OrderBy(c => c.OrderNo).ToList();

                    if (parentMenu != null)
                    {
                        //1.循环取出符合规范的最大排序菜单
                        foreach (var item in childMenus)
                        {
                            if (item.OrderNo.Length - parentMenu.OrderNo.Length == 3 && item.OrderNo.StartsWith(parentMenu.OrderNo)) //长度符合并且前缀符合
                            {
                                if (curMaxOrderNo == 0 || int.Parse(item.OrderNo) > curMaxOrderNo)
                                {
                                    curMaxOrderNo = int.Parse(item.OrderNo);
                                }
                            }
                        }

                        //2.重新匹配或赋值新子菜单
                        foreach (var item in childMenus)
                        {
                            curMaxOrderNoButton = 0;
                            if ((item.OrderNo.Length - parentMenu.OrderNo.Length) != 3 || !item.OrderNo.StartsWith(parentMenu.OrderNo))//长度不符合或者前缀不符合，排序值重新计算
                            {
                                if (curMaxOrderNo == 0)
                                {
                                    item.OrderNo = parentMenu.OrderNo + "010";
                                }
                                else
                                {
                                    item.OrderNo = (curMaxOrderNo + 10).ToString().PadLeft(parentMenu.OrderNo.Length + 3, '0');
                                }

                                curMaxOrderNo = int.Parse(item.OrderNo);
                            }

                            #region 整理菜单按钮
                            List<MenuButton> menuButtons = context.MenuButton.Where(c => c.MenuId == item.Id).OrderBy(c => c.OrderNo).ToList();
                            //1.循环取出符合规范的最大排序菜单
                            foreach (var button in menuButtons)
                            {
                                if (button.OrderNo.Length - item.OrderNo.Length == 3 && button.OrderNo.StartsWith(item.OrderNo)) //长度符合并且前缀符合
                                {
                                    if (curMaxOrderNoButton == 0 || int.Parse(button.OrderNo) > curMaxOrderNoButton)
                                    {
                                        curMaxOrderNoButton = int.Parse(button.OrderNo);
                                    }
                                }
                            }
                            //2.重新匹配或赋值新子菜单按钮
                            foreach (var button in menuButtons)
                            {
                                if (button.OrderNo.Length - item.OrderNo.Length != 3 || !button.OrderNo.StartsWith(item.OrderNo)) //长度不符合或者前缀不符合，排序值重新计算
                                {
                                    if (curMaxOrderNoButton == 0)
                                    {
                                        button.OrderNo = item.OrderNo + "010";
                                    }
                                    else
                                    {
                                        button.OrderNo = (curMaxOrderNoButton + 10).ToString().PadLeft(item.OrderNo.Length + 3, '0');
                                    }

                                    curMaxOrderNoButton = int.Parse(button.OrderNo);
                                }
                            }
                            #endregion 
                        }
                        context.SaveChanges();
                    }

                    //递归整理子级菜单
                    foreach (var item in childMenus)
                    {
                        this.FinishMenu(item.Id);
                    }
                }
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.RightMgt.FinishMenu 整理菜单Error：", ex);
            }
            return info;
        }
        #endregion
    }
}
