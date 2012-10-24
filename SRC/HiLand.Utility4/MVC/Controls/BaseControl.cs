using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI;
using HiLand.Utility.Data;

namespace HiLand.Utility4.MVC.Controls
{
    /// <summary>
    /// MVC控件基类
    /// </summary>
    /// <remarks>
    /// 其中MvcControlCssPrefix可以读取配置文件mvcControlCssPrefix
    /// </remarks>
    public abstract class BaseControl<T> where T : BaseControl<T>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseControl()
        {
            htmlAttributes = new RouteValueDictionary();
        }


        /// <summary>
        /// 名称
        /// </summary>
        protected string name { get; set; }
        /// <summary>
        /// 设置名称
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public T Name(string data)
        {
            this.name = data;
            return this as T;
        }

        /// <summary>
        /// 控件ID 【根据name 自动生成】
        /// </summary>
        protected string ID
        {
            get
            {
                return htmlAttributes.ContainsKey("id") ?
                       htmlAttributes["id"].ToString() :
                       (!string.IsNullOrEmpty(name) ? name.Replace(".", System.Web.Mvc.HtmlHelper.IdAttributeDotReplacement) : null);
            }
        }

        private string _cssClassName = string.Empty;
        /// <summary>
        /// CSS类的名称
        /// </summary>
        protected virtual string cssClassName
        {
            get { return this._cssClassName; }
        }

        /// <summary>
        /// 设置CSS类的名称
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public T CssClassName(string data)
        {
            this._cssClassName = data;
            return this as T;
        }

        /// <summary>
        /// Html属性字典集合
        /// </summary>
        protected IDictionary<string, object> htmlAttributes
        {
            get;
            private set;
        }

        /// <summary>
        /// 设置HTML属性
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public T HtmlAttributes(IDictionary<string, object> data)
        {
            AddOrUpdateAttributes(data);
            return this as T;
        }

        /// <summary>
        /// 添加或更新既有的HTML属性
        /// </summary>
        /// <param name="data"></param>
        private void AddOrUpdateAttributes(IDictionary<string, object> data)
        {
            this.htmlAttributes = CollectionHelper.Merger(htmlAttributes,data);
        }

        private string mvcControlCssPrefix = string.Empty;
        /// <summary>
        /// Css名称前缀
        /// </summary>
        protected string MvcControlCssPrefix
        {
            get
            {
                if (mvcControlCssPrefix == string.Empty)
                {
                    string tempData = ConfigurationManager.AppSettings["mvcControlCssPrefix"];
                    if (string.IsNullOrWhiteSpace(tempData) == false)
                    {
                        mvcControlCssPrefix = tempData;
                    }
                }
                return mvcControlCssPrefix;
            }
        }

        /// <summary>
        /// 控件的值
        /// </summary>
        protected string value = string.Empty;
        /// <summary>
        /// 设置控件的值
        /// </summary>
        public T Value(string data)
        {
            this.value = data;
            return this as T;
        }

        /// <summary>
        /// 设置控件的值
        /// </summary>
        public T Value(object data)
        {
            this.value = data.ToString();
            return this as T;
        }

        /// <summary>
        /// 以Html方式输出控件代码
        /// </summary>
        public IHtmlString Render(object htmlAttributes = null)
        {
            if (htmlAttributes != null)
            {
                RouteValueDictionary rvd = new RouteValueDictionary(htmlAttributes);
                if (rvd != null)
                {
                    AddOrUpdateAttributes(rvd);
                }
            }

            StringWriter sw = new StringWriter();
            using (HtmlTextWriter textWriter = new HtmlTextWriter(sw))
            {
                LoadStyleSheetFiles(textWriter);
                LoadJavaScriptFiles(textWriter);
                WriteHtml(textWriter);
                WriteJavascriptContent(textWriter);
            }
            return new MvcHtmlString(sw.ToString());
        }

        /// <summary>
        /// 输出控件Html代码
        /// </summary>
        /// <param name="writer"></param>
        protected abstract void WriteHtml(HtmlTextWriter writer);

        /// <summary>
        /// 需要载入的外部JavaScript文件
        /// </summary>
        protected List<string> javaScriptFiles = new List<string>();
        
        /// <summary>
        /// 设置需要载入的外部JavaScript文件
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public T JavaScriptFiles(List<string> value)
        {
            this.javaScriptFiles = value;
            return this as T;
        }

        /// <summary>
        /// 载入外部JavaScript文件
        /// </summary>
        /// <param name="writer"></param>
        protected virtual void LoadJavaScriptFiles(HtmlTextWriter writer)
        {
            string result = string.Empty;
            if (this.javaScriptFiles != null && this.javaScriptFiles.Count > 0)
            {
                foreach (string currentFile in this.javaScriptFiles)
                {
                    result += string.Format("<script src=\"{0}\" type=\"text/javascript\"></script>", currentFile);
                }
            }

            writer.Write(result);
        }

        /// <summary>
        /// 需要载入的外部CSS文件
        /// </summary>
        protected List<string> styleSheetFiles = new List<string>();
        
        /// <summary>
        /// 设置需要载入的外部CSS文件
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public T StyleSheetFiles(List<string> value)
        {
            this.styleSheetFiles = value;
            return this as T;
        }

        /// <summary>
        /// 载入的外部CSS文件
        /// </summary>
        /// <param name="writer"></param>
        protected virtual void LoadStyleSheetFiles(HtmlTextWriter writer)
        {
            string result = string.Empty;
            if (this.styleSheetFiles != null && this.styleSheetFiles.Count > 0)
            {
                foreach (string currentFile in this.styleSheetFiles)
                {
                    result += string.Format("<link href=\"{0}\" rel=\"stylesheet\" type=\"text/css\" />", currentFile);
                }
            }

            writer.Write(result);
        }

        /// <summary>
        /// 绘制javascript脚本内容
        /// </summary>
        /// <param name="writer"></param>
        protected virtual void WriteJavascriptContent(HtmlTextWriter writer)
        { 
            
        }

        /// <summary>
        /// 创建控件
        /// </summary>
        /// <param name="tagName">控件的标签名称</param>
        /// <param name="idInfo">控件的ID值</param>
        /// <param name="nameInfo">控件的name值</param>
        /// <returns>
        /// 各个参数如果为“”：使用系统默认的数据；如果为null：不呈现此信息
        /// </returns>
        protected virtual TagBuilder CreateTag(string tagName, string nameInfo = "", string idInfo = "")
        {
            TagBuilder tagBuilder = new TagBuilder(tagName);

            if (idInfo != null)
            {
                if (idInfo == string.Empty)
                {
                    tagBuilder.Attributes["id"] = ID;
                }
                else
                {
                    tagBuilder.Attributes["id"] = idInfo;
                }
            }

            if (nameInfo != null)
            {
                if (nameInfo == string.Empty)
                {
                    tagBuilder.Attributes["name"] = name;
                }
                else
                {
                    tagBuilder.Attributes["name"] = nameInfo;
                }
            }

            if (cssClassName != null)
            {
                if (cssClassName == string.Empty)
                {
                    tagBuilder.AddCssClass(MvcControlCssPrefix + tagName);
                }
                else
                {
                    tagBuilder.AddCssClass(MvcControlCssPrefix + cssClassName);
                }
            }

            tagBuilder.MergeAttributes(this.htmlAttributes);

            return tagBuilder;
        }
    }
}
