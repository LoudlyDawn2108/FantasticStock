using FantasticStock.Models.Sales;
using FantasticStock.ViewModels.Sales;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

        public CustomerManagementView()
        {
            InitializeComponent();

            try
            {
                // Initialize repository
                _viewmodel = new CustomerManagementViewModel();

                // Set up UI behavior
                SetupUIBehavior();

                // Initialize customer data
                InitializeData();

                // Initialize default state
                InitializeDefaultState();
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

            // Add event handlers for export/import if available
            if (btnExport != null)
                btnExport.Click += (s, e) => ExportCustomers();
            if (btnImport != null)
                btnImport.Click += (s, e) => ImportCustomers();

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
            // Make sure customer collection exists
            EnsureCustomerCollectionInitialized();

            // Add sample data if needed
            AddSampleCustomersIfNeeded();

            // Load data into UI
            LoadCustomerData();
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

        private void EnsureCustomerCollectionInitialized()
        {
            if (_viewmodel.Customers == null)
            {
                try
                {
                    // First try: Use reflection to set the property directly
                    var prop = _viewmodel.GetType().GetProperty("Customers");
                    if (prop != null && prop.CanWrite)
                    {
                        prop.SetValue(_viewmodel, new List<Customer>());
                        System.Diagnostics.Debug.WriteLine("Customer collection initialized via reflection");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Could not initialize Customers property: {ex.Message}");
                }

                try
                {
                    // Second try: Create a dummy customer to trigger initialization
                    var dummyCustomer = new Customer
                    {
                        CustomerName = "System Initialization",
                        Phone = "0000000000",
                        Email = "init@system.test",
                        Address = "System",
                        IsActive = false,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now
                    };

                    var result = _viewmodel.AddCustomer(dummyCustomer);
                    if (result != null)
                    {
                        // Success - now clean up by deactivating the dummy customer
                        _viewmodel.DeactivateCustomer(result.CustomerID);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Could not initialize via AddCustomer: {ex.Message}");

                    // If all initialization attempts fail, create local fallback
                    _filteredCustomers = new List<Customer>();
                }
            }
        }

        private void AddSampleCustomersIfNeeded()
        {
            try
            {
                // Only add sample data if no customers exist
                bool hasCustomers = _viewmodel.Customers != null &&
                    _viewmodel.Customers.Any(c => c.CustomerName != "System Initialization");

                if (!hasCustomers)
                {
                    var sampleCustomers = CreateSampleCustomers();
                    int addedCount = 0;
                    StringBuilder errors = new StringBuilder();

                    foreach (var customer in sampleCustomers)
                    {
                        try
                        {
                            // Important: Don't set CustomerID as it's likely auto-assigned
                            customer.CustomerID = 0;

                            var result = _viewmodel.AddCustomer(customer);
                            if (result != null)
                            {
                                addedCount++;
                            }
                            else
                            {
                                errors.AppendLine($"- Failed to add {customer.CustomerName}");
                            }
                        }
                        catch (Exception ex)
                        {
                            errors.AppendLine($"- Error adding {customer.CustomerName}: {ex.Message}");
                        }
                    }

                    // Report results
                    if (addedCount > 0)
                    {
                        MessageBox.Show($"Added {addedCount} sample customer(s) successfully.",
                            "Sample Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (errors.Length > 0)
                    {
                        // Try direct property setting as fallback
                        try
                        {
                            var prop = _viewmodel.GetType().GetProperty("Customers");
                            if (prop != null && prop.CanWrite)
                            {
                                // Assign IDs when setting directly
                                for (int i = 0; i < sampleCustomers.Count; i++)
                                {
                                    sampleCustomers[i].CustomerID = i + 1;
                                }

                                prop.SetValue(_viewmodel, sampleCustomers);
                                MessageBox.Show("Sample data initialized via direct assignment.",
                                    "Sample Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Failed to add sample customers.",
                                    "Sample Data Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                System.Diagnostics.Debug.WriteLine(errors.ToString());
                            }
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine($"Failed direct assignment: {ex.Message}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding sample data: {ex.Message}",
                    "Sample Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<Customer> CreateSampleCustomers()
        {
            return new List<Customer>
            {
                new Customer
                {
                    CustomerName = "Acme Corporation",
                    Phone = "(555) 123-4567",
                    Email = "info@acmecorp.com",
                    Address = "123 Main St, New York, NY 10001",
                    LoyaltyPoints = 250,
                    IsActive = true,
                    CreatedDate = DateTime.Now.AddMonths(-6),
                    ModifiedDate = DateTime.Now.AddDays(-15)
                },
                new Customer
                {
                    CustomerName = "TechSolutions Inc.",
                    Phone = "(555) 987-6543",
                    Email = "contact@techsolutions.com",
                    Address = "456 Innovation Ave, San Francisco, CA 94105",
                    LoyaltyPoints = 120,
                    IsActive = true,
                    CreatedDate = DateTime.Now.AddMonths(-3),
                    ModifiedDate = DateTime.Now.AddDays(-5)
                },
                new Customer
                {
                    CustomerName = "Global Enterprises",
                    Phone = "(555) 456-7890",
                    Email = "sales@globalent.com",
                    Address = "789 Business Blvd, Chicago, IL 60601",
                    LoyaltyPoints = 350,
                    IsActive = true,
                    CreatedDate = DateTime.Now.AddMonths(-8),
                    ModifiedDate = DateTime.Now.AddDays(-30)
                },
                new Customer
                {
                    CustomerName = "Local Shop",
                    Phone = "(555) 222-3333",
                    Email = "shop@local.com",
                    Address = "321 Small St, Portland, OR 97205",
                    LoyaltyPoints = 75,
                    IsActive = false,
                    CreatedDate = DateTime.Now.AddMonths(-12),
                    ModifiedDate = DateTime.Now.AddDays(-2)
                },
                new Customer
                {
                    CustomerName = "Big Retail Chain",
                    Phone = "(555) 777-8888",
                    Email = "orders@bigretail.com",
                    Address = "555 Commerce Way, Dallas, TX 75201",
                    LoyaltyPoints = 500,
                    IsActive = true,
                    CreatedDate = DateTime.Now.AddMonths(-9),
                    ModifiedDate = DateTime.Now.AddDays(-1)
                }
            };
        }

        private void LoadCustomerData()
        {
            try
            {
                // Filter to active customers initially
                if (_viewmodel.Customers != null)
                {
                    _filteredCustomers = _viewmodel.Customers.Where(c => c.IsActive).ToList();
                }
                else
                {
                    _filteredCustomers = new List<Customer>();
                }

                // Update grid
                RefreshGrid();

                // Configure search options
                SetupSearchOptions();
            }
            catch (Exception ex)
            {
                _filteredCustomers = new List<Customer>();
                dgvCustomers.DataSource = _filteredCustomers;
                MessageBox.Show($"Error loading customer data: {ex.Message}",
                    "Data Loading Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        #region Business Logic Methods
        private void SearchCustomers()
        {
            try
            {
                // Handle case where Customers collection might be null
                if (_viewmodel.Customers == null)
                {
                    _filteredCustomers = new List<Customer>();
                    RefreshGrid();
                    return;
                }

                string searchText = txtSearch.Text.Trim();
                bool showInactive = chkShowInactive.Checked;
                string searchBy = cboSearchBy.SelectedItem?.ToString() ?? "All Fields";

                // Apply filters based on search criteria
                if (string.IsNullOrEmpty(searchText) && !showInactive)
                {
                    // Show all active customers
                    _filteredCustomers = _viewmodel.Customers.Where(c => c.IsActive).ToList();
                }
                else if (string.IsNullOrEmpty(searchText) && showInactive)
                {
                    // Show all customers including inactive
                    _filteredCustomers = _viewmodel.Customers.ToList();
                }
                else
                {
                    // Search with multiple criteria
                    _filteredCustomers = _viewmodel.Customers.Where(c =>
                        (showInactive || c.IsActive) &&
                        (
                            searchBy == "All Fields" ? (
                                (c.CustomerName != null && c.CustomerName.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0) ||
                                (c.Phone != null && c.Phone.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0) ||
                                (c.Email != null && c.Email.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0) ||
                                (c.Address != null && c.Address.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
                            ) :
                            searchBy == "Customer Name" ? (c.CustomerName != null && c.CustomerName.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0) :
                            searchBy == "Phone" ? (c.Phone != null && c.Phone.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0) :
                            searchBy == "Email" ? (c.Email != null && c.Email.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0) :
                            searchBy == "Address" ? (c.Address != null && c.Address.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0) :
                            false
                        )
                    ).ToList();
                }

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

                // Handle new vs. existing customer
                if (_selectedCustomer.CustomerID == 0) // New customer
                {
                    var addedCustomer = _viewmodel.AddCustomer(_selectedCustomer);
                    if (addedCustomer == null)
                    {
                        MessageBox.Show("Failed to add the customer to the database.", "Add Customer Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    _selectedCustomer = addedCustomer; // Update with DB-assigned ID

                    // Add to filtered list if it should be visible
                    if (_selectedCustomer.IsActive || chkShowInactive.Checked)
                    {
                        _filteredCustomers.Add(_selectedCustomer);
                    }
                }
                else // Existing customer
                {
                    bool success = _viewmodel.UpdateCustomer(_selectedCustomer);
                    if (!success)
                    {
                        MessageBox.Show("Failed to update the customer in the database.", "Update Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
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
                    bool success = _viewmodel.DeactivateCustomer(_selectedCustomer.CustomerID);
                    if (!success)
                    {
                        MessageBox.Show("Failed to deactivate the customer.", "Deactivation Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Update local object
                    _selectedCustomer.IsActive = false;

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
                    _selectedCustomer = _viewmodel.GetCustomer(_selectedCustomer.CustomerID);
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

                // Reload data
                InitializeData();
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
            try
            {
                if (_filteredCustomers == null || _filteredCustomers.Count == 0)
                {
                    MessageBox.Show("There are no customers to export.", "Export",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (SaveFileDialog saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                    saveDialog.Title = "Export Customers to CSV";
                    saveDialog.DefaultExt = "csv";
                    saveDialog.FileName = $"Customers_Export_{DateTime.Now:yyyyMMdd}";

                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        StringBuilder csv = new StringBuilder();

                        // Add CSV header with fields from our Customer model
                        csv.AppendLine("Customer ID,Customer Name,Phone,Email,Address,Loyalty Points,Is Active,Created Date,Modified Date");

                        // Add data rows for filtered customers
                        foreach (var customer in _filteredCustomers)
                        {
                            csv.AppendLine(string.Format(
                                $"{customer.CustomerID},\"{EscapeCsvField(customer.CustomerName)}\"," +
                                $"\"{EscapeCsvField(customer.Phone)}\",\"{EscapeCsvField(customer.Email)}\",\"{EscapeCsvField(customer.Address)}\"," +
                                $"{customer.LoyaltyPoints}," +
                                $"{(customer.IsActive ? "Yes" : "No")},{customer.CreatedDate:yyyy-MM-dd},{customer.ModifiedDate:yyyy-MM-dd}"
                            ));
                        }

                        // Write the CSV to file
                        System.IO.File.WriteAllText(saveDialog.FileName, csv.ToString());

                        MessageBox.Show($"Successfully exported {_filteredCustomers.Count} customers to {saveDialog.FileName}",
                            "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting customers: {ex.Message}", "Export Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            try
            {
                using (OpenFileDialog openDialog = new OpenFileDialog())
                {
                    openDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                    openDialog.Title = "Import Customers from CSV";

                    if (openDialog.ShowDialog() == DialogResult.OK)
                    {
                        List<Customer> importedCustomers = new List<Customer>();
                        int importCount = 0;
                        int errorCount = 0;

                        // Read the CSV file
                        string[] lines = System.IO.File.ReadAllLines(openDialog.FileName);

                        if (lines.Length <= 1)
                        {
                            MessageBox.Show("The CSV file appears to be empty or contains only headers.",
                                "Import Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        // Skip header row
                        for (int i = 1; i < lines.Length; i++)
                        {
                            string line = lines[i];
                            if (string.IsNullOrWhiteSpace(line))
                                continue;

                            try
                            {
                                // Parse CSV line with support for quoted fields
                                string[] fields = ParseCsvLine(line);

                                if (fields.Length < 4) // Minimum required: Name, Phone, Email, Address
                                {
                                    System.Diagnostics.Debug.WriteLine($"Line {i + 1} does not have enough fields. Skipping.");
                                    errorCount++;
                                    continue;
                                }

                                // Create new customer - don't set ID as it's likely auto-assigned
                                Customer customer = new Customer
                                {
                                    CustomerName = fields[0].Trim(),
                                    Phone = fields.Length > 1 ? fields[1].Trim() : string.Empty,
                                    Email = fields.Length > 2 ? fields[2].Trim() : string.Empty,
                                    Address = fields.Length > 3 ? fields[3].Trim() : string.Empty,
                                    IsActive = fields.Length > 4 ? fields[4].Trim().Equals("yes", StringComparison.OrdinalIgnoreCase) : true,
                                    LoyaltyPoints = fields.Length > 5 && int.TryParse(fields[5], out int points) ? points : 0,
                                    CreatedDate = DateTime.Now,
                                    ModifiedDate = DateTime.Now
                                };

                                // Basic validation
                                if (string.IsNullOrWhiteSpace(customer.CustomerName))
                                {
                                    System.Diagnostics.Debug.WriteLine($"Line {i + 1}: Customer name is required. Skipping.");
                                    errorCount++;
                                    continue;
                                }

                                if (string.IsNullOrWhiteSpace(customer.Phone) && string.IsNullOrWhiteSpace(customer.Email))
                                {
                                    System.Diagnostics.Debug.WriteLine($"Line {i + 1}: Either phone or email is required. Skipping.");
                                    errorCount++;
                                    continue;
                                }

                                importedCustomers.Add(customer);
                                importCount++;
                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine($"Error processing line {i + 1}: {ex.Message}");
                                errorCount++;
                            }
                        }

                        // Add imported customers to repository
                        int addedCount = 0;
                        foreach (var customer in importedCustomers)
                        {
                            try
                            {
                                var result = _viewmodel.AddCustomer(customer);
                                if (result != null)
                                    addedCount++;
                                else
                                    errorCount++;
                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine($"Error adding customer {customer.CustomerName}: {ex.Message}");
                                errorCount++;
                            }
                        }

                        // Refresh the grid
                        SearchCustomers();

                        string message = $"Successfully imported {addedCount} customers.";
                        if (errorCount > 0)
                            message += $"\n{errorCount} records had errors and were skipped.";

                        MessageBox.Show(message, "Import Complete",
                            MessageBoxButtons.OK, errorCount > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error importing customers: {ex.Message}", "Import Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                // Get customer from repository
                var customer = _viewmodel.GetCustomer(customerId);
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

                // Save to repository
                bool success = _viewmodel.UpdateCustomer(customer);
                if (!success)
                {
                    MessageBox.Show("Failed to update loyalty points in the database.",
                        "Points Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
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
            // Call the existing ExportCustomers method
            ExportCustomers();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            // Call the existing ImportCustomers method
            ImportCustomers();
        }
    }
}
