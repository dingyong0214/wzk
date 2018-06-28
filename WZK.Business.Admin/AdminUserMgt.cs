using WZK.Business.Admin.Enum;
using WZK.Common;
using WZK.Common.ConfigData;
using WZK.Entity;
using EntityFramework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;

namespace WZK.Business.Admin
{
    /// <summary>
    /// 后台管理员相关业务
    /// author:dingyong
    /// date:2016-07-01
    /// </summary>
    public class AdminUserMgt : BusinessBase
    {
        #region 获取管理员数据
        /// <summary>
        /// 获取管理员数据
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="pwd">登录密码</param>
        /// <returns>管理员数据</returns>
        /// <remarks>author:dingyong date:2016-07-09</remarks>
        public ResultSingle<UserAdmin> GetUser(string name, string pwd)
        {
            ResultSingle<UserAdmin> info = new ResultSingle<UserAdmin>();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {

                    UserAdmin user = context.UserAdmin.FirstOrDefault(c => c.UserName == name);

                    if (user == null)
                    {
                        info.Status = ErrorStatus.S1014;
                    }
                    else if (user.State == (int)EUserAdminStatus.Invaild)
                    {
                        info.Status = ErrorStatus.S1006;
                    }
                    else if (user.State == (int)EUserAdminStatus.Del)
                    {
                        info.Status = ErrorStatus.S1015;
                    }
                    else
                    {
                        string key1 = user.Id.ToString().Split('-')[2];
                        string key2 = user.Id.ToString().Split('-')[3];
                        string enPwd = Encrypt.EncryptDES(pwd, key1 + key2);

                        if (!user.Password.Equals(enPwd))
                        {
                            info.Status = ErrorStatus.S1007;
                        }
                        else if (user.State != 1)
                        {
                            info.Status = ErrorStatus.S4007;
                        }
                        else
                        {
                            info.ReturnObject = user;
                            info.Status = ErrorStatus.S0001;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.AdminUserMgt.GetUser  获取管理员数据Error：", ex);
            }
            return info;
        }
        #endregion

        #region 获取用户列表
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="name">用户名</param>
        /// <returns>用户列表</returns>
        public ResultList<MUserAdmin> GetUserList(string name, int pageIndex)
        {
            ResultList<MUserAdmin> info = new ResultList<MUserAdmin>();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    var sql = from u in context.UserAdmin
                              join a in context.UserAdmin on u.InputUser equals a.Id into temp
                              from t in temp.DefaultIfEmpty()
                              where u.State != 3
                              select new
                              {
                                  u.Id,
                                  u.UserName,
                                  u.Name,
                                  u.Department,
                                  u.RegisterTime,
                                  u.State,
                                  u.RoleId,
                                  InputUser = t.UserName
                              };
                    if (string.IsNullOrWhiteSpace(name))
                    {
                        sql = sql.Where(t => t.UserName.Contains(name) || t.Name.Contains(name) || t.Department.Contains(name));
                    }
                    info.Count = sql.Count();
                    var data = sql.OrderByDescending(i => i.RegisterTime).Skip((pageIndex - 1) * PageSize).Take(PageSize).ToList();
                    if (data.Count == 0 && pageIndex > 1)
                    {
                        data = sql.OrderByDescending(i => i.RegisterTime).Skip((pageIndex - 2) * PageSize).Take(PageSize).ToList();
                    }
                    var list = new List<MUserAdmin>();
                    foreach (var item in data)
                    {
                        var model = new MUserAdmin();
                        model.Id = item.Id;
                        model.UserName = item.UserName;
                        model.Name = item.Name;
                        model.Department = item.Department;
                        model.RegisterTime = item.RegisterTime;
                        model.State = item.State;
                        model.RoleName = GetUserRoleName(item.RoleId);
                        model.InputUser = item.InputUser;
                        list.Add(model);
                    }
                    info.ReturnList = list;
                    info.Status = ErrorStatus.S0001;
                }
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.AdminUserMgt.GetUserList  获取用户列表Error：", ex);
            }
            return info;
        }
        #endregion

        #region 获取用户信息
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns>用户信息</returns>
        /// <remarks>author:dingyong date:2016-07-09</remarks>
        public ResultSingle<UserAdmin> GetUserAdmin(Guid id)
        {
            ResultSingle<UserAdmin> info = new ResultSingle<UserAdmin>();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    var model = context.UserAdmin.Find(id);
                    if (model != null)
                    {
                        try
                        {
                            Guid userId = model.Id;
                            string key1 = userId.ToString().Split('-')[2];
                            string key2 = userId.ToString().Split('-')[3];
                            model.Password = Encrypt.DecryptDES(model.Password, key1 + key2);
                        }
                        catch
                        {
                            model.Password = model.Password;
                        }
                    }
                    info.ReturnObject = model;
                    info.Status = ErrorStatus.S0001;
                }
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("CA.Business.Admin.AdminUserMgt.GetUserAdmin   获取用户信息Error：", ex);
            }
            return info;
        }
        #endregion

        #region 根据用户编号获取用户名称
        /// <summary>
        /// 根据用户编号获取用户名称
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>用户信息</returns>
        /// <remarks>author:dingyong date:2016-07-18</remarks>
        public string GetUserName(Guid userId)
        {
            var UserName = "";
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    var model = context.UserAdmin.Find(userId);
                    if (model != null)
                    {
                        UserName = model.Name ?? model.UserName;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("CA.Business.Admin.AdminUserMgt.GetUserName   根据用户编号获取用户名称Error：", ex);
            }
            return UserName;
        }
        #endregion

        #region 获取用户角色[根据用户编号]        
        /// <summary>
        /// 获取用户角色      
        /// </summary>
        /// <param name="userId">用户编号.</param>
        /// <returns>用户角色信息.</returns>
        /// <remarks>add by dingyong,2016-07-08 09:48:27 </remarks>
        public string GetUserRoleName(Guid userId)
        {
            var RoleName = string.Empty;
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    var user = context.UserAdmin.Find(userId);
                    if (user != null)
                    {
                        var sql = context.Role.Where(r => user.RoleId.Contains(r.RoleID.ToString())).ToList();
                        foreach (var item in sql)
                        {
                            RoleName += "," + item.RoleName;
                        }
                        if (!string.IsNullOrEmpty(RoleName))
                        {
                            RoleName = RoleName.Remove(0, 1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("CA.Business.Admin.AdminUserMgt.GetUserRoleName  获取用户角色 Error：", ex);
            }
            return RoleName;
        }
        #endregion

        #region 获取用户角色 [根据角色编号]       
        /// <summary>
        /// 获取用户角色      
        /// </summary>
        /// <param name="roleId">角色编号.</param>
        /// <returns>用户角色信息.</returns>
        /// <remarks>add by dingyong,2016-07-08 09:48:27 </remarks>
        public string GetUserRoleName(string roleId)
        {
            var RoleName = string.Empty;
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    var sql = context.Role.Where(r => roleId.Contains("," + r.RoleID.ToString() + ",")).ToList();
                    foreach (var item in sql)
                    {
                        RoleName += "," + item.RoleName;
                    }
                    if (!string.IsNullOrEmpty(RoleName))
                    {
                        RoleName = RoleName.Remove(0, 1);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("CA.Business.Admin.AdminUserMgt.GetUserRoleName  获取用户角色 Error：", ex);
            }
            return RoleName;
        }
        #endregion

        #region 修改登录密码
        /// <summary>
        /// 修改登录密码
        /// </summary>
        /// <param name="id">用户编号</param>
        /// <param name="oldPwd">旧密码</param>
        /// <param name="newPwd">新密码</param>
        /// <returns>修改结果</returns>
        /// <remarks>author:dingyong date:2016-07-15</remarks>
        public ResultInfo ModifyPwd(Guid id, string oldPwd, string newPwd, Guid operUserId)
        {
            ResultInfo info = new ResultInfo();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    UserAdmin user = context.UserAdmin.Find(id);
                    if (user == null)
                    {
                        info.Status = ErrorStatus.S4001;
                    }
                    else
                    {
                        oldPwd = Encrypt.EncryptDES(oldPwd);
                        if (!oldPwd.Equals(user.Password, StringComparison.CurrentCultureIgnoreCase))
                        {
                            info.Status = ErrorStatus.S4002;
                        }
                        else
                        {
                            user.Password = Encrypt.EncryptDES(newPwd);
                            user.UpdateTime = DateTime.Now;
                            user.UpdateUser = operUserId;
                            context.SaveChanges();

                            info.Status = ErrorStatus.S0001;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("CA.Business.Admin.AdminUserMgt.ModifyPwd   修改登录密码Error：", ex);
            }

            return info;
        }
        #endregion

        #region 重置登录密码
        /// <summary>
        /// 重置登录密码
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="loginPwd">当前系统登录密码</param>
        /// <param name="newPwd">新密码</param>
        /// <returns>状态</returns>
        /// <remarks>author:dingyong date:2016-07-05</remarks>
        public ResultInfo ResetPwd(Guid userId, string loginPwd, string newPwd, Guid operUserId)
        {
            ResultInfo info = new ResultInfo();
            try
            {
                var check = PublicBusiness.LoginPwd(operUserId, loginPwd);
                if (check)
                {
                    using (WZKEntities context = new WZKEntities())
                    {
                        var admin = context.UserAdmin.Find(userId);
                        if (admin != null)
                        {
                            string key1 = userId.ToString().Split('-')[2];
                            string key2 = userId.ToString().Split('-')[3];
                            admin.Password = Encrypt.EncryptDES(newPwd, key1 + key2);
                            admin.UpdateTime = DateTime.Now;
                            admin.UpdateUser = operUserId;
                            context.SaveChanges();

                            info.Status = ErrorStatus.S0001;
                        }
                        else
                        {
                            info.Status = ErrorStatus.S4001;
                        }
                    }
                }
                else
                {
                    info.Status = ErrorStatus.S4024;
                }
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.AdminUserMgt.ResetPwd   重置登录密码Error：", ex);
            }
            return info;
        }
        #endregion

        #region 新增用户
        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="entity">用户信息</param>
        /// <param name="operUserId">操作人</param>
        /// <param name="roleName">操作人角色名称.</param>
        /// <returns></returns>
        /// <remarks>author:dingyong date:2016-07-09</remarks>
        public ResultInfo Add(UserAdmin entity, Guid operUserId,string roleName)
        {
            ResultInfo info = new ResultInfo();
            try
            {
                entity.Id = Guid.NewGuid();
                //上传了图片
                if (!string.IsNullOrWhiteSpace(entity.Photo))
                {
                    WebImage pic = new WebImage(entity.Photo);
                    if (pic != null)
                    {
                        DateTime now = DateTime.Now;
                        string photoUrl = ImgHandler.Instance.UploadImg(pic, 2);
                        ImgHandler.Instance.DelectPhoto(entity.Photo);//删除原图片
                        entity.Photo = photoUrl;
                    }
                }

                using (WZKEntities context = new WZKEntities())
                {
                    //是否重名
                    if (context.UserAdmin.Any(c => c.UserName == entity.UserName))
                    {
                        info.Status = ErrorStatus.S4006;
                    }
                    else
                    {
                        Guid userId = entity.Id;
                        string key1 = userId.ToString().Split('-')[2];
                        string key2 = userId.ToString().Split('-')[3];

                        entity.UpdateTime = DateTime.Now;
                        entity.UpdateUser = operUserId;
                        entity.InputUser = operUserId;
                        entity.Password = Encrypt.EncryptDES(entity.Password, key1 + key2);
                        entity.RoleId = "";
                        context.UserAdmin.Add(entity);

                        info.Status = context.SaveChanges() > 0 ? ErrorStatus.S0001 : ErrorStatus.S0002;
                    }
                }
                var model = new OperateLog();
                model.UserId = operUserId;
                model.RoleName = roleName;
                model.MenuName = "权限管理---系统用户---新增---保存";
                model.OldTableName = "UserAdmin";
                model.OldBussnessId = entity.Id.ToString();
                model.OperateDesc = "新增用户";
                PublicBusiness.AddOperateLog(model);
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Admin.AdminUserMgt.Add 新增用户Error：", ex);
            }

            return info;
        }
        #endregion

        #region 删除用户
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="menuId">菜单Id.</param>
        /// <param name="roleName">操作人角色名称</param>
        /// <param name="operUserId">操作人</param>
        /// <returns>ResultInfo.</returns>
        /// <remarks>add by dingyong,2016-09-13 15:02:14 </remarks>
        public ResultInfo DelUser(Guid userId, string roleName, Guid operUserId)
        {
            ResultInfo info = new ResultInfo();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    var user = context.UserAdmin.SingleOrDefault(c => c.Id == userId);
                    if (user != null)
                    {
                        user.State = (int)EUserAdminStatus.Del;
                    }
                    info.Status = context.SaveChanges() > 0 ? ErrorStatus.S0001 : ErrorStatus.S0002;
                }
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("CA.Business.Admin.AdminUserMgt.DelUser 删除用户Error：", ex);
            }
            return info;
        }
        #endregion

        #region 修改用户信息
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="entity">用户信息</param>
        /// <param name="operUserId">操作人</param>
        /// <param name="roleName">操作人角色名称</param>
        /// <returns></returns>
        /// <remarks>author:dingyong date:2016-07-09</remarks>
        public ResultInfo Modify(UserAdmin entity, Guid operUserId,string roleName)
        {
            ResultInfo info = new ResultInfo();
            try
            {
                //重新上传了图片
                if (entity.Photo != null && entity.Photo.IndexOf(AliyunConfig.TempUpload) > 0)
                {
                    WebImage pic = new WebImage(entity.Photo);
                    if (pic != null)
                    {
                        DateTime now = DateTime.Now;
                        string photoUrl = ImgHandler.Instance.UploadImg(pic, 2);
                        ImgHandler.Instance.DelectPhoto(entity.Photo);//删除原图片
                        entity.Photo = photoUrl;
                    }
                }

                using (WZKEntities context = new WZKEntities())
                {
                    var user = context.UserAdmin.Find(entity.Id);

                    //是否重名
                    if (context.UserAdmin.Any(c => c.UserName == entity.UserName && c.Id != entity.Id))
                    {
                        info.Status = ErrorStatus.S4006;
                    }
                    else
                    {
                        Guid userId = entity.Id;
                        string key1 = userId.ToString().Split('-')[2];
                        string key2 = userId.ToString().Split('-')[3];

                        user.Password = Encrypt.EncryptDES(entity.Password, key1 + key2);
                        user.Department = entity.Department;
                        user.IdCard = entity.IdCard;
                        user.Mail = entity.Mail;
                        user.Mobile = entity.Mobile;
                        user.Name = entity.Name;
                        user.Photo = entity.Photo;
                        user.State = entity.State;
                        user.Telephone = entity.Telephone;
                        user.UpdateTime = DateTime.Now;
                        user.UpdateUser = operUserId;
                        user.UserName = entity.UserName;

                        context.SaveChanges();
                        info.Status = ErrorStatus.S0001;
                    }
                }
                var model = new OperateLog();
                model.UserId = operUserId;
                model.RoleName = roleName;
                model.MenuName = "权限管理---系统用户---查看---保存";
                model.OldTableName = "UserAdmin";
                model.OldBussnessId = entity.Id.ToString();
                model.OperateDesc = "修改用户信息";
                PublicBusiness.AddOperateLog(model);
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("CA.Business.Admin.AdminUserMgt.Modify 修改用户信息Error：", ex);
            }

            return info;
        }
        #endregion

        #region 根据关键字和角色获取用户
        /// <summary>
        /// Gets the adm user by keyword.
        /// </summary>
        /// <param name="keyword">The keyword.</param>
        /// <param name="roleId">The role identifier.</param>
        /// <returns>List&lt;UserAdmin&gt;.</returns>
        /// <remarks>add by dingyong,2016-08-08 15:37:43 </remarks>
        public List<UserAdmin> GetUserByKeyword(string keyword, int roleId = 0)
        {
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    var sql = context.UserAdmin.Where(c => c.UserName.Contains(keyword) || c.Name.Contains(keyword) || c.Mobile.Contains(keyword));
                    if (roleId > 0)
                    {
                        sql = sql.Where(c => c.RoleId.Contains("," + roleId + ","));
                    }
                    return sql.OrderByDescending(c => c.RegisterTime).Take(10).ToList();
                }
            }
            catch (Exception ex)
            {
                log.Error("CA.Business.Admin.AdminUserMgt.GetAdmUserByKeyword , 获取后台用户信息Error：", ex);
            }
            return null;
        }
        #endregion
       
    }
}
