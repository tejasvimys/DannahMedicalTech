using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using TinnitusTrioADBBridge.UserControls;

namespace TinnitusTrioADBBridge
{
    public partial class InsertPatientForm : DevExpress.XtraEditors.XtraForm
    {
        private string _doctor;
        private string _dcId;
        public InsertPatientForm(string doctor, string dcId)
        {
            _doctor = doctor;
            _dcId = dcId;
            InitializeComponent();
        }

        private void InsertPatientForm_Load(object sender, EventArgs e)
        {
            try
            {
                var ucPatientDetails = new UC_GetPatientDetails(_doctor, _dcId);
                panelControl1.Controls.Clear();
                ucPatientDetails.Dock = DockStyle.Fill;
                panelControl1.Controls.Add(ucPatientDetails);

                SuspendLayout();
                panelControl1.Size = new Size(800, 700);

                panelControl1.Location = new Point(ClientSize.Width / 2 - 800  / 2, ClientSize.Height / 2 - 700 / 2);
                panelControl1.Anchor = AnchorStyles.None;
                panelControl1.Dock = DockStyle.None;

                ResumeLayout();
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

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}