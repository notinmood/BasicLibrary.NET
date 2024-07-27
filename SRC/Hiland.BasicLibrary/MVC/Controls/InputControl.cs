//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Web.Mvc;

//namespace Hiland.BasicLibrary4.MVC.Controls
//{
//    /// <summary>
//    /// 需要收集用户输入信息的MVC控件基类
//    /// </summary>
//    public abstract class InputControl<T> : BaseControl<T> where T : BaseControl<T>
//    {
//        /// <summary>
//        /// 控件的使用模式
//        /// </summary>
//        protected MvcControlUsingModes usingMode = MvcControlUsingModes.Editable;

//        /// <summary>
//        /// 设置控件的使用模式
//        /// </summary>
//        /// <param name="data"></param>
//        /// <returns></returns>
//        public T UsingMode(MvcControlUsingModes data)
//        {
//            this.usingMode = data;
//            return this as T;
//        }

//        /// <summary>
//        /// 控件是否不可以使用
//        /// </summary>
//        protected bool isDisabled = false;

//        /// <summary>
//        /// 设置控件是否不可以使用
//        /// </summary>
//        /// <param name="data"></param>
//        /// <returns></returns>
//        public T IsDisabled(bool data)
//        {
//            this.isDisabled = data;
//            return this as T;
//        }

//        /// <summary>
//        /// 输入框前说明文本显示的内容
//        /// </summary>
//        protected string lable = string.Empty;

//        /// <summary>
//        /// 设置输入框前说明文本显示的内容
//        /// </summary>
//        /// <param name="data"></param>
//        /// <returns></returns>
//        public T Lable(string data)
//        {
//            this.lable = data;
//            return this as T;
//        }

//        /// <summary>
//        /// 提示和数据之间的分割符号
//        /// </summary>
//        protected string seperator = ":";
//        /// <summary>
//        /// 设置提示和数据之间的分割符号
//        /// </summary>
//        public T Seperator(string data)
//        {
//            this.seperator = data;
//            return this as T;
//        }

//        private bool isUseSelftContainer = true;
//        /// <summary>
//        /// 设置是否将生成的控件放入自己的容器（DIV）内
//        /// </summary>
//        /// <param name="data"></param>
//        /// <returns></returns>
//        public T IsUseSelfContainer(bool data)
//        {
//            this.isUseSelftContainer = data;
//            return this as T;
//        }

//        private IDictionary<string, object> labelHtmlAttributes;
//        /// <summary>
//        /// 设置label上的Html属性字典集合
//        /// </summary>
//        /// <param name="data"></param>
//        /// <returns></returns>
//        public T LabelHtmlAttributes(IDictionary<string, object> data)
//        {
//            this.labelHtmlAttributes = data;
//            return this as T;
//        }

//        /// <summary>
//        ///  输出控件Html代码
//        /// </summary>
//        /// <param name="writer"></param>
//        protected sealed override void WriteHtml(System.Web.UI.HtmlTextWriter writer)
//        {
//            string displayInfo = string.Empty;

//            if (this.isUseSelftContainer == true)
//            {
//                TagBuilder tagDiv = new TagBuilder("div");
//                tagDiv.AddCssClass("hlDiv");
//                tagDiv.InnerHtml = WriteLabelString();
//                tagDiv.InnerHtml += WriteCoreHtml();

//                displayInfo = tagDiv.ToString();
//            }
//            else
//            {
//                displayInfo = WriteCoreHtml();
//            }

//            writer.Write(displayInfo);
//        }

//        /// <summary>
//        /// 绘制控件核心部分的Html代码
//        /// </summary>
//        /// <returns></returns>
//        protected abstract string WriteCoreHtml();

//        /// <summary>
//        /// 绘制文本说明字符代码
//        /// </summary>
//        /// <returns></returns>
//        private string WriteLabelString()
//        {
//            if (string.IsNullOrWhiteSpace(lable))
//            {
//                return string.Empty;
//            }
//            else
//            {
//                string lableString = string.Empty;
//                if (this.isUseSelftContainer == true)
//                {
//                    TagBuilder tagLable = new TagBuilder("div");
//                    tagLable.AddCssClass("span");
//                    tagLable.Attributes["style"] = "display:inline-block;vertical-align:top;";
//                    if (labelHtmlAttributes != null)
//                    {
//                        tagLable.MergeAttributes(this.labelHtmlAttributes);
//                    }
//                    tagLable.InnerHtml = lable + seperator;
//                    lableString = tagLable.ToString();
//                }
//                else
//                {
//                    lableString = lable + seperator;
//                }

//                return lableString;
//            }
//        }

//        /// <summary>
//        /// 创建标签
//        /// </summary>
//        /// <param name="tagName"></param>
//        /// <param name="nameInfo"></param>
//        /// <param name="idInfo"></param>
//        /// <returns></returns>
//        protected override TagBuilder CreateTag(string tagName, string nameInfo = "", string idInfo = "")
//        {
//            return CreateTag(tagName, nameInfo, idInfo, string.Empty);
//        }

//        /// <summary>
//        /// 创建标签
//        /// </summary>
//        /// <param name="tagName"></param>
//        /// <param name="valueInfo"></param>
//        /// <param name="htmlAttributes"></param>
//        /// <returns></returns>
//        protected TagBuilder CreateTag(string tagName, string valueInfo, IDictionary<string, string> htmlAttributes = null)
//        {
//            return CreateTag(tagName, string.Empty, string.Empty, valueInfo, htmlAttributes);
//        }

//        /// <summary>
//        /// 创建标签
//        /// </summary>
//        /// <param name="tagName"></param>
//        /// <param name="nameInfo"></param>
//        /// <param name="idInfo"></param>
//        /// <param name="valueInfo"></param>
//        /// <param name="htmlAttributes"></param>
//        /// <returns></returns>
//        protected TagBuilder CreateTag(string tagName, string nameInfo = "", string idInfo = "", string valueInfo = "", IDictionary<string, string> htmlAttributes = null)
//        {
//            TagBuilder tagBuilder = base.CreateTag(tagName, nameInfo, idInfo);
//            if (string.IsNullOrWhiteSpace(valueInfo))
//            {
//                tagBuilder.Attributes["value"] = value;
//            }
//            else
//            {
//                tagBuilder.Attributes["value"] = valueInfo;
//            }

//            tagBuilder.MergeAttributes(htmlAttributes);

//            return tagBuilder;
//        }
//    }
//}
