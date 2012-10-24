using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HiLand.General.Entity;
using HiLand.Utility.Data;
using HiLand.General.BLL;

namespace WebResourceCollection.Test
{
    public partial class AttributeTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            this.Button1.Text = "";
            LoanBasicEntity entity = new LoanBasicEntity();
            entity.LoanID = 8;
            entity.LoanGuid = GuidHelper.NewGuid();
            string[] keyNames = entity.BusinessKeyNames;
            string[] keyValues = entity.BusinessKeyValues;

            if (keyNames.Length == keyValues.Length)
            {
                for (int i = 0; i < keyNames.Length; i++)
                {
                    this.Button1.Text += string.Format("{0}:{1};", keyNames[i], keyValues[i]);
                }
            }
        }

        private Guid myGuid = new Guid("93463A25-38E5-4589-B900-808F3E7A6905");
        protected void Button2_Click(object sender, EventArgs e)
        {
            LoanBasicEntity entity = new LoanBasicEntity();
            entity.LoanGuid = myGuid;//GuidHelper.NewGuid();
            entity.CheckDate = DateTime.Now;
            entity.LoanPurpose = "sssssssssssss";
            LoanBasicBLL.Instance.Create(entity);
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            LoanBasicEntity entity = LoanBasicBLL.Instance.Get(myGuid);
        }
    }
}