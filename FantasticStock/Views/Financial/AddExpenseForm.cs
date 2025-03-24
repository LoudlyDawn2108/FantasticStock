using FantasticStock.Models.Financial;
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
using FantasticStock.Services.Admin;

namespace FantasticStock.Views.Financial
{
    public partial class AddExpenseForm: Form
    {
        private List<ExpensesView.ComboboxItem> _categories;
        private List<ExpensesView.ComboboxItem> _suppliers;
        private bool _isEditMode;
        private int _expenseId;
        private readonly SqlDatabaseService _databaseService = new SqlDatabaseService();

        public AddExpenseForm()
        {
            InitializeComponent();
            LoadCategoriesAndSuppliers();
            LoadPaymentMethods();
            GenerateExpenseNumber();

            _databaseService = new SqlDatabaseService();

            // Default date to today
            dtpExpenseDate.Value = DateTime.Today;

            // Default tax deductible to true
            chkTaxDeductible.Checked = true;
        }

        public AddExpenseForm(Expense expense) : this()
        {
            // Set form to edit mode and load expense data
            _isEditMode = true;
            _expenseId = expense.ExpenseID;

            // Set form title
            this.Text = "Edit Expense";
            btnSave.Text = "Update";

            // Populate controls with expense data
            txtExpenseNumber.Text = expense.ExpenseNumber;
            dtpExpenseDate.Value = expense.ExpenseDate;

            // Select the correct supplier
            if (expense.SupplierID.HasValue)
            {
                for (int i = 0; i < cboSupplier.Items.Count; i++)
                {
                    var item = cboSupplier.Items[i] as ExpensesView.ComboboxItem;
                    if (item != null && Convert.ToInt32(item.Value) == expense.SupplierID.Value)
                    {
                        cboSupplier.SelectedIndex = i;
                        break;
                    }
                }
            }

            for (int i = 0; i < cboCategory.Items.Count; i++)
            {
                var item = cboCategory.Items[i] as ExpensesView.ComboboxItem;
                if (item != null && Convert.ToInt32(item.Value) == expense.CategoryID)
                {
                    cboCategory.SelectedIndex = i;
                    break;
                }
            }

            for (int i = 0; i < cboPaymentMethod.Items.Count; i++)
            {
                var item = cboPaymentMethod.Items[i] as ExpensesView.ComboboxItem;
                if (item != null && item.Value.ToString() == expense.PaymentMethod)
                {
                    cboPaymentMethod.SelectedIndex = i;
                    break;
                }
            }

            txtReferenceNumber.Text = expense.ReferenceNumber;
            txtAmount.Text = expense.Amount.ToString("0.00");
            txtTaxAmount.Text = expense.TaxAmount.ToString("0.00");
            UpdateTotalAmount();
            txtNotes.Text = expense.Notes;
            chkTaxDeductible.Checked = expense.IsTaxDeductible;
        }

        private void LoadCategoriesAndSuppliers()
        {
            try
            {
                // Load categories
                string categoryQuery = "SELECT CategoryID, CategoryName FROM ExpenseCategories WHERE IsActive = 1 ORDER BY CategoryName";
                DataTable categories = _databaseService.ExecuteQuery(categoryQuery);

                cboCategory.Items.Clear();
                _categories = new List<ExpensesView.ComboboxItem>();
                foreach (DataRow row in categories.Rows)
                {
                    var item = new ExpensesView.ComboboxItem
                    {
                        Text = row["CategoryName"].ToString(),
                        Value = Convert.ToInt32(row["CategoryID"])
                    };

                    _categories.Add(item);
                    cboCategory.Items.Add(item);
                }

                if (cboCategory.Items.Count > 0)
                    cboCategory.SelectedIndex = 0;

                // Load suppliers
                string supplierQuery = "SELECT SupplierID, SupplierName FROM Supplier WHERE IsActive = 1 ORDER BY SupplierName";
                DataTable suppliers = _databaseService.ExecuteQuery(supplierQuery);

                cboSupplier.Items.Clear();
                _suppliers = new List<ExpensesView.ComboboxItem>();

                // Add "None" option for supplier
                var noneItem = new ExpensesView.ComboboxItem { Text = "-- None --", Value = 0 };
                _suppliers.Add(noneItem);
                cboSupplier.Items.Add(noneItem);

                foreach (DataRow row in suppliers.Rows)
                {
                    var item = new ExpensesView.ComboboxItem
                    {
                        Text = row["SupplierName"].ToString(),
                        Value = Convert.ToInt32(row["SupplierID"])
                    };

                    _suppliers.Add(item);
                    cboSupplier.Items.Add(item);
                }

                if (cboSupplier.Items.Count > 0)
                    cboSupplier.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading categories and suppliers: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPaymentMethods()
        {
            cboPaymentMethod.Items.Clear();
            cboPaymentMethod.Items.Add(new ExpensesView.ComboboxItem { Text = "Credit Card", Value = "Credit Card" });
            cboPaymentMethod.Items.Add(new ExpensesView.ComboboxItem { Text = "Cash", Value = "Cash" });
            cboPaymentMethod.Items.Add(new ExpensesView.ComboboxItem { Text = "Bank Transfer", Value = "Bank Transfer" });
            cboPaymentMethod.Items.Add(new ExpensesView.ComboboxItem { Text = "Check", Value = "Check" });
            cboPaymentMethod.Items.Add(new ExpensesView.ComboboxItem { Text = "Other", Value = "Other" });
            cboPaymentMethod.SelectedIndex = 0;
        }

        private void GenerateExpenseNumber()
        {
            try
            {
                // Generate a unique expense number (e.g., EXP-yyyyMMdd-001)
                string prefix = "EXP-" + DateTime.Now.ToString("yyyyMMdd");

                string query = "SELECT MAX(ExpenseNumber) FROM Expenses WHERE ExpenseNumber LIKE @Prefix+'%'";
                SqlParameter[] parameters = { new SqlParameter("@Prefix", prefix) };

                object result = _databaseService.ExecuteScalar(query, parameters);
                int sequence = 1;

                if (result != null && result != DBNull.Value)
                {
                    string lastNumber = result.ToString();
                    if (lastNumber.Length >= prefix.Length + 4)
                    {
                        string sequenceStr = lastNumber.Substring(prefix.Length + 1);
                        if (int.TryParse(sequenceStr, out int lastSequence))
                        {
                            sequence = lastSequence + 1;
                        }
                    }
                }

                txtExpenseNumber.Text = $"{prefix}-{sequence:D3}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating expense number: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Fallback to a simple format
                txtExpenseNumber.Text = $"EXP-{DateTime.Now.ToString("yyyyMMddHHmmss")}";
            }
        }

        private void TxtAmount_Leave(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtAmount.Text, out decimal amount))
            {
                txtAmount.Text = amount.ToString("0.00");
            }
            else
            {
                txtAmount.Text = "0.00";
            }

            UpdateTotalAmount();
        }

        private void TxtTaxAmount_Leave(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtTaxAmount.Text, out decimal tax))
            {
                txtTaxAmount.Text = tax.ToString("0.00");
            }
            else
            {
                txtTaxAmount.Text = "0.00";
            }

            UpdateTotalAmount();
        }

        private void UpdateTotalAmount()
        {
            decimal amount = decimal.TryParse(txtAmount.Text, out decimal a) ? a : 0;
            decimal tax = decimal.TryParse(txtTaxAmount.Text, out decimal t) ? t : 0;
            decimal total = amount + tax;

            lblTotal.Text = total.ToString("C2");
        }

        private void BtnAddSupplier_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Add Supplier functionality would be implemented here.", "Add Supplier",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            // In a real application, you would open a form to add a new supplier
            // After successfully adding the supplier, you would refresh the supplier list
            // LoadCategoriesAndSuppliers();
        }

        private void BtnAddCategory_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Add Category functionality would be implemented here.", "Add Category",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            // In a real application, you would open a form to add a new category
            // After successfully adding the category, you would refresh the category list
            // LoadCategoriesAndSuppliers();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                SaveExpense();
            }
        }

        private bool ValidateForm()
        {
            // Validate required fields
            if (string.IsNullOrEmpty(txtExpenseNumber.Text))
            {
                MessageBox.Show("Please enter an expense number.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtExpenseNumber.Focus();
                return false;
            }

            if (cboCategory.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a category.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboCategory.Focus();
                return false;
            }

            if (cboPaymentMethod.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a payment method.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboPaymentMethod.Focus();
                return false;
            }

            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Please enter a valid amount greater than zero.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAmount.Focus();
                return false;
            }

            if (!decimal.TryParse(txtTaxAmount.Text, out decimal tax) || tax < 0)
            {
                MessageBox.Show("Please enter a valid tax amount (zero or greater).", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTaxAmount.Focus();
                return false;
            }

            return true;
        }

        private void SaveExpense()
        {
            try
            {
                // Parse form values
                string expenseNumber = txtExpenseNumber.Text.Trim();
                DateTime expenseDate = dtpExpenseDate.Value;
                int? supplierId = null;
                if (cboSupplier.SelectedIndex > 0) // First item is "-- None --"
                {
                    supplierId = Convert.ToInt32(((ExpensesView.ComboboxItem)cboSupplier.SelectedItem).Value);
                }

                int categoryId = Convert.ToInt32(((ExpensesView.ComboboxItem)cboCategory.SelectedItem).Value);
                string paymentMethod = ((ExpensesView.ComboboxItem)cboPaymentMethod.SelectedItem).Value.ToString();
                string referenceNumber = txtReferenceNumber.Text.Trim();
                decimal amount = decimal.Parse(txtAmount.Text);
                decimal taxAmount = decimal.Parse(txtTaxAmount.Text);
                string notes = txtNotes.Text.Trim();
                bool isTaxDeductible = chkTaxDeductible.Checked;

                _databaseService.ExecuteInTransaction((connection, transaction) =>
                {
                    string query;
                    List<SqlParameter> parameters = new List<SqlParameter>();

                    if (_isEditMode)
                    {
                        // Update existing expense
                        query = @"
                    UPDATE Expenses
                    SET ExpenseNumber = @ExpenseNumber,
                        ExpenseDate = @ExpenseDate,
                        SupplierID = @SupplierID,
                        CategoryID = @CategoryID,
                        PaymentMethod = @PaymentMethod,
                        ReferenceNumber = @ReferenceNumber,
                        Amount = @Amount,
                        TaxAmount = @TaxAmount,
                        Notes = @Notes,
                        IsTaxDeductible = @IsTaxDeductible
                    WHERE ExpenseID = @ExpenseID";

                        parameters.Add(new SqlParameter("@ExpenseID", _expenseId));
                    }
                    else
                    {
                        // Insert new expense
                        query = @"
                    INSERT INTO Expenses (
                        ExpenseNumber,
                        ExpenseDate,
                        SupplierID,
                        CategoryID,
                        PaymentMethod,
                        ReferenceNumber,
                        Amount,
                        TaxAmount,
                        Notes,
                        IsTaxDeductible,
                        CreatedBy,
                        CreatedDate
                    ) VALUES (
                        @ExpenseNumber,
                        @ExpenseDate,
                        @SupplierID,
                        @CategoryID,
                        @PaymentMethod,
                        @ReferenceNumber,
                        @Amount,
                        @TaxAmount,
                        @Notes,
                        @IsTaxDeductible,
                        @CreatedBy,
                        @CreatedDate
                    )";

                        parameters.Add(new SqlParameter("@CreatedBy", 1)); // Replace with current user ID in a real app
                        parameters.Add(new SqlParameter("@CreatedDate", DateTime.Now));
                    }
                    parameters.Add(new SqlParameter("@ExpenseNumber", expenseNumber));
                    parameters.Add(new SqlParameter("@ExpenseDate", expenseDate));
                    parameters.Add(new SqlParameter("@SupplierID", supplierId ?? (object)DBNull.Value));
                    parameters.Add(new SqlParameter("@CategoryID", categoryId));
                    parameters.Add(new SqlParameter("@PaymentMethod", paymentMethod));
                    parameters.Add(new SqlParameter("@ReferenceNumber",
                        string.IsNullOrEmpty(referenceNumber) ? (object)DBNull.Value : referenceNumber));
                    parameters.Add(new SqlParameter("@Amount", amount));
                    parameters.Add(new SqlParameter("@TaxAmount", taxAmount));
                    parameters.Add(new SqlParameter("@Notes",
                        string.IsNullOrEmpty(notes) ? (object)DBNull.Value : notes));
                    parameters.Add(new SqlParameter("@IsTaxDeductible", isTaxDeductible));

                    using (SqlCommand command = new SqlCommand(query, connection, transaction))
                    {
                        command.Parameters.AddRange(parameters.ToArray());
                        command.ExecuteNonQuery();
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving expense: {ex.Message}", "Save Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
