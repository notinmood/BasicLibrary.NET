//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Text;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;

//namespace Hiland.BasicLibrary.UI
//{
//    /// <summary>
//    /// 这个通用搜索功能需要继续完成，目前项目使用Web项目中的UCommonSearch控件
//    /// </summary>

//    [DefaultProperty("Text")]
//    [ToolboxData("<{0}:CCommonSearch runat=server></{0}:CCommonSearch>")]
//    public class CCommonSearch : WebControl
//    {
//        [Bindable(true)]
//        [Category("Appearance")]
//        [DefaultValue("")]
//        [Localizable(true)]
//        public string Text
//        {
//            get
//            {
//                String s = (String)ViewState["Text"];
//                return ((s == null) ? String.Empty : s);
//            }

//            set
//            {
//                ViewState["Text"] = value;
//            }
//        }

//        protected override void RenderContents(HtmlTextWriter output)
//        {
//            output.Write(Text);
//        }
//    }
//}
