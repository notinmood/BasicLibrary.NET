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
    public class QueryControl : BaseControl<QueryControl>
    {
        /// <summary>
        /// 输出控件Html代码
        /// </summary>
        /// <param name="writer"></param>
        protected override void WriteHtml(System.Web.UI.HtmlTextWriter writer)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<table class=\"{0}\" style=\"width: 100%;\">", this.cssClassName);

            sb.Append("<tr>");
            sb.AppendFormat("<th style=\"width: 2%;\">{0}</th>", "(");
            sb.AppendFormat("<th style=\"width: 20%;\">{0}</th>", "名称");
            sb.AppendFormat("<th style=\"width: 5%;\">{0}</th>", "符号");
            sb.AppendFormat("<th style=\"width: auto;\">{0}</th>", "值");
            sb.AppendFormat("<th style=\"width: 2%;\">{0}</th>", ")");
            sb.AppendFormat("<th style=\"width: 10%;\">{0}<span class=\"queryButton\">展开查询</span>{1}</th>", "", GetQueryControlDisplayStatusString());
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
                sb.AppendFormat("<td >{0}</td>", GetLeftBracketsString(i));
                sb.AppendFormat("<td >{0}</td>", GetConditionNameString(currentItem, i));
                sb.AppendFormat("<td >{0}</td>", GetConditionOperatorString(currentItem, i));
                sb.AppendFormat("<td >{0}</td>", GetConditionValueString(currentItem, i));
                sb.AppendFormat("<td >{0}</td>", GetRightBracketsString(i));
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
            string queryControlDisplayStatusFullName = this.name + QueryControlHelper.QueryControlDisplayStatus;
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
                                $(this).text('收起查询');
                            },
                            function () { 
                                var trObject = $(this).parents('tr');                                
                                trObject.siblings().hide(); 
                                trObject.find('.queryStatus').val('closed');
                                $(this).text('展开查询');
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
            string leftBracketsFullName = this.name + QueryControlHelper.LeftBracketsName + number;
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
            string rightBracketsFullName = this.name + QueryControlHelper.RightBracketsName + number;
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
            return string.Format("{0}<input type=\"hidden\" name=\"{1}\" value=\"{2}\" />", queryConditionItem.ConditionDisplayName, this.name + QueryControlHelper.ConditionFieldName + number, queryConditionItem.ConditionFieldName);
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
            result.AppendFormat("<input type=\"hidden\" name=\"{0}\" value=\"{1}\">", this.name + QueryControlHelper.ConditionTypeName + number, TypeHelper.GetTypeShortDescription(queryConditionItem.ConditionType));

            string conditionValueFullName = this.name + QueryControlHelper.ConditionValueName + number;
            string conditionValueValue = MVCHelper.GetParam(conditionValueFullName);

            //TODO:xieran20121001 日期类型需要使用日期输入框
            if (queryConditionItem.ConditionType == typeof(String) ||
                (TypeHelper.ConfirmIsNumberType(queryConditionItem.ConditionType) == true && queryConditionItem.ConditionType.IsEnum == false) ||
                queryConditionItem.ConditionType == typeof(DateTime))
            {
                result.AppendFormat("<input type=\"text\" name=\"{0}\" value=\"{1}\" />", conditionValueFullName, conditionValueValue);
            }

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

            string conditionOperatorFullName = this.name + QueryControlHelper.ConditionOperatorName + number;
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
                result.Append(GetCompareModeOptionString(CompareModes.Equal, conditionOperatorValue));
                result.Append(GetCompareModeOptionString(CompareModes.Like, conditionOperatorValue));
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
                result.Append(GetCompareModeOptionString(CompareModes.Equal, conditionOperatorValue));
                result.Append(GetCompareModeOptionString(CompareModes.NotEqual, conditionOperatorValue));
                result.Append(GetCompareModeOptionString(CompareModes.LessThan, conditionOperatorValue));
                result.Append(GetCompareModeOptionString(CompareModes.MoreThan, conditionOperatorValue));
                result.Append(GetCompareModeOptionString(CompareModes.NotLessThan, conditionOperatorValue));
                result.Append(GetCompareModeOptionString(CompareModes.NotMoreThan, conditionOperatorValue));
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
        private string GetCompareModeOptionString(CompareModes compareMode, string selectedValue)
        {
            string itemSelectedString = string.Empty;
            string text = EnumHelper.GetDisplayValue(compareMode);
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
                result.AppendFormat("<input type=\"hidden\" name=\"{0}\" value=\"{1}\" />", this.name + QueryControlHelper.ConditionCountName, number + 1);
            }
            else
            {
                string conditionRelationshipFullName = this.name + QueryControlHelper.ConditionRelationshipName + number;
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
    /// </summary>
    public class QueryConditionItem
    {
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
        /// 查询条件项对应的数据库字段名称
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
        public Type ConditionType { get; set; }

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
