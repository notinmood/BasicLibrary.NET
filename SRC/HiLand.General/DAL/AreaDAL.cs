using System.Data.SqlClient;
using HiLand.General.DALCommon;

namespace HiLand.General.DAL
{
    /// <summary>
    /// 地区SqlServer的数据访问类
    /// </summary>
    public class AreaDAL : AreaCommonDAL<SqlTransaction, SqlConnection, SqlCommand, SqlDataReader, SqlParameter>
    {

    }
}
