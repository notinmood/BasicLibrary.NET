using System.Data.SqlClient;
using HiLand.General.DALCommon;
using HiLand.Utility.DataBase;

namespace HiLand.General.DAL
{
    public class BasicSettingDAL : BasicSettingCommonDAL< SqlTransaction, SqlConnection, SqlCommand, SqlDataReader, SqlParameter>,IBasicSettingDAL
    {
       
    }
}
