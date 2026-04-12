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
using TinnitusTrioADBBridge.UserControls;

namespace TinnitusTrioADBBridge
{
    public partial class RenewLicenseForm : DevExpress.XtraEditors.XtraForm
    {
        private string doctorcode;
        private string _doctorname;
        private string _doctoridentity;
        public RenewLicenseForm(string lblDoctorCode, string doctorname, string doctorid)
        {
            doctorcode = lblDoctorCode;
            _doctorname = doctorname;
            _doctoridentity = doctorid;
            InitializeComponent();
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            
        }

        private void RenewLicenseForm_Load(object sender, EventArgs e)
        {
            var ucPatientDetails = new UC_RenewLicense(doctorcode);
            panelControl1.Controls.Clear();
            ucPatientDetails.Dock = DockStyle.Fill;
            panelControl1.Controls.Add(ucPatientDetails);

            SuspendLayout();
            panelControl1.Size = new Size(800, 700);

            panelControl1.Location = new Point(ClientSize.Width / 2 - 800 / 2, ClientSize.Height / 2 - 700 / 2);
            panelControl1.Anchor = AnchorStyles.None;
            panelControl1.Dock = DockStyle.None;
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
            var frmPatientIdEntry = new QueryScreen(_doctorname, _doctoridentity);
            frmPatientIdEntry.ShowDialog();
            this.Close();
        }
    }
}