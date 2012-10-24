using System.Data.SqlClient;
using HiLand.General.DALCommon;

namespace HiLand.General.DAL
{
    public class ImageDAL : ImageCommonDAL< SqlTransaction, SqlConnection, SqlCommand, SqlDataReader, SqlParameter>
    {
       
    }
}
