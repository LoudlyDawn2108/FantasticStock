namespace FantasticStock.Views
{
    partial class SystemConfigurationView
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
            components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabCompanyInfo = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnBrowseLogo = new System.Windows.Forms.Button();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.btnSaveCompanyInfo = new System.Windows.Forms.Button();
            this.dtpEstablishedDate = new System.Windows.Forms.DateTimePicker();
            this.lblEstablishedDate = new System.Windows.Forms.Label();
            this.txtTaxID = new System.Windows.Forms.TextBox();
            this.lblTaxID = new System.Windows.Forms.Label();
            this.txtWebsite = new System.Windows.Forms.TextBox();
            this.lblWebsite = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.txtCountry = new System.Windows.Forms.TextBox();
            this.lblCountry = new System.Windows.Forms.Label();
            this.txtPostalCode = new System.Windows.Forms.TextBox();
            this.lblPostalCode = new System.Windows.Forms.Label();
            this.txtState = new System.Windows.Forms.TextBox();
            this.lblState = new System.Windows.Forms.Label();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.lblCity = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.txtCompanyName = new System.Windows.Forms.TextBox();
            this.lblCompanyName = new System.Windows.Forms.Label();
            this.tabSettings = new System.Windows.Forms.TabPage();
            this.tabControlSettings = new System.Windows.Forms.TabControl();
            this.tabGeneralSettings = new System.Windows.Forms.TabPage();
            this.dgvGeneralSettings = new System.Windows.Forms.DataGridView();
            this.tabInventorySettings = new System.Windows.Forms.TabPage();
            this.dgvInventorySettings = new System.Windows.Forms.DataGridView();
            this.tabSalesSettings = new System.Windows.Forms.TabPage();
            this.dgvSalesSettings = new System.Windows.Forms.DataGridView();
            this.tabFinancialSettings = new System.Windows.Forms.TabPage();
            this.dgvFinancialSettings = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnResetSettings = new System.Windows.Forms.Button();
            this.btnApplySettings = new System.Windows.Forms.Button();

            // Set up basic layout
            this.tabControl1.Controls.Add(this.tabCompanyInfo);
            this.tabControl1.Controls.Add(this.tabSettings);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1016, 626);
            this.tabControl1.TabIndex = 0;

            // Configure company info tab
            this.tabCompanyInfo.Controls.Add(this.groupBox1);
            this.tabCompanyInfo.Location = new System.Drawing.Point(4, 24);
            this.tabCompanyInfo.Name = "tabCompanyInfo";
            this.tabCompanyInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabCompanyInfo.Size = new System.Drawing.Size(1008, 598);
            this.tabCompanyInfo.TabIndex = 0;
            this.tabCompanyInfo.Text = "Company Information";
            this.tabCompanyInfo.UseVisualStyleBackColor = true;

            // Configure settings tab
            this.tabSettings.Controls.Add(this.tabControlSettings);
            this.tabSettings.Controls.Add(this.panel1);
            this.tabSettings.Location = new System.Drawing.Point(4, 24);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabSettings.Size = new System.Drawing.Size(1008, 598);
            this.tabSettings.TabIndex = 1;
            this.tabSettings.Text = "System Settings";
            this.tabSettings.UseVisualStyleBackColor = true;

            // Settings tabs
            this.tabControlSettings.Controls.Add(this.tabGeneralSettings);
            this.tabControlSettings.Controls.Add(this.tabInventorySettings);
            this.tabControlSettings.Controls.Add(this.tabSalesSettings);
            this.tabControlSettings.Controls.Add(this.tabFinancialSettings);
            this.tabControlSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlSettings.Location = new System.Drawing.Point(3, 3);
            this.tabControlSettings.Name = "tabControlSettings";
            this.tabControlSettings.SelectedIndex = 0;
            this.tabControlSettings.Size = new System.Drawing.Size(1002, 553);
            this.tabControlSettings.TabIndex = 0;

            // Settings panel with buttons
            this.panel1.Controls.Add(this.btnResetSettings);
            this.panel1.Controls.Add(this.btnApplySettings);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(3, 556);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1002, 39);
            this.panel1.TabIndex = 1;

            // Add the tab control to the main form
            this.Controls.Add(this.tabControl1);
            this.Name = "SystemConfigurationView";
            this.Size = new System.Drawing.Size(1016, 626);

            // Additional components setup would go here
            // For brevity, I'm omitting detailed control setup code that would be generated by the designer

            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabCompanyInfo;
        private System.Windows.Forms.TabPage tabSettings;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSaveCompanyInfo;
        private System.Windows.Forms.DateTimePicker dtpEstablishedDate;
        private System.Windows.Forms.Label lblEstablishedDate;
        private System.Windows.Forms.TextBox txtTaxID;
        private System.Windows.Forms.Label lblTaxID;
        private System.Windows.Forms.TextBox txtWebsite;
        private System.Windows.Forms.Label lblWebsite;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.TextBox txtCountry;
        private System.Windows.Forms.Label lblCountry;
        private System.Windows.Forms.TextBox txtPostalCode;
        private System.Windows.Forms.Label lblPostalCode;
        private System.Windows.Forms.TextBox txtState;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.TextBox txtCity;
        private System.Windows.Forms.Label lblCity;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.TextBox txtCompanyName;
        private System.Windows.Forms.Label lblCompanyName;
        private System.Windows.Forms.Button btnBrowseLogo;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.TabControl tabControlSettings;
        private System.Windows.Forms.TabPage tabGeneralSettings;
        private System.Windows.Forms.TabPage tabInventorySettings;
        private System.Windows.Forms.TabPage tabSalesSettings;
        private System.Windows.Forms.TabPage tabFinancialSettings;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnResetSettings;
        private System.Windows.Forms.Button btnApplySettings;
        private System.Windows.Forms.DataGridView dgvGeneralSettings;
        private System.Windows.Forms.DataGridView dgvInventorySettings;
        private System.Windows.Forms.DataGridView dgvSalesSettings;
        private System.Windows.Forms.DataGridView dgvFinancialSettings;
    }
}
