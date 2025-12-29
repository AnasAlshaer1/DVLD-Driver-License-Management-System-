using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class clsUser
    {
        public enum enMode { AddNew = 0, Update = 1 };
        enMode Mode = enMode.AddNew;

        public int UserID { get; set; }
        public int PersonID { get; set; }

        public clsPerson PersonInfo { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
      
        public clsUser()
        {
            this.UserID = -1;
            this.PersonID = -1;
            this.Username = "";
            this.Password = "";
            this.IsActive = false;
         


            Mode = enMode.AddNew;
        }
        private clsUser(int UserID, int PersonID, string Username
            , string Password, bool IsActive)
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.PersonInfo = clsPerson.Find(PersonID);
            this.Username = Username;
            this.Password = Password;
            this.IsActive = IsActive;
          
            Mode = enMode.Update;
        }


        public static clsUser FindByUserID(int ID)
        {
            string Username = "", Password = "";
            int PersonID = -1;
            bool IsActive = false;

            if (clsUserDataAccess.GetUserInfoByUserID( ID, ref  PersonID, ref  Username
          , ref  Password, ref  IsActive))
            {
                return new clsUser( ID,   PersonID,   Username
          ,   Password,   IsActive);
            }
            else
            {
                return null;
            }


        }

        public static clsUser FindByPersonID(int PersonID)
        {
            string Username = "", Password = "";
            int UserID = -1;
            bool IsActive = false;

            if (clsUserDataAccess.GetUserInfoByPersonID(ref UserID, PersonID, ref Username
          , ref Password, ref IsActive))
            {
                return new clsUser(PersonID, PersonID, Username
          , Password, IsActive);
            }
            else
            {
                return null;
            }


        }

        public static clsUser FindByUserNameAndPassword(string Username,string Password)
        {
            int ID=-1;
            int PersonID = -1;
            bool IsActive =false;

            if (clsUserDataAccess.GetUserInfoByUsernameAndPassword(ref ID, ref PersonID,  Username
          ,  Password, ref IsActive))
            {
                return new clsUser(ID, PersonID, Username
          , Password, IsActive);
            }
            else
            {
                return null;
            }


        }
        private bool _AddNewUser()
        {
            this.UserID = clsUserDataAccess .AddNewUser( this.PersonID, this.Username
          ,  this.Password, this.IsActive);

            return (this.UserID != -1);
        }

        private bool _UpdateUser()
        {

            return (clsUserDataAccess.UpdateUser(this.UserID,this.PersonID, this.Username
          , this.Password, this.IsActive));

        }

        public static bool DeleteUser(int ID)
        {
            return clsUserDataAccess.DeleteUser(ID);
        }


        public static DataTable GetAllUsers()
        {
            return clsUserDataAccess.ListUsers();
        }

        public static bool IsUserExist(int ID)
        {
            return clsUserDataAccess.IsUserExist(ID);
        }
        public static bool IsUserWithThisPersonIDExist(int PersonID)
        {
            return clsUserDataAccess.IsUserWithThisPersonIDExist(PersonID);
        }



        public bool Save()
        {
            switch (Mode)
            {

                case enMode.AddNew:
                    if (_AddNewUser())
                    {
                        Mode = enMode.Update;
                        return true;

                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:

                    return (_UpdateUser());



            }
            return false;
        }

    }
}
