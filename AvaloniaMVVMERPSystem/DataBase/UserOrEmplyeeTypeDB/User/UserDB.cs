using AvaloniaMVVMERPSystem.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Tmds.DBus.Protocol;

namespace AvaloniaMVVMERPSystem.DataBase
{
    public partial class Database
    {
        public void AddUser(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            using (SqlConnection conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("AddUser", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", user.LastName);
                    cmd.Parameters.AddWithValue("@Password", user.UPassWord);
                    cmd.Parameters.AddWithValue("@Balance", user.Balance);
                    cmd.Parameters.AddWithValue("@CPR", user.PInfo?.CPRNumber);
                    cmd.Parameters.AddWithValue("@PostalCode", user.PInfo?.PostalCode);
                    cmd.Parameters.AddWithValue("@Country", user.PInfo?.Country);
                    cmd.Parameters.AddWithValue("@Address", user.PInfo?.Address);
                    cmd.Parameters.AddWithValue("@Mail", user.PInfo?.Mail);
                    cmd.Parameters.AddWithValue("@City", user.PInfo?.City);
                    cmd.Parameters.AddWithValue("@HouseNumber", user.PInfo?.HouseNumber );
                    cmd.Parameters.AddWithValue("@Tlf", user.PInfo?.Tlf );
                    cmd.Parameters.AddWithValue("@RoadName", user.PInfo?.RoadName );

                    
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    CreateSqlUser(conn, user.UPassWord , (user.FirstName ?? "") + (user.LastName ?? ""));
                }
                conn.Close();
            }
        }

        public User? GetUser(int id)
        {
            using (SqlConnection conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("GetUserByID", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", id);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var Pinfo = new PersonaLInfo(
                                personalInfoId: reader.GetInt32(reader.GetOrdinal("Id")),
                                mail: (string)reader["Mail"],
                                tlf: (string)reader["Tlf"],
                                address: (string)reader["Address"],
                                postalCode: (string)reader["PostalCode"],
                                roadName: (string)reader["RoadName"], 
                                houseNumber: (string)reader["HouseNumber"],
                                city: (string)reader["City"],
                                country: (string)reader["Country"],
                                cprNumber: (string)reader["CprNumber"] 
                            );

                            var user = new User(
                                userId: reader.GetInt32(reader.GetOrdinal("UId")),
                                UpassWord: (string)reader["UserPassword"] ,
                                balance: (float)reader.GetDouble(reader.GetOrdinal("Balance")),
                                personId: reader.GetInt32(reader.GetOrdinal("PersonId")),
                                firstName: (string)reader["FirstName"] ,
                                lastName: (string)reader["LastName"] ,
                                Pinfo
                            );
                            conn.Close();
                            return user;
                        }
                    }
                }
            }
            return null;
        }
        public User? GetUserByName(string firstName, string lastName)
        {
            using (SqlConnection conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("GetUserByFullName", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FirstName", firstName);
                    cmd.Parameters.AddWithValue("@LastName", lastName);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var Pinfo = new PersonaLInfo(
                                personalInfoId: reader.GetInt32(reader.GetOrdinal("Id")),
                                mail: (string)reader["Mail"],
                                tlf: (string)reader["Tlf"],
                                address: (string)reader["Address"],
                                postalCode: (string)reader["PostalCode"],
                                roadName: (string)reader["RoadName"],
                                houseNumber: (string)reader["HouseNumber"],
                                city: (string)reader["City"],
                                country: (string)reader["Country"],
                                cprNumber: (string)reader["CprNumber"]
                            );

                            var user = new User(
                                userId: reader.GetInt32(reader.GetOrdinal("UId")),
                                UpassWord: (string)reader["UserPassword"],
                                balance: (float)reader.GetDecimal(reader.GetOrdinal("Balance")),
                                personId: reader.GetInt32(reader.GetOrdinal("PersonId")),
                                firstName: (string)reader["FirstName"],
                                lastName: (string)reader["LastName"],
                                Pinfo
                            );
                            conn.Close();
                            return user;
                        }
                    }
                }
            }
            return null;
        }

        public void UpdateUser(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            using (SqlConnection conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("UpdateUser", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserId", user.UserId);
                    cmd.Parameters.AddWithValue("@FirstName", user.FirstName );
                    cmd.Parameters.AddWithValue("@LastName", user.LastName );
                    cmd.Parameters.AddWithValue("@Password", user.UPassWord );
                    cmd.Parameters.AddWithValue("@CPR", user.PInfo?.CPRNumber );
                    cmd.Parameters.AddWithValue("@PostalCode", user.PInfo?.PostalCode );
                    cmd.Parameters.AddWithValue("@Country", user.PInfo?.Country );
                    cmd.Parameters.AddWithValue("@Address", user.PInfo?.Address );
                    cmd.Parameters.AddWithValue("@Mail", user.PInfo?.Mail );
                    cmd.Parameters.AddWithValue("@City", user.PInfo?.City );
                    cmd.Parameters.AddWithValue("@HouseNumber", user.PInfo?.HouseNumber );
                    cmd.Parameters.AddWithValue("@Tlf", user.PInfo?.Tlf );
                    cmd.Parameters.AddWithValue("@RoadName", user.PInfo?.RoadName );

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void DeleteUser(int id) 
        {
            using (SqlConnection conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("DeleteUser", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
