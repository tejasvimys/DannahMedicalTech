using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Diagnostics;
using TinnitusTrioADB_BAL;


namespace TinnitusTrioADBBridge.UserControls
{
    public partial class UC_RenewLicense : DevExpress.XtraEditors.XtraUserControl
    {
        private string _doctorcode;
        public UC_RenewLicense(string doctorCode)
        {
            _doctorcode = doctorCode;
            InitializeComponent();
        }

        private void hyperLinkEdit2_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = "http://www.google.com";
            process.StartInfo.Verb = "open";
            process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            try
            {
                process.Start();
            }
            catch { }
        }

        private void btnRenewLicense_Click(object sender, EventArgs e)
        {
            try
            {
                var licenseCode = txtLicenseId.Text.Trim();
                var objtinnitustriobal = new TinnitusTrioADB_BAL.LicenseManager();
                var retval = objtinnitustriobal.renewlicenses(licenseCode,_doctorcode);
                if (retval.Equals("licensesInserted"))
                {
                    XtraMessageBox.Show("Licenses Imported Successfully, Patient IDs Generated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else if(retval.Equals("licensesExists"))
                {
                    XtraMessageBox.Show("Licenses are present for allocation to the patients for your ID. Kindly raise a request after all licenses are expired.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else if(retval.Equals("nolicensesfound"))
                {
                    XtraMessageBox.Show("Licenses are not yet allocated for this ID. Kindly contact the support team, Tinnitus Trio", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            catch
            {
                throw;
            }
        }
    }
}
