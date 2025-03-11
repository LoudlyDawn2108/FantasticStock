namespace FantasticStock.Views
{
    partial class BackupView
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
            this.tabControl1 = new System.Windows.Forms.TabControl();

            // Backup and Restore Tab
            this.tabBackupRestore = new System.Windows.Forms.TabPage();

            // Backup Panel
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnBackupNow = new System.Windows.Forms.Button();
            this.btnSchedule = new System.Windows.Forms.Button();
            this.txtEncryptionPassword = new System.Windows.Forms.TextBox();
            this.chkEncrypt = new System.Windows.Forms.CheckBox();
            this.rbCompressionHigh = new System.Windows.Forms.RadioButton();
            this.rbCompressionNormal = new System.Windows.Forms.RadioButton();
            this.rbCompressionNone = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.chkIncludeAttachments = new System.Windows.Forms.CheckBox();
            this.btnBrowseLocation = new System.Windows.Forms.Button();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBackupLocation = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();

            // Backup History Panel
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnVerify = new System.Windows.Forms.Button();
            this.btnRestore = new System.Windows.Forms.Button();
            this.dgvBackupHistory = new System.Windows.Forms.DataGridView();

            // Restore Tab
            this.tabRestore = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnRestoreExecute = new System.Windows.Forms.Button();
            this.chkVerifyBeforeRestore = new System.Windows.Forms.CheckBox();
            this.rbMerge = new System.Windows.Forms.RadioButton();
            this.rbOverwrite = new System.Windows.Forms.RadioButton();
            this.label8 = new System.Windows.Forms.Label();
            this.lblSelectedPoint = new System.Windows.Forms.Label();
            this.dateRestorePoint = new System.Windows.Forms.DateTimePicker();
            this.lblRestorePoint = new System.Windows.Forms.Label();
            this.btnBrowseRestoreFile = new System.Windows.Forms.Button();
            this.txtRestoreFilePath = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();

            // Scheduled Backups Tab
            this.tabScheduledBackups = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnDisableSchedule = new System.Windows.Forms.Button();
            this.btnEnableSchedule = new System.Windows.Forms.Button();
            this.btnDeleteSchedule = new System.Windows.Forms.Button();
            this.btnEditSchedule = new System.Windows.Forms.Button();
            this.btnAddSchedule = new System.Windows.Forms.Button();
            this.dgvScheduledBackups = new System.Windows.Forms.DataGridView();

            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnCancelEdit = new System.Windows.Forms.Button();
            this.btnSaveSchedule = new System.Windows.Forms.Button();

            // Set up the control
            this.SuspendLayout();

            // tabControl1
            this.tabControl1.Controls.Add(this.tabBackupRestore);
            this.tabControl1.Controls.Add(this.tabRestore);
            this.tabControl1.Controls.Add(this.tabScheduledBackups);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 600);
            this.tabControl1.TabIndex = 0;

            // tabBackupRestore
            this.tabBackupRestore.Controls.Add(this.groupBox2);
            this.tabBackupRestore.Controls.Add(this.groupBox1);
            this.tabBackupRestore.Location = new System.Drawing.Point(4, 22);
            this.tabBackupRestore.Name = "tabBackupRestore";
            this.tabBackupRestore.Padding = new System.Windows.Forms.Padding(3);
            this.tabBackupRestore.Size = new System.Drawing.Size(792, 574);
            this.tabBackupRestore.TabIndex = 0;
            this.tabBackupRestore.Text = "Backup";
            this.tabBackupRestore.UseVisualStyleBackColor = true;

            // groupBox1 - Backup Panel
            this.groupBox1.Controls.Add(this.btnBackupNow);
            this.groupBox1.Controls.Add(this.btnSchedule);
            this.groupBox1.Controls.Add(this.txtEncryptionPassword);
            this.groupBox1.Controls.Add(this.chkEncrypt);
            this.groupBox1.Controls.Add(this.rbCompressionHigh);
            this.groupBox1.Controls.Add(this.rbCompressionNormal);
            this.groupBox1.Controls.Add(this.rbCompressionNone);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.chkIncludeAttachments);
            this.groupBox1.Controls.Add(this.btnBrowseLocation);
            this.groupBox1.Controls.Add(this.txtDescription);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtBackupLocation);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(776, 200);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.Text = "Create Backup";

            // Backup location label
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Backup Location:";

            // Backup location textbox
            this.txtBackupLocation.Location = new System.Drawing.Point(112, 21);
            this.txtBackupLocation.Name = "txtBackupLocation";
            this.txtBackupLocation.Size = new System.Drawing.Size(544, 20);
            this.txtBackupLocation.TabIndex = 1;

            // Browse location button
            this.btnBrowseLocation.Location = new System.Drawing.Point(664, 19);
            this.btnBrowseLocation.Name = "btnBrowseLocation";
            this.btnBrowseLocation.Size = new System.Drawing.Size(96, 23);
            this.btnBrowseLocation.TabIndex = 2;
            this.btnBrowseLocation.Text = "Browse...";
            this.btnBrowseLocation.UseVisualStyleBackColor = true;

            // Description label
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Description:";

            // Description textbox
            this.txtDescription.Location = new System.Drawing.Point(112, 49);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(648, 20);
            this.txtDescription.TabIndex = 4;

            // Include attachments checkbox
            this.chkIncludeAttachments.AutoSize = true;
            this.chkIncludeAttachments.Location = new System.Drawing.Point(112, 75);
            this.chkIncludeAttachments.Name = "chkIncludeAttachments";
            this.chkIncludeAttachments.Size = new System.Drawing.Size(126, 17);
            this.chkIncludeAttachments.TabIndex = 5;
            this.chkIncludeAttachments.Text = "Include Attachments";
            this.chkIncludeAttachments.UseVisualStyleBackColor = true;

            // Compression level label
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Compression Level:";

            // Compression radio buttons
            this.rbCompressionNone.AutoSize = true;
            this.rbCompressionNone.Location = new System.Drawing.Point(112, 96);
            this.rbCompressionNone.Name = "rbCompressionNone";
            this.rbCompressionNone.Size = new System.Drawing.Size(51, 17);
            this.rbCompressionNone.TabIndex = 7;
            this.rbCompressionNone.Text = "None";
            this.rbCompressionNone.UseVisualStyleBackColor = true;

            this.rbCompressionNormal.AutoSize = true;
            this.rbCompressionNormal.Checked = true;
            this.rbCompressionNormal.Location = new System.Drawing.Point(172, 96);
            this.rbCompressionNormal.Name = "rbCompressionNormal";
            this.rbCompressionNormal.Size = new System.Drawing.Size(58, 17);
            this.rbCompressionNormal.TabIndex = 8;
            this.rbCompressionNormal.TabStop = true;
            this.rbCompressionNormal.Text = "Normal";
            this.rbCompressionNormal.UseVisualStyleBackColor = true;

            this.rbCompressionHigh.AutoSize = true;
            this.rbCompressionHigh.Location = new System.Drawing.Point(239, 96);
            this.rbCompressionHigh.Name = "rbCompressionHigh";
            this.rbCompressionHigh.Size = new System.Drawing.Size(47, 17);
            this.rbCompressionHigh.TabIndex = 9;
            this.rbCompressionHigh.Text = "High";
            this.rbCompressionHigh.UseVisualStyleBackColor = true;

            // Encrypt checkbox
            this.chkEncrypt.AutoSize = true;
            this.chkEncrypt.Location = new System.Drawing.Point(112, 121);
            this.chkEncrypt.Name = "chkEncrypt";
            this.chkEncrypt.Size = new System.Drawing.Size(104, 17);
            this.chkEncrypt.TabIndex = 10;
            this.chkEncrypt.Text = "Encrypt Backup";
            this.chkEncrypt.UseVisualStyleBackColor = true;

            // Encryption password
            this.txtEncryptionPassword.Enabled = false;
            this.txtEncryptionPassword.Location = new System.Drawing.Point(222, 119);
            this.txtEncryptionPassword.Name = "txtEncryptionPassword";
            this.txtEncryptionPassword.PasswordChar = '*';
            this.txtEncryptionPassword.Size = new System.Drawing.Size(200, 20);
            this.txtEncryptionPassword.TabIndex = 11;

            // Schedule button
            this.btnSchedule.Location = new System.Drawing.Point(585, 154);
            this.btnSchedule.Name = "btnSchedule";
            this.btnSchedule.Size = new System.Drawing.Size(85, 28);
            this.btnSchedule.TabIndex = 12;
            this.btnSchedule.Text = "Schedule...";
            this.btnSchedule.UseVisualStyleBackColor = true;

            // Backup Now button
            this.btnBackupNow.Location = new System.Drawing.Point(676, 154);
            this.btnBackupNow.Name = "btnBackupNow";
            this.btnBackupNow.Size = new System.Drawing.Size(85, 28);
            this.btnBackupNow.TabIndex = 13;
            this.btnBackupNow.Text = "Backup Now";
            this.btnBackupNow.UseVisualStyleBackColor = true;

            // groupBox2 - Backup History
            this.groupBox2.Controls.Add(this.btnDelete);
            this.groupBox2.Controls.Add(this.btnVerify);
            this.groupBox2.Controls.Add(this.btnRestore);
            this.groupBox2.Controls.Add(this.dgvBackupHistory);
            this.groupBox2.Location = new System.Drawing.Point(8, 214);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(776, 352);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.Text = "Backup History";

            // Backup History DataGridView
            this.dgvBackupHistory.AllowUserToAddRows = false;
            this.dgvBackupHistory.AllowUserToDeleteRows = false;
            this.dgvBackupHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBackupHistory.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvBackupHistory.Location = new System.Drawing.Point(3, 16);
            this.dgvBackupHistory.MultiSelect = false;
            this.dgvBackupHistory.Name = "dgvBackupHistory";
            this.dgvBackupHistory.ReadOnly = true;
            this.dgvBackupHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBackupHistory.Size = new System.Drawing.Size(770, 299);
            this.dgvBackupHistory.TabIndex = 0;

            // Restore button
            this.btnRestore.Location = new System.Drawing.Point(585, 321);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(85, 25);
            this.btnRestore.TabIndex = 1;
            this.btnRestore.Text = "Restore";
            this.btnRestore.UseVisualStyleBackColor = true;

            // Verify button
            this.btnVerify.Location = new System.Drawing.Point(494, 321);
            this.btnVerify.Name = "btnVerify";
            this.btnVerify.Size = new System.Drawing.Size(85, 25);
            this.btnVerify.TabIndex = 2;
            this.btnVerify.Text = "Verify";
            this.btnVerify.UseVisualStyleBackColor = true;

            // Delete button
            this.btnDelete.Location = new System.Drawing.Point(676, 321);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(85, 25);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;

            // tabRestore - Restore Tab
            this.tabRestore.Controls.Add(this.groupBox5);
            this.tabRestore.Location = new System.Drawing.Point(4, 22);
            this.tabRestore.Name = "tabRestore";
            this.tabRestore.Size = new System.Drawing.Size(792, 574);
            this.tabRestore.TabIndex = 1;
            this.tabRestore.Text = "Restore";
            this.tabRestore.UseVisualStyleBackColor = true;

            // groupBox5 - Restore Options
            this.groupBox5.Controls.Add(this.btnRestoreExecute);
            this.groupBox5.Controls.Add(this.chkVerifyBeforeRestore);
            this.groupBox5.Controls.Add(this.rbMerge);
            this.groupBox5.Controls.Add(this.rbOverwrite);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.lblSelectedPoint);
            this.groupBox5.Controls.Add(this.dateRestorePoint);
            this.groupBox5.Controls.Add(this.lblRestorePoint);
            this.groupBox5.Controls.Add(this.btnBrowseRestoreFile);
            this.groupBox5.Controls.Add(this.txtRestoreFilePath);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Location = new System.Drawing.Point(8, 8);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(776, 219);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.Text = "Restore Database";

            // Backup file label
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Backup File:";

            // Backup file textbox
            this.txtRestoreFilePath.Location = new System.Drawing.Point(112, 21);
            this.txtRestoreFilePath.Name = "txtRestoreFilePath";
            this.txtRestoreFilePath.Size = new System.Drawing.Size(544, 20);
            this.txtRestoreFilePath.TabIndex = 1;

            // Browse restore file button
            this.btnBrowseRestoreFile.Location = new System.Drawing.Point(664, 19);
            this.btnBrowseRestoreFile.Name = "btnBrowseRestoreFile";
            this.btnBrowseRestoreFile.Size = new System.Drawing.Size(96, 23);
            this.btnBrowseRestoreFile.TabIndex = 2;
            this.btnBrowseRestoreFile.Text = "Browse...";
            this.btnBrowseRestoreFile.UseVisualStyleBackColor = true;

            // Restore point label
            this.lblRestorePoint.AutoSize = true;
            this.lblRestorePoint.Enabled = false;
            this.lblRestorePoint.Location = new System.Drawing.Point(16, 52);
            this.lblRestorePoint.Name = "lblRestorePoint";
            this.lblRestorePoint.Size = new System.Drawing.Size(74, 13);
            this.lblRestorePoint.TabIndex = 3;
            this.lblRestorePoint.Text = "Restore Point:";

            // Restore point date picker
            this.dateRestorePoint.Enabled = false;
            this.dateRestorePoint.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dateRestorePoint.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateRestorePoint.Location = new System.Drawing.Point(112, 49);
            this.dateRestorePoint.Name = "dateRestorePoint";
            this.dateRestorePoint.Size = new System.Drawing.Size(200, 20);
            this.dateRestorePoint.TabIndex = 4;

            // Selected point label
            this.lblSelectedPoint.AutoSize = true;
            this.lblSelectedPoint.Location = new System.Drawing.Point(318, 52);
            this.lblSelectedPoint.Name = "lblSelectedPoint";
            this.lblSelectedPoint.Size = new System.Drawing.Size(86, 13);
            this.lblSelectedPoint.TabIndex = 5;
            this.lblSelectedPoint.Text = "Selected Point: -";

            // Restore options label
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 80);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(87, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Restore Options:";

            // Overwrite radio button
            this.rbOverwrite.AutoSize = true;
            this.rbOverwrite.Checked = true;
            this.rbOverwrite.Location = new System.Drawing.Point(112, 78);
            this.rbOverwrite.Name = "rbOverwrite";
            this.rbOverwrite.Size = new System.Drawing.Size(160, 17);
            this.rbOverwrite.TabIndex = 7;
            this.rbOverwrite.TabStop = true;
            this.rbOverwrite.Text = "Overwrite existing database";
            this.rbOverwrite.UseVisualStyleBackColor = true;

            // Merge radio button
            this.rbMerge.AutoSize = true;
            this.rbMerge.Location = new System.Drawing.Point(277, 78);
            this.rbMerge.Name = "rbMerge";
            this.rbMerge.Size = new System.Drawing.Size(143, 17);
            this.rbMerge.TabIndex = 8;
            this.rbMerge.Text = "Merge with current data";
            this.rbMerge.UseVisualStyleBackColor = true;

            // Verify before restore checkbox
            this.chkVerifyBeforeRestore.AutoSize = true;
            this.chkVerifyBeforeRestore.Checked = true;
            this.chkVerifyBeforeRestore.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkVerifyBeforeRestore.Location = new System.Drawing.Point(112, 110);
            this.chkVerifyBeforeRestore.Name = "chkVerifyBeforeRestore";
            this.chkVerifyBeforeRestore.Size = new System.Drawing.Size(162, 17);
            this.chkVerifyBeforeRestore.TabIndex = 9;
            this.chkVerifyBeforeRestore.Text = "Verify backup before restore";
            this.chkVerifyBeforeRestore.UseVisualStyleBackColor = true;

            // Restore execute button
            this.btnRestoreExecute.Location = new System.Drawing.Point(664, 170);
            this.btnRestoreExecute.Name = "btnRestoreExecute";
            this.btnRestoreExecute.Size = new System.Drawing.Size(96, 28);
            this.btnRestoreExecute.TabIndex = 10;
            this.btnRestoreExecute.Text = "Restore";
            this.btnRestoreExecute.UseVisualStyleBackColor = true;

            // tabScheduledBackups - Scheduled Backups Tab
            this.tabScheduledBackups.Controls.Add(this.groupBox4);
            this.tabScheduledBackups.Controls.Add(this.groupBox3);
            this.tabScheduledBackups.Location = new System.Drawing.Point(4, 22);
            this.tabScheduledBackups.Name = "tabScheduledBackups";
            this.tabScheduledBackups.Size = new System.Drawing.Size(792, 574);
            this.tabScheduledBackups.TabIndex = 2;
            this.tabScheduledBackups.Text = "Scheduled Backups";
            this.tabScheduledBackups.UseVisualStyleBackColor = true;

            // groupBox3 - Scheduled Backups List
            this.groupBox3.Controls.Add(this.dgvScheduledBackups);
            this.groupBox3.Controls.Add(this.btnAddSchedule);
            this.groupBox3.Controls.Add(this.btnEditSchedule);
            this.groupBox3.Controls.Add(this.btnDeleteSchedule);
            this.groupBox3.Controls.Add(this.btnEnableSchedule);
            this.groupBox3.Controls.Add(this.btnDisableSchedule);
            this.groupBox3.Location = new System.Drawing.Point(8, 8);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(776, 340);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.Text = "Scheduled Backups";

            // Scheduled Backups DataGridView
            this.dgvScheduledBackups.AllowUserToAddRows = false;
            this.dgvScheduledBackups.AllowUserToDeleteRows = false;
            this.dgvScheduledBackups.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvScheduledBackups.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvScheduledBackups.Location = new System.Drawing.Point(3, 16);
            this.dgvScheduledBackups.MultiSelect = false;
            this.dgvScheduledBackups.Name = "dgvScheduledBackups";
            this.dgvScheduledBackups.ReadOnly = true;
            this.dgvScheduledBackups.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvScheduledBackups.Size = new System.Drawing.Size(770, 287);
            this.dgvScheduledBackups.TabIndex = 0;

            // Add Schedule button
            this.btnAddSchedule.Location = new System.Drawing.Point(312, 309);
            this.btnAddSchedule.Name = "btnAddSchedule";
            this.btnAddSchedule.Size = new System.Drawing.Size(85, 25);
            this.btnAddSchedule.TabIndex = 1;
            this.btnAddSchedule.Text = "Add";
            this.btnAddSchedule.UseVisualStyleBackColor = true;

            // Edit Schedule button
            this.btnEditSchedule.Location = new System.Drawing.Point(403, 309);
            this.btnEditSchedule.Name = "btnEditSchedule";
            this.btnEditSchedule.Size = new System.Drawing.Size(85, 25);
            this.btnEditSchedule.TabIndex = 2;
            this.btnEditSchedule.Text = "Edit";
            this.btnEditSchedule.UseVisualStyleBackColor = true;

            // Delete Schedule button
            this.btnDeleteSchedule.Location = new System.Drawing.Point(494, 309);
            this.btnDeleteSchedule.Name = "btnDeleteSchedule";
            this.btnDeleteSchedule.Size = new System.Drawing.Size(85, 25);
            this.btnDeleteSchedule.TabIndex = 3;
            this.btnDeleteSchedule.Text = "Delete";
            this.btnDeleteSchedule.UseVisualStyleBackColor = true;

            // Enable Schedule button
            this.btnEnableSchedule.Location = new System.Drawing.Point(585, 309);
            this.btnEnableSchedule.Name = "btnEnableSchedule";
            this.btnEnableSchedule.Size = new System.Drawing.Size(85, 25);
            this.btnEnableSchedule.TabIndex = 4;
            this.btnEnableSchedule.Text = "Enable";
            this.btnEnableSchedule.UseVisualStyleBackColor = true;

            // Disable Schedule button
            this.btnDisableSchedule.Location = new System.Drawing.Point(676, 309);
            this.btnDisableSchedule.Name = "btnDisableSchedule";
            this.btnDisableSchedule.Size = new System.Drawing.Size(85, 25);
            this.btnDisableSchedule.TabIndex = 5;
            this.btnDisableSchedule.Text = "Disable";
            this.btnDisableSchedule.UseVisualStyleBackColor = true;

            // groupBox4 - Schedule Edit Panel
            this.groupBox4.Controls.Add(this.btnSaveSchedule);
            this.groupBox4.Controls.Add(this.btnCancelEdit);
            this.groupBox4.Enabled = false; // Initially disabled until editing
            this.groupBox4.Location = new System.Drawing.Point(8, 354);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(776, 212);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.Text = "Schedule Details";

            // Save Schedule button
            this.btnSaveSchedule.Location = new System.Drawing.Point(585, 183);
            this.btnSaveSchedule.Name = "btnSaveSchedule";
            this.btnSaveSchedule.Size = new System.Drawing.Size(85, 23);
            this.btnSaveSchedule.TabIndex = 0;
            this.btnSaveSchedule.Text = "Save";
            this.btnSaveSchedule.UseVisualStyleBackColor = true;

            // Cancel Edit button
            this.btnCancelEdit.Location = new System.Drawing.Point(676, 183);
            this.btnCancelEdit.Name = "btnCancelEdit";
            this.btnCancelEdit.Size = new System.Drawing.Size(85, 23);
            this.btnCancelEdit.TabIndex = 1;
            this.btnCancelEdit.Text = "Cancel";
            this.btnCancelEdit.UseVisualStyleBackColor = true;

            // BackupView
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "BackupView";
            this.Size = new System.Drawing.Size(800, 600);

            this.tabControl1.ResumeLayout(false);
            this.tabBackupRestore.ResumeLayout(false);
            this.tabRestore.ResumeLayout(false);
            this.tabScheduledBackups.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBackupHistory)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvScheduledBackups)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabBackupRestore;
        private System.Windows.Forms.TabPage tabRestore;
        private System.Windows.Forms.TabPage tabScheduledBackups;

        // Backup panel
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBackupLocation;
        private System.Windows.Forms.Button btnBrowseLocation;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.CheckBox chkIncludeAttachments;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rbCompressionNone;
        private System.Windows.Forms.RadioButton rbCompressionNormal;
        private System.Windows.Forms.RadioButton rbCompressionHigh;
        private System.Windows.Forms.CheckBox chkEncrypt;
        private System.Windows.Forms.TextBox txtEncryptionPassword;
        private System.Windows.Forms.Button btnSchedule;
        private System.Windows.Forms.Button btnBackupNow;

        // Backup History panel
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvBackupHistory;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.Button btnVerify;
        private System.Windows.Forms.Button btnDelete;

        // Restore tab
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtRestoreFilePath;
        private System.Windows.Forms.Button btnBrowseRestoreFile;
        private System.Windows.Forms.Label lblRestorePoint;
        private System.Windows.Forms.DateTimePicker dateRestorePoint;
        private System.Windows.Forms.Label lblSelectedPoint;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RadioButton rbOverwrite;
        private System.Windows.Forms.RadioButton rbMerge;
        private System.Windows.Forms.CheckBox chkVerifyBeforeRestore;
        private System.Windows.Forms.Button btnRestoreExecute;

        // Scheduled Backups tab
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgvScheduledBackups;
        private System.Windows.Forms.Button btnAddSchedule;
        private System.Windows.Forms.Button btnEditSchedule;
        private System.Windows.Forms.Button btnDeleteSchedule;
        private System.Windows.Forms.Button btnEnableSchedule;
        private System.Windows.Forms.Button btnDisableSchedule;

        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnSaveSchedule;
        private System.Windows.Forms.Button btnCancelEdit;
    }
}
