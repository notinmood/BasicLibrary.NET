using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HiLand.Utility.Entity;

namespace WebApplicationConsole
{
    public partial class ResourceUsing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TestEnum();
        }


        private void TestEnum()
        {
            ValidateTypes vt = ValidateTypes.IsNumber | ValidateTypes.IsEmpty;
            Array array = Enum.GetValues(typeof(ValidateTypes));
            int length = array.Length;
            for (int i = 0; i < length; i++)
            {
                ValidateTypes j = (ValidateTypes)array.GetValue(i);

                bool isOk = false;
                if ((vt & j) == j)
                {
                    isOk = true;
                }
                else
                {
                    isOk = false;
                }

                if (isOk)
                { 
                    //
                }
            }

        }
    }
}