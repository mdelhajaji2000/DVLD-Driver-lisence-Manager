using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer
{
    public static class clsPeopleData
    {
        public static DataTable GetAllPeople()
        {
            DataTable people = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "Select * From People";

            SqlCommand command = new SqlCommand(Query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    people.Load(reader);
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

            return people;
        }

        public static bool GetPersonByID
            (
                int ID, ref int NationalNumber,ref string FirstName , ref string LastName, 
                ref DateTime DateOfBirth, ref char Gendor, ref string Address , ref string Email,
                ref string Phone, ref int CountryID, ref string ImagePath
            )
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "SELECT * FROM People Where PersonID = @PersonID";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@PersonID", ID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    NationalNumber = (int)reader["NationaNo"];
                    FirstName = (string)reader["FirstName"];
                    LastName = (string)reader["lastName"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gendor = (char)reader["Gendor"];
                    Address = (string)reader["Address"];
                    Email = (string)reader["Email"];
                    Phone = (string)reader["Phone"];
                    CountryID = (int)reader["CountryID"];
                    ImagePath = (string)reader["ImagePath"];

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

        public static bool GetPersonByNationalNumber
            (
                int NationalNumber, ref int ID, ref string FirstName, ref string LastName,
                ref DateTime DateOfBirth, ref char Gendor, ref string Address, ref string Email,
                ref string Phone, ref int CountryID, ref string ImagePath
            )
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "SELECT * FROM People Where NationalNo = @NationalNo";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@NationaNo", NationalNumber);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    ID = (int)reader["PersonID"];
                    FirstName = (string)reader["FirstName"];
                    LastName = (string)reader["lastName"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gendor = (char)reader["Gendor"];
                    Address = (string)reader["Address"];
                    Email = (string)reader["Email"];
                    Phone = (string)reader["Phone"];
                    CountryID = (int)reader["CountryID"];
                    ImagePath = (string)reader["ImagePath"];

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

        public static int CreateNewPerson
            (
                int NationalNumber, string FirstName, string LastName,
                DateTime DateOfBirth, char Gendor, string Address, string Email,
                string Phone, int CountryID, string ImagePath
            )
        {
            int ID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "INSERT INTO People (NationalNo, FirstName, LastName, DateOfBirth, Gendor, Address, Email" +
                " Phone, CountryID, ImagePath) " +
                "Values " +
                " (@NationalNo, @FirstName, @LastName, @DateOfBrirth, @Gendor, @Address, @Email, @Phone, @CountryID, @ImagePath);" +
                "  Select SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNumber);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gendor", Gendor);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@CountryID", CountryID);
            command.Parameters.AddWithValue("@ImagePath", ImagePath);

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

        public static bool UpdatePerson
            (
                 int ID, int NationalNumber, string FirstName, string LastName,
                DateTime DateOfBirth, char Gendor, string Address, string Email,
                string Phone, int CountryID, string ImagePath
            )
        {
            bool IsUpdated = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "Update People Set " +
                "NationaNo = @NationbalNo, " +
                "FirstName = @FirstName, " +
                "LastName = @LastName, " +
                "DateOfBrith = @DateOfBrith, " +
                "Gendor = @Gendor, " +
                "Address = @Gendor," +
                "Email = @Email, " +
                "Phone = @Phone, " +
                "CountryID = @CountryID, " +
                "ImagePath = @ImagePath" +
                " WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNumber);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gendor", Gendor);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@CountryID", CountryID);
            command.Parameters.AddWithValue("@ImagePath", ImagePath);
            command.Parameters.AddWithValue("@PersonID", ID);

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

        public static bool DeletePerson(int PersonID)
        {
            bool Isdeleted = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "DELETE FROM People WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                int AffectedRows = command.ExecuteNonQuery();

                Isdeleted = AffectedRows > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                connection.Close();
            }
            return Isdeleted;
        }
        

    }
}
