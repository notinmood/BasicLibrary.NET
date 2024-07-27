//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Web.Mvc;

//namespace Hiland.BasicLibrary4.MVC.Controls
//{
//    /// <summary>
//    /// 文本输入控件
//    /// </summary>
//    /// <remarks>
//    /// 如果要启用自动完成功能，必须引用jquery-ui.js和jquery-ui.css。
//    /// 如果设置了DynamicLoadDataUrl则自动启用自动完成功能
//    /// </remarks>
//    public class TextBoxControl : TextBoxControl<TextBoxControl>
//    {
//        private int minLengthForWakeup = 2;
//        /// <summary>
//        /// 设置能够唤醒自动提示的最短字符数（缺省为2）
//        /// </summary>
//        /// <param name="data"></param>
//        /// <returns></returns>
//        public TextBoxControl MinLengthForWakeup(int data)
//        {
//            this.minLengthForWakeup = data;
//            return this;
//        }

//        private string dynamicLoadDataUrl = string.Empty;
//        /// <summary>
//        /// 动态载入提示数据的地址
//        /// </summary>
//        /// <param name="data"></param>
//        /// <returns></returns>
//        /// <remarks>
//        /// 1、本地址对应的方法，在内部解析自动完成功能传入的已输入值时要从HttpRequest中读取“term”值（可以用RequestHelper.GetValue("term");接受）
//        /// 2、此地址返回的数据格式必须为 List<AutoCompleteEntity>集合的json格式
//        /// </remarks>
//        public TextBoxControl DynamicLoadDataUrl(string data)
//        {
//            this.dynamicLoadDataUrl = data;
//            return this;
//        }

//        /// <summary>
//        /// 是否启用自动完成
//        /// </summary>
//        private bool isUseAutoComplete
//        {
//            get
//            {
//                //如果this.dynamicLoadDataUrl不为空，表示启用自动完成功能
//                if (string.IsNullOrWhiteSpace(this.dynamicLoadDataUrl) == false)
//                {
//                    return true;
//                }
//                else
//                {
//                    return false;
//                }
//            }
//        }

//        private string extraParamFunctionName = "getAutoCompleteExtraParam";
//        /// <summary>
//        /// 设置获取在自动完成时，提交到服务器端的附加参数的JavaScript方法名称
//        /// </summary>
//        /// <param name="data"></param>
//        /// <returns></returns>
//        /// <remarks>
//        /// 本JavaScript函数应该返回一个数值。这数值将作为request["autoCompleteExtraParam"]的值被提交到服务器端
//        /// </remarks>
//        public TextBoxControl ExtraParamFunctionName(string data)
//        {
//            extraParamFunctionName = data;
//            return this;
//        }

//        private string selectedCallbackFunctionName = "autoCompleteSelectedCallback";
//        /// <summary>
//        /// 自动完成提示列表中选定项后的回调JS函数名称（缺省值为autoCompleteSelectedCallback）
//        /// </summary>
//        /// <param name="data"></param>
//        /// <returns></returns>
//        /// <remarks>
//        /// 此JS函数会传统一个参数item，其有如下几个属性（分别对应AutoCompleteEntity类的几个属性）
//        /// value：自动完成提示列表中选定项后，实际填充文本框的值
//        /// label：自动完成提示列表中显示的提示内容
//        /// key：自动完成提示列表中选定项后，后台对应的实际标志值（通常可以是某实体的ID信息）
//        /// details：自动完成提示列表中选定项后，可以传递和展示的其他内容
//        /// </remarks>
//        public TextBoxControl SelectedCallbackFunctionName(string data)
//        {
//            this.selectedCallbackFunctionName = data;
//            return this;
//        }

//        /// <summary>
//        /// 保存实际值的隐藏域的名称
//        /// </summary>
//        public string hiddenFieldName
//        {
//            get { return string.Format("{0}_Value", this.name); }
//        }

//        /// <summary>
//        /// 保存实际值的隐藏域的ID
//        /// </summary>
//        private string hiddenFieldID
//        {
//            get { return string.Format("{0}_Value", this.ID); }
//        }

//        /// <summary>
//        /// 保存实际值的隐藏域的值
//        /// </summary>
//        private string hiddenFieldValue = string.Empty;

//        /// <summary>
//        /// 设置保存实际值的隐藏域的值
//        /// </summary>
//        /// <param name="data"></param>
//        /// <returns></returns>
//        public TextBoxControl HiddenFieldValue(string data)
//        {
//            this.hiddenFieldValue = data;
//            return this;
//        }

//        /// <summary>
//        /// 绘制控件核心部分的Html代码
//        /// </summary>
//        /// <returns></returns>
//        protected override string WriteCoreHtml()
//        {
//            string result = base.WriteCoreHtml();

//            if (isUseAutoComplete == true)
//            {
//                StringBuilder sb = new StringBuilder();
//                sb.AppendFormat("<input id=\"{0}\" name=\"{1}\" type=\"hidden\" value=\"{2}\" />", 
//                    hiddenFieldID, hiddenFieldName, hiddenFieldValue);
//                result += new MvcHtmlString(sb.ToString());
//            }

//            return result;
//        }

//        /// <summary>
//        /// 绘制javascript脚本内容
//        /// </summary>
//        /// <param name="writer"></param>
//        protected override void WriteJavascriptContent(System.Web.UI.HtmlTextWriter writer)
//        {
//            if (isUseAutoComplete == true)
//            {
//                StringBuilder sb = new StringBuilder();
//                sb.AppendLine();
//                sb.AppendFormat(@"<script type='text/javascript'>
//                $(function ()|<|
//                    $('#{0}').autocomplete(|<|
//                      minLength: {1},
//                      source: function (request, response) |<|
//                            try |<|                     
//                                if (typeof eval('{5}') == 'function')|<|
//                                    request['autoCompleteExtraParam'] =eval('{5}')();
//                                |>|
//                            |>| catch (e) |<|
//                                 //do nothing;
//                            |>|                          

//                            $.ajax(|<|
//                              url: '{2}',
//                              dataType: 'json',
//                              data: request,
//                              success: function (data) |<|
//                                  response(data);
//                              |>|
//                          |>|);
//                      |>|,
//                      select: function (event, ui) |<|
//                            try |<|                     
//                                var realObj= $('#{4}');
//                                realObj.val(ui.item.key);

//                                if (typeof eval('{3}') == 'function')|<|
//                                    eval('{3}')(ui.item);
//                                |>|
//                            |>| catch (e) |<|
//                                 //do nothing;
//                            |>|
//                      |>|
//                    |>|);
//                |>|);
//                </script>", this.ID,
//                          this.minLengthForWakeup,
//                          this.dynamicLoadDataUrl,
//                          this.selectedCallbackFunctionName,
//                          this.hiddenFieldID,
//                          this.extraParamFunctionName
//                );

//                writer.Write(sb.Replace("|<|", "{").Replace("|>|", "}").ToString());
//            }
//        }
//    }

//    /// <summary>
//    /// 文本输入控件基类
//    /// </summary>
//    /// <typeparam name="T"></typeparam>
//    public class TextBoxControl<T> : InputControl<T> where T : InputControl<T>
//    {
//        /// <summary>
//        /// 构造函数
//        /// </summary>
//        public TextBoxControl()
//            : base()
//        {

//        }

//        /// <summary>
//        /// CSS类的名称
//        /// </summary>
//        protected override string cssClassName
//        {
//            get { return "textbox"; }
//        }

//        private bool _readOnly = false;
//        /// <summary>
//        /// 是否为只读性质
//        /// </summary>
//        protected virtual bool readOnly
//        {
//            get { return this._readOnly; }
//        }

//        /// <summary>
//        /// 设置是否为只读性质
//        /// </summary>
//        /// <param name="data"></param>
//        /// <returns></returns>
//        public T ReadOnly(bool data)
//        {
//            this._readOnly = data;
//            return this as T;
//        }

//        /// <summary>
//        /// 绘制控件核心部分的Html代码
//        /// </summary>
//        /// <returns></returns>
//        protected override string WriteCoreHtml()
//        {
//            return InputTag.ToString();
//        }

//        private TagBuilder inputTag = null;
//        /// <summary>
//        /// 输入标签
//        /// </summary>
//        protected TagBuilder InputTag
//        {
//            get
//            {
//                if (this.inputTag == null)
//                {
//                    this.inputTag = CreateTag("input");
//                    inputTag.Attributes["type"] = "text";

//                    if (this.usingMode == MvcControlUsingModes.Editable)
//                    {
//                        if (this.isDisabled)
//                        {
//                            inputTag.Attributes["disabled"] = "disabled";
//                        }
//                    }
//                    else
//                    {
//                        inputTag.Attributes["disabled"] = "disabled";
//                    }

//                    if (readOnly == true)
//                    {
//                        inputTag.Attributes["readonly"] = "readonly";
//                    }
//                }

//                return this.inputTag;
//            }
//        }
//    }
//}
