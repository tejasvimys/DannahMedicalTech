using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace TinnitusTrioADBBridge
{
    public partial class PatientIDEntry : DevExpress.XtraEditors.XtraForm
    {
        public PatientIDEntry(string doctorname, string doctorid)
        {
            InitializeComponent();
            lblDoctorName.Text = doctorname;
            lblDoctorId.Text = doctorid;
            LoadDoctorCode();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEnterSmrtBridge_Click(object sender, EventArgs e)
        {
            try
            {
                if (textEdit1.Text.Trim() != string.Empty)
                {
                    var checkifpatientexits = new TinnitusTrioADB_BAL.TinnitusTrioInsertPatient();
                    var patientexists = checkifpatientexits.CheckifPatientExists(lblDoctorCode.Text + labelControl3.Text + textEdit1.Text.Trim());

                    if (patientexists == "EXISTS")
                    {
                        this.Hide();
                        var frmPatientIdEntry = new Form1(lblDoctorName.Text.Trim(), lblDoctorId.Text.Trim(),
                             lblDoctorCode.Text + labelControl3.Text + textEdit1.Text.Trim());
                        frmPatientIdEntry.ShowDialog();
                        this.Close();
                    }

                    if (patientexists == "EXISTS")
                    {
                        this.Hide(); 
                        var frmPatientIdEntry = new Form1(lblDoctorName.Text.Trim(), lblDoctorId.Text.Trim(),
                             lblDoctorCode.Text + labelControl3.Text + textEdit1.Text.Trim());
                        frmPatientIdEntry.ShowDialog();
                        this.Close();
                    }

                    else
                    {
                        XtraMessageBox.Show("Patient ID Does not Exist!", "Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }

                else
                {
                    XtraMessageBox.Show("Enter Patient ID to Proceeed to Smart Bridge!", "Information",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
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

        private void PatientIDEntry_Load(object sender, EventArgs e)
        {
            SuspendLayout();
            panelControl2.Size = new Size(626, 507);

            panelControl2.Location = new Point(ClientSize.Width / 2 - 626 / 2, ClientSize.Height / 2 - 507 / 2);
            panelControl2.Anchor = AnchorStyles.None;
            panelControl2.Dock = DockStyle.None;

            ResumeLayout();
        }

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
            var frmPatientIdEntry = new QueryScreen(lblDoctorName.Text, lblDoctorId.Text);
            frmPatientIdEntry.ShowDialog();
            this.Close();
        }
        public void LoadDoctorCode()
        {
            var objlicenceManager = new TinnitusTrioADB_BAL.LicenseManager();
            lblDoctorCode.Text = objlicenceManager.GetDoctorCode();
        }
    }
}