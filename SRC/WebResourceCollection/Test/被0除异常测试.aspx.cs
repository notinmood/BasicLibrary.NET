using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HiLand.Utility.Data;

namespace WebResourceCollection.Test
{
    public partial class 被0除异常 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                被0除异常测试Class.Foo();
            }
            catch (Exception ex)
            {
                this.Literal1.Text = ExceptionHelper.GetExceptionMessage(ex);
            }
            
        }
    }
}