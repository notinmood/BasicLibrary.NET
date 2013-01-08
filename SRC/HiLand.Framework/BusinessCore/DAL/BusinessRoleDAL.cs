using System.Data.SqlClient;
using HiLand.Framework.BusinessCore.DALCommon;

namespace HiLand.Framework.BusinessCore.DAL
{
    public class BusinessRoleDAL : BusinessRoleCommonDAL<SqlTransaction, SqlConnection, SqlCommand, SqlDataReader, SqlParameter>
    {

    }
}
