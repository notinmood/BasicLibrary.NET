//using System;
//using System.Collections.Generic;
//using System.Web.Mvc;
//using System.Web.UI.WebControls;
//using Hiland.BasicLibrary.Enums.OP;

//namespace Hiland.BasicLibrary4.Data
//{
//    /// <summary>
//    /// 枚举辅助类
//    /// </summary>
//    public static class EnumEx
//    {
//        /// <summary>
//        /// 将某个枚举项的字符串值转化成其对应的枚举类型
//        /// </summary>
//        /// <typeparam name="TEnum"></typeparam>
//        /// <param name="enumItemValue"></param>
//        /// <returns></returns>
//        public static TEnum TryParse<TEnum>(string enumItemValue) where TEnum : struct
//        {
//            TEnum defaultValue = default(TEnum);
//            return TryParse<TEnum>(enumItemValue, defaultValue);
//        }

//        /// <summary>
//        /// 将某个枚举项的字符串值转化成其对应的枚举类型
//        /// </summary>
//        /// <typeparam name="TEnum"></typeparam>
//        /// <param name="enumItemValue"></param>
//        /// <param name="defaultValue"></param>
//        /// <returns></returns>
//        public static TEnum TryParse<TEnum>(string enumItemValue, TEnum defaultValue) where TEnum : struct
//        {
//            TEnum result = defaultValue;
//            Enum.TryParse<TEnum>(enumItemValue, true, out result);

//            return result;
//        }

//        /// <summary>
//        /// 将枚举的各个枚举项构建成下拉列表项列表（MVC下使用）
//        /// </summary>
//        /// <typeparam name="TEnum">枚举类型</typeparam>
//        /// <param name="displayTextCategory"></param>
//        /// <param name="selectedValue"></param>
//        /// <returns></returns>
//        public static List<SelectListItem> BuildSelectItemList<TEnum>(string displayTextCategory = "", string selectedValue = "") where TEnum : struct
//        {
//            return BuildSelectItemList(typeof(TEnum), displayTextCategory, selectedValue);
//        }

//        /// <summary>
//        /// 将枚举的各个枚举项构建成下拉列表项列表（MVC下使用）
//        /// </summary>
//        /// <param name="enumType"></param>
//        /// <param name="displayTextCategory"></param>
//        /// <param name="selectedValue"></param>
//        /// <returns></returns>
//        public static List<SelectListItem> BuildSelectItemList(Type enumType, string displayTextCategory = "", string selectedValue = "")
//        {
//            List<SelectListItem> itemList = new List<SelectListItem>();
//            ListItemCollection itemCollection = EnumBuilder.BuildItemCollection(enumType, displayTextCategory);
//            foreach (ListItem currentItem in itemCollection)
//            {
//                SelectListItem item = new SelectListItem();
//                item.Text = currentItem.Text;
//                item.Value = currentItem.Value;
//                if (selectedValue == currentItem.Value)
//                {
//                    currentItem.Selected = true;
//                }
//                itemList.Add(item);
//            }

//            return itemList;
//        }
//    }
//}
