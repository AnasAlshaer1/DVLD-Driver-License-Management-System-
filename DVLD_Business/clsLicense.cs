using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;
namespace DVLD_Business
{
    public class clsLicense
    {

        public int LicenseID { get; private set; }
        public int ApplicationID { get; set; }

        public clsApplication ApplicationInfo { get; set; }
        public int DriverID { get; set; }
        public clsDriver DriverInfo { get; set; }
        public int LicenseClass { get; set; }

        public clsLicenseClass _LicenseClassInfo { get; set; }  
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Notes { get; set; }
        public decimal PaidFees { get; set; }
        public bool IsActive { get; set; }
        public byte IssueReason { get; set; }
        public int CreatedByUserID { get; set; }

        private enum enMode { AddNew, Update }
        private enMode _Mode = enMode.AddNew;

        public clsLicense()
        {
            LicenseID = -1;
            ApplicationID = -1;
            DriverID = -1;
            LicenseClass = -1;
            IssueDate = DateTime.Now;
            ExpirationDate = DateTime.Now;
            Notes = "";
            PaidFees = 0;
            IsActive = true;
            IssueReason = 1;
            CreatedByUserID = -1;

            _Mode = enMode.AddNew;
        }

        private clsLicense(int licenseID, int applicationID, int driverID, int licenseClass,
            DateTime issueDate, DateTime expirationDate, string notes, decimal paidFees,
            bool isActive, byte issueReason, int createdByUserID)
        {
            this.LicenseID = licenseID;
            this.ApplicationID = applicationID;
            this.ApplicationInfo=clsApplication.Find(applicationID);
            this.DriverID = driverID;
            this.DriverInfo = clsDriver.FindByDriverID(driverID);
            this.LicenseClass = licenseClass;
            this._LicenseClassInfo=clsLicenseClass.Find(licenseClass);
            this.IssueDate = issueDate;
            this.ExpirationDate = expirationDate;
            this.Notes = notes;
            this.PaidFees = paidFees;
            this.IsActive = isActive;
            this.IssueReason = issueReason;
            this.CreatedByUserID = createdByUserID;

            _Mode = enMode.Update;
        }

        public static clsLicense FindLicenseByLicenseID(int LicenseID)
        {
            int AppID = -1, DriverID = -1, Class = -1;
            DateTime Issue, Expire;
            string Notes = "";
            decimal Fees = 0;
            bool Active = false;
            byte Reason = 0;
            int CreatedByID = -1;

            Issue = Expire = DateTime.Now;

            if (clsLicenseDataAccess.GetLicenseByLicenseID(LicenseID, ref AppID, ref DriverID,
                ref Class, ref Issue, ref Expire, ref Notes, ref Fees, ref Active,
                ref Reason, ref CreatedByID))
            {
                return new clsLicense(LicenseID, AppID, DriverID, Class, Issue, Expire,
                                      Notes, Fees, Active, Reason, CreatedByID);
            }
            else
                return null;
        }

        public static clsLicense FindLicenseByApplicationID(int ApplicationID)
        {
            int LicenseID = -1, DriverID = -1, Class = -1;
            DateTime Issue, Expire;
            string Notes = "";
            decimal Fees = 0;
            bool Active = false;
            byte Reason = 0;
            int CreatedByID = -1;

            Issue = Expire = DateTime.Now;

            if (clsLicenseDataAccess.GetLicenseByApplicationID(ref LicenseID, ApplicationID, ref DriverID,
                ref Class, ref Issue, ref Expire, ref Notes, ref Fees, ref Active,
                ref Reason, ref CreatedByID))
            {
                return new clsLicense(LicenseID, ApplicationID, DriverID, Class, Issue, Expire,
                                      Notes, Fees, Active, Reason, CreatedByID);
            }
            else
                return null;
        }


        private bool _AddNewLicense()
        {
            this.LicenseID = clsLicenseDataAccess.AddNewLicense(
                this.ApplicationID, this.DriverID, this.LicenseClass,
                this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees,
                this.IsActive, this.IssueReason, this.CreatedByUserID);

            return (this.LicenseID != -1);
        }

        private bool _UpdateLicense()
        {
            return clsLicenseDataAccess.UpdateLicense(
                this.LicenseID, this.ApplicationID, this.DriverID, this.LicenseClass,
                this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees,
                this.IsActive, this.IssueReason, this.CreatedByUserID);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLicense())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _UpdateLicense();
            }

            return false;
        }

        public static bool DeleteLicense(int LicenseID)
        {
            return clsLicenseDataAccess.DeleteLicense(LicenseID);
        }

        public static DataTable GetAllLicenses()
        {
            return clsLicenseDataAccess.GetAllLicenses();
        }
    }
}
