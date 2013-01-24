using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using HiLand.Utility.Data;
using HiLand.Utility.Enums;
using HiLand.Utility.Enums.OP;
using HiLand.Utility.Setting;

namespace HiLand.Utility4.MVC.Controls
{
    /// <summary>
    /// 综合查询控件
    /// </summary>
    /// <remarks>
    /// 此控件因为为日期输入使用了选择框，那么需要使用此控件的页面加入对css文件（jQuery.tools.dateinput.css）的引用
    /// </remarks>
    public class QueryControl : BaseControl<QueryControl>
    {
        private string queryButtonTextForOpen = "<i class=\"icon-zoom-in  icon-white\"></i>展开查询";
        public QueryControl QueryButtonTextForOpen(string data)
        {
            this.queryButtonTextForOpen = data;
            return this;
        }

        //TODO:xieran20121108需要在下面JS中使用这个变量
        private string queryButtonTextForClose = "<i class=\"icon-zoom-out  icon-white\"></i>收起查询";
        public QueryControl QueryButtonTextForClose(string data)
        {
            this.queryButtonTextForClose = data;
            return this;
        }

        private bool isDisplayBrackets = true;
        /// <summary>
        /// 是否显示括号运算符(其可以在多个条件之间设置优先级)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public QueryControl IsDisplayBrackets(bool data)
        {
            this.isDisplayBrackets = data;
            return this;
        }

        private bool isDisplayTableHead = false;
        /// <summary>
        /// 是否显示表头
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public QueryControl IsDisplayTableHead(bool data)
        {
            this.isDisplayTableHead = data;
            return this;
        }


        /// <summary>
        /// 输出控件Html代码
        /// </summary>
        /// <param name="writer"></param>
        protected override void WriteHtml(System.Web.UI.HtmlTextWriter writer)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table class=\"{0}\" style=\"width: 100%;\">", this.cssClassName);

            sb.Append("<tr>");
            if (isDisplayTableHead == true)
            {
                if (this.isDisplayBrackets == true)
                {
                    sb.AppendFormat("<th style=\"width: 2%;\">{0}</th>", "(");
                }
                sb.AppendFormat("<th style=\"width: 20%;\">{0}</th>", "名称");
                sb.AppendFormat("<th style=\"width: 5%;\">{0}</th>", "符号");
                sb.AppendFormat("<th style=\"width: auto;\">{0}</th>", "值");
                if (this.isDisplayBrackets == true)
                {
                    sb.AppendFormat("<th style=\"width: 2%;\">{0}</th>", ")");
                }
            }
            else
            {
                if (this.isDisplayBrackets == true)
                {
                    sb.Append("<th colspan=\"5\" style=\"width: 88%;\"></th>");
                }
                else
                {
                    sb.Append("<th colspan=\"3\" style=\"width: 88%;\"></th>");
                }
            }
            sb.AppendFormat("<th style=\"width: 12%;\">{0}<span class=\"queryButton btn btn-warning\">{2}</span>{1}</th>", "", GetQueryControlDisplayStatusString(), this.queryButtonTextForOpen);
            sb.Append("</tr>");

            for (int i = 0; i < queryConditionList.Count; i++)
            {
                QueryConditionItem currentItem = queryConditionList[i];
                bool isLastItem = false;
                if (i == queryConditionList.Count - 1)
                {
                    isLastItem = true;
                }
                sb.Append("<tr>");
                if (this.isDisplayBrackets == true)
                {
                    sb.AppendFormat("<td >{0}</td>", GetLeftBracketsString(i));
                }
                sb.AppendFormat("<td >{0}</td>", GetConditionNameString(currentItem, i));
                sb.AppendFormat("<td >{0}</td>", GetConditionOperatorString(currentItem, i));
                sb.AppendFormat("<td >{0}</td>", GetConditionValueString(currentItem, i));
                if (this.isDisplayBrackets == true)
                {
                    sb.AppendFormat("<td >{0}</td>", GetRightBracketsString(i));
                }
                sb.AppendFormat("<td >{0}</td>", GetRelationshipString(i, isLastItem));
                sb.Append("</tr>");
            }

            sb.Append("</table>");
            writer.Write(sb.ToString());
        }

        /// <summary>
        /// 获取查询控件展开/关闭模式的字符串
        /// </summary>
        /// <returns></returns>
        private string GetQueryControlDisplayStatusString()
        {
            string queryControlDisplayStatusFullName = this.name + QueryControlHelper.QueryControlDisplayStatusStringConst;
            string queryControlDisplayStatusValue = MVCHelper.GetParam(queryControlDisplayStatusFullName, "closed");
            return string.Format("<input type=\"hidden\" name=\"{0}\" class=\"queryStatus\" value=\"{1}\">", queryControlDisplayStatusFullName, queryControlDisplayStatusValue);
        }

        /// <summary>
        /// 绘制javascript脚本内容
        /// </summary>
        /// <param name="writer"></param>
        protected override void WriteJavascriptContent(System.Web.UI.HtmlTextWriter writer)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"
                <script type='text/javascript'>
                    $(document).ready(function () {   
                        var queryStatusValue= $('.queryButton').parents('tr').find('.queryStatus').val();                 
                        if(queryStatusValue=='closed'){
                            $('.queryButton').parents('tr').siblings().hide();
                        }
                        else{
                            $('.queryButton').parents('tr').siblings().show();
                        }

                        $('.queryButton').toggle(
                            function () { 
                                var trObject = $(this).parents('tr');
                                trObject.siblings().show();
                                trObject.find('.queryStatus').val('open');
                                $(this).html('<i class=\'icon-zoom-out  icon-white\'></i>收起查询');
                            },
                            function () { 
                                var trObject = $(this).parents('tr');                                
                                trObject.siblings().hide(); 
                                trObject.find('.queryStatus').val('closed');
                                $(this).html('<i class=\'icon-zoom-in  icon-white\'></i>展开查询');
                            }
                        );
                    });
                </script>
            ");

            writer.Write(sb.ToString());
        }

        /// <summary>
        /// 展示表的Css类名称
        /// </summary>
        protected override string cssClassName
        {
            get
            {
                return "table";
            }
        }

        private bool isDisplayQueryButton = true;
        /// <summary>
        /// 是否显示查询按钮
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public QueryControl IsDisplayQueryButton(bool data)
        {
            this.isDisplayQueryButton = data;
            return this;
        }

        /// <summary>
        /// 获取左括号的字符串
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private string GetLeftBracketsString(int number)
        {
            string leftBracketsFullName = this.name + QueryControlHelper.LeftBracketsNameStringConst + number;
            string leftBracketsValue = MVCHelper.GetParam(leftBracketsFullName);
            string checkedString = string.Empty;
            if (leftBracketsValue.ToLower() == "on")
            {
                checkedString = "checked=\"checked\"";
            }

            return string.Format("<input type=\"checkbox\" name=\"{0}\"  {1}/>", leftBracketsFullName, checkedString);
        }

        /// <summary>
        /// 获取右括号的字符串
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private string GetRightBracketsString(int number)
        {
            string rightBracketsFullName = this.name + QueryControlHelper.RightBracketsNameStringConst + number;
            string rightBracketsValue = MVCHelper.GetParam(rightBracketsFullName);
            string checkedString = string.Empty;
            if (rightBracketsValue.ToLower() == "on")
            {
                checkedString = "checked=\"checked\"";
            }

            return string.Format("<input type=\"checkbox\" name=\"{0}\"  {1}/>", rightBracketsFullName, checkedString);
        }

        /// <summary>
        /// 获取查询项条件中字段的名称字符串
        /// </summary>
        /// <param name="queryConditionItem"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        private string GetConditionNameString(QueryConditionItem queryConditionItem, int number)
        {
            if (queryConditionItem.isMultiFieldName == true)
            {
                string conditionFieldFullName = this.name + QueryControlHelper.ConditionFieldNameStringConst + number;
                string conditionFieldValue = MVCHelper.GetParam(conditionFieldFullName);

                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("<select name=\"{0}\">", this.name + QueryControlHelper.ConditionFieldNameStringConst + number);
                for (int i = 0; i < queryConditionItem.ConditionFieldNames.Length; i++)
                {
                    string selectedString = string.Empty;
                    if (conditionFieldValue == queryConditionItem.ConditionFieldNames[i])
                    {
                        selectedString = "selected=\"selected\"";
                    }

                    sb.AppendFormat("<option value=\"{0}\" {1}>{2}</option>", queryConditionItem.ConditionFieldNames[i], selectedString, queryConditionItem.ConditionDisplayNames[i]);
                }
                sb.Append("</select>");

                return sb.ToString();
            }
            else
            {
                return string.Format("{0}<input type=\"hidden\" name=\"{1}\" value=\"{2}\" />", queryConditionItem.ConditionDisplayName, this.name + QueryControlHelper.ConditionFieldNameStringConst + number, queryConditionItem.ConditionFieldName);
            }
        }

        /// <summary>
        /// 获取查询项条件中值字符串
        /// </summary>
        /// <param name="queryConditionItem"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        private string GetConditionValueString(QueryConditionItem queryConditionItem, int number)
        {
            StringBuilder result = new StringBuilder();

            //记录查询条件值的类型
            result.AppendFormat("<input type=\"hidden\" name=\"{0}\" value=\"{1}\">", this.name + QueryControlHelper.ConditionTypeNameStringConst + number, TypeHelper.GetTypeShortDescription(queryConditionItem.ConditionType));

            string conditionValueFullName = this.name + QueryControlHelper.ConditionValueNameStringConst + number;
            string conditionValueValue = MVCHelper.GetParam(conditionValueFullName);

            if (queryConditionItem.ConditionType.IsEnum == true)
            {
                string enumDisplayCategory = queryConditionItem.GetAddonItem("enumDisplayCategory");
                ListItemCollection coll = EnumBuilder.BuildItemCollection(queryConditionItem.ConditionType, enumDisplayCategory, true);
                result.AppendFormat("<select name=\"{0}\">", conditionValueFullName);

                foreach (ListItem currentItem in coll)
                {
                    string itemSelectedString = string.Empty;
                    if (string.IsNullOrEmpty(currentItem.Value) == false)
                    {
                        if (Enum.IsDefined(queryConditionItem.ConditionType, conditionValueValue))
                        {
                            if (currentItem.Value == Enum.Parse(queryConditionItem.ConditionType, conditionValueValue).ToString())
                            {
                                itemSelectedString = "selected";
                            }
                        }
                    }
                    result.AppendFormat("<option value=\"{0}\" {1}>{2}</option>", currentItem.Value, itemSelectedString, currentItem.Text);
                }

                result.Append("</select>");
            }
            else
            {
                if (queryConditionItem.ConditionValueItems.Count > 0)
                {
                    result.AppendFormat("<select name=\"{0}\">", conditionValueFullName);
                    foreach (ListItem currentItem in queryConditionItem.ConditionValueItems)
                    {
                        string itemSelectedString = string.Empty;
                        if (string.IsNullOrEmpty(currentItem.Value) == false)
                        {
                            if (currentItem.Value == conditionValueValue)
                            {
                                itemSelectedString = "selected";
                            }
                        }
                        result.AppendFormat("<option value=\"{0}\" {1}>{2}</option>", currentItem.Value, itemSelectedString, currentItem.Text);
                    }

                    result.Append("</select>");
                }
                else
                {
                    //if (queryConditionItem.ConditionType == typeof(String) ||
                    //    (TypeHelper.ConfirmIsNumberType(queryConditionItem.ConditionType) == true && queryConditionItem.ConditionType.IsEnum == false) ||
                    //    queryConditionItem.ConditionType == typeof(DateTime))

                    string innerInputClass = string.Empty;

                    if (queryConditionItem.ConditionType == typeof(DateTime))
                    {
                        innerInputClass = "innerDateControl";
                        string dateInputOptions = "{format: 'yyyy/mm/dd'}";
                        string dateFormat = queryConditionItem.GetAddonItem("dateFormat",string.Empty);
                        if (dateFormat != string.Empty)
                        {
                            dateInputOptions = "{format: '"+ dateFormat +"'}";
                        }
                        StringBuilder sb = new StringBuilder();
                        sb.Append("<script type=\"text/javascript\">");
                        sb.Append(" jQuery(document).ready(function () {");
                        sb.AppendFormat("$(\".{0}\").dateinput({1});", innerInputClass, dateInputOptions);
                        sb.Append("});</script>");
                        result.Append(sb.ToString());
                    }

                    result.AppendFormat("<input type=\"text\" name=\"{0}\" class=\"{2}\" value=\"{1}\" />", conditionValueFullName, conditionValueValue, innerInputClass);
                }
            }

            return result.ToString();
        }

        /// <summary>
        /// 获取查询条件中条件名称和条件值之间的连接关系字符串
        /// </summary>
        /// <param name="queryConditionItem"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        private string GetConditionOperatorString(QueryConditionItem queryConditionItem, int number)
        {
            StringBuilder result = new StringBuilder();

            string conditionOperatorFullName = this.name + QueryControlHelper.ConditionOperatorNameStringConst + number;
            string conditionOperatorValue = MVCHelper.GetParam(conditionOperatorFullName);
            result.AppendFormat("<select name=\"{0}\">", conditionOperatorFullName);

            //TODO:xieran20121022 考虑将自动完成作为一个单独的类型在查询控件内部使用
            //queryConditionItem.ConditionType.is

            if (queryConditionItem.ConditionType.IsEnum == true)
            {
                result.Append(GetCompareModeOptionString(CompareModes.Equal, conditionOperatorValue));
                result.Append(GetCompareModeOptionString(CompareModes.NotEqual, conditionOperatorValue));
            }

            if (queryConditionItem.ConditionType == typeof(string))
            {
                result.Append(GetCompareModeOptionString(CompareModes.Like, conditionOperatorValue));
                result.Append(GetCompareModeOptionString(CompareModes.Equal, conditionOperatorValue));
                result.Append(GetCompareModeOptionString(CompareModes.LikeLeft, conditionOperatorValue));
                result.Append(GetCompareModeOptionString(CompareModes.LikeRight, conditionOperatorValue));
            }

            if (TypeHelper.ConfirmIsNumberType(queryConditionItem.ConditionType) == true && queryConditionItem.ConditionType.IsEnum == false)
            {
                result.Append(GetCompareModeOptionString(CompareModes.Equal, conditionOperatorValue));
                result.Append(GetCompareModeOptionString(CompareModes.NotEqual, conditionOperatorValue));
                result.Append(GetCompareModeOptionString(CompareModes.LessThan, conditionOperatorValue));
                result.Append(GetCompareModeOptionString(CompareModes.MoreThan, conditionOperatorValue));
                result.Append(GetCompareModeOptionString(CompareModes.NotLessThan, conditionOperatorValue));
                result.Append(GetCompareModeOptionString(CompareModes.NotMoreThan, conditionOperatorValue));
            }

            if (queryConditionItem.ConditionType == typeof(DateTime))
            {
                result.Append(GetCompareModeOptionString(CompareModes.Equal, conditionOperatorValue, "date-SQL"));
                result.Append(GetCompareModeOptionString(CompareModes.LessThan, conditionOperatorValue, "date-SQL"));
                result.Append(GetCompareModeOptionString(CompareModes.MoreThan, conditionOperatorValue, "date-SQL"));
            }

            result.Append("</select>");

            return result.ToString();
        }

        /// <summary>
        /// 获取比较模式选项字符串
        /// </summary>
        /// <param name="compareMode"></param>
        /// <param name="selectedValue"></param>
        /// <returns></returns>
        private string GetCompareModeOptionString(CompareModes compareMode, string selectedValue,string displayTextCategory="")
        {
            string itemSelectedString = string.Empty;
            string text = EnumHelper.GetDisplayValue(compareMode, displayTextCategory);
            string value = EnumHelper.GetDisplayValue(compareMode, this.SQLMode);

            if (value == selectedValue)
            {
                itemSelectedString = "selected";
            }

            return string.Format("<option value=\"{0}\" {1}>{2}</option>", value, itemSelectedString, text);
        }

        /// <summary>
        /// 获取多个条件查询项之间的连接关系字符串
        /// </summary>
        /// <param name="number"></param>
        /// <param name="isLastItem"></param>
        /// <returns></returns>
        private string GetRelationshipString(int number, bool isLastItem)
        {
            StringBuilder result = new StringBuilder();

            if (isLastItem == true)
            {
                if (isDisplayQueryButton == true)
                {
                    result.AppendFormat("<input type=\"submit\" />");
                }
                result.AppendFormat("<input type=\"hidden\" name=\"{0}\" value=\"{1}\" />", this.name + QueryControlHelper.ConditionCountNameStringConst, number + 1);
            }
            else
            {
                string conditionRelationshipFullName = this.name + QueryControlHelper.ConditionRelationshipNameStringConst + number;
                string conditionRelationshipValue = MVCHelper.GetParam(conditionRelationshipFullName);
                string selectedStringForAND = string.Empty;
                string selectedStringForOr = string.Empty;
                if (conditionRelationshipValue == ConditionItemRelationships.AND.ToString())
                {
                    selectedStringForAND = "selected";
                }

                if (conditionRelationshipValue == ConditionItemRelationships.OR.ToString())
                {
                    selectedStringForOr = "selected";
                }

                result.AppendFormat("<select name=\"{0}\">", conditionRelationshipFullName);

                result.AppendFormat("<option value=\"{0}\" {1}>{2}</option>", ConditionItemRelationships.AND, selectedStringForAND, EnumHelper.GetDisplayValue(ConditionItemRelationships.AND));
                result.AppendFormat("<option value=\"{0}\" {1}>{2}</option>", ConditionItemRelationships.OR, selectedStringForOr, EnumHelper.GetDisplayValue(ConditionItemRelationships.OR));
                result.Append("</select>");
            }

            return result.ToString();
        }

        private List<QueryConditionItem> queryConditionList = new List<QueryConditionItem>();
        /// <summary>
        /// 设置查询条件集合
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public QueryControl QueryConditionList(List<QueryConditionItem> data)
        {
            if (data != null)
            {
                queryConditionList = data;
            }

            return this;
        }

        private string sqlMode = string.Empty;
        /// <summary>
        /// SQL语句的模式（标准sql语句、或者其他单独某数据库厂商的模式，默认值为stand-SQL）
        /// </summary>
        private string SQLMode
        {
            get
            {
                if (string.IsNullOrEmpty(this.sqlMode))
                {
                    this.sqlMode = Config.GetAppSetting("SQLMode", "stand-SQL");
                }

                return this.sqlMode;
            }
        }
    }

    /// <summary>
    /// 查询条件项
    /// ConditionFieldName和ConditionDisplayName可以设置多个字段的信息，
    /// 多个字段之间用“,”进行分割。但是这个多个字段必须是同种数据类型的。设置时，
    /// 请注意ConditionFieldName和ConditionDisplayName的内部的数量保证一致。
    /// </summary>
    public class QueryConditionItem
    {
        /// <summary>
        /// 在字段名称里面是否包含多个值
        /// </summary>
        internal bool isMultiFieldName
        {
            get
            {
                if (this.conditionDisplayName.IndexOf(",") > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        internal string[] ConditionDisplayNames
        {
            get
            {
                return StringHelper.SplitToArray(this.ConditionDisplayName);
            }
        }

        internal string[] ConditionFieldNames
        {
            get
            {
                return StringHelper.SplitToArray(this.ConditionFieldName);
            }
        }


        private string conditionDisplayName = string.Empty;
        /// <summary>
        /// 查询条件项显示的名称
        /// </summary>
        public string ConditionDisplayName
        {
            get
            {
                if (string.IsNullOrEmpty(this.conditionDisplayName))
                {
                    this.conditionDisplayName = this.conditionFieldName;
                }

                return this.conditionDisplayName;
            }
            set
            {
                this.conditionDisplayName = value;
            }
        }


        private string conditionFieldName = string.Empty;
        /// <summary>
        /// 查询条件项对应的数据库字段名称（可以设置多个字段，多个字段之间用“,”进行分割。但是这个多个字段必须是同种数据类型的）
        /// </summary>
        public string ConditionFieldName
        {
            get
            {
                return this.conditionFieldName;
            }
            set
            {
                this.conditionFieldName = value;
            }
        }

        /// <summary>
        /// 查询条件项的数据类型
        /// </summary>
        /// <remarks>请一定跟数据库字段的类型匹配（如果设置ConditionValueItems的值系统也会将其进行转换）</remarks>
        public Type ConditionType { get; set; }

        private List<ListItem> conditionValueItems = new List<ListItem>();
        /// <summary>
        /// 查询条件值的各个可选择项（在控件内部使用下拉列表的方式来呈现）
        /// </summary>
        public List<ListItem> ConditionValueItems
        {
            get { return conditionValueItems; }
            set { this.conditionValueItems = value; }
        }

        /// <summary>
        /// 查询条件项的附加数据
        /// </summary>
        /// <remarks>
        /// 其格式为 key1:value1||key2:value2
        /// </remarks>
        public string AddonData
        {
            get;
            set;
        }

        /// <summary>
        /// 获取查询条件附加数据中的某个附加信息的值
        /// </summary>
        /// <returns></returns>
        public string GetAddonItem(string itemName, string defaultValue = StringHelper.Empty)
        {
            string result = defaultValue;
            if (string.IsNullOrWhiteSpace(AddonData) == false)
            {
                Dictionary<string, string> addonDic = StringHelper.SplitToDictionary(AddonData, ":", "||");
                if (addonDic.ContainsKey(itemName))
                {
                    result = addonDic[itemName];
                }
            }

            return result;
        }
    }
}
