using System;
using System.Windows.Forms;
using AdminDomain.Common;
using AdminDomain.Services;

namespace AdminDomain.Views
{
    public partial class ChangePasswordForm : Form
    {
        private readonly IUserService _userService;
        
        public ChangePasswordForm()
        {
            InitializeComponent();
            _userService = ServiceLocator.GetService<IUserService>();
        }
        
        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            string currentPassword = txtCurrentPassword.Text;
            string newPassword = txtNewPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;
            
            // Validate inputs
            if (string.IsNullOrEmpty(currentPassword) || 
                string.IsNullOrEmpty(newPassword) || 
                string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("All fields are required.", "Validation Error", 
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            if (newPassword != confirmPassword)
            {
                MessageBox.Show("New password and confirmation do not match.", "Validation Error", 
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNewPassword.Clear();
                txtConfirmPassword.Clear();
                txtNewPassword.Focus();
                return;
            }
            
            if (newPassword.Length < 8)
            {
                MessageBox.Show("New password must be at least 8 characters long.", "Validation Error", 
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNewPassword.Clear();
                txtConfirmPassword.Clear();
                txtNewPassword.Focus();
                return;
            }
            
            try
            {
                // First verify current password
                bool isValid = _userService.ValidateUser(CurrentUser.Username, currentPassword);
                
                if (!isValid)
                {
                    MessageBox.Show("Current password is incorrect.", "Validation Error", 
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCurrentPassword.Clear();
                    txtCurrentPassword.Focus();
                    return;
                }
                
                // Update password
                bool success = _userService.UpdateUserPassword(CurrentUser.UserID, newPassword);
                
                if (success)
                {
                    // Log the action
                    var auditService = ServiceLocator.GetService<IAuditService>();
                    auditService.LogEvent(
                        CurrentUser.UserID,
                        "PasswordChange",
                        "Users",
                        CurrentUser.UserID.ToString(),
                        null,
                        $"User '{CurrentUser.Username}' changed their password at {DateTime.Parse("2025-03-02 16:19:04").ToString("yyyy-MM-dd HH:mm:ss")}"
                    );
                    
                    MessageBox.Show("Your password has been changed successfully.", "Success", 
                                   MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("Failed to change password. Please try again later.", "Error", 
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", 
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                // Log the error
                var monitoringService = ServiceLocator.GetService<IMonitoringService>();
                monitoringService.LogError("Security", $"Password change error: {ex.Message}", ex.StackTrace, 3);
            }
        }
        
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        
        // Designer-generated code would be here
        private void InitializeComponent()
        {
            this.txtCurrentPassword = new System.Windows.Forms.TextBox();
            this.txtNewPassword = new System.Windows.Forms.TextBox();
            this.txtConfirmPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnChangePassword = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtCurrentPassword
            // 
            this.txtCurrentPassword.Location = new System.Drawing.Point(158, 25);
            this.txtCurrentPassword.Name = "txtCurrentPassword";
            this.txtCurrentPassword.PasswordChar = '*';
            this.txtCurrentPassword.Size = new System.Drawing.Size(204, 23);
            this.txtCurrentPassword.TabIndex = 0;
            // 
            // txtNewPassword
            // 
            this.txtNewPassword.Location = new System.Drawing.Point(158, 67);
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.PasswordChar = '*';
            this.txtNewPassword.Size = new System.Drawing.Size(204, 23);
            this.txtNewPassword.TabIndex = 1;
            // 
            // txtConfirmPassword
            // 
            this.txtConfirmPassword.Location = new System.Drawing.Point(158, 109);
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.PasswordChar = '*';
            this.txtConfirmPassword.Size = new System.Drawing.Size(204, 23);
            this.txtConfirmPassword.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Current Password:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "New Password:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Confirm Password:";
            // 
            // btnChangePassword
            // 
            this.btnChangePassword.Location = new System.Drawing.Point(158, 153);
            this.btnChangePassword.Name = "btnChangePassword";
            this.btnChangePassword.Size = new System.Drawing.Size(98, 30);
            this.btnChangePassword.TabIndex = 6;
            this.btnChangePassword.Text = "Change";
            this.btnChangePassword.UseVisualStyleBackColor = true;
            this.btnChangePassword.Click += new System.EventHandler(this.btnChangePassword_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(264, 153);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(98, 30);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ChangePasswordForm
            // 
            this.AcceptButton = this.btnChangePassword;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(384, 201);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnChangePassword);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtConfirmPassword);
            this.Controls.Add(this.txtNewPassword);
            this.Controls.Add(this.txtCurrentPassword);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChangePasswordForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Change Password";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        
        private System.Windows.Forms.TextBox txtCurrentPassword;
        private System.Windows.Forms.TextBox txtNewPassword;
        private System.Windows.Forms.TextBox txtConfirmPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnChangePassword;
        private System.Windows.Forms.Button btnCancel;
    }
}