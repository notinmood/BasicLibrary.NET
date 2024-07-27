using Hiland.BasicLibrary.Enums.OP;
using System;
using System.Collections.Generic;

namespace Hiland.BasicLibrary.Data
{
    /// <summary>
    /// 枚举操作辅助类
    /// </summary>
    public static class EnumHelper
    {
        #region 获取某个枚举项上通过贴加上Attribute的内容
        /// <summary>
        ///  获取某个枚举项上通过贴加上Attribute的内容
        /// </summary>
        /// <param name="data">枚举项</param>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <returns>某个枚举项上通过贴加上Attribute的内容</returns>
        /// <remarks>
        ///  这个在枚举项上贴加的Attitude必须是EnumItemDescriptionAttribute类型
        /// </remarks>
        public static string GetDisplayValue<TEnum>(TEnum data)
        {
            Type enumType = typeof(TEnum);
            return GetDisplayValue(data, enumType, string.Empty);
        }

        /// <summary>
        ///  获取某个枚举项上通过贴加上Attribute的内容
        /// </summary>
        /// <param name="data">枚举项</param>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="displayTextCategory">显示哪个类别的文本描述信息.</param>
        /// <returns>某个枚举项上通过贴加上Attribute的内容</returns>
        /// <remarks>
        ///  这个在枚举项上贴加的Attitude必须是EnumItemDescriptionAttribute类型
        /// </remarks>
        public static string GetDisplayValue<TEnum>(TEnum data, string displayTextCategory)
        {
            Type enumType = typeof(TEnum);
            return GetDisplayValue(data, enumType, displayTextCategory);
        }


        /// <summary>
        ///  获取某个枚举项上通过贴加上Attribute的内容
        /// </summary>
        /// <param name="data">枚举项</param>
        /// <param name="enumType">枚举类型</param>
        /// <returns>某个枚举项上通过贴加上Attribute的内容</returns>
        /// <remarks>
        ///  这个在枚举项上贴加的Attitude必须是EnumItemDescriptionAttribute类型
        /// </remarks>
        public static string GetDisplayValue(object data, Type enumType)
        {
            return GetDisplayValue(data, enumType, string.Empty);
        }

        /// <summary>
        ///  获取某个枚举项上通过贴加上Attribute的内容
        /// </summary>
        /// <param name="data">枚举项</param>
        /// <param name="enumType">枚举类型</param>
        /// <param name="displayTextCategory">显示哪个类别的文本描述信息.</param>
        /// <returns>某个枚举项上通过贴加上Attribute的内容</returns>
        /// <remarks>
        ///  这个在枚举项上贴加的Attitude必须是EnumItemDescriptionAttribute类型
        /// </remarks>
        public static string GetDisplayValue(object data, Type enumType, string displayTextCategory)
        {
            return EnumItemDescriptionHelper.GetDisplayValue(data, enumType, displayTextCategory);
        }

        /// <summary>
        /// 从显示的友好名称获取枚举项
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="displayValue"></param>
        /// <returns></returns>
        public static TEnum GetItem<TEnum>(string displayValue)
        {
            return GetItem<TEnum>(displayValue, string.Empty);
        }

        /// <summary>
        /// 从显示的友好名称获取枚举项
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="displayValue"></param>
        /// <param name="displayTextCategory"></param>
        /// <returns></returns>
        public static TEnum GetItem<TEnum>(string displayValue, string displayTextCategory)
        {
            return GetItem<TEnum>(displayValue, displayTextCategory, default(TEnum));
        }

        /// <summary>
        /// 从显示的友好名称获取枚举项
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="displayValue"></param>
        /// <param name="displayTextCategory"></param>
        /// <param name="defaultValue">缺省值</param>
        /// <returns></returns>
        public static TEnum GetItem<TEnum>(string displayValue, string displayTextCategory, TEnum defaultValue)
        {
            Type enumType = typeof(TEnum);
            Array array = Enum.GetValues(enumType);
            for (int i = 0; i < array.Length; i++)
            {
                Object currentEnumItem = array.GetValue(i);
                Dictionary<string, EnumItemDescriptionAttribute> dic = EnumItemDescriptionHelper.GetDisplayValues(currentEnumItem, enumType);

                if (string.IsNullOrEmpty(displayTextCategory))
                {
                    foreach (KeyValuePair<string, EnumItemDescriptionAttribute> kvp in dic)
                    {
                        if (kvp.Value.DisplaySerialValue == displayValue)
                        {
                            return (TEnum)currentEnumItem;
                        }
                    }
                }
                else
                {
                    if (dic.ContainsKey(displayTextCategory) == true)
                    {
                        if (dic[displayTextCategory].DisplaySerialValue == displayValue)
                        {
                            return (TEnum)currentEnumItem;
                        }
                    }
                }
            }

            return defaultValue;
        }
        #endregion
    }
}
