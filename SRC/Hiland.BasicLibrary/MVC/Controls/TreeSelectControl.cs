//using System.Text;
//using System.Web.Mvc;
//using HiLand.Utility.Enums;

//namespace HiLand.Utility4.MVC.Controls
//{
//    /// <summary>
//    /// 树形选择控件
//    /// </summary>
//    /// <remarks>
//    /// 1.本控件使用了ztree，请在属性JavaScriptFiles里面设置 jQuery和jquery.ztree.all-3.3.js
//    ///                          请在属性StyleSheetFiles里面设置 zTreeStyle.css
//    ///                          
//    /// 2.关于jquery.ztree.all-3.3.js和zTreeStyle.css，请参见http://www.ztree.me的官方文档
//    /// 3.本控件向外暴露2个值，一个是显示的Name（通常是选定节点的名称，通过属性Name获取或设置）；一个是真实的value（通常是选定节点的ID，通过字段hiddenFieldName获取）
//    /// 4.动态加载节点的属性DataUrl，具体可以参考SampleConsoleMvcApplication\Controllers\HomeController.cs内的方法TreeData()
//    /// </remarks>
//    public class TreeSelectControl : TextBoxControl<TreeSelectControl>
//    {
//        /// <summary>
//        /// 绘制控件核心部分的Html代码
//        /// </summary>
//        /// <returns></returns>
//        protected override string WriteCoreHtml()
//        {
//            InputTag.Attributes["value"] = this.text;
//            string result = InputTag.ToString();
//            result += WriterHiddenField();
//            result += WriterTreeContainer();
//            result += WriteTreeScript();

//            return result;
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        protected override bool readOnly
//        {
//            get
//            {
//                return true;
//            }
//        }

//        /// <summary>
//        /// 绘制保存实际值（通常是选定节点的ID）的隐藏域
//        /// </summary>
//        /// <returns></returns>
//        private string WriterHiddenField()
//        {
//            string result = string.Empty;
//            TagBuilder tagInput = CreateTag("input", hiddenFieldName, hiddenFieldID);
//            tagInput.Attributes["type"] = "hidden";
//            result = tagInput.ToString();
//            return result;
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

//        private string _text = string.Empty;

//        private string text
//        {
//            get
//            {
//                //if (string.IsNullOrWhiteSpace(this._text))
//                //{
//                //    _text = this.value;
//                //}

//                return _text;
//            }
//        }

//        /// <summary>
//        /// 设置控件的文本
//        /// </summary>
//        /// <param name="data"></param>
//        /// <returns></returns>
//        public TreeSelectControl Text(string data)
//        {
//            this._text = data;
//            return this;
//        }


//        private string treeID
//        {
//            get
//            {
//                return string.Format("{0}_tree", this.name);
//            }
//        }


//        private string treeContainerID
//        {
//            get
//            {
//                return string.Format("{0}_menuContent", this.name);
//            }
//        }

//        private string dynamicDataUrl = string.Empty;
//        /// <summary>
//        /// 动态获取数据的URL
//        /// </summary>
//        /// <param name="data"></param>
//        /// <returns></returns>
//        public TreeSelectControl DynamicDataUrl(string data)
//        {
//            this.dynamicDataUrl = data;
//            return this;
//        }

//        private string staticDataNodes = string.Empty;
//        /// <summary>
//        /// 静态数据节点集合
//        /// </summary>
//        /// <param name="data">必须是JSON类型的数据（如果有嵌套关系，请使用zTree的简单数据模式）</param>
//        /// <returns></returns>
//        public TreeSelectControl StaticDataNodes(string data)
//        {
//            this.staticDataNodes = data;
//            return this;
//        }


//        private DataLoadTypes dataLoadType = DataLoadTypes.Static;
//        /// <summary>
//        /// 数据载入的方式
//        /// </summary>
//        /// <param name="data"></param>
//        /// <returns></returns>
//        public TreeSelectControl DataLoadType(DataLoadTypes data)
//        {
//            this.dataLoadType = data;
//            return this;
//        }

//        private DataSelectTypes dataSelectType = DataSelectTypes.Checkbox;
//        /// <summary>
//        /// 数据选择的方式
//        /// </summary>
//        /// <param name="data"></param>
//        /// <returns></returns>
//        public TreeSelectControl DataSelectType(DataSelectTypes data)
//        {
//            this.dataSelectType = data;
//            return this;
//        }

//        private bool isInPopupWindow = false;
//        /// <summary>
//        /// 本树是否在弹出窗口中使用
//        /// </summary>
//        /// <param name="data"></param>
//        /// <returns></returns>
//        public TreeSelectControl IsInPopupWindow(bool data)
//        {
//            this.isInPopupWindow = data;
//            return this;
//        }

//        /// <summary>
//        /// 树容器使用的布局方式
//        /// </summary>
//        private string containerLayout
//        {
//            get
//            {
//                string result = "absolute";
//                if (this.isInPopupWindow == true)
//                {
//                    result = "relative";
//                }
//                return result;
//            }
//        }

//        private string WriterTreeContainer()
//        {
//            StringBuilder sb = new StringBuilder();
//            sb.AppendFormat("<a id='{0}_menuBtn' href='#' onclick='{0}_showMenu(); return false;'>select</a>", this.name);
//            sb.AppendFormat("<div id=\"{0}\" class=\"menuContent\" style=\"display:none; position: {1};\">", treeContainerID,this.containerLayout);
//            sb.AppendFormat("<ul id=\"{0}\" class=\"ztree\" style=\"margin-top:0; width:180px; height: 200px;\"></ul>", treeID);
//            sb.AppendFormat("</div>");
//            return sb.ToString();
//        }

//        private string WriteTreeScript()
//        {
//            StringBuilder sb = new StringBuilder();
//            sb.Append("<script type=\"text/javascript\">");
//            sb.AppendLine();
//            sb.AppendFormat(@"var {0}_setting = |<|
//                callback: |<|
//                        onClick: {0}_onClick,				        
//                        beforeClick: {0}_beforeClick,
//				        onCheck: {0}_onCheck
//			        |>|,", this.name);

//            if (dataSelectType == DataSelectTypes.Checkbox)
//            {
//                sb.Append(@"check: |<|
//				    enable: true,
//				    chkboxType: |<|'Y':'', 'N':''|>|
//			    |>|,");
//            }
//            else
//            {
//                sb.Append(@"check: |<|
//				    enable: true,
//				    chkStyle: 'radio',
//                    radioType: 'all'
//			    |>|,");

//                sb.Append(@"data: |<|
//				    simpleData: |<|
//				    enable: true
//                    |>|
//			    |>|,");
//            }

//            if (dataLoadType == DataLoadTypes.Dynamic)
//            {
//                sb.AppendFormat(@"  async: |<|
//				    enable: true,
//				    url:'{0}',
//				    autoParam:['id', 'name=n', 'level=lv'],
//				    otherParam:|<|'otherParam':'zTreeAsyncTest'|>|,
//				    dataFilter: filter
//			    |>|,", dynamicDataUrl);
//            }

//            sb.Append(@"view: |<|
//				        dblClickExpand: false
//			        |>|");
//            sb.Append("|>|;");
//            sb.AppendLine();

//            if (dataSelectType == DataSelectTypes.Checkbox)
//            {
//                sb.AppendFormat(@"function {1}_beforeClick(treeId, treeNode) |<|
//			    var zTree = $.fn.zTree.getZTreeObj('{0}');
//			    zTree.checkNode(treeNode, !treeNode.checked, null, true);
//			    return false;|>|", treeID, this.name);
//                sb.AppendLine();
//                sb.AppendFormat(@"function {0}_onClick(e, treeId, treeNode) |<|
//			    |>|", this.name);
//            }
//            else
//            {
//                sb.AppendFormat(@"function {0}_beforeClick(treeId, treeNode) |<|
//			    |>|", this.name);
//                sb.AppendLine();
//                sb.AppendFormat(@"function {1}_onClick(e, treeId, treeNode) |<|
//                    var zTree = $.fn.zTree.getZTreeObj('{0}');
//                    zTree.checkNode(treeNode, !treeNode.checked, null, true);
//			    |>|", treeID, this.name);
//            }

//            sb.AppendLine();
//            sb.AppendFormat(@"function {3}_onCheck(e, treeId, treeNode) |<|
//			    var zTree = $.fn.zTree.getZTreeObj('{0}'),
//			    nodes = zTree.getCheckedNodes(true),
//			    displayValue = '';
//                realValue= '';
                
//			    for (var i=0, l=nodes.length; i<l; i++) |<|
//				    displayValue += nodes[i].name + ',';
//                    realValue+= nodes[i].id +',';
//			    |>|
//			    if (displayValue.length > 0 ) displayValue = displayValue.substring(0, displayValue.length-1);
//                if (realValue.length > 0 ) realValue= realValue.substring(0,realValue.length-1);
//			    var cityObj = $('#{1}');
//			    cityObj.attr('value', displayValue);
//                var realObj= $('#{2}');
//                realObj.val(realValue);", treeID, ID, hiddenFieldID, this.name);

//            //如果是单选类型，那么点击选择后就可以立即关闭弹出树了。
//            if (dataSelectType == DataSelectTypes.Radio)
//            {
//                sb.AppendFormat("{0}_hideMenu();",this.name);
//            }


//            sb.Append("|>|");


//            sb.AppendLine();

//            if (this.isInPopupWindow == true)
//            {
//                sb.AppendFormat(@"function {2}_showMenu() |<|
//			        $('#{1}').css(|<|left:+ '0px', top: + '0px'|>|).slideDown('fast');
//			        $('body').bind('mousedown', {2}_onBodyDown);
//		        |>|", ID, treeContainerID, this.name);
//            }
//            else
//            {
//                sb.AppendFormat(@"function {2}_showMenu() |<|
//			        var cityObj = $('#{0}');
//			        var cityOffset = $('#{0}').offset();
//			        $('#{1}').css(|<|left:cityOffset.left + 'px', top:cityOffset.top + cityObj.outerHeight() + 'px'|>|).slideDown('fast');
//			        $('body').bind('mousedown', {2}_onBodyDown);
//		        |>|", ID, treeContainerID, this.name);
//            }

//            sb.AppendLine();
//            sb.AppendFormat(@"function {1}_hideMenu() |<|
//			    $('#{0}').fadeOut('fast');
//			    $('body').unbind('mousedown', {1}_onBodyDown);
//		    |>|", treeContainerID, this.name);


//            sb.AppendLine();
//            sb.AppendFormat(@"function {2}_onBodyDown(event) |<|
//			    if (!(event.target.id == '{2}_menuBtn' || event.target.id == '{0}' || event.target.id == '{1}' || $(event.target).parents('#{1}').length>0)) |<|
//				    {2}_hideMenu();
//			    |>|
//		    |>|", ID, treeContainerID, this.name);

//            sb.AppendLine();
//            sb.AppendFormat(@"function filter(treeId, parentNode, childNodes) |<|
//			    if (!childNodes) return null;
//			    for (var i=0, l=childNodes.length; i<l; i++) |<|
//				    childNodes[i].name = childNodes[i].name.replace(/\.n/g, '.');
//			    |>|
//			    return childNodes;
//		    |>|");



//            sb.AppendLine();
//            if (dataLoadType == DataLoadTypes.Dynamic)
//            {
//                sb.AppendFormat(@"$(document).ready(function()|<|
//			        $.fn.zTree.init($('#{0}'), {1}_setting);
//		        |>|);", treeID, this.name);
//            }
//            else
//            {
//                sb.AppendFormat(@"var zNodes ={0};", this.staticDataNodes);
//                sb.AppendLine();
//                sb.AppendFormat(@"$(document).ready(function()|<|
//			        $.fn.zTree.init($('#{0}'), {1}_setting, zNodes);
//		        |>|);", treeID, this.name);
//            }

//            sb.AppendLine("</script>");

//            return sb.Replace("|<|", "{").Replace("|>|", "}").ToString();
//        }
//    }
//}