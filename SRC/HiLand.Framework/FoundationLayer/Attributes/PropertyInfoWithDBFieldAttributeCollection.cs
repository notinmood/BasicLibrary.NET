using System;
using System.Collections.Generic;
using System.Text;
using HiLand.Utility.Reflection;
using System.Reflection;

namespace HiLand.Framework.FoundationLayer.Attributes
{
    /// <summary>
    /// 带数据库字段特性的属性信息集合
    /// </summary>
    public class PropertyInfoWithDBFieldAttributeCollection : MemberInfoWithAttributeCollection<PropertyInfo, DBFieldAttribute>
    {
        /// <summary>
        /// 获取模型的所有被标注了DBFieldAttribute的属性信息（包括该属性的名称，是否为主键等）
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <returns></returns>
        public static Dictionary<PropertyInfo, DBFieldAttribute> GetExtendedPropertyInfo<TModel>()
        {
            return MemberInfoWithAttributeCollection<PropertyInfo, DBFieldAttribute>.GetExtendedMemberInfo<TModel>();
        }

        /// <summary>
        /// 获取主键信息
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <returns></returns>
        public static Dictionary<PropertyInfo, DBFieldAttribute> GetExtendedBusinessPrimaryKeys<TModel>()
        {
            Dictionary<PropertyInfo, DBFieldAttribute> allExtendedPropertyInfo = GetExtendedPropertyInfo<TModel>();
            Dictionary<PropertyInfo, DBFieldAttribute> result = new Dictionary<PropertyInfo, DBFieldAttribute>();
            foreach (var currentExtendedPropertyInfo in allExtendedPropertyInfo)
            {
                if (currentExtendedPropertyInfo.Value != null && currentExtendedPropertyInfo.Value.IsBusinessPrimaryKey == true)
                {
                    result[currentExtendedPropertyInfo.Key] = currentExtendedPropertyInfo.Value;
                }
            }
            return result;
        }

        /// <summary>
        /// 获取实体主键的名称
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <returns></returns>
        public static List<string> GetBusinessPrimaryKeyNames<TModel>()
        {
            Dictionary<PropertyInfo, DBFieldAttribute> extendedPropertyInfoDicOfThisModel = GetExtendedBusinessPrimaryKeys<TModel>();
            List<string> result = new List<string>();
            if (extendedPropertyInfoDicOfThisModel != null)
            {
                foreach (var currentItem in extendedPropertyInfoDicOfThisModel)
                {
                    result.Add(currentItem.Key.Name);
                }
            }
            return result;
        }

        /// <summary>
        /// 获取实体主键的值
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="model">实体信息</param>
        /// <returns></returns>
        public static List<string> GetBusinessPrimaryKeyValues<TModel>(TModel model)
        {
            List<string> businessKeyValueList = new List<string>();

            Dictionary<PropertyInfo, DBFieldAttribute> extendedPrimaryKeyDicOfThisModel = PropertyInfoWithDBFieldAttributeCollection.GetExtendedBusinessPrimaryKeys<TModel>(); ;
            if (extendedPrimaryKeyDicOfThisModel != null)
            {
                foreach (PropertyInfo currentPropertyInfo in extendedPrimaryKeyDicOfThisModel.Keys)
                {
                    businessKeyValueList.Add(currentPropertyInfo.GetValue(model, null).ToString());
                }
            }

            return businessKeyValueList;
        }
    }
}
