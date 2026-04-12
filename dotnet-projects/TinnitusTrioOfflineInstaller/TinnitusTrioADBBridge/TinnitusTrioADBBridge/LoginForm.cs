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
using TinnitusTrioADB_BAL;

namespace TinnitusTrioADBBridge
{
    public partial class LoginForm : DevExpress.XtraEditors.XtraForm
    {
        private readonly string _doctorid;
        private readonly string _doctorcode;
        public LoginForm(string doctorid, string doctorcode)
        {
            this._doctorid = doctorid;
            this._doctorcode = doctorcode;
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                var doctorId = _doctorid;
                var doctorCode = txtLoginId.Text.Trim();
                var password = txtPassword.Text.Trim();           
                var pin = txtPin.Text.Trim();

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

                if (doctorCode == string.Empty)
                {
                   
                    XtraMessageBox.Show("Login ID Field Cannot be Empty! Please Check and Try Again!", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

               
                var objBal = new Login();
               
                var retVal = objBal.CheckLogin(_doctorid, password, pin, doctorCode);
                if (retVal.Equals("PASS"))
                {
                   
                    XtraMessageBox.Show("Login Successful! Navigating to Tinnitus Trio Smart Bridge", "Success", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    this.Hide();
                    var frmPatientIdEntry = new QueryScreen(_doctorcode, _doctorid);
                    frmPatientIdEntry.ShowDialog();
                    this.Close();
                }

                else
                {
                   
                    XtraMessageBox.Show("Password and Pin Not Set or Matching! Please Check", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }

            }
            catch (Exception exception)
            {
               
                TinnitusTrioADB_BO.TinnitusTrioLogger.SendErrorReport(exception.ToString());
                XtraMessageBox.Show(
                    "Error in Application! Support team has got the error and are working on it. The error will be resolved Shortly! Thanks for your Patience!",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                txtLoginId.Text = string.Empty;
                txtPassword.Text = string.Empty;
                txtPin.Text = string.Empty;
            }
            catch (Exception exception)
            {
                TinnitusTrioADB_BO.TinnitusTrioLogger.SendErrorReport(exception.ToString());
                XtraMessageBox.Show(
                    "Error in Application! Support team has got the error and are working on it. The error will be resolved Shortly! Thanks for your Patience!",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            var objdataset = new TinnitusTrioADB_BAL.ReportGenerator();

            var dataset = objdataset.GetDoctorName();

            lblDrId.Text = dataset.Tables[0].Rows[0][1].ToString();
            txtLoginId.Text = dataset.Tables[0].Rows[0][2].ToString();
            txtLoginId.Enabled = false;
        }
    }
}