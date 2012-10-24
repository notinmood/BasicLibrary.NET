using System.Data.SqlClient;
using HiLand.General.DALCommon;

namespace HiLand.General.DAL
{
    public class LoanScheduleDAL : LoanScheduleCommonDAL<SqlTransaction, SqlConnection, SqlCommand, SqlDataReader, SqlParameter>, ILoanScheduleDAL
    {

    }
}
