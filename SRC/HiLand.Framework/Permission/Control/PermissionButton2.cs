//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Web.UI.WebControls;
//using HiLand.Utility.Enums;
//using System.Web.UI;

//namespace HiLand.Utility.UI
//{
//    public class PermissionButton2 : WebControl
//    {
//        /// <summary>
//        /// 按钮的显示类型
//        /// </summary>
//        public PermissionButtonTypes PermissionButtonType
//        {
//            get
//            {
//                if (this.ViewState["Control|PermissionButtonType"] != null)
//                {
//                    return (PermissionButtonTypes)this.ViewState["Control|PermissionButtonType"];
//                }
//                else
//                {
//                    return PermissionButtonTypes.Button;
//                }
//            }
//            set
//            {
//                this.ViewState["Control|PermissionButtonType"] = value;
//            }
//        }


//        //protected override System.Web.UI.HtmlTextWriterTag TagKey
//        //{
//        //    get
//        //    {
//        //        switch (this.PermissionButtonType)
//        //        {

//        //            case PermissionButtonTypes.HyperLink:
//        //            case PermissionButtonTypes.LinkButton:
//        //                return HtmlTextWriterTag.A;
//        //            case PermissionButtonTypes.Button:
//        //            default:
//        //                return HtmlTextWriterTag.Input;
//        //        }

//        //    }
//        //}

//        private PermissionTypes permissionType = PermissionTypes.ALL;
//        public PermissionTypes PermissionType
//        {
//            get { return this.permissionType; }
//            set { this.permissionType = value; }
//        }

//        public string Text
//        {
//            get
//            {
//                if (this.ViewState["Control|Text"] != null)
//                {
//                    return (string)this.ViewState["Control|Text"];
//                }
//                else
//                {
//                    return this.permissionType.ToString();
//                }
//            }
//            set
//            {
//                this.ViewState["Control|Text"] = value;
//            }
//        }

//        protected override void RenderContents(System.Web.UI.HtmlTextWriter writer)
//        {
//            writer.AddAttribute(HtmlTextWriterAttribute.Type,"button");
//            if (this.CssClass != string.Empty)
//            {
//                writer.AddAttribute(HtmlTextWriterAttribute.Class, this.CssClass);
//            }
//            writer.AddAttribute(HtmlTextWriterAttribute.Value,this.Text);
//            writer.RenderBeginTag(HtmlTextWriterTag.Input);
//            writer.RenderEndTag();
//            //writer.AddStyleAttribute(HtmlTextWriterStyle.);
//            //writer.AddAttribute(HtmlTextWriterAttribute.Class,"");
//            writer.Write("qingdao");
//            base.RenderContents(writer);

//        }


//    }
//}
