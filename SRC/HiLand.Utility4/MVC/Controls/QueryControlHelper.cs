using System;
using System.Text;
using HiLand.Utility.Data;
using HiLand.Utility.Enums;
using HiLand.Utility.Web;

namespace HiLand.Utility4.MVC.Controls
{
    /// <summary>
    /// 查询控件辅助工具
    /// </summary>
    /// <remarks>
    /// 获取QueryControl提交的结果，请使用本辅助工具内的方法GetQueryCondition，其会返回查询条件
    /// </remarks>
    public static class QueryControlHelper
    {
        /// <summary>
        /// 获取附加了查询字符串的新url
        /// </summary>
        /// <param name="queryControlName">查询控件的名称</param>
        /// <param name="baseUrl"></param>
        /// <returns></returns>
        public static string GetNewQueryUrl(string queryControlName, string baseUrl = StringHelper.Empty)
        {
            if (string.IsNullOrWhiteSpace(baseUrl))
            {
                baseUrl = RequestHelper.CurrentFullUrl;
            }

            UrlInfo urlInfo = UrlInfo.New(baseUrl);

            string queryControlDisplayStatusFullName = queryControlName + QueryControlDisplayStatus;
            string queryControlDisplayStatusValue = RequestHelper.GetValue(PassingParamValueSourceTypes.Form, queryControlDisplayStatusFullName, "closed");
            urlInfo.Concat(queryControlDisplayStatusFullName, queryControlDisplayStatusValue);

            string queryCountFullName = queryControlName + ConditionCountName;
            int queryConditionCount = RequestHelper.GetValue(PassingParamValueSourceTypes.Form, queryCountFullName, 0);
            if (queryConditionCount > 0)
            {
                urlInfo.Concat(queryCountFullName, queryConditionCount.ToString());

                for (int i = 0; i < queryConditionCount; i++)
                {
                    string leftBracketsFullName = queryControlName + LeftBracketsName + i;
                    string leftBracketsValue = RequestHelper.GetValue(PassingParamValueSourceTypes.Form, leftBracketsFullName, string.Empty).ToLower();
                    if (leftBracketsValue == "on")
                    {
                        urlInfo.Concat(leftBracketsFullName, leftBracketsValue);
                    }

                    string conditionFieldFullName = queryControlName + ConditionFieldName + i;
                    string conditionFieldValue = RequestHelper.GetValue(PassingParamValueSourceTypes.Form, conditionFieldFullName, string.Empty);
                    urlInfo.Concat(conditionFieldFullName, conditionFieldValue);

                    string conditionTypeFullName = queryControlName + ConditionTypeName + i;
                    string conditionTypeValue = RequestHelper.GetValue(PassingParamValueSourceTypes.Form, conditionTypeFullName, string.Empty);
                    urlInfo.Concat(conditionTypeFullName, conditionTypeValue);

                    string conditionValueFullName = queryControlName + ConditionValueName + i;
                    string conditionValueValue = RequestHelper.GetValue(PassingParamValueSourceTypes.Form, conditionValueFullName, string.Empty);
                    urlInfo.Concat(conditionValueFullName, conditionValueValue);

                    string conditionOperatorFullName = queryControlName + ConditionOperatorName + i;
                    string conditionOperatorValue = RequestHelper.GetValue(PassingParamValueSourceTypes.Form, conditionOperatorFullName, string.Empty);
                    urlInfo.Concat(conditionOperatorFullName, conditionOperatorValue);

                    string rightBracketsFullName = queryControlName + RightBracketsName + i;
                    string rightBracketsValue = RequestHelper.GetValue(PassingParamValueSourceTypes.Form, rightBracketsFullName, string.Empty).ToLower();
                    if (rightBracketsValue == "on")
                    {
                        urlInfo.Concat(rightBracketsFullName, rightBracketsValue);
                    }

                    string conditionRelationshipFullName = queryControlName + ConditionRelationshipName + i;
                    string conditionRelationshipValue = RequestHelper.GetValue(PassingParamValueSourceTypes.Form, conditionRelationshipFullName, string.Empty);
                    if (string.IsNullOrEmpty(conditionRelationshipValue) == false)
                    {
                        urlInfo.Concat(conditionRelationshipFullName, conditionRelationshipValue);
                    }
                }
            }

            return urlInfo.ToString();
        }

        /// <summary>
        /// 获取数据库查询获取查询条件字符串（Where子句部分）
        /// </summary>
        /// <param name="queryControlName">查询控件的名称</param>
        /// <returns></returns>
        public static string GetQueryCondition(string queryControlName)
        {
            StringBuilder sb = new StringBuilder(" 1=1 AND ");

            string queryControlDisplayStatusFullName = queryControlName + QueryControlDisplayStatus;
            string queryControlDisplayStatusValue = RequestHelper.GetValue(queryControlDisplayStatusFullName, "closed");
            MVCHelper.SetCustomData(queryControlDisplayStatusFullName, queryControlDisplayStatusValue);

            int queryConditionCount = RequestHelper.GetValue(queryControlName + ConditionCountName, 0);
            if (queryConditionCount > 0)
            {
                for (int i = 0; i < queryConditionCount; i++)
                {
                    string leftBracketsFullName = queryControlName + LeftBracketsName + i;
                    string leftBracketsValue = RequestHelper.GetValue(leftBracketsFullName, string.Empty).ToLower();
                    if (leftBracketsValue == "on")
                    {
                        sb.AppendFormat(" ( ");
                        MVCHelper.SetCustomData(leftBracketsFullName, leftBracketsValue);
                    }

                    string conditionFieldValue = RequestHelper.GetValue(queryControlName + ConditionFieldName + i, string.Empty);
                    string conditionTypeValue = RequestHelper.GetValue(queryControlName + ConditionTypeName + i, string.Empty);

                    string conditionValueFullName = queryControlName + ConditionValueName + i;
                    string conditionValueValue = RequestHelper.GetValue(conditionValueFullName, string.Empty);
                    MVCHelper.SetCustomData(conditionValueFullName, conditionValueValue);

                    string conditionOperatorFullName = queryControlName + ConditionOperatorName + i;
                    string conditionOperatorValue = RequestHelper.GetValue(conditionOperatorFullName, string.Empty);
                    MVCHelper.SetCustomData(conditionOperatorFullName, conditionOperatorValue);

                    if (string.IsNullOrEmpty(conditionValueValue) == true)
                    {
                        sb.Append(" 1=1 ");
                    }
                    else
                    {
                        Type conditionType = Type.GetType(conditionTypeValue);
                        if (conditionType == typeof(String) ||
                            conditionType == typeof(DateTime) ||
                            conditionType == typeof(Guid)
                            )
                        {
                            switch (conditionOperatorValue.ToLower())
                            {
                                case "likeleft":
                                    sb.AppendFormat(" [{0}] like '{1}%' ", conditionFieldValue, conditionValueValue);
                                    break;
                                case "likeright":
                                    sb.AppendFormat(" [{0}] like '%{1}' ", conditionFieldValue, conditionValueValue);
                                    break;
                                case "like":
                                    sb.AppendFormat(" [{0}] like '%{1}%' ", conditionFieldValue, conditionValueValue);
                                    break;
                                default:
                                    sb.AppendFormat(" [{0}] {1} '{2}' ", conditionFieldValue, conditionOperatorValue, conditionValueValue);
                                    break;
                            }
                        }
                        else
                        {
                            if (conditionType.IsEnum == true)
                            {
                                int enumValue = (int)Enum.Parse(conditionType, conditionValueValue);
                                sb.AppendFormat(" [{0}] {1} {2} ", conditionFieldValue, conditionOperatorValue, enumValue);
                            }
                            else
                            {
                                sb.AppendFormat(" [{0}] {1} {2} ", conditionFieldValue, conditionOperatorValue, conditionValueValue);
                            }
                        }
                    }

                    string rightBracketsFullName = queryControlName + RightBracketsName + i;
                    string rightBracketsValue = RequestHelper.GetValue(rightBracketsFullName, string.Empty).ToLower();
                    if (rightBracketsValue == "on")
                    {
                        sb.AppendFormat(" ) ");
                        MVCHelper.SetCustomData(rightBracketsFullName, rightBracketsValue);
                    }

                    string conditionRelationshipFullName = queryControlName + ConditionRelationshipName + i;
                    string conditionRelationshipValue = RequestHelper.GetValue(conditionRelationshipFullName, string.Empty);
                    if (string.IsNullOrEmpty(conditionRelationshipValue) == false)
                    {
                        sb.AppendFormat(" {0} ", conditionRelationshipValue);
                        MVCHelper.SetCustomData(conditionRelationshipFullName, conditionRelationshipValue);
                    }
                }
            }
            else
            {
                sb.Append(" 1=1 ");
            }

            return sb.ToString();
        }



        #region 文本常量定义
        public static string ConditionCountName = "||ConditionCountName";
        public static string QueryControlDisplayStatus = "||QueryControlDisplayStatus";

        public static string LeftBracketsName = "||LeftBracketsName||";
        public static string RightBracketsName = "||RightBracketsName||";

        public static string ConditionValueName = "||ConditionValueName||";
        public static string ConditionFieldName = "||ConditionFieldName||";
        public static string ConditionTypeName = "||ConditionTypeName||";

        public static string ConditionOperatorName = "||ConditionOperatorName||";
        public static string ConditionRelationshipName = "||ConditionRelationshipName||";
        #endregion
    }
}
