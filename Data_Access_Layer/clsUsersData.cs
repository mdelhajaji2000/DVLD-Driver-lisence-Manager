using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Data_Access_Layer
{
    public static class clsUsersData
    {
        public static DataTable GetAllUsers()
        {
            DataTable Users = new DataTable();

            SqlConnection connection = new SqlConnection();

            string Query = "SELECT * FROM Users";

            SqlCommand command = new SqlCommand(Query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                    Users.Load(reader);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                connection.Close();
            }

            return Users;
        }

        public static bool GetUserByUserName(string UserName, ref int PersonID, ref int UserID, ref string Password, ref bool IsActive)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "SELECT * FROM Users Where UserName = @UserName";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@UserName", UserName);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    UserID = (int)reader["UserID"];
                    PersonID = (int)reader["PersonID"];
                    Password = (string)reader["Password"];
                    IsActive = (bool)reader["IsActive"];
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                connection.Close();
            }

            return IsFound;
        }

        public static bool GetUserByUserID(int UserID, ref int PersonID, ref string UserName, ref string Password, ref bool IsActive)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "SELECT * FROM Users Where UserID = @UserID";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    UserName = (string)reader["UserName"];
                    PersonID = (int)reader["PersonID"];
                    Password = (string)reader["Password"];
                    IsActive = (bool)reader["IsActive"];
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                connection.Close();
            }

            return IsFound;
        }

        public static bool IsPerson_a_User(int PersonID)
        {
            bool IsPersonAnUser = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "SELECT 1 FROM Users WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@PeronsID", PersonID);

            try
            {
                connection.Open();

                object Result = command.ExecuteScalar();

                if (Result != null && int.TryParse(Result.ToString(), out int intResult))
                {
                    IsPersonAnUser = (intResult == 1);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                connection.Close();
            }

            return IsPersonAnUser;
        }

        public static int CreateNewUser(int PersonID, string UserName, string Password, bool IsActive)
        {
            int ID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "INSERT INTO Users (PersonID, UserName, Password, IsActive)" +
                " Values " +
                " (@PersonID, @UserName, @Password, @IsActive); " +
                "  Select SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);

            if (IsActive)
                command.Parameters.AddWithValue("@IsActive", 1);
            else
                command.Parameters.AddWithValue("@IsActive", 0);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int intResult))
                {
                    ID = intResult;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                connection.Close();
            }


            return ID;
        }

        public static bool UpdateUser(int UserID, int PersonID, string UserName, string Password, bool IsActive)
        {
            bool IsUpdated = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "Update Users Set " +
                "PersonID = @PersonID, " +
                "UserName = @UserName, " +
                "Password = @Password, " +
                "IsActive = @IsActive" +
                " WHERE UserID = @UserID";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@UserID", UserID);

            if (IsActive)
                command.Parameters.AddWithValue("@IsActive", 1);
            else
                command.Parameters.AddWithValue("@IsActive", 0);

            try
            {
                connection.Open();

                int AffectedRows = command.ExecuteNonQuery();

                IsUpdated = AffectedRows > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                connection.Close();
            }

            return IsUpdated;
        }

        public static bool DeleteUser(int UserID)
        {
            bool isDeleted = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "DELETE FROM Users WHERE UserID = @UserID";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);

            return isDeleted;
        }

        public static bool ChangeActivationStatus(int UserID, bool IsActive)
        {
            bool IsChanged = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "Update Users Set IsActive = @IsActive WHERE UserID = @UserID";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            if (IsActive)
                command.Parameters.AddWithValue("@IsActive", 1);
            else
                command.Parameters.AddWithValue("@IsActive", 0);

            try
            {
                connection.Open();

                IsChanged = command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                connection.Close();
            }

            return IsChanged;
        }

    }
}
