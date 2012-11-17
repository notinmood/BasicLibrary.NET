using System.Data.SqlClient;
using HiLand.General.DALCommon;

namespace HiLand.General.DAL
{
    /// <summary>
    /// （产品等）预定数据实体数据访问类
    /// </summary>
    public class ForeOrderDAL : ForeOrderCommonDAL<SqlTransaction, SqlConnection, SqlCommand, SqlDataReader, SqlParameter>
    {

    }
}
