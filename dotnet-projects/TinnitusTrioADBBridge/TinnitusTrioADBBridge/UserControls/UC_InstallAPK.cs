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
using TinnitusTrioADB_BO;

namespace TinnitusTrioADBBridge.UserControls
{
    public partial class UC_InstallAPK : UserControl
    {
        private static string _patientid;
        private static string _doctorid;
        private static string _doctorname;

        public UC_InstallAPK(string patientid, string doctorid, string doctorname)
        {
            _patientid = patientid;
            _doctorid = doctorid;
            _doctorname = doctorname;
            InitializeComponent();
        }

        private void btnInstallCMTApp_Click(object sender, EventArgs e)
        {
            try
            {

                var dialogResult = XtraMessageBox.Show("Do you want to Install and Acticvate the App for the Patient?",
                    "Activation Confirmation",
                    MessageBoxButtons.YesNo);

                if (dialogResult != DialogResult.Yes) return;
                var tinnitusObj = new TinnitusTrioSync();

                tinnitusObj.InstallandactivateApps(_patientid, "CMT");

                const string apkName = "CMT.apk";
                const string folderPath = "\\APK\\";
                var mainFolder = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                var apkPath = mainFolder + folderPath + apkName;
                //var process = Process.Start("CMD.exe", "/c adb install " + apkPath);


                var proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        WorkingDirectory = mainFolder + @"/adb",
                        FileName = "cmd.exe",
                       
                        Arguments = "/c adb install " + apkPath,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };
                proc.Start();

                proc.WaitForExit();

                var objTinnitusTrioBo = new DoctorLogging();
                var objTinnitusTrioBal = new LoginAudit();
                objTinnitusTrioBo.AppName = "CMT";
                objTinnitusTrioBo.Firstname = _doctorname;
                objTinnitusTrioBo.DoctorCode = _doctorid;
                objTinnitusTrioBo.PatientId = _patientid;

                var retVal = objTinnitusTrioBal.AuditPatientDetails(objTinnitusTrioBo);


                XtraMessageBox.Show("Composite Modulated Tones App Installed Successfully!", "Success",
                    MessageBoxButtons.OK);
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

        private void btnCMMInstaller_Click(object sender, EventArgs e)
        {
            try
            {

                var dialogResult = XtraMessageBox.Show("Do you want to Install and Acticvate the App for the Patient?",
                    "Activation Confirmation",
                    MessageBoxButtons.YesNo);

                if (dialogResult != DialogResult.Yes) return;
                var tinnitusObj = new TinnitusTrioSync();

                tinnitusObj.InstallandactivateApps(_patientid, "CMM");

                const string apkName = "CMM.apk";
                const string folderPath = "\\APK\\";
                var mainFolder = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                var apkPath = mainFolder + folderPath + apkName;
                //var process = Process.Start("CMD.exe", "/c adb install " + apkPath);

                var proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        WorkingDirectory = mainFolder + @"/adb",
                        FileName = "cmd.exe",
                        Arguments = "/c adb install " + apkPath,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };
                proc.Start();

                proc.WaitForExit();

                var objTinnitusTrioBo = new DoctorLogging();
                var objTinnitusTrioBal = new LoginAudit();
                objTinnitusTrioBo.AppName = "CMM";
                objTinnitusTrioBo.Firstname = _doctorname;
                objTinnitusTrioBo.DoctorCode = _doctorid;
                objTinnitusTrioBo.PatientId = _patientid;

                var retVal = objTinnitusTrioBal.AuditPatientDetails(objTinnitusTrioBo);

                XtraMessageBox.Show("Composite Modulated Music App Installed Successfully!", "Success",
                    MessageBoxButtons.OK);
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

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {

                var dialogResult = XtraMessageBox.Show("Do you want to Install and Acticvate the App for the Patient?",
                    "Activation Confirmation",
                    MessageBoxButtons.YesNo);

                if (dialogResult != DialogResult.Yes) return;
                var tinnitusObj = new TinnitusTrioSync();

                tinnitusObj.InstallandactivateApps(_patientid, "CMES");

                const string apkName = "CMES.apk";
                const string folderPath = "\\APK\\";
                var mainFolder = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                var apkPath = mainFolder + folderPath + apkName;
               // var process = Process.Start("CMD.exe", "/c adb install " + apkPath);
                //if (process != null) process.WaitForExit();
                var proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        WorkingDirectory = mainFolder + @"/adb",
                        FileName = "cmd.exe",
                        Arguments = "/c adb install " + apkPath,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };
                proc.Start();

                proc.WaitForExit();

                var objTinnitusTrioBo = new DoctorLogging();
                var objTinnitusTrioBal = new LoginAudit();
                objTinnitusTrioBo.AppName = "CMN";
                objTinnitusTrioBo.Firstname = _doctorname;
                objTinnitusTrioBo.DoctorCode = _doctorid;
                objTinnitusTrioBo.PatientId = _patientid;

                var retVal = objTinnitusTrioBal.AuditPatientDetails(objTinnitusTrioBo);
                XtraMessageBox.Show("Composite Modulated Environmental Sounds App Installed Successfully!", "Success",
                    MessageBoxButtons.OK);
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

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            try
            {

                var dialogResult = XtraMessageBox.Show("Do you want to Install and Acticvate the App for the Patient?",
                    "Activation Confirmation",
                    MessageBoxButtons.YesNo);

                if (dialogResult != DialogResult.Yes) return;
                var tinnitusObj = new TinnitusTrioSync();

                tinnitusObj.InstallandactivateApps(_patientid, "CMN");

                const string apkName = "CMN.apk";
                const string folderPath = "\\APK\\";
                var mainFolder = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                var apkPath = mainFolder + folderPath + apkName;
                //var process = Process.Start("CMD.exe", "/c adb install " + apkPath);
                //if (process != null) process.WaitForExit();

                var proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        WorkingDirectory = mainFolder + @"/adb",
                        FileName = "cmd.exe",
                        Arguments = "/c adb install " + apkPath,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };
                proc.Start();

                proc.WaitForExit();

                var objTinnitusTrioBo = new DoctorLogging();
                var objTinnitusTrioBal = new LoginAudit();
                objTinnitusTrioBo.AppName = "CMES";
                objTinnitusTrioBo.Firstname = _doctorname;
                objTinnitusTrioBo.DoctorCode = _doctorid;
                objTinnitusTrioBo.PatientId = _patientid;

                var retVal = objTinnitusTrioBal.AuditPatientDetails(objTinnitusTrioBo);

                XtraMessageBox.Show("Composite Modulated Noise App Installed Successfully!", "Success",
                    MessageBoxButtons.OK);
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

        private void UC_InstallAPK_Load(object sender, EventArgs e)
        {
           
        }
    }
}
