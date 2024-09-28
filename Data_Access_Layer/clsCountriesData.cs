using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer
{
    public static class clsCountriesData
    {
        public static DataTable GetAllCountries()
        {
            DataTable Countries = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "SELECT * FROM Countries";

            SqlCommand command = new SqlCommand(Query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    Countries.Load(reader);
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

            return Countries;
        }

        public static bool GetCountryByID(int CountryID, ref string CountryName)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "SELECT * FROM Countries WHERE CountryID = @CountryID";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@CountryID", CountryID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    CountryName = (string)reader["CountryName"];
                }
                else
                    IsFound = false;
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

        public static bool GetCountryIDByCountryName(string CountryName, ref int CountryID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "SELECT * FROM Countries WHERE CountryName = @CountryName";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@CountryName", CountryID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    CountryID = (int)reader["CountryID"];
                }
                else
                    IsFound = false;
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

    }
}
