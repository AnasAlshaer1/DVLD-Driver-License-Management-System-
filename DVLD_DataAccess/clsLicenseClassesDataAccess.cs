using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
namespace DVLD_DataAccess
{
    public class clsLicenseClassesDataAccess
    {
        public static bool GetLicenseClassByID(int ID, ref string ClassName,
       ref string ClassDescription, ref int MinimumAllowedAge,
       ref int DefaultValidityLength, ref decimal ClassFees)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "SELECT * FROM LicenseClasses WHERE LicenseClassID=@ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", ID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;
                    ClassName = (string)reader["ClassName"];
                    ClassDescription = (string)reader["ClassDescription"];
                    MinimumAllowedAge = (byte)reader["MinimumAllowedAge"];
                    DefaultValidityLength = (byte)reader["DefaultValidityLength"];
                    ClassFees = (decimal)reader["ClassFees"];
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


        //public static bool UpdateLicenseClass(int LicenseClassID, string ClassName,
        //string ClassDescription, int MinimumAllowedAge,
        //int DefaultValidityLength, decimal ClassFees)
        //{
        //    int RowsAffected = 0;
        //    SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

        //    string query = @"
        //    UPDATE LicenseClasses
        //    SET ClassName=@ClassName,
        //        ClassDescription=@ClassDescription,
        //        MinimumAllowedAge=@MinimumAllowedAge,
        //        DefaultValidityLength=@DefaultValidityLength,
        //        ClassFees=@ClassFees
        //    WHERE LicenseClassID=@LicenseClassID";

        //    SqlCommand command = new SqlCommand(query, connection);
        //    command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
        //    command.Parameters.AddWithValue("@ClassName", ClassName);
        //    command.Parameters.AddWithValue("@ClassDescription", ClassDescription);
        //    command.Parameters.AddWithValue("@MinimumAllowedAge", MinimumAllowedAge);
        //    command.Parameters.AddWithValue("@DefaultValidityLength", DefaultValidityLength);
        //    command.Parameters.AddWithValue("@ClassFees", ClassFees);

        //    try
        //    {
        //        connection.Open();
        //        RowsAffected = command.ExecuteNonQuery();
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //    finally
        //    {
        //        connection.Close();
        //    }

        //    return (RowsAffected > 0);
        //}

        //public static bool DeleteLicenseClass(int LicenseClassID)
        //{
        //    int RowsAffected = 0;
        //    SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

        //    string query = "DELETE FROM LicenseClasses WHERE LicenseClassID=@LicenseClassID";
        //    SqlCommand command = new SqlCommand(query, connection);
        //    command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

        //    try
        //    {
        //        connection.Open();
        //        RowsAffected = command.ExecuteNonQuery();
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //    finally
        //    {
        //        connection.Close();
        //    }

        //    return (RowsAffected > 0);
        //}

        public static DataTable ListLicenseClasses()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "SELECT * FROM LicenseClasses";
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

        public static bool IsLicenseClassExist(int LicenseClassID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "SELECT 1 FROM LicenseClasses WHERE LicenseClassID=@LicenseClassID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

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
