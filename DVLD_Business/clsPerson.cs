using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;
namespace DVLD_Business
{
    public class clsPerson
    {

        public enum enMode { AddNew = 0, Update = 1 };
        enMode Mode = enMode.AddNew;
        public int PersonID { get; set; }

        public string NationalNo {  get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
    
        public string LastName { get; set; }

        public string FullName
        {
            get { return FirstName + " " + SecondName + " " + ThirdName + " " + LastName; }

        }
        public short Gendor {  get; set; }

        public string Address {  get; set; }
        public DateTime DateOfBirth { get; set; }

        public int NationalityCountryID { get; set; }

        public string National {  get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }


        public clsCountry CountryInfo;
        public string ImagePath {  get; set; }

        public clsPerson()
        {
            this.PersonID = -1;
            this.NationalNo = "";
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.Email = "";
            this.Phone = "";
            this.Gendor = -1;
            this.DateOfBirth = DateTime.Now;
            this.NationalityCountryID = -1;
            this.Address = "";
            this.ImagePath = "";
           

            Mode = enMode.AddNew;
        }
        private clsPerson(int iD, string NationalNo,string firstName
            ,string SecondName,string ThirdName ,string lastName, DateTime dateOfBirth, short Gendor
           , string address,  string phone,string email, int NationalityCountryID ,string imagePath)
        {
            this.PersonID = iD;
            this.FirstName = firstName;
            this.SecondName=SecondName;
            this.ThirdName = ThirdName;
            this.LastName = lastName;
            this.Email = email;
           this.Phone = phone;
            this.Address = address;
            this.DateOfBirth = dateOfBirth;
            this.ImagePath = imagePath;
            this.NationalityCountryID = NationalityCountryID;
            this.NationalNo = NationalNo;
            this.Gendor=Gendor;
            this.National = National;
            this.CountryInfo = clsCountry.Find(NationalityCountryID);
            Mode = enMode.Update;
        }


        public static clsPerson Find(int ID)
        {
            string FirstName = "", SecondName = "" ,ThirdName = "", LastName = "", Email = "", Phone = "", Address = "", ImagePath = "", NationalNo = "";
            DateTime DateOfBirth = DateTime.Now;
            int NationalityCountryID = -1;
            short Gendor = 0;

            if (clsPersonDataAccess.GetPersonInfoByID( ID, ref NationalNo, ref FirstName
            ,  ref SecondName, ref ThirdName, ref LastName, ref DateOfBirth,ref  Gendor
           , ref Address, ref Phone,ref  Email,  ref NationalityCountryID,ref ImagePath))
            {
                return new clsPerson(ID,  NationalNo,  FirstName, SecondName
          , ThirdName,  LastName, DateOfBirth,  Gendor,  Address,  Phone
          ,  Email, NationalityCountryID, ImagePath);
            }
            else
            {
                return null;
            }


        }


        public static clsPerson Find(string NationalNo)
        {
            string FirstName = "", SecondName = "", ThirdName = "", LastName = "", Email = "", Phone = "", Address = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            int NationalityCountryID = -1,ID=-1;
            short Gendor = 0;

            if (clsPersonDataAccess.GetPersonInfoByNationalNo(ref ID,NationalNo, ref FirstName
            , ref SecondName, ref ThirdName, ref LastName, ref DateOfBirth, ref Gendor
           , ref Address, ref Phone, ref Email, ref NationalityCountryID, ref ImagePath))
            {
                return new clsPerson(ID, NationalNo, FirstName, SecondName
          , ThirdName, LastName, DateOfBirth, Gendor, Address, Phone
          , Email, NationalityCountryID, ImagePath);
            }
            else
            {
                return null;
            }


        }

        private bool _AddNewPerson()
        {
                this.PersonID = clsPersonDataAccess.AddNewPerson(this.NationalNo,this.FirstName, this.SecondName,
                this.ThirdName,this.LastName,this.DateOfBirth,this.Gendor ,
                this.Address, this.Phone, this.Email,this.NationalityCountryID, this.ImagePath);

            return (this.PersonID != -1);
        }

        private bool _UpdatePerson()
        {

            return (clsPersonDataAccess.UpdatePerson(this.PersonID,this.NationalNo, this.FirstName, this.SecondName,
                this.ThirdName, this.LastName, this.DateOfBirth, this.Gendor,
                this.Address, this.Phone, this.Email, this.NationalityCountryID, this.ImagePath));

        }

        public static bool DeletePerson(int ID)
        {
            return clsPersonDataAccess.DeletePerson(ID);
        }


        public static DataTable GetAllPeople()
        {
            return clsPersonDataAccess.ListPeople();
        }

        public static bool IsPersonExist(int ID)
        {
            return clsPersonDataAccess.IsPersonExist(ID);
        }
        public static bool IsPersonExist(string NationalNo)
        {
            return clsPersonDataAccess.IsPersonExist(NationalNo);
        }

        public bool Save()
        {
            switch (Mode)
            {

                case enMode.AddNew:
                    if (_AddNewPerson())
                    {
                        Mode = enMode.Update;
                        return true;

                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:

                    return (_UpdatePerson());



            }
            return false;
        }
    }
}
