using FantasticStock.Models;
using FantasticStock.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FantasticStock.Views
{
    public partial class UserManagementView : UserControl
    {
        private UserManagementViewModel _viewModel;
        private bool _isInitialized = false;

        public UserManagementView()
        {
            InitializeComponent();

            // Initialize view model
            _viewModel = new UserManagementViewModel();

            // Set up data bindings
            SetupBindings();

            // Format the grids
            FormatUserGrid();
            FormatActivityLogGrid();

            // Initialize UI elements
            InitializeStatusFilter();
            InitializePermissionsTreeView();

            _isInitialized = true;
        }

        private void SetupBindings()
        {
            try
            {
                // Bind users grid
                dgvUsers.DataSource = _viewModel.Users;
                dgvUsers.DataBindings.Add("DataSource", _viewModel, "Users", true, DataSourceUpdateMode.OnPropertyChanged);


                // Bind user detail fields
                txtUsername.DataBindings.Add("Text", _viewModel, "SelectedUser.Username", true, DataSourceUpdateMode.OnPropertyChanged);
                txtDisplayName.DataBindings.Add("Text", _viewModel, "SelectedUser.DisplayName", true, DataSourceUpdateMode.OnPropertyChanged);
                txtEmail.DataBindings.Add("Text", _viewModel, "SelectedUser.Email", true, DataSourceUpdateMode.OnPropertyChanged);
                txtPhone.DataBindings.Add("Text", _viewModel, "SelectedUser.Phone", true, DataSourceUpdateMode.OnPropertyChanged);
                chkTwoFactorEnabled.DataBindings.Add("Checked", _viewModel, "SelectedUser.TwoFactorEnabled", true, DataSourceUpdateMode.OnPropertyChanged);

                // Bind account expiry date
                dateTimePickerExpiration.DataBindings.Add("Value", _viewModel, "SelectedUser.AccountExpiry", true, DataSourceUpdateMode.OnPropertyChanged);
                dateTimePickerExpiration.Checked = _viewModel.SelectedUser?.AccountExpiry != null;

                // Bind role dropdown
                cmbRoles.DataSource = _viewModel.Roles;
                cmbRoles.DisplayMember = "RoleName";
                cmbRoles.ValueMember = "RoleID";
                cmbRoles.DataBindings.Add("SelectedValue", _viewModel, "SelectedUser.RoleID", true, DataSourceUpdateMode.OnPropertyChanged);

                // Bind search and filter controls
                txtSearch.DataBindings.Add("Text", _viewModel, "SearchText", true, DataSourceUpdateMode.OnPropertyChanged);

                // Status filter setup
                cmbStatusFilter.Items.Add(""); // Empty option for "Any status"
                cmbStatusFilter.Items.Add("Active");
                cmbStatusFilter.Items.Add("Inactive");
                cmbStatusFilter.Items.Add("Locked");
                cmbStatusFilter.DataBindings.Add("SelectedItem", _viewModel, "StatusFilter", true, DataSourceUpdateMode.OnPropertyChanged);

                // Role filter setup
                cmbRoleFilter.DataSource = new BindingSource(_viewModel.Roles, null);
                cmbRoleFilter.DisplayMember = "RoleName";
                cmbRoleFilter.ValueMember = "RoleID";
                cmbRoleFilter.DataBindings.Add("SelectedValue", _viewModel, "RoleFilter", true, DataSourceUpdateMode.OnPropertyChanged);

                // Insert a blank item at the beginning of the role filter
                ((BindingSource)cmbRoleFilter.DataSource).Insert(0, new Role { RoleID = 0, RoleName = "All Roles" });

                // Bind activity log grid
                dgvActivityLog.DataSource = _viewModel.ActivityLogs;

                // Activity log filters
                dtpStartDate.DataBindings.Add("Value", _viewModel, "ActivityStartDate", true, DataSourceUpdateMode.OnPropertyChanged);
                dtpEndDate.DataBindings.Add("Value", _viewModel, "ActivityEndDate", true, DataSourceUpdateMode.OnPropertyChanged);
                cmbActionType.DataBindings.Add("SelectedItem", _viewModel, "ActivityTypeFilter", true, DataSourceUpdateMode.OnPropertyChanged);

                // Bind commands to buttons
                btnAddUser.Click += (s, e) => _viewModel.AddUserCommand.Execute(null);
                btnEdit.Click += (s, e) => _viewModel.EditUserCommand.Execute(null);
                btnDeactivate.Click += (s, e) => _viewModel.DeactivateUserCommand.Execute(null);
                btnDelete.Click += (s, e) => _viewModel.DeleteUserCommand.Execute(null);
                btnResetPassword.Click += (s, e) => _viewModel.ResetPasswordCommand.Execute(null);
                btnSave.Click += (s, e) => _viewModel.SaveUserCommand.Execute(null);
                btnCancel.Click += (s, e) => _viewModel.CancelEditCommand.Execute(null);
                btnExport.Click += (s, e) => _viewModel.ExportActivityCommand.Execute(null);
                btnFilter.Click += (s, e) => _viewModel.FilterActivityCommand.Execute(null);

                // Set up selection change events
                dgvUsers.SelectionChanged += DgvUsers_SelectionChanged;
                txtPassword.TextChanged += TxtPassword_TextChanged;

                // Update UI based on edit mode
                UpdateUIEditState(_viewModel.IsEditMode);
                btnEdit.Enabled = true;
                _viewModel.PropertyChanged += ViewModel_PropertyChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error setting up bindings: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatUserGrid()
        {
            dgvUsers.AutoGenerateColumns = false;
            dgvUsers.Columns.Clear();

            // Add columns
            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "UserID",
                HeaderText = "ID",
                Width = 50,
                Visible = false
            });

            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Username",
                HeaderText = "Username",
                Width = 100
            });

            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DisplayName",
                HeaderText = "Name",
                Width = 150
            });

            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "RoleName",
                HeaderText = "Role",
                Width = 100
            });

            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Status",
                HeaderText = "Status",
                Width = 80
            });

            dgvUsers.Columns.Add(new DataGridViewCheckBoxColumn
            {
                DataPropertyName = "TwoFactorEnabled",
                HeaderText = "2FA",
                Width = 50
            });

            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "LastLogin",
                HeaderText = "Last Login",
                Width = 120
            });
        }

        private void FormatActivityLogGrid()
        {
            dgvActivityLog.AutoGenerateColumns = false;
            dgvActivityLog.Columns.Clear();

            // Add columns
            dgvActivityLog.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Timestamp",
                HeaderText = "Timestamp",
                Width = 150
            });

            dgvActivityLog.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Username",
                HeaderText = "User",
                Width = 100
            });

            dgvActivityLog.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "EventType",
                HeaderText = "Action",
                Width = 100
            });

            dgvActivityLog.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TableName",
                HeaderText = "Module",
                Width = 100
            });

            dgvActivityLog.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "RecordID",
                HeaderText = "Record ID",
                Width = 80
            });

            dgvActivityLog.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "IPAddress",
                HeaderText = "IP Address",
                Width = 120
            });

            dgvActivityLog.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "SeverityText",
                HeaderText = "Severity",
                Width = 80
            });
        }

        private void InitializeStatusFilter()
        {
            // Already implemented in SetupBindings
        }

        private void InitializePermissionsTreeView()
        {
            treeViewPermissions.Nodes.Clear();

            // Group permissions by category/module
            var permissionGroups = _viewModel.Permissions
                .GroupBy(p => p.Category ?? "General")
                .OrderBy(g => g.Key);

            foreach (var group in permissionGroups)
            {
                var categoryNode = new TreeNode(group.Key);

                foreach (var permission in group.OrderBy(p => p.PermissionName))
                {
                    var permissionNode = new TreeNode(permission.PermissionName)
                    {
                        Tag = permission.PermissionID,
                        ToolTipText = permission.Description,
                        Checked = permission.IsAssigned
                    };

                    categoryNode.Nodes.Add(permissionNode);
                }

                treeViewPermissions.Nodes.Add(categoryNode);
            }

            // Expand all nodes for better visibility
            treeViewPermissions.ExpandAll();
        }

        private void DgvUsers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count > 0 && !_viewModel.IsEditMode)
            {
                User selectedUser = dgvUsers.SelectedRows[0].DataBoundItem as User;
                _viewModel.SelectedUser = selectedUser;
            }
        }

        private void TxtPassword_TextChanged(object sender, EventArgs e)
        {
            // Calculate password strength
            string password = txtPassword.Text;
            int strength = CalculatePasswordStrength(password);

            // Update progress bar
            progressBarPasswordStrength.Value = strength;

            // Update strength label text and color
            if (strength < 40)
            {
                lblPasswordStrength.Text = "Weak";
                lblPasswordStrength.ForeColor = Color.Red;
            }
            else if (strength < 70)
            {
                lblPasswordStrength.Text = "Medium";
                lblPasswordStrength.ForeColor = Color.Orange;
            }
            else
            {
                lblPasswordStrength.Text = "Strong";
                lblPasswordStrength.ForeColor = Color.Green;
            }
        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsEditMode")
            {
                UpdateUIEditState(_viewModel.IsEditMode);
            }
        }

        private void UpdateUIEditState(bool isEditMode)
        {
            // Update control states based on edit mode
            txtUsername.ReadOnly = !isEditMode || (_viewModel.SelectedUser?.UserID > 0);
            txtDisplayName.ReadOnly = !isEditMode;
            txtEmail.ReadOnly = !isEditMode;
            txtPhone.ReadOnly = !isEditMode;
            cmbRoles.Enabled = isEditMode;
            chkTwoFactorEnabled.Enabled = isEditMode;
            dateTimePickerExpiration.Enabled = isEditMode;
            treeViewPermissions.Enabled = _viewModel.IsEditMode;

            // Security tab controls
            txtPassword.Enabled = isEditMode;
            txtConfirmPassword.Enabled = isEditMode;

            // Day restriction checkboxes
            chkSunday.Enabled = isEditMode;
            chkMonday.Enabled = isEditMode;
            chkTuesday.Enabled = isEditMode;
            chkWednesday.Enabled = isEditMode;
            chkThursday.Enabled = isEditMode;
            chkFriday.Enabled = isEditMode;
            chkSaturday.Enabled = isEditMode;

            // Button states
            btnSave.Enabled = isEditMode;
            btnCancel.Enabled = isEditMode;
            btnResetPassword.Enabled = !isEditMode && _viewModel.SelectedUser?.UserID > 0;

            // The following controls should be disabled in edit mode
            btnAddUser.Enabled = !isEditMode;
            btnEdit.Enabled = !isEditMode && _viewModel.SelectedUser != null;
            btnDeactivate.Enabled = !isEditMode && _viewModel.SelectedUser != null;
            btnDelete.Enabled = !isEditMode && _viewModel.SelectedUser != null;

            dgvUsers.Enabled = !isEditMode;

            // Update permissions tree based on role selection
            //if (_viewModel.SelectedUser != null)
            //{
            //    UpdatePermissionsTree();
            //}
        }

        private void UpdatePermissionsTree()
        {
            if (_viewModel.SelectedUser != null && treeViewPermissions.Nodes.Count > 0)
            {
                // Disable or enable nodes based on edit mode
                
                SetTreeNodesEnabled(treeViewPermissions.Nodes, _viewModel.IsEditMode);
            }
        }

        private void SetTreeNodesEnabled(TreeNodeCollection nodes, bool enabled)
        {
            foreach (TreeNode node in nodes)
            {
                node.ForeColor = enabled ? SystemColors.ControlText : SystemColors.GrayText;
                

                if (node.Nodes.Count > 0)
                {
                    SetTreeNodesEnabled(node.Nodes, enabled);
                }
            }
        }

        private int CalculatePasswordStrength(string password)
        {
            if (string.IsNullOrEmpty(password))
                return 0;

            int score = 0;

            // Length check
            if (password.Length >= 8)
                score += 20;
            else if (password.Length >= 6)
                score += 10;

            // Complexity checks
            if (password.Any(char.IsUpper))
                score += 20;

            if (password.Any(char.IsLower))
                score += 20;

            if (password.Any(char.IsDigit))
                score += 20;

            if (password.Any(c => !char.IsLetterOrDigit(c)))
                score += 20;

            return Math.Min(100, score);
        }
    }
}
