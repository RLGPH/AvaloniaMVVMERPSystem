﻿using AvaloniaMVVMERPSystem.Classes;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AvaloniaMVVMERPSystem.DataBase
{
    public partial class Database
    {
        public void CreateEmployee(Employee employee)
        {
            using (SqlConnection conn = GetConnection())
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

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    CreateSqlUser(conn, employee.EmployeePassword, employee.FirstName + employee.LastName);
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
                            // Creating PersonInfo instance
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

                            // Creating Admin instance with default values if not found
                            Admin admin = new Admin(
                                adminId: reader.IsDBNull(reader.GetOrdinal("AdminId")) ? 0 : reader.GetInt32(reader.GetOrdinal("AdminId")),
                                isAdmin: reader.IsDBNull(reader.GetOrdinal("IsAdmin")) ? false : reader.GetBoolean(reader.GetOrdinal("IsAdmin"))
                            );

                            // Creating Moderator instance with default values if not found
                            Moderator moderator = new Moderator(
                                modId: reader.IsDBNull(reader.GetOrdinal("ModeratorId")) ? 0 : reader.GetInt32(reader.GetOrdinal("ModeratorId")),
                                isMod: reader.IsDBNull(reader.GetOrdinal("IsMod")) ? false : reader.GetBoolean(reader.GetOrdinal("IsMod"))
                            );

                            // Creating and returning the Employee object
                            return new Employee(
                                employeeId: reader.GetInt32(reader.GetOrdinal("EmployeeId")),
                                emplyeePassword: reader.GetString(reader.GetOrdinal("EmployeePassword")),
                                title: reader.GetString(reader.GetOrdinal("Title")),
                                workMail: reader.GetString(reader.GetOrdinal("WorkMail")),
                                workTlf: reader.GetString(reader.GetOrdinal("WorkTlf")),
                                adminPassword: reader.IsDBNull(reader.GetOrdinal("AdminPassword")) ? null : reader.GetString(reader.GetOrdinal("AdminPassword")), // Null check for admin password
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

        public Employee EditEmployee(Employee ChangedEmployee, Employee OriginalEmployee)
        {
            using (SqlConnection conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("EditEmployee", conn))
                {
                    if (ChangedEmployee != OriginalEmployee && ChangedEmployee.EmployeeId == OriginalEmployee.EmployeeId)
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@EmployeeId", OriginalEmployee.EmployeeId);
                        cmd.Parameters.AddWithValue("@FirstName", ChangedEmployee.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", ChangedEmployee.LastName);
                        cmd.Parameters.AddWithValue("@Mail", ChangedEmployee.PInfo.Mail);
                        cmd.Parameters.AddWithValue("@Tlf", ChangedEmployee.PInfo.Tlf);
                        cmd.Parameters.AddWithValue("@CPRNumber", ChangedEmployee.CPRNumber);
                        cmd.Parameters.AddWithValue("@Address", ChangedEmployee.PInfo.Address);
                        cmd.Parameters.AddWithValue("@PostalCode", ChangedEmployee.PInfo.PostalCode);
                        cmd.Parameters.AddWithValue("@RoadName", ChangedEmployee.PInfo.RoadName);
                        cmd.Parameters.AddWithValue("@HouseNumber", ChangedEmployee.PInfo.HouseNumber);
                        cmd.Parameters.AddWithValue("@City", ChangedEmployee.PInfo.City);
                        cmd.Parameters.AddWithValue("@Country", ChangedEmployee.PInfo.Country);
                        cmd.Parameters.AddWithValue("@EmployeePassword", ChangedEmployee.EmployeePassword);
                        cmd.Parameters.AddWithValue("@Title", ChangedEmployee.Title);
                        cmd.Parameters.AddWithValue("@WorkMail", ChangedEmployee.WorkMail);
                        cmd.Parameters.AddWithValue("@WorkTlf", ChangedEmployee.WorkTlf);
                        cmd.Parameters.AddWithValue("@AdminPassword", (object)ChangedEmployee.AdminPassword ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@IsAdmin", ChangedEmployee._admin.IsAdmin);
                        cmd.Parameters.AddWithValue("@IsMod", ChangedEmployee._moderator.IsMod);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        return ChangedEmployee;
                    }

                    return OriginalEmployee;
                }
            }
        }
    }
}