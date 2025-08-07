using System;
using System.Data;
using System.Data.SqlClient;
namespace ContactsDataAccessLayer
{
    public class clsContactDataAccess
    {
        
        public static bool FindByContactID(int ContactID, ref string FirstName, ref string LastName, ref string Email
            , ref string Phone, ref string Address, ref string ImagePath, ref DateTime DateOfBirth, ref int CountryID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(cslDataAccessSettings.connectionString);
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

                    //ImageOath :allows null value so we should handle null
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
        public static int AddNewContact(string FirstName, string LastName, string Email
            , string Phone, string Address, string ImagePath, DateTime DateOfBirth, int CountryID)
        {
            int ContactID = -1;
            SqlConnection connection = new SqlConnection(cslDataAccessSettings.connectionString);
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

        public static bool UpdateContact(int ContactID, string FirstName, string LastName, string Email
                , string Phone, string Address, string ImagePath, DateTime DateOfBirth, int CountryID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(cslDataAccessSettings.connectionString);
            string Query = @"UPDATE Contacts
            SET FirstName = @FirstName,
            LastName = @LastName,
            Email = @Email,
            Phone = @Phone,
            Address = @Address,
            DateOfBirth = @DateOfBirth,
            CountryID = @CountryID,
            ImagePath = @ImagePath
            WHERE ContactID=@ContactID";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ContactID", ContactID);
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
                rowsAffected = command.ExecuteNonQuery();
            }
            catch
            {
                //Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

        public static bool DeleteContact(int ContactID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(cslDataAccessSettings.connectionString);
            string Query = @"DELETE Contacts
            
            WHERE ContactID=@ContactID";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ContactID", ContactID);


            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch
            {
                //Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }
    
    public static DataTable ListContacts()
        {
            DataTable dataTable = new DataTable();
            //List<stContact> ContactsList = new List<stContact>();
            
            SqlConnection connection = new SqlConnection(cslDataAccessSettings.connectionString);
            string Query = "SELECT * FROM Contacts";

            SqlCommand command = new SqlCommand(Query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if(reader.HasRows)
                {
                        //stContact contact = new stContact();
                     

                        //contact.FirstName = (string)reader["FirstName"];
                        //contact.LastName = (string)reader["LastName"];
                        //contact.Email = (string)reader["Email"];
                        //contact.Phone = (string)reader["Phone"];
                        //contact.Address = (string)reader["Address"];
                        //contact.DateOfBirth = (DateTime)reader["DateOfBirth"];
                        //contact.CountryID = (int)reader["CountryID"];

                        ////ImageOath :allows null value so we should handle null
                        //if (reader["ImagePath"] != DBNull.Value)
                        //{
                        //    contact.ImagePath = (string)reader["ImagePath"];
                        //}
                        //else
                        //{
                        //    contact.ImagePath = "";
                        //}
                        //ContactsList.Add(contact);
                        dataTable.Load(reader);
                }
                
                    reader.Close();
            }
            catch
            {
                //Console.WriteLine(ex.Message);
               
            }
            finally
            {
                connection.Close();
            }
            return dataTable;
        }
        public static bool IsContactExist(int ID)
        {
            SqlConnection connection = new SqlConnection(cslDataAccessSettings.connectionString);
            string Query = "SELECT 1 FROM Contacts WHERE ContactID = @ContactID";
            SqlCommand command= new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ContactID", ID);
            bool IsFound=false;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                IsFound = reader.HasRows;
                reader.Close();
            }
            catch
            {
                IsFound = false;
            }
            finally { 
                connection.Close();
            }
            return IsFound;
        }
        public static void testTiers()
        {
            Console.WriteLine("Go ");
        }
    }
    public class clsCountriesData
    {
        public static bool GetCountryInfoByID(int ID, ref string CountryName,
                                            ref string Code, ref string PhoneCode)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(cslDataAccessSettings.connectionString);

            string query = "SELECT * FROM Countries WHERE CountryID = @CountryID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@CountryID", ID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found
                    isFound = true;

                    CountryName = (string)reader["CountryName"];

                    if (reader["Code"] != DBNull.Value)
                    {
                        Code = (string)reader["Code"];
                    }
                    else
                    {
                        Code = "";
                    }

                    if (reader["PhoneCode"] != DBNull.Value)
                    {
                        PhoneCode = (string)reader["PhoneCode"];
                    }
                    else
                    {
                        PhoneCode = "";
                    }

                }
                else
                {
                    // The record was not found
                    isFound = false;
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
        public static int AddNewCountry(string CountryName, string Code, string PhoneCode)
        {
            //this function will return the new contact id if succeeded and -1 if not.
            int CountryID = -1;

            SqlConnection connection = new SqlConnection(cslDataAccessSettings.connectionString);

            string query = @"INSERT INTO Countries (CountryName,Code,PhoneCode)
                             VALUES (@CountryName,@Code,@PhoneCode);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@CountryName", CountryName);

            if (Code != "")
                command.Parameters.AddWithValue("@Code", Code);
            else
                command.Parameters.AddWithValue("@Code", System.DBNull.Value);

            if (PhoneCode != "")
                command.Parameters.AddWithValue("@PhoneCode", PhoneCode);
            else
                command.Parameters.AddWithValue("@PhoneCode", System.DBNull.Value);


            try
            {
                connection.Open();

                object result = command.ExecuteScalar();


                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    CountryID = insertedID;
                }
            }

            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);

            }

            finally
            {
                connection.Close();
            }


            return CountryID;
        }
        public static bool UpdateCountry(int ID, string CountryName, string Code, string PhoneCode)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(cslDataAccessSettings.connectionString);

            string query = @"Update  Countries  
                            set CountryName=@CountryName,
                                Code=@Code,
                                PhoneCode=@PhoneCode
                                where CountryID = @CountryID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@CountryID", ID);
            command.Parameters.AddWithValue("@CountryName", CountryName);
            command.Parameters.AddWithValue("@Code", Code);
            command.Parameters.AddWithValue("@PhoneCode", PhoneCode);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                return false;
            }

            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }
        public static bool GetCountryInfoByName(string CountryName, ref int ID,
                                                ref string Code, ref string PhoneCode)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(cslDataAccessSettings.connectionString);

            string query = "SELECT * FROM Countries WHERE CountryName = @CountryName";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@CountryName", CountryName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found
                    isFound = true;

                    ID = (int)reader["CountryID"];

                    if (reader["Code"] != DBNull.Value)
                    {
                        Code = (string)reader["Code"];
                    }
                    else
                    {
                        Code = "";
                    }

                    if (reader["PhoneCode"] != DBNull.Value)
                    {
                        PhoneCode = (string)reader["PhoneCode"];
                    }
                    else
                    {
                        PhoneCode = "";
                    }

                }
                else
                {
                    // The record was not found
                    isFound = false;
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
        public static bool IsCountryExistByName(string CountryName)
        {
            SqlConnection connection = new SqlConnection(cslDataAccessSettings.connectionString);
            string Query = @"SELECT * FROM Countries WHERE CountryName = @CountryName";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@CountryName", CountryName);
            bool isFound = false;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                isFound = reader.HasRows;
                reader.Close();
            }
            catch
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }
        public static DataTable ListCountries()
        {
            DataTable dataTable = new DataTable();
            //List<stContact> ContactsList = new List<stContact>();

            SqlConnection connection = new SqlConnection(cslDataAccessSettings.connectionString);
            string Query = "SELECT * FROM Countries";

            SqlCommand command = new SqlCommand(Query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    //stContact contact = new stContact();


                   
                    dataTable.Load(reader);
                }

                reader.Close();
            }
            catch
            {
                //Console.WriteLine(ex.Message);

            }
            finally
            {
                connection.Close();
            }
            return dataTable;
        }
        public static bool InsertIntoCountries()
        {
            SqlConnection connection = new SqlConnection(cslDataAccessSettings.connectionString);
            string Query = @"ALTER TABLE Countries ADD CityNumber int";
            SqlCommand command = new SqlCommand(Query, connection);


            try
            {
                connection.Open();
                command.ExecuteNonQuery();

            }
            catch
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
            return true;
        }

    }
}
