using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Excel= Microsoft.Office.Interop.Excel;

namespace TinnitusTrioADBBridge.UserControls
{
    public partial class UC_PatientInstallerReport : DevExpress.XtraEditors.XtraUserControl
    {
        public static string Doctorid;

        public UC_PatientInstallerReport(string doctorid)
        {
            InitializeComponent();
            Doctorid = doctorid;
        }

        //CHECK WHETHER THE REPORT TYPE IS FULL OR PATIENT SPECIFIC
        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlPatientIdentry.Visible = radioGroup1.SelectedIndex == 1;
        }

        //CHECK WHETHER THE REPORT DATES IS FULL OR IN BETWEEN DATE SPECIFIC
        private void radioGroup2_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlBtwDates.Visible = radioGroup2.SelectedIndex == 1;
        }

        private void UC_PatientInstallerReport_Load(object sender, EventArgs e)
        {
            try
            {
                pnlPatientIdentry.Visible = false;
                pnlBtwDates.Visible = false;
                btnGenReports.Enabled = false;
                btnxporttoExcel.Enabled = false;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void btnGenReports_Click(object sender, EventArgs e)
        {
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            btnxporttoExcel.Enabled = true;
            
            try
            {

                    if (radioGroup1.SelectedIndex == -1)
                    {
                        XtraMessageBox.Show("Please select a Report Type", "Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }

                    if (radioGroup2.SelectedIndex == -1)
                    {
                        XtraMessageBox.Show("Please select a Report Duration", "Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }


                    if (cmbReportName.SelectedItem.ToString().Equals("Installation"))
                    {
                        switch (radioGroup1.SelectedIndex)
                        {//Patient Specific
                            case 1:
                                switch (radioGroup2.SelectedIndex)
                                {
                                        //full
                                    case 0:
                                        var patientid = txtPatientid.Text.Trim();

                                        if (patientid.Equals(string.Empty))
                                        {
                                            XtraMessageBox.Show("Please Emter the Patient ID", "Error",
                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            return;
                                        }

                                        var objReportBo = new TinnitusTrioADB_BAL.ReportGenerator();
                                        var dsReport = objReportBo.GenerateAppInstallationReport(patientid, Doctorid,
                                            "full");
                                        gridControl1.DataSource = dsReport.Tables[0];
                                        break;
                                        //inbetweendates

                                    case 1:
                                        var patientid1 = txtPatientid.Text.Trim();
                                        var objReportBo1 = new TinnitusTrioADB_BAL.ReportGenerator();
                                        var dateIn = dtIndate.Text;
                                        var dateout = dtOutdate.Text;

                                        if (patientid1.Equals(string.Empty))
                                        {
                                            XtraMessageBox.Show("Please Emter the Patient ID", "Error",
                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            return;
                                        }

                                        if (dateIn.Equals(string.Empty) || dateout.Equals(string.Empty))
                                        {
                                            XtraMessageBox.Show("Please Select the Date for Duration", "Error",
                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            return;
                                        }
                                        var dsReport1 = objReportBo1.GenerateAppInstallationReport(patientid1, Doctorid,
                                            "patientid", dateIn, dateout);
                                        gridControl1.DataSource = dsReport1.Tables[0];
                                        break;
                                }

                                break;
                                //Full
                            case 0:
                                switch (radioGroup2.SelectedIndex)
                                {
                                        //FULL
                                    case 0:
                                        var objReportBo = new TinnitusTrioADB_BAL.ReportGenerator();
                                        var dsReport = objReportBo.GenerateAppInstallationReport(Doctorid, "full");
                                        gridControl1.DataSource = dsReport.Tables[0];
                                        break;
                                        //INBETWEEN DATES
                                    case 1:
                                        var objReportBo1 = new TinnitusTrioADB_BAL.ReportGenerator();
                                        var dateIn = dtIndate.Text;
                                        var dateout = dtOutdate.Text;

                                        if (dateIn.Equals(string.Empty) || dateout.Equals(string.Empty))
                                        {
                                            XtraMessageBox.Show("Please Select the Date for Duration", "Error",
                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            return;
                                        }
                                        var dsReport1 = objReportBo1.GenerateAppInstallationReport(Doctorid, "patientid",
                                            dateIn, dateout);
                                        gridControl1.DataSource = dsReport1.Tables[0];
                                        break;
                                }
                                break;
                        }
                    }

                    else if (cmbReportName.SelectedItem.ToString().Equals("Frequency"))
                    {
                        switch (radioGroup1.SelectedIndex)
                        {//Patient Specific
                            case 1:
                                switch (radioGroup2.SelectedIndex)
                                {
                                    //full
                                    case 0:
                                        var patientid = txtPatientid.Text.Trim();

                                        if (patientid.Equals(string.Empty))
                                        {
                                            XtraMessageBox.Show("Please Emter the Patient ID", "Error",
                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            return;
                                        }

                                        var objReportBo = new TinnitusTrioADB_BAL.ReportGenerator();
                                        var dsReport = objReportBo.GenerateFreqInstallationReport(patientid, Doctorid,
                                            "full");
                                        gridControl1.DataSource = dsReport.Tables[0];
                                        break;
                                    //inbetweendates

                                    case 1:
                                        var patientid1 = txtPatientid.Text.Trim();
                                        var objReportBo1 = new TinnitusTrioADB_BAL.ReportGenerator();
                                        var dateIn = dtIndate.Text;
                                        var dateout = dtOutdate.Text;

                                        if (patientid1.Equals(string.Empty))
                                        {
                                            XtraMessageBox.Show("Please Emter the Patient ID", "Error",
                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            return;
                                        }

                                        if (dateIn.Equals(string.Empty) || dateout.Equals(string.Empty))
                                        {
                                            XtraMessageBox.Show("Please Select the Date for Duration", "Error",
                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            return;
                                        }
                                        var dsReport1 = objReportBo1.GenerateFreqInstallationReport(patientid1, Doctorid,
                                            "patientid", dateIn, dateout);
                                        gridControl1.DataSource = dsReport1.Tables[0];
                                        break;}

                                break;
                            //Full
                            case 0:
                                switch (radioGroup2.SelectedIndex)
                                {
                                    //FULL
                                    case 0:
                                        var objReportBo = new TinnitusTrioADB_BAL.ReportGenerator();
                                        var dsReport = objReportBo.GenerateFreqInstallationReport(Doctorid, "full");
                                        gridControl1.DataSource = dsReport.Tables[0];
                                        break;
                                    //INBETWEEN DATES
                                    case 1:
                                        var objReportBo1 = new TinnitusTrioADB_BAL.ReportGenerator();
                                        var dateIn = dtIndate.Text;
                                        var dateout = dtOutdate.Text;

                                        if (dateIn.Equals(string.Empty) || dateout.Equals(string.Empty))
                                        {
                                            XtraMessageBox.Show("Please Select the Date for Duration", "Error",
                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            return;
                                        }
                                        var dsReport1 = objReportBo1.GenerateFreqInstallationReport(Doctorid, "patientid",
                                            dateIn, dateout);
                                        gridControl1.DataSource = dsReport1.Tables[0];
                                        break;
                                }
                                break;
                        }
                    }
           
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void cmbReportName_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnGenReports.Enabled = true;
        }

        private void btnxporttoExcel_Click(object sender, EventArgs e)
        {
            try
            {
                System.Guid guid1 = System.Guid.NewGuid();
                var substring1 = guid1.ToString();
                substring1 = substring1.Substring(0, 5);

                var directorypath = System.IO.Path.Combine(Environment.GetFolderPath(
                    Environment.SpecialFolder.MyDocuments), "TinnitusTrio");

                if (!Directory.Exists(directorypath))
                {
                    Directory.CreateDirectory(directorypath);
                }

                gridControl1.ExportToXlsx(directorypath + "\\" + substring1 + ".xls");

                var path = directorypath + "\\" + substring1 + ".xls";

                var excelApp = new Excel.Application();

                excelApp.Visible = true;

                var book = excelApp.Workbooks.Open(path,
                    0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "",
                    true, false, 0, true, false, false);


            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}