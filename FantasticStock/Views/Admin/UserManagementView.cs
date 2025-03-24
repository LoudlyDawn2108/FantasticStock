using FantasticStock.Common;
using FantasticStock.Models;
using FantasticStock.Presenters.Admin;
using FantasticStock.Services.Admin;
using FantasticStock.ViewModels;
using FantasticStock.Views.Admin;
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
    public partial class UserManagementView : UserControl, IUserManagementView
    {
        private readonly UserManagementPresenter _presenter;
        private bool _isEdit = false;

        public UserManagementView()
        {
            InitializeComponent();

            SetupUsersDataGridView();
            SetupActivityLogDataGridView();

            PopulateRoleComboBoxes();
            PopulateStatusComboBoxes();
            PopulateActionTypeComboBox();

            dtpStartDate.Value = DateTime.Now.AddDays(-7);
            dtpEndDate.Value = DateTime.Now;

            RegisterEventHandlers();

            IUserService userService = ServiceLocator.GetService<IUserService>();
            IAuditService auditService = ServiceLocator.GetService<IAuditService>();
            _presenter = new UserManagementPresenter(this, userService, auditService);
        }

        #region IUserManagementView properties

        public string SearchText => txtSearch.Text;

        public int? RoleFilter
        {
            get
            {
                if (cmbRoleFilter.SelectedIndex > 0)
                    return (int)cmbRoleFilter.SelectedValue;
                return null;
            }
        }

        public string StatusFilter
        {
            get
            {
                if (cmbStatusFilter.SelectedIndex > 0)
                    return (string)cmbStatusFilter.SelectedItem;
                return null;
            }
        }

        public int? SelectedUserId
        {
            get
            {
                if (dgvUsers.SelectedRows.Count > 0)
                    return (int)dgvUsers.SelectedRows[0].Cells["UserID"].Value;
                return null;
            }
        }

        public DateTime StartDate => dtpStartDate.Value;

        public DateTime EndDate => dtpEndDate.Value;

        public string ActionTypeFilter
        {
            get
            {
                if (cmbActionType.SelectedIndex > 0)
                    return (string)cmbActionType.SelectedValue;
                return null;
            }
        }

        public bool IsEdit
        {
            get { return _isEdit; }
            set 
            { 
                _isEdit = value;
                UpdateControlsEditState();
            }
        }

        #endregion

        #region IUserManagementView events

        public event EventHandler SearchTextChanged;
        public event EventHandler RoleFilterChanged;
        public event EventHandler StatusFilterChanged;
        public event EventHandler UserSelected;
        public event EventHandler AddUserClicked;
        public event EventHandler EditUserClicked;
        public event EventHandler DeactivateUserClicked;
        public event EventHandler DeleteUserClicked;
        public event EventHandler SaveUserClicked;
        public event EventHandler CancelEditClicked;
        public event EventHandler ResetPasswordClicked;
        public event EventHandler PasswordTextChanged;
        public event EventHandler FilterActivityClicked;
        public event EventHandler ExportActivityClicked;

        #endregion

        #region IUserManagementView methods

        public void DisplayUsers(BindingList<User> users)
        {
            dgvUsers.DataSource = null;
            dgvUsers.DataSource = users;
        }

        public void ShowUserDetails(User user)
        {
            txtUsername.Text = user.Username;
            txtDisplayName.Text = user.DisplayName;
            txtEmail.Text = user.Email;
            txtPhone.Text = user.Phone;
            cmbRoles.SelectedIndex = user.RoleID - 1;

            txtPassword.Text = string.Empty;
            txtConfirmPassword.Text = string.Empty;
            progressBarPasswordStrength.Value = 0;
            chkTwoFactorEnabled.Checked = user.TwoFactorEnabled;

            chkSunday.Checked = user.AllowedDays[0];
            chkMonday.Checked = user.AllowedDays[1];
            chkTuesday.Checked = user.AllowedDays[2];
            chkWednesday.Checked = user.AllowedDays[3];
            chkThursday.Checked = user.AllowedDays[4];
            chkFriday.Checked = user.AllowedDays[5];
            chkSaturday.Checked = user.AllowedDays[6];

            panelUserDetail.Enabled = true;
            UpdateControlsEditState();
        }

        public void ClearUserDetails()
        {
            txtUsername.Text = string.Empty;
            txtDisplayName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtPhone.Text = string.Empty;
            cmbRoles.SelectedIndex = 0;

            txtPassword.Text = string.Empty;
            txtConfirmPassword.Text = string.Empty;
            progressBarPasswordStrength.Value = 0;
            chkTwoFactorEnabled.Checked = false;

            chkSunday.Checked = true;
            chkMonday.Checked = true;
            chkTuesday.Checked = true;
            chkWednesday.Checked = true;
            chkThursday.Checked = true;
            chkFriday.Checked = true;
            chkSaturday.Checked = true;

            panelUserDetail.Enabled = true;
            IsEdit = true;
        }

        public User GetUserFromForm()
        {
            User user = new User
            {
                Username = txtUsername.Text.Trim(),
                DisplayName = txtDisplayName.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Phone = txtPhone.Text.Trim(),
                Password = txtPassword.Text,
                RoleID = (int)cmbRoles.SelectedValue,
                TwoFactorEnabled = chkTwoFactorEnabled.Checked,
                AllowedDays = new bool[]
                { 
                    chkSunday.Checked,
                    chkMonday.Checked,
                    chkTuesday.Checked,
                    chkWednesday.Checked,
                    chkThursday.Checked,
                    chkFriday.Checked,
                    chkSaturday.Checked
                }
            };

            return user;
        }

        public void SetPasswordStrength(int strengthPercentage)
        {
            progressBarPasswordStrength.Value = Math.Min(100, Math.Max(0, strengthPercentage));

            if (strengthPercentage < 40)
                progressBarPasswordStrength.ForeColor = Color.Red;
            else if (strengthPercentage < 70)
                progressBarPasswordStrength.ForeColor = Color.Yellow;
            else
                progressBarPasswordStrength.ForeColor = Color.Green;
        }

        public void DisplayActivities(BindingList<AuditLogEntry> activities)
        {
            dgvActivityLog.DataSource = null;
            dgvActivityLog.DataSource = activities;
        }

        public void UpdateEventTypeFilters(IEnumerable<string> availableEventTypes)
        {
            var currentSelectedValue = cmbActionType.SelectedIndex > 0
                ? cmbActionType.SelectedValue
                : null;

            var actionTypes = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("", "All event types")
            };

            foreach (var eventType in availableEventTypes)
            {
                actionTypes.Add(new KeyValuePair<string, string>(eventType, eventType));
            }

            cmbActionType.DisplayMember = "Value";
            cmbActionType.ValueMember = "Key";
            cmbActionType.DataSource = new BindingList<KeyValuePair<string, string>>(actionTypes);

            if (currentSelectedValue != null)
            {
                var matchingItem = actionTypes.FirstOrDefault(at => at.Key.Equals(currentSelectedValue));
                if (!EqualityComparer<KeyValuePair<string, string>>.Default.Equals(matchingItem, default))
                {
                    cmbActionType.SelectedValue = currentSelectedValue;
                }
                else
                {
                    cmbActionType.SelectedIndex = 0;
                }
            }
            else
            {
                cmbActionType.SelectedIndex = 0;
            }
        }


        public void ShowMessage(string message)
        {
            MessageBox.Show(message, "User Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public bool ConfirmAction(string message)
        {
            return MessageBox.Show(message, "Confirm Action", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        #endregion

        #region Helper methods
        private void SetupUsersDataGridView()
        {
            dgvUsers.AutoGenerateColumns = false;

            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "UserID",
                Name = "UserID",
                HeaderText = "ID",
                Width = 50,
                Visible = false
            });

            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Username",
                Name = "Username",
                HeaderText = "Username",
                Width = 120
            });

            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DisplayName",
                Name = "DisplayName",
                HeaderText = "Display Name",
                Width = 150
            });

            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Email",
                Name = "Email",
                HeaderText = "Email",
                Width = 180
            });

            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "RoleName",
                Name = "Role",
                HeaderText = "Role",
                Width = 100
            });

            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Status",
                Name = "Status",
                HeaderText = "Status",
                Width = 80
            });

            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "LastLoginDate",
                Name = "LastLoginDate",
                HeaderText = "Last Login",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "yyyy-MM-dd HH:mm" }
            });

            dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsers.AllowUserToAddRows = false;
            dgvUsers.AllowUserToDeleteRows = false;
            dgvUsers.ReadOnly = true;
            dgvUsers.MultiSelect = false;
        }

        private void SetupActivityLogDataGridView()
        {
            dgvActivityLog.AutoGenerateColumns = false;


            dgvActivityLog.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Username",
                Name = "Username",
                HeaderText = "Username",
                Width = 100
            });

            dgvActivityLog.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Timestamp",
                Name = "Timestamp",
                HeaderText = "Timestamp",
                Width = 150,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "yyyy-MM-dd HH:mm:ss" }
            });


            dgvActivityLog.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "EventType",
                Name = "EventType",
                HeaderText = "Event Type",
                Width = 120
            });

            dgvActivityLog.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvActivityLog.AllowUserToAddRows = false;
            dgvActivityLog.AllowUserToDeleteRows = false;
            dgvActivityLog.ReadOnly = true;
        }

        private void PopulateRoleComboBoxes()
        {
            var rolesList = new List<KeyValuePair<int, string>>
            {
                new KeyValuePair<int, string>(0, "All roles"),
                new KeyValuePair<int, string>(1, "Admin"),
                new KeyValuePair<int, string>(2, "Manager"),
                new KeyValuePair<int, string>(3, "Sales"),
                new KeyValuePair<int, string>(4, "Inventory"),
                new KeyValuePair<int, string>(5, "Finance")
            };

            cmbRoleFilter.DisplayMember = "Value";
            cmbRoleFilter.ValueMember = "Key";
            cmbRoleFilter.DataSource = new BindingList<KeyValuePair<int, string>>(rolesList);
            cmbRoleFilter.SelectedIndex = 0;

            cmbRoles.DisplayMember = "Value";
            cmbRoles.ValueMember = "Key";
            cmbRoles.DataSource = new BindingList<KeyValuePair<int, string>>(
                rolesList.Where(r => r.Key != 0).ToList());
        }

        private void PopulateStatusComboBoxes()
        {
            var statusList = new List<string>
            {
                "All statuses",
                "Active",
                "Inactive",
                "Locked"
            };

            cmbStatusFilter.DataSource = new BindingList<string>(statusList);
            cmbStatusFilter.SelectedIndex = 0;
        }

        private void PopulateActionTypeComboBox()
        {
            var actionTypes = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("", "All event types")
            };

            cmbActionType.DisplayMember = "Value";
            cmbActionType.ValueMember = "Key";
            cmbActionType.DataSource = new BindingList<KeyValuePair<string, string>>(actionTypes);
            cmbActionType.SelectedIndex = 0;
        }

        private void RegisterEventHandlers()
        {
            txtSearch.TextChanged += (s, e) => SearchTextChanged?.Invoke(s, e);
            cmbRoleFilter.SelectedIndexChanged += (s, e) => RoleFilterChanged?.Invoke(s, e);
            cmbStatusFilter.SelectedIndexChanged += (s, e) => StatusFilterChanged?.Invoke(s, e);
            dgvUsers.SelectionChanged += (s, e) => UserSelected?.Invoke(s, e);
            btnAddUser.Click += (s, e) => AddUserClicked?.Invoke(s, e);
            btnEdit.Click += (s, e) => EditUserClicked?.Invoke(s, e);
            btnDeactivate.Click += (s, e) => DeactivateUserClicked?.Invoke(s, e);
            btnDelete.Click += (s, e) => DeleteUserClicked?.Invoke(s, e);

            btnSave.Click += (s, e) => SaveUserClicked?.Invoke(s, e);
            btnCancel.Click += (s, e) => CancelEditClicked?.Invoke(s, e);
            btnResetPassword.Click += (s, e) => ResetPasswordClicked?.Invoke(s, e);
            txtPassword.TextChanged += (s, e) => PasswordTextChanged?.Invoke(s, e);

            btnFilter.Click += (s, e) => FilterActivityClicked?.Invoke(s, e);
            btnExport.Click += (s, e) => ExportActivityClicked?.Invoke(s, e);
        }

        private void UpdateControlsEditState()
        {
            txtUsername.Enabled = _isEdit;
            txtDisplayName.Enabled = _isEdit;
            txtEmail.Enabled = _isEdit;
            txtPhone.Enabled = _isEdit;
            cmbRoles.Enabled = _isEdit;

            txtPassword.Enabled = _isEdit;
            txtConfirmPassword.Enabled = _isEdit;
            chkTwoFactorEnabled.Enabled = _isEdit;

            chkSunday.Enabled = _isEdit;
            chkMonday.Enabled = _isEdit;
            chkTuesday.Enabled = _isEdit;
            chkWednesday.Enabled = _isEdit;
            chkThursday.Enabled = _isEdit;
            chkFriday.Enabled = _isEdit;
            chkSaturday.Enabled = _isEdit;

            btnSave.Enabled = _isEdit;
            btnCancel.Enabled = _isEdit;

            btnEdit.Enabled = !_isEdit && SelectedUserId.HasValue;
            btnAddUser.Enabled = !_isEdit;
            btnDeactivate.Enabled = !_isEdit && SelectedUserId.HasValue;
            btnDelete.Enabled = !_isEdit && SelectedUserId.HasValue;
            btnResetPassword.Enabled = !_isEdit && SelectedUserId.HasValue;
        }

        #endregion
    }
}
