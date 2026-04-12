using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using TinnitusTrioADB_BAL;

namespace TinnitusTrioADBBridge.UserControls
{
    public partial class UC_DeactivatePatient : UserControl
    {
        private static string _patientid;
        public UC_DeactivatePatient(string patientId)
        {
            _patientid = patientId;
            InitializeComponent();
        }

        private void btnInstallCMTApp_Click(object sender, EventArgs e)
        {
            try
            {
                var dialogResult = XtraMessageBox.Show("Do you want to de acticvate the App for the Patient?", "De Activation Confirmation",
                    MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    var tinnitusObj = new TinnitusTrioSync();

                    tinnitusObj.DeactivateApps(_patientid, "CMT");

                    XtraMessageBox.Show("Apps De - Activated for the Patient Successfully!", "Success",
                        MessageBoxButtons.OK);
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

    }
}
