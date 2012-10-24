using System.ComponentModel;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HiLand.Utility.Enums;

namespace HiLand.Utility.UI
{
    /// <summary>
    /// 验证码控件
    /// </summary>
    /// <remarks>
    /// 本验证控件要跟AuthCodeHandler一起使用，关于AuthCodeHandler在web.config中的配置，请使用如下格式：
    ///     <httpHandlers>
    ///         <add path="*.authcode" verb="*" type="HiLand.Utility.Handler.AuthCodeHandler,HiLand.Utility"/>
    ///     </httpHandlers>
    /// </remarks>
    [ToolboxData("<{0}:AuthCode runat=\"server\"></{0}:AuthCode>")]
    public class AuthCode : WebControl
    {
        /// <summary>
        /// 获得验证码的值
        /// </summary>
        /// <returns>验证码</returns>
        public string GetAuthCodeValue()
        {
            if (HttpContext.Current.Session["HiLand.AuthCodeValue"] != null)
            {
                return HttpContext.Current.Session["HiLand.AuthCodeValue"].ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 获得验证码的值
        /// </summary>
        public string AuthCodeValue
        {
            get { return GetAuthCodeValue(); }
        }

        internal static int charCount=4;

        [Bindable(true)]
        [Category("Appearance")]
        [Description("验证码字符长度")]
        [DefaultValue("4")]
        [Localizable(true)]
        public int CharCount
        {
            get { return AuthCode.charCount; }
            set { AuthCode.charCount = value; }
        }

        internal static CharCategories authCodeCharStyle = CharCategories.NumberAndChar;

        [Bindable(true)]
        [Category("Appearance")]
        [Description("验证码显示字符的样式")]
        [Localizable(true)]
        public CharCategories AuthCodeCharStyle
        {
            get { return AuthCode.authCodeCharStyle; }
            set { AuthCode.authCodeCharStyle = value; }
        }

        internal static Color foreColor = Color.Black;

        [Bindable(true)]
        [Category("Appearance")]
        [Description("验证码显示的前景颜色")]
        [Localizable(true)]
        public override Color ForeColor
        {
            get { return AuthCode.foreColor; }
            set { AuthCode.foreColor = value; }
        }

        internal static Color backColor = Color.Yellow;

        [Bindable(true)]
        [Category("Appearance")]
        [Description("验证码显示的背景颜色")]
        [Localizable(true)]
        public override Color BackColor
        {
            get { return AuthCode.backColor; }
            set { AuthCode.backColor = value; }
        }


        public AuthCode()
            : base(HtmlTextWriterTag.Img) //重写父类的构造（输出流的HTML标记）
        {

        }


        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);//将要输出的的HTML标签的属性和样式添加到指定的 HtmlTextWriter中
            writer.AddStyleAttribute(HtmlTextWriterStyle.Cursor, "pointer");//添加样式

            /**-
             * 图片的onclick事件 "this.src='VerifyImg.jd?id='+Math.random()"
             * 每次单击一次就有一个新的图片请求路径（VerifyImg.jd?id='+Math.random()）参数只是
             * 告诉浏览器这是一个新的请求然后经过 IHttpHander处理生成新的图片 id 没有任何实际意思（创造一个新的请求）
             * -**/
            writer.AddAttribute("onclick", "this.src='img.authcode?id='+Math.random()");//添加js VerifyImg.jd
            writer.AddAttribute(HtmlTextWriterAttribute.Src, "img.authcode");
            writer.AddAttribute("alt", "Click to refresh.");
        }

    }
}
