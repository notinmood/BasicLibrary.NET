//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Web.Mvc;

//namespace Hiland.BasicLibrary4.MVC.Controls
//{
//    /// <summary>
//    /// 下拉列表控件
//    /// </summary>
//    public class DropDownListControl : DropDownListControl<DropDownListControl>
//    {

//    }

//    /// <summary>
//    /// 下拉列表控件（抽象）
//    /// </summary>
//    /// <typeparam name="T"></typeparam>
//    /// <remarks>
//    /// 获取选中项显示文本时，可以使用RequestHelper.GetValue("{name}_Text")或者直接使用ControlHelper.GetDisplayText;
//    /// </remarks>
//    public class DropDownListControl<T> : MultiItemStatusSelectControl<T> where T : MultiItemStatusSelectControl<T>
//    {
//        /// <summary>
//        /// 
//        /// </summary>
//        public DropDownListControl()
//            : base()
//        {

//        }

//        private string optionLabel = string.Empty;
//        /// <summary>
//        /// 选择项的提示文本
//        /// </summary>
//        /// <param name="data"></param>
//        /// <returns></returns>
//        public T OptionLabel(string data)
//        {
//            this.optionLabel = data;
//            return this as T;
//        }

//        /// <summary>
//        /// 绘制控件核心部分的Html代码
//        /// </summary>
//        /// <returns></returns>
//        protected override string WriteCoreHtml()
//        {
//            TagBuilder tagSelect = CreateTag("select");
//            if (this.usingMode == MvcControlUsingModes.Display)
//            {
//                tagSelect.Attributes["disabled"] = "disabled";
//            }

//            tagSelect.InnerHtml = GetOptionItemString();

//            string hiddenName = string.Format("{0}_Text", name);
//            StringBuilder sb = new StringBuilder();
//            sb.Append(tagSelect.ToString());
//            sb.AppendFormat("<input id=\"{0}\" name=\"{0}\" type=\"hidden\" />", hiddenName);
//            sb.AppendFormat(@"<script type='text/javascript'>
//                $(document).ready(function () |<|
//                    $('#{0}').change(function () |<|
//                        var valueSelected = $(this).children('option:selected').val();
//                        var textSelected = '';
//                        if (valueSelected != '') |<|
//                            textSelected = $(this).children('option:selected').text();
//                        |>|
//                        $('#{1}').val(textSelected);
//                    |>|);
//                |>|);
//                </script>
//            ", ID, hiddenName);

//            MvcHtmlString result = new MvcHtmlString(sb.Replace("|<|", "{").Replace("|>|", "}").ToString());
//            return result.ToHtmlString();
//        }

//        private string GetOptionItemString()
//        {
//            string result = string.Empty;
//            if (string.IsNullOrWhiteSpace(this.optionLabel) == false)
//            {
//                result = string.Format("<option>{0}</option>", this.optionLabel);
//            }

//            foreach (SelectListItem item in itemList)
//            {
//                string selectedString = string.Empty;
//                if (item.Value == value || item.Selected == true)
//                {
//                    selectedString = "selected=\"selected\"";
//                }
//                result += string.Format("<option value=\"{0}\" {2}>{1}</option>", item.Value, item.Text, selectedString);
//            }

//            return result;
//        }

//        /// <summary>
//        /// 控件类型的名称（checkbox还是radio）
//        /// </summary>
//        protected override string InputTypeName
//        {
//            get { return ""; }
//        }
//    }
//}
