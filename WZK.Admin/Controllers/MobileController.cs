using System;
using System.Web.Mvc;
using WZK.Business.Admin;
using WZK.Common;
using WZK.Entity;
using WZK.Business.Admin.Tool;
using WZK.Admin.Models.Mobile;
using System.Collections.Generic;

namespace WZK.Admin.Controllers
{
    public class MobileController : BaseController
    {
        MobileMgt manage = new MobileMgt();

        #region 返回手机型号/所在机房/是否激活List
        /// <summary>
        /// 返回手机型号/所在机房/是否激活List
        /// </summary>
        /// <param name="isDefault">是否激活是否需要默认值 true-需要，false-不需要</param>
        /// <param name="isNeed">是否需要分辨率</param>
        /// <returns></returns>
        /// <remarks>author:dingyong date:2017-05-15</remarks>
        public Tuple<List<SelectListItem>, List<SelectListItem>, List<SelectListItem>> SelectListTuple(bool isDefault = true, bool isNeed = false)
        {
            List<SelectListItem> mtList = new List<SelectListItem>();
            var types = new MobileModelMgt().GetIMobileModelListAll();
            mtList.Add(new SelectListItem { Text = "--请选择--", Value = Guid.Empty.ToString() });
            foreach (MobileTypes mt in types.ReturnList)
            {
                if(isNeed)
                  mtList.Add(new SelectListItem { Text = mt.Name+"("+mt.DPI + ")", Value = mt.Id.ToString() });
                else
                  mtList.Add(new SelectListItem { Text = mt.Name, Value = mt.Id.ToString() });
            }
            List<SelectListItem> idcList = new List<SelectListItem>();
            var idcs = new IDCSMgt().GetIDCSListAll();
            idcList.Add(new SelectListItem { Text = "--请选择--", Value = Guid.Empty.ToString() });
            foreach (IDCS item in idcs.ReturnList)
            {
                idcList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            List<SelectListItem> isActive = new List<SelectListItem>();
            if (isDefault)
            {
               isActive.Add(new SelectListItem { Text = "--请选择--", Value = "-1" });
            }
            isActive.Add(new SelectListItem { Text = "是", Value = "1" });
            isActive.Add(new SelectListItem { Text = "否", Value = "0" });
            return  new Tuple<List<SelectListItem>, List<SelectListItem>, List<SelectListItem>>(mtList, idcList,isActive);
        }
        #endregion

        #region 手机列表
        /// <summary>
        /// 手机列表
        /// </summary>
        /// <returns></returns>
        /// <remarks>author:dingyong date:2017-05-15</remarks>
        public ActionResult Index()
        {
            var selectList = SelectListTuple();
            //手机型号
            ViewBag.MobileTypes = selectList.Item1;
            //所在机房
            ViewBag.IDCS = selectList.Item2;
            //是否激活
            ViewBag.IsActive = selectList.Item3;
            return View();
        }
        /// <summary>
        /// 手机列表数据
        /// </summary>
        /// <param name="id">页码</param>
        /// <param name="mobile">手机号码</param>
        /// <param name="mobileModel">手机型号</param>
        /// <param name="idc">所在机房</param>
        /// <param name="isActive">是否激活</param>
        /// <returns></returns>
        /// <remarks>author:dingyong date:2017-05-15</remarks>
        public ActionResult _Index(int id, string mobile,Guid mobileModel,Guid idc,int isActive)
        {
            ViewBag.List = manage.GetMobileList(mobile,mobileModel,idc,isActive,id);
            return View();
        }
        #endregion

        #region 手机列表
        /// <summary>
        /// 手机列表
        /// </summary>
        /// <param name="id">任务编号</param>
        /// <returns></returns>
        /// <remarks>author:dingyong date:2017-05-15</remarks>
        public ActionResult MobileList(Guid id)
        {
            var selectList = SelectListTuple();
            //手机型号
            ViewBag.MobileTypes = selectList.Item1;
            //所在机房
            ViewBag.IDCS = selectList.Item2;
            ViewBag.taskId = id;
            return View();
        }
        /// <summary>
        /// 手机列表数据
        /// </summary>
        /// <param name="id">页码</param>
        /// <param name="mobile">手机号码</param>
        /// <param name="mobileModel">手机型号</param>
        /// <param name="idc">所在机房</param>
        /// <returns></returns>
        /// <remarks>author:dingyong date:2017-05-15</remarks>
        public ActionResult _MobileList(int id, string mobile, Guid mobileModel, Guid idc,Guid taskId)
        {
            ViewBag.List = manage.GetMobileList(taskId,mobile, mobileModel, idc, id);
            return View();
        }
        #endregion

        #region 手机信息详情
        /// <summary>
        /// 手机信息详情
        /// </summary>
        /// <param name="id">手机编号</param>
        /// <returns></returns>
        /// <remarks>author:dingyong date:2017-05-15</remarks>
        public ActionResult MobileDetail(Guid? id)
        {
            var selectList = SelectListTuple(false,true);
            //手机型号
            ViewBag.MobileTypes = selectList.Item1;
            //所在机房
            ViewBag.IDCS = selectList.Item2;
            //是否激活
            ViewBag.ddIsActive = selectList.Item3;
            if (id == null)
            {
                ViewBag.name = "新增";
                return View(new ModifyMobile());
            }
            else
            {
                ViewBag.name = "详情";
                var info = manage.GetMobile(id.Value);
                return View(ModelMapping.ChangeModel<Entity.Mobile, ModifyMobile>(info.ReturnObject));
            }
        }
        #endregion

        #region 修改手机信息
        /// <summary>
        /// 修改手机信息
        /// </summary>
        /// <param name="model">手机信息</param>
        /// <returns></returns>
        /// <remarks>author:dingyong date:2017-05-15</remarks>
        public ActionResult ModifyMobileInfo(ModifyMobile model)
        {
            if (ModelState.IsValid)
            {
                ResultInfo info = new ResultInfo();
                //新增
                if (model.Id == Guid.Empty)
                {
                    info = manage.AddMobile(RoleName, ModelMapping.ChangeModel<ModifyMobile, Entity.Mobile>(model), this.UserId);
                }
                //修改
                else
                {
                    info = manage.ModifyMobile(RoleName, ModelMapping.ChangeModel<ModifyMobile, Entity.Mobile>(model), UserId);
                }
                return Json(info);
            }
            return this.GetAjaxErrorInfo();
        }
        #endregion

        #region 批量新增手机信息
        /// <summary>
        /// 批量新增界面
        /// </summary>
        /// <returns></returns>
        public ActionResult BatchAddMobileInfo()
        {
            var selectList = SelectListTuple(false,true);
            //手机型号
            ViewBag.MobileTypes = selectList.Item1;
            //所在机房
            ViewBag.IDCS = selectList.Item2;
            //是否激活
            ViewBag.ddIsActive = selectList.Item3;
            return View(new BatchMobileInfo());
        }

        /// <summary>
        /// 批量新增手机信息
        /// </summary>
        /// <param name="model">model.</param>
        /// <param name="Param">手机号 信息 （以多个以^分隔）</param>
        /// <param name="MobileName">手机名称</param>
        /// <param name="MobileType">手机型号</param>
        /// <param name="MobileTypeName">手机型号名称</param>
        /// <param name="DPI">分辨率</param>
        /// <param name="IDC">所在机房</param>
        /// <param name="Postion">所在位置</param>
        /// <param name="IsActive">是否激活 0-未激活，1-已激活</param>
        /// <returns></returns>
        /// <remarks>add by dingyong,2017-05-15 16:46:54 </remarks>
        public ActionResult InsertIntoMobile(BatchMobileInfo model)
        {
            if (ModelState.IsValid)
            {
                ResultInfo info = manage.InsertIntoMobile(this.RoleName, this.UserId, model.Param, model.MobileName, model.MobileType, model.MobileTypeName, model.DPI, model.IDC, model.Position, model.IsActive);
                return Json(info);
            }
            return this.GetAjaxErrorInfo();
        }
        #endregion

        #region 批量修改手机信息
        /// <summary>
        /// 批量修改手机界面
        /// </summary>
        /// <returns></returns>
        public ActionResult BatchUpdateMobileInfo(bool isAll=false)
        {
            var selectList = SelectListTuple(false,true);
            //手机型号
            ViewBag.MobileTypes = selectList.Item1;
            //所在机房
            ViewBag.IDCS = selectList.Item2;
            //是否激活
            ViewBag.IsActive = selectList.Item3;
            ViewBag.isAll = isAll;
            return View();
        }
        /// <summary>
        /// 批量修改手机信息
        /// </summary>
        /// <param name="param">手机编号（以多个以^分隔）</param>
        /// <param name="currType">当前手机型号</param>
        /// <param name="mType">修改后手机型号</param>
        /// <param name="mtName">手机型号名称</param>
        /// <param name="dpi">手机分辨率</param>
        /// <param name="currIDC">当前所在机房</param>
        /// <param name="idc">修改后所在机房</param>
        /// <param name="postion">所在位置</param>
        /// <param name="isActive">是否激活 0-未激活,1-已激活</param>
        /// <returns></returns>
        /// <remarks>add by dingyong,2017-05-15 16:46:54 </remarks>
        public ActionResult UpdateMobileInfo(string param, Guid? currType, Guid mType, string mtName,string dpi,Guid? currIDC, Guid idc, string position, int isActive)
        {
            ResultInfo info = manage.UpdateMobileInfo(this.RoleName, this.UserId, param, currType, mType, mtName,dpi,currIDC, idc,position, isActive);
            return Json(info);
        }
        #endregion

        #region 删除手机
        /// <summary>
        /// 删除手机
        /// </summary>
        /// <param name="Id">手机编号.</param>
        /// <returns>ActionResult.</returns>
        /// <remarks>author:dingyong date:2017-05-15 </remarks>
        [HttpPost]
        public ActionResult DelMobile(Guid id)
        {
            ResultInfo info = manage.DelMobile(id, RoleName, UserId);
            return Json(info);
        }
        #endregion

        #region 批量删除手机
        /// <summary>
        /// 批量删除手机
        /// </summary>
        /// <param name="param">手机编号（以多个以^分隔）</param>
        /// <returns>ActionResult.</returns>
        /// <remarks>author:dingyong date:2017-05-15 </remarks>
        [HttpPost]
        public ActionResult DelMobileInfo(string param)
        {
            ResultInfo info = manage.DelMobileInfo(param, RoleName, UserId);
            return Json(info);
        }
        #endregion

        #region 设备实时控制

        #region 页面详情
        /// <summary>
        /// 页面详情
        /// </summary>
        /// <param name="id">手机编号</param>
        /// <returns></returns>
        /// <remarks>author:dingyong date:2017-05-15</remarks>
        public ActionResult DeviceControl(Guid id)
        {
            var info = manage.GetMobile(id);
            //初始化数据
            var result=manage.initRealControl(id, this.UserId, this.RoleName);
            ViewBag.controlId = result.Item1;
            ViewBag.flag = result.Item2;
            return View(info.ReturnObject);
        }

        //更换手机
        [HttpPost]
        public JsonResult ChangeMobile(Guid id)
        {
            var info = manage.GetMobile(id);
            //初始化数据
            var result = manage.initRealControl(id, this.UserId, this.RoleName);
               return Json(new{
                    IsPass= info.IsPass,
                    Desc=info.Desc,
                    MobileName = info.ReturnObject.MobileName,
                    MobileNo= info.ReturnObject.MobileNo,
                    MobileTypeName = info.ReturnObject.MobileTypeName,
                    AndroidClientId = info.ReturnObject.AndroidClientId,
                    ControlId= result.Item1,
                    Flag = result.Item2
            });
        }

        #endregion

        #region App通知图片上传成功
        /// <summary>
        /// App通知图片上传成功
        /// </summary>
        /// <param name="connectionId">会话Id</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public JsonResult UploadSuccess(string connectionId)
        {
            Guid CId = Guid.Empty;
            var info = new ResultSingle<bool>() { Status = ErrorStatus.S0002, ReturnObject = false };
            if (Guid.TryParse(connectionId, out CId))
            {
                WZK.Admin.Notifier.NotifierClientUploadSs(connectionId, "success");
                info.Status = ErrorStatus.S0001;
                info.ReturnObject = true;
            }
            return Json(info);
        }

        #endregion

        #region App通知图片上传失败
        /// <summary>
        /// App通知图片上传失败
        /// </summary>
        /// <param name="connectionId">会话Id</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public JsonResult UploadFail(string connectionId)
        {
            Guid CId = Guid.Empty;
            var info = new ResultSingle<bool>() { Status = ErrorStatus.S0002, ReturnObject = false };
            if (Guid.TryParse(connectionId, out CId))
            {
                WZK.Admin.Notifier.NotifierClientUploadFail(connectionId, "fail");
                info.Status = ErrorStatus.S0001;
                info.ReturnObject = true;
            }
            return Json(info);
        }

        #endregion

        #region 获取坐标，发送指令
        /// <summary>
        /// 获取坐标，发送指令（坐标为空，截当前屏幕）
        /// </summary>
        /// <param name="coordinate">坐标</param>
        /// <param name="connectionId">会话Id</param>
        /// <param name="pushId">推送标识clientId</param>
        /// <param name="controlId">实时控制编号</param>
        /// <param name="operationType">操作类型 1-坐标，2-菜单，3,-home键，4-返回，后退，5-滚屏</param>
        /// <param name="flag">是否在其他地方控制，连接断开</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SendCoordinate(string coordinate,string connectionId,string pushId,Guid controlId,int operationType,Guid flag)
        {
 
            var info = new ResultSingle<int>() { Status = ErrorStatus.S0002, ReturnObject =0 };
            //bool-是否推送成功，int-0:正常，1：设备在其他地方被控制，2：数据初始化失败
            var result = PublicBusiness.PushMsgToSingle(coordinate, connectionId, pushId, controlId, flag, 1,operationType);
            if (result.Item1)
            {
                info.Status = ErrorStatus.S0001;
                info.ReturnObject = result.Item2;
            }
            else
            {
                info.ReturnObject = result.Item2;
            }
            return Json(info);
        }

        #endregion

        #region 获取手机截屏信息
        /// <summary>
        /// 获取手机截屏信息
        /// </summary>
        /// <param name="controlId">实时控制编号</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult RealtimeControlInfo(Guid controlId)
        {
            var info = manage.GetRealtimeControl(controlId);
            return Json(info);
        }
        #endregion

        #endregion

    }
}