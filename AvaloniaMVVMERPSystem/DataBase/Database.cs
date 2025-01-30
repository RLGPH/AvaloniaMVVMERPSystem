using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace AvaloniaMVVMERPSystem.DataBase
{
    public partial class Database
    {
        //jnldfnbdjfnb
        private static SqlConnection GetConnection()
        {
            SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder
            {
                DataSource = "localhost",
                InitialCatalog = "PhoenixEnterpriseDB",
                UserID = "AddsUsers_User",
                Password = "Password123!"
            };

            SqlConnection connection = new SqlConnection(sb.ToString());
            return connection;
        }
        private void CreateSqlUser(SqlConnection conn, string password, string loginName)
        {
            using (SqlCommand cmd = new SqlCommand("CreateAUser", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // Add parameters for login name and password
                cmd.Parameters.AddWithValue("@LoginName", loginName);
                cmd.Parameters.AddWithValue("@Password", password);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
