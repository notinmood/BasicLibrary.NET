//using System.Collections.Generic;
//using System.Web.Mvc;
//using HiLand.Utility4.Data;

//namespace HiLand.Utility4.MVC.Controls
//{
//    /// <summary>
//    /// HtmlHelper扩展器（扩展MVC控件）
//    /// </summary>
//    /// <remarks>
//    /// 每个新建的MVC控件，如果要在HtmlHelper中方便调用，请在此类中注册。
//    /// </remarks>
//    public static class ControlsHtmlHelper
//    {
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="name">控件的name值</param>
//        /// <returns></returns>
//        public static HiddenControl HiHidden(string name="")
//        {
//            return new HiddenControl().Name(name);
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="htmlHelper"></param>
//        /// <param name="name">控件的name值</param>
//        /// <returns></returns>
//        public static HiddenControl HiHidden(this HtmlHelper htmlHelper, string name = "")
//        {
//            return HiHidden(name);
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="name">控件的name值</param>
//        /// <returns></returns>
//        public static TextBoxControl HiTextBox(string name = "")
//        {
//            return new TextBoxControl().Name(name); 
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="htmlHelper"></param>
//        /// <param name="name">控件的name值</param>
//        /// <returns></returns>
//        public static TextBoxControl HiTextBox(this HtmlHelper htmlHelper, string name = "")
//        {
//            return HiTextBox(name);
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="name">控件的name值</param>
//        /// <returns></returns>
//        public static CheckBoxControl HiCheckBox(string name = "")
//        {
//            return new CheckBoxControl().Name(name);
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="htmlHelper"></param>
//        /// <param name="name">控件的name值</param>
//        /// <returns></returns>
//        public static CheckBoxControl HiCheckBox(this HtmlHelper htmlHelper, string name = "")
//        {
//            return HiCheckBox(name);
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="name">控件的name值</param>
//        /// <returns></returns>
//        public static TextAreaControl HiTextArea(string name = "")
//        {
//            return new TextAreaControl().Name(name);
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="htmlHelper"></param>
//        /// <param name="name">控件的name值</param>
//        /// <returns></returns>
//        public static TextAreaControl HiTextArea(this HtmlHelper htmlHelper, string name = "")
//        {
//            return HiTextArea(name);
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="name">控件的name值</param>
//        /// <returns></returns>
//        public static DropDownListControl HiDropDownList(string name = "")
//        {
//            return new DropDownListControl().Name(name);
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="htmlHelper"></param>
//        /// <param name="name">控件的name值</param>
//        /// <returns></returns>
//        public static DropDownListControl HiDropDownList(this HtmlHelper htmlHelper, string name = "")
//        {
//            return HiDropDownList(name);
//        }

//        /// <summary>
//        /// 将枚举类型转换成下拉列表
//        /// </summary>
//        /// <typeparam name="T">必须为枚举类型</typeparam>
//        /// <param name="selectedValue"></param>
//        /// <param name="name">控件的name值</param>
//        /// <returns></returns>
//        public static DropDownListControl HiDropDonwList<T>(string selectedValue = "", string name = "") where T : struct
//        {
//            List<SelectListItem> residentalTypeList = EnumEx.BuildSelectItemList<T>();
//            foreach (SelectListItem listItem in residentalTypeList)
//            {
//                if (listItem.Value == selectedValue)
//                {
//                    listItem.Selected = true;
//                }
//            }
//            return new DropDownListControl().ItemList(residentalTypeList).Name(name);
//        }

//        /// <summary>
//        /// 将枚举类型转换成下拉列表
//        /// </summary>
//        /// <typeparam name="T">必须为枚举类型</typeparam>
//        /// <param name="htmlHelper"></param>
//        /// <param name="selectedValue"></param>
//        /// <param name="name">控件的name值</param>
//        /// <returns></returns>
//        public static DropDownListControl HiDropDonwList<T>(this HtmlHelper htmlHelper, string selectedValue = "",string name="") where T:struct
//        {
//            return HiDropDonwList<T>(selectedValue).Name(name);
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="name">控件的name值</param>
//        /// <returns></returns>
//        public static RadioButtonControl HiRadioButton(string name = "")
//        {
//            return new RadioButtonControl().Name(name);
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="htmlHelper"></param>
//        /// <param name="name">控件的name值</param>
//        /// <returns></returns>
//        public static RadioButtonControl HiRadioButton(this HtmlHelper htmlHelper, string name = "")
//        {
//            return HiRadioButton(name);
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="name">控件的name值</param>
//        /// <returns></returns>
//        public static CheckBoxListControl HiCheckBoxList(string name = "")
//        {
//            return new CheckBoxListControl().Name(name);
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="htmlHelper"></param>
//        /// <param name="name">控件的name值</param>
//        /// <returns></returns>
//        public static CheckBoxListControl HiCheckBoxList(this HtmlHelper htmlHelper, string name = "")
//        {
//            return HiCheckBoxList(name);
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="name">控件的name值</param>
//        /// <returns></returns>
//        public static RadioButtonListControl HiRadioButtonList(string name = "")
//        {
//            return new RadioButtonListControl().Name(name);
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="htmlHelper"></param>
//        /// <param name="name">控件的name值</param>
//        /// <returns></returns>
//        public static RadioButtonListControl HiRadioButtonList(this HtmlHelper htmlHelper, string name = "")
//        {
//            return HiRadioButtonList(name);
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="name">控件的name值</param>
//        /// <returns></returns>
//        public static DateTimeControl HiDateTime(string name = "")
//        {
//            return new DateTimeControl().Name(name);
//        }

//        /// <summary>
//        /// 时间选择控件
//        /// </summary>
//        /// <param name="htmlHelper"></param>
//        /// <param name="name">控件的name值</param>
//        /// <returns></returns>
//        public static DateTimeControl HiDateTime(this HtmlHelper htmlHelper, string name = "")
//        {
//            return HiDateTime(name);
//        }

//        /// <summary>
//        /// 树选择控件
//        /// </summary>
//        /// <param name="name">控件的name值</param>
//        /// <returns></returns>
//        public static TreeSelectControl HiTreeSelect(string name = "")
//        {
//            return new TreeSelectControl().Name(name);
//        }

//        /// <summary>
//        /// 树选择控件
//        /// </summary>
//        /// <param name="htmlHelper"></param>
//        /// <param name="name">控件的name值</param>
//        /// <returns></returns>
//        public static TreeSelectControl HiTreeSelect(this HtmlHelper htmlHelper, string name = "")
//        {
//            return HiTreeSelect(name);
//        }

//        /// <summary>
//        /// 查询控件
//        /// </summary>
//        /// <param name="name">控件的name值</param>
//        /// <returns></returns>
//        public static QueryControl HiQuery(string name = "")
//        {
//            return new QueryControl().Name(name);
//        }

//        /// <summary>
//        /// 查询控件
//        /// </summary>
//        /// <param name="htmlHelper"></param>
//        /// <param name="name">控件的name值</param>
//        /// <returns></returns>
//        public static QueryControl HiQuery(this HtmlHelper htmlHelper, string name = "")
//        {
//            return HiQuery(name);
//        }

//        /// <summary>
//        /// 级联的下拉列表控件
//        /// </summary>
//        /// <param name="name">控件的name值</param>
//        /// <returns></returns>
//        public static CascadingDropDownListControl HiCascadingDropDownList(string name = "")
//        {
//            return new CascadingDropDownListControl().Name(name);
//        }

//        /// <summary>
//        /// 级联的下拉列表控件
//        /// </summary>
//        /// <param name="htmlHelper"></param>
//        /// <param name="name">控件的name值</param>
//        /// <returns></returns>
//        public static CascadingDropDownListControl HiCascadingDropDownList(this HtmlHelper htmlHelper, string name = "")
//        {
//            return HiCascadingDropDownList(name);
//        }
//    }
//}
