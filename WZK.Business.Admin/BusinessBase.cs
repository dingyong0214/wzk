using System;
using System.Collections.Generic;
using System.Linq;
using WZK.Common;
using System.Data;

namespace WZK.Business.Admin
{
    /// <summary>
    /// 业务基类
    /// author:XXX
    /// createTime:2015-11-05
    /// </summary>
    public class BusinessBase
    {
        public int PageSize { get { return WZK.Common.ConfigData.PageConfig.PageSize; } }

        protected static Logger log = new Logger();

        /// <summary>
        /// list转datatable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">需要转换的list</param>
        /// <returns></returns>
        /// <remarks>author:wcy date:2016-04-05</remarks>
        public DataTable ToDataSet<T>(IList<T> list)
        {
            Type elementType = typeof(T);
            var t = new DataTable();
            elementType.GetProperties().ToList().ForEach(propInfo => t.Columns.Add(propInfo.Name, Nullable.GetUnderlyingType(propInfo.PropertyType) ?? propInfo.PropertyType));
            foreach (T item in list)
            {
                var row = t.NewRow();
                elementType.GetProperties().ToList().ForEach(propInfo => row[propInfo.Name] = propInfo.GetValue(item, null) ?? DBNull.Value);
                t.Rows.Add(row);
            }
            return t;
        } 
    }
}
