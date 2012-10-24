using System.Web.Mvc;
using HiLand.Utility4.MVC;

namespace HiLand.Framework4.Permission.Controls
{
    /// <summary>
    /// 提交控件
    /// </summary>
    /// <remarks>
    /// 由于提交的时候都是对本页面对应的form进行提交，
    /// 所以本控件内设置了可以自动获取ACA的功能，其几个值可以不用手工设置
    /// </remarks>
    public class SubmitControl : OperateControl<SubmitControl>
    {
        protected override string area
        {
            get
            {
                return MVCHelper.GetCurrentAreaName();
            }
        }
        protected override string controller
        {
            get
            {
                return MVCHelper.GetCurrentControllerName();
            }
        }

        protected override string action
        {
            get
            {
                return MVCHelper.GetCurrentActionName();
            }
        }

        protected override void WriteHtml(System.Web.UI.HtmlTextWriter writer)
        {
            string result = string.Empty;
            TagBuilder tagInput = CreateTag("input");
            tagInput.Attributes["type"] = "submit";
            if (HasPermission == false)
            {
                tagInput.Attributes["disabled"] = "disabled";
            }

            tagInput.Attributes["value"] = this.value;

            result = tagInput.ToString();
            writer.Write(result);
        }
    }
}
