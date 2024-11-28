using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvaloniaMVVMERPSystem.Classes;

namespace AvaloniaMVVMERPSystem.DataBase
{
    public partial class Database
    {
        public void AddItem(Item item)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("AddItems", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ItemName", item.Name);
                    cmd.Parameters.AddWithValue("@Description", item.Description);

                }
            }
        }
        public void AddCombinedLocation(int LocationId, int ItemId)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@LocationId", LocationId);
                    cmd.Parameters.AddWithValue("@ItemId", ItemId);
                }
            }
        }
    }
}
