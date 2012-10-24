using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI;
using HiLand.Utility4.MVC;

namespace HiLand.Framework4.Permission.Controls
{
    /// <summary>
    /// 超连接控件
    /// </summary>
    public class AControl : OperateControl<AControl>
    {
        private string _href = string.Empty;

        private string href
        {
            get
            {
                string result = string.Empty;
                if (string.IsNullOrWhiteSpace(_href))
                {
                    UrlHelper url = UrlHelperEx.UrlHelper;
                    if (string.IsNullOrWhiteSpace(this.action) == false)
                    {
                        result = url.Action(this.action, this.controller, routeValue);
                    }
                    else
                    {
                        result = "#";
                    }
                }
                else
                {
                    result = _href;
                }

                return result;
            }
        }

        /// <summary>
        /// 超连接的目标地址
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public AControl Href(string value)
        {
            this._href = value;
            return this;
        }

        private RouteValueDictionary _routeValue = new RouteValueDictionary();

        private RouteValueDictionary routeValue
        {
            get
            {
                if (string.IsNullOrWhiteSpace(area) == false)
                {
                    _routeValue["area"] = area;
                }
                return this._routeValue;
            }
        }

        /// <summary>
        /// 路由数据
        /// </summary>
        /// <param name="value">其结构通常为类似如下： new { area="Manage",id=6,password="ssss"}</param>
        /// <returns></returns>
        public AControl RouteValue(object value)
        {
            this._routeValue = new RouteValueDictionary(value);
            return this;
        }

        protected override void WriteHtml(HtmlTextWriter writer)
        {
            string result = string.Empty;
            TagBuilder tagInput = null;
            if (HasPermission == true)
            {
                tagInput= CreateTag("a");
                tagInput.Attributes["href"] = href;
            }
            else
            {
                tagInput = CreateTag("span");
                
                // Hack:xieran 由于在弹出窗体的实现中，js会判断某个元素是否会存在rel属性，
                // 而在没有权限的情况下不能弹出这个窗口，因此此次强制清除rel属性
                if (tagInput.Attributes.ContainsKey("rel"))
                {
                    tagInput.Attributes.Remove("rel");
                }
            }

            tagInput.InnerHtml = this.value;
            
            result = tagInput.ToString();
            writer.Write(result);
        }
    }
}
