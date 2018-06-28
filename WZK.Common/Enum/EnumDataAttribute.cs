using System;
using System.Collections.Generic;

namespace WZK.Common
{
    /// <summary>
    /// 枚举自定义特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class EnumDataAttribute : Attribute
    {
        public EnumDataAttribute(string name)
        {
            this.Name = name;
            this.URL = "javascript:;;";
        }

        public EnumDataAttribute() { }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public object Title { get; set; }

        /// <summary>
        /// 成功内容
        /// </summary>
        public object SuccessContent { get; set; }

        /// <summary>
        /// 失败内容
        /// </summary>
        public object FailureContent { get; set; }

        /// <summary>
        /// URL
        /// </summary>
        public string URL { get; set; }

        private int _flag = -1;

        /// <summary>
        /// 类型(0、项目,1、融资,2、用户)
        /// </summary>
        public int Flag
        {
            get { return _flag; }
            set { _flag = value; }
        }
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class MessageAttribute : Attribute { }

    /// <summary>
    /// 记录日志
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class Visit_LogAttribute : Attribute
    {
        public Visit_LogAttribute(string name)
        {
            this.URL = name;
        }
        public Visit_LogAttribute() { }
        public string URL { get; set; }
    }

    /// <summary>
    /// 枚举类型值
    /// </summary>
    public static class EnumData
    {
        /// <summary>
        /// 获取属性值描述
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static EnumDataAttribute GetData(this System.Enum obj)
        {
            return GetData<EnumDataAttribute>(obj);
        }
        public static EnumDataAttribute GetData(string obj)
        {
            return GetData<EnumDataAttribute>(obj);
        }
        /// <summary>
        /// 获取属性描述
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T GetData<T>(System.Enum obj) where T : System.Attribute
        {
            Type _enumType = obj.GetType();
            return ((T)Attribute.GetCustomAttribute(_enumType.GetField(System.Enum.GetName(_enumType, obj)), typeof(T)));
        }
        /// <summary>
        /// 获取属性描述
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T GetData<T>(string obj) where T : System.Attribute
        {
            Type _enumType = obj.GetType();
            return ((T)Attribute.GetCustomAttribute(_enumType.GetField(obj), typeof(T)));
        }
        /// <summary>
        /// 所有特性集合
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <remarks>调用时候 传入枚举返回枚举所有特型集</remarks>
        public static List<EnumDataAttribute> GetListData(this Type obj)
        {
            List<EnumDataAttribute> list = new List<EnumDataAttribute>();
            var fields = obj.GetFields(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
            foreach (var fi in fields)
                list.Add((EnumDataAttribute)Attribute.GetCustomAttribute(fi, typeof(EnumDataAttribute)));
            return list;
        }

        /// <summary>
        /// 日志
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool Visit_Log(this System.Enum obj)
        {
            return obj.IsExist(typeof(Visit_LogAttribute));
        }

        /// <summary>
        /// 消息
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool SendMessage(this System.Enum obj)
        {
            return obj.IsExist(typeof(MessageAttribute));
        }

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private static bool IsExist(this System.Enum obj, Type type)
        {
            Type _enumType = obj.GetType();
            return Attribute.IsDefined(_enumType.GetField(obj.ToString()), type);
        }
    }
}

