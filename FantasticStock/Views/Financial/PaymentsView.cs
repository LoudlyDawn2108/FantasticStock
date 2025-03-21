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
        private const string ConnectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=FantasticStock;Integrated Security=True;TrustServerCertificate=True";

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
            dgvPayments.CellClick += DgvPayments_CellClick;
            //dgvPayments.CellDoubleClick += DgvPayments_CellDoubleClick;
            //txtSearch.KeyDown += TxtSearch_KeyDown;
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
            LoadPayments(); // Now this will only run when user clicks search
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

                string query = "SELECT CustomerID, CustomerName FROM Customers WHERE IsActive = 1 ORDER BY CustomerName";
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
            Customers c ON p.CustomerID = c.CustomerID
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
                Customers c ON p.CustomerID = c.CustomerID
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

                // Search text
                if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    query += parameters.Count == 0 ? " WHERE" : " AND";
                    query += @" (
                p.PaymentNumber LIKE @SearchText
                OR p.ReferenceNumber LIKE @SearchText
                OR c.CustomerName LIKE @SearchText
                OR i.InvoiceNumber LIKE @SearchText
                OR p.Notes LIKE @SearchText
            )";
                    parameters.Add(new SqlParameter("@SearchText", $"%{txtSearch.Text.Trim()}%"));
                }

                // Add ORDER BY clause
                query += " ORDER BY p.PaymentDate DESC, p.PaymentID DESC";

                // Execute query and get results
                DataTable dataTable = ExecuteQuery(query, parameters.ToArray());

                if (dataTable.Rows.Count > 0)
                {
                    // Debug information
                    MessageBox.Show($"Query found {dataTable.Rows.Count} payments with joins",
                        "Query Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Display column names for debugging
                    StringBuilder columnInfo = new StringBuilder();
                    foreach (DataColumn col in dataTable.Columns)
                    {
                        columnInfo.AppendLine($"- {col.ColumnName}");
                    }
                    MessageBox.Show($"DataTable columns:\n{columnInfo}", "Column Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Bind directly to the DataGridView
                    dgvPayments.DataSource = dataTable;

                    // Update status information if needed
                    decimal totalAmount = 0;
                    foreach (DataRow row in dataTable.Rows)
                    {
                        totalAmount += Convert.ToDecimal(row["Amount"]);
                    }
                    // lblTotalAmount.Text = $"Total: {dataTable.Rows.Count} payment(s), {totalAmount:C}";

                    MessageBox.Show($"Successfully bound {dataTable.Rows.Count} payments to grid",
                        "Binding Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No payments match your search criteria.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvPayments.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading payments: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}",
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

            DataGridViewButtonColumn allocateButtonColumn = new DataGridViewButtonColumn
            {
                HeaderText = "",
                Text = "Allocate",
                UseColumnTextForButtonValue = true,
                Width = 70
            };
            dgvPayments.Columns.Add(allocateButtonColumn);

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
            // Allocate button column
            else if (e.ColumnIndex == dgvPayments.Columns.Count - 2)
            {
                AllocatePayment(_selectedPayment);
            }
            // Delete button column
            else if (e.ColumnIndex == dgvPayments.Columns.Count - 1)
            {
                DeletePayment(_selectedPayment);
            }
        }
        private void EditPayment(Payment payment)
        {
            MessageBox.Show($"Edit payment {payment.PaymentNumber}: {payment.Amount:C}", "Edit Payment", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // In a real app, you would open a payment edit form here
        }

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
                                LoadPayments();
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

        private class AddPaymentForm : Form
        {
            private TextBox txtPaymentNumber;
            private DateTimePicker dtpPaymentDate;
            private ComboBox cboCustomer;
            private ComboBox cboInvoice;
            private ComboBox cboPaymentMethod;
            private TextBox txtReferenceNumber;
            private TextBox txtAmount;
            private TextBox txtNotes;
            private Button btnSave;
            private Button btnCancel;

            public AddPaymentForm()
            {
                InitializeComponents();
                LoadCustomers();
                LoadInvoices();
                LoadPaymentMethods();
            }

            private void InitializeComponents()
            {
                this.Text = "Add Payment";
                this.Size = new Size(400, 380);
                this.FormBorderStyle = FormBorderStyle.FixedDialog;
                this.MaximizeBox = false;
                this.MinimizeBox = false;
                this.StartPosition = FormStartPosition.CenterParent;

                // Create labels
                var lblPaymentNumber = new Label { Text = "Payment Number:", Left = 20, Top = 20, Width = 110 };
                var lblPaymentDate = new Label { Text = "Payment Date:", Left = 20, Top = 50, Width = 110 };
                var lblCustomer = new Label { Text = "Customer:", Left = 20, Top = 80, Width = 110 };
                var lblInvoice = new Label { Text = "Invoice:", Left = 20, Top = 110, Width = 110 };
                var lblPaymentMethod = new Label { Text = "Payment Method:", Left = 20, Top = 140, Width = 110 };
                var lblReferenceNumber = new Label { Text = "Reference #:", Left = 20, Top = 170, Width = 110 };
                var lblAmount = new Label { Text = "Amount:", Left = 20, Top = 200, Width = 110 };
                var lblNotes = new Label { Text = "Notes:", Left = 20, Top = 230, Width = 110 };

                // Create controls
                txtPaymentNumber = new TextBox { Left = 130, Top = 20, Width = 220 };
                dtpPaymentDate = new DateTimePicker { Left = 130, Top = 50, Width = 220 };
                cboCustomer = new ComboBox { Left = 130, Top = 80, Width = 220, DropDownStyle = ComboBoxStyle.DropDownList };
                cboInvoice = new ComboBox { Left = 130, Top = 110, Width = 220, DropDownStyle = ComboBoxStyle.DropDownList };
                cboPaymentMethod = new ComboBox { Left = 130, Top = 140, Width = 220, DropDownStyle = ComboBoxStyle.DropDownList };
                txtReferenceNumber = new TextBox { Left = 130, Top = 170, Width = 220 };
                txtAmount = new TextBox { Left = 130, Top = 200, Width = 220 };
                txtNotes = new TextBox { Left = 130, Top = 230, Width = 220, Height = 60, Multiline = true };

                // Create buttons
                btnSave = new Button { Text = "Save", Left = 130, Top = 300, Width = 100, DialogResult = DialogResult.OK };
                btnCancel = new Button { Text = "Cancel", Left = 250, Top = 300, Width = 100, DialogResult = DialogResult.Cancel };

                // Set default values
                dtpPaymentDate.Value = DateTime.Today;

                // Generate a payment number (e.g., PMT-yyyyMMdd-001)
                txtPaymentNumber.Text = $"PMT-{DateTime.Now:yyyyMMdd}-{new Random().Next(1, 999):D3}";

                // Set event handlers
                btnSave.Click += BtnSave_Click;
                cboCustomer.SelectedIndexChanged += CboCustomer_SelectedIndexChanged;

                // Add controls to form
                this.Controls.AddRange(new Control[] {
            lblPaymentNumber, txtPaymentNumber,
            lblPaymentDate, dtpPaymentDate,
            lblCustomer, cboCustomer,
            lblInvoice, cboInvoice,
            lblPaymentMethod, cboPaymentMethod,
            lblReferenceNumber, txtReferenceNumber,
            lblAmount, txtAmount,
            lblNotes, txtNotes,
            btnSave, btnCancel
        });

                this.AcceptButton = btnSave;
                this.CancelButton = btnCancel;
            }

            private void LoadCustomers()
            {
                try
                {
                    string query = "SELECT CustomerID, CustomerName FROM Customers WHERE IsActive = 1 ORDER BY CustomerName";
                    DataTable customersTable = ExecuteQuery(query);

                    cboCustomer.Items.Clear();
                    cboCustomer.Items.Add(new ComboboxItem { Text = "-- Select Customer --", Value = 0 });

                    foreach (DataRow row in customersTable.Rows)
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
                    MessageBox.Show($"Error loading customers: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            private void LoadInvoices()
            {
                cboInvoice.Items.Clear();
                cboInvoice.Items.Add(new ComboboxItem { Text = "-- No Invoice (Unallocated) --", Value = 0 });
                cboInvoice.SelectedIndex = 0;
            }

            private void LoadPaymentMethods()
            {
                cboPaymentMethod.Items.Clear();
                cboPaymentMethod.Items.Add(new ComboboxItem { Text = "-- Select Payment Method --", Value = string.Empty });
                cboPaymentMethod.Items.Add(new ComboboxItem { Text = "Cash", Value = "Cash" });
                cboPaymentMethod.Items.Add(new ComboboxItem { Text = "Check", Value = "Check" });
                cboPaymentMethod.Items.Add(new ComboboxItem { Text = "Credit Card", Value = "Credit Card" });
                cboPaymentMethod.Items.Add(new ComboboxItem { Text = "Bank Transfer", Value = "Bank Transfer" });
                cboPaymentMethod.SelectedIndex = 0;
            }

            private void LoadInvoicesForCustomer(int customerId)
            {
                try
                {
                    string query = @"
                SELECT InvoiceID, InvoiceNumber, Amount, PaidAmount 
                FROM Invoices 
                WHERE CustomerID = @CustomerID AND Status <> 'Paid'
                ORDER BY InvoiceDate DESC";

                    SqlParameter[] parameters = { new SqlParameter("@CustomerID", customerId) };
                    DataTable invoicesTable = ExecuteQuery(query, parameters);

                    cboInvoice.Items.Clear();
                    cboInvoice.Items.Add(new ComboboxItem { Text = "-- No Invoice (Unallocated) --", Value = 0 });

                    foreach (DataRow row in invoicesTable.Rows)
                    {
                        decimal amount = Convert.ToDecimal(row["Amount"]);
                        decimal paidAmount = Convert.ToDecimal(row["PaidAmount"]);
                        decimal balance = amount - paidAmount;

                        cboInvoice.Items.Add(new ComboboxItem
                        {
                            Text = $"{row["InvoiceNumber"]} (Balance: {balance:C2})",
                            Value = Convert.ToInt32(row["InvoiceID"])
                        });
                    }

                    cboInvoice.SelectedIndex = 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading invoices: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            private void CboCustomer_SelectedIndexChanged(object sender, EventArgs e)
            {
                if (cboCustomer.SelectedIndex > 0)
                {
                    var selectedCustomer = (ComboboxItem)cboCustomer.SelectedItem;
                    LoadInvoicesForCustomer(Convert.ToInt32(selectedCustomer.Value));
                }
                else
                {
                    LoadInvoices();
                }
            }

            private void BtnSave_Click(object sender, EventArgs e)
            {
                if (!ValidateInput())
                    return;

                try
                {
                    // Get form values
                    string paymentNumber = txtPaymentNumber.Text.Trim();
                    DateTime paymentDate = dtpPaymentDate.Value;
                    int customerId = Convert.ToInt32(((ComboboxItem)cboCustomer.SelectedItem).Value);
                    object invoiceId;
                    if (cboInvoice.SelectedIndex > 0)
                    {
                        invoiceId = Convert.ToInt32(((ComboboxItem)cboInvoice.SelectedItem).Value);
                    }
                    else
                    {
                        invoiceId = DBNull.Value;
                    }
                    string paymentMethod = ((ComboboxItem)cboPaymentMethod.SelectedItem).Value.ToString();
                    string referenceNumber = txtReferenceNumber.Text.Trim();
                    decimal amount = decimal.Parse(txtAmount.Text);
                    string notes = txtNotes.Text.Trim();

                    // Insert payment into database
                    string query = @"
                INSERT INTO Payments (
                    PaymentNumber,
                    PaymentDate,
                    CustomerID,
                    InvoiceID,
                    PaymentMethod,
                    ReferenceNumber,
                    Amount,
                    Notes,
                    CreatedBy,
                    CreatedDate
                ) VALUES (
                    @PaymentNumber,
                    @PaymentDate,
                    @CustomerID,
                    @InvoiceID,
                    @PaymentMethod,
                    @ReferenceNumber,
                    @Amount,
                    @Notes,
                    @CreatedBy,
                    @CreatedDate
                )";

                    SqlParameter[] parameters = {
                new SqlParameter("@PaymentNumber", paymentNumber),
                new SqlParameter("@PaymentDate", paymentDate),
                new SqlParameter("@CustomerID", customerId),
                new SqlParameter("@InvoiceID", invoiceId),
                new SqlParameter("@PaymentMethod", paymentMethod),
                new SqlParameter("@ReferenceNumber", string.IsNullOrEmpty(referenceNumber) ? DBNull.Value : (object)referenceNumber),
                new SqlParameter("@Amount", amount),
                new SqlParameter("@Notes", string.IsNullOrEmpty(notes) ? DBNull.Value : (object)notes),
                new SqlParameter("@CreatedBy", 1), // Use the current user ID in a real app
                new SqlParameter("@CreatedDate", DateTime.Now)
            };

                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        connection.Open();

                        // Start a transaction to ensure data consistency
                        using (SqlTransaction transaction = connection.BeginTransaction())
                        {
                            try
                            {
                                // Insert the payment
                                using (SqlCommand command = new SqlCommand(query, connection, transaction))
                                {
                                    command.Parameters.AddRange(parameters);
                                    command.ExecuteNonQuery();
                                }

                                // If payment is allocated to an invoice, update the invoice paid amount
                                if (invoiceId != DBNull.Value && Convert.ToInt32(invoiceId) > 0)
                                {
                                    string updateInvoiceQuery = @"
                                UPDATE Invoices
                                SET PaidAmount = PaidAmount + @Amount,
                                    Status = CASE 
                                              WHEN PaidAmount + @Amount >= Amount THEN 'Paid' 
                                              ELSE 'Partial' 
                                            END
                                WHERE InvoiceID = @InvoiceID";

                                    using (SqlCommand command = new SqlCommand(updateInvoiceQuery, connection, transaction))
                                    {
                                        command.Parameters.AddWithValue("@Amount", amount);
                                        command.Parameters.AddWithValue("@InvoiceID", invoiceId);
                                        command.ExecuteNonQuery();
                                    }
                                }

                                // Commit the transaction
                                transaction.Commit();
                            }
                            catch
                            {
                                transaction.Rollback();
                                throw;
                            }
                        }
                    }

                    MessageBox.Show("Payment saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving payment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            private bool ValidateInput()
            {
                if (string.IsNullOrWhiteSpace(txtPaymentNumber.Text))
                {
                    MessageBox.Show("Please enter a payment number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPaymentNumber.Focus();
                    return false;
                }

                if (cboCustomer.SelectedIndex <= 0)
                {
                    MessageBox.Show("Please select a customer.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboCustomer.Focus();
                    return false;
                }

                if (cboPaymentMethod.SelectedIndex <= 0)
                {
                    MessageBox.Show("Please select a payment method.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboPaymentMethod.Focus();
                    return false;
                }

                if (string.IsNullOrWhiteSpace(txtAmount.Text) || !decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
                {
                    MessageBox.Show("Please enter a valid amount greater than zero.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtAmount.Focus();
                    return false;
                }

                return true;
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

            // ComboboxItem class for storing text/value pairs in comboboxes
            public class ComboboxItem
            {
                public string Text { get; set; }
                public object Value { get; set; }

                public override string ToString()
                {
                    return Text;
                }
            }
        }

        private class AllocatePaymentForm : Form
        {
            private const string ConnectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=FantasticStock;Integrated Security=True;TrustServerCertificate=True";
            private Payment _payment;
            private Label lblPaymentInfo;
            private Label lblAvailableAmount;
            private DataGridView dgvInvoices;
            private Button btnAllocate;
            private Button btnCancel;
            private decimal _availableAmount;
            private List<InvoiceAllocation> _invoices;
            private int _selectedInvoiceId;

            public AllocatePaymentForm(Payment payment)
            {
                _payment = payment;
                _availableAmount = payment.Amount;
                _selectedInvoiceId = 0;
                _invoices = new List<InvoiceAllocation>();

                InitializeComponents();
                LoadInvoices();
            }

            private void InitializeComponents()
            {
                this.Text = "Allocate Payment to Invoice";
                this.Size = new Size(600, 500);
                this.FormBorderStyle = FormBorderStyle.FixedDialog;
                this.MaximizeBox = false;
                this.MinimizeBox = false;
                this.StartPosition = FormStartPosition.CenterParent;

                // Create controls
                lblPaymentInfo = new Label
                {
                    Text = $"Payment: {_payment.PaymentNumber} | Date: {_payment.PaymentDate:yyyy-MM-dd} | " +
                          $"Customer: {_payment.CustomerName} | Amount: {_payment.Amount:C2}",
                    Left = 20,
                    Top = 20,
                    Width = 560,
                    Font = new Font(Font.FontFamily, Font.Size, FontStyle.Bold)
                };

                lblAvailableAmount = new Label
                {
                    Text = $"Available Amount: {_availableAmount:C2}",
                    Left = 20,
                    Top = 50,
                    Width = 560
                };

                dgvInvoices = new DataGridView
                {
                    Left = 20,
                    Top = 80,
                    Width = 540,
                    Height = 320,
                    AllowUserToAddRows = false,
                    AllowUserToDeleteRows = false,
                    AllowUserToResizeRows = false,
                    MultiSelect = false,
                    SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                    ReadOnly = false,
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                };

                btnAllocate = new Button
                {
                    Text = "Allocate",
                    Left = 380,
                    Top = 420,
                    Width = 80,
                    Enabled = false
                };

                btnCancel = new Button
                {
                    Text = "Cancel",
                    Left = 480,
                    Top = 420,
                    Width = 80,
                    DialogResult = DialogResult.Cancel
                };

                // Configure grid
                ConfigureDataGridView();

                // Set event handlers
                dgvInvoices.CellValueChanged += DgvInvoices_CellValueChanged;
                dgvInvoices.CellClick += DgvInvoices_CellClick;
                dgvInvoices.CurrentCellDirtyStateChanged += DgvInvoices_CurrentCellDirtyStateChanged;
                btnAllocate.Click += BtnAllocate_Click;

                // Add controls to form
                this.Controls.AddRange(new Control[] {
            lblPaymentInfo,
            lblAvailableAmount,
            dgvInvoices,
            btnAllocate,
            btnCancel
        });

                this.AcceptButton = btnAllocate;
                this.CancelButton = btnCancel;
            }

            private void ConfigureDataGridView()
            {
                dgvInvoices.AutoGenerateColumns = false;
                dgvInvoices.Columns.Clear();

                // Add columns to the grid
                dgvInvoices.Columns.Add(new DataGridViewCheckBoxColumn
                {
                    Name = "Select",
                    HeaderText = "",
                    Width = 40,
                    ReadOnly = false
                });

                dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "InvoiceNumber",
                    DataPropertyName = "InvoiceNumber",
                    HeaderText = "Invoice #",
                    ReadOnly = true,
                    Width = 100
                });

                dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "InvoiceDate",
                    DataPropertyName = "InvoiceDate",
                    HeaderText = "Date",
                    ReadOnly = true,
                    Width = 100,
                    DefaultCellStyle = new DataGridViewCellStyle { Format = "yyyy-MM-dd" }
                });

                dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "DueDate",
                    DataPropertyName = "DueDate",
                    HeaderText = "Due Date",
                    ReadOnly = true,
                    Width = 100,
                    DefaultCellStyle = new DataGridViewCellStyle { Format = "yyyy-MM-dd" }
                });

                dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Amount",
                    DataPropertyName = "Amount",
                    HeaderText = "Total",
                    ReadOnly = true,
                    Width = 100,
                    DefaultCellStyle = new DataGridViewCellStyle
                    {
                        Format = "C2",
                        Alignment = DataGridViewContentAlignment.MiddleRight
                    }
                });

                dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Balance",
                    DataPropertyName = "Balance",
                    HeaderText = "Balance",
                    ReadOnly = true,
                    Width = 100,
                    DefaultCellStyle = new DataGridViewCellStyle
                    {
                        Format = "C2",
                        Alignment = DataGridViewContentAlignment.MiddleRight
                    }
                });

                dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "AllocateAmount",
                    DataPropertyName = "AllocateAmount",
                    HeaderText = "Allocate",
                    ReadOnly = false,
                    Width = 100,
                    DefaultCellStyle = new DataGridViewCellStyle
                    {
                        Format = "C2",
                        Alignment = DataGridViewContentAlignment.MiddleRight
                    }
                });
            }

            private void LoadInvoices()
            {
                try
                {
                    string query = @"
                SELECT 
                    i.InvoiceID,
                    i.InvoiceNumber,
                    i.InvoiceDate,
                    i.DueDate,
                    i.Amount,
                    i.PaidAmount,
                    i.Amount - i.PaidAmount AS Balance
                FROM 
                    Invoices i
                WHERE 
                    i.CustomerID = @CustomerID 
                    AND i.Status <> 'Paid'
                    AND i.Amount > i.PaidAmount
                ORDER BY 
                    i.DueDate ASC";

                    SqlParameter[] parameters = { new SqlParameter("@CustomerID", _payment.CustomerID) };
                    DataTable invoicesTable = ExecuteQuery(query, parameters);

                    _invoices.Clear();
                    foreach (DataRow row in invoicesTable.Rows)
                    {
                        _invoices.Add(new InvoiceAllocation
                        {
                            InvoiceID = Convert.ToInt32(row["InvoiceID"]),
                            InvoiceNumber = row["InvoiceNumber"].ToString(),
                            InvoiceDate = Convert.ToDateTime(row["InvoiceDate"]),
                            DueDate = Convert.ToDateTime(row["DueDate"]),
                            Amount = Convert.ToDecimal(row["Amount"]),
                            PaidAmount = Convert.ToDecimal(row["PaidAmount"]),
                            Balance = Convert.ToDecimal(row["Balance"]),
                            AllocateAmount = 0m,
                            IsSelected = false
                        });
                    }

                    // Bind invoices to grid
                    dgvInvoices.DataSource = new BindingList<InvoiceAllocation>(_invoices);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading invoices: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            private void DgvInvoices_CurrentCellDirtyStateChanged(object sender, EventArgs e)
            {
                // This is needed to commit checkbox changes immediately
                if (dgvInvoices.IsCurrentCellDirty && dgvInvoices.CurrentCell.ColumnIndex == 0)
                {
                    dgvInvoices.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
            }

            private void DgvInvoices_CellClick(object sender, DataGridViewCellEventArgs e)
            {
                // Handle checkbox column clicks
                if (e.RowIndex >= 0 && e.ColumnIndex == 0)
                {
                    DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dgvInvoices.Rows[e.RowIndex].Cells[0];
                    bool isChecked = Convert.ToBoolean(cell.Value);

                    // Update the model
                    _invoices[e.RowIndex].IsSelected = isChecked;

                    // If row is selected, set allocation amount
                    if (isChecked)
                    {
                        _selectedInvoiceId = _invoices[e.RowIndex].InvoiceID;

                        // Auto-fill with either the balance or the available amount, whichever is less
                        decimal balance = _invoices[e.RowIndex].Balance;
                        decimal autoAmount = Math.Min(balance, _availableAmount);
                        _invoices[e.RowIndex].AllocateAmount = autoAmount;

                        // Update the cell
                        dgvInvoices.Rows[e.RowIndex].Cells["AllocateAmount"].Value = autoAmount;
                    }
                    else
                    {
                        // If deselected, clear allocation
                        _invoices[e.RowIndex].AllocateAmount = 0;
                        dgvInvoices.Rows[e.RowIndex].Cells["AllocateAmount"].Value = 0;

                        if (_selectedInvoiceId == _invoices[e.RowIndex].InvoiceID)
                        {
                            _selectedInvoiceId = 0;
                        }
                    }

                    // Recalculate available amount
                    UpdateAvailableAmount();

                    // Enable/disable allocate button
                    UpdateAllocateButton();
                }
            }

            private void DgvInvoices_CellValueChanged(object sender, DataGridViewCellEventArgs e)
            {
                // Handle allocation amount changes
                if (e.RowIndex >= 0 && e.ColumnIndex == dgvInvoices.Columns["AllocateAmount"].Index)
                {
                    var cell = dgvInvoices.Rows[e.RowIndex].Cells[e.ColumnIndex];

                    if (cell.Value != null && decimal.TryParse(cell.Value.ToString(), out decimal amount))
                    {
                        // Get the invoice
                        InvoiceAllocation invoice = _invoices[e.RowIndex];

                        // Validate amount
                        if (amount < 0)
                        {
                            amount = 0;
                            cell.Value = amount;
                        }
                        else if (amount > invoice.Balance)
                        {
                            amount = invoice.Balance;
                            cell.Value = amount;
                        }

                        // Update the model
                        invoice.AllocateAmount = amount;

                        // If amount is > 0, make sure the invoice is selected
                        if (amount > 0 && !invoice.IsSelected)
                        {
                            invoice.IsSelected = true;
                            dgvInvoices.Rows[e.RowIndex].Cells[0].Value = true;
                            _selectedInvoiceId = invoice.InvoiceID;
                        }
                        // If amount is 0, deselect the invoice
                        else if (amount == 0 && invoice.IsSelected)
                        {
                            invoice.IsSelected = false;
                            dgvInvoices.Rows[e.RowIndex].Cells[0].Value = false;

                            if (_selectedInvoiceId == invoice.InvoiceID)
                            {
                                _selectedInvoiceId = 0;
                            }
                        }

                        // Recalculate available amount
                        UpdateAvailableAmount();
                    }
                }

                // Update allocate button state
                UpdateAllocateButton();
            }

            private void UpdateAvailableAmount()
            {
                // Calculate total allocated
                decimal allocated = _invoices.Sum(i => i.AllocateAmount);

                // Update available amount
                _availableAmount = _payment.Amount - allocated;
                lblAvailableAmount.Text = $"Available Amount: {_availableAmount:C2}";

                // Set text color based on amount
                if (_availableAmount < 0)
                {
                    lblAvailableAmount.ForeColor = Color.Red;
                }
                else
                {
                    lblAvailableAmount.ForeColor = SystemColors.ControlText;
                }
            }

            private void UpdateAllocateButton()
            {
                // Only enable if at least one invoice is selected and the allocated amount is valid
                decimal allocated = _invoices.Sum(i => i.AllocateAmount);
                btnAllocate.Enabled = _invoices.Any(i => i.IsSelected) && allocated > 0 && allocated <= _payment.Amount;
            }

            private void BtnAllocate_Click(object sender, EventArgs e)
            {
                // Only proceed if at least one invoice is selected
                if (!_invoices.Any(i => i.IsSelected))
                {
                    MessageBox.Show("Please select at least one invoice to allocate this payment to.",
                        "No Invoice Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Get selected invoices and their allocation amounts
                var selectedInvoices = _invoices.Where(i => i.IsSelected && i.AllocateAmount > 0).ToList();
                decimal totalAllocated = selectedInvoices.Sum(i => i.AllocateAmount);

                // Validate total
                if (totalAllocated <= 0)
                {
                    MessageBox.Show("Please enter an allocation amount greater than zero.",
                        "Invalid Amount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (totalAllocated > _payment.Amount)
                {
                    MessageBox.Show($"The total allocated amount ({totalAllocated:C2}) cannot exceed the payment amount ({_payment.Amount:C2}).",
                        "Invalid Amount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Confirm allocation
                var result = MessageBox.Show(
                    $"Are you sure you want to allocate {totalAllocated:C2} to {selectedInvoices.Count} invoice(s)?",
                    "Confirm Allocation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        using (SqlConnection connection = new SqlConnection(ConnectionString))
                        {
                            connection.Open();
                            using (SqlTransaction transaction = connection.BeginTransaction())
                            {
                                try
                                {
                                    // First, update the payment with the first invoice (if multiple, we'll split the payment)
                                    var firstInvoice = selectedInvoices.First();

                                    // Update the payment record
                                    string updatePaymentQuery = "UPDATE Payments SET InvoiceID = @InvoiceID WHERE PaymentID = @PaymentID";

                                    using (SqlCommand cmd = new SqlCommand(updatePaymentQuery, connection, transaction))
                                    {
                                        cmd.Parameters.AddWithValue("@InvoiceID", firstInvoice.InvoiceID);
                                        cmd.Parameters.AddWithValue("@PaymentID", _payment.PaymentID);
                                        cmd.ExecuteNonQuery();
                                    }

                                    // Update the first invoice's paid amount
                                    string updateInvoiceQuery = @"
                            UPDATE Invoices
                            SET PaidAmount = PaidAmount + @Amount,
                                Status = CASE 
                                          WHEN PaidAmount + @Amount >= Amount THEN 'Paid' 
                                          ELSE 'Partial' 
                                        END
                            WHERE InvoiceID = @InvoiceID";

                                    using (SqlCommand cmd = new SqlCommand(updateInvoiceQuery, connection, transaction))
                                    {
                                        cmd.Parameters.AddWithValue("@Amount", firstInvoice.AllocateAmount);
                                        cmd.Parameters.AddWithValue("@InvoiceID", firstInvoice.InvoiceID);
                                        cmd.ExecuteNonQuery();
                                    }

                                    // For each additional invoice, create a new payment record (splitting the original payment)
                                    for (int i = 1; i < selectedInvoices.Count; i++)
                                    {
                                        var invoice = selectedInvoices[i];

                                        // Insert a new payment record
                                        string insertPaymentQuery = @"
                                INSERT INTO Payments (
                                    PaymentNumber,
                                    PaymentDate,
                                    CustomerID,
                                    InvoiceID,
                                    PaymentMethod,
                                    ReferenceNumber,
                                    Amount,
                                    Notes,
                                    CreatedBy,
                                    CreatedDate
                                ) VALUES (
                                    @PaymentNumber,
                                    @PaymentDate,
                                    @CustomerID,
                                    @InvoiceID,
                                    @PaymentMethod,
                                    @ReferenceNumber,
                                    @Amount,
                                    @Notes,
                                    @CreatedBy,
                                    @CreatedDate
                                )";

                                        using (SqlCommand cmd = new SqlCommand(insertPaymentQuery, connection, transaction))
                                        {
                                            cmd.Parameters.AddWithValue("@PaymentNumber", $"{_payment.PaymentNumber}-{i + 1}");
                                            cmd.Parameters.AddWithValue("@PaymentDate", _payment.PaymentDate);
                                            cmd.Parameters.AddWithValue("@CustomerID", _payment.CustomerID);
                                            cmd.Parameters.AddWithValue("@InvoiceID", invoice.InvoiceID);
                                            cmd.Parameters.AddWithValue("@PaymentMethod", _payment.PaymentMethod);
                                            cmd.Parameters.AddWithValue("@ReferenceNumber",
                                                string.IsNullOrEmpty(_payment.ReferenceNumber) ? DBNull.Value : (object)_payment.ReferenceNumber);
                                            cmd.Parameters.AddWithValue("@Amount", invoice.AllocateAmount);
                                            cmd.Parameters.AddWithValue("@Notes",
                                                string.IsNullOrEmpty(_payment.Notes) ? $"Split from payment {_payment.PaymentNumber}" :
                                                $"{_payment.Notes} | Split from payment {_payment.PaymentNumber}");
                                            cmd.Parameters.AddWithValue("@CreatedBy", 1); // Use current user ID in a real app
                                            cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                                            cmd.ExecuteNonQuery();
                                        }

                                        // Update this invoice's paid amount
                                        using (SqlCommand cmd = new SqlCommand(updateInvoiceQuery, connection, transaction))
                                        {
                                            cmd.Parameters.AddWithValue("@Amount", invoice.AllocateAmount);
                                            cmd.Parameters.AddWithValue("@InvoiceID", invoice.InvoiceID);
                                            cmd.ExecuteNonQuery();
                                        }
                                    }

                                    // Update the original payment amount to reflect the first allocation if we've split the payment
                                    if (selectedInvoices.Count > 1)
                                    {
                                        string updateAmountQuery = "UPDATE Payments SET Amount = @Amount WHERE PaymentID = @PaymentID";

                                        using (SqlCommand cmd = new SqlCommand(updateAmountQuery, connection, transaction))
                                        {
                                            cmd.Parameters.AddWithValue("@Amount", firstInvoice.AllocateAmount);
                                            cmd.Parameters.AddWithValue("@PaymentID", _payment.PaymentID);
                                            cmd.ExecuteNonQuery();
                                        }
                                    }

                                    // Commit the transaction
                                    transaction.Commit();

                                    MessageBox.Show("Payment allocated successfully!", "Success",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    // Set dialog result and close
                                    this.DialogResult = DialogResult.OK;
                                    this.Close();
                                }
                                catch (Exception ex)
                                {
                                    // Roll back the transaction on error
                                    transaction.Rollback();
                                    throw new Exception($"Error during payment allocation: {ex.Message}");
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error allocating payment: {ex.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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

            // Class to store invoice allocation data
            public class InvoiceAllocation
            {
                public int InvoiceID { get; set; }
                public string InvoiceNumber { get; set; }
                public DateTime InvoiceDate { get; set; }
                public DateTime DueDate { get; set; }
                public decimal Amount { get; set; }
                public decimal PaidAmount { get; set; }
                public decimal Balance { get; set; }
                public decimal AllocateAmount { get; set; }
                public bool IsSelected { get; set; }
            }
        }
    }
}
