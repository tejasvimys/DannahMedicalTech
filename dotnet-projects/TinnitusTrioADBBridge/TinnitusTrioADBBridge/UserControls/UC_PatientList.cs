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

namespace TinnitusTrioADBBridge.UserControls
{
    public partial class UC_PatientList : DevExpress.XtraEditors.XtraUserControl
    {
        public UC_PatientList()
        {
            InitializeComponent();
        }

        private void UC_PatientList_Load(object sender, EventArgs e)
        {
            var objdataset = new TinnitusTrioADB_BAL.ReportGenerator();

            var dataset = objdataset.GeneratePatientReports();

            gridControl1.DataSource = dataset.Tables[0];

        }
    }
}
