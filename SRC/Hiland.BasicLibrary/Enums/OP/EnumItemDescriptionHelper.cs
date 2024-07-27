using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Hiland.BasicLibrary.Enums.OP
{
    public static class EnumItemDescriptionHelper
    {
        /// <summary>
        ///   A cache store, because accessing Attribute is according to reflection, it cost too much
        ///   So I add a cache, which based on special enum Type.
        /// </summary>
        private static readonly Dictionary<Type, Dictionary<object, Dictionary<string, EnumItemDescriptionAttribute>>> cache =
            new Dictionary<Type, Dictionary<object, Dictionary<string, EnumItemDescriptionAttribute>>>();

        /// <summary>
        ///   Gets all <see cref="EnumItemDescriptionAttribute"/>
        ///   for special enum value: <paramref name="value"/> and special enum type: <paramref name="enumType"/>.
        /// </summary>
        /// <param name="value">the value of special enum</param>
        /// <param name="enumType">the enum type of special enum</param>
        /// <returns>All attributes attatched on special enum value.</returns>
        public static Dictionary<string, EnumItemDescriptionAttribute> GetDisplayValues(object value, Type enumType)
        {
            if (enumType == null || enumType.IsEnum == false)
            {
                throw new NotSupportedException("enumType is not a Enum");
            }
            if (value == null || Enum.IsDefined(enumType, value) == false)
            {
                throw new ArgumentException("value is not defined in " + enumType.FullName);
            }

            return GetDisplayValuesDetails(value, enumType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static string GetDisplayValue(object value, Type enumType)
        {
            return GetDisplayValue(value, enumType, string.Empty);
        }


        /// <summary>
        ///   Gets the <see cref="EnumItemDescriptionAttribute"/> for special enum value: <paramref name="value"/>,
        ///   special enum type: <paramref name="enumType"/> and special culture.
        /// </summary>
        /// <param name="value">the value of special enum</param>
        /// <param name="enumType">the enum type of special enum</param>
        /// <param name="displayTextCategory">显示哪个类别的文本描述信息.</param>
        /// <returns>The attributes attatched on special enum value.</returns>
        public static string GetDisplayValue(object value, Type enumType, string displayTextCategory)
        {
            if (enumType == null || enumType.IsEnum == false)
            {
                throw new NotSupportedException("enumType is not a Enum");
            }
            if (value == null || Enum.IsDefined(enumType, value) == false)
            {
                throw new ArgumentException("value is not defined in " + enumType.FullName);
            }

            if (string.IsNullOrEmpty(displayTextCategory))
            {
                CultureInfo currentUICulute = Thread.CurrentThread.CurrentUICulture;
                displayTextCategory = currentUICulute.Name;
            }

            if (displayTextCategory == null)
            {
                return value.ToString();
            }

            Dictionary<string, EnumItemDescriptionAttribute> disc = GetDisplayValuesDetails(value, enumType);
            if (disc.ContainsKey(displayTextCategory))
            {
                return disc[displayTextCategory].DisplaySerialValue;
            }

            return value.ToString();
        }


        /// <summary>
        ///   Gets all <see cref="EnumItemDescriptionAttribute"/>
        ///   for special enum value: <paramref name="enumItem"/> and special enum type: <paramref name="enumType"/>.
        /// </summary>
        /// <param name="enumItem">the value of special enum</param>
        /// <param name="enumType">the enum type of special enum</param>
        /// <returns>All attributes attatched on special enum value.</returns>
        private static Dictionary<string, EnumItemDescriptionAttribute> GetDisplayValuesDetails(object enumItem, Type enumType)
        {
            if (cache.ContainsKey(enumType) == false)
            {
                Dictionary<object, Dictionary<string, EnumItemDescriptionAttribute>> displayValues =
                    new Dictionary<object, Dictionary<string, EnumItemDescriptionAttribute>>();
                foreach (Enum currentEnumItem in Enum.GetValues(enumType))
                {
                    Dictionary<string, EnumItemDescriptionAttribute> descriptionsOfEnumItem = new Dictionary<string, EnumItemDescriptionAttribute>();
                    FieldInfo enumField = enumType.GetField(currentEnumItem.ToString());
                    if (enumField != null)
                    {
                        foreach (EnumItemDescriptionAttribute desc in enumField.GetCustomAttributes(typeof(EnumItemDescriptionAttribute), true))
                        {
                            descriptionsOfEnumItem.Add(desc.DisplaySerialName, desc);
                        }
                    }
                    displayValues.Add(currentEnumItem, descriptionsOfEnumItem);
                }
                cache.Add(enumType, displayValues);
            }

            return cache[enumType][enumItem];
        }
    }
}
