using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;
namespace DVLD_Business
{
    public class clsLocalDrivingLicenseApplication
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode Mode = enMode.AddNew;

        public int LocalDrivingLicenseApplicationID { get; set; }
        public int ApplicationID { get; set; }

        public clsApplication ApplicationInfo { get; set; }
        public int LicenseClassID { get; set; }

        public clsLicenseClass LicenseClassInfo { get; set; }

        // Default constructor = new record
        public clsLocalDrivingLicenseApplication()
        {
            this.LocalDrivingLicenseApplicationID = -1;
            this.ApplicationID = -1;
            this.LicenseClassID = -1;

            Mode = enMode.AddNew;
        }

        // Private constructor = existing record
        private clsLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID, int ApplicationID, int LicenseClassID)
        {
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.ApplicationID = ApplicationID;
            this.ApplicationInfo=clsApplication.Find(ApplicationID);
            this.LicenseClassID = LicenseClassID;
            this.LicenseClassInfo = clsLicenseClass.Find(LicenseClassID);

            Mode = enMode.Update;
        }

        // ===== Find by ID =====
        public static clsLocalDrivingLicenseApplication FindLDLAppByLDLAppID(int ID)
        {
            int ApplicationID = -1, LicenseClassID = -1;

            if (clsLocalDrivingLicenseApplicationDataAccess.GetLocalDrivingLicenseApplicationByLDLAppID(ID,
                ref ApplicationID, ref LicenseClassID))
            {
                return new clsLocalDrivingLicenseApplication(ID, ApplicationID, LicenseClassID);
            }
            else
            {
                return null;
            }
        }


        public static clsLocalDrivingLicenseApplication FindLDLAppByApplicationID(int ApplicationID)
        {
            int LDLAppID = -1, LicenseClassID = -1;

            if (clsLocalDrivingLicenseApplicationDataAccess.GetLocalDrivingLicenseApplicationByApplicaitonID(ref LDLAppID,
                 ApplicationID, ref LicenseClassID))
            {
                return new clsLocalDrivingLicenseApplication(LDLAppID, ApplicationID, LicenseClassID);
            }
            else
            {
                return null;
            }
        }


        // ===== Add New =====
        private bool _AddNewLocalDrivingLicenseApplication()
        {
            this.LocalDrivingLicenseApplicationID =
                clsLocalDrivingLicenseApplicationDataAccess.AddNewLocalDrivingLicenseApplication(this.ApplicationID, this.LicenseClassID);

            return (this.LocalDrivingLicenseApplicationID != -1);
        }

        // ===== Update =====
        private bool _UpdateLocalDrivingLicenseApplication()
        {
            return clsLocalDrivingLicenseApplicationDataAccess.UpdateLocalDrivingLicenseApplication(
                this.LocalDrivingLicenseApplicationID, this.ApplicationID, this.LicenseClassID);
        }

        // ===== Delete =====
        public static bool DeleteLocalDrivingLicenseApplication(int ID)
        {
            return clsLocalDrivingLicenseApplicationDataAccess.DeleteLocalDrivingLicenseApplication(ID);
        }

        // ===== List All =====
        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            return clsLocalDrivingLicenseApplicationDataAccess.ListLocalDrivingLicenseApplicationsArranged();
        }

        // ===== Existence Check =====
        public static bool IsLocalDrivingLicenseApplicationExist(int ID)
        {
            return clsLocalDrivingLicenseApplicationDataAccess.IsLocalDrivingLicenseApplicationExist(ID);
        }

        // ===== Save =====
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLocalDrivingLicenseApplication())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateLocalDrivingLicenseApplication();
            }

            return false;
        }

        public static DataTable GetDriverLicenses(int DriverID)
        {
            return clsLocalDrivingLicenseApplicationDataAccess.GetDriverLicenses(DriverID);
        }

    }
}
