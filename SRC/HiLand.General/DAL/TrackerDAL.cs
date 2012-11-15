using System.Data.SqlClient;
using HiLand.General.DALCommon;

namespace HiLand.General.DAL
{
    /// <summary>
    /// 回访、跟踪数据访问类
    /// </summary>
    public class TrackerDAL : TrackerCommonDAL<SqlTransaction, SqlConnection, SqlCommand, SqlDataReader, SqlParameter>
    {

    }
}
