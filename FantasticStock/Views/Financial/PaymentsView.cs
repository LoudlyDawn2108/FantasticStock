using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FantasticStock.Models.Financial;
using static FantasticStock.Views.Financial.ExpensesView;

namespace FantasticStock.Views.Financial
{
    public partial class PaymentsView : UserControl
    {

        private List<Payment> _payments;
        private Payment _selectedPayment;
        private const string ConnectionString = "Data Source=TUNGCORN\\SQLEXPRESS;Initial Catalog=FantasticStock1;Integrated Security=True;TrustServerCertificate=True";

        public PaymentsView()
        {
            InitializeComponent();
            dtpFromDate.Value = DateTime.Today.AddMonths(-1);
            dtpToDate.Value = DateTime.Today;

            this.Load += PaymentsView_Load;
            btnSearch.Click += BtnSearch_Click;
            btnReset.Click += BtnReset_Click;
            btnExport.Click += BtnExport_Click;
            btnAddPayment.Click += BtnAddPayment_Click;
            txtSearch.KeyDown += TxtSearch_KeyDown;
        }

        private void PaymentsView_Load(object sender, EventArgs e)
        {
            ConfigureDataGridView();
            LoadCustomers();
            LoadPaymentMethods();
            LoadInvoices();
            LoadRecentPayments(20);
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

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            LoadPayments();
        }
        private void LoadPaymentMethods()
        {
            cboPaymentMethod.Items.Clear();
            cboPaymentMethod.Items.Add(new ComboboxItem { Text = "All Methods", Value = "All" });
            cboPaymentMethod.Items.Add(new ComboboxItem { Text = "Credit Card", Value = "Credit Card" });
            cboPaymentMethod.Items.Add(new ComboboxItem { Text = "Cash", Value = "Cash" });
            cboPaymentMethod.Items.Add(new ComboboxItem { Text = "Bank Transfer", Value = "Bank Transfer" });
            cboPaymentMethod.Items.Add(new ComboboxItem { Text = "Check", Value = "Check" });
            cboPaymentMethod.SelectedIndex = 0;
        }

        private void LoadCustomers()
        {
            try
            {
                cboCustomer.Items.Clear();
                cboCustomer.Items.Add(new ComboboxItem { Text = "All Customers", Value = 0 });

                string query = "SELECT CustomerID, CustomerName FROM Customer WHERE IsActive = 1 ORDER BY CustomerName";
                DataTable customers = ExecuteQuery(query);

                foreach (DataRow row in customers.Rows)
                {
                    cboCustomer.Items.Add(new ComboboxItem
                    {
                        Text = row["CustomerName"].ToString(),
                        Value = Convert.ToInt32(row["CustomerID"])
                    });
                }

                cboCustomer.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customers: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadInvoices()
        {
            try
            {
                cboInvoice.Items.Clear();
                cboInvoice.Items.Add(new ComboboxItem { Text = "All Invoices", Value = 0 });
                cboInvoice.Items.Add(new ComboboxItem { Text = "Unallocated Payments", Value = -1 });

                string query = "SELECT InvoiceID, InvoiceNumber FROM Invoices ORDER BY InvoiceDate DESC";
                DataTable invoices = ExecuteQuery(query);

                foreach (DataRow row in invoices.Rows)
                {
                    cboInvoice.Items.Add(new ComboboxItem
                    {
                        Text = row["InvoiceNumber"].ToString(),
                        Value = Convert.ToInt32(row["InvoiceID"])
                    });
                }

                cboInvoice.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading invoices: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadRecentPayments(int limit)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                string query = @"
        SELECT TOP (@Limit)
            p.PaymentID,
            p.PaymentNumber,
            p.PaymentDate,
            p.CustomerID,
            c.CustomerName,
            p.InvoiceID,
            i.InvoiceNumber,
            p.PaymentMethod,
            p.ReferenceNumber,
            p.Amount,
            p.Notes,
            p.CreatedBy,
            u.UserName AS CreatedByName,
            p.CreatedDate
        FROM 
            Payments p
        LEFT JOIN 
            Customer c ON p.CustomerID = c.CustomerID
        LEFT JOIN 
            Invoices i ON p.InvoiceID = i.InvoiceID
        LEFT JOIN 
            Users u ON p.CreatedBy = u.UserID
        ORDER BY 
            p.PaymentDate DESC, p.PaymentID DESC";

                SqlParameter[] parameters = { new SqlParameter("@Limit", limit) };
                DataTable dataTable = ExecuteQuery(query, parameters);

                if (dataTable.Rows.Count > 0)
                {
                    // Convert to your Payment objects
                    _payments = new List<Payment>();
                    foreach (DataRow row in dataTable.Rows)
                    {
                        _payments.Add(new Payment
                        {
                            PaymentID = Convert.ToInt32(row["PaymentID"]),
                            PaymentNumber = row["PaymentNumber"].ToString(),
                            PaymentDate = Convert.ToDateTime(row["PaymentDate"]),
                            CustomerID = Convert.ToInt32(row["CustomerID"]),
                            CustomerName = row["CustomerName"]?.ToString() ?? string.Empty,
                            InvoiceID = row["InvoiceID"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["InvoiceID"]),
                            InvoiceNumber = row["InvoiceNumber"]?.ToString(),
                            PaymentMethod = row["PaymentMethod"].ToString(),
                            ReferenceNumber = row["ReferenceNumber"]?.ToString(),
                            Amount = Convert.ToDecimal(row["Amount"]),
                            Notes = row["Notes"]?.ToString(),
                            CreatedBy = Convert.ToInt32(row["CreatedBy"]),
                            CreatedByName = row["CreatedByName"]?.ToString() ?? string.Empty,
                            CreatedDate = Convert.ToDateTime(row["CreatedDate"])
                        });
                    }

                    dgvPayments.DataSource = _payments;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading recent payments: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void LoadPayments()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                string query = @"
            SELECT 
                p.PaymentID,
                p.PaymentNumber,
                p.PaymentDate,
                p.CustomerID,
                c.CustomerName,
                p.InvoiceID,
                i.InvoiceNumber,
                p.PaymentMethod,
                p.ReferenceNumber,
                p.Amount,
                p.Notes,
                p.CreatedBy,
                u.UserName AS CreatedByName,
                p.CreatedDate
            FROM 
                Payments p
            LEFT JOIN 
                Customer c ON p.CustomerID = c.CustomerID
            LEFT JOIN 
                Invoices i ON p.InvoiceID = i.InvoiceID
            LEFT JOIN 
                Users u ON p.CreatedBy = u.UserID";

                // Add filters based on UI selections
                List<SqlParameter> parameters = new List<SqlParameter>();

                // Date filter
                if (dtpFromDate.Checked && dtpToDate.Checked)
                {
                    query += " WHERE p.PaymentDate BETWEEN @FromDate AND @ToDate";
                    parameters.Add(new SqlParameter("@FromDate", dtpFromDate.Value));
                    parameters.Add(new SqlParameter("@ToDate", dtpToDate.Value.AddDays(1).AddSeconds(-1)));
                }

                // Customer filter
                if (cboCustomer.SelectedIndex > 0)
                {
                    query += parameters.Count == 0 ? " WHERE" : " AND";
                    query += " p.CustomerID = @CustomerID";
                    parameters.Add(new SqlParameter("@CustomerID", ((ComboboxItem)cboCustomer.SelectedItem).Value));
                }

                // Payment method filter
                if (cboPaymentMethod.SelectedIndex > 0)
                {
                    query += parameters.Count == 0 ? " WHERE" : " AND";
                    query += " p.PaymentMethod = @PaymentMethod";
                    parameters.Add(new SqlParameter("@PaymentMethod", ((ComboboxItem)cboPaymentMethod.SelectedItem).Value));
                }

                // Invoice filter
                if (cboInvoice.SelectedIndex > 0)
                {
                    query += parameters.Count == 0 ? " WHERE" : " AND";

                    int invoiceValue = Convert.ToInt32(((ComboboxItem)cboInvoice.SelectedItem).Value);
                    if (invoiceValue == -1) // Unallocated payments
                    {
                        query += " p.InvoiceID IS NULL";
                    }
                    else
                    {
                        query += " p.InvoiceID = @InvoiceID";
                        parameters.Add(new SqlParameter("@InvoiceID", invoiceValue));
                    }
                }

                // Search text - make sure it searches across multiple columns
                if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    string searchText = txtSearch.Text.Trim();
                    query += parameters.Count == 0 ? " WHERE" : " AND";
                    query += @" (
                    p.PaymentNumber LIKE @SearchText
                    OR p.ReferenceNumber LIKE @SearchText
                    OR c.CustomerName LIKE @SearchText
                    OR i.InvoiceNumber LIKE @SearchText
                    OR CAST(p.Amount AS NVARCHAR) LIKE @SearchText
                    OR p.Notes LIKE @SearchText
                )";
                    parameters.Add(new SqlParameter("@SearchText", $"%{searchText}%"));
                }

                // Add ORDER BY clause
                query += " ORDER BY p.PaymentDate DESC, p.PaymentID DESC";

                // Execute query and get results
                DataTable dataTable = ExecuteQuery(query, parameters.ToArray());

                // Convert DataTable to list of Payment objects
                _payments = new List<Payment>();

                foreach (DataRow row in dataTable.Rows)
                {
                    _payments.Add(new Payment
                    {
                        PaymentID = Convert.ToInt32(row["PaymentID"]),
                        PaymentNumber = row["PaymentNumber"].ToString(),
                        PaymentDate = Convert.ToDateTime(row["PaymentDate"]),
                        CustomerID = Convert.ToInt32(row["CustomerID"]),
                        CustomerName = row["CustomerName"]?.ToString() ?? string.Empty,
                        InvoiceID = row["InvoiceID"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["InvoiceID"]),
                        InvoiceNumber = row["InvoiceNumber"]?.ToString(),
                        PaymentMethod = row["PaymentMethod"].ToString(),
                        ReferenceNumber = row["ReferenceNumber"]?.ToString(),
                        Amount = Convert.ToDecimal(row["Amount"]),
                        Notes = row["Notes"]?.ToString(),
                        CreatedBy = Convert.ToInt32(row["CreatedBy"]),
                        CreatedByName = row["CreatedByName"]?.ToString() ?? string.Empty,
                        CreatedDate = Convert.ToDateTime(row["CreatedDate"])
                    });
                }

                // Bind to grid
                dgvPayments.DataSource = _payments;

                // Update status information if available
                if (_payments.Count > 0)
                {
                    decimal totalAmount = _payments.Sum(p => p.Amount);

                    // If you have a status label, you can display a summary like:
                    // lblStatus.Text = $"Found {_payments.Count} payments, Total: {totalAmount:C2}";

                    // You might want to add a status label to your form in the designer
                }
                else
                {
                    // If no results were found
                    MessageBox.Show("No payments match your search criteria.", "Search Results",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Optional: clear the grid or keep previous results
                    // dgvPayments.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching payments: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void ConfigureDataGridView()
        {
            // Configure DataGridView columns
            dgvPayments.AutoGenerateColumns = false;
            dgvPayments.Columns.Clear();

            // Add columns
            dgvPayments.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PaymentNumber",
                HeaderText = "Payment #",
                Width = 100
            });

            dgvPayments.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PaymentDate",
                HeaderText = "Date",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "yyyy-MM-dd",
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            });

            dgvPayments.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CustomerName",
                HeaderText = "Customer",
                Width = 150
            });

            dgvPayments.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "InvoiceNumber",
                HeaderText = "Invoice #",
                Width = 100
            });

            dgvPayments.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PaymentMethod",
                HeaderText = "Payment Method",
                Width = 120
            });

            dgvPayments.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ReferenceNumber",
                HeaderText = "Reference #",
                Width = 100
            });

            dgvPayments.Columns.Add(new DataGridViewTextBoxColumn
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

            // Add action buttons
            DataGridViewButtonColumn editButtonColumn = new DataGridViewButtonColumn
            {
                HeaderText = "",
                Text = "Edit",
                UseColumnTextForButtonValue = true,
                Width = 60
            };
            dgvPayments.Columns.Add(editButtonColumn);

            /*
            DataGridViewButtonColumn allocateButtonColumn = new DataGridViewButtonColumn
            {
                HeaderText = "",
                Text = "Allocate",
                UseColumnTextForButtonValue = true,
                Width = 70
            };
            dgvPayments.Columns.Add(allocateButtonColumn);
            */

            DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn
            {
                HeaderText = "",
                Text = "Delete",
                UseColumnTextForButtonValue = true,
                Width = 60
            };
            dgvPayments.Columns.Add(deleteButtonColumn);

            // Set up cell click handler for buttons
            dgvPayments.CellClick += DgvPayments_CellClick;
        }

        private void DgvPayments_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Handle button clicks
            if (e.RowIndex < 0 || _payments == null || e.RowIndex >= _payments.Count)
                return;

            _selectedPayment = _payments[e.RowIndex];

            // Edit button column
            if (e.ColumnIndex == dgvPayments.Columns.Count - 3)
            {
                EditPayment(_selectedPayment);
            }
            /*
            // Allocate button column (TODO: Fix this)
            else if (e.ColumnIndex == dgvPayments.Columns.Count - 2)
            {
                //AllocatePayment(_selectedPayment);
                MessageBox.Show("Not fully implemented", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            */
            // Delete button column
            else if (e.ColumnIndex == dgvPayments.Columns.Count - 1)
            {
                DeletePayment(_selectedPayment);
            }
        }
        private void EditPayment(Payment payment)
        {
            try
            {
                using (var editPaymentForm = new AddPaymentForm(payment))
                {
                    if (editPaymentForm.ShowDialog() == DialogResult.OK)
                    {
                        // Refresh the payments list
                        LoadRecentPayments(20);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error editing payment: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /*
        private void AllocatePayment(Payment payment)
        {
            try
            {
                if (payment.InvoiceID.HasValue)
                {
                    MessageBox.Show("This payment is already allocated to an invoice.",
                        "Allocate Payment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Show the allocation form - create it with a fully qualified name
                using (var allocateForm = new AllocatePaymentForm(payment))
                {
                    if (allocateForm.ShowDialog() == DialogResult.OK)
                    {
                        // Refresh the payments list
                        LoadRecentPayments(20);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening allocation form: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        */

        private void DeletePayment(Payment payment)
        {
            var result = MessageBox.Show($"Are you sure you want to delete payment {payment.PaymentNumber}?",
                "Delete Payment", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Start a transaction to ensure data consistency
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        connection.Open();
                        using (SqlTransaction transaction = connection.BeginTransaction())
                        {
                            try
                            {
                                // Check if the payment is allocated to an invoice
                                if (payment.InvoiceID.HasValue)
                                {
                                    // Need to update the invoice balance first
                                    string updateInvoiceQuery = @"
                                        UPDATE Invoices 
                                        SET PaidAmount = PaidAmount - @Amount,
                                            Status = CASE 
                                                        WHEN PaidAmount - @Amount <= 0 THEN 'Open'
                                                        WHEN PaidAmount - @Amount < Amount THEN 'Partial'
                                                        ELSE Status 
                                                    END
                                        WHERE InvoiceID = @InvoiceID";

                                    using (SqlCommand cmd = new SqlCommand(updateInvoiceQuery, connection, transaction))
                                    {
                                        cmd.Parameters.AddWithValue("@Amount", payment.Amount);
                                        cmd.Parameters.AddWithValue("@InvoiceID", payment.InvoiceID.Value);
                                        cmd.ExecuteNonQuery();
                                    }
                                }

                                // Now delete the payment
                                string deleteQuery = "DELETE FROM Payments WHERE PaymentID = @PaymentID";
                                using (SqlCommand cmd = new SqlCommand(deleteQuery, connection, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@PaymentID", payment.PaymentID);
                                    cmd.ExecuteNonQuery();
                                }

                                // Commit the transaction
                                transaction.Commit();
                                MessageBox.Show("Payment deleted successfully.", "Delete Payment", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // Refresh the payments list
                                LoadRecentPayments(20);
                            }
                            catch (Exception ex)
                            {
                                // Roll back the transaction on error
                                transaction.Rollback();
                                throw new Exception($"Error during payment deletion: {ex.Message}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting payment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            // If Enter key is pressed, perform search
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Prevents the beep sound
                e.Handled = true;
                LoadPayments();
            }
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            // Reset all filters
            dtpFromDate.Value = DateTime.Today.AddMonths(-1);
            dtpToDate.Value = DateTime.Today;
            txtSearch.Text = string.Empty;
            cboCustomer.SelectedIndex = 0;
            cboPaymentMethod.SelectedIndex = 0;
            cboInvoice.SelectedIndex = 0;

            // Reload data
            LoadRecentPayments(20);
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            if (_payments == null || _payments.Count == 0)
            {
                MessageBox.Show("No payments to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SaveFileDialog saveDialog = new SaveFileDialog
            {
                Filter = "CSV Files (*.csv)|*.csv|Excel Files (*.xlsx)|*.xlsx",
                DefaultExt = "csv",
                FileName = $"Payments_{DateTime.Now:yyyyMMdd}"
            };

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;

                    string filePath = saveDialog.FileName;
                    string fileExt = System.IO.Path.GetExtension(filePath).ToLower();

                    if (fileExt == ".csv")
                    {
                        // Export to CSV
                        using (var sw = new System.IO.StreamWriter(filePath, false, Encoding.UTF8))
                        {
                            // Write header
                            sw.WriteLine("Payment Number,Date,Customer,Invoice Number,Payment Method,Reference Number,Amount,Notes");

                            // Write data rows
                            foreach (var payment in _payments)
                            {
                                sw.WriteLine($"\"{payment.PaymentNumber}\",\"{payment.PaymentDate:yyyy-MM-dd}\",\"{payment.CustomerName}\"," +
                                             $"\"{payment.InvoiceNumber ?? "N/A"}\",\"{payment.PaymentMethod}\",\"{payment.ReferenceNumber ?? ""}\"," +
                                             $"{payment.Amount},\"{payment.Notes?.Replace("\"", "\"\"") ?? ""}\"");
                            }
                        }

                        MessageBox.Show($"Payments exported successfully to {filePath}", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (fileExt == ".xlsx")
                    {
                        MessageBox.Show("Excel export not implemented yet.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error exporting payments: {ex.Message}", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            }
        }

        private void BtnAddPayment_Click(object sender, EventArgs e)
        {
            try
            {

                try
                {
                    using (var addPaymentForm = new AddPaymentForm())
                    {
                        if (addPaymentForm.ShowDialog() == DialogResult.OK)
                        {
                            LoadRecentPayments(20);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error adding payment: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding payment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }       
    }
}
