using AvaloniaMVVMERPSystem.Classes;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AvaloniaMVVMERPSystem.DataBase
{
    public partial class Database
    {
        // CreateEmployee method to add a new employee
        public int CreateEmployee(Employee employee)
        {
            using (SqlConnection conn = GetConnection()) // Call the static GetConnection method
            {
                using (SqlCommand cmd = new SqlCommand("CreateEmployee", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", employee.LastName);
                    cmd.Parameters.AddWithValue("@Mail", employee.PInfo.Mail);
                    cmd.Parameters.AddWithValue("@Tlf", employee.PInfo.Tlf);
                    cmd.Parameters.AddWithValue("@CPRNumber", employee.CPRNumber);
                    cmd.Parameters.AddWithValue("@Address", employee.PInfo.Address);
                    cmd.Parameters.AddWithValue("@PostalCode", employee.PInfo.PostalCode);
                    cmd.Parameters.AddWithValue("@RoadName", employee.PInfo.RoadName);
                    cmd.Parameters.AddWithValue("@HouseNumber", employee.PInfo.HouseNumber);
                    cmd.Parameters.AddWithValue("@City", employee.PInfo.City);
                    cmd.Parameters.AddWithValue("@Country", employee.PInfo.Country);
                    cmd.Parameters.AddWithValue("@EmployeePassword", employee.EmployeePassword);
                    cmd.Parameters.AddWithValue("@Title", employee.Title);
                    cmd.Parameters.AddWithValue("@WorkMail", employee.WorkMail);
                    cmd.Parameters.AddWithValue("@WorkTlf", employee.WorkTlf);
                    cmd.Parameters.AddWithValue("@AdminPassword", (object)employee.AdminPassword ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IsAdmin", employee._admin.IsAdmin);
                    cmd.Parameters.AddWithValue("@IsMod", employee._moderator.IsMod);

                    // Output parameter to get the created EmployeeId
                    SqlParameter outputIdParam = new SqlParameter("@EmployeeId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputIdParam);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    return (int)outputIdParam.Value; // Return the EmployeeId
                }
            }
        }

        public Employee GetEmployee(int employeeId)
        {
            using (SqlConnection conn = GetConnection()) // Call the static GetConnection method
            {
                using (SqlCommand cmd = new SqlCommand("GetEmployee", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmployeeId", employeeId);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Creating PersonaLInfo instance
                            var personalInfo = new PersonaLInfo(
                                personalInfoId: reader.GetInt32(reader.GetOrdinal("PAddressId")),
                                mail: reader.GetString(reader.GetOrdinal("Mail")),
                                tlf: reader.GetString(reader.GetOrdinal("Tlf")),
                                address: reader.GetString(reader.GetOrdinal("Address")),
                                postalCode: reader.GetString(reader.GetOrdinal("PostalCode")),
                                roadName: reader.GetString(reader.GetOrdinal("RoadName")),
                                houseNumber: reader.GetString(reader.GetOrdinal("HouseNumber")),
                                city: reader.GetString(reader.GetOrdinal("City")),
                                country: reader.GetString(reader.GetOrdinal("Country"))
                            );

                            // Creating Admin instance
                            var admin = new Admin(
                                adminId: reader.GetInt32(reader.GetOrdinal("AdminId")),
                                isAdmin: reader.GetBoolean(reader.GetOrdinal("IsAdmin"))
                            );

                            // Creating Moderator instance
                            var moderator = new Moderator(
                                modId: reader.GetInt32(reader.GetOrdinal("ModeratorId")),
                                isMod: reader.GetBoolean(reader.GetOrdinal("IsMod"))
                            );

                            // Creating and returning the Employee object
                            return new Employee(
                                employeeId: reader.GetInt32(reader.GetOrdinal("EmployeeId")),
                                emplyeePassword: reader.GetString(reader.GetOrdinal("EmployeePassword")),
                                title: reader.GetString(reader.GetOrdinal("Title")),
                                workMail: reader.GetString(reader.GetOrdinal("WorkMail")),
                                workTlf: reader.GetString(reader.GetOrdinal("WorkTlf")),
                                adminPassword: reader.GetString(reader.GetOrdinal("AdminPassword")), // No null check needed if DB always provides a value
                                personId: reader.GetInt32(reader.GetOrdinal("PersonId")),
                                firstName: reader.GetString(reader.GetOrdinal("FirstName")),
                                lastName: reader.GetString(reader.GetOrdinal("LastName")),
                                cprNumber: reader.GetString(reader.GetOrdinal("CPRNumber")),
                                pInfo: personalInfo,
                                admin: admin,
                                moderator: moderator
                            );
                        }
                        else
                        {
                            return null; // No employee found
                        }
                    }
                }
            }
        }

    }
}
