using System;
using System.Linq;
using System.Data;
using WZK.Entity;
using WZK.Common;
using System.Configuration;
using WZK.Common.ConfigData;

namespace WZK.Business
{
    /// <summary>
    /// App接口
    /// author:dingyong
    /// date:2017-05-24
    /// </summary>
    public class Home: BaseBusiness
    {
        #region APP登录
        /// <summary>
        /// APP登录
        /// </summary>
        /// <param name="pwd">6位数密码</param>
        /// <returns>返回登录信息</returns>
        /// <remarks>add by dingyong,2017-05-12 16:46:54 </remarks>
        public ResultSingle<string> Login(int pwd,string customer)
        {
            ResultSingle<string> result = new ResultSingle<string>();
            result.Status = ErrorStatus.S1010;
            string token = string.Empty;
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    var authorize = context.AppAuthorize.Where(p => p.Password == pwd&&p.Customer==customer).FirstOrDefault();
                    if (authorize != null)
                    {
                        if (authorize.State == 0)
                        {
                            result.Status = ErrorStatus.S1006;
                        }
                        else
                        {
                            token = authorize.Token;
                            result.Status = ErrorStatus.S0001;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Home.Login 登录Error：", ex);
            }
            result.ReturnObject = token;
            return result;
        }
        #endregion

        #region APP版本升级
        /// <summary>
        /// APP版本升级
        /// </summary>
        /// <param name="vcode">版本号</param>
        /// <param name="type">1-autoreply,2-wzk</param>
        /// <remarks>added by dingyong date:2017-05-24</remarks>
        public ResultSingle<string> Upgrade(int vcode,int type)
        {
            ResultSingle<string> info = new ResultSingle<string>() { Status = ErrorStatus.S0002, Desc = "暂无新本版" };
            try
            {
                var code = type == 1 ? Convert.ToInt32(AppConfig.Vcode) : Convert.ToInt32(AppConfig.WzkVcode);
                if (vcode != 0 && code > vcode)
                {
                    info.Status = ErrorStatus.S0001;
                    info.ReturnObject = type == 1? AppConfig.ApkUrl.Trim():AppConfig.WzkApkUrl.Trim();
                    info.Desc = "检测到新版本";
                }
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Home.Upgrade APP版本升级Error：", ex);
            }
            return info;
        }
        #endregion

        #region 更新手机表mac地址和android推送Id
        /// <summary>
        /// 更新手机表mac地址和android推送Id
        /// </summary>
        /// <param name="mobile">手机号/微信号</param>
        /// <param name="mac">mac地址</param>
        /// <param name="clientId">android推送标识</param>
        /// <returns>ResultInfo.</returns>
        /// <remarks>add by dingyong,2017-05-24 16:46:54 </remarks>
        public ResultInfo ModifyMobileInfo(string mobile,string mac,string clientId)
        {
            ResultInfo info = new ResultInfo() { Status= ErrorStatus.S0002,Desc="微信号不存在" };
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    var model = context.Mobile.Where(m=>m.MobileNo==mobile).FirstOrDefault();
                    var entity = context.Mobile.Where(c => c.AndroidClientId == clientId).FirstOrDefault();
                    if (model != null)
                    {
                        if (entity != null)
                        {
                            entity.AndroidClientId =string.Empty;
                        }
                        model.Mac = mac;
                        model.AndroidClientId = clientId;
                        context.SaveChanges();
                        info.Status = ErrorStatus.S0001;
                        log.Error("WZK.Business.Home.ModifyMobileInfo  手机号"+ mobile+"更新推送标识成功：" + clientId,null);
                    }
                }
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Home.ModifyMobileInfo 更新手机表mac地址和android推送IdError：", ex);
            }
            return info;
        }
        #endregion

        #region 自动回复模板信息        
        /// <summary>
        /// 自动回复模板信息 .      
        /// </summary>
        /// <param name="id">自动回复模板编号.</param>
        /// <returns>自动回复模板信息.</returns>
        /// <remarks>add by dingyong,2017-05-15 16:08:14 </remarks>
        public ResultTwoList<AutoReplyTemplate,AutoReply> GetTemplate(Guid id)
        {
            ResultTwoList<AutoReplyTemplate, AutoReply> info = new ResultTwoList<AutoReplyTemplate, AutoReply>();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    info.ReturnObject = context.AutoReplyTemplate.Find(id);
                    info.ReturnList = context.AutoReply.Where(t => t.TemplateId == id).ToList();
                    info.Status = ErrorStatus.S0001;
                }
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Home.GetTemplate  获取自动回复模板信息Error：", ex);
            }
            return info;
        }
        #endregion

        #region 定位信息        
        /// <summary>
        /// 定位信息 .      
        /// </summary>
        /// <param name="id">定位编号.</param>
        /// <returns>定位信息.</returns>
        /// <remarks>add by dingyong,2017-05-15 16:08:14 </remarks>
        public ResultSingle<Location> GetLocation(Guid id)
        {
            ResultSingle<Location> info = new ResultSingle<Location>();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    info.ReturnObject = context.Location.Find(id);
                    info.Status = ErrorStatus.S0001;
                }
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Home.GetLocation  获取定位信息Error：", ex);
            }
            return info;
        }
        #endregion

        #region 朋友圈模板信息
        /// <summary>
        /// 朋友圈模板信息 .      
        /// </summary>
        /// <param name="id">朋友圈模板编号.</param>
        /// <returns>朋友圈模板信息.</returns>
        /// <remarks>add by dingyong,2017-05-18 19:42:14 </remarks>
        public ResultSingle<MomentsTemplate> GetMoments(Guid id)
        {
            ResultSingle<MomentsTemplate> info = new ResultSingle<MomentsTemplate>();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    info.ReturnObject = context.MomentsTemplate.Find(id);
                    info.Status = ErrorStatus.S0001;
                }
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Home.GetMoments  获取朋友圈模板信息Error：", ex);
            }
            return info;
        }
        #endregion

        #region 脚本信息
        /// <summary>
        /// 脚本信息 .      
        /// </summary>
        /// <param name="id">脚本编号.</param>
        /// <returns>脚本信息.</returns>
        /// <remarks>add by dingyong,2017-05-18 19:42:14 </remarks>
        public ResultSingle<Script> GetScript(Guid id)
        {
            ResultSingle<Script> info = new ResultSingle<Script>();
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    info.ReturnObject = context.Script.Find(id);
                    info.Status = ErrorStatus.S0001;
                }
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Home.GetScript  获取脚本信息Error：", ex);
            }
            return info;
        }
        #endregion

        #region 更新任务明细表
        /// <summary>
        /// 更新任务明细表
        /// </summary>
        /// <param name="id">任务编号</param>
        /// <param name="mobile">手机号码/微信号</param>
        /// <param name="times">已经执行的次数：默认0</param>
        /// <param name="state">状态：0-新任务，1-已接受，2-开始执行，3-已完成，4-执行失败</param>
        /// <returns>ResultInfo.</returns>
        /// <remarks>add by dingyong,2017-05-24 16:46:54 </remarks>
        public ResultInfo ModifyTaskDetail(Guid id,string mobile,int times,int state)
        {
            ResultInfo info = new ResultInfo() { Status = ErrorStatus.S0002, Desc = "该任务不存在" };
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    var model = (from td in context.TaskDetail
                                 join m in context.Mobile
                                 on td.MobileId equals m.Id
                                 where td.TaskId == id && m.MobileNo == mobile
                                 select td).FirstOrDefault();
                    if (model != null)
                    {
                        model.ExecutedTimes = times;
                        model.State = state;
                        if (state == 3)
                            model.EndTime = DateTime.Now;
                        context.SaveChanges();
                        info.Status = ErrorStatus.S0001;
                        log.Error("WZK.Business.Home.ModifyTaskDetail 微信："+ mobile+" 更新了任务明细表。任务编号："+id+",任务状态："+state, null);
                    }
                }
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Home.ModifyTaskDetail 更新任务明细表 Error：", ex);
            }
            return info;
        }
        #endregion

        #region 上传手机截屏图片
        /// <summary>
        /// 上传手机截屏图片
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="pic">图片地址</param>
        /// <returns></returns>
        public ResultInfo UploadImg(Guid id, string pic)
        {
            ResultInfo info = new ResultInfo() { Status = ErrorStatus.S0002, Desc = "图片上传失败" };
            try
            {
                using (WZKEntities context = new WZKEntities())
                {
                    var model = context.RealtimeControl.Find(id);
                    if (model != null)
                    {
                        model.Shootme = pic;
                        model.IsUsed = 0;
                        context.SaveChanges();
                        info.Status = ErrorStatus.S0001;
                    }
                }
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("WZK.Business.Home.UploadImg 上传手机截屏图片 Error：", ex);
            }
            return info;

        }
        #endregion
    }
}
