using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer
{
    public static class clsApplicationsTypeData
    {
        public static DataTable GetAllApplicationsTypes()
        {
            DataTable ApplicationsTypes = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "SELECT * FROM ApplicationTypes";

            SqlCommand command = new SqlCommand(Query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                    ApplicationsTypes.Load(reader);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                connection.Close();
            }

            return ApplicationsTypes;
        }

        public static bool GetApplicationTypeByID(int ApplicationTypeID, ref string ApplicationTypeTitle, ref int ApplicationFees)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "SELECT * From ApplicationTypes WHERE ApplicationTypeID = @ApplicationTypeID";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    ApplicationTypeTitle = (string)reader["ApplicationTypeTitle"];
                    ApplicationFees = (int)reader["ApplicationFees"];

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

        public static bool GetApplicationByApplicationTitle(string ApplicationTypeTitle, ref int ApplicationTypeID, ref int ApplicationFees)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "SELECT * From ApplicationTypes WHERE ApplicationTypetitle = @ApplicationTypeTitle";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ApplicationTypeTitle", ApplicationTypeTitle);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    ApplicationTypeID = (int)reader["ApplicationTypeID"];
                    ApplicationFees = (int)reader["ApplicationFees"];

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

        public static bool UpdateApplicationFees(int ApplicationTypeID, int ApplicationFees)
        {
            bool IsUpdated = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "UPDATE ApplicationsType SET ApplicationFess = @ApplicationFees WHERE ApplicationTypeID = @ApplicationTypeID";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ApplicationFees", ApplicationFees);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

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

}

}
