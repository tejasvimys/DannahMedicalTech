using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.Design.Ruler;
using TinnitusTrioADB_BAL;

namespace TinnitusTrioADBBridge
{
    public partial class ResetPassword : DevExpress.XtraEditors.XtraForm
    {
        private readonly string _doctorid;
        private readonly string _doctorcode;
        public ResetPassword(string doctorid,  string doctorcode)
        {
            this._doctorid = doctorid;
            this._doctorcode = doctorcode;
            InitializeComponent();
        }

        //Resetting the password
        private void btnLogin_Click(object sender, EventArgs e)
        {
           
            try
            {
                var doctorId = lblDoctorId.Text.Trim();
                var doctorCode = txtLoginId.Text.Trim();
                var password = txtPassword.Text.Trim();
                var confirmPassword = txtConfirmPassword.Text.Trim();
                var pin = txtPin.Text.Trim();
                var confirmpin = txtConfirmPin.Text.Trim();

                if (password == string.Empty)
                {
                    XtraMessageBox.Show("Password Field Cannot be Empty! Please Check and Try Again!", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                    return;
                }


                if (pin == string.Empty)
                {
     
                    XtraMessageBox.Show("Pin Field Cannot be Empty! Please Check and Try Again!", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                   
                    return;
                }


                if (confirmPassword == string.Empty)
                {
              
                    XtraMessageBox.Show("Confirm Password Field Cannot be Empty! Please Check and Try Again!", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                   
                    return;
                }


                if (confirmpin == string.Empty)
                {
                   
                    XtraMessageBox.Show("Confirm Pin Field Cannot be Empty! Please Check and Try Again!", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
           
                    return;
                }


                if (doctorCode == string.Empty)
                {
                  
                    XtraMessageBox.Show("Login ID Field Cannot be Empty! Please Check and Try Again!", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                 
                    return;
                }

               
                if (password != confirmPassword)
                {
                   
                    XtraMessageBox.Show("Passwords Do Not Match! Please Check and Try Again!", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                if (pin != confirmpin)
                {
                   
                    XtraMessageBox.Show("Pin Do Not Match! Please Check and Try Again!", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                    return;
                }

                if (doctorCode==string.Empty)
                {
                   
                    XtraMessageBox.Show("Passwords Do Not Match! Please Check and Try Again!", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                  
                    return;
                }

                if (CheckforPasswordCharacterstics(password)) //At least one symbol  
                {
                   
                    XtraMessageBox.Show("Passwords Do Not Meet the Criteria! Please Check and Try Again!", "Error",
                         MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                    return;
                }

                WaitForm.ShowSplashScreen();
                
                var objBal = new Login();

                var retVal = objBal.ResetPassword(_doctorid, password, pin, doctorCode);
                if (retVal.Equals("SUCCESS"))
                {
                   
                    XtraMessageBox.Show("Password and Pin Set Successfully! Closing Application for security reasons. Please open the application again and Login ", "Success", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    //this.Hide();
                    //var frmPatientIdEntry = new QueryScreen(_doctorcode, _doctorid);
                    //frmPatientIdEntry.ShowDialog();
                    WaitForm.CloseForm();
                    this.Close();
                }

                else
                {
                   
                    XtraMessageBox.Show("Password and Pin Not Set! Please Check", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    WaitForm.CloseForm();
                }

              

            }
            catch (Exception exception)
            {
               
                TinnitusTrioADB_BO.TinnitusTrioLogger.SendErrorReport(exception.ToString());
                XtraMessageBox.Show(
                    "Error in Application! Support team has got the error and are working on it. The error will be resolved Shortly! Thanks for your Patience!",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                WaitForm.CloseForm();
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private static bool CheckforPasswordCharacterstics(string password)
        {
            return password.Length < 8 && // Must be above 8 characters
                   password.Any(char.IsUpper) && //At least one uppercase
                   password.Any(char.IsLower) && //At least one lowercase
                   password.Any(char.IsSymbol);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                txtConfirmPassword.Text = string.Empty;
                txtPassword.Text = string.Empty;
                txtPin.Text = string.Empty;
                txtConfirmPin.Text = string.Empty;
                txtLoginId.Text = string.Empty;
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

        private void ResetPassword_Load(object sender, EventArgs e)
        {
            SuspendLayout();
            panelControl1.Size = new Size(864, 413);

            panelControl1.Location = new Point(ClientSize.Width / 2 - 864 / 2, ClientSize.Height / 2 - 413 / 2);
            panelControl1.Anchor = AnchorStyles.None;
            panelControl1.Dock = DockStyle.None;

            ResumeLayout();
        }
    }
}