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
    public partial class CMView : UserControl
    {
        private CustomerManagementViewModel _viewmodel;
        private List<Customer> _filteredCustomers;
        private Customer _selectedCustomer;
        private bool _isEditing = false;

        public CMView()
        {
            InitializeComponent();

            // Initialize repository
            _viewmodel = new CustomerManagementViewModel();

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
            colLoyaltyPoints.DataPropertyName = "LoyaltyPoints";

            // Setup event handlers
            btnSearch.Click += (s, e) => SearchCustomers();
            btnClear.Click += (s, e) => ClearSearch();
            btnNewCustomer.Click += (s, e) => CreateNewCustomer();
            btnSave.Click += (s, e) => SaveCustomer();
            btnCancel.Click += (s, e) => CancelEdit();
            btnEdit.Click += (s, e) => EditCustomer();
            btnDelete.Click += (s, e) => DeleteCustomer();

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
        }

        private void LoadData()
        {
            // Initial load of all customers
            _filteredCustomers = _viewmodel.Customers.Where(c => c.IsActive).ToList();
            dgvCustomers.DataSource = _filteredCustomers;
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

            // Enable/disable input fields
            txtCustomerName.ReadOnly = !editMode;
            txtPhone.ReadOnly = !editMode;
            txtEmail.ReadOnly = !editMode;
            txtAddress.ReadOnly = !editMode;

            // Show/hide appropriate buttons
            btnSave.Enabled = editMode;
            btnCancel.Enabled = editMode;
            btnEdit.Enabled = !editMode && _selectedCustomer != null;
            btnDelete.Enabled = !editMode && _selectedCustomer != null;
            btnNewCustomer.Enabled = !editMode;

            // Disable grid selection during edit
            dgvCustomers.Enabled = !editMode;
            grpSearch.Enabled = !editMode;
        }

        private void ClearCustomerDetails()
        {
            txtCustomerId.Text = string.Empty;
            txtCustomerName.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtLoyaltyPoints.Text = "0";
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

            txtCustomerId.Text = customer.CustomerID.ToString();
            txtCustomerName.Text = customer.CustomerName ?? string.Empty;
            txtPhone.Text = customer.Phone ?? string.Empty;
            txtEmail.Text = customer.Email ?? string.Empty;
            txtAddress.Text = customer.Address ?? string.Empty;
            txtLoyaltyPoints.Text = customer.LoyaltyPoints.ToString();
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
        #endregion

        #region Business Logic Methods

        private void SearchCustomers()
        {
            string searchText = txtSearch.Text.Trim();
            bool showInactive = chkShowInactive.Checked;

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
                // Search by text and status
                var results = _viewmodel.SearchCustomers(searchText);

                if (!showInactive)
                {
                    results = results.Where(c => c.IsActive).ToList();
                }

                _filteredCustomers = results.ToList();
            }

            // Update DataGridView
            dgvCustomers.DataSource = null;
            dgvCustomers.DataSource = _filteredCustomers;

            // Update status
            lblResults.Text = $"{_filteredCustomers.Count} customer(s) found";
        }

        private void ClearSearch()
        {
            txtSearch.Text = string.Empty;
            chkShowInactive.Checked = false;
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
            if (!ValidateCustomerData())
                return;

            // Create or update customer object
            if (_selectedCustomer.CustomerID == 0) // New customer
            {
                _selectedCustomer.CustomerName = txtCustomerName.Text.Trim();
                _selectedCustomer.Phone = txtPhone.Text.Trim();
                _selectedCustomer.Email = txtEmail.Text.Trim();
                _selectedCustomer.Address = txtAddress.Text.Trim();

                // Add to repository
                _viewmodel.AddCustomer(_selectedCustomer);

                // Add to grid
                _filteredCustomers.Add(_selectedCustomer);
            }
            else // Existing customer
            {
                _selectedCustomer.CustomerName = txtCustomerName.Text.Trim();
                _selectedCustomer.Phone = txtPhone.Text.Trim();
                _selectedCustomer.Email = txtEmail.Text.Trim();
                _selectedCustomer.Address = txtAddress.Text.Trim();
                _selectedCustomer.ModifiedDate = DateTime.Now;

                // Update in repository
                _viewmodel.UpdateCustomer(_selectedCustomer);
            }

            // Exit edit mode
            SetDetailsEditMode(false);

            // Refresh grid to show changes
            RefreshGrid();

            // Select the current customer in the grid
            SelectCustomerInGrid(_selectedCustomer.CustomerID);
        }

        private void DeleteCustomer()
        {
            if (_selectedCustomer == null)
                return;

            if (MessageBox.Show("Are you sure you want to deactivate this customer?",
                               "Confirm Deactivation",
                               MessageBoxButtons.YesNo,
                               MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // Deactivate in repository
                _viewmodel.DeactivateCustomer(_selectedCustomer.CustomerID);

                // Remove from filtered list if it doesn't show inactive
                if (!chkShowInactive.Checked)
                {
                    _filteredCustomers.Remove(_selectedCustomer);
                    RefreshGrid();
                }
                else
                {
                    // Just update the customer status in the grid
                    RefreshGrid();
                }

                // Clear selection
                _selectedCustomer = null;
                ClearCustomerDetails();
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
            }
        }

        private void CancelEdit()
        {
            if (_selectedCustomer?.CustomerID == 0) // New customer
            {
                _selectedCustomer = null;
                ClearCustomerDetails();
            }
            else if (_selectedCustomer != null) // Existing customer
            {
                DisplayCustomerDetails(_selectedCustomer);
            }

            SetDetailsEditMode(false);
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

            // Validate email if entered
            if (!string.IsNullOrWhiteSpace(txtEmail.Text) &&
                !txtEmail.Text.Contains("@") || !txtEmail.Text.Contains("."))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            return true;
        }
        #endregion

        #region Helper Methods

        private void RefreshGrid()
        {
            dgvCustomers.DataSource = null;
            dgvCustomers.DataSource = _filteredCustomers;
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

        public void AdjustLoyaltyPoints(int customerId, int points)
        {
            var customer = _viewmodel.GetCustomer(customerId);
            if (customer != null)
            {
                customer.LoyaltyPoints += points;
                customer.ModifiedDate = DateTime.Now;
                _viewmodel.UpdateCustomer(customer);

                // Update UI if this is the selected customer
                if (_selectedCustomer != null && _selectedCustomer.CustomerID == customerId)
                {
                    _selectedCustomer = customer;
                    txtLoyaltyPoints.Text = customer.LoyaltyPoints.ToString();
                    txtModifiedDate.Text = customer.ModifiedDate.ToString("yyyy-MM-dd HH:mm");
                }
            }
        }
        #endregion
    }
}
