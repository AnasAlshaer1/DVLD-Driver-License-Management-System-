using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Business;
using DVLD_Presentaion.Applications;
using DVLD_Presentaion.Licenses;
using DVLD_Presentaion.Licenses.International_License;

namespace DVLD_Presentaion
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }


        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManagePeople frmPeople = new frmManagePeople();
            clsGlobalSettings.OpenNewFormAndEditNewFormCordination(frmPeople,1220,500,30,120);

        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsGlobalSettings.CurrentUser = null;
            this.Close();
         
            

           
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageUsers frmUsers = new frmManageUsers();
            clsGlobalSettings.OpenNewFormAndEditNewFormCordination(frmUsers,795,580,250,80);
         
        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListDrivers frmDrivers = new frmListDrivers();
            clsGlobalSettings.OpenNewFormAndEditNewFormCordination(frmDrivers, 822, 585, 255, 85);
        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserInfo frmUserInfo = new frmUserInfo(clsGlobalSettings.CurrentUser);
            clsGlobalSettings.OpenNewFormAndEditNewFormCordination(frmUserInfo, 805, 500, 230, 120);

       
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frmchangepass = new frmChangePassword(clsGlobalSettings.CurrentUser);
            clsGlobalSettings.OpenNewFormAndEditNewFormCordination(frmchangepass, 805, 620, 230, 60);

         
        }

        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListApplicationTypes applicationTypes = new frmListApplicationTypes();
            clsGlobalSettings.OpenNewFormAndEditNewFormCordination(applicationTypes,625,638,350,60);
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
                 frmManageTestTypes testTypes = new frmManageTestTypes();
            clsGlobalSettings.OpenNewFormAndEditNewFormCordination(testTypes,785,575,275,85);



        }

        private void localDrivingLicneseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageLocalDrivingLicenseApplications frmLocalDrivngApp= new frmManageLocalDrivingLicenseApplications();
            clsGlobalSettings.OpenNewFormAndEditNewFormCordination(frmLocalDrivngApp,1030,590,150,90);
        }

        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditNewLocalDriverLicenseApplication addnewLocalLicenseApp= new frmAddEditNewLocalDriverLicenseApplication();
            clsGlobalSettings.OpenNewFormAndEditNewFormCordination(addnewLocalLicenseApp, 845, 660);

        }

        private void retakeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageLocalDrivingLicenseApplications frmLocalDrivngApp = new frmManageLocalDrivingLicenseApplications();
            clsGlobalSettings.OpenNewFormAndEditNewFormCordination(frmLocalDrivngApp, 1030, 590, 150, 90);
        }

        private void internationalLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListInternationalLicenseApplications frmInternationalDrivngApp = new frmListInternationalLicenseApplications();
            clsGlobalSettings.OpenNewFormAndEditNewFormCordination(frmInternationalDrivngApp, 1030, 590, 150, 90);
        }

        private void internationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicenseApplication frmNewInternationalDrivngApp = new frmNewInternationalLicenseApplication();
            clsGlobalSettings.OpenNewFormAndEditNewFormCordination(frmNewInternationalDrivngApp, 780, 715, 230, 8);
        }

        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRenewLicenseApplication frmRenewLicense = new frmRenewLicenseApplication();
            clsGlobalSettings.OpenNewFormAndEditNewFormCordination(frmRenewLicense, 780, 790, 230, 0);
        }

        private void ReplacementForDamagedDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReplacementForDamagedLicense frmReplacement = new frmReplacementForDamagedLicense();
            clsGlobalSettings.OpenNewFormAndEditNewFormCordination(frmReplacement, 780, 690, 230, 23);
        }

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDetainLicense frmDetainLicnese = new frmDetainLicense();
            clsGlobalSettings.OpenNewFormAndEditNewFormCordination(frmDetainLicnese, 780, 700, 230, 23);
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frmReleaseDetainLicnese = new frmReleaseDetainedLicense();
            clsGlobalSettings.OpenNewFormAndEditNewFormCordination(frmReleaseDetainLicnese, 780, 750, 230, 8);
        }

        private void releaseDetainedDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frmReleaseDetainLicnese = new frmReleaseDetainedLicense();
            clsGlobalSettings.OpenNewFormAndEditNewFormCordination(frmReleaseDetainLicnese, 780, 750, 230, 8);
        }

        private void manageDetainedLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListDetainedLicenses frmDetainedLicense = new frmListDetainedLicenses();
            clsGlobalSettings.OpenNewFormAndEditNewFormCordination(frmDetainedLicense, 1030, 590, 150, 90);
        }
    }
    
}
