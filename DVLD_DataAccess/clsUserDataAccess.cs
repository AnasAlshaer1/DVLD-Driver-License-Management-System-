using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DVLD_DataAccess
{
    public class clsUserDataAccess
    {
        public static bool GetUserInfoByUserID(int ID, ref int PersonID, ref string Username
          , ref string Password,ref bool IsActive)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);


            string query = "SELECT * FROM Users WHERE UserID=@UserID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", ID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;
                    PersonID = (int)reader["PersonID"];
                    Username = (string)reader["Username"];
                    Password = (string)reader["Password"];
                    IsActive = (bool)reader["IsActive"];
               

                  


                }
                else
                {
                    IsFound = false;
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return IsFound;
        }

        public static bool GetUserInfoByPersonID(ref int ID,  int PersonID, ref string Username
        , ref string Password, ref bool IsActive)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);


            string query = "SELECT * FROM Users WHERE PersonID=@PersonID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;
                    ID = (int)reader["UserID"];
                    Username = (string)reader["Username"];
                    Password = (string)reader["Password"];
                    IsActive = (bool)reader["IsActive"];





                }
                else
                {
                    IsFound = false;
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return IsFound;
        }
        public static bool GetUserInfoByUsernameAndPassword(ref int ID, ref int PersonID,  string Username
  ,  string Password, ref bool IsActive)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);


            string query = "SELECT * FROM Users WHERE Username=@Username and Password=@Password";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Username",Username);
            command.Parameters.AddWithValue("@Password", Password);


            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;
                    PersonID = (int)reader["PersonID"];
                    ID=(int)reader["UserID"];
                    IsActive = (bool)reader["IsActive"];





                }
                else
                {
                    IsFound = false;
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return IsFound;
        }

        public static int AddNewUser( int PersonID,  string Username
          ,  string Password, bool IsActive)
        {

            int UserID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"INSERT INTO Users (PersonID,Username,Password,IsActive)
                VALUES (@PersonID,@Username,@Password,@IsActive);
                 SELECT SCOPE_IDENTITY();";



            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@Username", Username);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);
       

            

            try
            {
                connection.Open();
                object Result = command.ExecuteScalar();

                if (Result != null && int.TryParse(Result.ToString(), out int insertedID))
                {
                    UserID = insertedID;
                }
                else
                {
                    UserID = -1;
                }



            }
            catch (Exception ex)
            {

                connection.Close();
            }
            finally
            {
                connection.Close();
            }


            return UserID;
        }


        public static bool UpdateUser(int ID,int PersonID, string Username
          , string Password, bool IsActive)
        {
            int RowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"UPDATE Users 
                            set 
                                PersonID=@PersonID,
                                Username=@Username,
                                Password=@Password,
                                IsActive=@IsActive
                                WHERE UserID=@UserID";



            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", ID);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@Username", Username);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);


            try
            {
                connection.Open();
                RowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

                return false;
            }
            finally
            {
                connection.Close();
            }

            return (RowsAffected > 0);



        }


        public static bool DeleteUser(int ID)
        {
            int RowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"DELETE Users 
                                WHERE UserID=@UserID";



            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", ID);


            try
            {
                connection.Open();
                RowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

                return false;
            }
            finally
            {
                connection.Close();
            }

            return (RowsAffected > 0);



        }


        public static DataTable ListUsers()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            //string Query = "SELECT * FROM People";
            string query = "SELECT \r\n    Users.UserID, \r\n    " +
                "Users.PersonID, \r\n    FullName = CASE \r\n                 " +
                " WHEN People.ThirdName IS NOT NULL \r\n                     " +
                "  THEN People.FirstName + ' ' + People.SecondName + ' ' + People.ThirdName + ' ' + People.LastName\r\n      " +
                "            ELSE People.FirstName + ' ' + People.SecondName + ' ' + People.LastName\r\n               END,\r\n    Users.UserName,\r\n   " +
                " Users.IsActive\r\nFROM Users\r\nINNER JOIN People ON Users.PersonID = People.PersonID;";

            SqlCommand command = new SqlCommand(query, connection);


            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();


            }
            catch (Exception ex)
            {

            }
            finally
            {

                connection.Close();

            }
            return dt;
        }


        public static bool IsUserExist(int ID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string Query = "SELECT Found=1 FROM Users WHERE UserID=@UserID";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@UserID", ID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                IsFound = reader.HasRows;
                reader.Close();


            }
            catch (Exception ex)
            {
                return IsFound;
            }
            finally
            {

                connection.Close();

            }
            return IsFound;
        }

        public static bool IsUserWithThisPersonIDExist(int ID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string Query = "SELECT Found=1 FROM Users WHERE PersonID=@PersonID";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@PersonID", ID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                IsFound = reader.HasRows;
                reader.Close();


            }
            catch (Exception ex)
            {
                return IsFound;
            }
            finally
            {

                connection.Close();

            }
            return IsFound;
        }

    }
}
