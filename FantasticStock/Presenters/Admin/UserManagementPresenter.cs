using FantasticStock.Models;
using FantasticStock.Services.Admin;
using FantasticStock.Views.Admin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasticStock.Presenters.Admin
{
    class UserManagementPresenter
    {
        private readonly IUserManagementView _view;
        private readonly IUserService _userService;
        private readonly IAuditService _activityService;
        private User _currentUser;

        public UserManagementPresenter(IUserManagementView view, IUserService userService, IAuditService activityService)
        {
            _view = view;
            _userService = userService;
            _activityService = activityService;

            _view.SearchTextChanged += OnSearchTextChanged;
            _view.RoleFilterChanged += OnRoleFilterChanged;
            _view.StatusFilterChanged += OnStatusFilterChanged;
            _view.UserSelected += OnUserSelected;
            _view.AddUserClicked += OnAddUserClicked;
            _view.EditUserClicked += OnEditUserClicked;
            _view.DeactivateUserClicked += OnDeactivateUserClicked;
            _view.DeleteUserClicked += OnDeleteUserClicked;
            _view.SaveUserClicked += OnSaveUserClicked;
            _view.CancelEditClicked += OnCancelEditClicked;
            _view.ResetPasswordClicked += OnResetPasswordClicked;
            _view.PasswordTextChanged += OnPasswordTextChanged;
            _view.FilterActivityClicked += OnFilterActivityClicked;
            _view.ExportActivityClicked += OnExportActivityClicked;

            LoadAllUsers();
        }

        private void LoadAllUsers()
        {
            var users = new BindingList<User>(_userService.GetAllUsers());
            _view.DisplayUsers(users);
        }

        private void SearchUsers()
        {
            
            var userList = _userService.GetAllUsers();

            var filteredUsers = userList.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(_view.SearchText))
            {
                string searchLower = _view.SearchText.ToLower();
                filteredUsers = filteredUsers.Where(u =>
                    u.Username.ToLower().Contains(searchLower) ||
                    u.DisplayName.ToLower().Contains(searchLower) ||
                    u.Email.ToLower().Contains(searchLower));
            }

            if (!string.IsNullOrWhiteSpace(_view.StatusFilter) && _view.StatusFilter != "All status")
            {
                filteredUsers = filteredUsers.Where(u => u.Status == _view.StatusFilter);
            }

            if (_view.RoleFilter.HasValue && _view.RoleFilter.Value > 0)
            {
                filteredUsers = filteredUsers.Where(u => u.RoleID == _view.RoleFilter.Value);
            }

            var users = new BindingList<User>(filteredUsers.ToList());
            _view.DisplayUsers(users);
        }

        private void OnSearchTextChanged(object sender, EventArgs e)
        {
            SearchUsers();
        }

        private void OnRoleFilterChanged(object sender, EventArgs e)
        {
            SearchUsers();
        }

        private void OnStatusFilterChanged(object sender, EventArgs e)
        {
            SearchUsers();
        }

        private void OnUserSelected(object sender, EventArgs e)
        {
            if (_view.SelectedUserId.HasValue)
            {
                _currentUser = _userService.GetUserById(_view.SelectedUserId.Value);
                if (_currentUser != null)
                {
                    _view.ShowUserDetails(_currentUser);
                    _view.IsEdit = false;
                }
            }
        }

        private void OnAddUserClicked(object sender, EventArgs e)
        {
            _currentUser = null;
            _view.ClearUserDetails();
            _view.IsEdit = true;
        }

        private void OnEditUserClicked(object sender, EventArgs e)
        {
            if (_view.SelectedUserId.HasValue)
            {
                _currentUser = _userService.GetUserById(_view.SelectedUserId.Value);
                if (_currentUser != null)
                {
                    _view.ShowUserDetails(_currentUser);
                    _view.IsEdit = true;
                }
                else
                {
                    _view.ShowMessage("Unable to load selected user for editing.");
                }
            }
            else
            {
                _view.ShowMessage("Please select a user to edit.");
            }
        }

        private void OnDeactivateUserClicked(object sender, EventArgs e)
        {
            if (_view.SelectedUserId.HasValue)
            {
                if (_view.ConfirmAction("Are you sure you want to deactivate this user?"))
                {
                    if (_userService.DeactivateUser(_view.SelectedUserId.Value))
                    {
                        _view.ShowMessage("User has been deactivated successfully.");
                        SearchUsers();
                    }
                    else
                    {
                        _view.ShowMessage("Failed to deactivate user.");
                    }
                }
            }
            else
            {
                _view.ShowMessage("Please select a user to deactivate.");
            }
        }

        private void OnDeleteUserClicked(object sender, EventArgs e)
        {
            if (_view.SelectedUserId.HasValue)
            {
                if (_view.ConfirmAction("Are you sure you want to delete this user? This action cannot be undone."))
                {
                    if (_userService.DeleteUser(_view.SelectedUserId.Value))
                    {
                        _view.ShowMessage("User has been deleted successfully.");
                        _view.ClearUserDetails();
                        SearchUsers();
                    }
                    else
                    {
                        _view.ShowMessage("Failed to delete user.");
                    }
                }
            }
            else
            {
                _view.ShowMessage("Please select a user to delete.");
            }
        }

        private void OnSaveUserClicked(object sender, EventArgs e)
        {
            User user = _view.GetUserFromForm();
            bool isNewUser = _currentUser == null;

            if (isNewUser)
            {
                user.Status = "Active";
                if (_userService.CreateUser(user, user.Password) != -1)
                {
                    _view.ShowMessage("User has been added successfully.");
                    _view.ClearUserDetails();
                    _view.IsEdit = false;
                    SearchUsers();
                }
                else
                {
                    _view.ShowMessage("Failed to add user. The username may already be in use.");
                }
            }
            else
            {
                user.UserID = _currentUser.UserID;
                user.Status = _currentUser.Status;
                if (_userService.UpdateUser(user))
                {
                    _view.ShowMessage("User has been updated successfully.");
                    _view.IsEdit = false;
                    SearchUsers();
                }
                else
                {
                    _view.ShowMessage("Failed to update user. The username may already be in use.");
                }
            }
        }

        private void OnCancelEditClicked(object sender, EventArgs e)
        {
            _currentUser = null;
            _view.ClearUserDetails();
            _view.IsEdit = false;
        }

        private void OnResetPasswordClicked(object sender, EventArgs e)
        {
            if (_view.SelectedUserId.HasValue)
            {
                string newPassword = "TemporaryPwd123!";

                if (_userService.UpdateUserPassword(_view.SelectedUserId.Value, newPassword))
                {
                    _view.ShowMessage($"Password has been reset. Temporary password: {newPassword}");
                }
                else
                {
                    _view.ShowMessage("Failed to reset password.");
                }
            }
            else
            {
                _view.ShowMessage("Please select a user to reset password.");
            }
        }

        private void OnPasswordTextChanged(object sender, EventArgs e)
        {
            User formUser = _view.GetUserFromForm();
            if (!string.IsNullOrEmpty(formUser.Password))
            {
                int strength = EvaluatePasswordStrength(formUser.Password);
                _view.SetPasswordStrength(strength);
            }
            else
            {
                _view.SetPasswordStrength(0);
            }
        }

        private int EvaluatePasswordStrength(string password)
        {
            if (string.IsNullOrEmpty(password))
                return 0;

            int score = 0;

            if (password.Length >= 8)
                score += 20;
            else if (password.Length >= 6)
                score += 10;

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

        private void OnFilterActivityClicked(object sender, EventArgs e)
        {
            List<AuditLogEntry> activities;

            if (!string.IsNullOrWhiteSpace(_view.ActionTypeFilter))
            {
                activities = _activityService.GetAuditLogs(
                    _view.StartDate, _view.EndDate, _view.ActionTypeFilter);
            }
            else
            {
                activities = _activityService.GetAuditLogs(_view.StartDate, _view.EndDate);
            }

            var bindingListActivities = new BindingList<AuditLogEntry>(activities);
            _view.DisplayActivities(bindingListActivities);

            var availableEventTypes = activities.Select(a => a.EventType)
                                      .Where(t => !string.IsNullOrEmpty(t))
                                      .Distinct()
                                      .ToList();
            _view.UpdateEventTypeFilters(availableEventTypes);
        }

        private void OnExportActivityClicked(object sender, EventArgs e)
        {
            string filePath = "C:\\Exports\\activity_log.csv";

            List<AuditLogEntry> activities;
            if (!string.IsNullOrWhiteSpace(_view.ActionTypeFilter))
            {
                activities = _activityService.GetAuditLogs(
                    _view.StartDate, _view.EndDate, _view.ActionTypeFilter);
            }
            else
            {
                activities = _activityService.GetAuditLogs(_view.StartDate, _view.EndDate);
            }

            if (_activityService.ExportActivities(activities, filePath))
            {
                _view.ShowMessage($"Activity log has been exported to {filePath}");
            }
            else
            {
                _view.ShowMessage("Failed to export activity log.");
            }
        }
    }
}
