//using System.Web.Mvc;

//namespace HiLand.Utility4.MVC.Controls
//{
//    /// <summary>
//    /// 隐藏域控件
//    /// </summary>
//    public class HiddenControl : InputControl<HiddenControl>
//    {
//        /// <summary>
//        /// 绘制控件核心部分的Html代码
//        /// </summary>
//        /// <returns></returns>
//        protected override string WriteCoreHtml()
//        {
//            string result = string.Empty;
//            TagBuilder tagInput = CreateTag("input");
//            tagInput.Attributes["type"] = "hidden";

//            result = tagInput.ToString();
//            return result;
//        }
//    }
//}
