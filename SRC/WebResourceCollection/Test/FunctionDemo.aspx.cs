using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HiLand.General.BLL;
using HiLand.General.Entity;
using HiLand.Utility.Enums;

namespace WebResourceCollection.Test
{
    public partial class FunctionDemo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int rowCount = BankBLL.Instance.GetTotalCount(string.Empty);
            this.Button1.Text = rowCount.ToString();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            int rowCount = LoanBasicBLL.Instance.GetCountTest();
            this.Button2.Text = rowCount.ToString();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            BankEntity entity = new BankEntity();
            entity.AccountName = "AccountName";
            entity.AccountNumber = "AccountNumber";
            entity.AccountStatus = 1;
            entity.AreaCode = "sssssssssss";
            entity.BankAddress = "BankAddress";
            entity.BankCode = "BankCode";
            entity.BankGuid = Guid.NewGuid();
            entity.BankName = "BankName";
            entity.BankNo = 5;
            entity.Branch = "Branch";
            entity.CanUsable = Logics.True;
            entity.Email = "Email"; ;
            entity.Fax = "Fax";
            entity.IsPrimary = Logics.True;
            entity.Telephone = "Telephone";
            entity.UserGuid = Guid.NewGuid();

            BankBLL.Instance.Create(entity);
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            BankEntity entity = BankBLL.Instance.Get("fef474cd-6a62-4be9-af62-0418c6a5146b");
            if (entity != null)
            {
                entity.AccountName = "AccountName";
                entity.AccountNumber = "AccountNumber";
                entity.AccountStatus = 1;
                entity.AreaCode = "sssssssssss";
                entity.BankAddress = "BankAddress";
                entity.BankCode = "BankCode";
                entity.BankName = "BankName";
                entity.BankNo = 5;
                entity.Branch = "Branch";
                entity.CanUsable = Logics.True;
                entity.Email = "Email"; ;
                entity.Fax = "Fax";
                entity.IsPrimary = Logics.True;
                entity.Telephone = "Telephone";
                entity.UserGuid = Guid.NewGuid();

                BankBLL.Instance.Update(entity);
            }
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
           this.Button5.Text= BankBLL.Instance.Delete("01432be4-e609-420f-856b-541148abd60c").ToString();
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            BankEntity entity = BankBLL.Instance.Get("fef474cd-6a62-4be9-af62-0418c6a5146c");
            if (entity != null)
            {
                this.Button6.Text = "Yes";
            }
            else
            {
                this.Button6.Text = "No";
            }
        }

        protected void Button8_Click(object sender, EventArgs e)
        {
            this.Button8.Text = BankBLL.Instance.GetTotalCount("").ToString();
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            this.Button7.Text= BankBLL.Instance.GetList(" BankID>60 ").Count.ToString();
        }

        protected void Button9_Click(object sender, EventArgs e)
        {
            this.Button9.Text = BankBLL.Instance.GetPagedCollection(50,5,"","").TotalCount.ToString();
        }
    }
}