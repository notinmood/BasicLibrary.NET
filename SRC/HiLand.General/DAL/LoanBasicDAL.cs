using System.Data.SqlClient;
using HiLand.General.DALCommon;

namespace HiLand.General.DAL
{
    public class LoanBasicDAL : LoanBasicCommonDAL<SqlTransaction, SqlConnection, SqlCommand, SqlDataReader, SqlParameter>, ILoanBasicDAL
    {

    }
}
