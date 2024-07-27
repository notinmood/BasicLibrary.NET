//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Web.Mvc;
//using System.Web.Mvc.Html;
//using System.Web.Routing;

//namespace Hiland.BasicLibrary4.MVC
//{
//    /// <summary>
//    /// HtmlHelper扩展类
//    /// </summary>
//    public static class HtmlHelperEx
//    {
//        public static MvcHtmlString Link(this HtmlHelper htmlHelper, string linkText, string linkHref, object htmlAttributes)
//        {
//            string resultString = string.Empty;
//            var builder = new TagBuilder("a");
//            builder.Attributes["href"] = linkHref;
//            builder.InnerHtml = linkText;
//            builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));

//            resultString = builder.ToString();
//            return MvcHtmlString.Create(resultString);
//        }

//        /// <summary>
//        /// 带级别格式的下拉列表
//        /// </summary>
//        /// <param name="htmlHelper"></param>
//        /// <param name="name"></param>
//        /// <param name="itemList"></param>
//        /// <param name="optionLabel"></param>
//        /// <param name="codeLenghtPerLevel"></param>
//        /// <param name="textPrefix">出现在子级别前面的字符，用以跟父级别区分</param>
//        /// <returns>
//        /// 这不是一个通用的下拉列表，满足一下条件才能使用
//        /// 1.列表项的Value必须满足海澜科技Code的编码规则（Code规则另见）
//        /// 2.Code中每几位表示一个级别，需要通过参数codeLenghtPerLevel指定（通常是2位表示一个级别）
//        /// </returns>
//        public static MvcHtmlString DropDownListWithHierachical(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> itemList, string optionLabel, int codeLenghtPerLevel = 2, string textPrefix = "-")
//        {
//            return DropDownListWithHierachical(htmlHelper, name, itemList, optionLabel, null, codeLenghtPerLevel, textPrefix);
//        }

//        /// <summary>
//        /// 带级别格式的下拉列表
//        /// </summary>
//        /// <param name="htmlHelper"></param>
//        /// <param name="name"></param>
//        /// <param name="itemList"></param>
//        /// <param name="htmlAttributes"></param>
//        /// <param name="codeLenghtPerLevel"></param>
//        /// <param name="textPrefix">出现在子级别前面的字符，用以跟父级别区分</param>
//        /// <returns>
//        /// 这不是一个通用的下拉列表，满足一下条件才能使用
//        /// 1.列表项的Value必须满足海澜科技Code的编码规则（Code规则另见）
//        /// 2.Code中每几位表示一个级别，需要通过参数codeLenghtPerLevel指定（通常是2位表示一个级别）
//        /// </returns>
//        public static MvcHtmlString DropDownListWithHierachical(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> itemList, object htmlAttributes, int codeLenghtPerLevel = 2, string textPrefix = "-")
//        {
//            return DropDownListWithHierachical(htmlHelper, name, itemList, null, htmlAttributes, codeLenghtPerLevel, textPrefix);
//        }

//        /// <summary>
//        /// 带级别格式的下拉列表
//        /// </summary>
//        /// <param name="htmlHelper"></param>
//        /// <param name="name"></param>
//        /// <param name="itemList"></param>
//        /// <param name="htmlAttributes"></param>
//        /// <param name="codeLenghtPerLevel"></param>
//        /// <param name="textPrefix">出现在子级别前面的字符，用以跟父级别区分</param>
//        /// <returns>
//        /// 这不是一个通用的下拉列表，满足一下条件才能使用
//        /// 1.列表项的Value必须满足海澜科技Code的编码规则（Code规则另见）
//        /// 2.Code中每几位表示一个级别，需要通过参数codeLenghtPerLevel指定（通常是2位表示一个级别）
//        /// </returns>
//        public static MvcHtmlString DropDownListWithHierachical(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> itemList, string optionLabel, object htmlAttributes, int codeLenghtPerLevel = 2, string textPrefix = "-")
//        {
//            IOrderedEnumerable<SelectListItem> orderedItemList = itemList.OrderBy(selectListItem => selectListItem.Value);
//            List<SelectListItem> newList = new List<SelectListItem>();
//            foreach (var v in orderedItemList)
//            {
//                int level = v.Value.Length / codeLenghtPerLevel - 1;
//                string prefixInfo = string.Empty;
//                for (int i = 0; i < level; i++)
//                {
//                    prefixInfo += textPrefix;
//                }
//                string newText = prefixInfo + v.Text;
//                SelectListItem newItem = new SelectListItem();
//                newItem.Text = newText;
//                newItem.Value = v.Value;
//                newItem.Selected = v.Selected;
//                newList.Add(newItem);
//            }

//            return htmlHelper.DropDownList(name, newList, optionLabel, htmlAttributes);
//        }

//        /// <summary>
//        /// 英文26字母的下拉列表
//        /// </summary>
//        /// <param name="htmlHelper"></param>
//        /// <param name="name"></param>
//        /// <param name="htmlAttributes"></param>
//        /// <param name="isUpper">是否使用大写字母（确省为true）</param>
//        /// <returns></returns>
//        public static MvcHtmlString DropDownListAllEnglishChars(this HtmlHelper htmlHelper, string name, object htmlAttributes, bool isUpper = true)
//        {
//            List<SelectListItem> list = new List<SelectListItem>();
//            if (isUpper == true)
//            {
//                for (int i = 65; i <= 90; i++)
//                {
//                    SelectListItem currentItem = GenerateSelectListItem(i);
//                    list.Add(currentItem);
//                }
//            }
//            else
//            {
//                for (int i = 97; i <= 122; i++)
//                {
//                    SelectListItem currentItem = GenerateSelectListItem(i);
//                    list.Add(currentItem);
//                }
//            }

//            return htmlHelper.DropDownList(name, list, htmlAttributes);
//        }

//        /// <summary>
//        /// 通过将数字转换成相应的字符来创建DropDownList的选择项
//        /// </summary>
//        /// <param name="i"></param>
//        /// <returns></returns>
//        private static SelectListItem GenerateSelectListItem(int i)
//        {
//            string currentChar = Convert.ToChar(i).ToString();
//            SelectListItem currentItem = new SelectListItem();
//            currentItem.Text = currentChar;
//            currentItem.Value = currentChar;
//            return currentItem;
//        }

//        /// <summary>
//        /// Lamda版Html.RenderAction
//        /// </summary>
//        /// <typeparam name="TController"></typeparam>
//        /// <param name="htmlHelper"></param>
//        /// <param name="operation"></param>
//        /// <remarks>
//        /// 在MVC3下Html.RenderAction("RecentNews")，调用Action的时候需要通过其名称调用，但是这样不利于代码重构，
//        /// 因此借鉴MVC4的做法，使用强类型的方式：Html.RenderAction<AggSiteController>(c => c.PostList(1,20));
//        /// （MVC4下直接有这种类型的重载，不需要使用此方法）
//        /// </remarks>
//        public static void RenderAction<TController>(this HtmlHelper htmlHelper, Expression<Action<TController>> operation)
//           where TController : Controller
//        {
//            var controllerName = typeof(TController).Name;
//            if (controllerName.EndsWith("Controller"))
//            {
//                controllerName = controllerName.Substring(0, controllerName.Length - "Controller".Length);
//            }

//            var call = operation.Body as MethodCallExpression;
//            if (call != null)
//            {
//                var actionName = call.Method.Name;
//                var parameters = call.Method.GetParameters();
//                if (parameters.Length > 0)
//                {
//                    var routeValues = new RouteValueDictionary();
//                    for (int i = 0; i < parameters.Length; i++)
//                    {
//                        var ce = call.Arguments[i] as ConstantExpression;
//                        if (ce != null)
//                        {
//                            routeValues.Add(parameters[i].Name, ce.Value);
//                        }
//                        else
//                        {
//                            var lambda = Expression.Lambda(call.Arguments[i], operation.Parameters);
//                            Delegate d = lambda.Compile();
//                            var value = d.DynamicInvoke(new object[1]);
//                            routeValues.Add(parameters[i].Name, value);
//                        }
//                    }
//                    htmlHelper.RenderAction(actionName, controllerName, routeValues);
//                }
//                else
//                {
//                    htmlHelper.RenderAction(actionName, controllerName);
//                }
//            }
//        }
//    }
//}
