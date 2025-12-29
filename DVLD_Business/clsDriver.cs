using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;
namespace DVLD_Business
{
    public class clsDriver
    {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int DriverID { get; set; }
        public int PersonID { get; set; }
        public int CreatedByUserID { get; set; }
        public DateTime CreatedDate { get; set; }

        public clsDriver()
        {
            DriverID = -1;
            PersonID = -1;
            CreatedByUserID = -1;
            CreatedDate = DateTime.Now;

            Mode = enMode.AddNew;
        }

        private clsDriver(int driverID, int personID, int createdByUserID, DateTime createdDate)
        {
            DriverID = driverID;
            PersonID = personID;
            CreatedByUserID = createdByUserID;
            CreatedDate = createdDate;

            Mode = enMode.Update;
        }



        public static DataTable GetAllDrivers()
        {
            return clsDriversDataAccess.ListDrivers();
        }
        public static clsDriver FindByDriverID(int DriverID)
        {
            int PersonID = -1, CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.Now;

            if (clsDriversDataAccess.GetDriverByID(DriverID, ref PersonID, ref CreatedByUserID, ref CreatedDate))
            {
                return new clsDriver(DriverID, PersonID, CreatedByUserID, CreatedDate);
            }
            else
            {
                return null;
            }
        }

        public static clsDriver FindByPersonID(int PersonID)
        {

            int DriverID = -1; int CreatedByUserID = -1; DateTime CreatedDate = DateTime.Now;

            if (clsDriversDataAccess.GetDriverInfoByPersonID(ref DriverID, PersonID, ref CreatedByUserID, ref CreatedDate))

                return new clsDriver(DriverID, PersonID, CreatedByUserID, CreatedDate);
            else
                return null;

        }
        private bool _AddNewDriver()
        {
            this.DriverID = clsDriversDataAccess.AddNewDriver(
                this.PersonID, this.CreatedByUserID, this.CreatedDate);

            return this.DriverID != -1;
        }

        private bool _UpdateDriver()
        {
            return clsDriversDataAccess.UpdateDriver(
                this.DriverID, this.PersonID, this.CreatedByUserID, this.CreatedDate);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDriver())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _UpdateDriver();
            }

            return false;
        }

        public static bool DeleteDriver(int DriverID)
        {
            return clsDriversDataAccess.DeleteDriver(DriverID);
        }

        public static DataTable GetAllDriversTheNewOne()
        {
            return clsDriversDataAccess.ListDriversTheNewOne();
        }

        public static bool IsDriverExist(int DriverID)
        {
            return clsDriversDataAccess.IsDriverExist(DriverID);
        }

    }
}
