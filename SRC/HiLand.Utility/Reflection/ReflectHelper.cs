using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using HiLand.Utility.Data;

namespace HiLand.Utility.Reflection
{
    /// <summary>
    /// 反射技术辅助类
    /// </summary>
    public static class ReflectHelper
    {
        /// <summary>
        /// 获取类型带Assembly信息的名称，主要用于反射（其中Assembly名称中不包括版本，语言等信息）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string GetTypeShortDescription<T>()
        {
            Type type = typeof(T);
            return GetTypeShortDescription(type);
        }

        /// <summary>
        /// 获取类型带Assembly信息的名称，主要用于反射（其中Assembly名称中不包括版本，语言等信息）
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetTypeShortDescription(Type type)
        {
            string longName = type.AssemblyQualifiedName;
            string[] longNameArray = StringHelper.SplitToArray(longName, ",");
            if (longNameArray.Length >= 2)
            {
                longName = string.Format("{0},{1}", longNameArray[0], longNameArray[1]);
            }
            return longName;
        }

        /// <summary>
        /// 通过属性名称获取给定对象的属性值（如果在此对象上属性名称不存在，那么返回null）
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="model"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static object GetPropertyValue<TModel>(TModel model, string propertyName)
        {
            object result = null;
            Type type = typeof(TModel);
            PropertyInfo propertyInfo = type.GetProperty(propertyName);
            if (propertyInfo != null && propertyInfo.CanRead == true)
            {
                result = propertyInfo.GetValue(model, null);
            }

            return result;
        }

        /// <summary>
        /// 通过属性名称获取给定对象的属性值（如果在此对象上属性名称不存在，那么返回null）
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="model"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static TResult GetPropertyValue<TModel, TResult>(TModel model, string propertyName)
        {
            object result = GetPropertyValue<TModel>(model, propertyName);
            if (result != null)
            {
                return Converter.ChangeType<TResult>(result);
            }
            else
            {
                return default(TResult);
            }
        }

        /// <summary>
        /// 通过属性名称给指定对象的属性赋值
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="model"></param>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        /// <returns></returns>
        public static void SetPropertyValue<TModel>(TModel model, string propertyName, object propertyValue)
        {
            Type type = typeof(TModel);
            PropertyInfo propertyInfo = type.GetProperty(propertyName);
            if (propertyInfo != null && propertyInfo.CanWrite == true)
            {
                Type declaringType = propertyInfo.PropertyType;
                object convertedValue = Converter.ChangeType(propertyValue, declaringType);
                propertyInfo.SetValue(model, convertedValue, null);
            }
        }

        /// <summary>
        /// 通过属性名称给指定对象的属性赋值
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="model"></param>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        /// <returns></returns>
        public static void SetPropertyValue<TModel, TProperty>(TModel model, string propertyName, TProperty propertyValue)
        {
            Type type = typeof(TModel);
            PropertyInfo propertyInfo = type.GetProperty(propertyName);
            if (propertyInfo != null && propertyInfo.CanWrite == true)
            {
                propertyInfo.SetValue(model, propertyValue, null);
            }
        }

        /// <summary>
        /// 在成员上获取指定的特性
        /// </summary>
        /// <typeparam name="TAttribute">特性类型</typeparam>
        /// <param name="memberInfo">类型的成员信息</param>
        /// <returns></returns>
        public static TAttribute GetAttribute<TAttribute>(MemberInfo memberInfo) where TAttribute : Attribute
        {
            return (TAttribute)Attribute.GetCustomAttribute(memberInfo, typeof(TAttribute), true);
        }

        /// <summary>
        /// 两实体之间相同成员（目前仅包含字段和属性）名称的值复制
        /// </summary>
        /// <typeparam name="TFrom"></typeparam>
        /// <typeparam name="TTo"></typeparam>
        /// <param name="fromEntity"></param>
        /// <param name="toEntity"></param>
        /// <returns></returns>
        public static TTo CopyMemberValue<TFrom, TTo>(TFrom fromEntity, TTo toEntity)
        {
            return CopyMemberValue<TFrom, TTo>(fromEntity, toEntity, false);
        }

        /// <summary>
        /// 两实体之间相同成员（目前仅包含字段和属性）名称的值复制
        /// </summary>
        /// <typeparam name="TFrom"></typeparam>
        /// <typeparam name="TTo"></typeparam>
        /// <param name="fromEntity"></param>
        /// <param name="toEntity"></param>
        /// <param name="isOnlyCopyDeclaredMember">是否仅复制TFrom本身的成员（即不包括TFrom从父类继承的成员）</param>
        /// <returns></returns>
        public static TTo CopyMemberValue<TFrom, TTo>(TFrom fromEntity, TTo toEntity, bool isOnlyCopyDeclaredMember)
        {
            Type typeOfFrom = typeof(TFrom);
            Type typeOfTo = typeof(TTo);

            BindingFlags bindingFlag = BindingFlags.Instance | BindingFlags.Public;
            if (isOnlyCopyDeclaredMember == true)
            {
                bindingFlag = bindingFlag | BindingFlags.DeclaredOnly;
            }

            //1.设置字段
            FieldInfo[] fiArray = typeOfFrom.GetFields(bindingFlag);
            foreach (FieldInfo currentItem in fiArray)
            {
                string currentItemName = currentItem.Name;
                object valueInBase = currentItem.GetValue(fromEntity);

                FieldInfo fiOfTo = typeOfTo.GetField(currentItemName);
                if (fiOfTo != null)
                {
                    fiOfTo.SetValue(toEntity, valueInBase);
                }
            }

            //2.设置属性
            PropertyInfo[] piArray = typeOfFrom.GetProperties(bindingFlag);
            foreach (PropertyInfo currentItem in piArray)
            {
                if (currentItem.CanRead)
                {
                    string currentItemName = currentItem.Name;
                    object valueInBase = currentItem.GetValue(fromEntity, null);
                    PropertyInfo piOfTo = typeOfTo.GetProperty(currentItemName);
                    if (piOfTo != null && piOfTo.CanWrite)
                    {
                        piOfTo.SetValue(toEntity, valueInBase, null);
                    }
                }
            }

            return toEntity;
        }



        ///// <summary>
        ///// 
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="propertyName"></param>
        ///// <param name="attributeName"></param>
        ///// <returns></returns>
        //public static string GetMemberAttribute<T>(string propertyName, string attributeName)
        //{
        //    Type type= typeof(T);
        //    return GetMemberAttribute( type,  propertyName,  attributeName);
        //}

        //public static string GetMemberAttribute(Type type, string propertyName, string attributeName)
        //{
        //    string result = string.Empty;
        //    //object[] attributeValues= GetMemberAttribute( type, propertyName);

        //    return result;
        //}

        //public static Dictionary<string, object> GetMemberAttribute(Type type, string propertyName)
        //{
        //    PropertyInfo propertyInfo = type.GetProperty(propertyName);
        //    //TODO：加入缓存容器，提高性能
        //    Dictionary<string, object> atrributeDictionary = new Dictionary<string, object>();
        //    object[] attributeValues = propertyInfo.GetCustomAttributes(true);
        //    if (attributeValues != null)
        //    {
        //        //atrributeDictionary.Add();
        //    }

        //    return atrributeDictionary;
        //}
    }
}

