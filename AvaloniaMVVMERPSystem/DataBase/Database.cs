using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace AvaloniaMVVMERPSystem.DataBase
{
    public partial class Database
    {
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
    }
}
