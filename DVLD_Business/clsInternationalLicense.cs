using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;
namespace DVLD_Business
{
    public class clsInternationalLicense
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int InternationalLicenseID { get; set; }
        public int ApplicationID { get; set; }

        public clsApplication _ApplicationInfo;
        public int DriverID { get; set; }

        public clsDriver _DriverInfo;
        public int IssuedUsingLocalLicenseID { get; set; }

        public clsLicense _LocalLicenseInfo { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }
        public int CreatedByUserID { get; set; }

        // Default Constructor – Add New
        public clsInternationalLicense()
        {
            InternationalLicenseID = -1;
            ApplicationID = -1;
            DriverID = -1;
            IssuedUsingLocalLicenseID = -1;
            IssueDate = DateTime.Now;
            ExpirationDate = DateTime.Now.AddYears(1);
            IsActive = true;
            CreatedByUserID = -1;

            Mode = enMode.AddNew;
        }

        // Private Constructor – For Find
        private clsInternationalLicense(
            int internationalLicenseID, int applicationID,
            int driverID, int issuedUsingLocalLicenseID,
            DateTime issueDate, DateTime expirationDate,
            bool isActive, int createdByUserID)
        {
            InternationalLicenseID = internationalLicenseID;
            ApplicationID = applicationID;
            _ApplicationInfo=clsApplication.Find(applicationID);
            DriverID = driverID;
            _DriverInfo=clsDriver.FindByDriverID(driverID);
            IssuedUsingLocalLicenseID = issuedUsingLocalLicenseID;
            _LocalLicenseInfo = clsLicense.FindLicenseByLicenseID(issuedUsingLocalLicenseID);
            IssueDate = issueDate;
            ExpirationDate = expirationDate;
            IsActive = isActive;
            CreatedByUserID = createdByUserID;

            Mode = enMode.Update;
        }

        // ===============================  
        //          FIND  
        // ===============================
        public static clsInternationalLicense Find(int InternationalLicenseID)
        {
            int applicationID = -1, driverID = -1, issuedLocal = -1;
            int createdBy = -1;
            bool isActive = false;
            DateTime issueDate = DateTime.Now, expirationDate = DateTime.Now;

            bool isFound = clsInternationalLicenseDataAccess.GetInternationalLicenseInfoByID(
                InternationalLicenseID,
                ref applicationID,
                ref driverID,
                ref issuedLocal,
                ref issueDate,
                ref expirationDate,
                ref isActive,
                ref createdBy
            );

            if (isFound)
            {
                return new clsInternationalLicense(
                    InternationalLicenseID, applicationID, driverID,
                    issuedLocal, issueDate, expirationDate,
                    isActive, createdBy
                );
            }
            else
                return null;
        }

        // ===============================  
        //        LIST / TABLES  
        // ===============================  
        public static DataTable GetAllInternationalLicenses()
        {
            return clsInternationalLicenseDataAccess.GetAllInternationalLicenses();
        }

        public static DataTable GetDriverInternationalLicenses(int DriverID)
        {
            return clsInternationalLicenseDataAccess.GetDriverInternationalLicenses(DriverID);
        }

        public static int GetActiveInternationalLicenseIDByDriverID(int DriverID)
        {
            return clsInternationalLicenseDataAccess.GetActiveInternationalLicenseIDByDriverID(DriverID);
        }

        // ===============================  
        //       ADD & UPDATE  
        // ===============================  
        private bool _AddNewInternationalLicense()
        {
            this.InternationalLicenseID =
                clsInternationalLicenseDataAccess.AddNewInternationalLicense(
                    this.ApplicationID,
                    this.DriverID,
                    this.IssuedUsingLocalLicenseID,
                    this.IssueDate,
                    this.ExpirationDate,
                    this.IsActive,
                    this.CreatedByUserID
                );

            return (this.InternationalLicenseID != -1);
        }

        private bool _UpdateInternationalLicense()
        {
            return clsInternationalLicenseDataAccess.UpdateInternationalLicense(
                this.InternationalLicenseID,
                this.ApplicationID,
                this.DriverID,
                this.IssuedUsingLocalLicenseID,
                this.IssueDate,
                this.ExpirationDate,
                this.IsActive,
                this.CreatedByUserID
            );
        }

        // ===============================  
        //            SAVE  
        // ===============================  
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewInternationalLicense())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _UpdateInternationalLicense();
            }

            return false;
        }

       
    }
}
