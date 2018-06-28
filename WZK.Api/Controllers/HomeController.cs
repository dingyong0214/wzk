using System.Web.Http;
using WZK.Common;
using WZK.Business;
using System;
using WZK.Models.api;
using System.Web.Helpers;

namespace WZK.Api.Controllers
{
    /// <summary>
    /// 首页
    /// </summary>
    [Authorize]
    [RoutePrefix("Home")]
    public class HomeController : BaseController
    {
        Home manage = new Home();

        #region 登录
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="model">{"PassWord":"密码","Customer":"客户"}</param>
        /// <returns>返回token</returns>
        /// <remarks>added by dingyong date:2017-05-24</remarks>
        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public IHttpActionResult Login([FromBody]MLogin model)
        {
            Guid userId = Guid.Empty;
            ResultSingle<string> result = manage.Login(model.PassWord,model.Customer);
            return Json(result);
        }
        #endregion

        #region APP版本升级
        /// <summary>
        /// APP版本升级
        /// </summary>
        /// <param name="vcode">版本号</param>
        /// <remarks>added by dingyong date:2017-05-24</remarks>
        [HttpGet]
        [AllowAnonymous]
        [Route("Upgrade")]
        public IHttpActionResult Upgrade(int vcode)
        {
            var result = manage.Upgrade(vcode,1);
            return Json(result);
        }
        #endregion

        #region WZK APP版本升级
        /// <summary>
        ///WZK APP版本升级
        /// </summary>
        /// <param name="vcode">版本号</param>
        /// <remarks>added by dingyong date:2017-07-31</remarks>
        [HttpGet]
        [AllowAnonymous]
        [Route("WzkUpgrade")]
        public IHttpActionResult WzkUpgrade(int vcode)
        {
            var result = manage.Upgrade(vcode,2);
            return Json(result);
        }
        #endregion

        #region 更新手机表mac地址和android推送Id
        /// <summary>
        /// 更新手机表mac地址和android推送Id
        /// </summary>
        /// <param name="model">{Mobile:手机号，Mac：mac地址，ClientId：android推送Id}</param>
        /// <remarks>added by dingyong date:2017-05-24</remarks>
        [HttpPost]
        [Route("ModifyMobileInfo")]
        public IHttpActionResult ModifyMobileInfo([FromBody]MMobile model)
        {
            var result = manage.ModifyMobileInfo(model.Mobile,model.Mac,model.ClientId);
            return Json(result);
        }
        #endregion

        #region 获取模板信息
        /// <summary>
        /// 获取模板信息
        /// </summary>
        /// <param name="taskType">任务类型：1-执行脚本，2-自动回复，3-发朋友圈,4-定位</param>
        /// <param name="templateId">模板编号</param>
        /// <returns></returns>
        /// <remarks>added by dingyong date:2017-05-24</remarks>
        [HttpGet]
        [Route("GetTemplate")]
        public IHttpActionResult GetTemplate(int taskType, Guid templateId)
        {
            object result;
            switch (taskType)
            {
                case 1:
                     result = manage.GetScript(templateId);
                    break;
                case 2:
                     result = manage.GetTemplate(templateId);
                    break;
                case 3:
                    result = manage.GetMoments(templateId);
                    break;
                case 4:
                     result = manage.GetLocation(templateId);
                    break;
                default:
                    result = new ResultInfo() {Status=ErrorStatus.S0002 };
                    break;
            }
            return Json(result);
        }
        #endregion

        #region 更新任务明细表
        /// <summary>
        /// 更新任务明细表
        /// </summary>
        /// <param name="model">{Id:任务编号, Mobile:手机号码,Times:已经执行的次数,State:任务状态：0-新任务，1-已接受，2-开始执行，3-已完成，4-执行失败}</param>
        /// <remarks>added by dingyong date:2017-05-24</remarks>
        [HttpPost]
        [Route("ModifyTaskDetail")]
        public IHttpActionResult ModifyTaskDetail([FromBody]MTaskDetail model)
        {
            var result = manage.ModifyTaskDetail(model.Id,model.Mobile,model.Times,model.State);
            return Json(result);
        }
        #endregion

        #region 手机截屏图片上传
        /// <summary>
        /// 手机截屏图片上传
        /// </summary>
        /// <returns>截屏图片上传是否成功</returns>
        /// <remarks>add by dingyong,2017-09-26</remarks>
        [HttpPost]
        [Route("Upload")]
        public IHttpActionResult Upload()
        {
            ResultInfo info = new ResultInfo() { Status = ErrorStatus.S0002, Desc = "图片上传失败" };
            string Id = System.Web.HttpContext.Current.Request["Id"];
            Guid pid = Guid.Empty;
            Guid.TryParse(Id, out pid);
            WebImage pic = ImgHandler.Instance.GetImageFromRequest();   
            if (pic != null)
            {
                string path = ImgHandler.Instance.UploadImg(pic, 6);
                info = manage.UploadImg(pid, path);
            }
            return Json(info);
        }
        #endregion

    }
}
