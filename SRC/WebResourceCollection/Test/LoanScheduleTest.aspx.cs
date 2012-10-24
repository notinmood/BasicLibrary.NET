using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HiLand.Utility.Finance;

namespace WebResourceCollection.Test
{
    public partial class LoanScheduleTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        double rate = 0.04;
        int paymentCount = 12;
        double totalAmount = 10000;
        PaymentTermTypes paymentTermType = PaymentTermTypes.Monthly;
        DateTime loanStartDate = new DateTime(2011,12,10);

        protected void Button1_Click(object sender, EventArgs e)
        {
            this.Literal1.Text = string.Empty;
            List<Payment> paymentList = CPMLoan.GetPaymentSchedule(rate, totalAmount, paymentCount, paymentTermType, loanStartDate);
            foreach (Payment currentPayment in paymentList)
            {
                string displayInfoForPayment = string.Format("Amount:{0}-Principal:{1}-Interest:{2}", currentPayment.Amount, currentPayment.Principal, currentPayment.Interest);
                this.Literal1.Text += displayInfoForPayment+"<br/>";
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            this.Literal1.Text = string.Empty;
            List<Payment> paymentList = CAMLoan.GetPaymentSchedule(rate, totalAmount, paymentCount, paymentTermType, loanStartDate);
            foreach (Payment currentPayment in paymentList)
            {
                string displayInfoForPayment = string.Format("Amount:{0}-Principal:{1}-Interest:{2}", currentPayment.Amount, currentPayment.Principal, currentPayment.Interest);
                this.Literal1.Text += displayInfoForPayment + "<br/>";
            }
        }
    }
}