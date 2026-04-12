using System.Windows.Forms;

namespace TinnitusTrioADBBridge
{
    partial class QueryScreen
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QueryScreen));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lblDoctorname = new DevExpress.XtraEditors.LabelControl();
            this.lblDoctorId = new DevExpress.XtraEditors.LabelControl();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.pictureEdit2 = new DevExpress.XtraEditors.PictureEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.btnEnterNewPatient = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnRenewLicense = new DevExpress.XtraEditors.SimpleButton();
            this.dispPatientDetails = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Green;
            this.labelControl1.Location = new System.Drawing.Point(22, 93);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(294, 32);
            this.labelControl1.TabIndex = 5;
            this.labelControl1.Text = "Configurations for. . .";
            // 
            // lblDoctorname
            // 
            this.lblDoctorname.Location = new System.Drawing.Point(353, 107);
            this.lblDoctorname.Name = "lblDoctorname";
            this.lblDoctorname.Size = new System.Drawing.Size(63, 13);
            this.lblDoctorname.TabIndex = 7;
            this.lblDoctorname.Text = "labelControl3";
            this.lblDoctorname.Visible = false;
            // 
            // lblDoctorId
            // 
            this.lblDoctorId.Location = new System.Drawing.Point(655, 107);
            this.lblDoctorId.Name = "lblDoctorId";
            this.lblDoctorId.Size = new System.Drawing.Size(63, 13);
            this.lblDoctorId.TabIndex = 8;
            this.lblDoctorId.Text = "labelControl4";
            this.lblDoctorId.Visible = false;
            // 
            // pictureEdit2
            // 
            this.pictureEdit2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureEdit2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureEdit2.EditValue = global::TinnitusTrioADBBridge.Properties.Resources.title1;
            this.pictureEdit2.Location = new System.Drawing.Point(621, 20);
            this.pictureEdit2.Name = "pictureEdit2";
            this.pictureEdit2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit2.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit2.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            this.pictureEdit2.Size = new System.Drawing.Size(235, 67);
            this.pictureEdit2.TabIndex = 9;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Image = global::TinnitusTrioADBBridge.Properties.Resources.E_P_BTN;
            this.simpleButton1.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.simpleButton1.Location = new System.Drawing.Point(460, 144);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(396, 62);
            this.simpleButton1.TabIndex = 1;
            this.simpleButton1.Text = "Edit an Existing Patient Record";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // btnEnterNewPatient
            // 
            this.btnEnterNewPatient.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.btnEnterNewPatient.Appearance.BackColor2 = System.Drawing.Color.Transparent;
            this.btnEnterNewPatient.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnterNewPatient.Appearance.Options.UseBackColor = true;
            this.btnEnterNewPatient.Appearance.Options.UseFont = true;
            this.btnEnterNewPatient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEnterNewPatient.Image = global::TinnitusTrioADBBridge.Properties.Resources.NP_BTN;
            this.btnEnterNewPatient.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnEnterNewPatient.Location = new System.Drawing.Point(22, 144);
            this.btnEnterNewPatient.Name = "btnEnterNewPatient";
            this.btnEnterNewPatient.Size = new System.Drawing.Size(394, 62);
            this.btnEnterNewPatient.TabIndex = 0;
            this.btnEnterNewPatient.Click += new System.EventHandler(this.btnEnterNewPatient_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.simpleButton2.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton2.Appearance.Options.UseFont = true;
            this.simpleButton2.Image = global::TinnitusTrioADBBridge.Properties.Resources.EXIT;
            this.simpleButton2.Location = new System.Drawing.Point(314, 300);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(246, 61);
            this.simpleButton2.TabIndex = 6;
            this.simpleButton2.Text = "Exit Application";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureEdit1.EditValue = global::TinnitusTrioADBBridge.Properties.Resources._500x300;
            this.pictureEdit1.Location = new System.Drawing.Point(11, 20);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            this.pictureEdit1.Size = new System.Drawing.Size(235, 67);
            this.pictureEdit1.TabIndex = 10;
            // 
            // panelControl1
            // 
            this.panelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.panelControl1.Controls.Add(this.btnRenewLicense);
            this.panelControl1.Controls.Add(this.dispPatientDetails);
            this.panelControl1.Controls.Add(this.simpleButton2);
            this.panelControl1.Controls.Add(this.pictureEdit1);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.pictureEdit2);
            this.panelControl1.Controls.Add(this.lblDoctorname);
            this.panelControl1.Controls.Add(this.simpleButton1);
            this.panelControl1.Controls.Add(this.btnEnterNewPatient);
            this.panelControl1.Controls.Add(this.lblDoctorId);
            this.panelControl1.Location = new System.Drawing.Point(0, 5);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(883, 366);
            this.panelControl1.TabIndex = 11;
            // 
            // btnRenewLicense
            // 
            this.btnRenewLicense.Image = global::TinnitusTrioADBBridge.Properties.Resources.RENEWLICENSE;
            this.btnRenewLicense.Location = new System.Drawing.Point(460, 222);
            this.btnRenewLicense.Name = "btnRenewLicense";
            this.btnRenewLicense.Size = new System.Drawing.Size(394, 61);
            this.btnRenewLicense.TabIndex = 12;
            this.btnRenewLicense.Text = "Renew License";
            this.btnRenewLicense.Click += new System.EventHandler(this.btnRenewLicense_Click);
            // 
            // dispPatientDetails
            // 
            this.dispPatientDetails.Image = global::TinnitusTrioADBBridge.Properties.Resources.patientdetails;
            this.dispPatientDetails.Location = new System.Drawing.Point(22, 222);
            this.dispPatientDetails.Name = "dispPatientDetails";
            this.dispPatientDetails.Size = new System.Drawing.Size(394, 61);
            this.dispPatientDetails.TabIndex = 11;
            this.dispPatientDetails.Text = "Patient Details";
            this.dispPatientDetails.Click += new System.EventHandler(this.dispPatientDetails_Click);
            // 
            // QueryScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(883, 376);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "QueryScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Query Screen";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.QueryScreen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton btnEnterNewPatient;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.LabelControl lblDoctorname;
        private DevExpress.XtraEditors.LabelControl lblDoctorId;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private DevExpress.XtraEditors.PictureEdit pictureEdit2;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton dispPatientDetails;
        private DevExpress.XtraEditors.SimpleButton btnRenewLicense;
    
    }
}