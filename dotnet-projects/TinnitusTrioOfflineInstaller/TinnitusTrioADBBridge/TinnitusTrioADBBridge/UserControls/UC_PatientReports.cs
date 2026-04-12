using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.ColorPick.Picker;
using DevExpress.XtraReports.UI;

namespace TinnitusTrioADBBridge.UserControls
{
    public partial class UC_PatientReports : UserControl
    {
        private string _patientid;
        private string _patientName;

        public UC_PatientReports(string patientid, string patientname)
        {
            InitializeComponent();
            _patientid = patientid;
            _patientName = patientname;
        }

        private void UC_PatientReports_Load(object sender, EventArgs e)
        {
            try
            {
                comboBoxEdit1.SelectedIndex = 0;
                comboBoxEdit2.SelectedIndex = 0;

                panelControl2.Visible = false;

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
            try
            {

               

                if (comboBoxEdit1.SelectedText == "CMT")
                {
                    if (comboBoxEdit2.SelectedText == "Full")
                    {
                        var report = new CMTReport(_patientid, "Full", "","",_patientName);
                        var tool = new ReportPrintTool(report);
                        tool.ShowPreview();
                    }

                    else
                    {
                        var report = new CMTReport(_patientid, "BTDates", dateEdit1.Text, dateEdit2.Text,_patientName);
                        var tool = new ReportPrintTool(report);
                        tool.ShowPreview();
                    }
                }

                else if (comboBoxEdit1.SelectedText == "CMM")
                {
                    if (comboBoxEdit2.SelectedText == "Full")
                    {
                        var report = new CMMReport(_patientid, "Full", "", "", _patientName);
                        var tool = new ReportPrintTool(report);
                        tool.ShowPreview();
                    }

                    else
                    {
                        var report = new CMMReport(_patientid, "BTDates", dateEdit1.Text, dateEdit2.Text, _patientName);
                        var tool = new ReportPrintTool(report);
                        tool.ShowPreview();
                    }
                }

                else if (comboBoxEdit1.SelectedText == "CMES")
                {
                    if (comboBoxEdit2.SelectedText == "Full")
                    {
                        var report = new CMESReport(_patientid, "Full", "", "", _patientName);
                        var tool = new ReportPrintTool(report);
                        tool.ShowPreview();
                    }

                    else
                    {
                        var report = new CMESReport(_patientid, "BTDates", dateEdit1.Text, dateEdit2.Text, _patientName);
                        var tool = new ReportPrintTool(report);
                        tool.ShowPreview();
                    }
                }

                else if (comboBoxEdit1.SelectedText == "CMN")
                {
                    if (comboBoxEdit2.SelectedText == "Full")
                    {
                        var report = new CMNReport(_patientid, "Full", "", "", _patientName);
                        var tool = new ReportPrintTool(report);
                        tool.ShowPreview();
                    }

                    else
                    {
                        var report = new CMNReport(_patientid, "BTDates", dateEdit1.Text, dateEdit2.Text, _patientName);
                        var tool = new ReportPrintTool(report);
                        tool.ShowPreview();
                    }
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

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBoxEdit2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxEdit2.SelectedText != "Full")
                {
                    panelControl2.Visible = true;
                }

                else
                {
                    panelControl2.Visible = false;
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
