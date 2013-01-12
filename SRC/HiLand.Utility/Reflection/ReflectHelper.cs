using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using HiLand.Utility.Data;
using HiLand.Utility.Entity;
using HiLand.Utility.Attributes;

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
            Type type = typeof(TModel);
            return GetPropertyValue(type, model, propertyName);
        }

        /// <summary>
        /// 通过属性名称获取给定对象的属性值（如果在此对象上属性名称不存在，那么返回null）
        /// </summary>
        /// <param name="modelType"></param>
        /// <param name="model"></param>
        /// <param name="propertyName">支持二级属性，比如CurrentBank.AccountNumber
        /// 其会加载属性CurrentBank的子属性AccountNumber的信息</param>
        /// <returns></returns>
        public static object GetPropertyValue(Type modelType, object model, string propertyName)
        {
            object result = null;
            Type type = modelType;
            if (propertyName.IndexOf(".") > 0)
            {
                string propertyNameOfLevelThis = StringHelper.GetBeforeSeperatorString(propertyName, ".");
                string propertyNameOfLevelNext = StringHelper.GetAfterSeperatorString(propertyName, ".");
                object propertyValueOfLevelThis = GetPropertyValue(modelType, model, propertyNameOfLevelThis);

                if (propertyValueOfLevelThis == null)
                {
                    return string.Empty;
                }
                else
                {
                    PropertyInfo piOfLevelThis = modelType.GetProperty(propertyNameOfLevelThis);
                    if (piOfLevelThis == null)
                    {
                        return string.Empty;
                    }
                    else
                    {
                        Type propertyTypeOfLevelThis = piOfLevelThis.PropertyType;
                        return GetPropertyValue(propertyTypeOfLevelThis, propertyValueOfLevelThis, propertyNameOfLevelNext);
                    }
                }
            }
            else
            {
                PropertyInfo propertyInfo = type.GetProperty(propertyName);
                if (propertyInfo != null && propertyInfo.CanRead == true)
                {
                    result = propertyInfo.GetValue(model, null);
                }

                return result;
            }
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
            if (memberInfo == null)
            {
                return null;
            }
            else
            {
                return (TAttribute)Attribute.GetCustomAttribute(memberInfo, typeof(TAttribute), true);
            }
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
            Dictionary<FieldInfo, NonCopyMember> nonCopyFieldMember = MemberInfoWithAttributeCollection<FieldInfo, NonCopyMember>.GetExtendedMemberInfo<TFrom>();
            foreach (FieldInfo currentItem in fiArray)
            {
                if (nonCopyFieldMember.ContainsKey(currentItem))
                {
                    continue;
                }

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
            Dictionary<PropertyInfo, NonCopyMember> nonCopyPropertyMember = MemberInfoWithAttributeCollection<PropertyInfo, NonCopyMember>.GetExtendedMemberInfo<TFrom>();
            foreach (PropertyInfo currentItem in piArray)
            {
                if (nonCopyPropertyMember.ContainsKey(currentItem))
                {
                    continue;
                }

                if (currentItem.CanRead)
                {
                    string currentItemName = currentItem.Name;
                    PropertyInfo piOfTo = typeOfTo.GetProperty(currentItemName);
                    if (piOfTo != null && piOfTo.CanWrite)
                    {
                        object valueInBase = currentItem.GetValue(fromEntity, null);
                        piOfTo.SetValue(toEntity, valueInBase, null);
                    }
                }
            }

            return toEntity;
        }

        /// <summary>
        /// 两个对象属性比较
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceEntity"></param>
        /// <param name="targetEntity"></param>
        /// <param name="resultData">属性并更的信息：key为变更的属性名称；value为包含变更前值和后值的数据</param>
        /// <param name="excludePropertyName">不进行比较的属性名称集合</param>
        /// <returns></returns>
        public static bool Compare<T>(T sourceEntity, T targetEntity, out Dictionary<string, DataForChange<string>> resultData, params string[] excludePropertyName)
        {
            bool result = true;
            resultData = new Dictionary<string, DataForChange<string>>();
            Type typeOfEntity = typeof(T);
            BindingFlags bindingFlag = BindingFlags.Instance | BindingFlags.Public;

            PropertyInfo[] piArray = typeOfEntity.GetProperties(bindingFlag);
            foreach (PropertyInfo currentItem in piArray)
            {
                string currentItemName = currentItem.Name;
                bool isJumpOut = false;
                for (int i = 0; i < excludePropertyName.Length; i++)
                {
                    if (currentItemName.ToLower() == excludePropertyName[i].ToLower())
                    {
                        isJumpOut = true;
                        continue;
                    }
                }

                if (isJumpOut == true)
                {
                    continue;
                }

                if (currentItem.CanRead == true)
                {
                    object valueInSource = currentItem.GetValue(sourceEntity, null) ?? string.Empty;
                    object valueInTarget = currentItem.GetValue(targetEntity, null) ?? string.Empty;

                    string valueInSourceString = valueInSource.ToString();
                    string valueInTargetString = valueInTarget.ToString();

                    if (valueInSourceString != valueInTargetString)
                    {
                        result = false;
                        resultData[currentItemName] = new DataForChange<string>(valueInSourceString, valueInTargetString);
                    }
                }
            }

            return result;
        }
    }
}

