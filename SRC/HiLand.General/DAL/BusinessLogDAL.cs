using System.Data.SqlClient;
using HiLand.General.DALCommon;

namespace HiLand.General.DAL
{
    public class BusinessLogDAL : BusinessLogCommonDAL<SqlTransaction, SqlConnection, SqlCommand, SqlDataReader, SqlParameter>
    {

    }
}
