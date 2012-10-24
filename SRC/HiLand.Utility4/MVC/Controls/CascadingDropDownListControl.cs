using System.Text;
using System.Web.UI;

namespace HiLand.Utility4.MVC.Controls
{
    /// <summary>
    /// 级联更新的列表控件
    /// </summary>
    /// <remarks>
    /// 必须在页面或者本控件的JavaScriptFiles方法中指定jquery.cascadingDropDown.js
    /// </remarks>
    public class CascadingDropDownListControl : DropDownListControl<CascadingDropDownListControl>
    {
        private string parentSelectControlSelector = string.Empty;
        /// <summary>
        /// 父下拉列表空间的选择器
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public CascadingDropDownListControl ParentSelectControlSelector(string data)
        {
            this.parentSelectControlSelector = data;
            return this;
        }

        private string dynamicSelectItemsLoadUrl = string.Empty;
        /// <summary>
        /// 当前下拉列表控件选择性的动态获取地址（这个地址返回的值必须是CascadingCollection（HiLand.Utility\Entity\CascadingCollection.cs）转成JSON的字符串）
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public CascadingDropDownListControl DynamicSelectItemsLoadUrl(string data)
        {
            this.dynamicSelectItemsLoadUrl = data;
            return this;
        }

        /// <summary>
        /// 绘制javascript脚本内容
        /// </summary>
        /// <param name="writer"></param>
        protected override void WriteJavascriptContent(HtmlTextWriter writer)
        {
            StringBuilder sb= new StringBuilder();
            sb.AppendFormat(@"<br/><script type='text/javascript'>
                    $(document).ready(function () |<|
                        $('#{0}').CascadingDropDown('{1}', '{2}');
                   |>|);
                </script>
            ", this.ID, this.parentSelectControlSelector, dynamicSelectItemsLoadUrl);

            sb.Replace("|<|", "{");
            sb.Replace("|>|", "}");

            writer.Write(sb.ToString());
        }
    }
}
