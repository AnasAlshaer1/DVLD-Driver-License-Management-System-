using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class clsCountry
    {
        public int CountryID { get; set; }

        public string CountryName { get; set; }

        public clsCountry(int CountryID,string CountryName) {
            this.CountryID = CountryID;
            this.CountryName = CountryName;
       
        
        }

        public static clsCountry Find(int CountryID)
        {
            string CountryName = "";

            if (clsCountryDataAccess.GetCountryInfoByID(CountryID, ref CountryName))
            {
                return new clsCountry(CountryID,CountryName);
            }
            else
            {
                return null;
            }

        }

        public static clsCountry Find(string CountryName)
        {
            int CountryID = -1;  

            if (clsCountryDataAccess.GetCountryInfoByName(ref CountryID,  CountryName))
            {
                return new clsCountry(CountryID, CountryName);
            }
            else
            {
                return null;
            }

        }
        public static DataTable GetAllCountries()
        {
            return clsCountryDataAccess.ListCountries();
          
        }

    }
}
