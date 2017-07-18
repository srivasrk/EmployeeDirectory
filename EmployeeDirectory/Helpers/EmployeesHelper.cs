using EmployeeDirectory.Models;
using EmployeeDirectory.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeDirectory.Helpers
{
    public static class EmployeesHelper
    {

        public static void DeleteEmployeeById(int EmployeeId)
        {

            int count = 0;
            string Query = "DELETE FROM [dbo].[Employees] WHERE EmployeeID = @EmployeeID";
            
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                using (var command = new SqlCommand(Query, conn))
                {
                    command.Parameters.Add("@EmployeeID", SqlDbType.Int);
                    command.Parameters["@EmployeeID"].Value = EmployeeId;
                    command.ExecuteScalar();
                }
            }
        }

        public static int GetTotalNumberOfEmployees()
        {
            int count = 0;
            string Query = "SELECT Count(*) FROM [dbo].[Employees]";

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                using (var command = new SqlCommand(Query, conn))
                {
                    count = (int)command.ExecuteScalar();
                }
            }

            return count;
        }

        public static IEnumerable<EmployeeModel> GetEmployees(string Search, int Offset)
        {
            string Query;
            if (String.IsNullOrEmpty(Search))
            {
                Query = "SELECT * FROM [dbo].[Employees] " +
                    " ORDER BY EmployeeID OFFSET " + 
                    Convert.ToString(Offset) +
                    " ROWS FETCH NEXT 6 ROWS ONLY;";                
            }
            else
            {
                Query = "SELECT * FROM[dbo].[Employees] WHERE" +
                    "(FirstName LIKE '%" + Search + "%') OR " +
                    "(LastName LIKE '%" + Search + "%') OR " +
                    "(Email LIKE '%" + Search + "%') ";                
            }


            IList<EmployeeModel> Employees = new List<EmployeeModel>();
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                using (var command = new SqlCommand(Query, conn))
                {
                    ExecuteCommandAndAddEmployees(command, Employees);
                }
            }

            return Employees;
        }

        private static void ExecuteCommandAndAddEmployees(SqlCommand Command, IList<EmployeeModel> Employees)
        {
            Command.CommandType = CommandType.Text;
            using (SqlDataReader reader = Command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Employees.Add(
                           new EmployeeModel()
                           {
                               EmployeeId = Convert.ToInt32(reader["EmployeeId"]),
                               FirstName = Convert.ToString(reader["FirstName"]).Trim(),
                               LastName = Convert.ToString(reader["LastName"]).Trim(),
                               JobTitle = Convert.ToString(reader["JobTitle"]).Trim(),
                               Email = Convert.ToString(reader["Email"]).Trim(),
                               Location = Convert.ToString(reader["Location"]).Trim(),
                               IsAdmin = Convert.ToBoolean(reader["IsAdmin"])
                           });
                }

                reader.Close();
            }
            Command.Cancel();

        }

        public static IEnumerable<EmployeeModel> GetEmployees()
        {
            IList<EmployeeModel> Employees = new List<EmployeeModel>();
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                using (var command = new SqlCommand(string.Format("SELECT * FROM [dbo].[Employees]"), conn))
                {
                    ExecuteCommandAndAddEmployees(command, Employees);
                }
            }

            return Employees;
        }

        public static bool DoesEmployeeExists(string Email)
        {
            int count = 0;
            string Query = "SELECT Count(*) FROM [dbo].[Employees] Where " +
                    "(Email = '" + Email + "')";

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                using (var command = new SqlCommand(Query, conn))
                {
                    count = (int)command.ExecuteScalar();
                }
            }

            return (count > 0);
        }
        
        public static void AddEmployee(EmployeeModel Employee)
        {
            string Query = "INSERT INTO [dbo].[Employees](FirstName, LastName, JobTitle, Location, Email, IsAdmin) " +
                "VALUES(@FirstName, @LastName, @JobTitle, @Location, @Email, @IsAdmin)";
                                    
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                using (var command = new SqlCommand(Query, conn))
                {
                    command.Parameters.Add("@FirstName", SqlDbType.VarChar, 50);
                    command.Parameters.Add("@LastName", SqlDbType.VarChar, 50);
                    command.Parameters.Add("@Location", SqlDbType.VarChar, 50);
                    command.Parameters.Add("@JobTitle", SqlDbType.VarChar, 50);
                    command.Parameters.Add("@Email", SqlDbType.VarChar, 50);
                    command.Parameters.Add("@IsAdmin", SqlDbType.Bit);

                    command.Parameters["@FirstName"].Value = Employee.FirstName;
                    command.Parameters["@LastName"].Value = Employee.LastName;
                    command.Parameters["@Location"].Value = Employee.Location;
                    command.Parameters["@Email"].Value = Employee.Email;
                    command.Parameters["@JobTitle"].Value = Employee.JobTitle;
                    command.Parameters["@IsAdmin"].Value = Employee.IsAdmin;
                    command.ExecuteNonQuery();
                }
            }                      

        }

        public static void UpdatePassword(EmpoyeeCredentialsViewModel EmployeeCredentials)
        {
            string Query = "UPDATE [dbo].[User] SET Password = @Password WHERE Email = @Email";

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                using (var command = new SqlCommand(Query, conn))
                {
                    command.Parameters.Add("@Password", SqlDbType.VarChar, 50);
                    command.Parameters.Add("@Email", SqlDbType.VarChar, 50);

                    command.Parameters["@Password"].Value = EmployeeCredentials.Password;
                    command.Parameters["@Email"].Value = EmployeeCredentials.Email;
                    command.ExecuteNonQuery();
                }
            }

        }

        public static void UpdateEmployeeData(EmployeeModel Employee)
        {

            string Query = "UPDATE [dbo].[Employees] SET " +
                "FirstName = @FirstName, " +
                "LastName = @LastName, " +
                "Email = @Email, " +
                "JobTitle = @JobTitle, " +
                "Location = @Location, " +
                "IsAdmin = @IsAdmin " +
                "WHERE EmployeeId = @EmployeeId";

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                conn.Open();
                using (var command = new SqlCommand(Query, conn))
                {
                    command.Parameters.Add("@FirstName", SqlDbType.VarChar, 50);
                    command.Parameters.Add("@LastName", SqlDbType.VarChar, 50);
                    command.Parameters.Add("@Email", SqlDbType.VarChar, 50);
                    command.Parameters.Add("@JobTitle", SqlDbType.VarChar, 50);
                    command.Parameters.Add("@Location", SqlDbType.VarChar, 50);
                    command.Parameters.Add("@IsAdmin", SqlDbType.Bit);
                    command.Parameters.Add("@EmployeeId", SqlDbType.Int);

                    command.Parameters["@FirstName"].Value = Employee.FirstName;
                    command.Parameters["@LastName"].Value = Employee.LastName;
                    command.Parameters["@Email"].Value = Employee.Email;
                    command.Parameters["@JobTitle"].Value = Employee.JobTitle;
                    command.Parameters["@Location"].Value = Employee.Location;
                    command.Parameters["@IsAdmin"].Value = Employee.IsAdmin;
                    command.Parameters["@EmployeeId"].Value = Employee.EmployeeId;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}