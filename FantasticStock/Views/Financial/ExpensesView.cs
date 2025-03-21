using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FantasticStock.Models.Financial;
using System.Data.SqlClient;

namespace FantasticStock.Views.Financial
{
    public partial class ExpensesView : UserControl
    {
        private List<Expense> _expenses;
        private Expense _selectedExpense;

        public ExpensesView()
        {
            InitializeComponent();
            dtpFromDate.Value = DateTime.Today.AddMonths(-1);
            dtpToDate.Value = DateTime.Today;
            this.Load += ExpensesView_Load;
            txtSearch.KeyDown += TxtSearch_KeyDown;
            btnSearch.Click += BtnSearch_Click;
            btnReset.Click += BtnReset_Click;
            btnExport.Click += BtnExport_Click;
            btnAddExpense.Click += BtnAddExpense_Click;
        }

        private void ExpensesView_Load(object sender, EventArgs e)
        {
            ConfigureDataGridView();
            LoadCategories();
            LoadRecentExpenses(20);
            LoadPaymentMethods();
            LoadTaxStatuses();
            LoadSuppliers();
        }

        public class ComboboxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }

        private DataTable ExecuteQuery(string query, params SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=FantasticStock;Integrated Security=True;TrustServerCertificate=True"))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }

        private int ExecuteNonQuery(string query, params SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=FantasticStock;Integrated Security=True;TrustServerCertificate=True"))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    return command.ExecuteNonQuery();
                }
            }
        }

        private void LoadRecentExpenses(int limit)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                string query = @"
            SELECT TOP (@Limit)
                e.ExpenseID,
                e.ExpenseNumber,
                e.ExpenseDate,
                e.SupplierID,
                s.SupplierName,
                e.CategoryID,
                c.CategoryName,
                e.PaymentMethod,
                e.ReferenceNumber,
                e.Amount,
                e.TaxAmount,
                e.Amount + e.TaxAmount AS TotalAmount,
                e.Notes,
                e.IsTaxDeductible,
                e.CreatedBy,
                u.UserName AS CreatedByName,
                e.CreatedDate,
                FORMAT(e.ExpenseDate, 'yyyy-MM-dd') AS FormattedDate
            FROM 
                Expenses e
            LEFT JOIN 
                Suppliers s ON e.SupplierID = s.SupplierID
            LEFT JOIN 
                ExpenseCategories c ON e.CategoryID = c.CategoryID
            LEFT JOIN 
                Users u ON e.CreatedBy = u.UserID
            ORDER BY 
                e.ExpenseDate DESC, e.ExpenseID DESC";

                SqlParameter[] parameters = { new SqlParameter("@Limit", limit) };
                DataTable dataTable = ExecuteQuery(query, parameters);

                // Convert DataTable to List<Expense>
                _expenses = new List<Expense>();
                foreach (DataRow row in dataTable.Rows)
                {
                    _expenses.Add(new Expense
                    {
                        ExpenseID = Convert.ToInt32(row["ExpenseID"]),
                        ExpenseNumber = row["ExpenseNumber"].ToString(),
                        ExpenseDate = Convert.ToDateTime(row["ExpenseDate"]),
                        SupplierID = row["SupplierID"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["SupplierID"]),
                        SupplierName = row["SupplierName"].ToString(),
                        CategoryID = Convert.ToInt32(row["CategoryID"]),
                        CategoryName = row["CategoryName"].ToString(),
                        PaymentMethod = row["PaymentMethod"].ToString(),
                        ReferenceNumber = row["ReferenceNumber"].ToString(),
                        Amount = Convert.ToDecimal(row["Amount"]),
                        TaxAmount = Convert.ToDecimal(row["TaxAmount"]),
                        Notes = row["Notes"].ToString(),
                        IsTaxDeductible = Convert.ToBoolean(row["IsTaxDeductible"]),
                        CreatedBy = Convert.ToInt32(row["CreatedBy"]),
                        CreatedByName = row["CreatedByName"].ToString(),
                        CreatedDate = Convert.ToDateTime(row["CreatedDate"])
                    });
                }

                // Bind to grid
                dgvExpenses.DataSource = null;
                dgvExpenses.DataSource = _expenses;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading recent expenses: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void LoadExpenses()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                string query = @"
                    SELECT 
                        e.ExpenseID,
                        e.ExpenseNumber,
                        e.ExpenseDate,
                        e.SupplierID,
                        s.SupplierName,
                        e.CategoryID,
                        c.CategoryName,
                        e.PaymentMethod,
                        e.ReferenceNumber,
                        e.Amount,
                        e.TaxAmount,
                        e.Amount + e.TaxAmount AS TotalAmount,
                        e.Notes,
                        e.IsTaxDeductible,
                        e.CreatedBy,
                        u.UserName AS CreatedByName,
                        e.CreatedDate,
                        FORMAT(e.ExpenseDate, 'yyyy-MM-dd') AS FormattedDate
                    FROM 
                        Expenses e
                    LEFT JOIN 
                        Suppliers s ON e.SupplierID = s.SupplierID
                    LEFT JOIN 
                        ExpenseCategories c ON e.CategoryID = c.CategoryID
                    LEFT JOIN 
                        Users u ON e.CreatedBy = u.UserID
                    WHERE 
                        e.ExpenseDate BETWEEN @FromDate AND @ToDate
                        AND (@CategoryID = 0 OR e.CategoryID = @CategoryID)
                        AND (@SupplierID = 0 OR e.SupplierID = @SupplierID)
                        AND (@PaymentMethod = 'All' OR e.PaymentMethod = @PaymentMethod)
                        AND (@TaxStatus = 'All' OR (@TaxStatus = 'Deductible' AND e.IsTaxDeductible = 1) OR (@TaxStatus = 'Non-Deductible' AND e.IsTaxDeductible = 0))
                        AND (@SearchText = '' 
                             OR e.ExpenseNumber LIKE '%' + @SearchText + '%'
                             OR e.ReferenceNumber LIKE '%' + @SearchText + '%'
                             OR s.SupplierName LIKE '%' + @SearchText + '%'
                             OR e.Notes LIKE '%' + @SearchText + '%')
                    ORDER BY 
                        e.ExpenseDate DESC, e.ExpenseID DESC";

                // Create parameters for the query
                SqlParameter[] parameters = {
    new SqlParameter("@FromDate", dtpFromDate.Value),
    new SqlParameter("@ToDate", dtpToDate.Value.AddDays(1).AddSeconds(-1)),
    new SqlParameter("@CategoryID", cboCategory.SelectedItem != null ? ((ComboboxItem)cboCategory.SelectedItem).Value : 0),
    new SqlParameter("@SupplierID", cboSupplier.SelectedItem != null ? ((ComboboxItem)cboSupplier.SelectedItem).Value : 0),
    new SqlParameter("@PaymentMethod", cboPaymentMethod.SelectedIndex == 0 ? "All" : cboPaymentMethod.SelectedItem != null ? ((ComboboxItem)cboPaymentMethod.SelectedItem).Value : "All"),
    new SqlParameter("@TaxStatus", cboTaxStatus.SelectedIndex == 0 ? "All" : cboTaxStatus.SelectedItem != null ? ((ComboboxItem)cboTaxStatus.SelectedItem).Value : "All"),
    new SqlParameter("@SearchText", txtSearch.Text?.Trim() ?? string.Empty)
};

                DataTable dataTable = ExecuteQuery(query, parameters);

                // Convert DataTable to List<Expense>
                _expenses = new List<Expense>();
                foreach (DataRow row in dataTable.Rows)
                {
                    _expenses.Add(new Expense
                    {
                        ExpenseID = Convert.ToInt32(row["ExpenseID"]),
                        ExpenseNumber = row["ExpenseNumber"].ToString(),
                        ExpenseDate = Convert.ToDateTime(row["ExpenseDate"]),
                        SupplierID = row["SupplierID"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["SupplierID"]),
                        SupplierName = row["SupplierName"].ToString(),
                        CategoryID = Convert.ToInt32(row["CategoryID"]),
                        CategoryName = row["CategoryName"].ToString(),
                        PaymentMethod = row["PaymentMethod"].ToString(),
                        ReferenceNumber = row["ReferenceNumber"].ToString(),
                        Amount = Convert.ToDecimal(row["Amount"]),
                        TaxAmount = Convert.ToDecimal(row["TaxAmount"]),
                        Notes = row["Notes"].ToString(),
                        IsTaxDeductible = Convert.ToBoolean(row["IsTaxDeductible"]),
                        CreatedBy = Convert.ToInt32(row["CreatedBy"]),
                        CreatedByName = row["CreatedByName"].ToString(),
                        CreatedDate = Convert.ToDateTime(row["CreatedDate"])
                    });
                }

                // Bind to grid
                dgvExpenses.DataSource = null;
                dgvExpenses.DataSource = _expenses;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading expenses: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void DeleteExpense(Expense expense)
        {
            // In a real app, this would delete the expense from the database
            var result = MessageBox.Show($"Are you sure you want to delete expense {expense.ExpenseNumber}?", "Delete Expense", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                // Delete the expense from the database
                string query = "DELETE FROM Expenses WHERE ExpenseID = @ExpenseID";
                SqlParameter[] parameters = { new SqlParameter("@ExpenseID", expense.ExpenseID) };
                ExecuteNonQuery(query, parameters);

                // Refresh the list
                LoadRecentExpenses(20);
            }
        }

        private void LoadCategories()
        {
            try
            {
                cboCategory.Items.Clear();
                cboCategory.Items.Add(new ComboboxItem { Text = "All Categories", Value = 0 });

                string query = "SELECT CategoryID, CategoryName FROM ExpenseCategories WHERE IsActive = 1 ORDER BY CategoryName";
                DataTable categories = ExecuteQuery(query);

                foreach (DataRow row in categories.Rows)
                {
                    cboCategory.Items.Add(new ComboboxItem
                    {
                        Text = row["CategoryName"].ToString(),
                        Value = Convert.ToInt32(row["CategoryID"])
                    });
                }

                cboCategory.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading categories: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPaymentMethods()
        {
            cboPaymentMethod.Items.Clear();
            cboPaymentMethod.Items.Add(new ComboboxItem { Text = "All Methods", Value = "All" });
            cboPaymentMethod.Items.Add(new ComboboxItem { Text = "Credit Card", Value = "Credit Card" });
            cboPaymentMethod.Items.Add(new ComboboxItem { Text = "Cash", Value = "Cash" });
            cboPaymentMethod.Items.Add(new ComboboxItem { Text = "Bank Transfer", Value = "Bank Transfer" });
            cboPaymentMethod.Items.Add(new ComboboxItem { Text = "Check", Value = "Check" });
            cboPaymentMethod.Items.Add(new ComboboxItem { Text = "Other", Value = "Other" });
            cboPaymentMethod.SelectedIndex = 0;
        }

        private void LoadTaxStatuses()
        {
            cboTaxStatus.Items.Clear();
            cboTaxStatus.Items.Add(new ComboboxItem { Text = "All", Value = "All" });
            cboTaxStatus.Items.Add(new ComboboxItem { Text = "Deductible", Value = "Deductible" });
            cboTaxStatus.Items.Add(new ComboboxItem { Text = "Non-Deductible", Value = "Non-Deductible" });
            cboTaxStatus.SelectedIndex = 0;
        }

        private void LoadSuppliers()
        {
            try
            {
                cboSupplier.Items.Clear();
                cboSupplier.Items.Add(new ComboboxItem { Text = "All Suppliers", Value = 0 });

                string query = "SELECT SupplierID, SupplierName FROM Suppliers WHERE IsActive = 1 ORDER BY SupplierName";
                DataTable suppliers = ExecuteQuery(query);

                foreach (DataRow row in suppliers.Rows)
                {
                    cboSupplier.Items.Add(new ComboboxItem
                    {
                        Text = row["SupplierName"].ToString(),
                        Value = Convert.ToInt32(row["SupplierID"])
                    });
                }

                cboSupplier.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading suppliers: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureDataGridView()
        {
            // Configure DataGridView columns
            dgvExpenses.AutoGenerateColumns = false;
            dgvExpenses.Columns.Clear();

            // Add columns
            dgvExpenses.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ExpenseNumber",
                HeaderText = "Expense #",
                Width = 100
            });

            dgvExpenses.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "FormattedDate",
                HeaderText = "Date",
                Width = 100
            });

            dgvExpenses.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "SupplierName",
                HeaderText = "Supplier",
                Width = 150
            });

            dgvExpenses.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CategoryName",
                HeaderText = "Category",
                Width = 120
            });

            dgvExpenses.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PaymentMethod",
                HeaderText = "Payment Method",
                Width = 120
            });

            dgvExpenses.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Amount",
                HeaderText = "Amount",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            dgvExpenses.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TaxAmount",
                HeaderText = "Tax",
                Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            dgvExpenses.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TotalAmount",
                HeaderText = "Total",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            // Add action buttons
            DataGridViewButtonColumn editButtonColumn = new DataGridViewButtonColumn
            {
                HeaderText = "",
                Text = "Edit",
                UseColumnTextForButtonValue = true,
                Width = 60
            };
            dgvExpenses.Columns.Add(editButtonColumn);

            DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn
            {
                HeaderText = "",
                Text = "Delete",
                UseColumnTextForButtonValue = true,
                Width = 60
            };
            dgvExpenses.Columns.Add(deleteButtonColumn);

            // Set up cell click handler for buttons
            dgvExpenses.CellClick += DgvExpenses_CellClick;
        }


        private void DgvExpenses_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Handle button clicks
            if (e.RowIndex < 0 || e.RowIndex >= _expenses.Count)
                return;

            _selectedExpense = _expenses[e.RowIndex];
            // Edit button column
            if (e.ColumnIndex == dgvExpenses.Columns.Count - 2)
            {
                EditExpense(_selectedExpense);
            }
            // Delete button column
            else if (e.ColumnIndex == dgvExpenses.Columns.Count - 1)
            {
                DeleteExpense(_selectedExpense);
            }
        }

        private void EditExpense(Expense expense)
        {
            try
            {
                // Open the AddExpenseForm in edit mode
                using (var editExpenseForm = new AddExpenseForm(expense))
                {
                    if (editExpenseForm.ShowDialog() == DialogResult.OK)
                    {
                        // Refresh the list
                        LoadRecentExpenses(20);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error editing expense: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SearchExpenses()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                // Build the search query
                string query = @"
            SELECT 
                e.ExpenseID,
                e.ExpenseNumber,
                e.ExpenseDate,
                e.SupplierID,
                s.SupplierName,
                e.CategoryID,
                c.CategoryName,
                e.PaymentMethod,
                e.ReferenceNumber,
                e.Amount,
                e.TaxAmount,
                e.Amount + e.TaxAmount AS TotalAmount,
                e.Notes,
                e.IsTaxDeductible,
                e.CreatedBy,
                u.UserName AS CreatedByName,
                e.CreatedDate,
                FORMAT(e.ExpenseDate, 'yyyy-MM-dd') AS FormattedDate
            FROM 
                Expenses e
            LEFT JOIN 
                Suppliers s ON e.SupplierID = s.SupplierID
            LEFT JOIN 
                ExpenseCategories c ON e.CategoryID = c.CategoryID
            LEFT JOIN 
                Users u ON e.CreatedBy = u.UserID
            WHERE 
                e.ExpenseDate BETWEEN @FromDate AND @ToDate";

                // Build parameter list
                List<SqlParameter> parameters = new List<SqlParameter>
        {
            new SqlParameter("@FromDate", dtpFromDate.Value),
            new SqlParameter("@ToDate", dtpToDate.Value.AddDays(1).AddSeconds(-1))
        };

                // Add category filter
                if (cboCategory.SelectedIndex > 0)
                {
                    query += " AND e.CategoryID = @CategoryID";
                    parameters.Add(new SqlParameter("@CategoryID", ((ComboboxItem)cboCategory.SelectedItem).Value));
                }

                // Add supplier filter
                if (cboSupplier.SelectedIndex > 0)
                {
                    query += " AND e.SupplierID = @SupplierID";
                    parameters.Add(new SqlParameter("@SupplierID", ((ComboboxItem)cboSupplier.SelectedItem).Value));
                }

                // Add payment method filter
                if (cboPaymentMethod.SelectedIndex > 0)
                {
                    query += " AND e.PaymentMethod = @PaymentMethod";
                    parameters.Add(new SqlParameter("@PaymentMethod", ((ComboboxItem)cboPaymentMethod.SelectedItem).Value));
                }

                // Add tax status filter
                if (cboTaxStatus.SelectedIndex > 0)
                {
                    if (cboTaxStatus.SelectedIndex == 1) // Deductible
                    {
                        query += " AND e.IsTaxDeductible = 1";
                    }
                    else if (cboTaxStatus.SelectedIndex == 2) // Non-Deductible
                    {
                        query += " AND e.IsTaxDeductible = 0";
                    }
                }

                // Add text search filter
                if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    string searchText = txtSearch.Text.Trim();
                    query += @" AND (
                e.ExpenseNumber LIKE @SearchText 
                OR e.ReferenceNumber LIKE @SearchText
                OR s.SupplierName LIKE @SearchText
                OR e.Notes LIKE @SearchText
                OR c.CategoryName LIKE @SearchText
            )";
                    parameters.Add(new SqlParameter("@SearchText", $"%{searchText}%"));
                }

                // Add sort order
                query += " ORDER BY e.ExpenseDate DESC, e.ExpenseID DESC";

                // Execute the query
                DataTable dataTable = ExecuteQuery(query, parameters.ToArray());

                // Convert DataTable to List<Expense>
                _expenses = new List<Expense>();
                foreach (DataRow row in dataTable.Rows)
                {
                    _expenses.Add(new Expense
                    {
                        ExpenseID = Convert.ToInt32(row["ExpenseID"]),
                        ExpenseNumber = row["ExpenseNumber"]?.ToString() ?? string.Empty,
                        ExpenseDate = Convert.ToDateTime(row["ExpenseDate"]),
                        SupplierID = row["SupplierID"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["SupplierID"]),
                        SupplierName = row["SupplierName"]?.ToString() ?? string.Empty,
                        CategoryID = Convert.ToInt32(row["CategoryID"]),
                        CategoryName = row["CategoryName"]?.ToString() ?? string.Empty,
                        PaymentMethod = row["PaymentMethod"]?.ToString() ?? string.Empty,
                        ReferenceNumber = row["ReferenceNumber"]?.ToString() ?? string.Empty,
                        Amount = Convert.ToDecimal(row["Amount"]),
                        TaxAmount = Convert.ToDecimal(row["TaxAmount"]),
                        Notes = row["Notes"]?.ToString() ?? string.Empty,
                        IsTaxDeductible = Convert.ToBoolean(row["IsTaxDeductible"]),
                        CreatedBy = Convert.ToInt32(row["CreatedBy"]),
                        CreatedByName = row["CreatedByName"]?.ToString() ?? string.Empty,
                        CreatedDate = Convert.ToDateTime(row["CreatedDate"])
                    });
                }

                // Update the data grid
                dgvExpenses.DataSource = null;
                dgvExpenses.DataSource = _expenses;

                // Show results count and total
                decimal totalAmount = _expenses.Sum(e => e.TotalAmount);
                string statusMessage = $"Found {_expenses.Count} expense(s). Total: {totalAmount:C}";

                // You could display this in a status strip label if you have one
                // lblStatus.Text = statusMessage;

                // Or show it in a message box for now
                if (_expenses.Count == 0)
                {
                    MessageBox.Show("No expenses match your search criteria.", "Search Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching expenses: {ex.Message}", "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        // Update the BtnSearch_Click handler to use this new search function
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            SearchExpenses();
        }

        // Add a key down handler for the txtSearch control to allow searching by pressing Enter
        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Prevent beep sound
                e.Handled = true;
                SearchExpenses();
            }
        }


        private void BtnAddExpense_Click(object sender, EventArgs e)
        {
            try
            {
                // Open the AddExpenseForm
                using (var addExpenseForm = new AddExpenseForm())
                {
                    if (addExpenseForm.ShowDialog() == DialogResult.OK)
                    {
                        // Refresh the list
                        LoadRecentExpenses(20);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening expense form: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            // Reset all filters
            dtpFromDate.Value = DateTime.Today.AddMonths(-1);
            dtpToDate.Value = DateTime.Today;
            txtSearch.Text = string.Empty;
            cboCategory.SelectedIndex = 0;
            cboSupplier.SelectedIndex = 0;
            cboPaymentMethod.SelectedIndex = 0;
            cboTaxStatus.SelectedIndex = 0;

            // Reload data
            LoadRecentExpenses(20);
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            if (_expenses == null || _expenses.Count == 0)
            {
                MessageBox.Show("No expenses to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SaveFileDialog saveDialog = new SaveFileDialog
            {
                Filter = "Excel Files (*.xlsx)|*.xlsx|CSV Files (*.csv)|*.csv",
                DefaultExt = "csv",
                FileName = $"Expenses_{DateTime.Now:yyyyMMdd}"
            };

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;

                    string filePath = saveDialog.FileName;
                    string fileExt = System.IO.Path.GetExtension(filePath).ToLower();

                    if (fileExt == ".xlsx")
                    {
                        MessageBox.Show("Excel export not supported yet.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (fileExt == ".csv")
                    {
                        // Export to CSV
                        using (var sw = new System.IO.StreamWriter(filePath, false, Encoding.UTF8))
                        {
                            // Write header
                            sw.WriteLine("Expense Number,Date,Supplier,Category,Payment Method,Amount,Tax Amount,Total Amount,Reference Number,Tax Deductible,Notes");

                            // Write data rows
                            foreach (var expense in _expenses)
                            {
                                sw.WriteLine($"\"{expense.ExpenseNumber}\",\"{expense.ExpenseDate:yyyy-MM-dd}\",\"{expense.SupplierName}\",\"{expense.CategoryName}\"," +
                                             $"\"{expense.PaymentMethod}\",{expense.Amount},{expense.TaxAmount},{expense.TotalAmount},\"{expense.ReferenceNumber}\"," +
                                             $"{(expense.IsTaxDeductible ? "Yes" : "No")},\"{expense.Notes.Replace("\"", "\"\"")}\"");
                            }
                        }

                        MessageBox.Show($"Expenses exported successfully to {filePath}", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error exporting expenses: {ex.Message}", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            }
        }


        public partial class AddExpenseForm : Form
        {
            private const string ConnectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=FantasticStock;Integrated Security=True;TrustServerCertificate=True";
            private List<ExpensesView.ComboboxItem> _categories;
            private List<ExpensesView.ComboboxItem> _suppliers;
            private bool _isEditMode;
            private int _expenseId;

            public AddExpenseForm()
            {
                InitializeComponent();
                LoadCategoriesAndSuppliers();
                LoadPaymentMethods();
                GenerateExpenseNumber();

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

            private void InitializeComponent()
            {
                this.SuspendLayout();

                // Form setup
                this.Text = "Add New Expense";
                this.ClientSize = new Size(500, 500);
                this.FormBorderStyle = FormBorderStyle.FixedDialog;
                this.MaximizeBox = false;
                this.MinimizeBox = false;
                this.StartPosition = FormStartPosition.CenterParent;
                this.AutoScaleMode = AutoScaleMode.Font;
                this.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);

                // Create Labels
                var lblExpenseNumber = new Label { Text = "Expense #:", Left = 20, Top = 20, Width = 120 };
                var lblExpenseDate = new Label { Text = "Date:", Left = 20, Top = 50, Width = 120 };
                var lblSupplier = new Label { Text = "Supplier:", Left = 20, Top = 80, Width = 120 };
                var lblCategory = new Label { Text = "Category:", Left = 20, Top = 110, Width = 120 };
                var lblPaymentMethod = new Label { Text = "Payment Method:", Left = 20, Top = 140, Width = 120 };
                var lblReferenceNumber = new Label { Text = "Reference #:", Left = 20, Top = 170, Width = 120 };
                var lblAmount = new Label { Text = "Amount:", Left = 20, Top = 200, Width = 120 };
                var lblTaxAmount = new Label { Text = "Tax Amount:", Left = 20, Top = 230, Width = 120 };
                var lblTotalAmount = new Label { Text = "Total:", Left = 20, Top = 260, Width = 120 };
                var lblNotes = new Label { Text = "Notes:", Left = 20, Top = 290, Width = 120 };

                // Create controls
                txtExpenseNumber = new TextBox { Left = 150, Top = 20, Width = 300, ReadOnly = true };
                dtpExpenseDate = new DateTimePicker { Left = 150, Top = 50, Width = 200 };
                cboSupplier = new ComboBox { Left = 150, Top = 80, Width = 300, DropDownStyle = ComboBoxStyle.DropDownList };

                // Add supplier button
                var btnAddSupplier = new Button { Text = "+", Left = 460, Top = 80, Width = 25, Height = 25 };
                btnAddSupplier.Click += BtnAddSupplier_Click;

                cboCategory = new ComboBox { Left = 150, Top = 110, Width = 300, DropDownStyle = ComboBoxStyle.DropDownList };

                // Add category button
                var btnAddCategory = new Button { Text = "+", Left = 460, Top = 110, Width = 25, Height = 25 };
                btnAddCategory.Click += BtnAddCategory_Click;

                cboPaymentMethod = new ComboBox { Left = 150, Top = 140, Width = 300, DropDownStyle = ComboBoxStyle.DropDownList };
                txtReferenceNumber = new TextBox { Left = 150, Top = 170, Width = 300 };
                txtAmount = new TextBox { Left = 150, Top = 200, Width = 150, TextAlign = HorizontalAlignment.Right };
                txtTaxAmount = new TextBox { Left = 150, Top = 230, Width = 150, TextAlign = HorizontalAlignment.Right };
                lblTotal = new Label { Left = 150, Top = 260, Width = 150, Text = "$0.00", Font = new Font("Segoe UI", 9F, FontStyle.Bold), TextAlign = ContentAlignment.MiddleRight };
                txtNotes = new TextBox { Left = 150, Top = 290, Width = 300, Height = 80, Multiline = true };

                // Create tax deductible checkbox
                chkTaxDeductible = new CheckBox { Text = "Tax Deductible", Left = 150, Top = 380, Width = 150, Checked = true };
                btnSave = new Button { Text = "Save", Left = 230, Top = 420, Width = 100 };
                btnCancel = new Button { Text = "Cancel", Left = 350, Top = 420, Width = 100, DialogResult = DialogResult.Cancel };

                // Wire up events
                txtAmount.Leave += TxtAmount_Leave;
                txtTaxAmount.Leave += TxtTaxAmount_Leave;
                btnSave.Click += BtnSave_Click;

                // Add controls to form
                this.Controls.AddRange(new Control[] {
                lblExpenseNumber, txtExpenseNumber,
                lblExpenseDate, dtpExpenseDate,
                lblSupplier, cboSupplier, btnAddSupplier,
                lblCategory, cboCategory, btnAddCategory,
                lblPaymentMethod, cboPaymentMethod,
                lblReferenceNumber, txtReferenceNumber,
                lblAmount, txtAmount,
                lblTaxAmount, txtTaxAmount,
                lblTotalAmount, lblTotal,
                lblNotes, txtNotes,
                chkTaxDeductible,
                btnSave, btnCancel
            });

                this.AcceptButton = btnSave;
                this.CancelButton = btnCancel;

                this.ResumeLayout(false);
            }

            // UI Controls
            private TextBox txtExpenseNumber;
            private DateTimePicker dtpExpenseDate;
            private ComboBox cboSupplier;
            private ComboBox cboCategory;
            private ComboBox cboPaymentMethod;
            private TextBox txtReferenceNumber;
            private TextBox txtAmount;
            private TextBox txtTaxAmount;
            private Label lblTotal;
            private TextBox txtNotes;
            private CheckBox chkTaxDeductible;
            private Button btnSave;
            private Button btnCancel;

            private void LoadCategoriesAndSuppliers()
            {
                try
                {
                    // Load categories
                    string categoryQuery = "SELECT CategoryID, CategoryName FROM ExpenseCategories WHERE IsActive = 1 ORDER BY CategoryName";
                    DataTable categories = ExecuteQuery(categoryQuery);

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
                    string supplierQuery = "SELECT SupplierID, SupplierName FROM Suppliers WHERE IsActive = 1 ORDER BY SupplierName";
                    DataTable suppliers = ExecuteQuery(supplierQuery);

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

                    object result = ExecuteScalar(query, parameters);
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

                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        connection.Open();

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
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddRange(parameters.ToArray());
                            command.ExecuteNonQuery();
                        }

                        MessageBox.Show(_isEditMode ? "Expense updated successfully!" : "Expense added successfully!",
                            "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving expense: {ex.Message}", "Save Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            private DataTable ExecuteQuery(string query, params SqlParameter[] parameters)
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            return dataTable;
                        }
                    }
                }
            }

            private object ExecuteScalar(string query, params SqlParameter[] parameters)
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }
                        return command.ExecuteScalar();
                    }
                }
            }
        }
    }
}
