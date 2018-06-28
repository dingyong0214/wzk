usingBoat.Common;
using Boat.Entity;
using Boat.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Boat.Business.Common
{
    public class MessageCommon : BaseBusiness
    {
        #region 发送验证码
        /// <summary>
        /// 发送验证码.
        /// </summary>
        /// <param name="mobile">手机号</param>
        /// <param name="bussinessType">业务类型：0-注册 1-修改登录密码 2-修改交易密码 3-忘记登录密码 4-忘记支付密码 5-验证手机号码 6-修改手机号码</param>
        /// <param name="channel">激活渠道：0-网站 1-微信 2-安卓 3-IOS 4-触屏版 5-管理后台 6-支付宝</param>
        /// <returns>ErrorStatus.</returns>
        /// <remarks>add by xiongchonglong,2016-08-17 13:38:31 </remarks>
        public static ResultInfo SendCode(string mobile, int bussinessType = (int)ESmsBussinessType.Register, int channel = (int)EChannel.Site)
        {
            ResultInfo info = new ResultInfo();
            using (CaYiCaEntities context = new CaYiCaEntities())
            {
                //短信同一手机号同一业务每日发送限制
                var systemParam = ProfileCommon.GetDictItemList(DictEnum.DictType.SystemParam.ToString());
                string smsSendNumStr = ProfileCommon.GetDictItemValue(systemParam, DictItemEnum.SystemParamItem.SmsSendNum.ToString());
                int smsSendNum = 8; //发送次数每日限制默认八次
                int.TryParse(smsSendNumStr, out smsSendNum);
                DateTime dateTimeNow = DateTime.Now;
                DateTime minToday = DateTime.Today;
                DateTime maxToday = DateTime.Today.AddDays(86399F / 86400);
                if (context.VerifyCode.Count(c => c.PhoneNo == mobile && c.BussinessType == bussinessType && c.SendTime >= minToday && c.SendTime <= maxToday) >= smsSendNum)
                {
                    //发送次数超限
                    info.Status = ErrorStatus.S1021;
                }
                else if (((ESmsBussinessType)bussinessType == ESmsBussinessType.Register || (ESmsBussinessType)bussinessType == ESmsBussinessType.UpdatePhoneNumber) && context.Users.Any(p => p.Mobile == mobile.Trim()))
                {
                    //已经注册
                    info.Status = ErrorStatus.S1000;
                }
                else if ((ESmsBussinessType)bussinessType == ESmsBussinessType.ForgetLoginPassport && !context.Users.Any(p => p.Mobile == mobile.Trim()))
                {
                    //没有注册
                    info.Status = ErrorStatus.S1001;
                }
                else
                {
                    VerifyCode verifyCode = context.VerifyCode.Where(p => p.PhoneNo == mobile.Trim() && p.BussinessType == bussinessType).OrderByDescending(c => c.SendTime).FirstOrDefault();
                    if (verifyCode != null && (dateTimeNow - verifyCode.SendTime).TotalSeconds < 60)
                    {
                        //60秒内发送短信限制
                        info.Status = ErrorStatus.S1022;
                    }
                    else
                    {
                        var dictList = ProfileCommon.GetDictItemList(DictEnum.DictType.YunTongXun.ToString());
                        string isOpen = ProfileCommon.GetDictItemValue(dictList, DictItemEnum.YunTongXunValue.IsOpen.ToString());
                        string isDefaultCode = ProfileCommon.GetDictItemValue(dictList, DictItemEnum.YunTongXunValue.IsDefaultCode.ToString());
                        string defaultCode = ProfileCommon.GetDictItemValue(dictList, DictItemEnum.YunTongXunValue.DefaultCode.ToString());
                        string second = ProfileCommon.GetDictItemValue(dictList, DictItemEnum.YunTongXunValue.ValidTime.ToString());
                        int secondInt = 3;
                        if (int.TryParse(second, out secondInt)) { }
                        Code myCode = new Code();
                        string code = "0".Equals(isDefaultCode) ? myCode.CreateCode() : defaultCode;
                        MSendMsgResult mmr = new MSendMsgResult();
                        if ("1".Equals(isOpen))
                        {
                            mmr = SendSMS(code, mobile);
                            if (mmr.statusCode != "000000")
                            {
                                mmr.statusMsg = VoiceVerify(code, mobile);
                            }
                        }
                        else
                        {
                            mmr.statusCode = "000000";
                        }
                        if (mmr.statusCode == "000000")
                        {  
                            context.VerifyCode.Add(new VerifyCode()
                            {
                                BussinessType = bussinessType,
                                Channel = channel,
                                Code = code,
                                CodeType = 0,
                                Id = Guid.NewGuid(),
                                IP = Tool.GetIpAdress(),
                                PhoneNo = mobile,
                                SendTime = dateTimeNow,
                                ValidTime = dateTimeNow.AddMinutes(secondInt),
                                ValidTimeLen = secondInt,
                                VerifyFlag = 0,
                                Way = mmr.SendType,
                                VerifyNum = 0
                            });
                            context.SaveChanges();
                            info.Status = ErrorStatus.S0001;
                        }
                        info.Desc = mmr.statusMsg;
                    }
                }
            }
            return info;
        }
        #endregion

        #region 发送短信
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="code">验证码</param>
        /// <param name="mobile">手机号码</param>
        /// <returns>MSendMsgResult.</returns>
        /// <remarks>add by xiongchonglong,2016-08-17 20:03:25 </remarks>
        public static MSendMsgResult SendSMS(string code, string mobile)
        {
            string ret = null;
            MSendMsgResult mmr = new MSendMsgResult();
            CCPRestSDK.CCPRestSDK api = new CCPRestSDK.CCPRestSDK();
            //ip格式如下，不带https://
            var dictList = ProfileCommon.GetDictItemList(DictEnum.DictType.YunTongXun.ToString());
            string host = ProfileCommon.GetDictItemValue(dictList, DictItemEnum.YunTongXunValue.Host.ToString());
            string port = ProfileCommon.GetDictItemValue(dictList, DictItemEnum.YunTongXunValue.Port.ToString());
            string accountId = ProfileCommon.GetDictItemValue(dictList, DictItemEnum.YunTongXunValue.AccountSid.ToString());
            string accountToken = ProfileCommon.GetDictItemValue(dictList, DictItemEnum.YunTongXunValue.AuthToken.ToString());
            string appId = ProfileCommon.GetDictItemValue(dictList, DictItemEnum.YunTongXunValue.SmsAppId.ToString());
            string tempId = ProfileCommon.GetDictItemValue(dictList, DictItemEnum.YunTongXunValue.TempId.ToString());
            bool isInit = api.init(host, port);
            api.setAccount(accountId, accountToken);//主帐号，主帐号令牌
            api.setAppId(appId);//应用程序id
            try
            {
                if (isInit)
                {
                    var messageTypeList = ProfileCommon.GetDictItemList(DictEnum.DictType.SendMessageType.ToString());
                    string[] data = { code, ProfileCommon.GetDictItemValue(dictList, DictItemEnum.YunTongXunValue.ValidTime.ToString()) + "分钟" };// "模版内容数据"
                    Dictionary<string, object> retData = api.SendTemplateSMS(mobile, tempId, data);//模版id
                    object statusCode = "160050";   //短信发送失败
                    object statusMsg = "短信发送失败";
                    retData.TryGetValue("statusCode", out statusCode);
                    retData.TryGetValue("statusMsg", out statusMsg);
                    if (statusCode.ToString().Trim() != "000000")
                    {
                        var clList = ProfileCommon.GetDictItemList(DictEnum.DictType.Chuanglan.ToString());
                        var clTempList = ProfileCommon.GetDictItemList(DictEnum.DictType.ChuanglanTemp.ToString());
                        string postUrl = ProfileCommon.GetDictItemValue(clList, DictItemEnum.ChuanglanValue.PostUrl.ToString());
                        string posurl = string.Format("{0}?account={1}&pswd={2}&mobile={3}&msg={4}&needstatus=true", "http://" + (postUrl.Contains("http://") ? postUrl.Replace("http://", "") : postUrl), ProfileCommon.GetDictItemValue(clList, DictItemEnum.ChuanglanValue.Account.ToString()), ProfileCommon.GetDictItemValue(clList, DictItemEnum.ChuanglanValue.Password.ToString()), mobile, HttpUtility.UrlEncode(string.Format(ProfileCommon.GetDictItemValue(clTempList, tempId.Trim()), code, ProfileCommon.GetDictItemValue(dictList, DictItemEnum.YunTongXunValue.ValidTime.ToString()) + "分钟"), Encoding.UTF8), "");
                        string reponseStr = "";
                        HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(posurl);
                        try
                        {
                            HttpWebResponse httpWResp = (HttpWebResponse)httpWReq.GetResponse();
                            StreamReader reader = new StreamReader(httpWResp.GetResponseStream(), Encoding.UTF8);
                            reponseStr = reader.ReadToEnd();
                            reader.Close();
                        }
                        catch (WebException ex)
                        {
                            reponseStr = "";
                        }
                        var reponseArray = reponseStr.Split(',');
                        if (reponseArray.Length == 2)
                        {
                            if (reponseArray[1].IndexOf("0\n") == 0)  //成功
                            {
                                mmr = new MSendMsgResult() { statusCode = "000000", statusMsg = "发送成功", SendType = Convert.ToInt32(ProfileCommon.GetDictItemValue(messageTypeList, DictItemEnum.SendMessageTypeValue.Chuanglan.ToString())) };
                            }
                            else
                            {
                                string resultMsg = "";
                                var clResultList = ProfileCommon.GetDictItemList(DictEnum.DictType.ChuanglanResult.ToString());
                                resultMsg = ProfileCommon.GetDictItemValue(clResultList, reponseArray[1]);
                                mmr = new MSendMsgResult() { statusCode = reponseArray[1].ToString(), statusMsg = resultMsg.ToString(), SendType = Convert.ToInt32(ProfileCommon.GetDictItemValue(messageTypeList, DictItemEnum.SendMessageTypeValue.Gotye.ToString())) };
                            }
                        }
                    }
                    else
                    {
                        mmr = new MSendMsgResult() { statusCode = statusCode.ToString(), statusMsg = statusMsg.ToString(), SendType = Convert.ToInt32(ProfileCommon.GetDictItemValue(messageTypeList, DictItemEnum.SendMessageTypeValue.Gotye.ToString())) };
                    }
                }
                else
                {
                    ret = "初始化失败";
                }
            }
            catch (Exception exc)
            {
                ret = exc.Message;
            }
            return mmr;
        }
        #endregion

        #region 发送短信
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="code">验证码</param>
        /// <param name="mobile">手机号码</param>
        /// <returns>MSendMsgResult.</returns>
        /// <remarks>add by xiongchonglong,2016-08-17 20:03:25 </remarks>
        public static MSendMsgResult SendSMS(string mobile, string tempId, string[] tempContent)
        {
            string ret = null;
            MSendMsgResult mmr = new MSendMsgResult();
            CCPRestSDK.CCPRestSDK api = new CCPRestSDK.CCPRestSDK();
            //ip格式如下，不带https://
            var dictList = ProfileCommon.GetDictItemList(DictEnum.DictType.YunTongXun.ToString());
            string host = ProfileCommon.GetDictItemValue(dictList, DictItemEnum.YunTongXunValue.Host.ToString());
            string port = ProfileCommon.GetDictItemValue(dictList, DictItemEnum.YunTongXunValue.Port.ToString());
            string accountId = ProfileCommon.GetDictItemValue(dictList, DictItemEnum.YunTongXunValue.AccountSid.ToString());
            string accountToken = ProfileCommon.GetDictItemValue(dictList, DictItemEnum.YunTongXunValue.AuthToken.ToString());
            string appId = ProfileCommon.GetDictItemValue(dictList, DictItemEnum.YunTongXunValue.SmsAppId.ToString());
            bool isInit = api.init(host, port);
            api.setAccount(accountId, accountToken);//主帐号，主帐号令牌
            api.setAppId(appId);//应用程序id
            try
            {
                if (isInit)
                {
                    var messageTypeList = ProfileCommon.GetDictItemList(DictEnum.DictType.SendMessageType.ToString());
                    //string[] data = { code, ProfileCommon.GetDictItemValue(dictList, DictItemEnum.GotyeValue.Second.ToString()) + "分钟" };// "模版内容数据"
                    Dictionary<string, object> retData = api.SendTemplateSMS(mobile, tempId, tempContent);//模版id
                    object statusCode = "160050";   //短信发送失败
                    object statusMsg = "短信发送失败";
                    retData.TryGetValue("statusCode", out statusCode);
                    retData.TryGetValue("statusMsg", out statusMsg);
                    if (statusCode.ToString().Trim() != "000000")
                    {
                        var clList = ProfileCommon.GetDictItemList(DictEnum.DictType.Chuanglan.ToString());
                        var clTempList = ProfileCommon.GetDictItemList(DictEnum.DictType.ChuanglanTemp.ToString());
                        string postUrl = "http://222.73.117.158/msg/HttpBatchSendSM";
                        string posurl = string.Format("{0}?account={1}&pswd={2}&mobile={3}&msg={4}&needstatus=true", "http://" + (postUrl.Contains("http://") ? postUrl.Replace("http://", "") : postUrl), "tasike", "Tasike123", mobile, HttpUtility.UrlEncode(ProfileCommon.GetDictItemValue(clTempList, tempId.Trim()), Encoding.UTF8), "");

                        string reponseStr = "";
                        HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(posurl);
                        try
                        {
                            HttpWebResponse httpWResp = (HttpWebResponse)httpWReq.GetResponse();
                            StreamReader reader = new StreamReader(httpWResp.GetResponseStream(), Encoding.UTF8);
                            reponseStr = reader.ReadToEnd();
                            reader.Close();
                        }
                        catch (WebException ex)
                        {
                            reponseStr = "";
                        }
                        var reponseArray = reponseStr.Split(',');
                        if (reponseArray.Length == 2)
                        {
                            if (reponseArray[1].IndexOf("0\n") == 0)  //成功
                            {
                                mmr = new MSendMsgResult() { statusCode = "000000", statusMsg = "发送成功", SendType = Convert.ToInt32(ProfileCommon.GetDictItemValue(messageTypeList, DictItemEnum.SendMessageTypeValue.Chuanglan.ToString())) };
                            }
                            else
                            {
                                string resultMsg = "";
                                var clResultList = ProfileCommon.GetDictItemList(DictEnum.DictType.ChuanglanResult.ToString());
                                resultMsg = ProfileCommon.GetDictItemValue(clResultList, reponseArray[1]);
                                mmr = new MSendMsgResult() { statusCode = reponseArray[1].ToString(), statusMsg = resultMsg.ToString(), SendType = Convert.ToInt32(ProfileCommon.GetDictItemValue(messageTypeList, DictItemEnum.SendMessageTypeValue.Gotye.ToString())) };
                            }
                        }
                    }
                    else
                    {
                        mmr = new MSendMsgResult() { statusCode = statusCode.ToString(), statusMsg = statusMsg.ToString(), SendType = Convert.ToInt32(ProfileCommon.GetDictItemValue(messageTypeList, DictItemEnum.SendMessageTypeValue.Gotye.ToString())) };
                    }
                }
                else
                {
                    ret = "初始化失败";
                }
            }
            catch (Exception exc)
            {
                ret = exc.Message;
            }
            return mmr;
        }
        #endregion

        #region 验证语音验证码
        /// <summary>
        /// 验证语音验证码
        /// </summary>
        /// <param name="code">验证码</param>
        /// <param name="mobile">手机号码</param>
        /// <returns></returns>
        /// <remarks>added by zengbo date:2016-07-20</remarks>
        public static string VoiceVerify(string code, string mobile)
        {
            string ret = null;

            CCPRestSDK.CCPRestSDK api = new CCPRestSDK.CCPRestSDK();
            //ip格式如下，不带https://
            var dictList = ProfileCommon.GetDictItemList(DictEnum.DictType.YunTongXun.ToString());
            string host = ProfileCommon.GetDictItemValue(dictList, DictItemEnum.YunTongXunValue.Host.ToString());
            string port = ProfileCommon.GetDictItemValue(dictList, DictItemEnum.YunTongXunValue.Port.ToString());
            string accountId = ProfileCommon.GetDictItemValue(dictList, DictItemEnum.YunTongXunValue.AccountSid.ToString());
            string accountToken = ProfileCommon.GetDictItemValue(dictList, DictItemEnum.YunTongXunValue.AuthToken.ToString());
            string appId = ProfileCommon.GetDictItemValue(dictList, DictItemEnum.YunTongXunValue.SmsAppId.ToString());
            string tempId = ProfileCommon.GetDictItemValue(dictList, DictItemEnum.YunTongXunValue.TempId.ToString());
            bool isInit = api.init(host, port);
            api.setAccount(accountId, accountToken);//主帐号，主帐号令牌
            api.setAppId(appId);//应用程序id
            try
            {
                if (isInit)
                {
                    Dictionary<string, object> retData = api.VoiceVerify(mobile, code, "", "3", "");
                    Code myCode = new Code();
                    ret = myCode.getDictionaryData(retData);
                }
                else
                {
                    ret = "初始化失败";
                }
            }
            catch (Exception exc)
            {
                ret = exc.Message;
            }
            return ret;

        }
        #endregion

        #region 验证短信验证码
        /// <summary>
        /// 验证短信验证码
        /// </summary>
        /// <param name="mobile">手机号码.</param>
        /// <param name="code">验证码.</param>
        /// <param name="bussinessType">业务类型：0-注册 1-修改登录密码 2-修改支付密码 3-忘记登录密码 4-忘记支付密码 5-验证手机号码 6-修改手机号码</param>
        /// <returns>System.Boolean.</returns>
        /// <remarks>added by zengbo date:2016-07-21</remarks>
        public static ResultInfo CheckCode(string mobile, string code, int bussinessType)
        {
            ResultInfo info = new ResultInfo();
            info.Status = ErrorStatus.S0004;

            using (CaYiCaEntities context = new CaYiCaEntities())
            {
                DateTime verifyTime = DateTime.Now;
                VerifyCode verifyCode = context.VerifyCode.Where(p => p.PhoneNo == mobile.Trim() && p.BussinessType == bussinessType && p.VerifyFlag == 0 && p.VerifyNum < 4).OrderByDescending(c => c.SendTime).FirstOrDefault();
                if (verifyCode != null)
                {
                    if (verifyCode.Code.Equals(code.Trim()))
                    {
                        if (verifyCode.ValidTime < DateTime.Now)
                        {
                            verifyCode.VerifyFlag = 3;
                            info.Status = ErrorStatus.S0003;
                        }
                        else
                        {
                            verifyCode.VerifyFlag = 1;
                            info.Status = ErrorStatus.S0001;
                        }
                    }
                    verifyCode.VerifyNum += 1;
                    context.SaveChanges();
                }
                else
                {
                    info.Status = ErrorStatus.S0004;
                }
                return info;
            }
        }
        #endregion

        #region APP版本更新
        /// <summary>
        /// APP版本更新
        /// </summary>
        /// <param name="appId">APP编号</param>
        /// <param name="appVersion">当前版本号</param>
        /// <param name="os">手机操作系统:Android,ios</param>
        /// <returns>升级信息</returns>
        /// <remarks>added by zengbo date:2016-07-21</remarks>
        public static ResultSingle<MUpgradeVersion> Upgrade(string appId, string appVersion, string os)
        {
            var dictList = Boat.Business.Common.ProfileCommon.GetDictItemList(DictEnum.DictType.AppUpdate.ToString());
            MUpgradeVersion newVersion = new MUpgradeVersion();
            var IsOpen = 0;
            var compareVersion = dictList.Where(p => p.ItemName == DictItemEnum.AppUpdateValue.CompareVersion.ToString()).FirstOrDefault().ItemValue;
            if (string.Equals(os, "android", StringComparison.OrdinalIgnoreCase))
            {
                IsOpen = int.Parse(dictList.Where(p => p.ItemName == DictItemEnum.AppUpdateValue.AndroidIsOpen.ToString()).FirstOrDefault().ItemValue);
            }
            else if (string.Equals(os, "ios", StringComparison.OrdinalIgnoreCase))
            {
                IsOpen = int.Parse(dictList.Where(p => p.ItemName == DictItemEnum.AppUpdateValue.IosIsOpen.ToString()).FirstOrDefault().ItemValue);
            }
            if (IsOpen == 1)
            {
                var updateVersion = dictList.Where(p => p.ItemName == DictItemEnum.AppUpdateValue.UpdateVersion.ToString()).FirstOrDefault().ItemValue;//以资源更新版本为最新版本号比对
                var configAppId = dictList.Where(p => p.ItemName == DictItemEnum.AppUpdateValue.AppId.ToString()).FirstOrDefault().ItemValue;
                if (appId == configAppId && appVersion != updateVersion)
                {
                    var packageSize = "";
                    var downloadUrl = "";
                    var updateType = int.Parse(dictList.Where(p => p.ItemName == DictItemEnum.AppUpdateValue.UpdateType.ToString()).FirstOrDefault().ItemValue);
                    //1整包升级，2资源升级
                    if (updateType == 2)
                    {
                        string[] appVersionArr = appVersion.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                        string[] compareVersionArr = compareVersion.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                        int appVersion0 = int.Parse(appVersionArr[0]),
                            appVersion1 = int.Parse(appVersionArr[1]),
                            appVersion2 = int.Parse(appVersionArr[2]),
                            compareVersion0 = int.Parse(compareVersionArr[0]),
                            compareVersion1 = int.Parse(compareVersionArr[1]),
                            compareVersion2 = int.Parse(compareVersionArr[2]);
                        //APP版本小于需要整包更新的版本就整包升级
                        if (appVersion0 < compareVersion0 || appVersion1 < compareVersion1 || appVersion2 < compareVersion2)
                        {
                            updateType = 1;
                            packageSize = dictList.Where(p => p.ItemName == DictItemEnum.AppUpdateValue.WholePackageSize.ToString()).FirstOrDefault().ItemValue;
                            if (string.Equals(os, "android", StringComparison.OrdinalIgnoreCase))
                            {
                                downloadUrl = dictList.Where(p => p.ItemName == DictItemEnum.AppUpdateValue.AndroidPackageUrl.ToString()).FirstOrDefault().ItemValue;
                            }
                            else
                            {
                                downloadUrl = dictList.Where(p => p.ItemName == DictItemEnum.AppUpdateValue.IosPackageUrl.ToString()).FirstOrDefault().ItemValue;
                            }
                            updateVersion = compareVersion;
                        }
                        else
                        {
                            packageSize = dictList.Where(p => p.ItemName == DictItemEnum.AppUpdateValue.ResourceSize.ToString()).FirstOrDefault().ItemValue;
                            downloadUrl = dictList.Where(p => p.ItemName == DictItemEnum.AppUpdateValue.ResourceUrl.ToString()).FirstOrDefault().ItemValue;
                        }
                    }
                    else
                    {
                        packageSize = dictList.Where(p => p.ItemName == DictItemEnum.AppUpdateValue.WholePackageSize.ToString()).FirstOrDefault().ItemValue;
                        if (string.Equals(os, "android", StringComparison.OrdinalIgnoreCase))
                        {
                            downloadUrl = dictList.Where(p => p.ItemName == DictItemEnum.AppUpdateValue.AndroidPackageUrl.ToString()).FirstOrDefault().ItemValue;
                        }
                        else
                        {
                            downloadUrl = dictList.Where(p => p.ItemName == DictItemEnum.AppUpdateValue.IosPackageUrl.ToString()).FirstOrDefault().ItemValue;
                        }
                        updateVersion = compareVersion;
                    }
                    //更新类型,下载地址 ,安装包大小,app新版本号                  
                    newVersion.HasVersion = true;
                    newVersion.UpdateType = updateType;
                    newVersion.DownloadUrl = downloadUrl;
                    newVersion.PackageSize = packageSize;
                    newVersion.Version = updateVersion;
                    newVersion.NewFunction = dictList.Where(p => p.ItemName == DictItemEnum.AppUpdateValue.NewFunction.ToString()).FirstOrDefault().ItemValue;
                }
                else
                {
                    newVersion.HasVersion = false;
                }
            }
            else
            {
                newVersion.HasVersion = false;
            }
            ResultSingle<MUpgradeVersion> result = new ResultSingle<MUpgradeVersion>();
            result.Status = ErrorStatus.S0001;
            result.ReturnObject = newVersion;

            return result;
        }
        #endregion

        #region 验证随机验证码
        /// <summary>
        /// 验证随机验证码
        /// </summary>
        /// <param name="randomCode">随机验证码.</param>
        /// <returns>验证通过返回true，否则false</returns>
        /// <remarks>add by zengbo,2016-06-01 11:19:00 </remarks>
        public static bool VerifyRandomCode(string randomCode)
        {
            string key = randomCode.Substring(0, 6);//加密后的随机数
            string mytime = randomCode.Substring(6, 6);//当前时间
            string mima = "000000";
            log.Debug("key=" + key + ";mytime=" + mytime);
            byte[] result = Encoding.Default.GetBytes(mytime);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);

            string myText = BitConverter.ToString(output).Replace("-", "");

            int j = 0;
            for (int i = myText.Length; i > 0; i--)
            {
                int tmp = 0;
                string stmp = myText.Substring(i - 1, 1);
                if (int.TryParse(stmp, out tmp))
                {
                    j++;
                    if (j == 7)
                        break;
                    mima = mima + (9 - tmp).ToString();
                }
            }
            mima = mima.Substring(mima.Length - 6, 6);

            //log.Debug("mima=" + mima);

            if (mima == key)
            {
                //解密时间
                string mytime2 = "";//当前时间
                for (int i = 0; i < mytime.Length; i++)
                {
                    string stmp = mytime.Substring(i, 1);
                    int tmp1 = 0;
                    int tmp2 = 0;
                    if (int.TryParse(stmp, out tmp1))
                    {
                        if (tmp1 == 8)
                        {
                            tmp1 = -2;
                        }
                        else if (tmp1 == 9)
                        {
                            tmp1 = -1;
                        }
                        tmp2 = Math.Abs(7 - tmp1);
                        mytime2 = mytime2 + tmp2.ToString();
                    }
                }
                mytime2 = mytime2.Substring(0, 4);
                string mydate1 = DateTime.Now.AddMinutes(-1).ToString("HHmm");
                string mydate2 = DateTime.Now.ToString("HHmm");
                string mydate3 = DateTime.Now.AddMinutes(1).ToString("HHmm");

                //log.Debug("mytime2=" + mytime2 + ";mydate1=" + mydate1 + ";mydate2=" + mydate2 + ";mydate3=" + mydate3);
                if (mytime2 == mydate1 || mytime2 == mydate2 || mytime2 == mydate3)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 微信客服消息
        /// <summary>
        /// 微信客服消息（文字消息）
        /// </summary>
        /// <param name="openId">微信开放Id.</param>
        /// <param name="content">消息内容.</param>
        /// <remarks>add by liaojiahua,2016-09-08 16:14:53 </remarks>
        public static void CustomeMessageSendText(string openId, string content)
        {
            try
            {
                MAccessToken atToken = GetWeChatToken();
                if (atToken == null)
                {
                    return;
                }

                byte[] data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new
                {
                    touser = openId,
                    msgtype = "text",
                    text = new { content = content }
                }));
                string url = "https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=" + atToken.Token;
                HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "POST";
                httpRequest.ContentType = "application/json; charset=utf-8";
                httpRequest.ContentLength = data.Length;
                Stream s = httpRequest.GetRequestStream();
                s.Write(data, 0, data.Length);
                s.Close();
            }
            catch (Exception ex)
            {
                log.Error("Boat.Business.Common.MessageCommon.CustomeMessageSendText,微信客服消息（文字消息）发生错误", ex);
            }
        }

        #endregion

        #region 获取ToKen
        /// <summary>
        /// Gets to ten.
        /// </summary>
        /// <returns>AccessTokenModel.</returns>
        /// <remarks>add by liaojiahua,2016-08-31 14:33:46 </remarks> 
        public static MAccessToken GetWeChatToken()
        {
            try
            {
                string appid = ConfigurationManager.AppSettings["WeChatAppID"],
                 secret = ConfigurationManager.AppSettings["WeChatAppSecret"];

                HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", appid, secret));
                using (HttpWebResponse response = (HttpWebResponse)httpRequest.GetResponse())
                {
                    var rs = response.GetResponseStream();
                    StreamReader sr = new StreamReader(rs);
                    var respContent = sr.ReadToEnd();
                    sr.Close();
                    response.Close();
                    return JsonConvert.DeserializeObject<MAccessToken>(respContent);
                }
            }
            catch (Exception ex)
            {
                log.Error("Boat.Business.Common.MessageCommon.GetToken,获取Token发生错误", ex);
            }
            return null;
        }
        #endregion
    }
}
