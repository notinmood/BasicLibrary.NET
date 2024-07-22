//using HiLand.Utility.Reflection;
//using System;
//using System.Collections.Generic;

//namespace HiLand.Utility.Data
//{
//    /// <summary>
//    /// 类型操作辅助类
//    /// </summary>
//    public static class TypeHelper
//    {
//        /// <summary>
//        /// 根据类型描述通过反射技术获取指定类型的对象
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="typeDescription"></param>
//        /// <returns></returns>
//        public static T Activate<T>(string typeDescription)
//        {
//            Type targetClassType = Type.GetType(typeDescription);
//            return Activate<T>(targetClassType);
//        }

//        /// <summary>
//        /// 根据类型描述通过反射技术获取指定类型的对象
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="type"></param>
//        /// <returns></returns>
//        public static T Activate<T>(Type type)
//        {
//            T target = default(T);

//            try
//            {
//                target = (T)Activator.CreateInstance(type);
//            }
//            catch
//            {
//                //do nothing;
//            }

//            return target;
//        }

//        /// <summary>
//        /// 获取类型带Assembly信息的名称，主要用于反射（其中Assembly名称中不包括版本，语言等信息）
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <returns></returns>
//        public static string GetTypeShortDescription<T>()
//        {
//            return ReflectHelper.GetTypeShortDescription<T>();
//        }

//        /// <summary>
//        /// 获取类型带Assembly信息的名称，主要用于反射（其中Assembly名称中不包括版本，语言等信息）
//        /// </summary>
//        /// <param name="type"></param>
//        /// <returns></returns>
//        public static string GetTypeShortDescription(Type type)
//        {
//            return ReflectHelper.GetTypeShortDescription(type);
//        }

//        /// <summary>
//        /// 判断类型是否为简单数据类型
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <returns></returns>
//        public static bool ConfirmIsSimpleType<T>()
//        {
//            return ConfirmIsSimpleType(typeof(T));
//        }

//        /// <summary>
//        /// 判断类型是否为简单数据类型
//        /// </summary>
//        /// <param name="type"></param>
//        /// <returns></returns>
//        public static bool ConfirmIsSimpleType(Type type)
//        {
//            if (type.IsEnum)
//            {
//                return true;
//            }

//            if (type == typeof(bool))
//            {
//                return true;
//            }

//            if (ConfirmIsNumberType(type) == true)
//            {
//                return true;
//            }

//            if (ConfirmIsStringType(type) == true)
//            {
//                return true;
//            }

//            return false;
//        }

//        /// <summary>
//        /// 判断给定的类型是否为数字类型
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <returns></returns>
//        public static bool ConfirmIsNumberType<T>()
//        {
//            return ConfirmIsNumberType(typeof(T));
//        }

//        /// <summary>
//        /// 判断给定的类型是否为数字类型
//        /// </summary>
//        /// <param name="type"></param>
//        /// <returns></returns>
//        public static bool ConfirmIsNumberType(Type type)
//        {
//            if (type == typeof(Int16)
//                || type == typeof(Int32)
//                || type == typeof(Int64)
//                || type == typeof(Double)
//                || type == typeof(Single)
//                || type == typeof(Decimal)
//                || type.IsEnum)
//            {
//                return true;
//            }
//            else
//            {
//                return false;
//            }
//        }

//        /// <summary>
//        /// 判断给定的类型是否为字符串类型类型
//        /// </summary>
//        /// <param name="type"></param>
//        /// <returns></returns>
//        public static bool ConfirmIsStringType(Type type)
//        {
//            if (type == typeof(String)
//                || type == typeof(Char)
//                || type == typeof(Guid)
//                || type == typeof(DateTime))
//            {
//                return true;
//            }
//            else
//            {
//                return false;
//            }
//        }

//        /// <summary>
//        /// 获取数据友好的显示值
//        /// </summary>
//        /// <param name="data">请必须为简单类型，如果为负责类型其将返回此类型的ToString信息</param>
//        /// <returns></returns>
//        public static string GetFriendlyValue(object data)
//        {
//            return GetFriendlyValue(data, string.Empty);
//        }

//        /// <summary>
//        /// 获取数据友好的显示值
//        /// </summary>
//        /// <param name="data">请必须为简单类型，如果为负责类型其将返回此类型的ToString信息</param>
//        /// <param name="addon">
//        /// 对data操作的修正信息，格式为 datetime:yyyy/MM/dd||enum:zh-CN
//        /// 1、对DateTime类型，此修正信息为日期格式字符串，默认为“yyyy/MM/dd”
//        /// 2、对Enum类型，此修正信息为DisplayCategory，默认值为zh-CN
//        /// </param>
//        /// <returns></returns>
//        public static string GetFriendlyValue(object data, string addon)
//        {
//            if (data == null)
//            {
//                return string.Empty;
//            }

//            Type targetType = data.GetType();
//            string datetimeFormat = "yyyy/MM/dd";
//            string enumDisplayCategory = "zh-CN";

//            Dictionary<string, string> addonDic = StringHelper.SplitToDictionary(addon, ":", "||");
//            if (addonDic.ContainsKey("datetime"))
//            {
//                datetimeFormat = addonDic["datetime"];
//            }

//            if (addonDic.ContainsKey("enum"))
//            {
//                enumDisplayCategory = addonDic["enum"];
//            }

//            //1.如果为日期类型，显示安全的短日期类型
//            if (targetType == typeof(DateTime))
//            {
//                return DateTimeHelper.ToSaftString((DateTime)data, datetimeFormat);
//            }

//            //2.如果是枚举类型，则显示枚举的友好信息
//            if (targetType.IsEnum)
//            {
//                return EnumHelper.GetDisplayValue(data, targetType, enumDisplayCategory);
//            }

//            //0.包括字符串在内，其他类型都返回ToString信息
//            return data.ToString();
//        }
//    }
//}
