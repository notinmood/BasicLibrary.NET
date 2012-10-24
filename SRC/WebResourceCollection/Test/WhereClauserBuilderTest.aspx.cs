using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HiLand.General.BLL;
using HiLand.Utility.DataBase;
using System.Data.SqlClient;

namespace WebResourceCollection.Test
{
    public partial class WhereClauserBuilderTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ClauseModel<SqlParameter> clauseModel = SqlWhereClauseBuilder.Create().AppendCondition("LoanTermCount", 3).AppendCondition("LoanType", 0).GetClause();
            this.Button1.Text= LoanBasicBLL.Instance.GetList(clauseModel.CluaseString, clauseModel.ParameterList.ToArray()).Count.ToString();
        }
    }
}