using System.Data.SqlClient;
using HiLand.Framework.BusinessCore.DALCommon;

namespace HiLand.Framework.BusinessCore.DAL
{
    public class BusinessUserDAL : BusinessUserCommonDAL<SqlTransaction, SqlConnection, SqlCommand, SqlDataReader, SqlParameter>
    {

    }
}
