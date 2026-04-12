using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.Design.Ruler;
using TinnitusTrioADB_BAL;
using TinnitusTrioADB_BO;

namespace TinnitusTrioADBBridge.UserControls
{
    public partial class UC_GetUserDetails : UserControl
    {
        private string _patientid;
        private string _sdCardLocation;
        private string _doctorid;
        private string _doctorname;
        public UC_GetUserDetails(string patientid, string doctorid, string doctorname)
        {
            _patientid = patientid;
            _doctorid = doctorid;
            _doctorname = doctorname;
            InitializeComponent();
        }

        private void btnGetDeviceDetails_Click(object sender, EventArgs e)
        {
            try
            {
                var fileName = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "userinfo.txt");
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

        private void btnGetDeviceDetails_Click_1(object sender, EventArgs e)
        {
            try
            {
                var directorypath = System.IO.Path.Combine(Environment.GetFolderPath(
                    Environment.SpecialFolder.MyDocuments), "TinnitusTrio");
                
                if (!Directory.Exists(directorypath))
                {
                    Directory.CreateDirectory(directorypath);
                }

                if (!Directory.Exists(directorypath+"\\"+_patientid))
                {
                    Directory.CreateDirectory(directorypath + "\\" + _patientid);
                }

                using (var writer = XmlWriter.Create(directorypath + "\\" + _patientid + "\\" + "Freq.xml"))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("Frequency");

                    writer.WriteElementString("Value", txtDeviceDetails.Text.Trim());

                    writer.WriteEndElement();writer.WriteEndDocument();
                }

              var sdcardpath =  GetSdCardPath();
              if (sdcardpath != string.Empty)
              {
                  CreateDirectoryStructure();

                  PushXmlFile();

                  var tinnitusBal = new TinnitusTrioSync();
                  tinnitusBal.InsertFrequencyDetails(_patientid, txtDeviceDetails.Text.Trim());

                  var objTinnitusTrioBo = new DoctorLogging();
                  var objTinnitusTrioBal = new LoginAudit();
                  objTinnitusTrioBo.Mobile = txtDeviceDetails.Text.Trim();
                  objTinnitusTrioBo.Firstname = _doctorname;
                  objTinnitusTrioBo.DoctorCode = _doctorid;
                  objTinnitusTrioBo.PatientId = _patientid;

                  var retVal = objTinnitusTrioBal.AuditFrequencyDetails(objTinnitusTrioBo);
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
                    _sdCardLocation = line;

                    //TO CHECK IF SD CARD IS PRESENT OR NOT, IF PRESENT, GO AHEAD WITH THE ORIGINAL CODE, ELSE CHANGE OVER TO THE PRIMARY STORAGE.
                    var proc1 = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            WorkingDirectory = mainFolder + @"/adb",
                            FileName = "cmd.exe",
                            Arguments = "/c adb shell sh "+line+"/DCIM",
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            CreateNoWindow = true
                        }
                    };

                    proc1.Start();
                    while (!proc1.StandardOutput.EndOfStream)
                    {
                        line = proc1.StandardOutput.ReadLine();

                        if (line != null && line.Contains("Permission denied") ||line != null && line.Contains("No such file or directory"))
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
                                    _sdCardLocation = line;
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
                //        Arguments = "/c adb shell df",
                //        UseShellExecute = false,
                //        RedirectStandardOutput = true,
                //        CreateNoWindow = true
                //    }
                //};
                //proc.Start();

                //var procid = proc.Id;

                //while (!proc.StandardOutput.EndOfStream)
                //{
                //    var line = proc.StandardOutput.ReadLine();


                    //if (line == null || !line.Contains("/mnt")) continue;
                    //if (line.Contains("mnt/sdcard"))
                    //{
                    //    var resutltantindex = line.IndexOf("mnt/sdcard", System.StringComparison.Ordinal);
                    //    if (resutltantindex == -1) continue;
                    //    line = line.Substring(0, resutltantindex);
                    //    line = line + "mnt/sdcard";
                    //    _sdCardLocation = line;
                    //    return line;
                    //}

                    //else
                    //{
                    //    var resutltantindex = line.IndexOf("extsdcard", System.StringComparison.Ordinal);
                    //    if (resutltantindex == -1) continue;
                    //    line = line.Substring(0, resutltantindex);
                    //    line = line + "extsdcard";
                    //    _sdCardLocation = line;
                    //    return line;
                    //}


                    //if (line == null || !line.Contains("/storage")) continue;
                    //if (line.Contains("/sdcard1"))
                    //{
                    //    var resutltantindex = line.IndexOf("sdcard1", System.StringComparison.Ordinal);
                    //    if (resutltantindex == -1) continue;
                    //    line = line.Substring(0, resutltantindex);
                    //    line = line + "sdcard1";
                    //    _sdCardLocation = line;
                    //    return line;
                    //}

                    //else
                    //{
                    //    var resutltantindex = line.IndexOf("sdcard0", System.StringComparison.Ordinal);
                    //    if (resutltantindex == -1) continue;
                    //    line = line.Substring(0, resutltantindex);
                    //    line = line + "sdcard0";
                    //    _sdCardLocation = line;
                    //    return line;
                    //}

                    //if (line.Contains("sdcard"))
                    //{
                    //    var resutltantindex = line.IndexOf("sdcard", System.StringComparison.Ordinal);
                    //    if (resutltantindex == -1) continue;
                    //    line = line.Substring(0, resutltantindex);
                    //    line = line + "sdcard";
                    //    _sdCardLocation = line;
                    //    return line;
                    //}

                    //else if (line.Contains("mnt/extSdCard"))
                    //{
                    //    var resutltantindex = line.IndexOf("extSdCard", System.StringComparison.Ordinal);
                    //    if (resutltantindex == -1) continue;
                    //    line = line.Substring(0, resutltantindex);
                    //    line = line + "extSdCard";
                    //    _sdCardLocation = line;
                    //    return line;
                    //}

                    //else if (line.Contains("storage/extSdCard"))
                    //{
                    //    var resutltantindex = line.IndexOf("extSdCard", System.StringComparison.Ordinal);
                    //    if (resutltantindex == -1) continue;
                    //    line = line.Substring(0, resutltantindex);
                    //    line = line + "extSdCard";
                    //    _sdCardLocation = line;
                    //    return line;
                    //}

                    ////if (line == null || !line.Contains("/storage")) continue;
                    //else if (line.Contains("sdcard1"))
                    //{
                    //    var resutltantindex = line.IndexOf("sdcard1", System.StringComparison.Ordinal);
                    //    if (resutltantindex == -1) continue;
                    //    line = line.Substring(0, resutltantindex);
                    //    line = line + "sdcard1";
                    //    _sdCardLocation = line;
                    //    return line;
                    //}

                    //else
                    //{
                    //    var resutltantindex = line.IndexOf("sdcard0", System.StringComparison.Ordinal);
                    //    if (resutltantindex == -1) continue;
                    //    line = line.Substring(0, resutltantindex);
                    //    line = line + "sdcard0";
                    //    _sdCardLocation = line;
                    //    return line;
                    //}

                //}
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

        private void CreateDirectoryStructure()
        {
            try
            {
                var maindirectory = _sdCardLocation;
                const string directory1 = "/tinnitustrio";
                const string directory2 = "/cmt";

                //var process1 = Process.Start("CMD.exe", "/c adb shell mkdir " + maindirectory + directory1);
                //if (process1 != null) process1.WaitForExit();

                //var process2 = Process.Start("CMD.exe", "/c adb shell mkdir " + maindirectory + directory1 + directory2);
                //if (process2 != null) process2.WaitForExit();  

                var mainFolder = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

                var proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        WorkingDirectory = mainFolder + @"/adb",
                        FileName = "cmd.exe",
                        Arguments = "/c adb shell mkdir " + maindirectory + directory1,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };
                proc.Start();

                proc.WaitForExit();

                var proc1 = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        WorkingDirectory = mainFolder + @"/adb",
                        FileName = "cmd.exe",
                        Arguments = "/c adb shell mkdir " + maindirectory + directory1 + directory2,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };
                proc1.Start();

                proc1.WaitForExit();
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

        private void PushXmlFile()
        {
            try
            {
                var directorypath = System.IO.Path.Combine(Environment.GetFolderPath(
                   Environment.SpecialFolder.MyDocuments), "TinnitusTrio");

                //var process1 = Process.Start("CMD.exe",
                //        "/c adb push " + directorypath + "\\" + _patientid + "\\" + "Freq.xml" + " " + _sdCardLocation + "/tinnitustrio/cmt");
                //if (process1 != null) process1.WaitForExit();

                var mainFolder = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

                var proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        WorkingDirectory = mainFolder + @"/adb",
                        FileName = "cmd.exe",
                        Arguments =
                            "/c adb push " + directorypath + "\\" + _patientid + "\\" + "Freq.xml" + " " + _sdCardLocation + "/tinnitustrio/cmt",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };
                proc.Start();

                proc.WaitForExit();

                XtraMessageBox.Show("Tinnitus Frequency File Generated and Transferred to Device!", "Success",
                     MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void txtDeviceDetails_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }


    }
}
