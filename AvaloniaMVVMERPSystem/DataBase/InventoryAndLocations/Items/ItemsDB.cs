using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvaloniaMVVMERPSystem.Classes;

namespace AvaloniaMVVMERPSystem.DataBase
{
    public partial class Database
    {
        public int AddItem(Item item)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("AddItems", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ItemName", item.Name);
                    cmd.Parameters.AddWithValue("@Description", item.Description);

                    var newId = cmd.ExecuteScalar();

                    return Convert.ToInt32(newId);
                }
            }
        }

        public void AddCombinedLocation(int LocationId, int ItemId)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("AddCombinedLocation",conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@LocationId", LocationId);
                    cmd.Parameters.AddWithValue("@ItemId", ItemId);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
