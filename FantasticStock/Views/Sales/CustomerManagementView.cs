using FantasticStock.Models.Sales;
using FantasticStock.ViewModels.Sales;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FantasticStock.Views.Sales
{
    public partial class CustomerManagementView : UserControl
    {
        private CustomerManagementViewModel _viewmodel;
        private List<Customer> _filteredCustomers;
        private Customer _selectedCustomer;
        private bool _isEditing = false;

        // Update connection string to your FantasticStock database
    //    private string connectionString = @"Data Source=DESKTOP-DOEUG5N;Initial Catalog=FantasticStock;Integrated Security=True";

        private string connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=FantasticStock1;Integrated Security=True";

        private SqlConnection connection;

        public CustomerManagementView()
        {
            InitializeComponent();

            try
            {
                // Initialize repository
                _viewmodel = new CustomerManagementViewModel();

                // Initialize database connection
                connection = new SqlConnection(connectionString);

                // Set up UI behavior
                SetupUIBehavior();

                // Initialize customer data
                InitializeData();

                // Initialize default state
                InitializeDefaultState();

                // Add this line to initialize search options
                SetupSearchOptions();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing Customer Management: {ex.Message}",
                    "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Initialization Methods
        private void SetupUIBehavior()
        {
            // Add event handlers for export/import buttons
            btnExport.Click += (s, e) => ExportCustomers();
            btnImport.Click += (s, e) => ImportCustomers();

            // Setup DataGridView
            dgvCustomers.AutoGenerateColumns = false;

            // Configure DataGridView columns for data binding
            colCustomerId.DataPropertyName = "CustomerID";
            colCustomerName.DataPropertyName = "CustomerName";
            colPhone.DataPropertyName = "Phone";
            colEmail.DataPropertyName = "Email";
            colCity.DataPropertyName = "Address"; // Map Address to City column
            colLoyaltyPoints.DataPropertyName = "LoyaltyPoints";
            colIsActive.DataPropertyName = "IsActive";

            // Setup event handlers
            btnSearch.Click += (s, e) => SearchCustomers();
            btnClear.Click += (s, e) => ClearSearch();
            btnAdd.Click += (s, e) => CreateNewCustomer();
            btnSave.Click += (s, e) => SaveCustomer();
            btnCancel.Click += (s, e) => CancelEdit();
            btnEdit.Click += (s, e) => EditCustomer();
            btnDelete.Click += (s, e) => DeactivateCustomer();

            // Selection change events
            dgvCustomers.SelectionChanged += DgvCustomers_SelectionChanged;

            // Search text box event
            txtSearch.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    SearchCustomers();
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
            };

            // Validation event handlers
            txtEmail.Validating += TxtEmail_Validating;
            txtPhone.Validating += TxtPhone_Validating;

            // Date picker setup
            dtpBirthDate.Format = DateTimePickerFormat.Short;
            dtpBirthDate.Value = DateTime.Now.AddYears(-30);
        }

        private void InitializeData()
        {
            // Load customers directly from the database
            LoadCustomersFromDatabase();
        }

        private void LoadCustomersFromDatabase()
        {
            try
            {
                List<Customer> customers = new List<Customer>();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"
                        SELECT CustomerID, CustomerName, Phone, Email, Address, 
                               LoyaltyPoints, IsActive, CreatedDate, ModifiedDate
                        FROM Customer
                        WHERE IsActive = 1
                        ORDER BY CustomerName";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            customers.Add(new Customer
                            {
                                CustomerID = reader.GetInt32(reader.GetOrdinal("CustomerID")),
                                CustomerName = reader.GetString(reader.GetOrdinal("CustomerName")),
                                Phone = reader.IsDBNull(reader.GetOrdinal("Phone")) ? null : reader.GetString(reader.GetOrdinal("Phone")),
                                Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString(reader.GetOrdinal("Email")),
                                Address = reader.IsDBNull(reader.GetOrdinal("Address")) ? null : reader.GetString(reader.GetOrdinal("Address")),
                                LoyaltyPoints = reader.IsDBNull(reader.GetOrdinal("LoyaltyPoints")) ? 0 : reader.GetInt32(reader.GetOrdinal("LoyaltyPoints")),
                                IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                                CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                                ModifiedDate = reader.GetDateTime(reader.GetOrdinal("ModifiedDate"))
                            });
                        }
                    }
                }

                // Set up the viewmodel with this data
                if (_viewmodel.Customers == null)
                {
                    // Try to set it using reflection
                    var prop = _viewmodel.GetType().GetProperty("Customers");
                    if (prop != null && prop.CanWrite)
                    {
                        prop.SetValue(_viewmodel, customers);
                    }
                    else
                    {
                        // If we can't update the viewmodel, at least set our filtered list
                        _filteredCustomers = customers;
                    }
                }

                // Set the filtered customers list
                _filteredCustomers = customers.Where(c => c.IsActive).ToList();
                RefreshGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customers from database: {ex.Message}",
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Create an empty list as fallback
                _filteredCustomers = new List<Customer>();
                RefreshGrid();
            }
        }

        private void InitializeDefaultState()
        {
            // Set default button states
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;

            // Set details panel disabled
            SetDetailsEditMode(false);
            ClearCustomerDetails();
        }

        private void SetupSearchOptions()
        {
            cboSearchBy.Items.Clear();
            cboSearchBy.Items.AddRange(new string[] {
                "All Fields",
                "Customer Name",
                "Phone",
                "Email",
                "Address"
            });
            cboSearchBy.SelectedIndex = 0;
        }
        #endregion

        #region UI State Management
        private void SetDetailsEditMode(bool editMode)
        {
            _isEditing = editMode;

            // Enable/disable input fields
            txtCustomerName.ReadOnly = !editMode;
            txtContactName.ReadOnly = !editMode;
            txtPhone.ReadOnly = !editMode;
            txtEmail.ReadOnly = !editMode;
            txtAddress.ReadOnly = !editMode;
            txtCity.ReadOnly = !editMode;
            txtState.ReadOnly = !editMode;
            txtZip.ReadOnly = !editMode;
            txtCountry.ReadOnly = !editMode;
            txtNotes.ReadOnly = !editMode;

            // Enable/disable controls
            dtpBirthDate.Enabled = editMode;
            numLoyaltyPoints.Enabled = editMode;
            chkOptInMarketing.Enabled = editMode;
            chkVIP.Enabled = editMode;

            // Adjust button states
            btnSave.Enabled = editMode;
            btnCancel.Enabled = editMode;
            btnEdit.Enabled = !editMode && _selectedCustomer != null;
            btnDelete.Enabled = !editMode && _selectedCustomer != null;
            btnAdd.Enabled = !editMode;

            // Disable grid during edit
            dgvCustomers.Enabled = !editMode;
            grpSearch.Enabled = !editMode;
        }

        private void ClearCustomerDetails()
        {
            // Clear all UI fields
            txtCustomerId.Text = string.Empty;
            txtCustomerName.Text = string.Empty;
            txtContactName.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtState.Text = string.Empty;
            txtZip.Text = string.Empty;
            txtCountry.Text = string.Empty;
            numLoyaltyPoints.Value = 0;
            chkOptInMarketing.Checked = false;
            chkVIP.Checked = false;
            txtNotes.Text = string.Empty;
            dtpBirthDate.Value = DateTime.Now.AddYears(-30);
            txtCreatedDate.Text = string.Empty;
            txtModifiedDate.Text = string.Empty;
        }

        private void DisplayCustomerDetails(Customer customer)
        {
            if (customer == null)
            {
                ClearCustomerDetails();
                return;
            }

            // Map data to UI with null checks
            txtCustomerId.Text = customer.CustomerID.ToString();
            txtCustomerName.Text = customer.CustomerName ?? string.Empty;
            txtPhone.Text = customer.Phone ?? string.Empty;
            txtEmail.Text = customer.Email ?? string.Empty;
            txtAddress.Text = customer.Address ?? string.Empty;

            // Clear fields we don't have data for
            txtContactName.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtState.Text = string.Empty;
            txtZip.Text = string.Empty;
            txtCountry.Text = string.Empty;
            txtNotes.Text = string.Empty;

            // Set numeric value safely
            try
            {
                numLoyaltyPoints.Value = Math.Min(numLoyaltyPoints.Maximum,
                    Math.Max(numLoyaltyPoints.Minimum, customer.LoyaltyPoints));
            }
            catch
            {
                numLoyaltyPoints.Value = 0;
            }

            // Set status checkboxes
            chkVIP.Checked = customer.LoyaltyPoints >= 200;
            chkOptInMarketing.Checked = customer.LoyaltyPoints > 0;

            // Format dates safely
            txtCreatedDate.Text = customer.CreatedDate.ToString("yyyy-MM-dd HH:mm");
            txtModifiedDate.Text = customer.ModifiedDate.ToString("yyyy-MM-dd HH:mm");
        }

        private void RefreshGrid()
        {
            try
            {
                // Temporarily disable data binding to avoid flickering
                dgvCustomers.SuspendLayout();

                // Create defensive copy to avoid reference issues
                var dataSource = _filteredCustomers?.ToList() ?? new List<Customer>();
                dgvCustomers.DataSource = null;
                dgvCustomers.DataSource = dataSource;

                // Restore layout updates
                dgvCustomers.ResumeLayout();

                // Update results count
                lblResults.Text = $"{dataSource.Count} customer(s) found";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error refreshing grid: {ex.Message}");

                // Emergency recovery - ensure grid has at least an empty list
                try
                {
                    dgvCustomers.DataSource = new List<Customer>();
                    lblResults.Text = "0 customers found";
                }
                catch { /* Last resort failed - nothing more we can do */ }
            }
        }
        #endregion

        #region Event Handlers
        private void DgvCustomers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCustomers.SelectedRows.Count > 0 && !_isEditing)
            {
                try
                {
                    _selectedCustomer = dgvCustomers.SelectedRows[0].DataBoundItem as Customer;
                    if (_selectedCustomer != null)
                    {
                        DisplayCustomerDetails(_selectedCustomer);
                        btnEdit.Enabled = true;
                        btnDelete.Enabled = true;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error selecting customer: {ex.Message}");
                    _selectedCustomer = null;
                    ClearCustomerDetails();
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;
                }
            }
            else if (!_isEditing)
            {
                _selectedCustomer = null;
                ClearCustomerDetails();
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
            }
        }

        private void TxtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                // Simple email validation
                if (!txtEmail.Text.Contains("@") || !txtEmail.Text.Contains("."))
                {
                    errorProvider.SetError(txtEmail, "Please enter a valid email address");
                    e.Cancel = true;
                }
                else
                {
                    errorProvider.SetError(txtEmail, "");
                }
            }
            else
            {
                errorProvider.SetError(txtEmail, "");
            }
        }

        private void TxtPhone_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                // Simple phone validation - must have at least 10 digits
                string digitsOnly = new string(txtPhone.Text.Where(c => char.IsDigit(c)).ToArray());
                if (digitsOnly.Length < 10)
                {
                    errorProvider.SetError(txtPhone, "Phone number must have at least 10 digits");
                    e.Cancel = true;
                }
                else
                {
                    errorProvider.SetError(txtPhone, "");
                }
            }
            else
            {
                errorProvider.SetError(txtPhone, "");
            }
        }
        #endregion

        #region Database Operations
        private Customer GetCustomerFromDatabase(int customerId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"
                        SELECT CustomerID, CustomerName, Phone, Email, Address, 
                               LoyaltyPoints, IsActive, CreatedDate, ModifiedDate
                        FROM Customer
                        WHERE CustomerID = @CustomerID";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@CustomerID", customerId);
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Customer
                            {
                                CustomerID = reader.GetInt32(reader.GetOrdinal("CustomerID")),
                                CustomerName = reader.GetString(reader.GetOrdinal("CustomerName")),
                                Phone = reader.IsDBNull(reader.GetOrdinal("Phone")) ? null : reader.GetString(reader.GetOrdinal("Phone")),
                                Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString(reader.GetOrdinal("Email")),
                                Address = reader.IsDBNull(reader.GetOrdinal("Address")) ? null : reader.GetString(reader.GetOrdinal("Address")),
                                LoyaltyPoints = reader.IsDBNull(reader.GetOrdinal("LoyaltyPoints")) ? 0 : reader.GetInt32(reader.GetOrdinal("LoyaltyPoints")),
                                IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                                CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                                ModifiedDate = reader.GetDateTime(reader.GetOrdinal("ModifiedDate"))
                            };
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting customer from database: {ex.Message}");
                return null;
            }
        }

        private bool AddCustomerToDatabase(Customer customer)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"
                        INSERT INTO Customer (CustomerName, Phone, Email, Address, LoyaltyPoints, IsActive, CreatedDate, ModifiedDate)
                        OUTPUT INSERTED.CustomerID
                        VALUES (@CustomerName, @Phone, @Email, @Address, @LoyaltyPoints, @IsActive, @CreatedDate, @ModifiedDate)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@CustomerName", customer.CustomerName);
                    cmd.Parameters.AddWithValue("@Phone", string.IsNullOrEmpty(customer.Phone) ? (object)DBNull.Value : customer.Phone);
                    cmd.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(customer.Email) ? (object)DBNull.Value : customer.Email);
                    cmd.Parameters.AddWithValue("@Address", string.IsNullOrEmpty(customer.Address) ? (object)DBNull.Value : customer.Address);
                    cmd.Parameters.AddWithValue("@LoyaltyPoints", customer.LoyaltyPoints);
                    cmd.Parameters.AddWithValue("@IsActive", customer.IsActive);
                    cmd.Parameters.AddWithValue("@CreatedDate", customer.CreatedDate);
                    cmd.Parameters.AddWithValue("@ModifiedDate", customer.ModifiedDate);

                    conn.Open();
                    int newId = (int)cmd.ExecuteScalar();
                    customer.CustomerID = newId;

                    return newId > 0;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error adding customer to database: {ex.Message}");
                return false;
            }
        }

        private bool UpdateCustomerInDatabase(Customer customer)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"
                        UPDATE Customer
                        SET CustomerName = @CustomerName,
                            Phone = @Phone,
                            Email = @Email,
                            Address = @Address,
                            LoyaltyPoints = @LoyaltyPoints,
                            ModifiedDate = @ModifiedDate
                        WHERE CustomerID = @CustomerID";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@CustomerID", customer.CustomerID);
                    cmd.Parameters.AddWithValue("@CustomerName", customer.CustomerName);
                    cmd.Parameters.AddWithValue("@Phone", string.IsNullOrEmpty(customer.Phone) ? (object)DBNull.Value : customer.Phone);
                    cmd.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(customer.Email) ? (object)DBNull.Value : customer.Email);
                    cmd.Parameters.AddWithValue("@Address", string.IsNullOrEmpty(customer.Address) ? (object)DBNull.Value : customer.Address);
                    cmd.Parameters.AddWithValue("@LoyaltyPoints", customer.LoyaltyPoints);
                    cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error updating customer in database: {ex.Message}");
                return false;
            }
        }

        private bool DeactivateCustomerInDatabase(int customerId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"
                        UPDATE Customer
                        SET IsActive = 0,
                            ModifiedDate = @ModifiedDate
                        WHERE CustomerID = @CustomerID";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@CustomerID", customerId);
                    cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error deactivating customer in database: {ex.Message}");
                return false;
            }
        }

        private List<Customer> SearchCustomersInDatabase(string searchText, string searchBy, bool showInactive)
        {
            try
            {
                List<Customer> results = new List<Customer>();
                string whereClause = string.Empty;

                // Build the WHERE clause based on search criteria
                if (!string.IsNullOrEmpty(searchText))
                {
                    switch (searchBy)
                    {
                        case "All Fields":
                            whereClause = "WHERE (CustomerName LIKE @SearchText OR Phone LIKE @SearchText OR Email LIKE @SearchText OR Address LIKE @SearchText)";
                            break;
                        case "Customer Name":
                            whereClause = "WHERE CustomerName LIKE @SearchText";
                            break;
                        case "Phone":
                            whereClause = "WHERE Phone LIKE @SearchText";
                            break;
                        case "Email":
                            whereClause = "WHERE Email LIKE @SearchText";
                            break;
                        case "Address":
                            whereClause = "WHERE Address LIKE @SearchText";
                            break;
                        default:
                            whereClause = "WHERE CustomerName LIKE @SearchText";
                            break;
                    }

                    if (!showInactive)
                    {
                        whereClause += " AND IsActive = 1";
                    }
                }
                else if (!showInactive)
                {
                    whereClause = "WHERE IsActive = 1";
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = $@"
                        SELECT CustomerID, CustomerName, Phone, Email, Address, 
                               LoyaltyPoints, IsActive, CreatedDate, ModifiedDate
                        FROM Customer
                        {whereClause}
                        ORDER BY CustomerName";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    if (!string.IsNullOrEmpty(searchText))
                    {
                        cmd.Parameters.AddWithValue("@SearchText", $"%{searchText}%");
                    }

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results.Add(new Customer
                            {
                                CustomerID = reader.GetInt32(reader.GetOrdinal("CustomerID")),
                                CustomerName = reader.GetString(reader.GetOrdinal("CustomerName")),
                                Phone = reader.IsDBNull(reader.GetOrdinal("Phone")) ? null : reader.GetString(reader.GetOrdinal("Phone")),
                                Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString(reader.GetOrdinal("Email")),
                                Address = reader.IsDBNull(reader.GetOrdinal("Address")) ? null : reader.GetString(reader.GetOrdinal("Address")),
                                LoyaltyPoints = reader.IsDBNull(reader.GetOrdinal("LoyaltyPoints")) ? 0 : reader.GetInt32(reader.GetOrdinal("LoyaltyPoints")),
                                IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                                CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                                ModifiedDate = reader.GetDateTime(reader.GetOrdinal("ModifiedDate"))
                            });
                        }
                    }
                }

                return results;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error searching customers in database: {ex.Message}");
                return new List<Customer>();
            }
        }
        #endregion

        #region Business Logic Methods
        private void SearchCustomers()
        {
            try
            {
                string searchText = txtSearch.Text.Trim();
                bool showInactive = chkShowInactive.Checked;
                string searchBy = cboSearchBy.SelectedItem?.ToString() ?? "All Fields";

                // Search customers in database
                _filteredCustomers = SearchCustomersInDatabase(searchText, searchBy, showInactive);

                RefreshGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching customers: {ex.Message}", "Search Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearSearch()
        {
            txtSearch.Text = string.Empty;
            chkShowInactive.Checked = false;
            cboSearchBy.SelectedIndex = 0;
            SearchCustomers();
        }

        private void CreateNewCustomer()
        {
            // Create a new customer instance
            _selectedCustomer = new Customer
            {
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                IsActive = true,
                LoyaltyPoints = 0
            };

            ClearCustomerDetails();
            txtCreatedDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            txtModifiedDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

            SetDetailsEditMode(true);
        }

        private void EditCustomer()
        {
            if (_selectedCustomer != null)
            {
                SetDetailsEditMode(true);
            }
        }

        private void SaveCustomer()
        {
            try
            {
                // Validate all child controls
                if (!ValidateChildren())
                    return;

                // Validate customer-specific data
                if (!ValidateCustomerData())
                    return;

                // Update customer from form fields
                UpdateCustomerFromFields(_selectedCustomer);

                bool success = false;

                // Handle new vs. existing customer
                if (_selectedCustomer.CustomerID == 0) // New customer
                {
                    success = AddCustomerToDatabase(_selectedCustomer);
                    if (!success)
                    {
                        MessageBox.Show("Failed to add the customer to the database.", "Add Customer Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Add to filtered list if it should be visible
                    if (_selectedCustomer.IsActive || chkShowInactive.Checked)
                    {
                        _filteredCustomers.Add(_selectedCustomer);
                    }
                }
                else // Existing customer
                {
                    success = UpdateCustomerInDatabase(_selectedCustomer);
                    if (!success)
                    {
                        MessageBox.Show("Failed to update the customer in the database.", "Update Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Update the filtered list with the updated customer
                    var existingCustomer = _filteredCustomers.FirstOrDefault(c => c.CustomerID == _selectedCustomer.CustomerID);
                    if (existingCustomer != null)
                    {
                        existingCustomer.CustomerName = _selectedCustomer.CustomerName;
                        existingCustomer.Phone = _selectedCustomer.Phone;
                        existingCustomer.Email = _selectedCustomer.Email;
                        existingCustomer.Address = _selectedCustomer.Address;
                        existingCustomer.LoyaltyPoints = _selectedCustomer.LoyaltyPoints;
                        existingCustomer.IsActive = _selectedCustomer.IsActive;
                        existingCustomer.ModifiedDate = _selectedCustomer.ModifiedDate;
                    }
                }

                // Exit edit mode
                SetDetailsEditMode(false);

                // Refresh grid to show changes
                RefreshGrid();

                // Select the customer in the grid
                SelectCustomerInGrid(_selectedCustomer.CustomerID);

                MessageBox.Show("Customer saved successfully.", "Save Successful",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving customer: {ex.Message}", "Save Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateCustomerFromFields(Customer customer)
        {
            customer.CustomerName = txtCustomerName.Text.Trim();
            customer.Phone = txtPhone.Text.Trim();
            customer.Email = txtEmail.Text.Trim();
            customer.Address = txtAddress.Text.Trim();
            customer.LoyaltyPoints = (int)numLoyaltyPoints.Value;
            customer.ModifiedDate = DateTime.Now;

            // Ensure VIP customers have sufficient points
            if (chkVIP.Checked)
            {
                customer.LoyaltyPoints = Math.Max(customer.LoyaltyPoints, 200);
            }
        }

        private void DeactivateCustomer()
        {
            if (_selectedCustomer == null)
                return;

            try
            {
                if (MessageBox.Show("Are you sure you want to deactivate this customer?",
                                "Confirm Deactivation",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    bool success = DeactivateCustomerInDatabase(_selectedCustomer.CustomerID);
                    if (!success)
                    {
                        MessageBox.Show("Failed to deactivate the customer.", "Deactivation Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Update local data
                    _selectedCustomer.IsActive = false;
                    _selectedCustomer.ModifiedDate = DateTime.Now;

                    // Remove from filtered list if not showing inactive
                    if (!chkShowInactive.Checked)
                    {
                        _filteredCustomers.Remove(_selectedCustomer);
                    }

                    RefreshGrid();

                    // Clear selection
                    _selectedCustomer = null;
                    ClearCustomerDetails();
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;

                    MessageBox.Show("Customer has been deactivated successfully.", "Deactivation Complete",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deactivating customer: {ex.Message}", "Deactivation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CancelEdit()
        {
            try
            {
                if (_selectedCustomer?.CustomerID == 0) // New unsaved customer
                {
                    _selectedCustomer = null;
                    ClearCustomerDetails();
                }
                else if (_selectedCustomer != null) // Existing customer - reload
                {
                    // Get the customer from database
                    Customer refreshedCustomer = GetCustomerFromDatabase(_selectedCustomer.CustomerID);

                    if (refreshedCustomer != null)
                    {
                        _selectedCustomer = refreshedCustomer;
                    }

                    DisplayCustomerDetails(_selectedCustomer);
                }

                SetDetailsEditMode(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error cancelling edit: {ex.Message}", "Cancel Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateCustomerData()
        {
            // Customer name is required
            if (string.IsNullOrWhiteSpace(txtCustomerName.Text))
            {
                MessageBox.Show("Customer name is required.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCustomerName.Focus();
                return false;
            }

            // Either phone or email is required
            if (string.IsNullOrWhiteSpace(txtPhone.Text) && string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("At least one contact method (phone or email) must be provided.",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhone.Focus();
                return false;
            }

            // Email format validation
            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                if (!txtEmail.Text.Contains("@") || !txtEmail.Text.Contains("."))
                {
                    MessageBox.Show("Please enter a valid email address.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                    return false;
                }
            }

            // Phone number validation
            if (!string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                string digitsOnly = new string(txtPhone.Text.Where(c => char.IsDigit(c)).ToArray());
                if (digitsOnly.Length < 10)
                {
                    MessageBox.Show("Phone number must have at least 10 digits.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPhone.Focus();
                    return false;
                }
            }

            return true;
        }
        #endregion

        #region Helper Methods
        private void SelectCustomerInGrid(int customerId)
        {
            try
            {
                foreach (DataGridViewRow row in dgvCustomers.Rows)
                {
                    var customer = row.DataBoundItem as Customer;
                    if (customer != null && customer.CustomerID == customerId)
                    {
                        dgvCustomers.ClearSelection();
                        row.Selected = true;
                        dgvCustomers.FirstDisplayedScrollingRowIndex = row.Index;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error selecting customer in grid: {ex.Message}");
            }
        }

        public void ResetViewModel()
        {
            try
            {
                // Create a fresh instance of view model
                _viewmodel = new CustomerManagementViewModel();

                // Reload data from database
                LoadCustomersFromDatabase();

                // Reset the UI
                InitializeDefaultState();

                MessageBox.Show("Customer data has been reset successfully.",
                    "Reset Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to reset view model: {ex.Message}",
                    "Reset Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ExportCustomers()
        {
            
        }

        private string EscapeCsvField(string field)
        {
            if (string.IsNullOrEmpty(field))
                return string.Empty;

            // Replace double quotes with two double quotes to escape them in CSV
            return field.Replace("\"", "\"\"");
        }

        public void ImportCustomers()
        {
            
        }

        private string[] ParseCsvLine(string line)
        {
            List<string> result = new List<string>();
            bool inQuotes = false;
            int startIndex = 0;

            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == '"')
                {
                    inQuotes = !inQuotes;
                }
                else if (line[i] == ',' && !inQuotes)
                {
                    result.Add(line.Substring(startIndex, i - startIndex).Trim().Trim('"'));
                    startIndex = i + 1;
                }
            }

            // Add the last field
            result.Add(line.Substring(startIndex).Trim().Trim('"'));
            return result.ToArray();
        }

        /// <summary>
        /// Adjust loyalty points for a customer
        /// </summary>
        public void AdjustLoyaltyPoints(int customerId, int points)
        {
            try
            {
                // Get customer from database
                var customer = GetCustomerFromDatabase(customerId);
                if (customer == null)
                {
                    MessageBox.Show($"Customer with ID {customerId} not found.",
                        "Customer Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Make sure points don't go negative
                if (customer.LoyaltyPoints + points < 0)
                {
                    points = -customer.LoyaltyPoints;
                }

                // Update points
                customer.LoyaltyPoints += points;
                customer.ModifiedDate = DateTime.Now;

                // Update in database
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"
                        UPDATE Customer
                        SET LoyaltyPoints = @LoyaltyPoints,
                            ModifiedDate = @ModifiedDate
                        WHERE CustomerID = @CustomerID";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@CustomerID", customer.CustomerID);
                    cmd.Parameters.AddWithValue("@LoyaltyPoints", customer.LoyaltyPoints);
                    cmd.Parameters.AddWithValue("@ModifiedDate", customer.ModifiedDate);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected <= 0)
                    {
                        MessageBox.Show("Failed to update loyalty points in the database.",
                            "Points Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // Update UI if this is the currently selected customer
                if (_selectedCustomer != null && _selectedCustomer.CustomerID == customerId)
                {
                    _selectedCustomer = customer;
                    numLoyaltyPoints.Value = Math.Min(numLoyaltyPoints.Maximum,
                        Math.Max(numLoyaltyPoints.Minimum, customer.LoyaltyPoints));
                    txtModifiedDate.Text = customer.ModifiedDate.ToString("yyyy-MM-dd HH:mm");

                    // Update checkboxes to reflect loyalty status
                    chkVIP.Checked = customer.LoyaltyPoints >= 200;
                    chkOptInMarketing.Checked = customer.LoyaltyPoints > 0;
                }

                // Update grid if the customer is in the current view
                var gridCustomer = _filteredCustomers.FirstOrDefault(c => c.CustomerID == customerId);
                if (gridCustomer != null)
                {
                    gridCustomer.LoyaltyPoints = customer.LoyaltyPoints;
                    gridCustomer.ModifiedDate = customer.ModifiedDate;
                    RefreshGrid();
                }

                MessageBox.Show($"Loyalty points {(points >= 0 ? "added" : "deducted")} successfully. " +
                    $"New balance: {customer.LoyaltyPoints} points.",
                    "Points Adjusted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adjusting loyalty points: {ex.Message}",
                    "Points Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        private void btnExport_Click(object sender, EventArgs e)
        {
            
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            
        }
    }
}
