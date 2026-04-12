using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Drawing;

namespace TinnitusTrioADBBridge.UserControls
{
    public partial class UC_PushFiles : UserControl
    {

        MediaPlayer _player = new MediaPlayer();

        public UC_PushFiles()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Page load function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UC_PushFiles_Load(object sender, EventArgs e)
        {
            try
            {

                GetSdCardPath();
                if (txtSdCardLocation.Text.Trim().Equals(string.Empty))
                {
                    btnCreateDirectoryStructure.Enabled = false;
                    btnPushFiles.Enabled = false;
                    btnRemoveFile.Enabled = false;
                    btnStopmp3.Enabled = false;
                    simpleButton2.Enabled = false;
                    simpleButton1.Enabled = false;
                    chkTransferFolder.Enabled = false;

                    simpleButton6.Enabled = false;
                    simpleButton3.Enabled = false;
                    simpleButton5.Enabled = false;
                    simpleButton4.Enabled = false;
                    simpleButton2.Enabled = false;
                    checkEdit1.Enabled = false;
                    simpleButton7.Enabled = false;
                    return;
                }
                
                GetFolderList();
                GetCmesFolderList();
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

        private void GetSdCardPath()
        {
            try
            {

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
                //    if(line!=string.Empty)
                //    txtSdCardLocation.Text = line;
                //}

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
                     txtSdCardLocation.Text = line;

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
                                    txtSdCardLocation.Text = line;
                                return;
                            }
                        }

                        else
                        {
                            return;
                        }
                    }

                    return;
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

             

                //while (!proc.StandardOutput.EndOfStream)
                //{
                //    var line = proc.StandardOutput.ReadLine();
                //    //if (line == null || !line.Contains("/storage")) continue;
                //    //if (line.Contains("/sdcard1"))
                //    //{
                //    //    var resutltantindex = line.IndexOf("sdcard1", System.StringComparison.Ordinal);
                //    //    if (resutltantindex == -1) continue;
                //    //    line = line.Substring(0, resutltantindex);
                //    //    line = line + "sdcard1";
                //    //    txtSdCardLocation.Text = line;
                //    //}

                //    //else
                //    //{
                //    //    var resutltantindex = line.IndexOf("sdcard0", System.StringComparison.Ordinal);
                //    //    if (resutltantindex == -1) continue;
                //    //    line = line.Substring(0, resutltantindex);
                //    //    line = line + "sdcard0";
                //    //    txtSdCardLocation.Text = line;
                //    //}

                //    if (line.Contains("sdcard"))
                //    {
                //        var resutltantindex = line.IndexOf("sdcard", System.StringComparison.Ordinal);
                //        if (resutltantindex == -1) continue;
                //        line = line.Substring(0, resutltantindex);
                //        line = line + "sdcard";
                //        txtSdCardLocation.Text = line;
                //    }

                //    else if (line.Contains("extSdCard"))
                //    {
                //        var resutltantindex = line.IndexOf("extSdCard", System.StringComparison.Ordinal);
                //        if (resutltantindex == -1) continue;
                //        line = line.Substring(0, resutltantindex);
                //        line = line + "extSdCard";
                //        txtSdCardLocation.Text = line;
                //    }

                //    //if (line == null || !line.Contains("/storage")) continue;
                //    else if (line.Contains("sdcard1"))
                //    {
                //        var resutltantindex = line.IndexOf("sdcard1", System.StringComparison.Ordinal);
                //        if (resutltantindex == -1) continue;
                //        line = line.Substring(0, resutltantindex);
                //        line = line + "sdcard1";
                //        txtSdCardLocation.Text = line;
                //    }

                //    else if (line.Contains("sdcard0"))
                //    {
                //        var resutltantindex = line.IndexOf("sdcard0", System.StringComparison.Ordinal);
                //        if (resutltantindex == -1) continue;
                //        line = line.Substring(0, resutltantindex);
                //        line = line + "sdcard0";
                //        txtSdCardLocation.Text = line;
                //    }
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
        }

        private void GetFolderList()
        {
            try
            {
                lstCMMFolders.DataSource = null;
                lstCMMFolders.Items.Clear();
                const string folderPath = "\\CMM\\";
                var mainFolder = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

                var dirs = Directory.GetDirectories(mainFolder + folderPath);
                var list = new Dictionary<string, string>();

                // For folders in the directory
          
                foreach (var dir in dirs)
                {
                   
                    var pathname =  new DirectoryInfo(dir).Name;
                    var path = new DirectoryInfo(dir).FullName;                  
                    list.Add(path,pathname);
                }

                lstCMMFolders.DataSource = new BindingSource(list, null);
                lstCMMFolders.DisplayMember = "Value";
                lstCMMFolders.ValueMember = "Key";
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

        private void GetCmesFolderList()
        {
            try
            {
                lstCMESFolders.DataSource = null;
                lstCMESFolders.Items.Clear();
                const string folderPath = "\\CMES\\";
                var mainFolder = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

                var dirs = Directory.GetDirectories(mainFolder + folderPath);
                var list = new Dictionary<string, string>();

                // For folders in the directory

                foreach (var dir in dirs)
                {

                    var pathname = new DirectoryInfo(dir).Name;
                    var path = new DirectoryInfo(dir).FullName;
                    list.Add(path, pathname);
                }

                lstCMESFolders.DataSource = new BindingSource(list, null);
                lstCMESFolders.DisplayMember = "Value";
                lstCMESFolders.ValueMember = "Key";
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

        public void GetCmmFiles(string foldername)
        {
            try
            {
                lstCMMFiles.DataSource = null;
                lstCMMFiles.Items.Clear();
                const string folderPath = "\\CMM\\";
                var mainFolder = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                var filePaths = Directory.GetFiles(mainFolder + folderPath + foldername + "\\", "*.mp3",
                    SearchOption.AllDirectories);

                var list = new Dictionary<string, string>();

                foreach (var dir in filePaths)
                {
                    var fileValue = (dir);
                    var filename = Path.GetFileNameWithoutExtension(dir);
                    if (fileValue != null) list.Add(fileValue, filename);
                }

                lstCMMFiles.DataSource = new BindingSource(list, null);
                lstCMMFiles.DisplayMember = "Value";
                lstCMMFiles.ValueMember = "Key";
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

        public void GetCmesFiles(string foldername)
        {
            try
            {
                lstCMESFiles.DataSource = null;
                lstCMESFiles.Items.Clear();
                const string folderPath = "\\CMES\\";
                var mainFolder = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                var filePaths = Directory.GetFiles(mainFolder + folderPath + foldername + "\\", "*.mp3",
                    SearchOption.AllDirectories);

                var list = new Dictionary<string, string>();

                foreach (var dir in filePaths)
                {
                    var fileValue = (dir);
                    var filename = Path.GetFileNameWithoutExtension(dir);
                    if (fileValue != null) list.Add(fileValue, filename);
                }

                lstCMESFiles.DataSource = new BindingSource(list, null);
                lstCMESFiles.DisplayMember = "Value";
                lstCMESFiles.ValueMember = "Key";
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


        public void GetCmmFilesForTransferList(string foldername)
        {

            try
            {
                lstCMMFiles.DataSource = null;
                lstCMMFiles.Items.Clear();
                const string folderPath = "\\CMM\\";
                var mainFolder = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                var filePaths = Directory.GetFiles(mainFolder + folderPath + foldername + "\\", "*.mp3",
                    SearchOption.AllDirectories);

                var list = new Dictionary<string, string>();

                foreach (var dir in filePaths)
                {
                    var fileValue = (dir);
                    var filename = Path.GetFileNameWithoutExtension(dir);
                    if (fileValue != null) list.Add(fileValue, filename);
                }

                lstCMMCopiedFiles.DataSource = new BindingSource(list, null);
                lstCMMCopiedFiles.DisplayMember = "Value";
                lstCMMCopiedFiles.ValueMember = "Key";
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

        public void GetCmesFilesForTransferList(string foldername)
        {

            try
            {
                lstCMESFiles.DataSource = null;
                lstCMESFiles.Items.Clear();
                const string folderPath = "\\CMES\\";
                var mainFolder = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                var filePaths = Directory.GetFiles(mainFolder + folderPath + foldername + "\\", "*.mp3",
                    SearchOption.AllDirectories);

                var list = new Dictionary<string, string>();

                foreach (var dir in filePaths)
                {
                    var fileValue = (dir);
                    var filename = Path.GetFileNameWithoutExtension(dir);
                    if (fileValue != null) list.Add(fileValue, filename);
                }

                lstCMESCopiedFiles.DataSource = new BindingSource(list, null);
                lstCMESCopiedFiles.DisplayMember = "Value";
                lstCMESCopiedFiles.ValueMember = "Key";
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

        public void GetCmesFilenameforTransfer()
        {
            var list = new Dictionary<string, string>();
            var selectedProperty = (KeyValuePair<string, string>)lstCMESFiles.SelectedItem;
        }
        private void lstCMMFolders_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var selectedProperty = (KeyValuePair<string, string>)lstCMMFolders.SelectedItem;
                var keyTxt = selectedProperty.Value;

               // GetCmmFiles(lstCMMFolders.SelectedItem.ToString());
                GetCmmFiles(keyTxt);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //MP3 PLAY METHOD
  
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                _player.Stop();
                var lstSelecteditemValue = lstCMMFiles.SelectedValue.ToString();
                _player.Play(lstSelecteditemValue);
                
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void btnStopmp3_Click(object sender, EventArgs e)
        {
            _player.Stop();
        }

        private void btnCreateDirectoryStructure_Click(object sender, EventArgs e)
        {
            try
            {

                var maindirectory = txtSdCardLocation.Text.Trim();
                const string directory1 = "/tinnitustrio";
                const string directory2 = "/cmm";
                const string directory3 = "/music";
                const string directory4 = "/environmental";

                //var process1 = Process.Start("CMD.exe", "/c adb shell mkdir " + maindirectory + directory1);
                //if (process1 != null) process1.WaitForExit();

                //var process2 = Process.Start("CMD.exe", "/c adb shell mkdir "  +maindirectory + directory1+directory2);
                //if (process2 != null) process2.WaitForExit();

                //var process3 = Process.Start("CMD.exe", "/c adb shell mkdir " + maindirectory + directory1 + directory2+ directory3);
                //if (process3 != null) process3.WaitForExit();

                //var process4 = Process.Start("CMD.exe", "/c adb shell mkdir " + maindirectory + directory1 + directory2+directory4);
                //if (process4 != null) process4.WaitForExit();
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

                var proc2 = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        WorkingDirectory = mainFolder + @"/adb",
                        FileName = "cmd.exe",
                        Arguments = "/c adb shell mkdir " + maindirectory + directory1 + directory2 + directory3,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };
                proc2.Start();

                proc2.WaitForExit();

                var proc3 = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        WorkingDirectory = mainFolder + @"/adb",
                        FileName = "cmd.exe",
                        Arguments = "/c adb shell mkdir " + maindirectory + directory1 + directory2 + directory4,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };
                proc3.Start();

                proc3.WaitForExit();

                XtraMessageBox.Show("Directory Structure Created Succesfully!", "Success", MessageBoxButtons.OK);
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

        private void btnPushFiles_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkTransferFolder.Checked == true)
                {
                    lstCMMFiles.Enabled = false;
                    btnStopmp3.Enabled = false;
                    simpleButton2.Enabled = false;
                    var selectedProperty = (KeyValuePair<string, string>)lstCMMFolders.SelectedItem;
                    var keyTxt = selectedProperty.Value;
                    //GetCmmFilesForTransferList(lstCMMFolders.SelectedItem.ToString());
                    GetCmmFilesForTransferList(keyTxt);
                }

                else
                {
                    lstCMMFiles.Enabled = true;
                    simpleButton2.Enabled = true;
                    btnStopmp3.Enabled = false;

                    var ulist = new List<KeyValuePair<string, string>>();

                    foreach (int i in lstCMMFiles.SelectedIndices)
                    {
                        var seProperty = (KeyValuePair<string, string>)lstCMMFiles.Items[i];
                        ulist.Add(seProperty);
                    }

                    lstCMMCopiedFiles.DataSource = ulist;
                    lstCMMCopiedFiles.ValueMember = "Key";
                    lstCMMCopiedFiles.DisplayMember = "Value";
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

        private void chkTransferFolder_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTransferFolder.Checked == true)
            {
                lstCMMFiles.Enabled = false;
                btnStopmp3.Enabled = false;
                simpleButton2.Enabled = false;
            }

            else
            {
                lstCMMFiles.Enabled = true;
                btnStopmp3.Enabled = true;
                simpleButton2.Enabled = true;
            }
        }

        //ADB PUSH LOGIC
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {

                var returnValue = XtraMessageBox.Show("Have you created the directory Structure?", "Counter Check",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (!returnValue.Equals(DialogResult.Yes)) return;
                foreach (var drv in lstCMMCopiedFiles.Items)
                {
                    //var id =lstCMMCopiedFiles.ValueMember;

                    var id = (KeyValuePair<string, string>) drv;
                    var value = id.Key;
                    //var process1 = Process.Start("CMD.exe",
                    //    "/c adb push " + value + " " + txtSdCardLocation.Text.Trim() + "/tinnitustrio/cmm/music");

                    var mainFolder = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

                    var proc = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            WorkingDirectory = mainFolder + @"/adb",
                            FileName = "cmd.exe",
                            Arguments =
                                "/c adb push " + value + " " + txtSdCardLocation.Text.Trim() +
                                "/tinnitustrio/cmm/music",
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            CreateNoWindow = true
                        }
                    };
                    proc.Start();

                    proc.WaitForExit();
                    //if (process1 != null) process1.WaitForExit();

                }
                XtraMessageBox.Show("Transfer Complete!", "Transfer Success", MessageBoxButtons.OK);
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

        //PLAY CMES
        private void simpleButton6_Click(object sender, EventArgs e)
        {
            _player.Stop();
            var lstSelecteditemValue = lstCMESFiles.SelectedValue.ToString();
            _player.Play(lstSelecteditemValue);
        }

        private void lstCMESFolders_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var selectedProperty = (KeyValuePair<string, string>)lstCMESFolders.SelectedItem;
                var keyTxt = selectedProperty.Value;

                GetCmesFiles(keyTxt);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //CMES STOP
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            _player.Stop();
        }

        //CHECK EDIT CHANGE FOR CMES
        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkEdit1.Checked == true)
                {
                    lstCMESFiles.Enabled = false;
                    simpleButton6.Enabled = false;
                    simpleButton3.Enabled = false;
                }

                else
                {
                    lstCMESFiles.Enabled = true;
                    simpleButton6.Enabled = true;
                    simpleButton3.Enabled = true;
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

        //CMES ADD BUTTON CLICK EVENT
        private void simpleButton5_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkEdit1.Checked == true)
                {
                    lstCMESFiles.Enabled = false;
                    simpleButton6.Enabled = false;
                    simpleButton3.Enabled = false;
                    var selectedProperty = (KeyValuePair<string, string>)lstCMESFolders.SelectedItem;
                    var keyTxt = selectedProperty.Value;
                    //GetCmmFilesForTransferList(lstCMMFolders.SelectedItem.ToString());
                    GetCmesFilesForTransferList(keyTxt);
                }

                else
                {
                    lstCMESFiles.Enabled = true;
                    simpleButton6.Enabled = true;

                    var ulist = new List<KeyValuePair<string, string>>();

                    foreach (int i in lstCMESFiles.SelectedIndices)
                    {
                        var seProperty = (KeyValuePair<string, string>)lstCMESFiles.Items[i];
                        ulist.Add(seProperty);                      
                    }

                    //delete the value of the selected items
                    //var llist = new List<KeyValuePair<string, string>>();
                    //for (int j = 0; j < lstCMESFiles.Items.Count; j++)
                    //{
                    //    if (lstCMESFiles.SelectedIndex != j)
                    //    {
                    //        var seProperty = (KeyValuePair<string, string>)lstCMESFiles.Items[j];
                    //        llist.Add(seProperty);   
                    //    }
                    //}

                    lstCMESCopiedFiles.DataSource = ulist;
                    lstCMESCopiedFiles.ValueMember = "Key";
                    lstCMESCopiedFiles.DisplayMember = "Value";
                    simpleButton3.Enabled = true;
                  
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

        //cmes push 
        private void simpleButton7_Click(object sender, EventArgs e)
        {
            try
            {
                var returnValue = XtraMessageBox.Show("Have you created the directory Structure?", "Counter Check",
                  MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (!returnValue.Equals(DialogResult.Yes)) return;

                foreach (var drv in lstCMESCopiedFiles.Items)
                {
                    //var id =lstCMMCopiedFiles.ValueMember;

                    var id = (KeyValuePair<string, string>)drv;
                    var value = id.Key;
                    //var process1 = Process.Start("CMD.exe",
                    //    "/c adb push " + value + " " + txtSdCardLocation.Text.Trim() + "/tinnitustrio/cmm/environmental");
                    //if (process1 != null) process1.WaitForExit();

                    var mainFolder = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

                    var proc = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            WorkingDirectory = mainFolder + @"/adb",
                            FileName = "cmd.exe",
                            Arguments = "/c adb push " + value + " " + txtSdCardLocation.Text.Trim() + "/tinnitustrio/cmm/environmental",
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            CreateNoWindow = true
                        }
                    };
                    proc.Start();

                    proc.WaitForExit();

                }
                XtraMessageBox.Show("Transfer Complete!", "Transfer Success", MessageBoxButtons.OK);
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

        private void lstCMESCopiedFiles_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnRemoveFile_Click(object sender, EventArgs e)
        {
            lstCMMCopiedFiles.DataSource = null;
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            lstCMESCopiedFiles.DataSource = null;
        }

        private void xtraTabPage2_Paint(object sender, PaintEventArgs e)
        {

        }   
    }
}
