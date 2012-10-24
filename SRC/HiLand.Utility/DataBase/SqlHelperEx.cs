using System.Data;
using System.Data.SqlClient;
using HiLand.Utility.Setting;

namespace HiLand.Utility.DataBase
{
    /// <summary>
    /// SqlHelper包装器
    /// </summary>
    public class SqlHelperEx : CommonHelperEx<SqlTransaction, SqlConnection, SqlCommand, SqlDataReader, SqlParameter>
    {
       
    }
}
