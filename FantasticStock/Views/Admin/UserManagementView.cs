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

        public UserManagementView()
        {
            InitializeComponent();

            // Initialize view model
            _viewModel = new UserManagementViewModel();

            // Set up data bindings
            SetupBindings();

            FormatUserGrid();
        }

        private void SetupBindings()
        {
            // Bind users grid
            dgvUsers.DataSource = _viewModel.Users;

            // Bind roles grid
            dgvRoles.DataSource = _viewModel.Roles;

            // Bind permissions list
            lstPermissions.DataSource = _viewModel.Permissions;
            lstPermissions.DisplayMember = "PermissionName";
            lstPermissions.ValueMember = "PermissionID";

            // Bind activity logs grid
            dgvActivityLogs.DataSource = _viewModel.ActivityLogs;

            // Bind search and filter controls
            txtSearch.DataBindings.Add("Text", _viewModel, "SearchText", true, DataSourceUpdateMode.OnPropertyChanged);
            cmbStatusFilter.DataBindings.Add("SelectedItem", _viewModel, "StatusFilter", true, DataSourceUpdateMode.OnPropertyChanged);
            cmbRoleFilter.DataSource = _viewModel.Roles;
            cmbRoleFilter.DisplayMember = "RoleName";
            cmbRoleFilter.ValueMember = "RoleID";
            cmbRoleFilter.DataBindings.Add("SelectedValue", _viewModel, "RoleFilter", true, DataSourceUpdateMode.OnPropertyChanged);

            // Activity log filters
            dtpStartDate.DataBindings.Add("Value", _viewModel, "ActivityStartDate", true, DataSourceUpdateMode.OnPropertyChanged);
            dtpEndDate.DataBindings.Add("Value", _viewModel, "ActivityEndDate", true, DataSourceUpdateMode.OnPropertyChanged);
            cmbActivityType.DataBindings.Add("Text", _viewModel, "ActivityTypeFilter", true, DataSourceUpdateMode.OnPropertyChanged);

            // Bind commands to buttons
            btnAddUser.Click += (s, e) => _viewModel.AddUserCommand.Execute(null);
            btnEditUser.Click += (s, e) => _viewModel.EditUserCommand.Execute(null);
            btnDeactivate.Click += (s, e) => _viewModel.DeactivateUserCommand.Execute(null);
            btnDelete.Click += (s, e) => _viewModel.DeleteUserCommand.Execute(null);
            btnResetPassword.Click += (s, e) => _viewModel.ResetPasswordCommand.Execute(null);
            btnSaveUser.Click += (s, e) => _viewModel.SaveUserCommand.Execute(null);
            btnCancel.Click += (s, e) => _viewModel.CancelEditCommand.Execute(null);
            btnRefresh.Click += (s, e) => _viewModel.RefreshDataCommand.Execute(null);
            btnExportActivity.Click += (s, e) => _viewModel.ExportActivityCommand.Execute(null);
            btnFilterActivity.Click += (s, e) => _viewModel.FilterActivityCommand.Execute(null);

            // Selection change events
            //dgvUsers.SelectionChanged += (s, e) =>
            //{
            //    if (dgvUsers.SelectedRows.Count > 0)
            //    {
            //        _viewModel.SelectedUser = dgvUsers.SelectedRows[0].DataBoundItem as FantasticStock.Models.User;
            //    }
            //};

            dgvRoles.SelectionChanged += (s, e) =>
            {
                if (dgvRoles.SelectedRows.Count > 0)
                {
                    _viewModel.SelectedRole = dgvRoles.SelectedRows[0].DataBoundItem as FantasticStock.Models.Role;
                }
            };
        }

        private void FormatUserGrid()
        {
            dgvUsers.ReadOnly = true;
            dgvUsers.AllowUserToAddRows = false;
            dgvUsers.AllowUserToDeleteRows = false;
            dgvUsers.MultiSelect = false;

            dgvUsers.Columns["UserID"].Visible = false;
            dgvUsers.Columns["RoleID"].Visible = false;
            dgvUsers.Columns["TwoFactorEnabled"].Visible = false;
            dgvUsers.Columns["AccountExpiry"].Visible = false;
            dgvUsers.Columns["LastLogin"].HeaderText = "Last Login";
            dgvUsers.Columns["CreatedDate"].Visible = false;
            dgvUsers.Columns["ModifiedDate"].Visible = false;
            dgvUsers.Columns["Email"].Visible = false;
            dgvUsers.Columns["Phone"].Visible = false;
            dgvUsers.Columns["Status"].HeaderText = "Status";
            dgvUsers.Columns["Username"].HeaderText = "Username";
            dgvUsers.Columns["DisplayName"].HeaderText = "Display Name";
            dgvUsers.Columns["DisplayName"].Width = 150;
            dgvUsers.Columns["RoleName"].HeaderText = "Role";
        }

        private void dgvUsers_SelectionChanged(object sender, EventArgs e)
        {
            dgvUsers.ClearSelection();
        }

        private void flowLayoutPanel3_Resize(object sender, EventArgs e)
        {
            label6.Width = flowLayoutPanel3.Width - flowLayoutPanel3.Padding.Left * 2;
            label1.Width = label6.Width;
            if (label6.Width < 600)
            {
                flowLayoutPanel2.SetFlowBreak(panel5, true);
                flowLayoutPanel4.SetFlowBreak(panel9, true);
                flowLayoutPanel5.SetFlowBreak(panel11, true);
                flowLayoutPanel2.WrapContents = true;
                flowLayoutPanel4.WrapContents = true;
                flowLayoutPanel5.WrapContents = true;
                panel9.Width = label6.Width;
                panel10.Width = label6.Width;
                panel11.Width = label6.Width;
                panel12.Width = label6.Width;
                panel5.Width = label6.Width;
                panel6.Width = label6.Width;
            }
            else
            {
                flowLayoutPanel2.SetFlowBreak(panel5, false);
                flowLayoutPanel4.SetFlowBreak(panel9, false);
                flowLayoutPanel5.SetFlowBreak(panel9, false);
                flowLayoutPanel2.WrapContents = false;
                flowLayoutPanel4.WrapContents = false;
                flowLayoutPanel5.WrapContents = false;
                panel9.Width = label6.Width / 2;
                panel10.Width = label6.Width / 2;
                panel11.Width = label6.Width / 2;
                panel12.Width = label6.Width / 2;
                panel5.Width = label6.Width / 2;
                panel6.Width = label6.Width / 2;
            }
        }
    }
}
