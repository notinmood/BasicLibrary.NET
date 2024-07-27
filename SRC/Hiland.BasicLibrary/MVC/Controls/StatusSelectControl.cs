//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Web.Mvc;

//namespace Hiland.BasicLibrary4.MVC.Controls
//{
//    /// <summary>
//    /// 状态选择控件（CheckBox，RadioButton等）的基类
//    /// </summary>
//    /// <typeparam name="T"></typeparam>
//    public abstract class StatusSelectControl<T> : InputControl<T> where T : StatusSelectControl<T>
//    {
//        private bool isChecked = false;
//        /// <summary>
//        /// 设置控件是否被选中
//        /// </summary>
//        /// <param name="data"></param>
//        /// <returns></returns>
//        public T IsChecked(bool data)
//        {
//            this.isChecked = data;
//            return this as T;
//        }

//        private string description = string.Empty;
//        /// <summary>
//        /// 设置控件的描述信息（显示在checkbox框的后面）
//        /// </summary>
//        /// <param name="value"></param>
//        /// <returns></returns>
//        public T Description(string value)
//        {
//            this.description = value;
//            return this as T;
//        }

//        /// <summary>
//        /// 控件类型的名称（checkbox还是radio）
//        /// </summary>
//        protected abstract string InputTypeName { get; }

//        /// <summary>
//        /// 绘制控件核心部分的Html代码
//        /// </summary>
//        /// <returns></returns>
//        protected override string WriteCoreHtml()
//        {
//            return GetStatusItemString();
//        }

//        protected string GetStatusItemString(string idInfo="",string descriptionInfo="",bool isCheckedInfo=false,string valueInfo= "")
//        {
//            string result = string.Empty;
//            TagBuilder tagInfo = CreateTag("input", string.Empty,idInfo,valueInfo);

//            tagInfo.Attributes["type"] = InputTypeName;

//            if (this.isChecked==true || isCheckedInfo==true)
//            {
//                tagInfo.Attributes["checked"] = "checked";
//            }

//            if (this.usingMode == MvcControlUsingModes.Editable)
//            {
//                if (this.isDisabled)
//                {
//                    tagInfo.Attributes["disabled"] = "disabled";
//                }
//            }
//            else
//            {
//                tagInfo.Attributes["disabled"] = "disabled";
//            }

//            result = tagInfo.ToString();

//            if (string.IsNullOrWhiteSpace(descriptionInfo) == false || string.IsNullOrWhiteSpace(description) == false)
//            {
//                TagBuilder tagDesc = new TagBuilder("label");
//                tagDesc.AddCssClass("lableFor");
                
//                if (string.IsNullOrWhiteSpace(idInfo))
//                {
//                    tagDesc.Attributes["for"] = ID;
//                }
//                else
//                {
//                    tagDesc.Attributes["for"] = idInfo;
//                }

//                if (string.IsNullOrWhiteSpace(descriptionInfo))
//                {
//                    tagDesc.InnerHtml = description;
//                }
//                else
//                {
//                    tagDesc.InnerHtml = descriptionInfo;
//                }

//                result += tagDesc.ToString();
//            }
//            return result;
//        }
//    }
//}
