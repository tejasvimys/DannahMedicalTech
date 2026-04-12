namespace TinnitusTrioADBBridge.UserControls
{
    partial class UC_DeactivatePatient
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
            this.btnInstallCMTApp = new DevExpress.XtraEditors.SimpleButton();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.pictureEdit2 = new DevExpress.XtraEditors.PictureEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit2.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnInstallCMTApp
            // 
            this.btnInstallCMTApp.Appearance.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInstallCMTApp.Appearance.ForeColor = System.Drawing.Color.Green;
            this.btnInstallCMTApp.Appearance.Options.UseFont = true;
            this.btnInstallCMTApp.Appearance.Options.UseForeColor = true;
            this.btnInstallCMTApp.Image = global::TinnitusTrioADBBridge.Properties.Resources.DA_APPS;
            this.btnInstallCMTApp.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnInstallCMTApp.Location = new System.Drawing.Point(126, 177);
            this.btnInstallCMTApp.Name = "btnInstallCMTApp";
            this.btnInstallCMTApp.Size = new System.Drawing.Size(395, 64);
            this.btnInstallCMTApp.TabIndex = 8;
            this.btnInstallCMTApp.Text = "DE ACTIVATE APPS";
            this.btnInstallCMTApp.Click += new System.EventHandler(this.btnInstallCMTApp_Click);
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureEdit1.EditValue = global::TinnitusTrioADBBridge.Properties.Resources._500x300;
            this.pictureEdit1.Location = new System.Drawing.Point(44, 18);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            this.pictureEdit1.Size = new System.Drawing.Size(235, 67);
            this.pictureEdit1.TabIndex = 18;
            // 
            // pictureEdit2
            // 
            this.pictureEdit2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureEdit2.EditValue = global::TinnitusTrioADBBridge.Properties.Resources.title1;
            this.pictureEdit2.Location = new System.Drawing.Point(380, 18);
            this.pictureEdit2.Name = "pictureEdit2";
            this.pictureEdit2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit2.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit2.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            this.pictureEdit2.Size = new System.Drawing.Size(235, 67);
            this.pictureEdit2.TabIndex = 17;
            // 
            // UC_DeactivatePatient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.pictureEdit1);
            this.Controls.Add(this.btnInstallCMTApp);
            this.Controls.Add(this.pictureEdit2);
            this.Name = "UC_DeactivatePatient";
            this.Size = new System.Drawing.Size(661, 332);
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit2.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnInstallCMTApp;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraEditors.PictureEdit pictureEdit2;
    }
}
