using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using DevExpress.XtraEditors;
using TinnitusTrioADB_BO;
using TinnitusTrioADB_BAL;

namespace TinnitusTrioADBBridge.UserControls
{
    public partial class UC_GetPatientDetails : UserControl
    {
        private string _doctor;
        private string _dcId;
        public UC_GetPatientDetails(string doctor, string dcId)
        {
            _doctor = doctor;
            _dcId = dcId;

            InitializeComponent();
        }

        private void btnGetDeviceDetails_Click(object sender, EventArgs e)
        {
            try
            {
                var process = Process.Start("CMD.exe", "/c adb reboot");
                if (process != null) process.WaitForExit();
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

        private void btnGetDeviceDetails_Click_1(object sender, EventArgs e)
        {
           
           
            if (txtfirstname.Text.Trim() == string.Empty)
            {
            
                XtraMessageBox.Show("First Name Cannot be Empty!", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            if (txtlastname.Text.Trim() == string.Empty)
            {
         
                XtraMessageBox.Show("Last Name Cannot be Empty!", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            if (txtemail.Text.Trim() == string.Empty)
            {
                WaitForm.CloseForm();
                XtraMessageBox.Show("E-Mail Address Cannot be Empty!", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            if (txtmobile.Text.Trim() == string.Empty)
            {
              
                XtraMessageBox.Show("Mobile Number Cannot be Empty!", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            if (comboBox1.SelectedItem.ToString().Equals("--Select--"))
            {
                XtraMessageBox.Show("Country Cannot be Empty!", "Error", MessageBoxButtons.OK,
                   MessageBoxIcon.Information);

                return;
            }


            var isEmail = Regex.IsMatch(txtemail.Text.Trim(), @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);

            if (!isEmail)
            {
                
                XtraMessageBox.Show("Invalid E-Mail ID!", "Error", MessageBoxButtons.OK,
                   MessageBoxIcon.Information);
                return;
            }

            try
            {
                WaitForm.ShowSplashScreen();
                
                var objtinnitustrioinsert = new TinnitusTrioInsertPatient();
                var stateval = (CountryState)comboBox2.SelectedItem;
                var state = stateval.State;
                var countryVal = (CountryState)comboBox1.SelectedItem;
                var country = countryVal.Countryname;

                var objBoObject = new TinnitusTrioBO
                {
                    //PatientId = txtPatientid.Text.Trim(),
                    Firstname = txtfirstname.Text.Trim(),
                    Middlename = txtmiddlename.Text.Trim(),
                    Lastname = txtlastname.Text.Trim(),
                    Addressline1 = txtaddressline1.Text.Trim(),
                    Addressline2 = txtaddressline2.Text.Trim(),
                    City = txtcity.Text.Trim(),
                    State = state,
                    Country = country,
                    Zipcode = txtzipcode.Text.Trim(),
                    Telephone = txtcode.Text.Trim() + "-" + txtphoneno.Text.Trim(),
                    Email = txtemail.Text.Trim(),
                    Mobile = txtmobile.Text.Trim(),
                    DoctorCode = _dcId
                };

                //getting the textbox elements region here

                //getting the textbox elements region ends here

                var retval = objtinnitustrioinsert.InsertPatientDetails(objBoObject);

                switch (retval)
                {
                    case "1":
                        
                        XtraMessageBox.Show("Patient Details Entered Successfully! Continuing to Tinnitus Trio Smart Bridge", "Success", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        var topLevelControl = (Form)this.TopLevelControl;
                        if (topLevelControl != null) topLevelControl.Hide();
                        var frmPatientIdEntry = new Form1(_doctor, _dcId, "");
                        frmPatientIdEntry.ShowDialog();
                        var levelControl = (Form)TopLevelControl;
                        if (levelControl != null) levelControl.Close();
                        break;
                    case "PATIENTIDEXISTS":
                       
                        XtraMessageBox.Show("Patient Exists! Please Exit the Form and Click on Edit Patient Details", "Existing Data", MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                        WaitForm.CloseForm();
                        break;
                    case "MOBILENOEXISTS":
                       
                        XtraMessageBox.Show("Patient Mobile No Exists!", "Existing Data", MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                        WaitForm.CloseForm();
                        break;
                    case "EMAILIDEXISTS":
                       
                        XtraMessageBox.Show("Patient E-Mail Address Exists!", "Existing Data", MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                        WaitForm.CloseForm();
                        break;
                    default:
                       
                         XtraMessageBox.Show("Patient Details Entered Successfully! Continuing to Tinnitus Trio Smart Bridge", "Success", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                         WaitForm.CloseForm();

                        var topLevelControl1 = (Form)this.TopLevelControl;
                        if (topLevelControl1 != null) topLevelControl1.Hide();
                        var frmPatientIdEntry1 = new Form1(_doctor, _dcId, retval);
                        frmPatientIdEntry1.ShowDialog();
                        var levelControl1 = (Form)TopLevelControl;
                        if (levelControl1 != null) levelControl1.Close();
                        break;

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


        //populate country combobox
        private void PopulateCountry()
        {
            try
            {
                var objUtility = new Utility();
                var lstCountry = objUtility.GetCountry();

                comboBox1.Items.Add("--Select--");

                foreach (var country in lstCountry)
                {
                    comboBox1.Items.Add(country);
                }

                //comboBox1.DataSource = lstCountry;
                comboBox1.DisplayMember = "Countryname";
                comboBox1.ValueMember = "Countryid";
                comboBox1.SelectedIndex = 0;
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

        private void PopulateState()
        {
            try
            {
                var val = (CountryState)comboBox1.SelectedItem;

                var contryval = val.Countryid;

                var objUtility = new Utility();
                var lstCountry = objUtility.GetStates(Convert.ToInt32(contryval));

                comboBox2.DataSource = lstCountry;
                comboBox2.DisplayMember = "State";
                comboBox2.ValueMember = "Stateid";
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


        private void UC_GetPatientDetails_Load(object sender, EventArgs e)
        {
            try
            {
                PopulateCountry();
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.SelectedIndex > 0)
                {
                     PopulateState();
                }

                else
                {
                    return;
                }
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

        private void labelControl19_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            var topLevelControl = (Form)this.TopLevelControl;
            if (topLevelControl != null) topLevelControl.Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
            var frmPatientIdEntry = new QueryScreen(_doctor, _dcId);
            frmPatientIdEntry.ShowDialog();
            var topLevelControl = (Form)this.TopLevelControl;
            if (topLevelControl != null) topLevelControl.Close();
        }

       
    }

}
