using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WZK.Business.Admin;
using WZK.Common;
using WZK.Entity;
using WZK.Admin.Models.Right;
using WZK.Business.Admin.Tool;
using WZK.Business.Admin.Enum;
using WZK.Business.Admin.Models;
using WZK.Admin.Common;

namespace WZK.Admin.Controllers
{
    /// <summary>
    /// 权限管理
    /// author:dingyong
    /// date:2016-07-05
    /// </summary>
    public class RightController : BaseController
    {
        #region 后台用户列表
        /// <summary>
        /// 后台用户列表
        /// </summary>
        /// <returns>后台用户列表</returns>
        /// <remarks>author:dingyong date:2016-07-05</remarks>
        public ActionResult UserList()
        {
            return View();
        }
        #endregion

        #region 后台用户列表数据
        /// <summary>
        /// 后台用户列表数据
        /// </summary>
        /// <param name="id">页码</param>
        /// <param name="name">用户名</param>
        /// <returns>后台用户列表数据</returns>
        /// <remarks>author:dingyong date:2016-07-05</remarks>
        public ActionResult _UserList(string name, int id)
        {
            ViewBag.List = new AdminUserMgt().GetUserList(name, id);
            return View();
        }
        #endregion

        #region 用户详情
        /// <summary>
        /// 用户详情
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        /// <remarks>author:dingyong date:2016-07-05</remarks>
        public ActionResult UserDetail(Guid? id)
        {
            var status = new List<SelectListItem>();
            foreach (EUserAdminStatus item in Enum.GetValues(typeof(EUserAdminStatus)))
            {
                status.Add(new SelectListItem() { Text = item.GetDesc(), Value = ((int)item).ToString() });
            }
            ViewBag.Status = status;
            var dept = new List<SelectListItem>() {
                new SelectListItem() { Text ="==请选择==", Value = "" },
                new SelectListItem() { Text ="总经办", Value = "总经办" },
                new SelectListItem() { Text = "产品部", Value = "产品部" },
                new SelectListItem() { Text = "技术部", Value = "技术部" },
                new SelectListItem() { Text = "设计部", Value = "设计部" },
                new SelectListItem() { Text = "运营部", Value = "运营部" },
                new SelectListItem() { Text = "市场部", Value = "市场部" },
                new SelectListItem() { Text = "财务部", Value = "财务部" },
                new SelectListItem() { Text = "人事部", Value = "人事部" }
            };
            ViewBag.Dept = dept;//部门
            if (id == null)
            {
                ViewBag.name = "新增";
                return View(new ModifyUserInfo());
            }
            else
            {
                ViewBag.name = "详情";
                var info = new AdminUserMgt().GetUserAdmin(id.Value);

                return View(ModelMapping.ChangeModel<UserAdmin, ModifyUserInfo>(info.ReturnObject));
            }
        }
        #endregion

        #region 修改用户信息
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="model">用户信息</param>
        /// <returns></returns>
        /// <remarks>author:dingyong date:2016-07-05</remarks>
        public ActionResult ModifyUserInfo(ModifyUserInfo model)
        {
            if (ModelState.IsValid)
            {
                ResultInfo info = new ResultInfo();
                var manager = new AdminUserMgt();

                //新增
                if (model.Id == Guid.Empty)
                {
                    info = manager.Add(ModelMapping.ChangeModel<ModifyUserInfo, UserAdmin>(model), UserId,this.RoleName);
                }
                //修改
                else
                {
                    info = manager.Modify(ModelMapping.ChangeModel<ModifyUserInfo, UserAdmin>(model), UserId,this.RoleName);
                }
                return Json(info);
            }
            return Json("");
        }
        #endregion

        #region 删除用户
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="roleId">角色Id.</param>
        /// <returns>ActionResult.</returns>
        /// <remarks>add by dingyong,2016-09-13 15:17:45 </remarks>
        [HttpPost]
        public ActionResult DelUser(Guid userId)
        {
            ResultInfo info = new AdminUserMgt().DelUser(userId, RoleName, UserId);
            if (info.IsPass)
            {
                RightInfo.GetRightInfo().CacheRight(UserId);
            }
            return Json(info);
        }
        #endregion

        #region 重置登录密码
        /// <summary>
        /// 重置登录密码
        /// </summary>
        /// <returns></returns>
        /// <remarks>author:dingyong date:2016-07-05</remarks>
        public ActionResult ResetPwd(Guid id)
        {
            ViewBag.Id = id;
            return View();
        }

        /// <summary>
        /// 重置登录密码
        /// </summary>
        /// <param name="model">修改密码的模型</param>
        /// <returns></returns>
        /// <remarks>author:dingyong date:2016-07-05</remarks>
        [HttpPost]
        public ActionResult ResetPwd(ResetPwd model)
        {
            if (ModelState.IsValid)
            {
                var info = new AdminUserMgt().ResetPwd(model.Id, model.LoginPwd, model.NewPwd, UserId);
                return Json(info);
            }
            else
            {
                return Json(null);
            }
        }
        #endregion

        #region 修改用户角色
        /// <summary>
        /// 修改用户角色
        /// </summary>
        /// <param name="id">用户角色</param>
        /// <returns></returns>
        /// <remarks>author:dingyong date:2016-07-05</remarks>
        public ActionResult SetUserRole(Guid id)
        {
            ViewBag.AllRole = new RightMgt().GetRoleList(UserId, RoleName, "", 0, true).ReturnList;
            ViewBag.UserId = id;
            var roles = new AdminUserMgt().GetUserAdmin(id).ReturnObject.RoleId;
            if (!string.IsNullOrWhiteSpace(roles))
            {
                ViewBag.Roles = Newtonsoft.Json.JsonConvert.SerializeObject(roles.Split(','));
            }
            return View();
        }
        #endregion

        #region 保存用户角色
        /// <summary>
        /// 保存用户角色
        /// </summary>
        /// <returns></returns>
        /// <remarks>author:dingyong date:2016-07-05</remarks>
        public ActionResult SaveUserRole()
        {
            var id = Guid.Parse(Request.Form["userId"]);
            var roles = Request.Form["role"];
            var info = new RightMgt().SetUserRole(RoleName, id, roles, UserId);
            return Json(info);
        }
        #endregion

        #region 角色列表
        /// <summary>
        /// 角色列表
        /// </summary>
        /// <returns></returns>
        /// <remarks>author:dingyong date::2016-07-05</remarks>
        public ActionResult RoleList()
        {
            return View();
        }
        #endregion

        #region 角色列表数据
        /// <summary>
        /// 角色列表数据
        /// </summary>
        /// <param name="id">页码</param>
        /// <param name="name">角色名</param>
        /// <returns></returns>
        /// <remarks>author:dingyong date::2016-07-05</remarks>
        public ActionResult _RoleList(string name, int id)
        {
            ViewBag.List = new RightMgt().GetRoleList(UserId, RoleName, name, id);
            return View();
        }
        #endregion

        #region 角色详情
        /// <summary>
        /// 角色详情
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        /// <remarks>author:dingyong date:2016-07-05</remarks>
        public ActionResult RoleDetail(int? roleId)
        {
            if (roleId == null)
            {
                ViewBag.name = "新增";
                return View(new ModifyRoleInfo());
            }
            else
            {
                ViewBag.name = "详情";
                var info = new RightMgt().GetRole(UserId, RoleName, roleId.Value);
                return View(ModelMapping.ChangeModel<Role, ModifyRoleInfo>(info.ReturnObject));
            }
        }
        #endregion

        #region 修改角色信息
        /// <summary>
        /// 修改角色信息
        /// </summary>
        /// <returns></returns>
        /// <remarks>author:dingyong date::2016-07-05</remarks>
        public ActionResult ModifyRoleInfo(ModifyRoleInfo model)
        {
            if (ModelState.IsValid)
            {
                ResultInfo info = new ResultInfo();
                var manager = new RightMgt();

                //新增
                if (model.RoleID == 0)
                {
                    info = manager.AddRole(this.RoleName, ModelMapping.ChangeModel<ModifyRoleInfo, Role>(model), UserId);
                }
                //修改
                else
                {
                    info = manager.ModifyRole(this.RoleName, ModelMapping.ChangeModel<ModifyRoleInfo, Role>(model), UserId);
                }
                return Json(info);
            }
            return Json("");
        }
        #endregion

        #region 设置角色权限
        /// <summary>
        /// 设置角色权限
        /// </summary>
        /// <returns></returns>
        /// <remarks>author:dingyong date:2016-07-05</remarks>
        public ActionResult SetRoleRight(int roleId)
        {
            RightMgt manager = new RightMgt();
            var menus = manager.GetMenuList(this.UserId, this.RoleName, "", 0, true).ReturnList;
            ViewBag.Menus = Newtonsoft.Json.JsonConvert.SerializeObject(menus.Select(m => new { m.Id, m.Name, m.ParentId }));
            var actions = manager.GetMenuButtonList(0, "", 0, true).ReturnList;
            ViewBag.Actions = Newtonsoft.Json.JsonConvert.SerializeObject(actions.Select(a => new { a.Id, a.MenuId, a.Name }));
            var rights = manager.GetRole(this.UserId, this.RoleName, roleId).ReturnObject.ButtonIds ?? "";
            ViewBag.Rights = "[" + rights + "]";
            ViewBag.RoleId = roleId;
            return View();
        }
        #endregion

        #region 保存角色权限
        /// <summary>
        /// 保存角色权限
        /// </summary>
        /// <returns></returns>
        /// <remarks>author:dingyong date:2016-07-05</remarks>
        public ActionResult SaveRoleRight()
        {
            int roleId = Convert.ToInt32(Request.Form["roleId"]);
            string actions = Request.Form["actions"];

            var result = new RightMgt().SetRoleRight(RoleName, roleId, actions, UserId);
            if (result.IsPass)
            {
                RightInfo.GetRightInfo().CacheRight(UserId);
            }
            return Json(result);
        }
        #endregion

        #region 删除角色
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleId">角色Id.</param>
        /// <returns>ActionResult.</returns>
        /// <remarks>add by dingyong,2016-09-13 15:17:45 </remarks>
        [HttpPost]
        public ActionResult DelRole(int roleId)
        {
            ResultInfo info = new RightMgt().DelRole(roleId, RoleName, UserId);
            if (info.IsPass)
            {
                RightInfo.GetRightInfo().CacheRight(UserId);
            }
            return Json(info);
        }
        #endregion

        #region 菜单列表
        /// <summary>
        /// 菜单列表
        /// </summary>
        /// <returns></returns>
        /// <remarks>author:dingyong date:2016-07-05</remarks>
        [WhiteAction]
        public ActionResult MenuList(int? menuId)
        {
            ViewBag.MenuId = menuId;
            return View();
        }
        #endregion

        #region 菜单列表数据
        /// <summary>
        /// 菜单列表数据
        /// </summary>
        /// <param name="name">菜单名</param>
        /// <param name="id">页码</param>
        /// <param name="parentid">父级菜单.</param>
        /// <returns>ActionResult.</returns>
        /// <remarks>add by dingyong,2016-09-14 14:07:29 </remarks>
        public ActionResult _MenuList(string name, int id, int parentid = 0)
        {
            ViewBag.List = new RightMgt().GetMenuList(UserId, RoleName, name, id, false, parentid);
            return View();
        }
        #endregion

        #region 菜单详情
        /// <summary>
        /// 菜单详情
        /// </summary>
        /// <param name="id">菜单ID</param>
        /// <returns></returns>
        /// <remarks>author:dingyong date:2016-07-05</remarks>
        public ActionResult MenuDetail(int? id)
        {
            var manager = new RightMgt();
            ViewBag.menu = new List<SelectListItem>() {
                new SelectListItem() { Text ="主菜单", Value = "0" },
                new SelectListItem() { Text ="子菜单", Value = "1" }
            };
            if (id == null)
            {
                ResultSingle<string> result = new RightMgt().GetOrderNo(0, 2);
                ViewBag.name = "新增";
                return View(new ModifyMenuInfo() { OrderNo = result.ReturnObject });
            }
            else
            {
                ViewBag.name = "详情";
                var info = manager.GetMenu(UserId, RoleName, id.Value);
                var menu = ModelMapping.ChangeModel<Menus, ModifyMenuInfo>(info.ReturnObject);
                menu.AllOrderNo = menu.OrderNo;
                if (menu.OrderNo.Length >= 3)
                {
                    menu.OrderNo = menu.OrderNo.Substring(menu.OrderNo.Length - 3, 3);
                }
                return View(menu);
            }
        }
        #endregion

        #region 修改菜单
        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="model">菜单信息</param>
        /// <returns></returns>
        /// <remarks>author:dingyong date:2016-07-05</remarks>
        public ActionResult ModifyMenuInfo(ModifyMenuInfo model)
        {
            if (ModelState.IsValid)
            {
                ResultInfo info = new ResultInfo();
                var manager = new RightMgt();
                //新增
                if (model.Id == 0)
                {
                    info = manager.AddMenu(RoleName, ModelMapping.ChangeModel<ModifyMenuInfo, Menus>(model), UserId);
                }
                //修改
                else
                {
                    model.OrderNo = model.AllOrderNo;
                    info = manager.ModifyMenu(RoleName, ModelMapping.ChangeModel<ModifyMenuInfo, Menus>(model), UserId);
                }
                return Json(info);
            }
            return Json("");
        }
        #endregion

        #region 删除菜单
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="menuId">菜单按钮Id.</param>
        /// <returns>ActionResult.</returns>
        /// <remarks>add by dingyong,2016-09-13 15:01:23 </remarks>
        [HttpPost]
        public ActionResult DelMenu(int menuId)
        {
            ResultInfo info = new RightMgt().DelMenu(menuId, RoleName, UserId);
            if (info.IsPass)
            {
                RightInfo.GetRightInfo().CacheRight(UserId);
            }
            return Json(info);
        }
        #endregion

        #region 功能列表
        /// <summary>
        /// 功能列表
        /// </summary>
        /// <returns></returns>
        /// <remarks>author:dingyong date:2016-07-05</remarks>
        public ActionResult ActionList(int? menuId, string orderNo = "")
        {
            ViewBag.MenuId = menuId;
            ViewBag.OrderNo = orderNo;
            return View();
        }
        #endregion

        #region 功能列表数据
        /// <summary>
        /// 功能列表数据
        /// </summary>
        /// <param name="menuId">菜单ID</param>
        /// <param name="name">功能名</param>
        /// <param name="id">页码</param>
        /// <returns>ActionResult.</returns>
        /// <remarks>add by dingyong,2016-12-02 16:06:56 </remarks>
        public ActionResult _ActionList(int menuId, string name, int id)
        {
            ResultList<MMenuButton> result = new RightMgt().GetMenuButtonList( menuId, name, id);
            ViewBag.List = result;
            ViewBag.Count = result.Count;
            return View();
        }
        #endregion

        #region 菜单下拉框数据源
        /// <summary>
        /// 通过父级获取子级
        /// </summary>
        /// <param name="parentId">父级ID</param>
        /// <returns></returns>
        /// <remarks>author:dingyong date:2016-07-05</remarks>
        [WhiteAction]
        public ActionResult GetMenuList(int parentId)
        {
            var info = new RightMgt().GetMenuList( parentId);
            return Json(new { info.IsPass, info.Status, info.Desc, ReturnList = info.ReturnList.Select(m => new { m.Id, m.Name, m.ParentId, m.OrderNo }) });
        }

        /// <summary>
        /// 获取同级
        /// </summary>
        /// <param name="id">菜单ID</param>
        /// <returns></returns>
        /// <remarks>author:dingyong date:2016-07-05</remarks>
        [WhiteAction]
        public ActionResult GetSiblingMenuList(int id)
        {
            var info = new RightMgt().GetSiblingMenuList(UserId, RoleName, id);
            return Json(new { info.IsPass, info.Status, info.Desc, ReturnList = info.ReturnList.Select(m => new { m.Id, m.Name, m.ParentId, m.OrderNo }) });
        }
        #endregion

        #region 按钮详情
        /// <summary>
        /// 按钮详情
        /// </summary>
        /// <param name="id">功能ID</param>
        /// <returns></returns>
        /// <remarks>author:dingyong date:2016-07-05</remarks>
        public ActionResult ActionDetail(int? id, int? menuId)
        {
            ViewBag.Status = EnumTool.ToSelectList(typeof(EButtonType));//按键类型

            var manager = new RightMgt();
            ViewBag.List = new SelectList(manager.GetMenuList(0).ReturnList, "Id", "Name");

            if (id == null)
            {
                ViewBag.name = "新增";
                ViewBag.MenuId = menuId;
                return View(new ModifyMenuButtonInfo());
            }
            else
            {
                ViewBag.name = "详情";
                var info = manager.GetMenuButton(UserId, RoleName, id.Value);
                var viewModel = ModelMapping.ChangeModel<MMenuButton, ModifyMenuButtonInfo>(info.ReturnObject);
                viewModel.AllOrderNo = viewModel.OrderNo;
                viewModel.OrderNo = viewModel.OrderNo.Substring(viewModel.OrderNo.Length - 3, 3);
                return View(viewModel);
            }
        }
        #endregion

        #region 修改按钮
        /// <summary>
        /// 修改按钮
        /// </summary>
        /// <param name="model">功能信息</param>
        /// <returns></returns>
        /// <remarks>author:dingyong date:2016-07-05</remarks>
        public ActionResult ModifyActionInfo(ModifyMenuButtonInfo model)
        {
            if (ModelState.IsValid)
            {
                ResultInfo info = new ResultInfo();
                var manager = new RightMgt();

                //新增
                if (model.Id == 0)
                {
                    info = manager.AddMenuButton(this.RoleName,ModelMapping.ChangeModel<ModifyMenuButtonInfo, MenuButton>(model), UserId);
                }
                //修改
                else
                {
                    info = manager.ModifyMenuButton(this.RoleName,ModelMapping.ChangeModel<ModifyMenuButtonInfo, MenuButton>(model), UserId);
                }
                return Json(info);
            }
            return Json("");
        }
        #endregion

        #region 删除按钮
        /// <summary>
        /// 删除按钮
        /// </summary>
        /// <param name="menuButtonId">菜单按钮Id.</param>
        /// <returns>ActionResult.</returns>
        /// <remarks>add by dingyong,2016-09-13 12:01:32 </remarks>
        [HttpPost]
        public ActionResult DelAction(int menuButtonId)
        {
            ResultInfo info = new RightMgt().DelMenuButton(menuButtonId, RoleName, UserId);
            return Json(info);
        }
        #endregion

        #region 获取排序
        /// <summary>
        /// 获取排序
        /// </summary>
        /// <param name="menuId">菜单按钮Id.</param>
        /// <param name="type">类型 1.菜单按钮 2.菜单</param>
        /// <returns>JsonResult.</returns>
        /// <remarks>add by dingyong,2016-09-13 09:59:05 </remarks>
        [HttpGet]
        public JsonResult GetOrderNo(int menuId, int type)
        {
            ResultSingle<string> result = new RightMgt().GetOrderNo(menuId, type);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}