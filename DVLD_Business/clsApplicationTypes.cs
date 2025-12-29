using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class clsApplicationTypes
    {
        public int ApplicationTypeID { get; set; }
 
        public string Title { get; set; }
        public decimal Fees { get; set; }
       

        public clsApplicationTypes()
        {
            this.ApplicationTypeID = -1;
            this.Title = "";
            this.Fees = -1;
           



      
        }
        private clsApplicationTypes(int ApplicationTypeID,string Title
            , decimal Fees)
        {
            this.ApplicationTypeID = ApplicationTypeID;
            this.Title = Title;
            this.Fees = Fees;
           

        }

        public static clsApplicationTypes Find(int ApplicationTypeID)
        {
            string Title = "";
            decimal Fees = -1;

            if (clsApplicationTypesDataAccess.GetApplicationTypeInfoByID(ApplicationTypeID, ref Title,ref Fees))
            {
                return new clsApplicationTypes(ApplicationTypeID, Title,Fees);
            }
            else
            {
                return null;
            }

        }

        private bool _UpdateApplicationType()
        {

            return (clsApplicationTypesDataAccess.UpdateAppicationType(this.ApplicationTypeID,
                this.Title, this.Fees));

        }

     


        public static DataTable GetAllApplicationTypes()
        {
            return clsApplicationTypesDataAccess.GetAllApplicationTypes();
        }

       
        public bool Save()
        {

          return (_UpdateApplicationType());

        }





    }
}
