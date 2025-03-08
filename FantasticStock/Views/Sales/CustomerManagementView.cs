using FantasticStock.Data;
using FantasticStock.Models.Sales;
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
        private FakeDataRepository _repository;
        private List<Customer> _filteredCustomers;
        private Customer _selectedCustomer;
        private bool _isEditing = false;

        public CustomerManagementView()
        {
            InitializeComponent();

            // Initialize repository
            _repository = FakeDataRepository.Instance;

            // Set up UI behavior
            SetupUIBehavior();

            // Load data
            LoadData();

            // Initialize default state
            InitializeDefaultState();
        }

        #region Setup Methods
        private void SetupUIBehavior()
        {
            // Setup DataGridView
            dgvCustomers.AutoGenerateColumns = false;

            // Configure DataGridView columns for data binding
            colCustomerId.DataPropertyName = "CustomerID";
            colCustomerName.DataPropertyName = "CustomerName";
            colPhone.DataPropertyName = "Phone";
            colEmail.DataPropertyName = "Email";

            // Map Address to City column
            colCity.DataPropertyName = "Address";

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
            dtpBirthDate.Value = DateTime.Now.AddYears(-30); // Default to 30 years ago

            // Tab index setup for better navigation
            SetupTabOrder();
        }

        private void SetupTabOrder()
        {
            // Tab order already correctly set in the code
        }

        private void LoadData()
        {
            try
            {
                // Initial load of all customers
                _filteredCustomers = _repository.Customers.Where(c => c.IsActive).ToList();
                dgvCustomers.DataSource = _filteredCustomers;

                // Setup search dropdown options
                cboSearchBy.Items.Clear();
                cboSearchBy.Items.AddRange(new string[] {
                    "All Fields",
                    "Customer Name",
                    "Phone",
                    "Email",
                    "Address"
                });
                cboSearchBy.SelectedIndex = 0;

                lblResults.Text = $"{_filteredCustomers.Count} customer(s) found";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customer data: {ex.Message}", "Data Loading Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void SetDetailsEditMode(bool editMode)
        {
            _isEditing = editMode;

            // Enable/disable input fields based on edit mode
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

            // Show/hide appropriate buttons
            btnSave.Enabled = editMode;
            btnCancel.Enabled = editMode;
            btnEdit.Enabled = !editMode && _selectedCustomer != null;
            btnDelete.Enabled = !editMode && _selectedCustomer != null;
            btnAdd.Enabled = !editMode;

            // Disable grid selection during edit
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

            // Map the model fields to UI elements with null checks for safety
            txtCustomerId.Text = customer.CustomerID.ToString();
            txtCustomerName.Text = customer.CustomerName ?? string.Empty;

            // Using empty strings for fields not in the simpler Customer model
            txtContactName.Text = string.Empty;
            txtPhone.Text = customer.Phone ?? string.Empty;
            txtEmail.Text = customer.Email ?? string.Empty;
            txtAddress.Text = customer.Address ?? string.Empty;
            txtCity.Text = string.Empty;
            txtState.Text = string.Empty;
            txtZip.Text = string.Empty;
            txtCountry.Text = string.Empty;

            numLoyaltyPoints.Value = customer.LoyaltyPoints;

            // Set checkboxes based on loyalty points since CustomerTypeID may not be available
            chkVIP.Checked = customer.LoyaltyPoints >= 200;
            chkOptInMarketing.Checked = customer.LoyaltyPoints > 0;

            txtNotes.Text = string.Empty; // Notes field not available in simple model
            txtCreatedDate.Text = customer.CreatedDate.ToString("yyyy-MM-dd HH:mm");
            txtModifiedDate.Text = customer.ModifiedDate.ToString("yyyy-MM-dd HH:mm");
        }
        #endregion

        #region Event Handlers
        private void DgvCustomers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCustomers.SelectedRows.Count > 0 && !_isEditing)
            {
                _selectedCustomer = dgvCustomers.SelectedRows[0].DataBoundItem as Customer;
                DisplayCustomerDetails(_selectedCustomer);
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
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
                string searchText = txtSearch.Text.Trim();
                bool showInactive = chkShowInactive.Checked;
                string searchBy = cboSearchBy.SelectedItem?.ToString() ?? "All Fields";

                if (string.IsNullOrEmpty(searchText) && !showInactive)
                {
                    // Show all active customers
                    _filteredCustomers = _repository.Customers.Where(c => c.IsActive).ToList();
                }
                else if (string.IsNullOrEmpty(searchText) && showInactive)
                {
                    // Show all customers including inactive
                    _filteredCustomers = _repository.Customers.ToList();
                }
                else
                {
                    // Search by text and status
                    _filteredCustomers = _repository.Customers.Where(c =>
                        (showInactive || c.IsActive) &&
                        (
                            searchBy == "All Fields" ? (
                                (c.CustomerName != null && c.CustomerName.Contains(searchText)) ||
                                (c.Phone != null && c.Phone.Contains(searchText)) ||
                                (c.Email != null && c.Email.Contains(searchText)) ||
                                (c.Address != null && c.Address.Contains(searchText))
                            ) :
                            searchBy == "Customer Name" ? (c.CustomerName != null && c.CustomerName.Contains(searchText)) :
                            searchBy == "Phone" ? (c.Phone != null && c.Phone.Contains(searchText)) :
                            searchBy == "Email" ? (c.Email != null && c.Email.Contains(searchText)) :
                            searchBy == "Address" ? (c.Address != null && c.Address.Contains(searchText)) :
                            false
                        )
                    ).ToList();
                }

                // Update DataGridView
                RefreshGrid();

                // Update status
                lblResults.Text = $"{_filteredCustomers.Count} customer(s) found";
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
            // Clear details and enter edit mode
            _selectedCustomer = new Customer
            {
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                IsActive = true,
                LoyaltyPoints = 0
                // CustomerTypeID property might not exist in the simple model
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
                if (!ValidateChildren())
                    return;

                if (!ValidateCustomerData())
                    return;

                // Create or update customer object
                if (_selectedCustomer.CustomerID == 0) // New customer
                {
                    UpdateCustomerFromFields(_selectedCustomer);

                    // Add to repository
                    var addedCustomer = _repository.AddCustomer(_selectedCustomer);
                    _selectedCustomer = addedCustomer; // Update to get the assigned ID

                    // Add to grid if we're showing the right filter
                    if (_selectedCustomer.IsActive || chkShowInactive.Checked)
                    {
                        _filteredCustomers.Add(_selectedCustomer);
                    }
                }
                else // Existing customer
                {
                    UpdateCustomerFromFields(_selectedCustomer);

                    // Update in repository
                    bool success = _repository.UpdateCustomer(_selectedCustomer);
                    if (!success)
                    {
                        MessageBox.Show("Failed to update the customer in the database.", "Update Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // Exit edit mode
                SetDetailsEditMode(false);

                // Refresh grid to show changes and sort
                RefreshGrid();

                // Select the current customer in the grid
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
            // Map only the fields that exist in the simple Customer model
            customer.CustomerName = txtCustomerName.Text.Trim();
            customer.Phone = txtPhone.Text.Trim();
            customer.Email = txtEmail.Text.Trim();
            customer.Address = txtAddress.Text.Trim();
            customer.LoyaltyPoints = (int)numLoyaltyPoints.Value;
            customer.ModifiedDate = DateTime.Now;

            // Handle loyalty program and VIP status via checkbox states
            // Since we can't access CustomerTypeID directly, we'll use the loyalty points
            // to indicate marketing preferences and VIP status
            if (chkVIP.Checked)
            {
                // Ensure VIP customers have at least 200 loyalty points 
                customer.LoyaltyPoints = Math.Max(customer.LoyaltyPoints, 200);
            }
            else if (!chkOptInMarketing.Checked && customer.LoyaltyPoints > 0)
            {
                // If they opted out of marketing but had loyalty points,
                // keep their points but mark them as not interested in marketing
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
                    // Deactivate in repository
                    bool success = _repository.DeactivateCustomer(_selectedCustomer.CustomerID);

                    if (!success)
                    {
                        MessageBox.Show("Failed to deactivate the customer.", "Deactivation Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Remove from filtered list if it doesn't show inactive
                    if (!chkShowInactive.Checked)
                    {
                        _filteredCustomers.Remove(_selectedCustomer);
                        RefreshGrid();
                    }
                    else
                    {
                        // Just update the customer status in the grid
                        _selectedCustomer.IsActive = false;
                        RefreshGrid();
                    }

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
                if (_selectedCustomer?.CustomerID == 0) // New customer
                {
                    _selectedCustomer = null;
                    ClearCustomerDetails();
                }
                else if (_selectedCustomer != null) // Existing customer
                {
                    // Reload customer details from repository to discard changes
                    _selectedCustomer = _repository.GetCustomer(_selectedCustomer.CustomerID);
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
            // Basic validation
            if (string.IsNullOrWhiteSpace(txtCustomerName.Text))
            {
                MessageBox.Show("Customer name is required.", "Validation Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCustomerName.Focus();
                return false;
            }

            // Check if phone or email is provided
            if (string.IsNullOrWhiteSpace(txtPhone.Text) && string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("At least one contact method (phone or email) must be provided.", "Validation Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhone.Focus();
                return false;
            }

            // Validate email format if provided
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

            // Validate phone format if provided
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
        private void RefreshGrid()
        {
            // Temporarily disable data binding to avoid flickering
            dgvCustomers.SuspendLayout();

            // Update DataGridView
            dgvCustomers.DataSource = null;
            dgvCustomers.DataSource = _filteredCustomers;

            // Restore layout updates
            dgvCustomers.ResumeLayout();

            // Update the results count
            lblResults.Text = $"{_filteredCustomers.Count} customer(s) found";
        }

        private void SelectCustomerInGrid(int customerId)
        {
            // Find and select the customer in the grid
            foreach (DataGridViewRow row in dgvCustomers.Rows)
            {
                var customer = row.DataBoundItem as Customer;
                if (customer != null && customer.CustomerID == customerId)
                {
                    row.Selected = true;
                    dgvCustomers.FirstDisplayedScrollingRowIndex = row.Index;
                    return;
                }
            }
        }

        /// <summary>
        /// Adjust loyalty points for a customer
        /// </summary>
        /// <param name="customerId">The ID of the customer</param>
        /// <param name="points">Points to add (positive) or deduct (negative)</param>
        public void AdjustLoyaltyPoints(int customerId, int points)
        {
            try
            {
                var customer = _repository.GetCustomer(customerId);
                if (customer != null)
                {
                    // Make sure we don't go below zero points
                    if (customer.LoyaltyPoints + points < 0)
                    {
                        points = -customer.LoyaltyPoints;
                    }

                    customer.LoyaltyPoints += points;
                    customer.ModifiedDate = DateTime.Now;
                    _repository.UpdateCustomer(customer);

                    // Update UI if this is the selected customer
                    if (_selectedCustomer != null && _selectedCustomer.CustomerID == customerId)
                    {
                        _selectedCustomer = customer;
                        numLoyaltyPoints.Value = customer.LoyaltyPoints;
                        txtModifiedDate.Text = customer.ModifiedDate.ToString("yyyy-MM-dd HH:mm");

                        // Update UI to reflect VIP status based on points
                        chkVIP.Checked = customer.LoyaltyPoints >= 200;
                        chkOptInMarketing.Checked = customer.LoyaltyPoints > 0;

                        // Update grid if the customer is in the current view
                        RefreshGrid();
                    }

                    MessageBox.Show($"Loyalty points {(points >= 0 ? "added" : "deducted")} successfully. " +
                                  $"New balance: {customer.LoyaltyPoints} points.",
                                  "Points Adjusted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Customer with ID {customerId} not found.", "Customer Not Found",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adjusting loyalty points: {ex.Message}", "Points Update Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Export customers to CSV file
        /// </summary>
        public void ExportCustomers()
        {
            try
            {
                using (SaveFileDialog saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                    saveDialog.Title = "Export Customers to CSV";
                    saveDialog.DefaultExt = "csv";
                    saveDialog.FileName = $"Customers_Export_{DateTime.Now:yyyyMMdd}";

                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        StringBuilder csv = new StringBuilder();

                        // Add CSV header with only fields that are actually in our Customer model
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

        private void btnExport_Click(object sender, EventArgs e)
        {
            ExportCustomers();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            ImportCustomers();
        }

        private string EscapeCsvField(string field)
        {
            if (string.IsNullOrEmpty(field))
                return string.Empty;

            // Replace double quotes with two double quotes to escape them in CSV
            return field.Replace("\"", "\"\"");
        }

        /// <summary>
        /// Import customers from CSV file
        /// </summary>
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
                        int lineNumber = 0;

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
                            lineNumber = i + 1; // For error reporting (1-based)
                            string line = lines[i];

                            if (string.IsNullOrWhiteSpace(line))
                                continue;

                            // Parse the CSV line manually to handle quoted fields correctly
                            string[] fields = ParseCsvLine(line);

                            if (fields.Length < 5) // Minimum required fields: Name, Phone, Email, Address, Active
                            {
                                MessageBox.Show($"Line {lineNumber} does not have enough fields. Skipping.",
                                    "Import Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                continue;
                            }

                            // Create new customer with basic required fields
                            Customer customer = new Customer
                            {
                                CustomerName = fields[0],
                                Phone = fields[1],
                                Email = fields[2],
                                Address = fields[3],
                                IsActive = fields.Length >= 5 && fields[4].Trim().ToLower() == "yes",
                                LoyaltyPoints = fields.Length >= 6 && int.TryParse(fields[5], out int points) ? points : 0,
                                CreatedDate = DateTime.Now,
                                ModifiedDate = DateTime.Now
                            };

                            if (!string.IsNullOrWhiteSpace(customer.CustomerName))
                            {
                                importedCustomers.Add(customer);
                                importCount++;
                            }
                        }

                        // Save imported customers to repository
                        foreach (var customer in importedCustomers)
                        {
                            _repository.AddCustomer(customer);
                        }

                        // Refresh the grid
                        SearchCustomers();

                        MessageBox.Show($"Successfully imported {importCount} customers.",
                            "Import Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        #endregion
    }
}
