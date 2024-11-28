﻿using Avalonia.Controls;
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
        public void AddLocation(Classes.Location location)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("AddLocation", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@LocationName", location.LocationName);
                    cmd.Parameters.AddWithValue("@LCountry", location.LCountry);
                    cmd.Parameters.AddWithValue("@LStreet", location.LStreet);
                    cmd.Parameters.AddWithValue("@LCity", location.LCity);
                    cmd.Parameters.AddWithValue("@LZipCode", location.LZipCode);
                    cmd.Parameters.AddWithValue("@StorageSpaceLeft", location.StorageSpaceLeft);

                }
            }
        }

        public ObservableCollection<Classes.Location> GetLocations()
        {
            ObservableCollection<Classes.Location> locations = new ObservableCollection<Classes.Location>();

            using (SqlConnection conn = GetConnection()) 
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("GetAllLocations", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Classes.Location location = new Classes.Location(
                                reader.GetInt32(reader.GetOrdinal("LocationId")),
                                reader.GetString(reader.GetOrdinal("LocationName")),
                                reader.GetString(reader.GetOrdinal("LCountry")),
                                reader.GetString(reader.GetOrdinal("LCity")),
                                reader.GetString(reader.GetOrdinal("LStreet")),
                                reader.GetString(reader.GetOrdinal("LZipCode")),
                                reader.GetFloat(reader.GetOrdinal("StorageSpaceLeft"))
                            );
                            locations.Add(location);
                        }
                    }
                }
            }

            return locations;
        }
    }
}
