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
    public partial class QueryScreen : DevExpress.XtraEditors.XtraForm
    {
        public QueryScreen(string doctorname, string doctorid)
        {
            InitializeComponent();
            lblDoctorname.Text = doctorname;
            lblDoctorId.Text = doctorid;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                var frmPatientIdEntry = new PatientIDEntry(lblDoctorname.Text.Trim(), lblDoctorId.Text.Trim());
                frmPatientIdEntry.ShowDialog();
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

        private void btnEnterNewPatient_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                var frmPatientIdEntry = new InsertPatientForm(lblDoctorname.Text.Trim(), lblDoctorId.Text.Trim());
                frmPatientIdEntry.ShowDialog();
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

        private void QueryScreen_Load(object sender, EventArgs e)
        {
            SuspendLayout();
            panelControl1.Size = new Size(883, 366);

            panelControl1.Location = new Point(ClientSize.Width / 2 - 883 / 2, ClientSize.Height / 2 - 366 / 2);
            panelControl1.Anchor = AnchorStyles.None;
            panelControl1.Dock = DockStyle.None;

            ResumeLayout();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {

        }

        //Patient Details View popup
        private void dispPatientDetails_Click(object sender, EventArgs e)
        {
            var frm = new PatientViewForm();
            frm.ShowDialog();
        }

        
    }
}