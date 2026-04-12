namespace TinnitusTrioADBBridge.UserControls
{
    partial class UC_PushFiles
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtSdCardLocation = new DevExpress.XtraEditors.TextEdit();
            this.btnCreateDirectoryStructure = new DevExpress.XtraEditors.SimpleButton();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.chkTransferFolder = new DevExpress.XtraEditors.CheckEdit();
            this.lstCMMCopiedFiles = new System.Windows.Forms.ListBox();
            this.lstCMMFiles = new System.Windows.Forms.ListBox();
            this.lstCMMFolders = new System.Windows.Forms.ListBox();
            this.btnStopmp3 = new DevExpress.XtraEditors.SimpleButton();
            this.btnRemoveFile = new DevExpress.XtraEditors.SimpleButton();
            this.btnPushFiles = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.checkEdit1 = new DevExpress.XtraEditors.CheckEdit();
            this.lstCMESCopiedFiles = new System.Windows.Forms.ListBox();
            this.lstCMESFiles = new System.Windows.Forms.ListBox();
            this.lstCMESFolders = new System.Windows.Forms.ListBox();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton4 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton5 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton6 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton7 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSdCardLocation.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkTransferFolder.Properties)).BeginInit();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.txtSdCardLocation);
            this.panelControl1.Controls.Add(this.btnCreateDirectoryStructure);
            this.panelControl1.Location = new System.Drawing.Point(3, 3);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1096, 54);
            this.panelControl1.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(44, 20);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(86, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "SD Card Location:";
            // 
            // txtSdCardLocation
            // 
            this.txtSdCardLocation.Location = new System.Drawing.Point(136, 17);
            this.txtSdCardLocation.Name = "txtSdCardLocation";
            this.txtSdCardLocation.Size = new System.Drawing.Size(704, 20);
            this.txtSdCardLocation.TabIndex = 1;
            // 
            // btnCreateDirectoryStructure
            // 
            this.btnCreateDirectoryStructure.Image = global::TinnitusTrioADBBridge.Properties.Resources.btns0001;
            this.btnCreateDirectoryStructure.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnCreateDirectoryStructure.Location = new System.Drawing.Point(908, 9);
            this.btnCreateDirectoryStructure.Name = "btnCreateDirectoryStructure";
            this.btnCreateDirectoryStructure.Size = new System.Drawing.Size(154, 34);
            this.btnCreateDirectoryStructure.TabIndex = 0;
            this.btnCreateDirectoryStructure.Text = "Create Directory Structure";
            this.btnCreateDirectoryStructure.Click += new System.EventHandler(this.btnCreateDirectoryStructure_Click);
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Location = new System.Drawing.Point(3, 63);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(1101, 466);
            this.xtraTabControl1.TabIndex = 1;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.panelControl2);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(1095, 438);
            this.xtraTabPage1.Text = "Push Files for CMM";
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.panelControl2.Controls.Add(this.chkTransferFolder);
            this.panelControl2.Controls.Add(this.lstCMMCopiedFiles);
            this.panelControl2.Controls.Add(this.lstCMMFiles);
            this.panelControl2.Controls.Add(this.lstCMMFolders);
            this.panelControl2.Controls.Add(this.btnStopmp3);
            this.panelControl2.Controls.Add(this.btnRemoveFile);
            this.panelControl2.Controls.Add(this.btnPushFiles);
            this.panelControl2.Controls.Add(this.simpleButton2);
            this.panelControl2.Controls.Add(this.simpleButton1);
            this.panelControl2.Location = new System.Drawing.Point(3, 3);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1089, 432);
            this.panelControl2.TabIndex = 0;
            // 
            // chkTransferFolder
            // 
            this.chkTransferFolder.Location = new System.Drawing.Point(295, 22);
            this.chkTransferFolder.Name = "chkTransferFolder";
            this.chkTransferFolder.Properties.Caption = "Transfer Full Folder";
            this.chkTransferFolder.Size = new System.Drawing.Size(121, 19);
            this.chkTransferFolder.TabIndex = 11;
            this.chkTransferFolder.CheckedChanged += new System.EventHandler(this.chkTransferFolder_CheckedChanged);
            // 
            // lstCMMCopiedFiles
            // 
            this.lstCMMCopiedFiles.FormattingEnabled = true;
            this.lstCMMCopiedFiles.Location = new System.Drawing.Point(803, 55);
            this.lstCMMCopiedFiles.Name = "lstCMMCopiedFiles";
            this.lstCMMCopiedFiles.Size = new System.Drawing.Size(255, 355);
            this.lstCMMCopiedFiles.TabIndex = 10;
            // 
            // lstCMMFiles
            // 
            this.lstCMMFiles.FormattingEnabled = true;
            this.lstCMMFiles.Location = new System.Drawing.Point(295, 55);
            this.lstCMMFiles.Name = "lstCMMFiles";
            this.lstCMMFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lstCMMFiles.Size = new System.Drawing.Size(255, 355);
            this.lstCMMFiles.TabIndex = 9;
            // 
            // lstCMMFolders
            // 
            this.lstCMMFolders.FormattingEnabled = true;
            this.lstCMMFolders.Location = new System.Drawing.Point(6, 55);
            this.lstCMMFolders.Name = "lstCMMFolders";
            this.lstCMMFolders.Size = new System.Drawing.Size(255, 355);
            this.lstCMMFolders.TabIndex = 8;
            this.lstCMMFolders.SelectedIndexChanged += new System.EventHandler(this.lstCMMFolders_SelectedIndexChanged);
            // 
            // btnStopmp3
            // 
            this.btnStopmp3.Image = global::TinnitusTrioADBBridge.Properties.Resources.btns0004;
            this.btnStopmp3.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnStopmp3.Location = new System.Drawing.Point(599, 186);
            this.btnStopmp3.Name = "btnStopmp3";
            this.btnStopmp3.Size = new System.Drawing.Size(155, 35);
            this.btnStopmp3.TabIndex = 7;
            this.btnStopmp3.Text = "Stop File";
            this.btnStopmp3.Click += new System.EventHandler(this.btnStopmp3_Click);
            // 
            // btnRemoveFile
            // 
            this.btnRemoveFile.Image = global::TinnitusTrioADBBridge.Properties.Resources.btns0006;
            this.btnRemoveFile.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnRemoveFile.Location = new System.Drawing.Point(599, 315);
            this.btnRemoveFile.Name = "btnRemoveFile";
            this.btnRemoveFile.Size = new System.Drawing.Size(155, 35);
            this.btnRemoveFile.TabIndex = 6;
            this.btnRemoveFile.Text = "Remove All Files";
            this.btnRemoveFile.Click += new System.EventHandler(this.btnRemoveFile_Click);
            // 
            // btnPushFiles
            // 
            this.btnPushFiles.Image = global::TinnitusTrioADBBridge.Properties.Resources.btns0005;
            this.btnPushFiles.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnPushFiles.Location = new System.Drawing.Point(599, 247);
            this.btnPushFiles.Name = "btnPushFiles";
            this.btnPushFiles.Size = new System.Drawing.Size(155, 35);
            this.btnPushFiles.TabIndex = 5;
            this.btnPushFiles.Text = "Add File";
            this.btnPushFiles.Click += new System.EventHandler(this.btnPushFiles_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Image = global::TinnitusTrioADBBridge.Properties.Resources.btns0003;
            this.simpleButton2.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.simpleButton2.Location = new System.Drawing.Point(599, 125);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(155, 34);
            this.simpleButton2.TabIndex = 4;
            this.simpleButton2.Text = "Play File";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Image = global::TinnitusTrioADBBridge.Properties.Resources.btns0002;
            this.simpleButton1.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.simpleButton1.Location = new System.Drawing.Point(904, 13);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(154, 36);
            this.simpleButton1.TabIndex = 2;
            this.simpleButton1.Text = "Push Files";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.checkEdit1);
            this.xtraTabPage2.Controls.Add(this.lstCMESCopiedFiles);
            this.xtraTabPage2.Controls.Add(this.lstCMESFiles);
            this.xtraTabPage2.Controls.Add(this.lstCMESFolders);
            this.xtraTabPage2.Controls.Add(this.simpleButton3);
            this.xtraTabPage2.Controls.Add(this.simpleButton4);
            this.xtraTabPage2.Controls.Add(this.simpleButton5);
            this.xtraTabPage2.Controls.Add(this.simpleButton6);
            this.xtraTabPage2.Controls.Add(this.simpleButton7);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(1095, 438);
            this.xtraTabPage2.Text = "Push Files for CMES";
            this.xtraTabPage2.Paint += new System.Windows.Forms.PaintEventHandler(this.xtraTabPage2_Paint);
            // 
            // checkEdit1
            // 
            this.checkEdit1.Location = new System.Drawing.Point(303, 34);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Caption = "Transfer Full Folder";
            this.checkEdit1.Size = new System.Drawing.Size(121, 19);
            this.checkEdit1.TabIndex = 20;
            this.checkEdit1.CheckedChanged += new System.EventHandler(this.checkEdit1_CheckedChanged);
            // 
            // lstCMESCopiedFiles
            // 
            this.lstCMESCopiedFiles.FormattingEnabled = true;
            this.lstCMESCopiedFiles.Location = new System.Drawing.Point(806, 67);
            this.lstCMESCopiedFiles.Name = "lstCMESCopiedFiles";
            this.lstCMESCopiedFiles.Size = new System.Drawing.Size(255, 355);
            this.lstCMESCopiedFiles.TabIndex = 19;
            this.lstCMESCopiedFiles.SelectedIndexChanged += new System.EventHandler(this.lstCMESCopiedFiles_SelectedIndexChanged);
            // 
            // lstCMESFiles
            // 
            this.lstCMESFiles.FormattingEnabled = true;
            this.lstCMESFiles.Location = new System.Drawing.Point(303, 67);
            this.lstCMESFiles.Name = "lstCMESFiles";
            this.lstCMESFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lstCMESFiles.Size = new System.Drawing.Size(255, 355);
            this.lstCMESFiles.TabIndex = 18;
            // 
            // lstCMESFolders
            // 
            this.lstCMESFolders.FormattingEnabled = true;
            this.lstCMESFolders.Location = new System.Drawing.Point(14, 67);
            this.lstCMESFolders.Name = "lstCMESFolders";
            this.lstCMESFolders.Size = new System.Drawing.Size(255, 355);
            this.lstCMESFolders.TabIndex = 17;
            this.lstCMESFolders.SelectedIndexChanged += new System.EventHandler(this.lstCMESFolders_SelectedIndexChanged);
            // 
            // simpleButton3
            // 
            this.simpleButton3.Image = global::TinnitusTrioADBBridge.Properties.Resources.btns0004;
            this.simpleButton3.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.simpleButton3.Location = new System.Drawing.Point(607, 200);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(155, 34);
            this.simpleButton3.TabIndex = 16;
            this.simpleButton3.Text = "Stop File";
            this.simpleButton3.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // simpleButton4
            // 
            this.simpleButton4.Image = global::TinnitusTrioADBBridge.Properties.Resources.btns0006;
            this.simpleButton4.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.simpleButton4.Location = new System.Drawing.Point(607, 332);
            this.simpleButton4.Name = "simpleButton4";
            this.simpleButton4.Size = new System.Drawing.Size(155, 34);
            this.simpleButton4.TabIndex = 15;
            this.simpleButton4.Text = "Remove All Files";
            this.simpleButton4.Click += new System.EventHandler(this.simpleButton4_Click);
            // 
            // simpleButton5
            // 
            this.simpleButton5.Image = global::TinnitusTrioADBBridge.Properties.Resources.btns0005;
            this.simpleButton5.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.simpleButton5.Location = new System.Drawing.Point(607, 266);
            this.simpleButton5.Name = "simpleButton5";
            this.simpleButton5.Size = new System.Drawing.Size(155, 34);
            this.simpleButton5.TabIndex = 14;
            this.simpleButton5.Text = "Add File";
            this.simpleButton5.Click += new System.EventHandler(this.simpleButton5_Click);
            // 
            // simpleButton6
            // 
            this.simpleButton6.Image = global::TinnitusTrioADBBridge.Properties.Resources.btns0003;
            this.simpleButton6.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.simpleButton6.Location = new System.Drawing.Point(607, 133);
            this.simpleButton6.Name = "simpleButton6";
            this.simpleButton6.Size = new System.Drawing.Size(155, 35);
            this.simpleButton6.TabIndex = 13;
            this.simpleButton6.Text = "Play File";
            this.simpleButton6.Click += new System.EventHandler(this.simpleButton6_Click);
            // 
            // simpleButton7
            // 
            this.simpleButton7.Image = global::TinnitusTrioADBBridge.Properties.Resources.btns0002;
            this.simpleButton7.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.simpleButton7.Location = new System.Drawing.Point(896, 16);
            this.simpleButton7.Name = "simpleButton7";
            this.simpleButton7.Size = new System.Drawing.Size(153, 37);
            this.simpleButton7.TabIndex = 12;
            this.simpleButton7.Text = "Push Files";
            this.simpleButton7.Click += new System.EventHandler(this.simpleButton7_Click);
            // 
            // UC_PushFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xtraTabControl1);
            this.Controls.Add(this.panelControl1);
            this.Name = "UC_PushFiles";
            this.Size = new System.Drawing.Size(1124, 532);
            this.Load += new System.EventHandler(this.UC_PushFiles_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSdCardLocation.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkTransferFolder.Properties)).EndInit();
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtSdCardLocation;
        private DevExpress.XtraEditors.SimpleButton btnCreateDirectoryStructure;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton btnRemoveFile;
        private DevExpress.XtraEditors.SimpleButton btnPushFiles;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton btnStopmp3;
        private System.Windows.Forms.ListBox lstCMMFiles;
        private System.Windows.Forms.ListBox lstCMMFolders;
        private System.Windows.Forms.ListBox lstCMMCopiedFiles;
        private DevExpress.XtraEditors.CheckEdit chkTransferFolder;
        private DevExpress.XtraEditors.CheckEdit checkEdit1;
        private System.Windows.Forms.ListBox lstCMESCopiedFiles;
        private System.Windows.Forms.ListBox lstCMESFiles;
        private System.Windows.Forms.ListBox lstCMESFolders;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraEditors.SimpleButton simpleButton4;
        private DevExpress.XtraEditors.SimpleButton simpleButton5;
        private DevExpress.XtraEditors.SimpleButton simpleButton6;
        private DevExpress.XtraEditors.SimpleButton simpleButton7;
    }
}
