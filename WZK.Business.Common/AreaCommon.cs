using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
usingBoat.Common;
using Boat.Entity;
using Boat.Models;

namespace Boat.Business.Common
{
    /// <summary>
    /// 获取上线行政区域
    /// </summary>
    /// <remarks>added by xiongchonglong,2016-08-17</remarks>
    public class AreaCommon : BaseBusiness
    {
        #region 通过父级获取下属行政区域
        /// <summary>
        /// 通过父级获取下属行政区域
        /// </summary>
        /// <returns></returns>
        /// <remarks>add by xiongchonglong  addTime:2016.12.28 14:46</remarks>
        public ResultList<Area> GetChildArea(string parentCode, int state = -1)
        {
            ResultList<Area> info = new ResultList<Area>();
            try
            {
                using (CaYiCaEntities db = new CaYiCaEntities())
                {
                    int pCode = 0;
                    if (string.IsNullOrEmpty(parentCode))   //没有传值,获取省份
                    {
                        var tt = db.Area.Where(p => p.Depth == 0).FirstOrDefault();
                        if (tt != null)
                        {
                            pCode = tt.AreaCode;
                        }
                    }
                    else
                    {
                        int.TryParse(parentCode, out pCode);
                    }
                    var temp = from a in db.Area
                               where a.ParentCode == pCode
                               select new
                               {
                                   a.AreaCode,
                                   a.Name,
                                   a.State,
                                   a.ParentCode
                               };
                    if (state > 0)
                    {
                        temp = temp.Where(c => c.State == state);
                    }

                    info.ReturnList = temp.ToList().Select(a => new Area() { AreaCode = a.AreaCode, Name = a.Name, ParentCode = a.ParentCode }).ToList();
                    info.Status = ErrorStatus.S0001;
                }
            }
            catch (Exception ex)
            {
                info.Status = ErrorStatus.S0500;
                log.Error("Boat.Business.Admin.AdminUserManage.GetChildArea   通过父级获取下属行政区域Error：", ex);
            }
            return info;
        }
        #endregion

        #region 获取上线行政区域
        /// <summary>
        /// 获取上线行政区域
        /// </summary>
        /// <param name="parentAreaCode">上级行政区域</param>
        /// <param name="dtMaxTime">最新数据时间</param>
        /// <param name="updateFlag">是否重新获取</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="state">状态：0-下线 1-上线</param>
        /// <returns>上线行政区域</returns>
        /// <remarks>added by zengbo date:2016-07-30</remarks>
        public static AppResultList<OnlineArea> GetOnlineAreaList(string parentAreaCode, ref DateTime dtMaxTime, bool updateFlag = true, string timestamp = "", int? state = 1)
        {
            AppResultList<OnlineArea> result = new AppResultList<OnlineArea>();
            result.Status = ErrorStatus.S0001;
            if (!updateFlag)
            {
                return result;
            }
            int myParentAreaCode = 100000;
            int.TryParse(parentAreaCode, out myParentAreaCode);
            try
            {
                using (CaYiCaEntities context = new CaYiCaEntities())
                {
                    var areaList = context.Area.Where(p => p.Depth >= 1);
                    if (state.HasValue)
                    {
                        areaList = areaList.Where(p => p.State == state.Value);
                    }
                    var dataOneSql = areaList.OrderBy(a => a.AreaCode);
                    var dataMaxTime = dataOneSql.Max(p => p.UpdateTime == null ? p.InsertTime : p.UpdateTime.Value);
                    dtMaxTime = dataMaxTime > dtMaxTime ? dataMaxTime : dtMaxTime;

                    result.Timestamp = dtMaxTime.ToString("yyyyMMddHHmmssfff");
                    if (!string.IsNullOrWhiteSpace(timestamp) && timestamp == dtMaxTime.ToString("yyyyMMddHHmmssfff"))
                    {
                        result.NeedUpdate = false;
                        return result;
                    }
                    result.NeedUpdate = true;

                    var dataOne = dataOneSql.ToList();
                    List<OnlineArea> list = new List<OnlineArea>();
                    List<Area> province = new List<Area>();
                    //省
                    if (string.IsNullOrWhiteSpace(parentAreaCode))
                    {
                        myParentAreaCode = 100000;
                    }
                    province = dataOne.Where(c => c.ParentCode == myParentAreaCode && c.State == 1).OrderBy(c => c.AreaCode).ToList();

                    //市
                    foreach (var shen in province)
                    {
                        var cityList = dataOne.Where(c => c.ParentCode == shen.AreaCode).OrderBy(c => c.AreaCode).ToList();

                        OnlineArea area = new OnlineArea()
                        {
                            value = shen.AreaCode,
                            text = shen.Name,
                            children = new List<OnlineArea>()
                        };
                        //区
                        foreach (var city in cityList)
                        {
                            var sqlQuList = dataOne.Where(c => c.ParentCode == city.AreaCode).OrderBy(c => c.AreaCode).ToList();
                            OnlineArea areacity = new OnlineArea()
                            {
                                value = city.AreaCode,
                                text = city.Name,
                                children = new List<OnlineArea>()
                            };
                            area.children.Add(areacity);

                            foreach (var qu in sqlQuList)
                            {
                                var sqlTownList = dataOne.Where(c => c.ParentCode == qu.AreaCode).OrderBy(c => c.AreaCode).ToList();
                                areacity.children.Add(new OnlineArea()
                                {
                                    value = qu.AreaCode,
                                    text = qu.Name,
                                    children = sqlTownList.Select(v => new OnlineArea
                                    {
                                        value = v.AreaCode,
                                        text = v.Name,
                                    }).ToList()
                                });
                            }
                        }
                        list.Add(area);
                    }
                    result.ReturnList = list;
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.Status = ErrorStatus.S0002;
                log.Error("Boat.Business.CommonProfile.GetOnlineAreaList,获取上线行政区域错误：", ex);
                return result;
            }
        }
        #endregion

        #region 获取区域对象
        public static Area GetArea(int areaCode)
        {
            if (areaCode <= 0)
            {
                return null;
            }
            using (CaYiCaEntities context = new CaYiCaEntities())
            {
                return context.Area.FirstOrDefault(p => p.AreaCode == areaCode);
            }
        }
        #endregion

        #region 根据经纬度获取区域对象
        private static string[] arrBaiduAK = { "tHnF3940LyNh1cp3xAvvZjXTyS7XNhj9", "jmQpyp6kSudkRXce8kz6anGafKLuoAdy" };
        /// <summary>
        /// 根据经纬度获取区域对象
        /// </summary>
        /// <returns>Area.</returns>
        public static Area GetAreaByLongLat(double longitude, double latidute)
        {
            if (longitude == 0 || latidute == 0)//如果没有传位置信息时，默认返回深圳市
            {
                return GetArea(440300);
            }
            Area area = null;
            var index = new Random().Next(0, arrBaiduAK.Length - 1);
            string apiUrl = string.Format("http://api.map.baidu.com/geocoder/v2/?location={0},{1}&output=json&ak={2}&coordtype=wgs84ll", latidute, longitude, arrBaiduAK[index]);
            LongLatAddress address = Json.Deserialize<LongLatAddress>(Tool.HttpGet(apiUrl));
            if (address != null && address.status == 0 && address.result != null && address.result.addressComponent != null)
            {
                string provice = address.result.addressComponent.province;
                string city = address.result.addressComponent.city;
                string district = address.result.addressComponent.district;

                using (CaYiCaEntities context = new CaYiCaEntities())
                {
                    var matchProvice = context.Area.Where(p => p.Name == provice || provice.Contains(p.Name)).ToList();
                    var matchCity = context.Area.Where(p => p.Name == city || city.Contains(p.Name)).ToList();
                    var matchDistrict = context.Area.Where(p => p.Name == district || district.Contains(p.Name)).ToList();
                    if (matchProvice.Count > 0 && matchCity.Count > 0 && matchDistrict.Count > 0)
                    {
                        Dictionary<int, Area> dicProvice = new Dictionary<int, Area>();
                        Dictionary<int, Area> dicCity = new Dictionary<int, Area>();
                        Dictionary<int, Area> dicDistrict = new Dictionary<int, Area>();
                        Dictionary<int, int> dicDistrictWeight = new Dictionary<int, int>();
                        foreach (var item in matchProvice)
                        {
                            dicProvice[item.AreaCode] = item;
                        }
                        foreach (var item in matchCity)
                        {
                            dicCity[item.AreaCode] = item;
                        }
                        foreach (var item in matchDistrict)
                        {
                            var arrGuide = item.Guide.Split(new char[] { '^' }, StringSplitOptions.RemoveEmptyEntries);
                            if (arrGuide.Length < 4)
                            {
                                continue;
                            }
                            var itemproviceId = ConvertTryParse.TryParseInt(arrGuide[1]);
                            var itemcityId = ConvertTryParse.TryParseInt(arrGuide[2]);
                            var itemdistrictId = ConvertTryParse.TryParseInt(arrGuide[3]);
                            if (!dicDistrictWeight.ContainsKey(item.AreaCode))
                            {
                                dicDistrictWeight[item.AreaCode] = 1;
                            }
                            if (dicProvice.ContainsKey(itemproviceId))
                            {
                                dicDistrictWeight[item.AreaCode]++;
                            }
                            if (dicCity.ContainsKey(itemcityId))
                            {
                                dicDistrictWeight[item.AreaCode] = dicDistrictWeight[item.AreaCode] + 2;
                            }
                            dicDistrict[item.AreaCode] = item;
                        }
                        var resultList = dicDistrictWeight.OrderByDescending(p => p.Value).ToList();
                        if (resultList != null)
                        {
                            area = dicDistrict[resultList[0].Key];
                        }
                    }
                }
            }
            return area;
        }
        #endregion

        #region 根据区域AreaCode获取同级区域列表
        /// <summary>
        /// 根据区域AreaCode获取同级区域列表
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <param name="status">The status.</param>
        /// <returns>List&lt;Category&gt;.</returns>
        /// <remarks>add by 谢四仁,2017-02-14 10:47:04 </remarks>
        public List<Area> GetCurrentAreaList(int areaCode)
        {
            List<Area> result = new List<Area>();
            using (CaYiCaEntities context = new CaYiCaEntities())
            {
                var currentArea = context.Area.FirstOrDefault(p => p.AreaCode == areaCode);
                if (currentArea != null)
                {
                    var list = context.Area.Where(p => p.ParentCode == currentArea.ParentCode);
                    result = list.OrderBy(p => p.DisplayOrder).ToList();
                }
            }
            return result;
        }
        #endregion

        #region 根据区域AreaCode获取上级区域编码
        /// <summary>
        /// 根据区域AreaCode获取上级区域编码
        /// </summary>
        /// <param name="areaCode">子级编码</param>
        /// <returns>List&lt;Category&gt;.</returns>
        /// <remarks>add by Liaojiahua,2017-03-06 10:26:13 </remarks>
        public AreaViewModel GetAreasCode(int areaCode)
        {
            AreaViewModel result = new AreaViewModel();
            using (CaYiCaEntities context = new CaYiCaEntities())
            {
                var currentArea = context.Area.FirstOrDefault(p => p.AreaCode == areaCode); //当前编码
                if (currentArea != null)
                {
                    var second = context.Area.FirstOrDefault(p => p.AreaCode == currentArea.ParentCode);//次级
                    if (second != null)
                    {
                        var three = context.Area.FirstOrDefault(c => c.AreaCode == second.ParentCode);//三级
                        if (three != null && three.ParentCode != 100000)
                        {
                            var four = context.Area.FirstOrDefault(c => c.AreaCode == three.ParentCode);//四级
                            if (four != null)
                            {
                                result.Province = four.AreaCode;
                                result.City = three.AreaCode;
                                result.Area = second.AreaCode;
                                result.Town = currentArea.AreaCode;
                            }
                        }
                        else
                        {
                            result.Province = three.AreaCode;
                            result.City = second.AreaCode;
                            result.Area = currentArea.AreaCode;
                            result.Town = -1;
                        }
                    }
                }
            }
            return result;
        }
        #endregion

        #region 根据区域AreaCode获取地域全程
        /// <summary>
        /// 根据区域AreaCode获取地域全程
        /// </summary>
        /// <param name="areaCode">子级编码</param>
        /// <returns>List&lt;Category&gt;.</returns>
        /// <remarks>add by Liaojiahua,2017-03-06 10:26:13 </remarks>
        public string GetAreasName(int areaCode)
        { 
            string areaName = string.Empty;
            using (CaYiCaEntities context = new CaYiCaEntities())
            {
                var currentArea = context.Area.FirstOrDefault(p => p.AreaCode == areaCode); //当前编码
                if (currentArea != null)
                {
                    areaName = currentArea.Name;
                    var second = context.Area.FirstOrDefault(p => p.AreaCode == currentArea.ParentCode);//次级
                    if (second != null)
                    {
                        areaName = second.Name+areaName;
                        var three = context.Area.FirstOrDefault(c => c.AreaCode == second.ParentCode);//三级
                        if (three != null && three.ParentCode != 100000)
                        {
                            areaName = three.Name+areaName;
                            var four = context.Area.FirstOrDefault(c => c.AreaCode == three.ParentCode);//四级
                            if (four != null)
                            {
                                areaName = four.Name+areaName;
                            }
                        }
                        else
                        {
                            areaName = three.Name+areaName;
                        }
                    }
                }
            }
            return areaName;
        }
        #endregion

        #region 根据区域导航获取父级区域列表
        /// <summary>
        ///根据区域导航获取父级区域列表
        /// </summary>
        /// <param name="areaGuide">区域导航</param>
        /// <returns>List&lt;Area&gt;.</returns>
        /// <remarks>add by 谢四仁,2017-02-25 16:10:12 </remarks>
        public List<Area> GetParentAreaList(int areaCode)
        {
            var area = GetArea(areaCode);
            if (area == null)
            {
                return null;
            }
            if (string.IsNullOrWhiteSpace(area.Guide))
            {
                return null;
            }
            using (CaYiCaEntities context = new CaYiCaEntities())
            {
                return context.Area.Where(p => p.Depth >= 1 && area.Guide.Contains(p.Guide)).ToList();
            }
        } 
        #endregion
    }
}
