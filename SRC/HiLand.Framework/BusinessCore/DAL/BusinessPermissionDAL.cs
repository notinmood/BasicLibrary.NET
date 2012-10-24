using System.Data.SqlClient;
using HiLand.Framework.BusinessCore.DALCommon;

namespace HiLand.Framework.BusinessCore.DAL
{
    public class BusinessPermissionDAL : BusinessPermissionCommonDAL<SqlTransaction, SqlConnection, SqlCommand, SqlDataReader, SqlParameter>, IBusinessPermissionDAL
    {
    }
}
