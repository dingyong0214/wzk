using EmitMapper;
using System.Collections.Generic;

namespace WZK.Business.Admin.Tool
{
    public class ModelMapping
    {
        /// <summary>
        /// 实体拷贝
        /// </summary>
        /// <typeparam name="S">源类型</typeparam>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="obj">源类型对象</param>
        /// <returns></returns>
        /// <remarks>add by 赵小江，2015年10月28日, PM 01:55:12</remarks>
        public static T ChangeModel<S, T>(S obj)
        {
            var mapperMCateToCate = ObjectMapperManager.DefaultInstance.GetMapper<S, T>();
            return mapperMCateToCate.Map(obj);
        }

        /// <summary>
        /// 实体拷贝
        /// </summary>
        /// <typeparam name="S">源类型</typeparam>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="obj">源类型对象</param>
        /// <returns></returns>
        /// <remarks>add by 赵小江，2015年10月28日, PM 01:55:12</remarks>
        public static T ChangeModel<S, T>(S obj, IMappingConfigurator config)
        {
            var mapperMCateToCate = ObjectMapperManager.DefaultInstance.GetMapper<S, T>(config);
            return mapperMCateToCate.Map(obj);
        }

        /// <summary>
        /// 实体拷贝
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        /// <remarks>add by 赵小江，2015年10月28日, PM 01:55:12</remarks>
        public static IEnumerable<T> ChangeModelList<S, T>(IEnumerable<S> list)
        {
            var mapperMCateToCate = ObjectMapperManager.DefaultInstance.GetMapper<S, T>();
            return mapperMCateToCate.MapEnum(list);
        }
    }
}
