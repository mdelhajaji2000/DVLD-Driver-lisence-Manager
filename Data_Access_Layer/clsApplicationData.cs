using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Net.Configuration;
using System.Net.Http.Headers;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer
{
    public static class clsApplicationData
    {
        public static DataTable GetAllApplications()
        {
            DataTable Applications = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "SELECT * FROM Applications";

            SqlCommand command = new SqlCommand(Query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                    Applications.Load(reader);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                connection.Close();
            }

            return Applications;
        }

        public static DataTable GetAllApplicationByPerson(int PerosnID)
        {
            DataTable Applications = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "SELECT * FROM Applications WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@PersonID", PerosnID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                    Applications.Load(reader);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                connection.Close();
            }

            return Applications;
        }

        public static bool GetApplicationByApplicationID(int ApplicationID, ref int ApplicationPersonID, ref DateTime ApplicationDate,
            ref int ApplicationTypeID, ref int ApplicationStatus, ref DateTime LastStatusDate, ref int PaidFees, ref int CreatedByUserID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "SELECT * FROM Applications WHERE ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    ApplicationPersonID = (int)reader["ApplicationPersonID"];
                    ApplicationDate = (DateTime)reader["ApplicationDate"];
                    ApplicationTypeID = (int)reader["ApplicationTypeID"];
                    ApplicationStatus = (int)reader["ApplicationStatus"];
                    LastStatusDate = (DateTime)reader["LastStatusDate"];
                    PaidFees = (int)reader["PaidFees"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];

                    IsFound = true;
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

        public static int CreateNewApplication(int ApplicationPersonID, DateTime ApplicationDate,
            int ApplicationTypeID, int ApplicationStatus, DateTime LastStatusDate, int PaidFees, int CreatedByUserID)
        {
            int ApplicationID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "INSERT INTO Applications (ApplicationPersonID, ApplicationDate, ApplicationTypeID," +
                " ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID) " +
                " Values " +
                "(@ApplicationPersonID, @ApplicationDate, @ApplicationTypeID, @ApplicationStatus, @LastStatusDate, @PaidFees, @CreatedByUserID);" +
                "SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@ApplicationPersonID", ApplicationPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int intResult))
                {
                    ApplicationID = intResult;
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

            return ApplicationID;
        }

        public static bool UpdateApplication(int ApplicationID, int ApplicationPersonID, DateTime ApplicationDate,
            int ApplicationTypeID, int ApplicationStatus, DateTime LastStatusDate, int PaidFees, int CreatedByUserID)
        {
            bool IsUpdated = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "UPDATE Applications Set " + 
            "ApplicationPersonID = @ApplicationPersonID, " + 
            "ApplicationDate = @ApplicationDate, " +
            "ApplicationStatus = @ApplicationStatus, " + 
            "LastStatusDate = @LaststatusDate, " + 
            "PaidFees = @PaidFees, " +
            "CreatedByUserID " + 
            "WHERE ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@ApplicationPersonID", ApplicationPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();

                int AffectedRows = command.ExecuteNonQuery();

                IsUpdated = AffectedRows == 1;
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

        public static bool DeleteApplication(int ApplicationID)
        {
            bool IsDeleted = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "DELETE FROM Applications WHERE ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();

                int AffectedRows  = command.ExecuteNonQuery();

                IsDeleted = AffectedRows == 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                connection.Close();
            }


            return IsDeleted;
        }

    }

}
