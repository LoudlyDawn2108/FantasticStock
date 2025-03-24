using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FantasticStock.Models.Financial;

namespace FantasticStock.Views.Financial
{
    public partial class InvoicesView : UserControl
    {
        /*
        private List<Invoice> _invoices;
        private Invoice _selectedInvoice;
        private const string ConnectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=FantasticStock;Integrated Security=True;TrustServerCertificate=True";

        public InvoicesView()
        {
            InitializeComponent();

            // Wire up event handlers
            this.Load += InvoicesView_Load;
            btnAddInvoice.Click += BtnAddInvoice_Click;
            btnSearch.Click += BtnSearch_Click;
            btnReset.Click += BtnReset_Click;
            btnExport.Click += BtnExport_Click;

            // Add event handler for grid cell clicks
            dgvInvoices.CellClick += DgvInvoices_CellClick;

            // Setup date filters with default values
            dtpFromDate.Value = DateTime.Today.AddMonths(-1);
            dtpToDate.Value = DateTime.Today;
        }

        private void InvoicesView_Load(object sender, EventArgs e)
        {
            ConfigureDataGridView();
            LoadCustomers();
            LoadStatuses();
            LoadRecentInvoices(20);
        }

        #region Loading Data

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

        private void LoadStatuses()
        {
            cboStatus.Items.Clear();
            cboStatus.Items.Add(new ComboboxItem { Text = "All Statuses", Value = "All" });
            cboStatus.Items.Add(new ComboboxItem { Text = "Open", Value = "Open" });
            cboStatus.Items.Add(new ComboboxItem { Text = "Partial", Value = "Partial" });
            cboStatus.Items.Add(new ComboboxItem { Text = "Paid", Value = "Paid" });
            cboStatus.Items.Add(new ComboboxItem { Text = "Canceled", Value = "Canceled" });
            cboStatus.Items.Add(new ComboboxItem { Text = "Overdue", Value = "Overdue" });
            cboStatus.SelectedIndex = 0;
        }

        private void LoadRecentInvoices(int limit)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                string query = @"
                SELECT TOP (@Limit)
                    i.InvoiceID,
                    i.InvoiceNumber,
                    i.InvoiceDate,
                    i.DueDate,
                    i.CustomerID,
                    c.CustomerName,
                    i.Amount,
                    i.PaidAmount,
                    i.Status,
                    i.Description,
                    i.Notes,
                    i.CreatedBy,
                    u.UserName AS CreatedByName,
                    i.CreatedDate,
                    CASE WHEN i.DueDate < GETDATE() AND i.Status NOT IN ('Paid', 'Canceled') THEN 1 ELSE 0 END AS IsOverdue,
                    CASE WHEN i.DueDate < GETDATE() THEN DATEDIFF(day, i.DueDate, GETDATE()) ELSE 0 END AS DaysOverdue
                FROM 
                    Invoices i
                LEFT JOIN 
                    Customers c ON i.CustomerID = c.CustomerID
                LEFT JOIN 
                    Users u ON i.CreatedBy = u.UserID
                ORDER BY 
                    i.InvoiceDate DESC, i.InvoiceID DESC";

                SqlParameter[] parameters = { new SqlParameter("@Limit", limit) };
                DataTable dataTable = ExecuteQuery(query, parameters);

                LoadInvoicesFromDataTable(dataTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading invoices: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void LoadInvoices()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                string query = @"
                SELECT 
                    i.InvoiceID,
                    i.InvoiceNumber,
                    i.InvoiceDate,
                    i.DueDate,
                    i.CustomerID,
                    c.CustomerName,
                    i.Amount,
                    i.PaidAmount,
                    i.Status,
                    i.Description,
                    i.Notes,
                    i.CreatedBy,
                    u.UserName AS CreatedByName,
                    i.CreatedDate,
                    CASE WHEN i.DueDate < GETDATE() AND i.Status NOT IN ('Paid', 'Canceled') THEN 1 ELSE 0 END AS IsOverdue,
                    CASE WHEN i.DueDate < GETDATE() THEN DATEDIFF(day, i.DueDate, GETDATE()) ELSE 0 END AS DaysOverdue
                FROM 
                    Invoices i
                LEFT JOIN 
                    Customers c ON i.CustomerID = c.CustomerID
                LEFT JOIN 
                    Users u ON i.CreatedBy = u.UserID";

                // Add filters based on UI selections
                List<SqlParameter> parameters = new List<SqlParameter>();

                // Date filter
                if (dtpFromDate.Checked && dtpToDate.Checked)
                {
                    query += " WHERE i.InvoiceDate BETWEEN @FromDate AND @ToDate";
                    parameters.Add(new SqlParameter("@FromDate", dtpFromDate.Value));
                    parameters.Add(new SqlParameter("@ToDate", dtpToDate.Value.AddDays(1).AddSeconds(-1)));
                }

                // Customer filter
                if (cboCustomer.SelectedIndex > 0)
                {
                    query += parameters.Count == 0 ? " WHERE" : " AND";
                    query += " i.CustomerID = @CustomerID";
                    parameters.Add(new SqlParameter("@CustomerID", ((ComboboxItem)cboCustomer.SelectedItem).Value));
                }

                // Status filter
                if (cboStatus.SelectedIndex > 0)
                {
                    string statusValue = ((ComboboxItem)cboStatus.SelectedItem).Value.ToString();

                    // Special handling for Overdue status
                    if (statusValue == "Overdue")
                    {
                        query += parameters.Count == 0 ? " WHERE" : " AND";
                        query += " i.DueDate < GETDATE() AND i.Status NOT IN ('Paid', 'Canceled')";
                    }
                    else
                    {
                        query += parameters.Count == 0 ? " WHERE" : " AND";
                        query += " i.Status = @Status";
                        parameters.Add(new SqlParameter("@Status", statusValue));
                    }
                }

                // Search text
                if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    query += parameters.Count == 0 ? " WHERE" : " AND";
                    query += @" (
                        i.InvoiceNumber LIKE @SearchText
                        OR c.CustomerName LIKE @SearchText
                        OR i.Description LIKE @SearchText
                        OR i.Notes LIKE @SearchText
                        OR CAST(i.Amount AS NVARCHAR) LIKE @SearchText
                    )";
                    parameters.Add(new SqlParameter("@SearchText", $"%{txtSearch.Text.Trim()}%"));
                }

                // Add ORDER BY clause
                query += " ORDER BY i.InvoiceDate DESC, i.InvoiceID DESC";

                // Execute the query
                DataTable dataTable = ExecuteQuery(query, parameters.ToArray());
                LoadInvoicesFromDataTable(dataTable);

                // Update status information
                if (_invoices.Count > 0)
                {
                    decimal totalAmount = _invoices.Sum(i => i.Amount);
                    decimal totalPaid = _invoices.Sum(i => i.PaidAmount);
                    decimal totalDue = totalAmount - totalPaid;

                    lblStatus.Text = $"Found {_invoices.Count} invoice(s) | Total: {totalAmount:C} | Paid: {totalPaid:C} | Due: {totalDue:C}";
                }
                else
                {
                    MessageBox.Show("No invoices found matching your criteria.", "No Results",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblStatus.Text = "No invoices found";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching invoices: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void LoadInvoicesFromDataTable(DataTable dataTable)
        {
            _invoices = new List<Invoice>();

            foreach (DataRow row in dataTable.Rows)
            {
                _invoices.Add(new Invoice
                {
                    InvoiceID = Convert.ToInt32(row["InvoiceID"]),
                    InvoiceNumber = row["InvoiceNumber"].ToString(),
                    InvoiceDate = Convert.ToDateTime(row["InvoiceDate"]),
                    DueDate = Convert.ToDateTime(row["DueDate"]),
                    CustomerID = Convert.ToInt32(row["CustomerID"]),
                    CustomerName = row["CustomerName"]?.ToString() ?? string.Empty,
                    Amount = Convert.ToDecimal(row["Amount"]),
                    PaidAmount = Convert.ToDecimal(row["PaidAmount"]),
                    Status = row["Status"].ToString(),
                    Description = row["Description"]?.ToString(),
                    Notes = row["Notes"]?.ToString(),
                    CreatedBy = Convert.ToInt32(row["CreatedBy"]),
                    CreatedByName = row["CreatedByName"]?.ToString() ?? string.Empty,
                    CreatedDate = Convert.ToDateTime(row["CreatedDate"]),
                    Balance = Convert.ToDecimal(row["Amount"]) - Convert.ToDecimal(row["PaidAmount"]),
                    IsOverdue = Convert.ToBoolean(row["IsOverdue"]),
                    DaysOverdue = Convert.ToInt32(row["DaysOverdue"])
                });
            }

            dgvInvoices.DataSource = _invoices;
        }

        #endregion

        #region UI Helpers

        private void ConfigureDataGridView()
        {
            // Configure DataGridView columns
            dgvInvoices.AutoGenerateColumns = false;
            dgvInvoices.Columns.Clear();

            // Add columns
            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "InvoiceNumber",
                HeaderText = "Invoice #",
                Width = 100
            });

            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "InvoiceDate",
                HeaderText = "Date",
                Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "yyyy-MM-dd",
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            });

            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DueDate",
                HeaderText = "Due Date",
                Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "yyyy-MM-dd",
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            });

            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CustomerName",
                HeaderText = "Customer",
                Width = 150
            });

            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Amount",
                HeaderText = "Amount",
                Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PaidAmount",
                HeaderText = "Paid",
                Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Balance",
                HeaderText = "Balance",
                Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Status",
                HeaderText = "Status",
                Width = 70
            });

            dgvInvoices.Columns.Add(new DataGridViewCheckBoxColumn
            {
                DataPropertyName = "IsOverdue",
                HeaderText = "Overdue",
                Width = 60,
                ReadOnly = true
            });

            // Add action buttons
            DataGridViewButtonColumn viewButtonColumn = new DataGridViewButtonColumn
            {
                HeaderText = "",
                Text = "View",
                UseColumnTextForButtonValue = true,
                Width = 60
            };
            dgvInvoices.Columns.Add(viewButtonColumn);

            DataGridViewButtonColumn editButtonColumn = new DataGridViewButtonColumn
            {
                HeaderText = "",
                Text = "Edit",
                UseColumnTextForButtonValue = true,
                Width = 60
            };
            dgvInvoices.Columns.Add(editButtonColumn);

            DataGridViewButtonColumn payButtonColumn = new DataGridViewButtonColumn
            {
                HeaderText = "",
                Text = "Pay",
                UseColumnTextForButtonValue = true,
                Width = 60
            };
            dgvInvoices.Columns.Add(payButtonColumn);

            // Set row formatting to highlight overdue invoices
            dgvInvoices.RowPrePaint += DgvInvoices_RowPrePaint;
        }

        private void DgvInvoices_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < _invoices.Count)
            {
                Invoice invoice = _invoices[e.RowIndex];

                if (invoice.IsOverdue)
                {
                    dgvInvoices.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.MistyRose;
                    dgvInvoices.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                }
                else if (invoice.Status == "Paid")
                {
                    dgvInvoices.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Honeydew;
                }
            }
        }

        #endregion

        #region Event Handlers

        private void BtnAddInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                using (var addInvoiceForm = new AddInvoiceForm())
                {
                    if (addInvoiceForm.ShowDialog() == DialogResult.OK)
                    {
                        LoadRecentInvoices(20);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding invoice: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            LoadInvoices();
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            // Reset all filters
            dtpFromDate.Value = DateTime.Today.AddMonths(-1);
            dtpToDate.Value = DateTime.Today;
            txtSearch.Clear();
            cboCustomer.SelectedIndex = 0;
            cboStatus.SelectedIndex = 0;

            // Reload data
            LoadRecentInvoices(20);
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            if (_invoices == null || _invoices.Count == 0)
            {
                MessageBox.Show("No invoices to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SaveFileDialog saveDialog = new SaveFileDialog
            {
                Filter = "CSV Files (*.csv)|*.csv|Excel Files (*.xlsx)|*.xlsx",
                DefaultExt = "csv",
                FileName = $"Invoices_{DateTime.Now:yyyyMMdd}"
            };

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;

                    string filePath = saveDialog.FileName;
                    string fileExt = Path.GetExtension(filePath).ToLower();

                    if (fileExt == ".csv")
                    {
                        // Export to CSV
                        using (var sw = new StreamWriter(filePath, false, Encoding.UTF8))
                        {
                            // Write header
                            sw.WriteLine("Invoice Number,Date,Due Date,Customer,Amount,Paid Amount,Balance,Status,Overdue,Days Overdue");

                            // Write data rows
                            foreach (var invoice in _invoices)
                            {
                                sw.WriteLine($"\"{invoice.InvoiceNumber}\",\"{invoice.InvoiceDate:yyyy-MM-dd}\"," +
                                    $"\"{invoice.DueDate:yyyy-MM-dd}\",\"{invoice.CustomerName}\"," +
                                    $"{invoice.Amount},{invoice.PaidAmount},{invoice.Balance},\"{invoice.Status}\"," +
                                    $"\"{(invoice.IsOverdue ? "Yes" : "No")}\",{invoice.DaysOverdue}");
                            }
                        }

                        MessageBox.Show($"Invoices exported successfully to {filePath}", "Export",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (fileExt == ".xlsx")
                    {
                        MessageBox.Show("Excel export not implemented yet.", "Export",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error exporting invoices: {ex.Message}", "Export Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            }
        }

        private void DgvInvoices_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Handle button clicks
            if (e.RowIndex < 0 || _invoices == null || e.RowIndex >= _invoices.Count)
                return;

            _selectedInvoice = _invoices[e.RowIndex];

            // View button column
            if (e.ColumnIndex == dgvInvoices.Columns.Count - 3)
            {
                ViewInvoice(_selectedInvoice);
            }
            // Edit button column
            else if (e.ColumnIndex == dgvInvoices.Columns.Count - 2)
            {
                EditInvoice(_selectedInvoice);
            }
            // Pay button column
            else if (e.ColumnIndex == dgvInvoices.Columns.Count - 1)
            {
                PayInvoice(_selectedInvoice);
            }
        }

        private void ViewInvoice(Invoice invoice)
        {
            MessageBox.Show($"Invoice: {invoice.InvoiceNumber}\n" +
                           $"Date: {invoice.InvoiceDate:yyyy-MM-dd}\n" +
                           $"Customer: {invoice.CustomerName}\n" +
                           $"Amount: {invoice.Amount:C}\n" +
                           $"Paid: {invoice.PaidAmount:C}\n" +
                           $"Balance: {invoice.Balance:C}\n" +
                           $"Status: {invoice.Status}\n" +
                           $"{(invoice.IsOverdue ? $"OVERDUE by {invoice.DaysOverdue} days" : "")}",
                "Invoice Details", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // In a real implementation, you would show a proper invoice view form
        }

        private void EditInvoice(Invoice invoice)
        {
            try
            {
                using (var editInvoiceForm = new AddInvoiceForm(invoice))
                {
                    if (editInvoiceForm.ShowDialog() == DialogResult.OK)
                    {
                        LoadRecentInvoices(20);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error editing invoice: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PayInvoice(Invoice invoice)
        {
            // Open the AddPaymentForm pre-filled with the invoice info
            try
            {
                using (var addPaymentForm = new AddPaymentForm(invoice))
                {
                    if (addPaymentForm.ShowDialog() == DialogResult.OK)
                    {
                        LoadRecentInvoices(20);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating payment: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Helpers

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

        // ComboboxItem class for dropdown selections
        private class ComboboxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }

        #endregion
        */
    }
}
