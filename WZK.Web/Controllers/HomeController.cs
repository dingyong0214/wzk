using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WZK.Common;
using WZK.Common.Authentication;
using WZK.Web.Models;
using WZK.Business;

namespace WZK.Web.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        TouPiao WZK = new TouPiao();

        #region 选举程序步骤
        /// <summary>
        /// 选举程序步骤
        /// </summary>
        /// <returns></returns>
        /// <remarks>add by dingyong,2017-04-22 08:56:37 </remarks>
        public ActionResult Index()
        {
            ViewBag.List = WZK.ElectionProcessList();
            return View();
        }
        /// <summary>
        ///流程步骤对应操作
        /// </summary>
        /// <param name="id">流程步骤编号</param>
        /// <returns></returns>
         /// <remarks>add by dingyong,2017-04-22 08:56:37 </remarks>
        [HttpGet]
        public ActionResult Step(Guid id)
        {
            new Logger().Debug("流程步骤对应操作-进入到/home/step,id"+id);
            int flag = 0;
            var result = WZK.ElectionProcessDetail(id,UserId,out flag);
            //操作标识 1--投票，2--首页 3--结果
            if (flag == 2)
            {
                return RedirectToAction("Index");
            }
            else if (flag == 3||result.ReturnObject.State==2)
            {
                return RedirectToAction("VoteResult", new { result.ReturnObject.Id });
            }
            return View(result.ReturnObject);
        }
        #endregion

        #region  投票=>选举人列表
        /// <summary>
        /// 投票=>选举人列表
        /// </summary>
        /// <returns></returns>
        /// <remarks>add by dingyong,2017-04-22 08:56:37 </remarks>
        [HttpGet]
        public ActionResult Vote(Guid id)
        {
            int flag = 0;
            string title="";
            var detail = WZK.ProcessDetail(this.UserId,out flag, id,out title ).ReturnObject;
            if (flag == 2)
            {
                return RedirectToAction("Index");
            }
            else
            {
                if (flag == 0)
                {
                    ViewBag.List = WZK.GetZhangChengList();
                    ViewBag.Param = new Tuple<int, Guid, Guid, string>(0, detail.Id, detail.ProcessId, title);
                }
                else
                {
                    ViewBag.List = WZK.GetHouXuanRenList(detail.DutyId);
                    ViewBag.Param = new Tuple<int, Guid, Guid, string>(1, detail.Id, detail.ProcessId, title);
                }
            }
            return View();
        }
        #endregion

        #region 提交投票
        /// <summary>
        /// 提交投票
        /// </summary>
        /// <param name="param">投票信息{编号,结果^编号,结果}</param>
        /// <param name="flag">投票步骤 0-第一步</param>
        /// <param name="processDetailId">流程明细编号</param>
        /// <returns></returns>
        /// <remarks>add by dingyong，2017-04-22</remarks>
        [HttpPost]
        public ActionResult Vote(string param,int flag,Guid processDetailId)
        {
            ResultInfo result = new ResultInfo();
            if (flag == 0)
            {
                result = WZK.VoteZhangCheng(this.UserId, param,processDetailId);
            }
            else
            {
                result = WZK.Vote(this.UserId, param, processDetailId);
            }
           return Json(result);
        }
        #endregion

        #region  投票结果
        /// <summary>
        /// 投票结果
        /// </summary>
        /// <param name="id">步骤编号</param>
        /// <returns></returns>
        /// <remarks>add by dingyong,2017-04-22 08:56:37 </remarks>
        public ActionResult VoteResult(Guid id)
        {
            int flag = 0;
            var result = WZK.VoteTongJiStep(id, out flag);
            if (flag == -1)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.VoteResult = result;
                ViewBag.Flag = flag;
            }
            return View();
        }
        #endregion

    }
}