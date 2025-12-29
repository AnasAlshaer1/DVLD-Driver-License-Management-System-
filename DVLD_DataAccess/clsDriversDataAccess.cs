using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
namespace DVLD_DataAccess
{
    public  class clsDriversDataAccess
    {

        public static DataTable ListDrivers()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);


            //string query = "SELECT distinct Drivers.DriverID, Drivers.PersonID, People.NationalNo, People.FirstName+' '+People.SecondName" +
            //    "+' '+People.ThirdName+' '+People.LastName as FullName, Drivers.CreatedDate as Date," +
            //    "\r\nActiveLicenses =count(CASE WHEN Licenses.IsActive = 1 THEN 1 END)\r\nFROM   People INNER JOIN\r\n         " +
            //    "    Drivers ON People.PersonID = Drivers.PersonID INNER JOIN\r\n             Licenses ON Drivers.DriverID = Licenses.DriverID\r\n\t\t\t " +
            //    "group by Drivers.DriverID, Drivers.PersonID, People.NationalNo, People.FirstName+' '+People.SecondName+' '" +
            //    "+People.ThirdName+' '+People.LastName , Drivers.CreatedDate ";

            string query = "SELECT DISTINCT \r\n    Drivers.DriverID, \r\n    Drivers.PersonID, \r\n    People.NationalNo,\r\n    People.FirstName + ' ' + People.SecondName + ' ' + COALESCE(People.ThirdName, '') + ' ' + People.LastName AS FullName,\r\n    Drivers.CreatedDate AS Date,\r\n    ActiveLicenses = COUNT(CASE WHEN Licenses.IsActive = 1 THEN 1 END)\r\nFROM People\r\nINNER JOIN Drivers \r\n    ON People.PersonID = Drivers.PersonID\r\nINNER JOIN Licenses \r\n    ON Drivers.DriverID = Licenses.DriverID\r\nGROUP BY \r\n    Drivers.DriverID, \r\n    Drivers.PersonID, \r\n    People.NationalNo,\r\n    People.FirstName,\r\n    People.SecondName,\r\n    COALESCE(People.ThirdName, ''),\r\n    People.LastName,\r\n    Drivers.CreatedDate;\r\n ";

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

        public static bool GetDriverByID(int DriverID, ref int PersonID,
        ref int CreatedByUserID, ref DateTime CreatedDate)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"SELECT * FROM Drivers WHERE DriverID = @DriverID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    PersonID = (int)reader["PersonID"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    CreatedDate = (DateTime)reader["CreatedDate"];
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving driver.", ex);
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static int AddNewDriver(int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            int NewDriverID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"
            INSERT INTO Drivers (PersonID, CreatedByUserID, CreatedDate)
            VALUES (@PersonID, @CreatedByUserID, @CreatedDate);
            SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@CreatedDate", CreatedDate);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    NewDriverID = insertedID;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding driver.", ex);
            }
            finally
            {
                connection.Close();
            }

            return NewDriverID;
        }

        public static bool UpdateDriver(int DriverID, int PersonID,
            int CreatedByUserID, DateTime CreatedDate)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"
            UPDATE Drivers SET
                PersonID = @PersonID,
                CreatedByUserID = @CreatedByUserID,
                CreatedDate = @CreatedDate
            WHERE DriverID = @DriverID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@CreatedDate", CreatedDate);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating driver.", ex);
            }
            finally
            {
                connection.Close();
            }

            return rowsAffected > 0;
        }

        public static bool DeleteDriver(int DriverID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"DELETE FROM Drivers WHERE DriverID = @DriverID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting driver.", ex);
            }
            finally
            {
                connection.Close();
            }

            return rowsAffected > 0;
        }

        public static DataTable ListDriversTheNewOne()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"SELECT * FROM Drivers";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                dt.Load(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error listing drivers.", ex);
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public static bool IsDriverExist(int DriverID)
        {
            bool exists = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"SELECT 1 FROM Drivers WHERE DriverID=@DriverID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                exists = reader.HasRows;
                reader.Close();
            }
            catch
            {
                return false;
            }
            finally
            {
                connection.Close();
            }

            return exists;
        }

        public static bool GetDriverInfoByPersonID(ref int DriverID,  int PersonID,
          ref int CreatedByUserID, ref DateTime CreatedDate)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "SELECT * FROM Drivers WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found
                    isFound = true;

                    DriverID = (int)reader["DriverID"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    CreatedDate = (DateTime)reader["CreatedDate"];

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

    }
}
