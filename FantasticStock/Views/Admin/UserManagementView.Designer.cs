namespace FantasticStock.Views
{
    partial class UserManagementView
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
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageUsers = new System.Windows.Forms.TabPage();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.panelUserList = new System.Windows.Forms.Panel();
            this.dgvUsers = new System.Windows.Forms.DataGridView();
            this.panelFilters = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblStatusFilter = new System.Windows.Forms.Label();
            this.cmbStatusFilter = new System.Windows.Forms.ComboBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.cmbRoleFilter = new System.Windows.Forms.ComboBox();
            this.lblRoleFilter = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.panelListActions = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnDeactivate = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAddUser = new System.Windows.Forms.Button();
            this.panelUserDetail = new System.Windows.Forms.Panel();
            this.tabControlUserDetail = new System.Windows.Forms.TabControl();
            this.tabPageBasicInfo = new System.Windows.Forms.TabPage();
            this.cmbRoles = new System.Windows.Forms.ComboBox();
            this.lblRole = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtDisplayName = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblDisplayName = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.tabPageSecurity = new System.Windows.Forms.TabPage();
            this.chkTwoFactorEnabled = new System.Windows.Forms.CheckBox();
            this.lblPasswordStrength = new System.Windows.Forms.Label();
            this.progressBarPasswordStrength = new System.Windows.Forms.ProgressBar();
            this.txtConfirmPassword = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblConfirmPassword = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.tabPageRestrictions = new System.Windows.Forms.TabPage();
            this.chkSaturday = new System.Windows.Forms.CheckBox();
            this.chkFriday = new System.Windows.Forms.CheckBox();
            this.chkThursday = new System.Windows.Forms.CheckBox();
            this.chkWednesday = new System.Windows.Forms.CheckBox();
            this.chkTuesday = new System.Windows.Forms.CheckBox();
            this.chkMonday = new System.Windows.Forms.CheckBox();
            this.chkSunday = new System.Windows.Forms.CheckBox();
            this.lblAllowedDays = new System.Windows.Forms.Label();
            this.panelDetailActions = new System.Windows.Forms.Panel();
            this.btnResetPassword = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.tabPageActivityLog = new System.Windows.Forms.TabPage();
            this.dgvActivityLog = new System.Windows.Forms.DataGridView();
            this.panelActivityControls = new System.Windows.Forms.Panel();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnFilter = new System.Windows.Forms.Button();
            this.cmbActionType = new System.Windows.Forms.ComboBox();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.lblActionType = new System.Windows.Forms.Label();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabControlMain.SuspendLayout();
            this.tabPageUsers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.panelUserList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
            this.panelFilters.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panelListActions.SuspendLayout();
            this.panelUserDetail.SuspendLayout();
            this.tabControlUserDetail.SuspendLayout();
            this.tabPageBasicInfo.SuspendLayout();
            this.tabPageSecurity.SuspendLayout();
            this.tabPageRestrictions.SuspendLayout();
            this.panelDetailActions.SuspendLayout();
            this.tabPageActivityLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActivityLog)).BeginInit();
            this.panelActivityControls.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPageUsers);
            this.tabControlMain.Controls.Add(this.tabPageActivityLog);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Location = new System.Drawing.Point(0, 0);
            this.tabControlMain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(1320, 685);
            this.tabControlMain.TabIndex = 0;
            // 
            // tabPageUsers
            // 
            this.tabPageUsers.Controls.Add(this.splitContainerMain);
            this.tabPageUsers.Location = new System.Drawing.Point(4, 25);
            this.tabPageUsers.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPageUsers.Name = "tabPageUsers";
            this.tabPageUsers.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPageUsers.Size = new System.Drawing.Size(1312, 656);
            this.tabPageUsers.TabIndex = 0;
            this.tabPageUsers.Text = "Users";
            this.tabPageUsers.UseVisualStyleBackColor = true;
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.Location = new System.Drawing.Point(4, 4);
            this.splitContainerMain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.panelUserList);
            this.splitContainerMain.Panel1.Controls.Add(this.panelFilters);
            this.splitContainerMain.Panel1.Controls.Add(this.panelListActions);
            this.splitContainerMain.Panel1MinSize = 350;
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.panelUserDetail);
            this.splitContainerMain.Panel2.Controls.Add(this.panelDetailActions);
            this.splitContainerMain.Panel2MinSize = 350;
            this.splitContainerMain.Size = new System.Drawing.Size(1304, 648);
            this.splitContainerMain.SplitterDistance = 634;
            this.splitContainerMain.SplitterWidth = 5;
            this.splitContainerMain.TabIndex = 0;
            // 
            // panelUserList
            // 
            this.panelUserList.Controls.Add(this.dgvUsers);
            this.panelUserList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelUserList.Location = new System.Drawing.Point(0, 98);
            this.panelUserList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelUserList.Name = "panelUserList";
            this.panelUserList.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.panelUserList.Size = new System.Drawing.Size(634, 497);
            this.panelUserList.TabIndex = 2;
            // 
            // dgvUsers
            // 
            this.dgvUsers.AllowUserToAddRows = false;
            this.dgvUsers.AllowUserToDeleteRows = false;
            this.dgvUsers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUsers.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvUsers.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUsers.GridColor = System.Drawing.Color.Cyan;
            this.dgvUsers.Location = new System.Drawing.Point(7, 6);
            this.dgvUsers.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvUsers.MultiSelect = false;
            this.dgvUsers.Name = "dgvUsers";
            this.dgvUsers.ReadOnly = true;
            this.dgvUsers.RowHeadersWidth = 30;
            this.dgvUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUsers.Size = new System.Drawing.Size(620, 485);
            this.dgvUsers.TabIndex = 0;
            // 
            // panelFilters
            // 
            this.panelFilters.Controls.Add(this.tableLayoutPanel1);
            this.panelFilters.Controls.Add(this.txtSearch);
            this.panelFilters.Controls.Add(this.lblSearch);
            this.panelFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilters.Location = new System.Drawing.Point(0, 0);
            this.panelFilters.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelFilters.Name = "panelFilters";
            this.panelFilters.Size = new System.Drawing.Size(634, 98);
            this.panelFilters.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(24, 50);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(556, 39);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lblStatusFilter);
            this.panel3.Controls.Add(this.cmbStatusFilter);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(4, 4);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(270, 31);
            this.panel3.TabIndex = 0;
            // 
            // lblStatusFilter
            // 
            this.lblStatusFilter.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblStatusFilter.AutoSize = true;
            this.lblStatusFilter.Location = new System.Drawing.Point(4, 8);
            this.lblStatusFilter.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatusFilter.Name = "lblStatusFilter";
            this.lblStatusFilter.Size = new System.Drawing.Size(79, 16);
            this.lblStatusFilter.TabIndex = 1;
            this.lblStatusFilter.Text = "Status Filter:";
            // 
            // cmbStatusFilter
            // 
            this.cmbStatusFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbStatusFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatusFilter.FormattingEnabled = true;
            this.cmbStatusFilter.Items.AddRange(new object[] {
            "All status",
            "Active",
            "Inactive",
            "Locked"});
            this.cmbStatusFilter.Location = new System.Drawing.Point(107, 4);
            this.cmbStatusFilter.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbStatusFilter.Name = "cmbStatusFilter";
            this.cmbStatusFilter.Size = new System.Drawing.Size(153, 24);
            this.cmbStatusFilter.TabIndex = 4;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.cmbRoleFilter);
            this.panel4.Controls.Add(this.lblRoleFilter);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(282, 4);
            this.panel4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(270, 31);
            this.panel4.TabIndex = 1;
            // 
            // cmbRoleFilter
            // 
            this.cmbRoleFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbRoleFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRoleFilter.FormattingEnabled = true;
            this.cmbRoleFilter.Location = new System.Drawing.Point(107, 3);
            this.cmbRoleFilter.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbRoleFilter.Name = "cmbRoleFilter";
            this.cmbRoleFilter.Size = new System.Drawing.Size(149, 24);
            this.cmbRoleFilter.TabIndex = 5;
            // 
            // lblRoleFilter
            // 
            this.lblRoleFilter.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblRoleFilter.AutoSize = true;
            this.lblRoleFilter.Location = new System.Drawing.Point(19, 8);
            this.lblRoleFilter.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRoleFilter.Name = "lblRoleFilter";
            this.lblRoleFilter.Size = new System.Drawing.Size(71, 16);
            this.lblRoleFilter.TabIndex = 2;
            this.lblRoleFilter.Text = "Role Filter:";
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Location = new System.Drawing.Point(135, 18);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(426, 22);
            this.txtSearch.TabIndex = 3;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(40, 22);
            this.lblSearch.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(53, 16);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "Search:";
            // 
            // panelListActions
            // 
            this.panelListActions.Controls.Add(this.btnDelete);
            this.panelListActions.Controls.Add(this.btnDeactivate);
            this.panelListActions.Controls.Add(this.btnEdit);
            this.panelListActions.Controls.Add(this.btnAddUser);
            this.panelListActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelListActions.Location = new System.Drawing.Point(0, 595);
            this.panelListActions.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelListActions.Name = "panelListActions";
            this.panelListActions.Size = new System.Drawing.Size(634, 53);
            this.panelListActions.TabIndex = 0;
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnDelete.ForeColor = System.Drawing.Color.Red;
            this.btnDelete.Location = new System.Drawing.Point(486, 12);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 28);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnDeactivate
            // 
            this.btnDeactivate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnDeactivate.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnDeactivate.Location = new System.Drawing.Point(349, 12);
            this.btnDeactivate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDeactivate.Name = "btnDeactivate";
            this.btnDeactivate.Size = new System.Drawing.Size(100, 28);
            this.btnDeactivate.TabIndex = 2;
            this.btnDeactivate.Text = "Deactivate";
            this.btnDeactivate.UseVisualStyleBackColor = true;
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnEdit.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnEdit.Location = new System.Drawing.Point(194, 12);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(100, 28);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            // 
            // btnAddUser
            // 
            this.btnAddUser.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAddUser.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnAddUser.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnAddUser.Location = new System.Drawing.Point(45, 12);
            this.btnAddUser.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Size = new System.Drawing.Size(100, 28);
            this.btnAddUser.TabIndex = 0;
            this.btnAddUser.Text = "Add User";
            this.btnAddUser.UseVisualStyleBackColor = false;
            // 
            // panelUserDetail
            // 
            this.panelUserDetail.Controls.Add(this.tabControlUserDetail);
            this.panelUserDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelUserDetail.Location = new System.Drawing.Point(0, 0);
            this.panelUserDetail.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelUserDetail.Name = "panelUserDetail";
            this.panelUserDetail.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.panelUserDetail.Size = new System.Drawing.Size(665, 595);
            this.panelUserDetail.TabIndex = 1;
            // 
            // tabControlUserDetail
            // 
            this.tabControlUserDetail.Controls.Add(this.tabPageBasicInfo);
            this.tabControlUserDetail.Controls.Add(this.tabPageSecurity);
            this.tabControlUserDetail.Controls.Add(this.tabPageRestrictions);
            this.tabControlUserDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlUserDetail.Location = new System.Drawing.Point(7, 6);
            this.tabControlUserDetail.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabControlUserDetail.Name = "tabControlUserDetail";
            this.tabControlUserDetail.SelectedIndex = 0;
            this.tabControlUserDetail.Size = new System.Drawing.Size(651, 583);
            this.tabControlUserDetail.TabIndex = 0;
            // 
            // tabPageBasicInfo
            // 
            this.tabPageBasicInfo.Controls.Add(this.cmbRoles);
            this.tabPageBasicInfo.Controls.Add(this.lblRole);
            this.tabPageBasicInfo.Controls.Add(this.txtPhone);
            this.tabPageBasicInfo.Controls.Add(this.txtEmail);
            this.tabPageBasicInfo.Controls.Add(this.txtDisplayName);
            this.tabPageBasicInfo.Controls.Add(this.txtUsername);
            this.tabPageBasicInfo.Controls.Add(this.lblPhone);
            this.tabPageBasicInfo.Controls.Add(this.lblEmail);
            this.tabPageBasicInfo.Controls.Add(this.lblDisplayName);
            this.tabPageBasicInfo.Controls.Add(this.lblUsername);
            this.tabPageBasicInfo.Location = new System.Drawing.Point(4, 25);
            this.tabPageBasicInfo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPageBasicInfo.Name = "tabPageBasicInfo";
            this.tabPageBasicInfo.Padding = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.tabPageBasicInfo.Size = new System.Drawing.Size(643, 554);
            this.tabPageBasicInfo.TabIndex = 0;
            this.tabPageBasicInfo.Text = "Basic Info";
            this.tabPageBasicInfo.UseVisualStyleBackColor = true;
            // 
            // cmbRoles
            // 
            this.cmbRoles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbRoles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRoles.FormattingEnabled = true;
            this.cmbRoles.Location = new System.Drawing.Point(160, 177);
            this.cmbRoles.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbRoles.Name = "cmbRoles";
            this.cmbRoles.Size = new System.Drawing.Size(451, 24);
            this.cmbRoles.TabIndex = 9;
            // 
            // lblRole
            // 
            this.lblRole.AutoSize = true;
            this.lblRole.Location = new System.Drawing.Point(27, 182);
            this.lblRole.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(39, 16);
            this.lblRole.TabIndex = 8;
            this.lblRole.Text = "Role:";
            // 
            // txtPhone
            // 
            this.txtPhone.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPhone.Location = new System.Drawing.Point(160, 135);
            this.txtPhone.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(451, 22);
            this.txtPhone.TabIndex = 7;
            // 
            // txtEmail
            // 
            this.txtEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEmail.Location = new System.Drawing.Point(160, 98);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(451, 22);
            this.txtEmail.TabIndex = 6;
            // 
            // txtDisplayName
            // 
            this.txtDisplayName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDisplayName.Location = new System.Drawing.Point(160, 62);
            this.txtDisplayName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtDisplayName.Name = "txtDisplayName";
            this.txtDisplayName.Size = new System.Drawing.Size(451, 22);
            this.txtDisplayName.TabIndex = 5;
            // 
            // txtUsername
            // 
            this.txtUsername.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUsername.Location = new System.Drawing.Point(160, 25);
            this.txtUsername.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(451, 22);
            this.txtUsername.TabIndex = 4;
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Location = new System.Drawing.Point(27, 139);
            this.lblPhone.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(49, 16);
            this.lblPhone.TabIndex = 3;
            this.lblPhone.Text = "Phone:";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(27, 102);
            this.lblEmail.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(44, 16);
            this.lblEmail.TabIndex = 2;
            this.lblEmail.Text = "Email:";
            // 
            // lblDisplayName
            // 
            this.lblDisplayName.AutoSize = true;
            this.lblDisplayName.Location = new System.Drawing.Point(27, 65);
            this.lblDisplayName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDisplayName.Name = "lblDisplayName";
            this.lblDisplayName.Size = new System.Drawing.Size(96, 16);
            this.lblDisplayName.TabIndex = 1;
            this.lblDisplayName.Text = "Display Name:";
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(27, 28);
            this.lblUsername.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(73, 16);
            this.lblUsername.TabIndex = 0;
            this.lblUsername.Text = "Username:";
            // 
            // tabPageSecurity
            // 
            this.tabPageSecurity.Controls.Add(this.chkTwoFactorEnabled);
            this.tabPageSecurity.Controls.Add(this.lblPasswordStrength);
            this.tabPageSecurity.Controls.Add(this.progressBarPasswordStrength);
            this.tabPageSecurity.Controls.Add(this.txtConfirmPassword);
            this.tabPageSecurity.Controls.Add(this.txtPassword);
            this.tabPageSecurity.Controls.Add(this.lblConfirmPassword);
            this.tabPageSecurity.Controls.Add(this.lblPassword);
            this.tabPageSecurity.Location = new System.Drawing.Point(4, 25);
            this.tabPageSecurity.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPageSecurity.Name = "tabPageSecurity";
            this.tabPageSecurity.Padding = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.tabPageSecurity.Size = new System.Drawing.Size(641, 552);
            this.tabPageSecurity.TabIndex = 1;
            this.tabPageSecurity.Text = "Security";
            this.tabPageSecurity.UseVisualStyleBackColor = true;
            // 
            // chkTwoFactorEnabled
            // 
            this.chkTwoFactorEnabled.AutoSize = true;
            this.chkTwoFactorEnabled.Location = new System.Drawing.Point(31, 210);
            this.chkTwoFactorEnabled.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkTwoFactorEnabled.Name = "chkTwoFactorEnabled";
            this.chkTwoFactorEnabled.Size = new System.Drawing.Size(183, 20);
            this.chkTwoFactorEnabled.TabIndex = 3;
            this.chkTwoFactorEnabled.Text = "Two-Factor Authentication";
            this.chkTwoFactorEnabled.UseVisualStyleBackColor = true;
            // 
            // lblPasswordStrength
            // 
            this.lblPasswordStrength.AutoSize = true;
            this.lblPasswordStrength.Location = new System.Drawing.Point(160, 84);
            this.lblPasswordStrength.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPasswordStrength.Name = "lblPasswordStrength";
            this.lblPasswordStrength.Size = new System.Drawing.Size(55, 16);
            this.lblPasswordStrength.TabIndex = 5;
            this.lblPasswordStrength.Text = "Medium";
            // 
            // progressBarPasswordStrength
            // 
            this.progressBarPasswordStrength.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarPasswordStrength.Location = new System.Drawing.Point(160, 102);
            this.progressBarPasswordStrength.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.progressBarPasswordStrength.Name = "progressBarPasswordStrength";
            this.progressBarPasswordStrength.Size = new System.Drawing.Size(409, 12);
            this.progressBarPasswordStrength.TabIndex = 4;
            this.progressBarPasswordStrength.Value = 50;
            // 
            // txtConfirmPassword
            // 
            this.txtConfirmPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConfirmPassword.Location = new System.Drawing.Point(160, 134);
            this.txtConfirmPassword.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.PasswordChar = '•';
            this.txtConfirmPassword.Size = new System.Drawing.Size(408, 22);
            this.txtConfirmPassword.TabIndex = 3;
            // 
            // txtPassword
            // 
            this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPassword.Location = new System.Drawing.Point(160, 48);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '•';
            this.txtPassword.Size = new System.Drawing.Size(408, 22);
            this.txtPassword.TabIndex = 2;
            // 
            // lblConfirmPassword
            // 
            this.lblConfirmPassword.AutoSize = true;
            this.lblConfirmPassword.Location = new System.Drawing.Point(27, 139);
            this.lblConfirmPassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblConfirmPassword.Name = "lblConfirmPassword";
            this.lblConfirmPassword.Size = new System.Drawing.Size(118, 16);
            this.lblConfirmPassword.TabIndex = 1;
            this.lblConfirmPassword.Text = "Confirm Password:";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(27, 53);
            this.lblPassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(70, 16);
            this.lblPassword.TabIndex = 0;
            this.lblPassword.Text = "Password:";
            // 
            // tabPageRestrictions
            // 
            this.tabPageRestrictions.Controls.Add(this.chkSaturday);
            this.tabPageRestrictions.Controls.Add(this.chkFriday);
            this.tabPageRestrictions.Controls.Add(this.chkThursday);
            this.tabPageRestrictions.Controls.Add(this.chkWednesday);
            this.tabPageRestrictions.Controls.Add(this.chkTuesday);
            this.tabPageRestrictions.Controls.Add(this.chkMonday);
            this.tabPageRestrictions.Controls.Add(this.chkSunday);
            this.tabPageRestrictions.Controls.Add(this.lblAllowedDays);
            this.tabPageRestrictions.Location = new System.Drawing.Point(4, 25);
            this.tabPageRestrictions.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPageRestrictions.Name = "tabPageRestrictions";
            this.tabPageRestrictions.Padding = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.tabPageRestrictions.Size = new System.Drawing.Size(641, 552);
            this.tabPageRestrictions.TabIndex = 3;
            this.tabPageRestrictions.Text = "Restrictions";
            this.tabPageRestrictions.UseVisualStyleBackColor = true;
            // 
            // chkSaturday
            // 
            this.chkSaturday.AutoSize = true;
            this.chkSaturday.Checked = true;
            this.chkSaturday.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSaturday.Location = new System.Drawing.Point(160, 100);
            this.chkSaturday.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkSaturday.Name = "chkSaturday";
            this.chkSaturday.Size = new System.Drawing.Size(83, 20);
            this.chkSaturday.TabIndex = 8;
            this.chkSaturday.Text = "Saturday";
            this.chkSaturday.UseVisualStyleBackColor = true;
            // 
            // chkFriday
            // 
            this.chkFriday.AutoSize = true;
            this.chkFriday.Checked = true;
            this.chkFriday.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFriday.Location = new System.Drawing.Point(293, 75);
            this.chkFriday.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkFriday.Name = "chkFriday";
            this.chkFriday.Size = new System.Drawing.Size(67, 20);
            this.chkFriday.TabIndex = 7;
            this.chkFriday.Text = "Friday";
            this.chkFriday.UseVisualStyleBackColor = true;
            // 
            // chkThursday
            // 
            this.chkThursday.AutoSize = true;
            this.chkThursday.Checked = true;
            this.chkThursday.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkThursday.Location = new System.Drawing.Point(160, 75);
            this.chkThursday.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkThursday.Name = "chkThursday";
            this.chkThursday.Size = new System.Drawing.Size(86, 20);
            this.chkThursday.TabIndex = 6;
            this.chkThursday.Text = "Thursday";
            this.chkThursday.UseVisualStyleBackColor = true;
            // 
            // chkWednesday
            // 
            this.chkWednesday.AutoSize = true;
            this.chkWednesday.Checked = true;
            this.chkWednesday.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkWednesday.Location = new System.Drawing.Point(293, 50);
            this.chkWednesday.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkWednesday.Name = "chkWednesday";
            this.chkWednesday.Size = new System.Drawing.Size(103, 20);
            this.chkWednesday.TabIndex = 5;
            this.chkWednesday.Text = "Wednesday";
            this.chkWednesday.UseVisualStyleBackColor = true;
            // 
            // chkTuesday
            // 
            this.chkTuesday.AutoSize = true;
            this.chkTuesday.Checked = true;
            this.chkTuesday.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTuesday.Location = new System.Drawing.Point(160, 50);
            this.chkTuesday.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkTuesday.Name = "chkTuesday";
            this.chkTuesday.Size = new System.Drawing.Size(83, 20);
            this.chkTuesday.TabIndex = 4;
            this.chkTuesday.Text = "Tuesday";
            this.chkTuesday.UseVisualStyleBackColor = true;
            // 
            // chkMonday
            // 
            this.chkMonday.AutoSize = true;
            this.chkMonday.Checked = true;
            this.chkMonday.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMonday.Location = new System.Drawing.Point(293, 26);
            this.chkMonday.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkMonday.Name = "chkMonday";
            this.chkMonday.Size = new System.Drawing.Size(78, 20);
            this.chkMonday.TabIndex = 3;
            this.chkMonday.Text = "Monday";
            this.chkMonday.UseVisualStyleBackColor = true;
            // 
            // chkSunday
            // 
            this.chkSunday.AutoSize = true;
            this.chkSunday.Checked = true;
            this.chkSunday.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSunday.Location = new System.Drawing.Point(160, 26);
            this.chkSunday.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkSunday.Name = "chkSunday";
            this.chkSunday.Size = new System.Drawing.Size(75, 20);
            this.chkSunday.TabIndex = 2;
            this.chkSunday.Text = "Sunday";
            this.chkSunday.UseVisualStyleBackColor = true;
            // 
            // lblAllowedDays
            // 
            this.lblAllowedDays.AutoSize = true;
            this.lblAllowedDays.Location = new System.Drawing.Point(27, 26);
            this.lblAllowedDays.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAllowedDays.Name = "lblAllowedDays";
            this.lblAllowedDays.Size = new System.Drawing.Size(93, 16);
            this.lblAllowedDays.TabIndex = 1;
            this.lblAllowedDays.Text = "Allowed Days:";
            // 
            // panelDetailActions
            // 
            this.panelDetailActions.Controls.Add(this.btnResetPassword);
            this.panelDetailActions.Controls.Add(this.btnCancel);
            this.panelDetailActions.Controls.Add(this.btnSave);
            this.panelDetailActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelDetailActions.Location = new System.Drawing.Point(0, 595);
            this.panelDetailActions.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelDetailActions.Name = "panelDetailActions";
            this.panelDetailActions.Size = new System.Drawing.Size(665, 53);
            this.panelDetailActions.TabIndex = 0;
            // 
            // btnResetPassword
            // 
            this.btnResetPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetPassword.Location = new System.Drawing.Point(327, 12);
            this.btnResetPassword.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnResetPassword.Name = "btnResetPassword";
            this.btnResetPassword.Size = new System.Drawing.Size(121, 28);
            this.btnResetPassword.TabIndex = 2;
            this.btnResetPassword.Text = "Reset Password";
            this.btnResetPassword.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(457, 12);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(93, 28);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnSave.ForeColor = System.Drawing.SystemColors.Menu;
            this.btnSave.Location = new System.Drawing.Point(558, 12);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(93, 28);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            // 
            // tabPageActivityLog
            // 
            this.tabPageActivityLog.Controls.Add(this.dgvActivityLog);
            this.tabPageActivityLog.Controls.Add(this.panelActivityControls);
            this.tabPageActivityLog.Location = new System.Drawing.Point(4, 25);
            this.tabPageActivityLog.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPageActivityLog.Name = "tabPageActivityLog";
            this.tabPageActivityLog.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPageActivityLog.Size = new System.Drawing.Size(1312, 657);
            this.tabPageActivityLog.TabIndex = 1;
            this.tabPageActivityLog.Text = "Activity Log";
            this.tabPageActivityLog.UseVisualStyleBackColor = true;
            // 
            // dgvActivityLog
            // 
            this.dgvActivityLog.AllowUserToAddRows = false;
            this.dgvActivityLog.AllowUserToDeleteRows = false;
            this.dgvActivityLog.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvActivityLog.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvActivityLog.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvActivityLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvActivityLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvActivityLog.GridColor = System.Drawing.Color.Cyan;
            this.dgvActivityLog.Location = new System.Drawing.Point(4, 102);
            this.dgvActivityLog.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvActivityLog.Name = "dgvActivityLog";
            this.dgvActivityLog.ReadOnly = true;
            this.dgvActivityLog.RowHeadersWidth = 51;
            this.dgvActivityLog.Size = new System.Drawing.Size(1304, 551);
            this.dgvActivityLog.TabIndex = 1;
            // 
            // panelActivityControls
            // 
            this.panelActivityControls.Controls.Add(this.btnExport);
            this.panelActivityControls.Controls.Add(this.btnFilter);
            this.panelActivityControls.Controls.Add(this.cmbActionType);
            this.panelActivityControls.Controls.Add(this.dtpEndDate);
            this.panelActivityControls.Controls.Add(this.dtpStartDate);
            this.panelActivityControls.Controls.Add(this.lblActionType);
            this.panelActivityControls.Controls.Add(this.lblEndDate);
            this.panelActivityControls.Controls.Add(this.lblStartDate);
            this.panelActivityControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelActivityControls.Location = new System.Drawing.Point(4, 4);
            this.panelActivityControls.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelActivityControls.Name = "panelActivityControls";
            this.panelActivityControls.Size = new System.Drawing.Size(1304, 98);
            this.panelActivityControls.TabIndex = 0;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(1065, 55);
            this.btnExport.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(100, 28);
            this.btnExport.TabIndex = 7;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            // 
            // btnFilter
            // 
            this.btnFilter.Location = new System.Drawing.Point(948, 55);
            this.btnFilter.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(100, 28);
            this.btnFilter.TabIndex = 6;
            this.btnFilter.Text = "Apply Filter";
            this.btnFilter.UseVisualStyleBackColor = true;
            // 
            // cmbActionType
            // 
            this.cmbActionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbActionType.FormattingEnabled = true;
            this.cmbActionType.Items.AddRange(new object[] {
            "All Actions",
            "Login",
            "Logout",
            "Create",
            "Update",
            "Delete",
            "Failed Login"});
            this.cmbActionType.Location = new System.Drawing.Point(681, 58);
            this.cmbActionType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbActionType.Name = "cmbActionType";
            this.cmbActionType.Size = new System.Drawing.Size(219, 24);
            this.cmbActionType.TabIndex = 5;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.CustomFormat = "MM/dd/yyyy HH:mm";
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.Location = new System.Drawing.Point(389, 58);
            this.dtpEndDate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(192, 22);
            this.dtpEndDate.TabIndex = 4;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.CustomFormat = "MM/dd/yyyy HH:mm";
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Location = new System.Drawing.Point(109, 58);
            this.dtpStartDate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(192, 22);
            this.dtpStartDate.TabIndex = 3;
            // 
            // lblActionType
            // 
            this.lblActionType.AutoSize = true;
            this.lblActionType.Location = new System.Drawing.Point(611, 62);
            this.lblActionType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblActionType.Name = "lblActionType";
            this.lblActionType.Size = new System.Drawing.Size(47, 16);
            this.lblActionType.TabIndex = 2;
            this.lblActionType.Text = "Action:";
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Location = new System.Drawing.Point(317, 62);
            this.lblEndDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(66, 16);
            this.lblEndDate.TabIndex = 1;
            this.lblEndDate.Text = "End Date:";
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Location = new System.Drawing.Point(24, 62);
            this.lblStartDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(69, 16);
            this.lblStartDate.TabIndex = 0;
            this.lblStartDate.Text = "Start Date:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1320, 53);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label1.Location = new System.Drawing.Point(24, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(211, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "User Mangement";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tabControlMain);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 53);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1320, 685);
            this.panel2.TabIndex = 2;
            // 
            // UserManagementView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "UserManagementView";
            this.Size = new System.Drawing.Size(1320, 738);
            this.tabControlMain.ResumeLayout(false);
            this.tabPageUsers.ResumeLayout(false);
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.panelUserList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
            this.panelFilters.ResumeLayout(false);
            this.panelFilters.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panelListActions.ResumeLayout(false);
            this.panelUserDetail.ResumeLayout(false);
            this.tabControlUserDetail.ResumeLayout(false);
            this.tabPageBasicInfo.ResumeLayout(false);
            this.tabPageBasicInfo.PerformLayout();
            this.tabPageSecurity.ResumeLayout(false);
            this.tabPageSecurity.PerformLayout();
            this.tabPageRestrictions.ResumeLayout(false);
            this.tabPageRestrictions.PerformLayout();
            this.panelDetailActions.ResumeLayout(false);
            this.tabPageActivityLog.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvActivityLog)).EndInit();
            this.panelActivityControls.ResumeLayout(false);
            this.panelActivityControls.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageUsers;
        private System.Windows.Forms.TabPage tabPageActivityLog;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.Panel panelUserList;
        private System.Windows.Forms.Panel panelFilters;
        private System.Windows.Forms.Panel panelListActions;
        private System.Windows.Forms.Panel panelUserDetail;
        private System.Windows.Forms.Panel panelDetailActions;
        private System.Windows.Forms.DataGridView dgvUsers;
        private System.Windows.Forms.ComboBox cmbRoleFilter;
        private System.Windows.Forms.ComboBox cmbStatusFilter;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblRoleFilter;
        private System.Windows.Forms.Label lblStatusFilter;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnDeactivate;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAddUser;
        private System.Windows.Forms.TabControl tabControlUserDetail;
        private System.Windows.Forms.TabPage tabPageBasicInfo;
        private System.Windows.Forms.TabPage tabPageSecurity;
        private System.Windows.Forms.TabPage tabPageRestrictions;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtDisplayName;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblDisplayName;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblPasswordStrength;
        private System.Windows.Forms.ProgressBar progressBarPasswordStrength;
        private System.Windows.Forms.TextBox txtConfirmPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblConfirmPassword;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.CheckBox chkSaturday;
        private System.Windows.Forms.CheckBox chkFriday;
        private System.Windows.Forms.CheckBox chkThursday;
        private System.Windows.Forms.CheckBox chkWednesday;
        private System.Windows.Forms.CheckBox chkTuesday;
        private System.Windows.Forms.CheckBox chkMonday;
        private System.Windows.Forms.CheckBox chkSunday;
        private System.Windows.Forms.Label lblAllowedDays;
        private System.Windows.Forms.CheckBox chkTwoFactorEnabled;
        private System.Windows.Forms.Button btnResetPassword;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel panelActivityControls;
        private System.Windows.Forms.DataGridView dgvActivityLog;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.ComboBox cmbActionType;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label lblActionType;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ComboBox cmbRoles;
        private System.Windows.Forms.Label lblRole;
    }
}
