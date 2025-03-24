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
        private const string ConnectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=FantasticStock1;Integrated Security=True;TrustServerCertificate=True";

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

        private int ExecuteNonQuery(string query, params SqlParameter[] parameters)
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
                Supplier s ON e.SupplierID = s.SupplierID
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

        private void DeleteExpense(Expense expense)
        {
            
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

                string query = "SELECT SupplierID, SupplierName FROM Supplier WHERE IsActive = 1 ORDER BY SupplierName";
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
                Supplier s ON e.SupplierID = s.SupplierID
            LEFT JOIN 
                ExpenseCategories c ON e.CategoryID = c.CategoryID
            LEFT JOIN 
                Users u ON e.CreatedBy = u.UserID
            WHERE 
                1=1";

                // Build parameter list

                query += " AND e.ExpenseDate BETWEEN @FromDate AND @ToDate";
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
    }
}
