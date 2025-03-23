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
            this.tabBackupRestore = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnVerify = new System.Windows.Forms.Button();
            this.btnRestore = new System.Windows.Forms.Button();
            this.dgvBackupHistory = new System.Windows.Forms.DataGridView();
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
            this.tabScheduledBackups = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dtpScheduleTime = new System.Windows.Forms.DateTimePicker();
            this.cmbScheduleType = new System.Windows.Forms.ComboBox();
            this.lblDaysofWeek = new System.Windows.Forms.Label();
            this.lblScheduleTime = new System.Windows.Forms.Label();
            this.lblScheduleType = new System.Windows.Forms.Label();
            this.btnSaveSchedule = new System.Windows.Forms.Button();
            this.btnCancelEdit = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgvScheduledBackups = new System.Windows.Forms.DataGridView();
            this.btnAddSchedule = new System.Windows.Forms.Button();
            this.btnEditSchedule = new System.Windows.Forms.Button();
            this.btnDeleteSchedule = new System.Windows.Forms.Button();
            this.btnEnableSchedule = new System.Windows.Forms.Button();
            this.btnDisableSchedule = new System.Windows.Forms.Button();
            this.clbDaysofWeek = new System.Windows.Forms.CheckedListBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tabControl1.SuspendLayout();
            this.tabBackupRestore.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBackupHistory)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabRestore.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tabScheduledBackups.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvScheduledBackups)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabBackupRestore);
            this.tabControl1.Controls.Add(this.tabRestore);
            this.tabControl1.Controls.Add(this.tabScheduledBackups);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 600);
            this.tabControl1.TabIndex = 0;
            // 
            // tabBackupRestore
            // 
            this.tabBackupRestore.Controls.Add(this.tableLayoutPanel1);
            this.tabBackupRestore.Location = new System.Drawing.Point(4, 22);
            this.tabBackupRestore.Name = "tabBackupRestore";
            this.tabBackupRestore.Padding = new System.Windows.Forms.Padding(3);
            this.tabBackupRestore.Size = new System.Drawing.Size(792, 574);
            this.tabBackupRestore.TabIndex = 0;
            this.tabBackupRestore.Text = "Backup";
            this.tabBackupRestore.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnDelete);
            this.groupBox2.Controls.Add(this.btnVerify);
            this.groupBox2.Controls.Add(this.btnRestore);
            this.groupBox2.Controls.Add(this.dgvBackupHistory);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 213);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(780, 352);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Backup History";
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Location = new System.Drawing.Point(676, 321);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(85, 25);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnVerify
            // 
            this.btnVerify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVerify.Location = new System.Drawing.Point(494, 321);
            this.btnVerify.Name = "btnVerify";
            this.btnVerify.Size = new System.Drawing.Size(85, 25);
            this.btnVerify.TabIndex = 2;
            this.btnVerify.Text = "Verify";
            this.btnVerify.UseVisualStyleBackColor = true;
            // 
            // btnRestore
            // 
            this.btnRestore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRestore.Location = new System.Drawing.Point(585, 321);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(85, 25);
            this.btnRestore.TabIndex = 1;
            this.btnRestore.Text = "Restore";
            this.btnRestore.UseVisualStyleBackColor = true;
            // 
            // dgvBackupHistory
            // 
            this.dgvBackupHistory.AllowUserToAddRows = false;
            this.dgvBackupHistory.AllowUserToDeleteRows = false;
            this.dgvBackupHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvBackupHistory.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvBackupHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBackupHistory.GridColor = System.Drawing.Color.Cyan;
            this.dgvBackupHistory.Location = new System.Drawing.Point(3, 16);
            this.dgvBackupHistory.MultiSelect = false;
            this.dgvBackupHistory.Name = "dgvBackupHistory";
            this.dgvBackupHistory.ReadOnly = true;
            this.dgvBackupHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBackupHistory.Size = new System.Drawing.Size(770, 299);
            this.dgvBackupHistory.TabIndex = 0;
            // 
            // groupBox1
            // 
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
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(780, 204);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Create Backup";
            // 
            // btnBackupNow
            // 
            this.btnBackupNow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBackupNow.Location = new System.Drawing.Point(676, 161);
            this.btnBackupNow.Name = "btnBackupNow";
            this.btnBackupNow.Size = new System.Drawing.Size(85, 28);
            this.btnBackupNow.TabIndex = 13;
            this.btnBackupNow.Text = "Backup Now";
            this.btnBackupNow.UseVisualStyleBackColor = true;
            // 
            // btnSchedule
            // 
            this.btnSchedule.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSchedule.Location = new System.Drawing.Point(585, 161);
            this.btnSchedule.Name = "btnSchedule";
            this.btnSchedule.Size = new System.Drawing.Size(85, 28);
            this.btnSchedule.TabIndex = 12;
            this.btnSchedule.Text = "Schedule...";
            this.btnSchedule.UseVisualStyleBackColor = true;
            // 
            // txtEncryptionPassword
            // 
            this.txtEncryptionPassword.Enabled = false;
            this.txtEncryptionPassword.Location = new System.Drawing.Point(230, 128);
            this.txtEncryptionPassword.Name = "txtEncryptionPassword";
            this.txtEncryptionPassword.PasswordChar = '*';
            this.txtEncryptionPassword.Size = new System.Drawing.Size(200, 20);
            this.txtEncryptionPassword.TabIndex = 11;
            // 
            // chkEncrypt
            // 
            this.chkEncrypt.AutoSize = true;
            this.chkEncrypt.Location = new System.Drawing.Point(120, 130);
            this.chkEncrypt.Name = "chkEncrypt";
            this.chkEncrypt.Size = new System.Drawing.Size(102, 17);
            this.chkEncrypt.TabIndex = 10;
            this.chkEncrypt.Text = "Encrypt Backup";
            this.chkEncrypt.UseVisualStyleBackColor = true;
            // 
            // rbCompressionHigh
            // 
            this.rbCompressionHigh.AutoSize = true;
            this.rbCompressionHigh.Location = new System.Drawing.Point(247, 105);
            this.rbCompressionHigh.Name = "rbCompressionHigh";
            this.rbCompressionHigh.Size = new System.Drawing.Size(47, 17);
            this.rbCompressionHigh.TabIndex = 9;
            this.rbCompressionHigh.Text = "High";
            this.rbCompressionHigh.UseVisualStyleBackColor = true;
            // 
            // rbCompressionNormal
            // 
            this.rbCompressionNormal.AutoSize = true;
            this.rbCompressionNormal.Checked = true;
            this.rbCompressionNormal.Location = new System.Drawing.Point(180, 105);
            this.rbCompressionNormal.Name = "rbCompressionNormal";
            this.rbCompressionNormal.Size = new System.Drawing.Size(58, 17);
            this.rbCompressionNormal.TabIndex = 8;
            this.rbCompressionNormal.TabStop = true;
            this.rbCompressionNormal.Text = "Normal";
            this.rbCompressionNormal.UseVisualStyleBackColor = true;
            // 
            // rbCompressionNone
            // 
            this.rbCompressionNone.AutoSize = true;
            this.rbCompressionNone.Location = new System.Drawing.Point(120, 105);
            this.rbCompressionNone.Name = "rbCompressionNone";
            this.rbCompressionNone.Size = new System.Drawing.Size(51, 17);
            this.rbCompressionNone.TabIndex = 7;
            this.rbCompressionNone.Text = "None";
            this.rbCompressionNone.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Compression Level:";
            // 
            // chkIncludeAttachments
            // 
            this.chkIncludeAttachments.AutoSize = true;
            this.chkIncludeAttachments.Location = new System.Drawing.Point(120, 84);
            this.chkIncludeAttachments.Name = "chkIncludeAttachments";
            this.chkIncludeAttachments.Size = new System.Drawing.Size(123, 17);
            this.chkIncludeAttachments.TabIndex = 5;
            this.chkIncludeAttachments.Text = "Include Attachments";
            this.chkIncludeAttachments.UseVisualStyleBackColor = true;
            // 
            // btnBrowseLocation
            // 
            this.btnBrowseLocation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseLocation.Location = new System.Drawing.Point(664, 19);
            this.btnBrowseLocation.Name = "btnBrowseLocation";
            this.btnBrowseLocation.Size = new System.Drawing.Size(96, 23);
            this.btnBrowseLocation.TabIndex = 2;
            this.btnBrowseLocation.Text = "Browse...";
            this.btnBrowseLocation.UseVisualStyleBackColor = true;
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.Location = new System.Drawing.Point(120, 49);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(640, 20);
            this.txtDescription.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Description:";
            // 
            // txtBackupLocation
            // 
            this.txtBackupLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBackupLocation.Location = new System.Drawing.Point(120, 21);
            this.txtBackupLocation.Name = "txtBackupLocation";
            this.txtBackupLocation.Size = new System.Drawing.Size(536, 20);
            this.txtBackupLocation.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Backup Location:";
            // 
            // tabRestore
            // 
            this.tabRestore.Controls.Add(this.groupBox5);
            this.tabRestore.Location = new System.Drawing.Point(4, 22);
            this.tabRestore.Name = "tabRestore";
            this.tabRestore.Size = new System.Drawing.Size(792, 574);
            this.tabRestore.TabIndex = 1;
            this.tabRestore.Text = "Restore";
            this.tabRestore.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.groupBox5.MinimumSize = new System.Drawing.Size(500, 170);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(776, 219);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Restore Database";
            // 
            // btnRestoreExecute
            // 
            this.btnRestoreExecute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRestoreExecute.Location = new System.Drawing.Point(664, 170);
            this.btnRestoreExecute.Name = "btnRestoreExecute";
            this.btnRestoreExecute.Size = new System.Drawing.Size(96, 28);
            this.btnRestoreExecute.TabIndex = 10;
            this.btnRestoreExecute.Text = "Restore";
            this.btnRestoreExecute.UseVisualStyleBackColor = true;
            // 
            // chkVerifyBeforeRestore
            // 
            this.chkVerifyBeforeRestore.AutoSize = true;
            this.chkVerifyBeforeRestore.Checked = true;
            this.chkVerifyBeforeRestore.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkVerifyBeforeRestore.Location = new System.Drawing.Point(112, 110);
            this.chkVerifyBeforeRestore.Name = "chkVerifyBeforeRestore";
            this.chkVerifyBeforeRestore.Size = new System.Drawing.Size(159, 17);
            this.chkVerifyBeforeRestore.TabIndex = 9;
            this.chkVerifyBeforeRestore.Text = "Verify backup before restore";
            this.chkVerifyBeforeRestore.UseVisualStyleBackColor = true;
            // 
            // rbMerge
            // 
            this.rbMerge.AutoSize = true;
            this.rbMerge.Location = new System.Drawing.Point(277, 78);
            this.rbMerge.Name = "rbMerge";
            this.rbMerge.Size = new System.Drawing.Size(137, 17);
            this.rbMerge.TabIndex = 8;
            this.rbMerge.Text = "Merge with current data";
            this.rbMerge.UseVisualStyleBackColor = true;
            // 
            // rbOverwrite
            // 
            this.rbOverwrite.AutoSize = true;
            this.rbOverwrite.Checked = true;
            this.rbOverwrite.Location = new System.Drawing.Point(112, 78);
            this.rbOverwrite.Name = "rbOverwrite";
            this.rbOverwrite.Size = new System.Drawing.Size(155, 17);
            this.rbOverwrite.TabIndex = 7;
            this.rbOverwrite.TabStop = true;
            this.rbOverwrite.Text = "Overwrite existing database";
            this.rbOverwrite.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 80);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(86, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Restore Options:";
            // 
            // lblSelectedPoint
            // 
            this.lblSelectedPoint.AutoSize = true;
            this.lblSelectedPoint.Location = new System.Drawing.Point(267, 52);
            this.lblSelectedPoint.Name = "lblSelectedPoint";
            this.lblSelectedPoint.Size = new System.Drawing.Size(85, 13);
            this.lblSelectedPoint.TabIndex = 5;
            this.lblSelectedPoint.Text = "Selected Point: -";
            // 
            // dateRestorePoint
            // 
            this.dateRestorePoint.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dateRestorePoint.Enabled = false;
            this.dateRestorePoint.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateRestorePoint.Location = new System.Drawing.Point(112, 49);
            this.dateRestorePoint.Name = "dateRestorePoint";
            this.dateRestorePoint.Size = new System.Drawing.Size(136, 20);
            this.dateRestorePoint.TabIndex = 4;
            // 
            // lblRestorePoint
            // 
            this.lblRestorePoint.AutoSize = true;
            this.lblRestorePoint.Enabled = false;
            this.lblRestorePoint.Location = new System.Drawing.Point(16, 52);
            this.lblRestorePoint.Name = "lblRestorePoint";
            this.lblRestorePoint.Size = new System.Drawing.Size(74, 13);
            this.lblRestorePoint.TabIndex = 3;
            this.lblRestorePoint.Text = "Restore Point:";
            // 
            // btnBrowseRestoreFile
            // 
            this.btnBrowseRestoreFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseRestoreFile.Location = new System.Drawing.Point(664, 19);
            this.btnBrowseRestoreFile.Name = "btnBrowseRestoreFile";
            this.btnBrowseRestoreFile.Size = new System.Drawing.Size(96, 23);
            this.btnBrowseRestoreFile.TabIndex = 2;
            this.btnBrowseRestoreFile.Text = "Browse...";
            this.btnBrowseRestoreFile.UseVisualStyleBackColor = true;
            // 
            // txtRestoreFilePath
            // 
            this.txtRestoreFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRestoreFilePath.Location = new System.Drawing.Point(112, 21);
            this.txtRestoreFilePath.Name = "txtRestoreFilePath";
            this.txtRestoreFilePath.Size = new System.Drawing.Size(544, 20);
            this.txtRestoreFilePath.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Backup File:";
            // 
            // tabScheduledBackups
            // 
            this.tabScheduledBackups.Controls.Add(this.groupBox4);
            this.tabScheduledBackups.Controls.Add(this.groupBox3);
            this.tabScheduledBackups.Location = new System.Drawing.Point(4, 22);
            this.tabScheduledBackups.Name = "tabScheduledBackups";
            this.tabScheduledBackups.Size = new System.Drawing.Size(792, 574);
            this.tabScheduledBackups.TabIndex = 2;
            this.tabScheduledBackups.Text = "Scheduled Backups";
            this.tabScheduledBackups.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.clbDaysofWeek);
            this.groupBox4.Controls.Add(this.dtpScheduleTime);
            this.groupBox4.Controls.Add(this.cmbScheduleType);
            this.groupBox4.Controls.Add(this.lblDaysofWeek);
            this.groupBox4.Controls.Add(this.lblScheduleTime);
            this.groupBox4.Controls.Add(this.lblScheduleType);
            this.groupBox4.Controls.Add(this.btnSaveSchedule);
            this.groupBox4.Controls.Add(this.btnCancelEdit);
            this.groupBox4.Enabled = false;
            this.groupBox4.Location = new System.Drawing.Point(8, 354);
            this.groupBox4.MinimumSize = new System.Drawing.Size(700, 212);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(776, 212);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Schedule Details";
            // 
            // dtpScheduleTime
            // 
            this.dtpScheduleTime.CustomFormat = "hh\':\'mm tt";
            this.dtpScheduleTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpScheduleTime.Location = new System.Drawing.Point(29, 102);
            this.dtpScheduleTime.Name = "dtpScheduleTime";
            this.dtpScheduleTime.Size = new System.Drawing.Size(238, 20);
            this.dtpScheduleTime.TabIndex = 6;
            this.dtpScheduleTime.Value = new System.DateTime(2025, 3, 21, 0, 39, 0, 0);
            // 
            // cmbScheduleType
            // 
            this.cmbScheduleType.FormattingEnabled = true;
            this.cmbScheduleType.Location = new System.Drawing.Point(29, 47);
            this.cmbScheduleType.Name = "cmbScheduleType";
            this.cmbScheduleType.Size = new System.Drawing.Size(294, 21);
            this.cmbScheduleType.TabIndex = 5;
            // 
            // lblDaysofWeek
            // 
            this.lblDaysofWeek.AutoSize = true;
            this.lblDaysofWeek.Location = new System.Drawing.Point(26, 137);
            this.lblDaysofWeek.Name = "lblDaysofWeek";
            this.lblDaysofWeek.Size = new System.Drawing.Size(75, 13);
            this.lblDaysofWeek.TabIndex = 4;
            this.lblDaysofWeek.Text = "Days of Week";
            // 
            // lblScheduleTime
            // 
            this.lblScheduleTime.AutoSize = true;
            this.lblScheduleTime.Location = new System.Drawing.Point(26, 85);
            this.lblScheduleTime.Name = "lblScheduleTime";
            this.lblScheduleTime.Size = new System.Drawing.Size(78, 13);
            this.lblScheduleTime.TabIndex = 3;
            this.lblScheduleTime.Text = "Schedule Time";
            // 
            // lblScheduleType
            // 
            this.lblScheduleType.AutoSize = true;
            this.lblScheduleType.Location = new System.Drawing.Point(26, 31);
            this.lblScheduleType.Name = "lblScheduleType";
            this.lblScheduleType.Size = new System.Drawing.Size(79, 13);
            this.lblScheduleType.TabIndex = 2;
            this.lblScheduleType.Text = "Schedule Type";
            // 
            // btnSaveSchedule
            // 
            this.btnSaveSchedule.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveSchedule.Location = new System.Drawing.Point(585, 183);
            this.btnSaveSchedule.Name = "btnSaveSchedule";
            this.btnSaveSchedule.Size = new System.Drawing.Size(85, 23);
            this.btnSaveSchedule.TabIndex = 0;
            this.btnSaveSchedule.Text = "Save";
            this.btnSaveSchedule.UseVisualStyleBackColor = true;
            // 
            // btnCancelEdit
            // 
            this.btnCancelEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelEdit.Location = new System.Drawing.Point(676, 183);
            this.btnCancelEdit.Name = "btnCancelEdit";
            this.btnCancelEdit.Size = new System.Drawing.Size(85, 23);
            this.btnCancelEdit.TabIndex = 1;
            this.btnCancelEdit.Text = "Cancel";
            this.btnCancelEdit.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.dgvScheduledBackups);
            this.groupBox3.Controls.Add(this.btnAddSchedule);
            this.groupBox3.Controls.Add(this.btnEditSchedule);
            this.groupBox3.Controls.Add(this.btnDeleteSchedule);
            this.groupBox3.Controls.Add(this.btnEnableSchedule);
            this.groupBox3.Controls.Add(this.btnDisableSchedule);
            this.groupBox3.Location = new System.Drawing.Point(8, 8);
            this.groupBox3.MinimumSize = new System.Drawing.Size(700, 200);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(776, 340);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Scheduled Backups";
            // 
            // dgvScheduledBackups
            // 
            this.dgvScheduledBackups.AllowUserToAddRows = false;
            this.dgvScheduledBackups.AllowUserToDeleteRows = false;
            this.dgvScheduledBackups.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvScheduledBackups.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvScheduledBackups.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvScheduledBackups.GridColor = System.Drawing.Color.Cyan;
            this.dgvScheduledBackups.Location = new System.Drawing.Point(3, 16);
            this.dgvScheduledBackups.MultiSelect = false;
            this.dgvScheduledBackups.Name = "dgvScheduledBackups";
            this.dgvScheduledBackups.ReadOnly = true;
            this.dgvScheduledBackups.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvScheduledBackups.Size = new System.Drawing.Size(770, 287);
            this.dgvScheduledBackups.TabIndex = 0;
            // 
            // btnAddSchedule
            // 
            this.btnAddSchedule.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddSchedule.Location = new System.Drawing.Point(312, 309);
            this.btnAddSchedule.Name = "btnAddSchedule";
            this.btnAddSchedule.Size = new System.Drawing.Size(85, 25);
            this.btnAddSchedule.TabIndex = 1;
            this.btnAddSchedule.Text = "Add";
            this.btnAddSchedule.UseVisualStyleBackColor = true;
            // 
            // btnEditSchedule
            // 
            this.btnEditSchedule.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditSchedule.Location = new System.Drawing.Point(403, 309);
            this.btnEditSchedule.Name = "btnEditSchedule";
            this.btnEditSchedule.Size = new System.Drawing.Size(85, 25);
            this.btnEditSchedule.TabIndex = 2;
            this.btnEditSchedule.Text = "Edit";
            this.btnEditSchedule.UseVisualStyleBackColor = true;
            // 
            // btnDeleteSchedule
            // 
            this.btnDeleteSchedule.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteSchedule.Location = new System.Drawing.Point(494, 309);
            this.btnDeleteSchedule.Name = "btnDeleteSchedule";
            this.btnDeleteSchedule.Size = new System.Drawing.Size(85, 25);
            this.btnDeleteSchedule.TabIndex = 3;
            this.btnDeleteSchedule.Text = "Delete";
            this.btnDeleteSchedule.UseVisualStyleBackColor = true;
            // 
            // btnEnableSchedule
            // 
            this.btnEnableSchedule.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEnableSchedule.Location = new System.Drawing.Point(585, 309);
            this.btnEnableSchedule.Name = "btnEnableSchedule";
            this.btnEnableSchedule.Size = new System.Drawing.Size(85, 25);
            this.btnEnableSchedule.TabIndex = 4;
            this.btnEnableSchedule.Text = "Enable";
            this.btnEnableSchedule.UseVisualStyleBackColor = true;
            // 
            // btnDisableSchedule
            // 
            this.btnDisableSchedule.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDisableSchedule.Location = new System.Drawing.Point(676, 309);
            this.btnDisableSchedule.Name = "btnDisableSchedule";
            this.btnDisableSchedule.Size = new System.Drawing.Size(85, 25);
            this.btnDisableSchedule.TabIndex = 5;
            this.btnDisableSchedule.Text = "Disable";
            this.btnDisableSchedule.UseVisualStyleBackColor = true;
            // 
            // clbDaysofWeek
            // 
            this.clbDaysofWeek.BackColor = System.Drawing.SystemColors.Window;
            this.clbDaysofWeek.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.clbDaysofWeek.FormattingEnabled = true;
            this.clbDaysofWeek.Items.AddRange(new object[] {
            "Sunday",
            "Monday",
            "Tuesday",
            "Wednesday",
            "Friday",
            "Saturday"});
            this.clbDaysofWeek.Location = new System.Drawing.Point(29, 153);
            this.clbDaysofWeek.MultiColumn = true;
            this.clbDaysofWeek.Name = "clbDaysofWeek";
            this.clbDaysofWeek.Size = new System.Drawing.Size(684, 15);
            this.clbDaysofWeek.TabIndex = 7;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.MinimumSize = new System.Drawing.Size(500, 500);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 36.97183F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 63.02817F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(786, 568);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // BackupView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "BackupView";
            this.Size = new System.Drawing.Size(800, 600);
            this.tabControl1.ResumeLayout(false);
            this.tabBackupRestore.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBackupHistory)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabRestore.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.tabScheduledBackups.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvScheduledBackups)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
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
        private System.Windows.Forms.Label lblScheduleType;
        private System.Windows.Forms.Label lblDaysofWeek;
        private System.Windows.Forms.Label lblScheduleTime;
        private System.Windows.Forms.ComboBox cmbScheduleType;
        private System.Windows.Forms.DateTimePicker dtpScheduleTime;
        private System.Windows.Forms.CheckedListBox clbDaysofWeek;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
