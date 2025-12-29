using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
namespace DVLD_DataAccess
{
    public class clsTestsDataAccess
    {
        public static bool GetTestByID(int TestID, ref int TestAppointmentID, ref bool TestResult,
        ref string Notes, ref int CreatedByUserID)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                string query = @"SELECT [TestAppointmentID],[TestResult],[Notes],[CreatedByUserID] 
                             FROM Tests WHERE TestID = @TestID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TestID", TestID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        isFound = true;
                        TestAppointmentID = (int)reader["TestAppointmentID"];
                        TestResult =(bool) reader["TestResult"];
                        CreatedByUserID = (int)reader["CreatedByUserID"];

                        if (reader["Notes"] != DBNull.Value)
                        {
                            Notes = (string)reader["Notes"];
                        }
                        else
                        {
                            Notes = "";
                        }

                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error while fetching Test data", ex);
                }
                finally
                {
                    connection.Close();
                }
            }

            return isFound;
        }

        public static int AddNewTest(int TestAppointmentID, bool TestResult,
            string Notes, int CreatedByUserID)
        {
            int TestID = -1;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                string query = @"INSERT INTO Tests ([TestAppointmentID],[TestResult],[Notes],[CreatedByUserID])
                             VALUES (@TestAppointmentID,@TestResult,@Notes,@CreatedByUserID);
                             SELECT SCOPE_IDENTITY();";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
                command.Parameters.AddWithValue("@TestResult", TestResult);
                command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

                if (Notes != "")
                {
                    command.Parameters.AddWithValue("@Notes", Notes);
                }
                else
                {
                    command.Parameters.AddWithValue("@Notes", System.DBNull.Value);

                }

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int newID))
                    {
                        TestID = newID;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error while inserting new Test", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
            

            return TestID;
        }

        public static bool UpdateTest(int TestID, int TestAppointmentID, bool TestResult,
            string Notes, int CreatedByUserID)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                string query = @"UPDATE Tests 
                             SET [TestAppointmentID] = @TestAppointmentID,
                                 [TestResult] = @TestResult,
                                 [Notes] = @Notes,
                                 [CreatedByUserID] = @CreatedByUserID
                             WHERE TestID = @TestID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TestID", TestID);
                command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
                command.Parameters.AddWithValue("@TestResult", TestResult);
                command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

                if (Notes != "")
                {
                    command.Parameters.AddWithValue("@Notes", Notes);
                }
                else
                {
                    command.Parameters.AddWithValue("@Notes", System.DBNull.Value);

                }

                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error while updating Test", ex);
                }
                finally
                {
                    connection.Close();
                }
            }

            return (rowsAffected > 0);
        }

        public static bool DeleteTest(int TestID)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                string query = "DELETE FROM Tests WHERE TestID = @TestID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TestID", TestID);

                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error while deleting Test", ex);
                }
                finally
                {
                    connection.Close();
                }
            }

            return (rowsAffected > 0);
        }

        public static DataTable GetAllTests()
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                string query = "SELECT * FROM Tests";

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
                    throw new Exception("Error while fetching all Tests", ex);
                }
                finally
                {
                    connection.Close();
                }
            }

            return dt;
        }
    }
}
