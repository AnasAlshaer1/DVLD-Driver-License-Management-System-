using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
namespace DVLD_DataAccess
{
    public class clsTestTypesDataAccess
    {
        public static bool GetTestTypeInfoByID(int ID,ref string description ,ref string Title, ref decimal Fees)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "SELECT * FROM TestTypes WHERE TestTypeID=@TestTypeID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeID", ID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;
                    Title = (string)reader["TestTypeTitle"];
                    Fees = (decimal)reader["TestTypeFees"];
                    description = (string)reader["TestTypeDescription"];
                }
                else
                {
                    IsFound = false;
                }

            }
            catch (Exception ex)
            {
                IsFound = false;
            }
            finally
            {
                connection.Close();
            }

            return IsFound;
        }//



        public static DataTable GetAllTestTypes()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

       
            string query = "SELECT TestTypeID as ID, TestTypeTitle AS Title, TestTypeDescription as Description, TestTypeFees as Fees\r\nFROM   TestTypes";

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




        public static bool UpdateTestType(int ID,string description ,string Title
        , decimal Fees)
        {
            int RowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"UPDATE TestTypes 
                            set 
                                TestTypeTitle=@Title,
                                TestTypeFees=@Fees,
                                TestTypeDescription=@Description
                                WHERE TestTypeID=@TestTypeID";



            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeID", ID);
            command.Parameters.AddWithValue("@Title", Title);
            command.Parameters.AddWithValue("@Fees", Fees);
            command.Parameters.AddWithValue("@Description", description);




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



    }
}
