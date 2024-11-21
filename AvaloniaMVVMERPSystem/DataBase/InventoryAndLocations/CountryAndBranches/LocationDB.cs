using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AvaloniaMVVMERPSystem.Classes;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace AvaloniaMVVMERPSystem.DataBase
{
    public partial class Database
    {
        public ObservableCollection<Classes.Location> GetLocations()
        {
            ObservableCollection<Classes.Location> locations = new ObservableCollection<Classes.Location>();

            using (SqlConnection conn = GetConnection()) // Call the static GetConnection method
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("GetAllLocations", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Classes.Location location = new Classes.Location
                            {
                                LocationId = reader.GetInt32(reader.GetOrdinal("LocationId")),
                                LocationName = reader.GetString(reader.GetOrdinal("LocationName")),
                                LCountry = reader.GetString(reader.GetOrdinal("LCountry")),
                                LCity = reader.GetString(reader.GetOrdinal("LCity")),
                                LStreet = reader.GetString(reader.GetOrdinal("LStreet")),
                                LZipCode = reader.GetString(reader.GetOrdinal("LZipCode")),
                                StorageSpaceLeft = reader.GetInt32(reader.GetOrdinal("StorageSpaceLeft"))
                            };
                            locations.Add(location);
                        }
                    }
                }
            }

            return locations;
        }
    }
}
