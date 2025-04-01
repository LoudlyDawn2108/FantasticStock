using FantasticStock.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasticStock.Views.Admin
{
    interface IUserManagementView
    {
        void DisplayUsers(BindingList<User> users);
        string SearchText { get; }
        int? RoleFilter { get; }
        string StatusFilter { get; }
        int? SelectedUserId { get; }
        bool IsEdit { get; set; }

        void ShowUserDetails(User user);
        void ClearUserDetails();
        User GetUserFromForm();
        void SetPasswordStrength(int strengthPercentage);

        void DisplayActivities(BindingList<AuditLogEntry> activities);
        void UpdateEventTypeFilters(IEnumerable<string> availableEventTypes);
        DateTime StartDate { get; }
        DateTime EndDate { get; }
        string ActionTypeFilter { get; }

        event EventHandler SearchTextChanged;
        event EventHandler RoleFilterChanged;
        event EventHandler StatusFilterChanged;
        event EventHandler UserSelected;
        event EventHandler AddUserClicked;
        event EventHandler EditUserClicked;
        event EventHandler DeactivateUserClicked;
        event EventHandler DeleteUserClicked;
        event EventHandler SaveUserClicked;
        event EventHandler CancelEditClicked;
        event EventHandler ResetPasswordClicked;
        event EventHandler PasswordTextChanged;
        event EventHandler FilterActivityClicked;
        event EventHandler ExportActivityClicked;

        void ShowMessage(string message);
        bool ConfirmAction(string message);
    }
}
