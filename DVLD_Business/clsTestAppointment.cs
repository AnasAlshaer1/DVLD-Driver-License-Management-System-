using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;
namespace DVLD_Business
{
    public class clsTestAppointment
    {
        
            public enum enMode { AddNew = 0, Update = 1 };
            enMode Mode = enMode.AddNew;

            public int TestAppointmentID { get; set; }
            public int TestTypeID { get; set; }

            public clsTestType TestTypeInfo { get; set; }
            public int LocalDrivingLicenseApplicationID { get; set; }

            public clsLocalDrivingLicenseApplication LocalDrivingLicenseApplicationInfo { get; set; }    
            public DateTime AppointmentDate { get; set; }
            public decimal PaidFees { get; set; }
            public int CreatedByUserID { get; set; }
            public bool IsLocked { get; set; }
            public int? RetakeTestApplicationID { get; set; }

            public clsTestAppointment()
            {
                this.TestAppointmentID = -1;
                this.TestTypeID = -1;
                this.LocalDrivingLicenseApplicationID = -1;
                this.AppointmentDate = DateTime.Now;
                this.PaidFees = 0;
                this.CreatedByUserID = -1;
                this.IsLocked = false;
                this.RetakeTestApplicationID = null;

                Mode = enMode.AddNew;
            }

            private clsTestAppointment(int testAppointmentID, int testTypeID, int localDrivingLicenseApplicationID,
                DateTime appointmentDate, decimal paidFees, int createdByUserID,
                bool isLocked, int? retakeTestApplicationID)
            {
                this.TestAppointmentID = testAppointmentID;
                this.TestTypeID = testTypeID;
               this.TestTypeInfo=clsTestType.Find(testTypeID);
                this.LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            this.LocalDrivingLicenseApplicationInfo = clsLocalDrivingLicenseApplication.FindLDLAppByLDLAppID(localDrivingLicenseApplicationID);
                this.AppointmentDate = appointmentDate;
                this.PaidFees = paidFees;
                this.CreatedByUserID = createdByUserID;
                this.IsLocked = isLocked;
                this.RetakeTestApplicationID = retakeTestApplicationID;

                Mode = enMode.Update;
            }

            public static clsTestAppointment Find(int testAppointmentID)
            {
                int testTypeID = -1, localDrivingLicenseApplicationID = -1, createdByUserID = -1;
                DateTime appointmentDate = DateTime.Now;
                decimal paidFees = 0;
                bool isLocked = false;
                int? retakeTestApplicationID = null;

                if (clsTestAppointmentsDataAccess.GetTestAppointmentByID(testAppointmentID,
                    ref testTypeID, ref localDrivingLicenseApplicationID,
                    ref appointmentDate, ref paidFees, ref createdByUserID,
                    ref isLocked, ref retakeTestApplicationID))
                {
                    return new clsTestAppointment(testAppointmentID, testTypeID, localDrivingLicenseApplicationID,
                        appointmentDate, paidFees, createdByUserID, isLocked, retakeTestApplicationID);
                }
                else
                {
                    return null;
                }
            }

            private bool _AddNewTestAppointment()
            {
                this.TestAppointmentID = clsTestAppointmentsDataAccess.AddNewTestAppointment(
                    this.TestTypeID, this.LocalDrivingLicenseApplicationID,
                    this.AppointmentDate, this.PaidFees, this.CreatedByUserID,
                    this.IsLocked, this.RetakeTestApplicationID);

                return (this.TestAppointmentID != -1);
            }

            private bool _UpdateTestAppointment()
            {
                return clsTestAppointmentsDataAccess.UpdateTestAppointment(
                    this.TestAppointmentID, this.TestTypeID, this.LocalDrivingLicenseApplicationID,
                    this.AppointmentDate, this.PaidFees, this.CreatedByUserID,
                    this.IsLocked, this.RetakeTestApplicationID);
            }

            public static bool DeleteTestAppointment(int testAppointmentID)
            {
                return clsTestAppointmentsDataAccess.DeleteTestAppointment(testAppointmentID);
            }

            public static DataTable GetAllTestAppointments(int TestTypeID,int LocalDrivingLicenseAppID)
            {
                
                return clsTestAppointmentsDataAccess.ListTestAppointments(TestTypeID,LocalDrivingLicenseAppID);
            }

        public static bool DoesPersonHaveActiveTestAppointment(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            return clsTestAppointmentsDataAccess.DoesPersonHaveActiveTestAppointment( LocalDrivingLicenseApplicationID, TestTypeID);
        }

        public static int GetActiveTestAppointmentID(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            return clsTestAppointmentsDataAccess.GetActiveTestAppointmentID( LocalDrivingLicenseApplicationID,  TestTypeID);
        }




        public static bool IsTestAppointmentExist(int testAppointmentID)
            {
                return clsTestAppointmentsDataAccess.IsTestAppointmentExist(testAppointmentID);
            }

            public bool Save()
            {
                switch (Mode)
                {
                    case enMode.AddNew:
                        if (_AddNewTestAppointment())
                        {
                            Mode = enMode.Update;
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                    case enMode.Update:
                        return _UpdateTestAppointment();
                }

                return false;
            }
        }
}
