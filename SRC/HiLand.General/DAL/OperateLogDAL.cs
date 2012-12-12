using System.Data.SqlClient;
using HiLand.General.DALCommon;

namespace HiLand.General.DAL
{
    public class OperateLogDAL : OperateLogCommonDAL<SqlTransaction, SqlConnection, SqlCommand, SqlDataReader, SqlParameter>
    {

    }
}
