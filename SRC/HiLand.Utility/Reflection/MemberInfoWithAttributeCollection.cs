using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace HiLand.Utility.Reflection
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TMemberInfo"></typeparam>
    /// <typeparam name="TAttribute"></typeparam>
    public class MemberInfoWithAttributeCollection<TMemberInfo, TAttribute>
        where TMemberInfo : MemberInfo
        where TAttribute : Attribute
    {
        private static Dictionary<Type, Dictionary<TMemberInfo, TAttribute>> typeWithExtendedMemberInfosDic = new Dictionary<Type, Dictionary<TMemberInfo, TAttribute>>();

        /// <summary>
        /// 获取模型的所有被标注了TAttribute特性的成员信息
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Dictionary<TMemberInfo, TAttribute> GetExtendedMemberInfo(Type type)
        {
            if (typeWithExtendedMemberInfosDic.ContainsKey(type))
            {
                return typeWithExtendedMemberInfosDic[type];
            }
            else
            {
                Dictionary<TMemberInfo, TAttribute> extendedPropertyInfo = new Dictionary<TMemberInfo, TAttribute>();
                MemberInfo[] allMemberInfos = type.GetMembers();
                List<TMemberInfo> targetMemberInfoList = new List<TMemberInfo>();
                foreach (MemberInfo currentMemberInfo in allMemberInfos)
                {
                    if (currentMemberInfo is TMemberInfo)
                    {
                        targetMemberInfoList.Add(currentMemberInfo as TMemberInfo);
                    }
                }

                foreach (TMemberInfo currentPropertyInfo in targetMemberInfoList)
                {
                    TAttribute dbFieldAttribute = ReflectHelper.GetAttribute<TAttribute>(currentPropertyInfo);
                    if (dbFieldAttribute != null)
                    {
                        extendedPropertyInfo[currentPropertyInfo] = dbFieldAttribute;
                    }
                }

                typeWithExtendedMemberInfosDic[type] = extendedPropertyInfo;

                return extendedPropertyInfo;
            }

        }

        /// <summary>
        /// 获取模型的所有被标注了TAttribute的属性信息
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <returns></returns>
        public static Dictionary<TMemberInfo, TAttribute> GetExtendedMemberInfo<TModel>()
        {
            Type type = typeof(TModel);
            return GetExtendedMemberInfo(type);
        }
    }
}
