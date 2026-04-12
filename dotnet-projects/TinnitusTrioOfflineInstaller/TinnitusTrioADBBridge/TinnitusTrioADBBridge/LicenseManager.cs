using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.SqlServer.Server;
using TinnitusTrioADB_BAL;
using System.Net.NetworkInformation;

namespace TinnitusTrioADBBridge
{
    public partial class LicenseManager : DevExpress.XtraEditors.XtraForm
    {
        public LicenseManager()
        {
            InitializeComponent();
            txtserialkey.Text = GetMacAddress().ToString();
        }


        /// <summary>
        /// Function to return sha256 hashvalue based on the serial key.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static String sha256_hash(String value)
        {
            StringBuilder Sb = new StringBuilder();
                
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }

        /// <summary>
        /// Function to fetch the ethernet Mac address
        /// </summary>
        public static PhysicalAddress GetMacAddress()
        {
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                // Only consider Ethernet network interfaces
                if (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    return nic.GetPhysicalAddress();
                }
            }
            return null;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private string doctorCode;
        private void btnActivate_Click(object sender, EventArgs e)
        {
            //WaitForm.ShowSplashScreen();
            try
            {
              
                string shaVal = sha256_hash(txtserialkey.Text).Trim();
                string serialkey = txtactivationkey.Text;
                string serialkeyfull;
                serialkeyfull= txtactivationkey.Text;
               
             
                if (txtactivationkey.Text == string.Empty)
                {
                    XtraMessageBox.Show(
                        "Activation key can not be Empty!!",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    WaitForm.CloseForm();
                    return;
                    // MessageBox.Show(serialkey);
                }
                serialkey = serialkey.Substring(0, serialkey.Length - 3);
                doctorCode = (serialkeyfull.Length > 3) ? serialkeyfull.Substring(serialkeyfull.Length - 3, 3) : serialkeyfull;
               // WaitForm.CloseForm();
                if (serialkey == shaVal)
                 {
                    setADBEnvironmentPath();
                   try
                   {

                     
                       var objLicensemanager = new TinnitusTrioADB_BAL.LicenseManager();

                       var obj = objLicensemanager.InsertDoctorActivation(doctorCode);

                       XtraMessageBox.Show("Serial Number Activated Successfully! Please set your name and password in the next screen after pressing OK!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                       this.Hide();
                        var objlogin = new TinnitusTrioADB_BAL.Login();

                        var isFirstLogin = objlogin.CheckFirstLogin(doctorCode);

                          if (isFirstLogin != "FIRSTLOGIN") return;
                      // Application.Run(new ResetPassword(doctorCode));

                         var resetPasswordScreen = new ResetPassword(doctorCode);
                         //WaitForm.CloseForm();
                         resetPasswordScreen.ShowDialog();
                        this.Close();

                       
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
                else
                {
                    XtraMessageBox.Show("Invalid Serial Number! Please Try Again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    WaitForm.CloseForm();
                }

               // var objLicensemanager = new TinnitusTrioADB_BAL.LicenseManager();

              //  var obj = objLicensemanager.CheckLicenseManager(objTinnitusTrioBo);

               // if (obj.RegistrarName == "INVUSR")
                //{
                  
               // }

                //else
              //  {
                    //WaitForm.CloseForm();
                   
               // }

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
                //SplashForm.CloseForm();
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