using AvaloniaMVVMERPSystem.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Tmds.DBus.Protocol;

namespace AvaloniaMVVMERPSystem.DataBase
{
    public partial class Database
    {
        public void AddUser(User user)
        {
            using (SqlConnection conn = GetConnection()) 
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("AddUser", conn)) 
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", user.LastName);
                    cmd.Parameters.AddWithValue("@Password", user.UPassWord);
                    cmd.Parameters.AddWithValue("@CPR", user.CPRNumber);
                    
                    cmd.ExecuteNonQuery();
                    CreateSqlUser(conn, user.UPassWord, user.FirstName + user.LastName);
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
                                personalInfoId: reader.GetInt32(reader.GetOrdinal("@Id")),
                                mail: reader.GetString(reader.GetOrdinal("@Mail")),
                                tlf: reader.GetString(reader.GetOrdinal("@Tlf")),
                                address: reader.GetString(reader.GetOrdinal("@Address")),
                                postalCode: reader.GetString(reader.GetOrdinal("@postalCode")),
                                roadName: reader.GetString(reader.GetOrdinal("@RoadName")),
                                houseNumber: reader.GetString(reader.GetOrdinal("@HousNumber")),
                                city: reader.GetString(reader.GetOrdinal("@City")),
                                country: reader.GetString(reader.GetOrdinal("@Country"))
                            );

                            var user = new User(
                                userId: reader.GetInt32(reader.GetOrdinal("@UId")),
                                UpassWord: reader.GetString(reader.GetOrdinal("@UserPassword")),
                                personId: reader.GetInt32(reader.GetOrdinal("@PersonId")),
                                firstName: reader.GetString(reader.GetOrdinal("@FirstName")),
                                lastName: reader.GetString(reader.GetOrdinal("@LastName")),
                                cprNumber: reader.GetString(reader.GetOrdinal("@CprNumber")),
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
        public void UpdateUser(User user) 
        {
            using (SqlConnection conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("UpdateUser", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserId", user.UserId);
                    cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", user.LastName);
                    cmd.Parameters.AddWithValue("@Password", user.UPassWord);
                    cmd.Parameters.AddWithValue("@CPR", user.CPRNumber);
                    cmd.Parameters.AddWithValue("@PostalCode", user.PInfo.PostalCode);
                    cmd.Parameters.AddWithValue("@Country", user.PInfo.Country);
                    cmd.Parameters.AddWithValue("@Address", user.PInfo.Address);
                    cmd.Parameters.AddWithValue("@Mail", user.PInfo.Mail);
                    cmd.Parameters.AddWithValue("@City", user.PInfo.City);
                    cmd.Parameters.AddWithValue("@HouseNumber", user.PInfo.HouseNumber);
                    cmd.Parameters.AddWithValue("@Tlf", user.PInfo.Tlf);
                    cmd.Parameters.AddWithValue("@RoadName", user.PInfo.RoadName);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
