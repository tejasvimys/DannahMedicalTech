namespace TinnitusTrioADBBridge.UserControls
{
    partial class UC_GetUserDetails
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtDeviceDetails = new DevExpress.XtraEditors.TextEdit();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.btnGetDeviceDetails = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtDeviceDetails.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Green;
            this.labelControl1.Location = new System.Drawing.Point(68, 147);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(299, 25);
            this.labelControl1.TabIndex = 9;
            this.labelControl1.Text = "Enter Frequency of Tinnitus:";
            // 
            // txtDeviceDetails
            // 
            this.txtDeviceDetails.Location = new System.Drawing.Point(373, 149);
            this.txtDeviceDetails.Name = "txtDeviceDetails";
            this.txtDeviceDetails.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeviceDetails.Properties.Appearance.Options.UseFont = true;
            this.txtDeviceDetails.Properties.MaxLength = 5;
            this.txtDeviceDetails.Size = new System.Drawing.Size(224, 26);
            this.txtDeviceDetails.TabIndex = 8;
            this.txtDeviceDetails.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDeviceDetails_KeyPress);
            // 
            // panelControl2
            // 
            this.panelControl2.Appearance.Options.UseImage = true;
            this.panelControl2.Controls.Add(this.labelControl2);
            this.panelControl2.Location = new System.Drawing.Point(289, 19);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(408, 86);
            this.panelControl2.TabIndex = 9;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.Green;
            this.labelControl2.Location = new System.Drawing.Point(24, 30);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(358, 25);
            this.labelControl2.TabIndex = 10;
            this.labelControl2.Text = "Tinnitus Trio CMT Configurator";
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.EditValue = global::TinnitusTrioADBBridge.Properties.Resources._500x300;
            this.pictureEdit1.Location = new System.Drawing.Point(20, 19);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            this.pictureEdit1.Size = new System.Drawing.Size(249, 85);
            this.pictureEdit1.TabIndex = 15;
            // 
            // btnGetDeviceDetails
            // 
            this.btnGetDeviceDetails.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.btnGetDeviceDetails.Image = global::TinnitusTrioADBBridge.Properties.Resources.PR_TONE;
            this.btnGetDeviceDetails.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnGetDeviceDetails.Location = new System.Drawing.Point(157, 220);
            this.btnGetDeviceDetails.Name = "btnGetDeviceDetails";
            this.btnGetDeviceDetails.Size = new System.Drawing.Size(382, 64);
            this.btnGetDeviceDetails.TabIndex = 10;
            this.btnGetDeviceDetails.Text = "Generate Tone File";
            this.btnGetDeviceDetails.Click += new System.EventHandler(this.btnGetDeviceDetails_Click_1);
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.Green;
            this.labelControl3.Location = new System.Drawing.Point(603, 152);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(35, 25);
            this.labelControl3.TabIndex = 16;
            this.labelControl3.Text = "Hz.";
            // 
            // UC_GetUserDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.pictureEdit1);
            this.Controls.Add(this.btnGetDeviceDetails);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txtDeviceDetails);
            this.Name = "UC_GetUserDetails";
            this.Size = new System.Drawing.Size(718, 333);
            ((System.ComponentModel.ISupportInitialize)(this.txtDeviceDetails.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnGetDeviceDetails;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtDeviceDetails;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl3;



    }
}
