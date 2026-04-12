using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.SqlServer.Server;
using TinnitusTrioADB_BAL;

namespace TinnitusTrioADBBridge
{
    public partial class LicenseManager : DevExpress.XtraEditors.XtraForm
    {
        public LicenseManager()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnActivate_Click(object sender, EventArgs e)
        {

             //XtraMessageBox.Show("Serial Number Activated Successfully! Product Activated to " , "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            WaitForm.ShowSplashScreen();
            try
            {
                var serial1 = textEdit1.Text.Trim();
                var serial2 = textEdit2.Text.Trim();
                var serial3 = textEdit3.Text.Trim();
                var serial4 = textEdit4.Text.Trim();
                var serial5 = textEdit5.Text.Trim();

                var objTinnitusTrioBo = new TinnitusTrioADB_BO.LicenseManager
                {
                    Serial1 = serial1,
                    Serial2 = serial2,
                    Serial3 = serial3,
                    Serial4 = serial4,
                    Serial5 = serial5
                };

                var objLicensemanager = new TinnitusTrioADB_BAL.LicenseManager();

                var obj = objLicensemanager.CheckLicenseManager(objTinnitusTrioBo);

                if (obj.RegistrarName == "INVUSR")
                {
                    XtraMessageBox.Show("Invalid Serial Number! Please Try Again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    WaitForm.CloseForm();
                }

                else
                {
                    //WaitForm.CloseForm();
                    XtraMessageBox.Show("Serial Number Activated Successfully! Product Activated to " + obj.RegistrarName, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RegisterDoctor(obj.RegistrarId, obj.RegistrarName, obj.DoctorCode);
                    setADBEnvironmentPath();
                    this.Hide();

                    //loginscreen 

                    var objlogin = new TinnitusTrioADB_BAL.Login();

                    var isFirstLogin = objlogin.CheckFirstLogin(obj.RegistrarId);

                    if (isFirstLogin != "FIRSTLOGIN") return;
                    //Application.(new ResetPassword());

                    var resetPasswordScreen = new ResetPassword(obj.RegistrarId, obj.DoctorCode);
                    WaitForm.CloseForm();
                    resetPasswordScreen.ShowDialog();
                    this.Close();
                }

            }
            catch (Exception exception)
            {
                WaitForm.CloseForm();
                TinnitusTrioADB_BO.TinnitusTrioLogger.SendErrorReport(exception.ToString());
                XtraMessageBox.Show(
                    "Error in Application! Support team has got the error and are working on it. The error will be resolved Shortly! Thanks for your Patience!",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {
            Text2Focus();
        }

        private void textEdit2_EditValueChanged(object sender, EventArgs e)
        {
            Text3Focus();
        }

        private void textEdit3_EditValueChanged(object sender, EventArgs e)
        {
            Text4Focus();
        }

        private void textEdit4_EditValueChanged(object sender, EventArgs e)
        {
            Text5Focus();
        }

        private void Text2Focus()
        {
            if (textEdit1.Text.Length == 5)
            {
                textEdit2.Focus();
            }
        }

        private void Text3Focus()
        {
            if (textEdit2.Text.Length == 5)
            {
                textEdit3.Focus();
            }
        }

        private void Text4Focus()
        {
            if (textEdit3.Text.Length == 5)
            {
                textEdit4.Focus();
            }
        }

        private void Text5Focus()
        {
            if (textEdit4.Text.Length == 5)
            {
                textEdit5.Focus();
            }
        }

        private void RegisterDoctor(string doctorid, string doctorname, string doctorcode)
        {
            try
            {
                var objLicensemanager = new TinnitusTrioADB_BAL.LicenseManager();

                var obj = objLicensemanager.InsertDoctorDetails(doctorid, doctorname, doctorcode);
            }
            catch (Exception exception)
            {
                //WaitForm.CloseForm();
                TinnitusTrioADB_BO.TinnitusTrioLogger.SendErrorReport(exception.ToString());
                XtraMessageBox.Show(
                    "Error in Application! Support team has got the error and are working on it. The error will be resolved Shortly! Thanks for your Patience!",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void setADBEnvironmentPath()
        {
            try
            {
                string pathvar = System.Environment.GetEnvironmentVariable("PATH");
                var name = "PATH";
                var value = pathvar + @";" + System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\adb\\adb.exe";
                var target = EnvironmentVariableTarget.User;
                System.Environment.SetEnvironmentVariable(name, value, target);
            }
            catch (Exception exception)
            {
                //WaitForm.CloseForm();
                TinnitusTrioADB_BO.TinnitusTrioLogger.SendErrorReport(exception.ToString());
                XtraMessageBox.Show(
                    "Error in Application! Support team has got the error and are working on it. The error will be resolved Shortly! Thanks for your Patience!",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LicenseManager_Load(object sender, EventArgs e)
        {
            try
            {
                SplashForm.CloseForm();
            }
            catch (Exception exception)
            {
                //WaitForm.CloseForm();
                TinnitusTrioADB_BO.TinnitusTrioLogger.SendErrorReport(exception.ToString());
                XtraMessageBox.Show(
                    "Error in Application! Support team has got the error and are working on it. The error will be resolved Shortly! Thanks for your Patience!",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }



}