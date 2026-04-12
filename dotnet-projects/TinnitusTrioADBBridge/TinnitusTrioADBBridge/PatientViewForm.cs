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
    public partial class PatientViewForm : DevExpress.XtraEditors.XtraForm
    {
        public PatientViewForm()
        {
            InitializeComponent();
        }

        private void PatientViewForm_Load(object sender, EventArgs e)
        {
            try
            {
                var ucpushfiles = new UC_PatientList();
                pnlMain.Controls.Clear();
                ucpushfiles.Dock = DockStyle.Fill;
                pnlMain.Controls.Add(ucpushfiles);
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