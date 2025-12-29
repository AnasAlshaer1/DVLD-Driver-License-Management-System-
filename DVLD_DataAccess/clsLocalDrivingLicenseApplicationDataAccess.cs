using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
namespace DVLD_DataAccess
{
    public class clsLocalDrivingLicenseApplicationDataAccess
    {
        public static bool GetLocalDrivingLicenseApplicationByLDLAppID(int ID,
        ref int ApplicationID, ref int LicenseClassID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "SELECT * FROM LocalDrivingLicenseApplications WHERE LocalDrivingLicenseApplicationID=@ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", ID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;
                    ApplicationID = (int)reader["ApplicationID"];
                    LicenseClassID = (int)reader["LicenseClassID"];
                }
            }
            catch (Exception ex)
            {
                // Handle exception if needed
            }
            finally
            {
                connection.Close();
            }

            return IsFound;
        }


        public static bool GetLocalDrivingLicenseApplicationByApplicaitonID(ref int LDLAppID,
         int ID, ref int LicenseClassID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "SELECT * FROM LocalDrivingLicenseApplications WHERE ApplicationID=@ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", ID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;
                    LDLAppID = (int)reader["LocalDrivingLicenseApplicationID"];
                    LicenseClassID = (int)reader["LicenseClassID"];
                }
            }
            catch (Exception ex)
            {
                // Handle exception if needed
            }
            finally
            {
                connection.Close();
            }

            return IsFound;
        }

        public static int AddNewLocalDrivingLicenseApplication(int ApplicationID, int LicenseClassID)
        {
            int LocalDrivingLicenseApplicationID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"
            INSERT INTO LocalDrivingLicenseApplications (ApplicationID, LicenseClassID)
            VALUES (@ApplicationID, @LicenseClassID);
            SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    LocalDrivingLicenseApplicationID = insertedID;
            }
            catch (Exception ex)
            {
                // Handle exception
            }
            finally
            {
                connection.Close();
            }

            return LocalDrivingLicenseApplicationID;
        }

        public static bool UpdateLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID, int ApplicationID, int LicenseClassID)
        {
            int RowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"
            UPDATE LocalDrivingLicenseApplications
            SET ApplicationID=@ApplicationID,
                LicenseClassID=@LicenseClassID
            WHERE LocalDrivingLicenseApplicationID=@ID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

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

        public static bool DeleteLocalDrivingLicenseApplication(int ID)
        {
            int RowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "DELETE FROM LocalDrivingLicenseApplications WHERE LocalDrivingLicenseApplicationID=@ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", ID);

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

        public static DataTable ListLocalDrivingLicenseApplicationsArranged()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            //string query = "SELECT * FROM LocalDrivingLicenseApplications";
            string query = "SELECT \r\n    L.LocalDrivingLicenseApplicationID,\r\n    LC.ClassName,\r\n    P.NationalNo,\r\n\r\n    LTRIM(\r\n        RTRIM(\r\n            P.FirstName + ' ' \r\n            + P.SecondName + ' ' \r\n            + ISNULL(P.ThirdName + ' ', '')  \r\n            + P.LastName\r\n        )\r\n    ) AS FullName,\r\n    A.ApplicationDate,\r\n    COUNT(CASE WHEN T.TestResult = 1 THEN 1 END) AS PassedTests,\r\n    CASE \r\n        WHEN A.ApplicationStatus = 1 THEN 'New'\r\n        WHEN A.ApplicationStatus = 2 THEN 'Canceled'\r\n        WHEN A.ApplicationStatus = 3 THEN 'Completed'\r\n    END AS Status\r\nFROM \r\n    LocalDrivingLicenseApplications AS L\r\nINNER JOIN \r\n    LicenseClasses AS LC ON L.LicenseClassID = LC.LicenseClassID\r\nINNER JOIN \r\n    Applications AS A ON L.ApplicationID = A.ApplicationID\r\nINNER JOIN \r\n    People AS P ON A.ApplicantPersonID = P.PersonID\r\nLEFT JOIN \r\n    TestAppointments AS TA ON L.LocalDrivingLicenseApplicationID = TA.LocalDrivingLicenseApplicationID\r\nLEFT JOIN \r\n    Tests AS T ON TA.TestAppointmentID = T.TestAppointmentID\r\nGROUP BY \r\n    L.LocalDrivingLicenseApplicationID,\r\n    LC.ClassName,\r\n    P.NationalNo,\r\n    P.FirstName,\r\n    P.SecondName,\r\n    P.ThirdName,\r\n    P.LastName,\r\n    A.ApplicationDate,\r\n    A.ApplicationStatus;";
            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                    dt.Load(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                // Handle exception
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public static DataTable ListLocalDrivingLicenseApplications()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            //string query = "SELECT * FROM LocalDrivingLicenseApplications";
            string query = "SELECT * from LocalDrivingLicenseApplications;";
            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                    dt.Load(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                // Handle exception
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public static DataTable GetDriverLicenses(int DriverID)
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"SELECT     
                           Licenses.LicenseID,
                           ApplicationID,
		                   LicenseClasses.ClassName, Licenses.IssueDate, 
		                   Licenses.ExpirationDate, Licenses.IsActive
                           FROM Licenses INNER JOIN
                                LicenseClasses ON Licenses.LicenseClass = LicenseClasses.LicenseClassID
                            where DriverID=@DriverID
                            Order By IsActive Desc, ExpirationDate Desc";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);

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
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return dt;

        }

        public static bool IsLocalDrivingLicenseApplicationExist(int ID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "SELECT 1 FROM LocalDrivingLicenseApplications WHERE LocalDrivingLicenseApplicationID=@ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", ID);

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
