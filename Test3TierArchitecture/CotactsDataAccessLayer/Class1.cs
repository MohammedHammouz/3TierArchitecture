using System;
using System.Data;

using System.Data.SqlClient;
namespace CotactsDataAccessLayer
{
    public class clsDataAccess
    {
        public static bool FindByContactID(int ContactID, ref string FirstName, ref string LastName, ref string Email
            , ref string Phone, ref string Address,ref string ImagePath, ref DateTime DateOfBirth, ref int CountryID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string Query = "SELECT * FROM Contacts WHERE ContactID=@ContactID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ContactID", ContactID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;

                    FirstName = (string)reader["FirstName"];
                    LastName = (string)reader["LastName"];
                    Email = (string)reader["Email"];
                    Phone = (string)reader["Phone"];
                    Address = (string)reader["Address"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    CountryID = (int)reader["CountryID"];
                    if (reader["ImagePath"] != DBNull.Value)
                    {
                        ImagePath = (string)reader["ImagePath"];
                    }
                    else
                    {
                        ImagePath = "";
                    }
                }
                else
                {
                    isFound = false;
                }
                connection.Close();
                reader.Close();
            }
            catch 
            {
                //Console.WriteLine(ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }
        public static int AddNewContact(string FirstName,string LastName,string Email
            ,string Phone, string Address,DateTime DateOfBirth,int CountryID, string ImagePath)
        {
            int ContactID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string Query = @"INSERT INTO Contacts (FirstName, LastName, Email, Phone, Address, DateOfBirth, CountryID, ImagePath)
VALUES (@FirstName,@LastName,@Email,@Phone,@Address,@DateOfBirth,@CountryID,@ImagePath) SELECT SCOPE_IDENTITY()";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Address", Address);

            if (DateOfBirth != null)
            {
                command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            }
            else
            {
                command.Parameters.AddWithValue("@DateOfBirth", DateTime.Now);
            }
            command.Parameters.AddWithValue("@CountryID", CountryID);
            if (ImagePath != null)
            {
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            }
            else
            {
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);
            }

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int resultID))
                {
                    ContactID = resultID;
                    Console.WriteLine($"Newly inserted id: {resultID}");
                }
                else
                {
                    Console.WriteLine("Failed to insert newid");
                }
            }
            catch
            {
                //Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return ContactID;
        }
        public static bool UpdateContact(int ContactID, string FirstName, string LastName, 
            string Email, string Phone, string Address,
            string ImagePath, DateTime DateOfBirth, int CountryID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string Query = @"INSERT INTO Contacts (FirstName, LastName, Email, Phone, Address, DateOfBirth, CountryID, ImagePath)
VALUES (@FirstName,@LastName,@Email,@Phone,@Address,@DateOfBirth,@CountryID,@ImagePath) SELECT SCOPE_IDENTITY()";
            SqlCommand command=new SqlCommand(Query, connection);
            int rowsAffected = 0;
            try
            {
                connection.Open();
                rowsAffected= command.ExecuteNonQuery();
            }
            catch { 
            
            }
            finally { 
                connection.Close(); 
            }
            return rowsAffected > 0;
        }
        public static bool DeleteContact(int ContactID) {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string Query = @"DELETE Contacts
            WHERE ContactID=@ContactID";
            SqlCommand command=new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ContactID", ContactID);
            int rowsAffected = 0;
            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch
            {
            }
            finally { 
                connection.Close();
            }
            return rowsAffected > 0;
        }
        public static DataTable GetListContacts() {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string Query = "SELECT * FROM Contacts";
            SqlCommand command= new SqlCommand(Query, connection);
            DataTable dataTable = new DataTable();

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dataTable.Load(reader);
                }
                reader.Close();
            }
            catch
            {

            }
            finally {
                connection.Close();
            }
            return dataTable;
        }
    }

}
