//using System;
//using System.Collections.Generic;
//using System.Reflection;
//using System.Text;
//using System.Web.UI.WebControls;
//using Hiland.BasicLibrary.Attributes;
//using Hiland.BasicLibrary.Reflection;

//namespace Hiland.BasicLibrary.Enums.OP
//{
//    /// <summary>
//    /// 将枚举项构建成选择项的辅助工具类
//    /// </summary>
//    public class EnumBuilder
//    {
//        /// <summary>
//        /// 构造枚举项的 列表项集合(即ListItemCollection,列表控件中使用)
//        /// </summary>
//        /// <typeparam name="T">必须为枚举类型</typeparam>
//        /// <returns></returns>
//        public static ListItemCollection BuildItemCollection<T>()
//        {
//            return BuildItemCollection<T>(string.Empty);
//        }

//        /// <summary>
//        /// 构造枚举项的 列表项集合(即ListItemCollection,列表控件中使用)
//        /// </summary>
//        /// <typeparam name="T">必须为枚举类型</typeparam>
//        /// <param name="displaySerialName"></param>
//        /// <returns></returns>
//        public static ListItemCollection BuildItemCollection<T>(string displaySerialName)
//        {
//            return BuildItemCollection(typeof(T), displaySerialName);
//        }

//        /// <summary>
//        /// 构造枚举项的 列表项集合(即ListItemCollection,列表控件中使用)
//        /// </summary>
//        /// <typeparam name="T">必须为枚举类型</typeparam>
//        /// <param name="isDisplayEmptyItem">是否显示空项</param>
//        /// <returns></returns>
//        public static ListItemCollection BuildItemCollection<T>(bool isDisplayEmptyItem)
//        {
//            return BuildItemCollection(typeof(T), isDisplayEmptyItem);
//        }

//        /// <summary>
//        /// 构造枚举项的 列表项集合(即ListItemCollection,列表控件中使用)
//        /// </summary>
//        /// <typeparam name="T">必须为枚举类型</typeparam>
//        /// <param name="displaySerialName"></param>
//        /// <param name="isDisplayEmptyItem">是否显示空项</param>
//        /// <returns></returns>
//        public static ListItemCollection BuildItemCollection<T>(string displaySerialName, bool isDisplayEmptyItem)
//        {
//            return BuildItemCollection(typeof(T), displaySerialName, isDisplayEmptyItem);
//        }

//        /// <summary>
//        /// 构造枚举项的 列表项集合(即ListItemCollection,列表控件中使用)
//        /// </summary>
//        /// <typeparam name="T">必须为枚举类型</typeparam>
//        /// <returns></returns>
//        public static ListItemCollection BuildItemCollection(Type enumType)
//        {
//            return BuildItemCollection(enumType, string.Empty);
//        }

//        /// <summary>
//        /// 构造枚举项的 列表项集合(即ListItemCollection,列表控件中使用)
//        /// </summary>
//        /// <param name="enumType">枚举的类型</param>
//        /// <param name="displaySerialName"></param>
//        /// <returns></returns>
//        public static ListItemCollection BuildItemCollection(Type enumType, string displaySerialName)
//        {
//            return BuildItemCollection(enumType, displaySerialName, false);
//        }

//        /// <summary>
//        /// 构造枚举项的 列表项集合(即ListItemCollection,列表控件中使用)
//        /// </summary>
//        /// <param name="enumType">枚举的类型</param>
//        /// <param name="isDisplayEmptyItem">是否显示空项</param>
//        /// <returns></returns>
//        public static ListItemCollection BuildItemCollection(Type enumType, bool isDisplayEmptyItem)
//        {
//            return BuildItemCollection(enumType, string.Empty, isDisplayEmptyItem);
//        }

//        /// <summary>
//        /// 构造枚举项的 列表项集合(即ListItemCollection,列表控件中使用)
//        /// </summary>
//        /// <param name="enumType">枚举的类型</param>
//        /// <param name="displaySerialName"></param>
//        /// <param name="isDisplayEmptyItem">是否显示空项</param>
//        /// <returns></returns>
//        public static ListItemCollection BuildItemCollection(Type enumType, string displaySerialName, bool isDisplayEmptyItem)
//        {
//            ListItemCollection items = new ListItemCollection();
//            Array array = Enum.GetValues(enumType);
//            for (int i = 0; i < array.Length; i++)
//            {
//                Object currentEnumItemType = array.GetValue(i);
//                FieldInfo enumItemFieldInfo = enumType.GetField(currentEnumItemType.ToString());
//                EnumItemIsDisplayInListAttribute attr = ReflectHelper.GetAttribute<EnumItemIsDisplayInListAttribute>(enumItemFieldInfo);
//                if (attr == null || attr.IsDisplayInList == true)
//                {
//                    string currentEnumItemTypeText = EnumItemDescriptionHelper.GetDisplayValue(currentEnumItemType, enumType, displaySerialName);
//                    ListItem currentItem = new ListItem(currentEnumItemTypeText, currentEnumItemType.ToString());
//                    items.Add(currentItem);
//                }
//            }

//            if (isDisplayEmptyItem == true)
//            {
//                ListItem emptyItem = new ListItem("Please Choose", string.Empty);
//                items.Insert(0, emptyItem);
//            }

//            return items;
//        }
//    }
//}
