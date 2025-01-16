using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using AvaloniaMVVMERPSystem.Classes;
using Tmds.DBus.Protocol;

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

        public ObservableCollection<CombinedItemLocation> GetAllItems()
        {
            var combinedList = new ObservableCollection<CombinedItemLocation>();

            using (SqlConnection conn = GetConnection()) 
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("GetItems", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    

                    using (SqlDataReader reader = cmd.ExecuteReader()) 
                    {
                        if (reader.Read()) 
                        {
                            var items = new Item(
                                id: reader.GetInt32(reader.GetOrdinal("ItemId")),
                                name: reader.GetString(reader.GetOrdinal("ItemName")),
                                description: reader.GetString(reader.GetOrdinal("ItemDescription"))
                                );

                            var locations = new Location(
                                locationId: reader.GetInt32(reader.GetOrdinal("LocationId")),
                                locationName: reader.GetString(reader.GetOrdinal("LocationName")),
                                lCountry: reader.GetString(reader.GetOrdinal("LCountry")),
                                lCity: reader.GetString(reader.GetOrdinal("LCity")),
                                lStreet: reader.GetString(reader.GetOrdinal("LStreet")),
                                lZipCode: reader.GetString(reader.GetOrdinal("LZipCode")),
                                storageSpaceLeft: (float)reader.GetDouble(reader.GetOrdinal("StorageSpaceLeft")) // Explicit conversion
                            );


                            var combined = new CombinedItemLocation(
                                cominedID: reader.GetInt32(reader.GetOrdinal("CombinedId")),
                                location: locations,
                                thing: items
                                );
                            combinedList.Add( combined );
                        }
                    }
                    conn.Close();
                    return combinedList;
                }
            }
        }

        public void UpdateItem(Item item)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("UpdateItem", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("ItemId", item.Id);
                    cmd.Parameters.AddWithValue("@ItemName", item.Name);
                    cmd.Parameters.AddWithValue("@Description", item.Description);
                }
                conn.Close();
            }
        }
    }
}
