using System;
using System.Windows.Forms;
using AdminDomain.Common;
using AdminDomain.Services;

namespace AdminDomain.Views
{
    public partial class LoginForm : Form
    {
        private readonly IUserService _userService;
        
        public LoginForm()
        {
            InitializeComponent();
            _userService = ServiceLocator.GetService<IUserService>();
        }
        
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Login Error", 
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            try
            {
                bool isValid = _userService.ValidateUser(username, password);
                
                if (isValid)
                {
                    // Get user details for current session
                    var user = _userService.GetUserByUsername(username);
                    
                    // Set current user information
                    CurrentUser.Initialize(
                        user.UserID, 
                        user.Username, 
                        user.DisplayName, 
                        user.RoleID, 
                        user.RoleName);
                    
                    // Log the login event
                    var auditService = ServiceLocator.GetService<IAuditService>();
                    auditService.LogEvent(
                        user.UserID,
                        "UserLogin",
                        "Users",
                        user.UserID.ToString(),
                        null,
                        $"User '{username}' logged in at {DateTime.Parse("2025-03-02 16:16:14").ToString("yyyy-MM-dd HH:mm:ss")}"
                    );
                    
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("Invalid username or password.\nPlease try again.", "Login Failed", 
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Clear();
                    txtPassword.Focus();
                    
                    // Log failed login attempt
                    var monitoringService = ServiceLocator.GetService<IMonitoringService>();
                    monitoringService.LogError(
                        "Security", 
                        $"Failed login attempt for user '{username}'", 
                        null, 
                        2);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during login: {ex.Message}", "Error", 
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                // Log the error
                var monitoringService = ServiceLocator.GetService<IMonitoringService>();
                monitoringService.LogError("Security", $"Login error: {ex.Message}", ex.StackTrace, 3);
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
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.SuspendLayout();
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(171, 76);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(200, 23);
            this.txtUsername.TabIndex = 0;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(171, 118);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(200, 23);
            this.txtPassword.TabIndex = 1;
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(171, 165);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(90, 30);
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(281, 165);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 30);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(95, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Username:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(95, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Password:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 76);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 64);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // LoginForm
            // 
            this.AcceptButton = this.btnLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(400, 230);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsername);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Admin Domain Login";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}