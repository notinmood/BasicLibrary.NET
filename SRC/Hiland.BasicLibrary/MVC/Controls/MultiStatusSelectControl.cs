//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Web.Mvc;

//namespace HiLand.Utility4.MVC.Controls
//{
//    /// <summary>
//    /// 多子项列表状态控件
//    /// </summary>
//    /// <typeparam name="T"></typeparam>
//    public abstract class MultiItemStatusSelectControl<T> : StatusSelectControl<T> where T : MultiItemStatusSelectControl<T>
//    {
//        /// <summary>
//        /// 列表项
//        /// </summary>
//        protected IEnumerable<SelectListItem> itemList = new List<SelectListItem>();
        
//        /// <summary>
//        /// 设置列表项
//        /// </summary>
//        /// <param name="data"></param>
//        /// <returns></returns>
//        public T ItemList(IEnumerable<SelectListItem> data)
//        {
//            this.itemList = data;
//            return this as T;
//        }

//        /// <summary>
//        /// 绘制控件核心部分的Html代码
//        /// </summary>
//        /// <returns></returns>
//        protected override string WriteCoreHtml()
//        {
//            string result = string.Empty;
//            List<string> valueList = new List<string>();
//            if (string.IsNullOrWhiteSpace(value) == false)
//            {
//                valueList = value.Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries).ToList();
//            }

//            int itemIndex = 0;
//            foreach (SelectListItem item in itemList)
//            {
//                itemIndex++;
//                bool isChecked = false;
//                foreach (string valueItem in valueList)
//                {
//                    if (item.Value == valueItem)
//                    {
//                        isChecked = true;
//                    }
//                }

//                string idInfo = string.Format("{0}_{1}", item.Value, itemIndex);
//                string currentItemString = GetStatusItemString(idInfo, item.Text, isChecked,item.Value);
//                result += currentItemString;
//            }

//            return result;
//        }
//    }
//}
