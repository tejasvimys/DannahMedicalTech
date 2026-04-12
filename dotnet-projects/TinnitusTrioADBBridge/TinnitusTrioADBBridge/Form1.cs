using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using DevExpress.UserSkins;
using TinnitusTrioADBBridge.Properties;
using TinnitusTrioADBBridge.UserControls;
using System.Reflection;
using System.Threading;
using AutoUpdaterDotNET;
using TinnitusTrioADB_BO;


namespace TinnitusTrioADBBridge
{
    public partial class Form1 : XtraForm
    {
        public Form1(string doctorName, string doctorId, string patientId)
        {
            InitializeComponent();
            lblDoctorClinic.Text = doctorName;
            lblDoctorId.Text = doctorId;
            lblPatientID.Text = patientId;

            //usb checking method
            USBConnectivity.RegisterUsbDeviceNotification(this.Handle);
        }
        
        /// <summary>
        /// usb connectivity check for android applications
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == USBConnectivity.WmDevicechange)
            {
                switch ((int)m.WParam)
                {
                    case USBConnectivity.DbtDeviceremovecomplete:

                        lblCheckUSBConnection.ForeColor = System.Drawing.Color.Blue;
                        lblCheckUSBConnection.Font = new Font("Arial", 24, FontStyle.Bold);
                        lblCheckUSBConnection.Text = "USB Disconnected";
                      

                        //XtraMessageBox.Show("USB Dis Connected");
                       
                        break;
                    case USBConnectivity.DbtDevicearrival:

                        lblCheckUSBConnection.Text = Resources.Form1_WndProc_USB_Device_Connected__Please_CLICK_ON_OK__If_a_pop_up_appears_in_the_android_device_;

                        //XtraMessageBox.Show("USB Device Connected. Please CLICK ON OK, If a pop up appears in the android device.");

                        Thread.Sleep(5000);

                        var retPath = GetSdCardPath();

                        if (retPath.Trim().Equals("") || retPath.Trim().Equals("* daemon not running. starting it now on port 5037 *"))
                        {
                            retPath = null;
                        }

                        if (retPath == null)
                        {
                            lblCheckUSBConnection.ForeColor = System.Drawing.Color.Red;
                            lblCheckUSBConnection.Font = new Font("Arial", 8, FontStyle.Regular);
                            lblCheckUSBConnection.Text =
                                "Smart Phone Not Connected Properly. Please check the device connectivity by plugging out and plugging in again. Please CLICK ON OK, If a pop up appears in the android device while plugged in.";
                            //XtraMessageBox.Show("Smart Phone Not Connected Properly. Please check the device connectivity by plugging out and plugging in again. Please CLICK ON OK, If a pop up appears in the android device while plugged in.");
                            
                        }

                        else
                        {
                            lblCheckUSBConnection.ForeColor = System.Drawing.Color.Green;
                            lblCheckUSBConnection.Font = new Font("Arial", 16, FontStyle.Bold);
                            lblCheckUSBConnection.Text = "Smart Phone Connected Properly.";
                            //XtraMessageBox.Show("Smart Phone Connected Properly.");
                            
                        }

                        break;
                }
            }
        }   
       
        private void labelControl1_Click(object sender, EventArgs e)
        {

        }

        private string GetSdCardPath()
        {
           var retval = "";
            try
            {

                var mainFolder = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

                var proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        WorkingDirectory = mainFolder + @"/adb",
                        FileName = "cmd.exe",
                        Arguments = "/c adb shell echo $SECONDARY_STORAGE",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };
                proc.Start();

                var line = "";

                while (!proc.StandardOutput.EndOfStream)
                {
                    line = proc.StandardOutput.ReadLine();
                    if (line != string.Empty)
                        return line;

                    //TO CHECK IF SD CARD IS PRESENT OR NOT, IF PRESENT, GO AHEAD WITH THE ORIGINAL CODE, ELSE CHANGE OVER TO THE PRIMARY STORAGE.
                    var proc1 = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            WorkingDirectory = mainFolder + @"/adb",
                            FileName = "cmd.exe",
                            Arguments = "/c adb shell sh " + line + "/DCIM",
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            CreateNoWindow = true
                        }
                    };

                    proc1.Start();
                    while (!proc1.StandardOutput.EndOfStream)
                    {
                        line = proc1.StandardOutput.ReadLine();

                        if (line != null && line.Contains("Permission denied") || line != null && line.Contains("No such file or directory"))
                        {
                            var proc2 = new Process
                            {
                                StartInfo = new ProcessStartInfo
                                {
                                    WorkingDirectory = mainFolder + @"/adb",
                                    FileName = "cmd.exe",
                                    Arguments = "/c adb shell echo $EXTERNAL_STORAGE",
                                    UseShellExecute = false,
                                    RedirectStandardOutput = true,
                                    CreateNoWindow = true
                                }
                            };
                            proc2.Start();

                            while (!proc2.StandardOutput.EndOfStream)
                            {
                                line = proc2.StandardOutput.ReadLine();
                                if (line != string.Empty)
                                return line;
                            }
                        }

                        else
                        {
                            return line;
                        }
                    }

                    return line;
                }

                //var mainFolder = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

                //var proc = new Process
                //{
                //    StartInfo = new ProcessStartInfo
                //    {
                //        WorkingDirectory = mainFolder + @"/adb",
                //        FileName = "cmd.exe",
                //        Arguments = "/c adb shell echo $SECONDARY_STORAGE",
                //        UseShellExecute = false,
                //        RedirectStandardOutput = true,
                //        CreateNoWindow = true
                //    }
                //};
                //proc.Start();

                //while (!proc.StandardOutput.EndOfStream)
                //{
                //    var line = proc.StandardOutput.ReadLine();
                //    return line;
                //}


                ////var mainFolder = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

                ////var proc = new Process
                ////{
                ////    StartInfo = new ProcessStartInfo
                ////    {
                ////        WorkingDirectory = mainFolder + @"/adb",
                ////        FileName = "cmd.exe",
                ////        Arguments = "/c adb shell df",
                ////        UseShellExecute = false,
                ////        RedirectStandardOutput = true,
                ////        CreateNoWindow = true
                ////    }
                ////};
                ////proc.Start();

                ////var procid = proc.Id;

                ////while (!proc.StandardOutput.EndOfStream)
                ////{
                ////    var line = proc.StandardOutput.ReadLine();

                ////    //if (line == null || !line.Contains("/extSdCard"))continue;
                ////    if (line.Contains("sdcard"))
                ////    {
                ////        var resutltantindex = line.IndexOf("sdcard", System.StringComparison.Ordinal);
                ////        if (resutltantindex == -1) continue;
                ////        line = line.Substring(0, resutltantindex);
                ////        line = line + "sdcard";
                ////        return line;
                ////    }

                ////    else if (line.Contains("extSdCard"))
                ////    {
                ////        var resutltantindex = line.IndexOf("extSdCard", System.StringComparison.Ordinal);
                ////        if (resutltantindex == -1) continue;
                ////        line = line.Substring(0, resutltantindex);
                ////        line = line + "extSdCard";
                ////        return line;
                ////    }

                ////    //if (line == null || !line.Contains("/storage")) continue;
                ////    else if (line.Contains("sdcard1"))
                ////    {
                ////        var resutltantindex = line.IndexOf("sdcard1", System.StringComparison.Ordinal);
                ////        if (resutltantindex == -1) continue;
                ////        line = line.Substring(0, resutltantindex);
                ////        line = line + "sdcard1";
                ////        return line;
                ////    }

                ////    else if (line.Contains("sdcard0"))
                ////    {
                ////        var resutltantindex = line.IndexOf("sdcard0", System.StringComparison.Ordinal);
                ////        if (resutltantindex == -1) continue;
                ////        line = line.Substring(0, resutltantindex);
                ////        line = line + "sdcard0";
                ////        return line;
                ////    }


               // }
            }
            catch (Exception exception)
            {
                //WaitForm.CloseForm();
                TinnitusTrioADB_BO.TinnitusTrioLogger.SendErrorReport(exception.ToString());
                XtraMessageBox.Show(
                    "Error in Application! Support team has got the error and are working on it. The error will be resolved Shortly! Thanks for your Patience!",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return retval;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            var dialogResult = XtraMessageBox.Show("Are You Sure To Exit?", "Exit Confirmation", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void patientDetails_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                  
                //var ucPatientDetails = new UC_GetPatientDetails();
                //pnlMain.Controls.Clear();
                //ucPatientDetails.Dock=DockStyle.Fill;
                //pnlMain.Controls.Add(ucPatientDetails);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void deviceDriverInstall_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                const string apkName = "ADBDriverInstaller.exe";
                const string folderPath = "\\adbdriver\\";
                var mainFolder = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

                var installerExe = mainFolder + folderPath + apkName;
                var process = Process.Start(installerExe);
                Thread.Sleep(500);
                if (process == null) return;
                process.WaitForInputIdle();
                SetParent(process.MainWindowHandle, this.Handle);
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

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("USER32.dll")]
        private static extern bool MoveWindow(IntPtr hwnd, int x, int y, int cx, int cy, bool repaint);

        private void navpushappstodevice_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                pnlMain.Controls.Clear();
                var retPath = GetSdCardPath();

                if (retPath.Trim().Equals("") || retPath.Trim().Equals("* daemon not running. starting it now on port 5037 *"))
                {
                    retPath = null;
                }

                

                if (retPath!=null)
                {

                    var ucinstallAPks = new UC_InstallAPK(lblPatientID.Text.Trim(),lblDoctorId.Text.Trim(), lblDoctorClinic.Text.Trim());
                    
                    ucinstallAPks.Dock = DockStyle.Fill;
                    pnlMain.Controls.Add(ucinstallAPks);
                }

                else
                {
                    XtraMessageBox.Show(
                        "Please check the connection of the Smart Device! Either the Device Might not be connected or the Driver Might not be installed!",
                        "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void navPushitemstoDevice_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                //pnlMain.Controls.Clear();
                //var retPath = GetSdCardPath();

               // if (retPath != string.Empty)

                pnlMain.Controls.Clear();
                var retPath = GetSdCardPath();

                if (retPath.Trim().Equals("") || retPath.Trim().Equals("* daemon not running. starting it now on port 5037 *"))
                {
                    retPath = null;
                }



                if (retPath != null)

                {
                    var ucpushfiles = new UC_PushFiles();
                    ucpushfiles.Dock = DockStyle.Fill;
                    pnlMain.Controls.Add(ucpushfiles);
                }

                else
                {
                    XtraMessageBox.Show(
                        "Please check the connection of the Smart Device! Either the Device Might not be connected or the Driver Might not be installed!",
                        "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                pnlMain.BackgroundImage = Properties.Resources.PNG1_bg_with_tinnitus_trio_logo;
                var balobj = new TinnitusTrioADB_BAL.Login();
                var retVal = balobj.GetPatientName(lblPatientID.Text);

                lblPatientName.Text = retVal;
                panelControl4.BackgroundImage = Properties.Resources.gradient_header;
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

        private void outboxItem_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                //pnlMain.Controls.Clear();
                //var retPath = GetSdCardPath();

                //if (retPath != string.Empty)
                pnlMain.Controls.Clear();
                var retPath = GetSdCardPath();

                if (retPath.Trim().Equals("") || retPath.Trim().Equals("* daemon not running. starting it now on port 5037 *"))
                {
                    retPath = null;
                }



                if (retPath != null)
                {
                    var ucpushfiles = new UC_GetUserDetails(lblPatientID.Text.Trim(),  lblDoctorId.Text, lblDoctorClinic.Text);

                    ucpushfiles.Dock = DockStyle.Fill;
                    pnlMain.Controls.Add(ucpushfiles);
                }
                else
                {
                    XtraMessageBox.Show(
                        "Please check the connection of the Smart Device! Either the Device Might not be connected or the Driver Might not be installed!",
                        "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void organizerGroup_ItemChanged(object sender, EventArgs e)
        {
            try                               
            {
                pnlMain.Controls.Clear();
                pnlMain.BackgroundImage = Properties.Resources.PNG1_bg_with_tinnitus_trio_logo;
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

        private void DeActivatePatient_Clicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                    var ucpushfiles = new UC_DeactivatePatient(lblPatientID.Text.Trim());
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

        private void lnkRpt_Clicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                var ucpushfiles = new UC_PatientReports(lblPatientID.Text.Trim(), lblPatientName.Text);
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

        private void labelControl2_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
            var frmPatientIdEntry = new QueryScreen(lblDoctorClinic.Text, lblDoctorId.Text);
            frmPatientIdEntry.ShowDialog();
            this.Close();
        }

      
        private void samsungdriver_linkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                const string apkName = "SAMSUNG_USB_Driver_for_Mobile_Phones.exe";
                const string folderPath = "\\Drivers\\";
                var mainFolder = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

                var installerExe = mainFolder + folderPath + apkName;
                var process = Process.Start(installerExe);
                Thread.Sleep(500);
                if (process == null) return;
                process.WaitForInputIdle();
                SetParent(process.MainWindowHandle, this.Handle);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void patientRecords_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
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

        //AUTO UPDATER FEATURE
        private void checkforUpdates_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                AutoUpdater.Start("http://52.37.207.186/TinnitusTrioUpdate/Updater.xml");
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        private void pntRecords_ItemChanged(object sender, EventArgs e)
        {
            try
            {
                var ucpushfiles = new UC_PatientInstallerReport(lblDoctorId.Text.Trim());
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

        private void pntRecords_LinkChanged(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                var ucpushfiles = new UC_PatientInstallerReport(lblDoctorId.Text.Trim());
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
            }}

    }
}