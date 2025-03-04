namespace AdminDomain.Views
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
            this.tabScheduledBackups = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnCancelEdit = new System.Windows.Forms.Button();
            this.btnSaveSchedule = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnDisableSchedule = new System.Windows.Forms.Button();
            this.btnEnableSchedule = new System.Windows.Forms.Button();
            this.btnDeleteSchedule = new System.Windows.Forms.Button();
            this.btnEditSchedule = new System.Windows.Forms.Button();
            this.btnAddSchedule = new System.Windows.Forms.Button();
            this.dgvScheduledBackups = new System.Windows.Forms.DataGridView();

            // Additional designer code would continue here

            this.SuspendLayout();

            // Basic layout setup
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "BackupView";
            this.Size = new System.Drawing.Size(1016, 626);

            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabBackupRestore;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnVerify;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.DataGridView dgvBackupHistory;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnBackupNow;
        private System.Windows.Forms.TextBox txtEncryptionPassword;
        private System.Windows.Forms.CheckBox chkEncrypt;
        private System.Windows.Forms.RadioButton rbCompressionHigh;
        private System.Windows.Forms.RadioButton rbCompressionNormal;
        private System.Windows.Forms.RadioButton rbCompressionNone;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkIncludeAttachments;
        private System.Windows.Forms.Button btnBrowseLocation;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBackupLocation;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabScheduledBackups;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnCancelEdit;
        private System.Windows.Forms.Button btnSaveSchedule;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnDisableSchedule;
        private System.Windows.Forms.Button btnEnableSchedule;
        private System.Windows.Forms.Button btnDeleteSchedule;
        private System.Windows.Forms.Button btnEditSchedule;
        private System.Windows.Forms.Button btnAddSchedule;
        private System.Windows.Forms.DataGridView dgvScheduledBackups;
    }
}
