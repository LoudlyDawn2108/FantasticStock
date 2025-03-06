namespace FantasticStock.Views
{
    partial class MonitoringView
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
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabSystemResources = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblCurrentTime = new System.Windows.Forms.Label();
            this.tabErrorLogs = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabAuditLogs = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblCPU = new System.Windows.Forms.Label();
            this.lblMemory = new System.Windows.Forms.Label();
            this.lblDisk = new System.Windows.Forms.Label();
            this.progressCPU = new System.Windows.Forms.ProgressBar();
            this.progressMemory = new System.Windows.Forms.ProgressBar();
            this.progressDisk = new System.Windows.Forms.ProgressBar();
            this.lblCPUValue = new System.Windows.Forms.Label();
            this.lblMemoryValue = new System.Windows.Forms.Label();
            this.lblDiskValue = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lblDatabaseSizeLabel = new System.Windows.Forms.Label();
            this.lblActiveUsersLabel = new System.Windows.Forms.Label();
            this.lblLastBackupLabel = new System.Windows.Forms.Label();
            this.lblUptimeLabel = new System.Windows.Forms.Label();
            this.lblDatabaseSize = new System.Windows.Forms.Label();
            this.lblActiveUsers = new System.Windows.Forms.Label();
            this.lblLastBackup = new System.Windows.Forms.Label();
            this.lblUptime = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnClearCache = new System.Windows.Forms.Button();
            this.btnRebuildIndexes = new System.Windows.Forms.Button();
            this.btnOptimizeDB = new System.Windows.Forms.Button();
            this.btnRefreshResources = new System.Windows.Forms.Button();
            this.dgvErrorLogs = new System.Windows.Forms.DataGridView();
            this.btnClearErrorLogs = new System.Windows.Forms.Button();
            this.btnFilterErrorLogs = new System.Windows.Forms.Button();
            this.cmbErrorSeverity = new System.Windows.Forms.ComboBox();
            this.lblErrorSeverity = new System.Windows.Forms.Label();
            this.txtErrorModuleFilter = new System.Windows.Forms.TextBox();
            this.lblErrorModule = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.dgvAuditLogs = new System.Windows.Forms.DataGridView();
            this.btnExportLogs = new System.Windows.Forms.Button();
            this.btnClearAuditLogs = new System.Windows.Forms.Button();
            this.btnFilterAuditLogs = new System.Windows.Forms.Button();
            this.cmbAuditUser = new System.Windows.Forms.ComboBox();
            this.lblAuditUser = new System.Windows.Forms.Label();
            this.txtAuditTableFilter = new System.Windows.Forms.TextBox();
            this.lblAuditTable = new System.Windows.Forms.Label();
            this.cmbAuditType = new System.Windows.Forms.ComboBox();
            this.lblAuditType = new System.Windows.Forms.Label();
            this.tmrRefresh = new System.Windows.Forms.Timer(this.components);
            this.tabControl1.SuspendLayout();
            this.tabSystemResources.SuspendLayout();
            this.tabErrorLogs.SuspendLayout();
            this.tabAuditLogs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvErrorLogs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAuditLogs)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabSystemResources);
            this.tabControl1.Controls.Add(this.tabErrorLogs);
            this.tabControl1.Controls.Add(this.tabAuditLogs);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1016, 626);
            this.tabControl1.TabIndex = 0;
            // 
            // tabSystemResources
            // 
            this.tabSystemResources.Controls.Add(this.tableLayoutPanel1);
            this.tabSystemResources.Controls.Add(this.lblCurrentTime);
            this.tabSystemResources.Location = new System.Drawing.Point(4, 22);
            this.tabSystemResources.Name = "tabSystemResources";
            this.tabSystemResources.Padding = new System.Windows.Forms.Padding(3);
            this.tabSystemResources.Size = new System.Drawing.Size(1008, 600);
            this.tabSystemResources.TabIndex = 0;
            this.tabSystemResources.Text = "System Resources";
            this.tabSystemResources.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Size = new System.Drawing.Size(200, 100);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lblCurrentTime
            // 
            this.lblCurrentTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCurrentTime.Location = new System.Drawing.Point(1353, 1075);
            this.lblCurrentTime.Name = "lblCurrentTime";
            this.lblCurrentTime.Size = new System.Drawing.Size(457, 15);
            this.lblCurrentTime.TabIndex = 1;
            this.lblCurrentTime.Text = "Last updated: 2025-03-02 17:41:59 by LoudlyDawn2108";
            this.lblCurrentTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabErrorLogs
            // 
            this.tabErrorLogs.Controls.Add(this.groupBox4);
            this.tabErrorLogs.Controls.Add(this.panel1);
            this.tabErrorLogs.Location = new System.Drawing.Point(4, 22);
            this.tabErrorLogs.Name = "tabErrorLogs";
            this.tabErrorLogs.Padding = new System.Windows.Forms.Padding(3);
            this.tabErrorLogs.Size = new System.Drawing.Size(1008, 600);
            this.tabErrorLogs.TabIndex = 1;
            this.tabErrorLogs.Text = "Error Logs";
            this.tabErrorLogs.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(200, 100);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 100);
            this.panel1.TabIndex = 1;
            // 
            // tabAuditLogs
            // 
            this.tabAuditLogs.Controls.Add(this.groupBox5);
            this.tabAuditLogs.Controls.Add(this.panel2);
            this.tabAuditLogs.Location = new System.Drawing.Point(4, 22);
            this.tabAuditLogs.Name = "tabAuditLogs";
            this.tabAuditLogs.Size = new System.Drawing.Size(1008, 600);
            this.tabAuditLogs.TabIndex = 2;
            this.tabAuditLogs.Text = "Audit Logs";
            this.tabAuditLogs.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Location = new System.Drawing.Point(0, 0);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(200, 100);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 100);
            this.panel2.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.Size = new System.Drawing.Size(200, 100);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // lblCPU
            // 
            this.lblCPU.Location = new System.Drawing.Point(0, 0);
            this.lblCPU.Name = "lblCPU";
            this.lblCPU.Size = new System.Drawing.Size(100, 23);
            this.lblCPU.TabIndex = 0;
            // 
            // lblMemory
            // 
            this.lblMemory.Location = new System.Drawing.Point(0, 0);
            this.lblMemory.Name = "lblMemory";
            this.lblMemory.Size = new System.Drawing.Size(100, 23);
            this.lblMemory.TabIndex = 0;
            // 
            // lblDisk
            // 
            this.lblDisk.Location = new System.Drawing.Point(0, 0);
            this.lblDisk.Name = "lblDisk";
            this.lblDisk.Size = new System.Drawing.Size(100, 23);
            this.lblDisk.TabIndex = 0;
            // 
            // progressCPU
            // 
            this.progressCPU.Location = new System.Drawing.Point(0, 0);
            this.progressCPU.Name = "progressCPU";
            this.progressCPU.Size = new System.Drawing.Size(100, 23);
            this.progressCPU.TabIndex = 0;
            // 
            // progressMemory
            // 
            this.progressMemory.Location = new System.Drawing.Point(0, 0);
            this.progressMemory.Name = "progressMemory";
            this.progressMemory.Size = new System.Drawing.Size(100, 23);
            this.progressMemory.TabIndex = 0;
            // 
            // progressDisk
            // 
            this.progressDisk.Location = new System.Drawing.Point(0, 0);
            this.progressDisk.Name = "progressDisk";
            this.progressDisk.Size = new System.Drawing.Size(100, 23);
            this.progressDisk.TabIndex = 0;
            // 
            // lblCPUValue
            // 
            this.lblCPUValue.Location = new System.Drawing.Point(0, 0);
            this.lblCPUValue.Name = "lblCPUValue";
            this.lblCPUValue.Size = new System.Drawing.Size(100, 23);
            this.lblCPUValue.TabIndex = 0;
            // 
            // lblMemoryValue
            // 
            this.lblMemoryValue.Location = new System.Drawing.Point(0, 0);
            this.lblMemoryValue.Name = "lblMemoryValue";
            this.lblMemoryValue.Size = new System.Drawing.Size(100, 23);
            this.lblMemoryValue.TabIndex = 0;
            // 
            // lblDiskValue
            // 
            this.lblDiskValue.Location = new System.Drawing.Point(0, 0);
            this.lblDiskValue.Name = "lblDiskValue";
            this.lblDiskValue.Size = new System.Drawing.Size(100, 23);
            this.lblDiskValue.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 100);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.Size = new System.Drawing.Size(200, 100);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // lblDatabaseSizeLabel
            // 
            this.lblDatabaseSizeLabel.Location = new System.Drawing.Point(0, 0);
            this.lblDatabaseSizeLabel.Name = "lblDatabaseSizeLabel";
            this.lblDatabaseSizeLabel.Size = new System.Drawing.Size(100, 23);
            this.lblDatabaseSizeLabel.TabIndex = 0;
            // 
            // lblActiveUsersLabel
            // 
            this.lblActiveUsersLabel.Location = new System.Drawing.Point(0, 0);
            this.lblActiveUsersLabel.Name = "lblActiveUsersLabel";
            this.lblActiveUsersLabel.Size = new System.Drawing.Size(100, 23);
            this.lblActiveUsersLabel.TabIndex = 0;
            // 
            // lblLastBackupLabel
            // 
            this.lblLastBackupLabel.Location = new System.Drawing.Point(0, 0);
            this.lblLastBackupLabel.Name = "lblLastBackupLabel";
            this.lblLastBackupLabel.Size = new System.Drawing.Size(100, 23);
            this.lblLastBackupLabel.TabIndex = 0;
            // 
            // lblUptimeLabel
            // 
            this.lblUptimeLabel.Location = new System.Drawing.Point(0, 0);
            this.lblUptimeLabel.Name = "lblUptimeLabel";
            this.lblUptimeLabel.Size = new System.Drawing.Size(100, 23);
            this.lblUptimeLabel.TabIndex = 0;
            // 
            // lblDatabaseSize
            // 
            this.lblDatabaseSize.Location = new System.Drawing.Point(0, 0);
            this.lblDatabaseSize.Name = "lblDatabaseSize";
            this.lblDatabaseSize.Size = new System.Drawing.Size(100, 23);
            this.lblDatabaseSize.TabIndex = 0;
            // 
            // lblActiveUsers
            // 
            this.lblActiveUsers.Location = new System.Drawing.Point(0, 0);
            this.lblActiveUsers.Name = "lblActiveUsers";
            this.lblActiveUsers.Size = new System.Drawing.Size(100, 23);
            this.lblActiveUsers.TabIndex = 0;
            // 
            // lblLastBackup
            // 
            this.lblLastBackup.Location = new System.Drawing.Point(0, 0);
            this.lblLastBackup.Name = "lblLastBackup";
            this.lblLastBackup.Size = new System.Drawing.Size(100, 23);
            this.lblLastBackup.TabIndex = 0;
            // 
            // lblUptime
            // 
            this.lblUptime.Location = new System.Drawing.Point(0, 0);
            this.lblUptime.Name = "lblUptime";
            this.lblUptime.Size = new System.Drawing.Size(100, 23);
            this.lblUptime.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 100);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            // 
            // btnClearCache
            // 
            this.btnClearCache.Location = new System.Drawing.Point(0, 0);
            this.btnClearCache.Name = "btnClearCache";
            this.btnClearCache.Size = new System.Drawing.Size(75, 23);
            this.btnClearCache.TabIndex = 0;
            // 
            // btnRebuildIndexes
            // 
            this.btnRebuildIndexes.Location = new System.Drawing.Point(0, 0);
            this.btnRebuildIndexes.Name = "btnRebuildIndexes";
            this.btnRebuildIndexes.Size = new System.Drawing.Size(75, 23);
            this.btnRebuildIndexes.TabIndex = 0;
            // 
            // btnOptimizeDB
            // 
            this.btnOptimizeDB.Location = new System.Drawing.Point(0, 0);
            this.btnOptimizeDB.Name = "btnOptimizeDB";
            this.btnOptimizeDB.Size = new System.Drawing.Size(75, 23);
            this.btnOptimizeDB.TabIndex = 0;
            // 
            // btnRefreshResources
            // 
            this.btnRefreshResources.Location = new System.Drawing.Point(0, 0);
            this.btnRefreshResources.Name = "btnRefreshResources";
            this.btnRefreshResources.Size = new System.Drawing.Size(75, 23);
            this.btnRefreshResources.TabIndex = 0;
            // 
            // dgvErrorLogs
            // 
            this.dgvErrorLogs.Location = new System.Drawing.Point(0, 0);
            this.dgvErrorLogs.Name = "dgvErrorLogs";
            this.dgvErrorLogs.Size = new System.Drawing.Size(240, 150);
            this.dgvErrorLogs.TabIndex = 0;
            // 
            // btnClearErrorLogs
            // 
            this.btnClearErrorLogs.Location = new System.Drawing.Point(0, 0);
            this.btnClearErrorLogs.Name = "btnClearErrorLogs";
            this.btnClearErrorLogs.Size = new System.Drawing.Size(75, 23);
            this.btnClearErrorLogs.TabIndex = 0;
            // 
            // btnFilterErrorLogs
            // 
            this.btnFilterErrorLogs.Location = new System.Drawing.Point(0, 0);
            this.btnFilterErrorLogs.Name = "btnFilterErrorLogs";
            this.btnFilterErrorLogs.Size = new System.Drawing.Size(75, 23);
            this.btnFilterErrorLogs.TabIndex = 0;
            // 
            // cmbErrorSeverity
            // 
            this.cmbErrorSeverity.Location = new System.Drawing.Point(0, 0);
            this.cmbErrorSeverity.Name = "cmbErrorSeverity";
            this.cmbErrorSeverity.Size = new System.Drawing.Size(121, 21);
            this.cmbErrorSeverity.TabIndex = 0;
            // 
            // lblErrorSeverity
            // 
            this.lblErrorSeverity.Location = new System.Drawing.Point(0, 0);
            this.lblErrorSeverity.Name = "lblErrorSeverity";
            this.lblErrorSeverity.Size = new System.Drawing.Size(100, 23);
            this.lblErrorSeverity.TabIndex = 0;
            // 
            // txtErrorModuleFilter
            // 
            this.txtErrorModuleFilter.Location = new System.Drawing.Point(0, 0);
            this.txtErrorModuleFilter.Name = "txtErrorModuleFilter";
            this.txtErrorModuleFilter.Size = new System.Drawing.Size(100, 20);
            this.txtErrorModuleFilter.TabIndex = 0;
            // 
            // lblErrorModule
            // 
            this.lblErrorModule.Location = new System.Drawing.Point(0, 0);
            this.lblErrorModule.Name = "lblErrorModule";
            this.lblErrorModule.Size = new System.Drawing.Size(100, 23);
            this.lblErrorModule.TabIndex = 0;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Location = new System.Drawing.Point(0, 0);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(200, 20);
            this.dtpEndDate.TabIndex = 0;
            // 
            // lblEndDate
            // 
            this.lblEndDate.Location = new System.Drawing.Point(0, 0);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(100, 23);
            this.lblEndDate.TabIndex = 0;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Location = new System.Drawing.Point(0, 0);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(200, 20);
            this.dtpStartDate.TabIndex = 0;
            // 
            // lblStartDate
            // 
            this.lblStartDate.Location = new System.Drawing.Point(0, 0);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(100, 23);
            this.lblStartDate.TabIndex = 0;
            // 
            // dgvAuditLogs
            // 
            this.dgvAuditLogs.Location = new System.Drawing.Point(0, 0);
            this.dgvAuditLogs.Name = "dgvAuditLogs";
            this.dgvAuditLogs.Size = new System.Drawing.Size(240, 150);
            this.dgvAuditLogs.TabIndex = 0;
            // 
            // btnExportLogs
            // 
            this.btnExportLogs.Location = new System.Drawing.Point(0, 0);
            this.btnExportLogs.Name = "btnExportLogs";
            this.btnExportLogs.Size = new System.Drawing.Size(75, 23);
            this.btnExportLogs.TabIndex = 0;
            // 
            // btnClearAuditLogs
            // 
            this.btnClearAuditLogs.Location = new System.Drawing.Point(0, 0);
            this.btnClearAuditLogs.Name = "btnClearAuditLogs";
            this.btnClearAuditLogs.Size = new System.Drawing.Size(75, 23);
            this.btnClearAuditLogs.TabIndex = 0;
            // 
            // btnFilterAuditLogs
            // 
            this.btnFilterAuditLogs.Location = new System.Drawing.Point(0, 0);
            this.btnFilterAuditLogs.Name = "btnFilterAuditLogs";
            this.btnFilterAuditLogs.Size = new System.Drawing.Size(75, 23);
            this.btnFilterAuditLogs.TabIndex = 0;
            // 
            // cmbAuditUser
            // 
            this.cmbAuditUser.Location = new System.Drawing.Point(0, 0);
            this.cmbAuditUser.Name = "cmbAuditUser";
            this.cmbAuditUser.Size = new System.Drawing.Size(121, 21);
            this.cmbAuditUser.TabIndex = 0;
            // 
            // lblAuditUser
            // 
            this.lblAuditUser.Location = new System.Drawing.Point(0, 0);
            this.lblAuditUser.Name = "lblAuditUser";
            this.lblAuditUser.Size = new System.Drawing.Size(100, 23);
            this.lblAuditUser.TabIndex = 0;
            // 
            // txtAuditTableFilter
            // 
            this.txtAuditTableFilter.Location = new System.Drawing.Point(0, 0);
            this.txtAuditTableFilter.Name = "txtAuditTableFilter";
            this.txtAuditTableFilter.Size = new System.Drawing.Size(100, 20);
            this.txtAuditTableFilter.TabIndex = 0;
            // 
            // lblAuditTable
            // 
            this.lblAuditTable.Location = new System.Drawing.Point(0, 0);
            this.lblAuditTable.Name = "lblAuditTable";
            this.lblAuditTable.Size = new System.Drawing.Size(100, 23);
            this.lblAuditTable.TabIndex = 0;
            // 
            // cmbAuditType
            // 
            this.cmbAuditType.Location = new System.Drawing.Point(0, 0);
            this.cmbAuditType.Name = "cmbAuditType";
            this.cmbAuditType.Size = new System.Drawing.Size(121, 21);
            this.cmbAuditType.TabIndex = 0;
            // 
            // lblAuditType
            // 
            this.lblAuditType.Location = new System.Drawing.Point(0, 0);
            this.lblAuditType.Name = "lblAuditType";
            this.lblAuditType.Size = new System.Drawing.Size(100, 23);
            this.lblAuditType.TabIndex = 0;
            // 
            // tmrRefresh
            // 
            this.tmrRefresh.Interval = 5000;
            this.tmrRefresh.Tick += new System.EventHandler(this.tmrRefresh_Tick);
            // 
            // MonitoringView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "MonitoringView";
            this.Size = new System.Drawing.Size(1016, 626);
            this.tabControl1.ResumeLayout(false);
            this.tabSystemResources.ResumeLayout(false);
            this.tabErrorLogs.ResumeLayout(false);
            this.tabAuditLogs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvErrorLogs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAuditLogs)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabSystemResources;
        private System.Windows.Forms.TabPage tabErrorLogs;
        private System.Windows.Forms.TabPage tabAuditLogs;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lblCPU;
        private System.Windows.Forms.Label lblMemory;
        private System.Windows.Forms.Label lblDisk;
        private System.Windows.Forms.ProgressBar progressCPU;
        private System.Windows.Forms.ProgressBar progressMemory;
        private System.Windows.Forms.ProgressBar progressDisk;
        private System.Windows.Forms.Label lblCPUValue;
        private System.Windows.Forms.Label lblMemoryValue;
        private System.Windows.Forms.Label lblDiskValue;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label lblDatabaseSizeLabel;
        private System.Windows.Forms.Label lblActiveUsersLabel;
        private System.Windows.Forms.Label lblLastBackupLabel;
        private System.Windows.Forms.Label lblUptimeLabel;
        private System.Windows.Forms.Label lblDatabaseSize;
        private System.Windows.Forms.Label lblActiveUsers;
        private System.Windows.Forms.Label lblLastBackup;
        private System.Windows.Forms.Label lblUptime;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnClearCache;
        private System.Windows.Forms.Button btnRebuildIndexes;
        private System.Windows.Forms.Button btnOptimizeDB;
        private System.Windows.Forms.Button btnRefreshResources;
        private System.Windows.Forms.Label lblCurrentTime;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataGridView dgvErrorLogs;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnClearErrorLogs;
        private System.Windows.Forms.Button btnFilterErrorLogs;
        private System.Windows.Forms.ComboBox cmbErrorSeverity;
        private System.Windows.Forms.Label lblErrorSeverity;
        private System.Windows.Forms.TextBox txtErrorModuleFilter;
        private System.Windows.Forms.Label lblErrorModule;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.DataGridView dgvAuditLogs;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnExportLogs;
        private System.Windows.Forms.Button btnClearAuditLogs;
        private System.Windows.Forms.Button btnFilterAuditLogs;
        private System.Windows.Forms.ComboBox cmbAuditUser;
        private System.Windows.Forms.Label lblAuditUser;
        private System.Windows.Forms.TextBox txtAuditTableFilter;
        private System.Windows.Forms.Label lblAuditTable;
        private System.Windows.Forms.ComboBox cmbAuditType;
        private System.Windows.Forms.Label lblAuditType;
        private System.Windows.Forms.Timer tmrRefresh;
    }
}
