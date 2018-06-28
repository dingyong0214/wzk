using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using WZK.Common;
using System.Text;
using System.Web.Http.Results;

namespace WZK.Api.Controllers
{
    public class BaseController : ApiController
    {
        /// <summary>
        /// 登录用户编号
        /// </summary>
        public Guid UserId {
            get {
                return string.IsNullOrEmpty(HttpContext.Current.User.Identity.Name) ? Guid.Empty : Guid.Parse(HttpContext.Current.User.Identity.Name);
            }
        }

        protected string ModelErrorMsg()
        {
            var res = ModelState.Values.GetEnumerator();
            res.MoveNext();
            if (res.Current != null)
            {
                return res.Current.Errors[0].ErrorMessage;
            }
            else
            {
                return "输入参数有误";
            }
        }

        /// <summary>
        /// 重写Json方法，指定序列化格式
        /// </summary>
        /// <typeparam name="T">泛型T</typeparam>
        /// <param name="content">实体对象T</param>
        /// <param name="serializerSettings">序列化设置</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        /// <remarks>author:zengbo date:2015-12-01</remarks>
        protected override JsonResult<T> Json<T>(T content, JsonSerializerSettings serializerSettings, Encoding encoding)
        {
            serializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            return base.Json<T>(content, serializerSettings, encoding);
        }

        /// <summary>
        /// 返回列表
        /// </summary>
        /// <typeparam name="T">泛型T</typeparam>
        /// <param name="list">泛型T列表</param>
        /// <param name="status">错误状态码</param>
        /// <param name="count">数量</param>
        /// <returns>列表</returns>
        protected ResultList<T> ReturnList<T>(List<T> list, ErrorStatus status = ErrorStatus.S0001, int? count = null)
        {
            return new ResultList<T> { Count = count ?? list.Count, Status = status, ReturnList = list };
        }

        /// <summary>
        /// 返回单个实体
        /// </summary>
        /// <typeparam name="T">泛型T</typeparam>
        /// <param name="obj">实体对象T</param>
        /// <param name="status">错误状态码</param>
        /// <returns>单个实体对象T</returns>
        protected ResultSingle<T> ReturnObject<T>(T obj, ErrorStatus status = ErrorStatus.S0001)
        {
            return new ResultSingle<T> { ReturnObject = obj, Status = status };
        }

        /// <summary>
        /// 返回操作状态
        /// </summary>
        /// <param name="status">错误状态码</param>
        /// <returns>ResultInfo</returns>
        protected ResultInfo Return(ErrorStatus status = ErrorStatus.S0001)
        {
            return new ResultInfo { Status = status };
        }
    }
}
