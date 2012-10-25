using System.Data.SqlClient;
using HiLand.General.DALCommon;

namespace HiLand.General.DAL
{
    public class EnterpriseDAL : EnterpriseCommonDAL< SqlTransaction, SqlConnection, SqlCommand, SqlDataReader, SqlParameter>
    {
      
    }
}
