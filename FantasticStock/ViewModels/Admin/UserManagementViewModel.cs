using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using FantasticStock.Common;
using FantasticStock.Models;
using FantasticStock.Services.Admin;

namespace FantasticStock.ViewModels
{
    public class UserManagementViewModel : ViewModelBase
    {
        private readonly IUserService _userService;
        private readonly IAuditService _auditService;

        private BindingList<User> _users;
        private BindingList<Role> _roles;
        private BindingList<AuditLogEntry> _activityLogs;
        private User _selectedUser;
        private Role _selectedRole;
        private string _searchText;
        private string _statusFilter;
        private int? _roleFilter;
        private DateTime? _activityStartDate;
        private DateTime? _activityEndDate;
        private string _activityTypeFilter;
        private bool _isEditMode;

        public UserManagementViewModel()
        {
            _userService = ServiceLocator.GetService<IUserService>();
            _auditService = ServiceLocator.GetService<IAuditService>();
            
            // Initialize commands
            AddUserCommand = new RelayCommand(AddUser);
            EditUserCommand = new RelayCommand(EditUser, CanEditUser);
            DeactivateUserCommand = new RelayCommand(DeactivateUser, CanEditUser);
            DeleteUserCommand = new RelayCommand(DeleteUser, CanEditUser);
            ResetPasswordCommand = new RelayCommand(ResetPassword, CanEditUser);
            SaveUserCommand = new RelayCommand(SaveUser, CanSaveUser);
            CancelEditCommand = new RelayCommand(CancelEdit);
            RefreshDataCommand = new RelayCommand(RefreshData);
            ExportActivityCommand = new RelayCommand(ExportActivity);
            FilterActivityCommand = new RelayCommand(FilterActivity);

            // Initialize data
            LoadData();

            RoleFilter = 0;
        }

        #region Properties

        public BindingList<User> Users
        {
            get => _users;
        
            private set => SetProperty(ref _users, value);
        }

        public BindingList<Role> Roles
        {
            get => _roles;
            private set => SetProperty(ref _roles, value);
        }

        public BindingList<AuditLogEntry> ActivityLogs
        {
            get => _activityLogs;
            private set => SetProperty(ref _activityLogs, value);
        }

        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                if (SetProperty(ref _selectedUser, value) && value != null)
                {
                    // Update commands can execute status
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        public Role SelectedRole
        {
            get => _selectedRole;
            set
            {
                if (SetProperty(ref _selectedRole, value) && value != null)
                {
                }
            }
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                if (SetProperty(ref _searchText, value))
                {
                    FilterUsers();
                }
            }
        }

        public string StatusFilter
        {
            get => _statusFilter;
            set
            {
                if (SetProperty(ref _statusFilter, value))
                {
                    FilterUsers();
                }
            }
        }

        public int? RoleFilter
        {
            get => _roleFilter;
            set
            {
                if (SetProperty(ref _roleFilter, value))
                {
                    FilterUsers();
                }
            }
        }

        public DateTime? ActivityStartDate
        {
            get => _activityStartDate;
            set => SetProperty(ref _activityStartDate, value);
        }

        public DateTime? ActivityEndDate
        {
            get => _activityEndDate;
            set => SetProperty(ref _activityEndDate, value);
        }

        public string ActivityTypeFilter
        {
            get => _activityTypeFilter;
            set => SetProperty(ref _activityTypeFilter, value);
        }

        #endregion

        #region Commands

        public ICommand AddUserCommand { get; }
        public ICommand EditUserCommand { get; }
        public ICommand DeactivateUserCommand { get; }
        public ICommand ActivateUserCommand { get; }
        public ICommand DeleteUserCommand { get; }
        public ICommand ResetPasswordCommand { get; }
        public ICommand SaveUserCommand { get; }
        public ICommand CancelEditCommand { get; }
        public ICommand RefreshDataCommand { get; }
        public ICommand ExportActivityCommand { get; }
        public ICommand FilterActivityCommand { get; }

        #endregion

        #region Command Implementations

        private void AddUser(object parameter)
        {
            // Create a new user with default values
            SelectedUser = new User
            {
                Status = "Active",
                TwoFactorEnabled = false,
                CreatedDate = DateTime.Parse("2025-03-02 16:05:52"),
                ModifiedDate = DateTime.Parse("2025-03-02 16:05:52")
            };
            
            // Notify UI that we're in edit mode
            IsEditMode = true;
        }

        private void EditUser(object parameter)
        {
            // Make a copy of the selected user for editing
            if (SelectedUser != null)
            {
                OriginalUser = SelectedUser.Clone();
                IsEditMode = true;
            }
        }

        private void DeactivateUser(object parameter)
        {
            if (SelectedUser == null) 
                return;
            
            // Check if user is active or inactive to determine the action
            bool isActive = SelectedUser.Status == "Active";
            string action = isActive ? "deactivate" : "activate";
            string confirmTitle = isActive ? "Confirm Deactivation" : "Confirm Activation";
            
            // Show confirmation with appropriate text
            if (MessageService.ShowConfirmation($"Are you sure you want to {action} user '{SelectedUser.Username}'?", confirmTitle))
            {
                try
                {
                    bool success;
                    if (isActive)
                    {
                        // Deactivate active user
                        success = _userService.DeactivateUser(SelectedUser.UserID);
                        if (success)
                            MessageService.ShowInformation("User deactivated successfully.", "Success");
                    }
                    else
                    {
                        // Activate inactive user
                        success = _userService.ActivateUser(SelectedUser.UserID);
                        if (success)
                            MessageService.ShowInformation("User activated successfully.", "Success");
                    }
                    
                    // Refresh data to show updated status
                    RefreshData(null);
                }
                catch (Exception ex)
                {
                    MessageService.ShowError($"Failed to {action} user: {ex.Message}", "Error");
                }
            }
        }

        private void DeleteUser(object parameter)
        {
            if (SelectedUser == null)
                return;
                
            // Confirm deletion
            if (MessageService.ShowConfirmation($"Are you sure you want to delete user '{SelectedUser.Username}'?\nThis action cannot be undone!", "Confirm Deletion"))
            {
                try
                {
                    _userService.DeleteUser(SelectedUser.UserID);
                    MessageService.ShowInformation("User deleted successfully.", "Success");
                    RefreshData(null);
                }
                catch (Exception ex)
                {
                    MessageService.ShowError($"Failed to delete user: {ex.Message}", "Error");
                }
            }
        }

        private void ResetPassword(object parameter)
        {
            if (SelectedUser == null)
                return;
                
            // In a real app, you'd show a password reset dialog
            string newPassword = "DefaultPwd123!";
            
            try
            {
                _userService.UpdateUserPassword(SelectedUser.UserID, newPassword);
                MessageService.ShowInformation("Password reset successfully.", "Success");
            }
            catch (Exception ex)
            {
                MessageService.ShowError($"Failed to reset password: {ex.Message}", "Error");
            }
        }

        private User OriginalUser { get; set; }

        
        public bool IsEditMode { get => _isEditMode; set => SetProperty(ref _isEditMode, value); }

        private void SaveUser(object parameter)
        {
            if (SelectedUser == null)
                return;
                
            try
            {
                if (SelectedUser.UserID == 0)
                {
                    // Create new user
                    string password = "DefaultPwd123!"; // In a real app, get from UI
                    int userId = _userService.CreateUser(SelectedUser, password);
                    MessageService.ShowInformation("User created successfully.", "Success");
                }
                else
                {
                    // Update existing user
                    _userService.UpdateUser(SelectedUser);
                    MessageService.ShowInformation("User updated successfully.", "Success");
                }
                
                // Exit edit mode
                IsEditMode = false;
                OriginalUser = null;
                
                // Refresh data
                RefreshData(null);
            }
            catch (Exception ex)
            {
                MessageService.ShowError($"Failed to save user: {ex.Message}", "Error");
            }
        }

        private void CancelEdit(object parameter)
        {
            if (SelectedUser.UserID == 0)
            {
                // Was creating a new user, just clear selection
                SelectedUser = null;
            }
            else if (OriginalUser != null)
            {
                // Restore original values
                SelectedUser = OriginalUser;
            }
            
            IsEditMode = false;
            OriginalUser = null;
        }

        private void RefreshData(object parameter)
        {
            LoadData();
        }

        private void ExportActivity(object parameter)
        {
            MessageService.ShowInformation("Export functionality would be implemented here.", "Export");
        }

        private void FilterActivity(object parameter)
        {
            LoadActivityLogs();
        }

        #endregion

        #region Helper Methods

        private bool CanEditUser(object parameter)
        {
            return SelectedUser != null && !IsEditMode;
        }

        private bool CanSaveUser(object parameter)
        {
            return IsEditMode && SelectedUser != null && 
                   !string.IsNullOrEmpty(SelectedUser.Username) && 
                   !string.IsNullOrEmpty(SelectedUser.DisplayName) && 
                   !string.IsNullOrEmpty(SelectedUser.Email) &&
                   SelectedUser.RoleID > 0;
        }

        private void LoadData()
        {
            try
            {
                var allUsers = _userService.GetAllUsers();
                Users = new BindingList<User>(allUsers);
                
                var roleList = _userService.GetAllRoles();
                Roles = new BindingList<Role>(roleList);
                
                LoadActivityLogs();
                
                _searchText = null;
                _statusFilter = null;
                _roleFilter = null;
                OnPropertyChanged(nameof(SearchText));
                OnPropertyChanged(nameof(StatusFilter));
                OnPropertyChanged(nameof(RoleFilter));
            }
            catch (Exception ex)
            {
                MessageService.ShowError($"Failed to load data: {ex.Message}", "Error");
            }
        }

        private void LoadActivityLogs()
        {
            try
            {
                if (!ActivityStartDate.HasValue)
                    ActivityStartDate = DateTime.Parse("2025-03-02 16:08:31").AddDays(-7);
                
                if (!ActivityEndDate.HasValue)
                    ActivityEndDate = DateTime.Now;

                Console.WriteLine($"Filtering activity logs from {ActivityStartDate} to {ActivityEndDate}");

                var userId = SelectedUser?.UserID;
                var logs = _auditService.GetAuditLogs(
                    ActivityStartDate, 
                    ActivityEndDate, 
                    string.IsNullOrWhiteSpace(ActivityTypeFilter) ? null : ActivityTypeFilter,
                    userId);
                
                ActivityLogs = new BindingList<AuditLogEntry>(logs);
            }
            catch (Exception ex)
            {
                MessageService.ShowError($"Failed to load activity logs: {ex.Message}", "Error");
            }
        }

        private void FilterUsers()
        {
            if (_users == null)
                return;
                
            // Get the full list of users from the service
            var userList = _userService.GetAllUsers();
            
            // Apply filters
            var filteredUsers = userList.AsEnumerable();
            
            // Apply text search
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                string searchLower = SearchText.ToLower();
                filteredUsers = filteredUsers.Where(u => 
                    u.Username.ToLower().Contains(searchLower) ||
                    u.DisplayName.ToLower().Contains(searchLower) ||
                    u.Email.ToLower().Contains(searchLower));
            }
            
            // Apply status filter
            if (!string.IsNullOrWhiteSpace(StatusFilter) && StatusFilter != "All status")
            {
                filteredUsers = filteredUsers.Where(u => u.Status == StatusFilter);
            }
            
            // Apply role filter
            if (RoleFilter.HasValue && RoleFilter.Value > 0)
            {
                filteredUsers = filteredUsers.Where(u => u.RoleID == RoleFilter.Value);
            }
            
            // Update the collection
            Users = new BindingList<User>(filteredUsers.ToList());
        }
        
        #endregion
    }

    public static class MessageService
    {
        public static bool ShowConfirmation(string message, string title)
        {
            // In a real app, this would show a dialog and return the result
            var result = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return result == DialogResult.Yes;
        }
        
        public static void ShowInformation(string message, string title)
        {
            // In a real app, this would show a dialog
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        
        public static void ShowError(string message, string title)
        {
            // In a real app, this would show a dialog
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    public static class UserExtensions
    {
        public static User Clone(this User user)
        {
            return new User
            {
                UserID = user.UserID,
                Username = user.Username,
                DisplayName = user.DisplayName,
                Email = user.Email,
                Phone = user.Phone,
                RoleID = user.RoleID,
                RoleName = user.RoleName,
                Status = user.Status,
                TwoFactorEnabled = user.TwoFactorEnabled,
                LastLogin = user.LastLogin,
                CreatedDate = user.CreatedDate,
                ModifiedDate = user.ModifiedDate
            };
        }
    }
}