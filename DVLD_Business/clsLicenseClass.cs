using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;
namespace DVLD_Business
{
    public class clsLicenseClass
    {
        //public enum enMode { AddNew = 0, Update = 1 };
        //private enMode Mode = enMode.AddNew;

        public int LicenseClassID { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public int MinimumAllowedAge { get; set; }
        public int DefaultValidityLength { get; set; }
        public decimal ClassFees { get; set; }

        // Default constructor = new record
        public clsLicenseClass()
        {
            this.LicenseClassID = -1;
            this.ClassName = "";
            this.ClassDescription = "";
            this.MinimumAllowedAge = 0;
            this.DefaultValidityLength = 0;
            this.ClassFees = 0;

       
        }

        // Private constructor = existing record
        private clsLicenseClass(int LicenseClassID, string ClassName, string ClassDescription,
            int MinimumAllowedAge, int DefaultValidityLength, decimal ClassFees)
        {
            this.LicenseClassID = LicenseClassID;
            this.ClassName = ClassName;
            this.ClassDescription = ClassDescription;
            this.MinimumAllowedAge = MinimumAllowedAge;
            this.DefaultValidityLength = DefaultValidityLength;
            this.ClassFees = ClassFees;

            //Mode = enMode.Update;
        }

        // ===== Find by ID =====
        public static clsLicenseClass Find(int LicenseClassID)
        {
            string ClassName = "", ClassDescription = "";
            int MinimumAllowedAge = 0, DefaultValidityLength = 0;
            decimal ClassFees = 0;

            if (clsLicenseClassesDataAccess.GetLicenseClassByID(LicenseClassID,
                ref ClassName, ref ClassDescription, ref MinimumAllowedAge,
                ref DefaultValidityLength, ref ClassFees))
            {
                return new clsLicenseClass(LicenseClassID, ClassName, ClassDescription,
                    MinimumAllowedAge, DefaultValidityLength, ClassFees);
            }
            else
            {
                return null;
            }
        }

    

        // ===== Update =====
        //private bool _Update()
        //{
        //    return clsLicenseClassesDataAccess.UpdateLicenseClass(
        //        this.LicenseClassID, this.ClassName, this.ClassDescription,
        //        this.MinimumAllowedAge, this.DefaultValidityLength, this.ClassFees);
        //}

        // ===== Delete =====
        //public static bool Delete(int LicenseClassID)
        //{
        //    return clsLicenseClassesDataAccess.DeleteLicenseClass(LicenseClassID);
        //}

        // ===== List All =====
        public static DataTable GetAllLicneseClasses()
        {
            return clsLicenseClassesDataAccess.ListLicenseClasses();
        }

        // ===== Existence Check =====
        public static bool IsLicneseClassExist(int LicenseClassID)
        {
            return clsLicenseClassesDataAccess.IsLicenseClassExist(LicenseClassID);
        }

        // ===== Save =====
        //public bool Save()
        //{
        //    switch (Mode)
        //    {
        //        case enMode.AddNew:
        //            if (_AddNew())
        //            {
        //                Mode = enMode.Update;
        //                return true;
        //            }
        //            else
        //            {
        //                return false;
        //            }

        //        case enMode.Update:
        //            return _Update();
        //    }

        //    return false;
        //}
    }


}
