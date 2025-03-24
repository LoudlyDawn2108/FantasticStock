using FantasticStock.ViewModels.Financial;
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
using FantasticStock.Models.Financial;


namespace FantasticStock.Views.Financial
{
    public partial class FinancialDashboardView : UserControl
    {
        //private FinancialDashboardViewModel _viewModel;
        private const string ConnectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=FantasticStock1;Integrated Security=True;TrustServerCertificate=True";

        private BindingList<Payment> _recentPayments;
        private BindingList<Expense> _recentExpenses;
        private BindingList<FinancialDashboardView.OutstandingInvoice> _outstandingInvoices;

        // Dashboard metrics
        private decimal _totalSalesCurrentMonth;
        private decimal _totalExpensesCurrentMonth;
        private decimal _netIncomeCurrentMonth;
        private decimal _totalReceivables;
        private decimal _totalReceivablesOverdue;
        private decimal _cashBalance;

        // Date ranges
        private DateTime _startDate;
        private DateTime _endDate;

        public FinancialDashboardView()
        {
            InitializeComponent();

            // Initialize data collections
            _recentPayments = new BindingList<Payment>();
            _recentExpenses = new BindingList<Expense>();
            _outstandingInvoices = new BindingList<OutstandingInvoice>();

            // Initialize date pickers
            dtpStartDate.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dtpEndDate.Value = DateTime.Today;

            // Wire up events
            btnRefresh.Click += BtnRefresh_Click;
            dtpStartDate.ValueChanged += DateRange_ValueChanged;
            dtpEndDate.ValueChanged += DateRange_ValueChanged;

            // Configure grid views when form loads
            ConfigureDataGridViews();

            // Load initial data
            LoadDashboardData();

          
        }
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadDashboardData();
        }

        private void DateRange_ValueChanged(object sender, EventArgs e)
        {
            // Ensure end date is not before start date
            if (dtpEndDate.Value < dtpStartDate.Value)
                dtpEndDate.Value = dtpStartDate.Value;

            LoadDashboardData();
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

        private void ConfigureDataGridViews()
        {
            // Recent Payments Grid
            dgvRecentPayments.AutoGenerateColumns = false;
            dgvRecentPayments.Columns.Clear();

            dgvRecentPayments.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PaymentDate",
                HeaderText = "Date",
                Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "yyyy-MM-dd" }
            });

            dgvRecentPayments.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PaymentNumber",
                HeaderText = "Payment #",
                Width = 80
            });

            dgvRecentPayments.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CustomerName",
                HeaderText = "Customer",
                Width = 120
            });

            dgvRecentPayments.Columns.Add(new DataGridViewTextBoxColumn
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

            // Recent Expenses Grid
            dgvRecentExpenses.AutoGenerateColumns = false;
            dgvRecentExpenses.Columns.Clear();

            dgvRecentExpenses.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ExpenseDate",
                HeaderText = "Date",
                Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "yyyy-MM-dd" }
            });

            dgvRecentExpenses.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ExpenseNumber",
                HeaderText = "Expense #",
                Width = 80
            });

            dgvRecentExpenses.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "SupplierName",
                HeaderText = "Supplier",
                Width = 120
            });

            dgvRecentExpenses.Columns.Add(new DataGridViewTextBoxColumn
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

            // Outstanding Invoices Grid
            dgvOutstandingInvoices.AutoGenerateColumns = false;
            dgvOutstandingInvoices.Columns.Clear();

            dgvOutstandingInvoices.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "InvoiceNumber",
                HeaderText = "Invoice #",
                Width = 80
            });

            dgvOutstandingInvoices.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CustomerName",
                HeaderText = "Customer",
                Width = 120
            });

            dgvOutstandingInvoices.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "InvoiceDate",
                HeaderText = "Date",
                Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "yyyy-MM-dd" }
            });

            dgvOutstandingInvoices.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DueDate",
                HeaderText = "Due Date",
                Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "yyyy-MM-dd" }
            });

            dgvOutstandingInvoices.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Amount",
                HeaderText = "Total",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            dgvOutstandingInvoices.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "AmountDue",
                HeaderText = "Due",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            dgvOutstandingInvoices.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DaysOverdue",
                HeaderText = "Days Late",
                Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight }
            });
        }
        private void LoadDashboardData()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                // Update financial metrics
                LoadFinancialMetrics();

                // Load grid data
                LoadRecentPayments();
                LoadRecentExpenses();
                LoadOutstandingInvoices();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading dashboard data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void LoadFinancialMetrics()
        {
            DateTime startDate = dtpStartDate.Value;
            DateTime endDate = dtpEndDate.Value;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                // Total Sales for current period
                string salesQuery = @"
                    SELECT COALESCE(SUM(i.Amount), 0) AS TotalSales
                    FROM Invoices i
                    WHERE i.InvoiceDate BETWEEN @StartDate AND @EndDate";

                using (SqlCommand command = new SqlCommand(salesQuery, connection))
                {
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);

                    _totalSalesCurrentMonth = Convert.ToDecimal(command.ExecuteScalar() ?? 0m);
                    lblTotalSalesValue.Text = _totalSalesCurrentMonth.ToString("C2");
                }

                // Total Expenses for current period
                string expensesQuery = @"
                    SELECT COALESCE(SUM(e.Amount + e.TaxAmount), 0) AS TotalExpenses
                    FROM Expenses e
                    WHERE e.ExpenseDate BETWEEN @StartDate AND @EndDate";

                using (SqlCommand command = new SqlCommand(expensesQuery, connection))
                {
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);

                    _totalExpensesCurrentMonth = Convert.ToDecimal(command.ExecuteScalar() ?? 0m);
                    lblTotalExpensesValue.Text = _totalExpensesCurrentMonth.ToString("C2");
                }

                // Calculate Net Income
                _netIncomeCurrentMonth = _totalSalesCurrentMonth - _totalExpensesCurrentMonth;
                lblNetIncomeValue.Text = _netIncomeCurrentMonth.ToString("C2");

                // Total Receivables
                string receivablesQuery = @"
                    SELECT COALESCE(SUM(i.Amount - i.PaidAmount), 0) AS TotalReceivables
                    FROM Invoices i
                    WHERE i.Status <> 'Paid'";

                using (SqlCommand command = new SqlCommand(receivablesQuery, connection))
                {
                    _totalReceivables = Convert.ToDecimal(command.ExecuteScalar() ?? 0m);
                    lblTotalReceivablesValue.Text = _totalReceivables.ToString("C2");
                }

                // Overdue Receivables
                string overdueQuery = @"
                    SELECT COALESCE(SUM(i.Amount - i.PaidAmount), 0) AS OverdueReceivables
                    FROM Invoices i
                    WHERE i.DueDate < GETDATE() AND i.Status <> 'Paid'";

                using (SqlCommand command = new SqlCommand(overdueQuery, connection))
                {
                    _totalReceivablesOverdue = Convert.ToDecimal(command.ExecuteScalar() ?? 0m);
                    lblOverdueReceivablesValue.Text = _totalReceivablesOverdue.ToString("C2");
                }

                // Cash Balance (estimated)
                string cashQuery = @"
                    SELECT 
                        (SELECT COALESCE(SUM(Amount), 0) FROM Payments) - 
                        (SELECT COALESCE(SUM(Amount + TaxAmount), 0) FROM Expenses)
                    AS CashBalance";

                using (SqlCommand command = new SqlCommand(cashQuery, connection))
                {
                    _cashBalance = Convert.ToDecimal(command.ExecuteScalar() ?? 0m);
                    lblCashBalanceValue.Text = _cashBalance.ToString("C2");
                }
            }

            // Update display colors based on values
            UpdateDisplayColors();
        }

        private void UpdateDisplayColors()
        {
            // Set net income color based on positive/negative
            lblNetIncomeValue.ForeColor = _netIncomeCurrentMonth >= 0
                ? Color.DarkGreen
                : Color.DarkRed;

            // Set overdue receivables color if significant
            lblOverdueReceivablesValue.ForeColor =
                _totalReceivablesOverdue > (_totalReceivables * 0.3m)
                ? Color.DarkRed
                : SystemColors.ControlText;

            // Set cash balance color based on positive/negative
            lblCashBalanceValue.ForeColor = _cashBalance >= 0
                ? Color.DarkGreen
                : Color.DarkRed;
        }

        private void LoadRecentPayments()
        {
            string query = @"
                SELECT TOP 10
                    p.PaymentID,
                    p.PaymentNumber,
                    p.PaymentDate,
                    p.CustomerID,
                    c.CustomerName,
                    p.Amount
                FROM 
                    Payments p
                LEFT JOIN 
                    Customer c ON p.CustomerID = c.CustomerID
                ORDER BY 
                    p.PaymentDate DESC, p.PaymentID DESC";

            DataTable paymentsTable = ExecuteQuery(query);

            // Clear the existing list
            _recentPayments.Clear();

            // Add items from the query result
            foreach (DataRow row in paymentsTable.Rows)
            {
                _recentPayments.Add(new Payment
                {
                    PaymentID = Convert.ToInt32(row["PaymentID"]),
                    PaymentNumber = row["PaymentNumber"].ToString(),
                    PaymentDate = Convert.ToDateTime(row["PaymentDate"]),
                    CustomerID = Convert.ToInt32(row["CustomerID"]),
                    CustomerName = row["CustomerName"].ToString(),
                    Amount = Convert.ToDecimal(row["Amount"])
                });
            }

            // Set the data source
            dgvRecentPayments.DataSource = _recentPayments;
        }

        private void LoadRecentExpenses()
        {
            string query = @"
        SELECT TOP 10
            e.ExpenseID,
            e.ExpenseNumber,
            e.ExpenseDate,
            e.SupplierID,
            s.SupplierName,
            e.Amount,
            e.TaxAmount,
            e.Amount + e.TaxAmount AS TotalAmount
        FROM Expenses e
        LEFT JOIN Supplier s ON e.SupplierID = s.SupplierID
        ORDER BY e.ExpenseDate DESC, e.ExpenseID DESC";

            DataTable expensesData = ExecuteQuery(query);

            // Clear the existing list
            _recentExpenses.Clear();

            // Add items from the query result
            foreach (DataRow row in expensesData.Rows)
            {
                _recentExpenses.Add(new Expense
                {
                    ExpenseID = Convert.ToInt32(row["ExpenseID"]),
                    ExpenseNumber = row["ExpenseNumber"].ToString(),
                    ExpenseDate = Convert.ToDateTime(row["ExpenseDate"]),
                    SupplierID = row["SupplierID"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["SupplierID"]),
                    SupplierName = row["SupplierName"].ToString(),
                    Amount = Convert.ToDecimal(row["Amount"]),
                    TaxAmount = Convert.ToDecimal(row["TaxAmount"]),
                    //TotalAmount = Convert.ToDecimal(row["TotalAmount"])
                });
            }

            // Set the data source
            dgvRecentExpenses.DataSource = _recentExpenses;
        }

        private void LoadOutstandingInvoices()
        {
            string query = @"
        SELECT
            i.InvoiceID,
            i.InvoiceNumber,
            i.CustomerID,
            c.CustomerName,
            i.InvoiceDate,
            i.DueDate,
            i.Amount,
            i.PaidAmount,
            i.Amount - i.PaidAmount AS AmountDue,
            CASE 
                WHEN i.DueDate < GETDATE() THEN DATEDIFF(day, i.DueDate, GETDATE())
                ELSE 0
            END AS DaysOverdue
        FROM Invoices i
        LEFT JOIN Customer c ON i.CustomerID = c.CustomerID
        WHERE i.Status <> 'Paid'
        ORDER BY i.DueDate ASC";

            DataTable invoicesData = ExecuteQuery(query);

            // Clear the existing list
            _outstandingInvoices.Clear();

            // Add items from the query result
            foreach (DataRow row in invoicesData.Rows)
            {
                _outstandingInvoices.Add(new OutstandingInvoice
                {
                    InvoiceID = Convert.ToInt32(row["InvoiceID"]),
                    InvoiceNumber = row["InvoiceNumber"].ToString(),
                    CustomerID = Convert.ToInt32(row["CustomerID"]),
                    CustomerName = row["CustomerName"].ToString(),
                    InvoiceDate = Convert.ToDateTime(row["InvoiceDate"]),
                    DueDate = Convert.ToDateTime(row["DueDate"]),
                    Amount = Convert.ToDecimal(row["Amount"]),
                    PaidAmount = Convert.ToDecimal(row["PaidAmount"]),
                    AmountDue = Convert.ToDecimal(row["AmountDue"]),
                    DaysOverdue = Convert.ToInt32(row["DaysOverdue"])
                });
            }

            // Set the data source
            dgvOutstandingInvoices.DataSource = _outstandingInvoices;
        }   

        // Define OutstandingInvoice class inside the UserControl
        public class OutstandingInvoice
        {
            public int InvoiceID { get; set; }
            public string InvoiceNumber { get; set; }
            public int CustomerID { get; set; }
            public string CustomerName { get; set; }
            public DateTime InvoiceDate { get; set; }
            public DateTime DueDate { get; set; }
            public decimal Amount { get; set; }
            public decimal PaidAmount { get; set; }
            public decimal AmountDue { get; set; }
            public int DaysOverdue { get; set; }
        }
    }
}
