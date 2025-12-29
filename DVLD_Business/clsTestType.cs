using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;
namespace DVLD_Business
{
    public class clsTestType
    {
        public int TestTypeID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
        public decimal Fees { get; set; }


        public clsTestType()
        {
            this.TestTypeID = -1;
            this.Description = "";
            this.Title = "";
            this.Fees = -1;





        }
        private clsTestType(int ApplicationTypeID,string Description, string Title
            , decimal Fees)
        {
            this.TestTypeID = ApplicationTypeID;
            this.Title = Title;
            this.Fees = Fees;
            this.Description= Description;
           


        }

        public static clsTestType Find(int ApplicationTypeID)
        {
            string Title = "",description="";
            decimal Fees = -1;

            if (clsTestTypesDataAccess.GetTestTypeInfoByID(ApplicationTypeID,ref description ,ref Title, ref Fees))
            {
                return new clsTestType(ApplicationTypeID,description ,Title, Fees);
            }
            else
            {
                return null;
            }

        }

        private bool _UpdateTestType()
        {

            return (clsTestTypesDataAccess.UpdateTestType(this.TestTypeID,this.Description,
                this.Title, this.Fees));

        }




        public static DataTable GetAllTestTypes()
        {
            return clsTestTypesDataAccess.GetAllTestTypes();
        }


        public bool Save()
        {

            return (_UpdateTestType());

        }





    }
}
