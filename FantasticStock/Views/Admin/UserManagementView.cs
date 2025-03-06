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
            dgvUsers.SelectionChanged += (s, e) =>
            {
                if (dgvUsers.SelectedRows.Count > 0)
                {
                    _viewModel.SelectedUser = dgvUsers.SelectedRows[0].DataBoundItem as FantasticStock.Models.User;
                }
            };

            dgvRoles.SelectionChanged += (s, e) =>
            {
                if (dgvRoles.SelectedRows.Count > 0)
                {
                    _viewModel.SelectedRole = dgvRoles.SelectedRows[0].DataBoundItem as FantasticStock.Models.Role;
                }
            };
        }
    }
}
