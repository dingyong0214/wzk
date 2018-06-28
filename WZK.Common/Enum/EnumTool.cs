using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Collections;

namespace WZK.Common
{
    /// <summary>
    /// 枚举工具
    /// author:lorne
    /// createTime:2015-11-02
    /// </summary>
    public static class EnumTool
    {
        /// <summary>
        /// 扩展方法，用于获取枚举的中文描述信息
        /// </summary>
        /// <param name="val">枚举值</param>
        /// <returns>中文描述</returns>
        /// <remarks>author:lorne date:2015-11-02</remarks>
        public static string GetDesc(this System.Enum val)
        {
            Type type = val.GetType();
            string name = System.Enum.GetName(type, val);
            object[] objs = type.GetField(name).GetCustomAttributes(typeof(DescriptionAttribute), false);//获取枚举项的特性
            if (objs.Length > 0)
                return (objs[0] as DescriptionAttribute).Description;
            else
                return name;//Description特性不存在时返回枚举名
        }

        /// <summary>
        /// 枚举转换成下拉列表.
        /// </summary>
        /// <param name="type">枚举</param>
        /// <param name="isList">是否显示在列表(默认否).</param>
        /// <param name="isDefault">是否需要默认项(默认true)</param>
        /// <returns>List&lt;SelectListItem&gt;.</returns>
        /// <remarks>add by dingyong,2016-11-02 16:03:51 </remarks>
        public static List<SelectListItem> ToSelectList(this Type type, bool isList = false,bool isDefault=true)
        {
            if (type.IsEnum)
            {
                List<SelectListItem> list = new List<SelectListItem>();
                Array _enumValues = System.Enum.GetValues(type);
                if(isDefault)
                    list.Add(new SelectListItem { Text = isList ? "全部" : "--请选择--", Value = "-99" });
                foreach (object value in _enumValues)
                {
                    list.Add(new SelectListItem
                    {
                        Text = GetDesc((System.Enum)value),
                        Value = ((int)value).ToString()
                    });
                }
                return list;
            }
            return new List<SelectListItem>();
        }
        
        /// <summary>
        /// 枚举转换成下拉列表.
        /// </summary>
        /// <param name="type">枚举</param>
        /// <param name="v">默认选中的Value</param>
        /// <param name="isList">是否显示在列表(默认否).</param>
        /// <returns>List&lt;SelectListItem&gt;.</returns>
        /// <remarks>add by dingyong,2016-12-26 16:50:00 </remarks>
        public static List<SelectListItem> ToSelectList(this Type type,string v, bool isList = false)
        {
            if (type.IsEnum)
            {
                List<SelectListItem> list = new List<SelectListItem>();
                Array _enumValues = System.Enum.GetValues(type);
                list.Add(new SelectListItem { Text = isList ? "全部" : "--请选择--", Value = "-99" });
                foreach (object value in _enumValues)
                {
                    SelectListItem item = new SelectListItem();
                    if (v== ((int)value).ToString())
                    {
                        list.Add(new SelectListItem
                        {
                            Text = GetDesc((System.Enum)value),
                            Value = ((int)value).ToString(),
                            Selected = true
                        });
                    }
                    else
                    {
                        list.Add(new SelectListItem
                        {
                            Text = GetDesc((System.Enum)value),
                            Value = ((int)value).ToString()
                        });
                    }
                }
                return list;
            }
            return new List<SelectListItem>();
        }
    }
}
