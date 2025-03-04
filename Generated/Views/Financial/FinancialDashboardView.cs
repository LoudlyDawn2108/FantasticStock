using System;
using System.Windows.Forms;
using AdminDomain.ViewModels.Financial;

namespace AdminDomain.Views.Financial
{
    public partial class FinancialDashboardView : UserControl
    {
        private FinancialDashboardViewModel _viewModel;
        
        public FinancialDashboardView()
        {
            InitializeComponent();
            
            // Initialize view model
            _viewModel = new FinancialDashboardViewModel();
            
            // Set up data bindings
            SetupBindings();
        }
        
        private void SetupBindings()
        {
            // Bind date range
            dtpStartDate.DataBindings.Add("Value", _viewModel, "StartDate", true, DataSourceUpdateMode.OnPropertyChanged);
            dtpEndDate.DataBindings.Add("Value", _viewModel, "EndDate", true, DataSourceUpdateMode.OnPropertyChanged);
            
            // Bind metric labels
            lblTotalSalesValue.DataBindings.Add("Text", _viewModel, "FormattedTotalSalesCurrentMonth", true);
            lblTotalExpensesValue.DataBindings.Add("Text", _viewModel, "FormattedTotalExpensesCurrentMonth", true);
            lblNetIncomeValue.DataBindings.Add("Text", _viewModel, "FormattedNetIncomeCurrentMonth", true);
            lblTotalReceivablesValue.DataBindings.Add("Text", _viewModel, "FormattedTotalReceivables", true);
            lblOverdueReceivablesValue.DataBindings.Add("Text", _viewModel, "FormattedTotalReceivablesOverdue", true);
            lblCashBalanceValue.DataBindings.Add("Text", _viewModel, "FormattedCashBalance", true);
            
            // Bind grids
            dgvRecentPayments.DataSource = _viewModel.RecentPayments;
            dgvRecentExpenses.DataSource = _viewModel.RecentExpenses;
            dgvOutstandingInvoices.DataSource = _viewModel.OutstandingInvoices;
            
            // Bind commands to buttons
            btnRefresh.Click += (s, e) => _viewModel.RefreshDataCommand.Execute(null);
            btnIncomeStatement.Click += (s, e) => _viewModel.GenerateIncomeStatementCommand.Execute(null);
            btnBalanceSheet.Click += (s, e) => _viewModel.GenerateBalanceSheetCommand.Execute(null);
            btnCashFlow.Click += (s, e) => _viewModel.GenerateCashFlowCommand.Execute(null);
        }
        
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.groupBoxMetrics = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblTotalSalesTitle = new System.Windows.Forms.Label();
            this.lblTotalSalesValue = new System.Windows.Forms.Label();
            this.lblTotalExpensesTitle = new System.Windows.Forms.Label();
            this.lblTotalExpensesValue = new System.Windows.Forms.Label();
            this.lblNetIncomeTitle = new System.Windows.Forms.Label();
            this.lblNetIncomeValue = new System.Windows.Forms.Label();
            this.lblTotalReceivablesTitle = new System.Windows.Forms.Label();
            this.lblTotalReceivablesValue = new System.Windows.Forms.Label();
            this.lblOverdueReceivablesTitle = new System.Windows.Forms.Label();
            this.lblOverdueReceivablesValue = new System.Windows.Forms.Label();
            this.lblCashBalanceTitle = new System.Windows.Forms.Label();
            this.lblCashBalanceValue = new System.Windows.Forms.Label();
            this.groupBoxPayments = new System.Windows.Forms.GroupBox();
            this.dgvRecentPayments = new System.Windows.Forms.DataGridView();
            this.groupBoxExpenses = new System.Windows.Forms.GroupBox();
            this.dgvRecentExpenses = new System.Windows.Forms.DataGridView();
            this.groupBoxOutstanding = new System.Windows.Forms.GroupBox();
            this.dgvOutstandingInvoices = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCashFlow = new System.Windows.Forms.Button();
            this.btnBalanceSheet = new System.Windows.Forms.Button();
            this.btnIncomeStatement = new System.Windows.Forms.Button();
            
            // Set up main layout
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxMetrics, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxPayments, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxExpenses, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxOutstanding, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1016, 626);
            this.tableLayoutPanel1.TabIndex = 0;
            
            // Date range panel
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.dtpEndDate);
            this.panel1.Controls.Add(this.lblEndDate);
            this.panel1.Controls.Add(this.dtpStartDate);
            this.panel1.Controls.Add(this.lblStartDate);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(502, 34);
            this.panel1.TabIndex = 0;
            
            // Financial metrics groupbox
            this.groupBoxMetrics.Controls.Add(this.tableLayoutPanel2);
            this.groupBoxMetrics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxMetrics.Location = new System.Drawing.Point(3, 43);
            this.groupBoxMetrics.Name = "groupBoxMetrics";
            this.groupBoxMetrics.Size = new System.Drawing.Size(502, 134);
            this.groupBoxMetrics.TabIndex = 1;
            this.groupBoxMetrics.Text = "Financial Metrics";
            this.tableLayoutPanel1.SetColumnSpan(this.groupBoxMetrics, 2);
            
            // Metrics layout
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Controls.Add(this.lblTotalSalesTitle, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblTotalSalesValue, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblTotalExpensesTitle, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblTotalExpensesValue, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblNetIncomeTitle, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblNetIncomeValue, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblTotalReceivablesTitle, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.lblTotalReceivablesValue, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.lblOverdueReceivablesTitle, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.lblOverdueReceivablesValue, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.lblCashBalanceTitle, 2, 2);
            this.tableLayoutPanel2.Controls.Add(this.lblCashBalanceValue, 2, 3);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 19);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(496, 112);
            this.tableLayoutPanel2.TabIndex = 0;
            
            // Recent payments groupbox
            this.groupBoxPayments.Controls.Add(this.dgvRecentPayments);
            this.groupBoxPayments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxPayments.Location = new System.Drawing.Point(3, 183);
            this.groupBoxPayments.Name = "groupBoxPayments";
            this.groupBoxPayments.Size = new System.Drawing.Size(502, 217);
            this.groupBoxPayments.TabIndex = 2;
            this.groupBoxPayments.Text = "Recent Payments";
            
            // Recent payments grid
            this.dgvRecentPayments.AllowUserToAddRows = false;
            this.dgvRecentPayments.AllowUserToDeleteRows = false;
            this.dgvRecentPayments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRecentPayments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecentPayments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRecentPayments.Location = new System.Drawing.Point(3, 19);
            this.dgvRecentPayments.MultiSelect = false;
            this.dgvRecentPayments.Name = "dgvRecentPayments";
            this.dgvRecentPayments.ReadOnly = true;
            this.dgvRecentPayments.RowHeadersVisible = false;
            this.dgvRecentPayments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRecentPayments.Size = new System.Drawing.Size(496, 195);
            this.dgvRecentPayments.TabIndex = 0;
            
            // Recent expenses groupbox
            this.groupBoxExpenses.Controls.Add(this.dgvRecentExpenses);
            this.groupBoxExpenses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxExpenses.Location = new System.Drawing.Point(511, 183);
            this.groupBoxExpenses.Name = "groupBoxExpenses";
            this.groupBoxExpenses.Size = new System.Drawing.Size(502, 217);
            this.groupBoxExpenses.TabIndex = 3;
            this.groupBoxExpenses.Text = "Recent Expenses";
            
            // Recent expenses grid
            this.dgvRecentExpenses.AllowUserToAddRows = false;
            this.dgvRecentExpenses.AllowUserToDeleteRows = false;
            this.dgvRecentExpenses.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRecentExpenses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecentExpenses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRecentExpenses.Location = new System.Drawing.Point(3, 19);
            this.dgvRecentExpenses.MultiSelect = false;
            this.dgvRecentExpenses.Name = "dgvRecentExpenses";
            this.dgvRecentExpenses.ReadOnly = true;
            this.dgvRecentExpenses.RowHeadersVisible = false;
            this.dgvRecentExpenses.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRecentExpenses.Size = new System.Drawing.Size(496, 195);
            this.dgvRecentExpenses.TabIndex = 0;
            
            // Outstanding invoices groupbox
            this.groupBoxOutstanding.Controls.Add(this.dgvOutstandingInvoices);
            this.groupBoxOutstanding.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxOutstanding.Location = new System.Drawing.Point(3, 406);
            this.groupBoxOutstanding.Name = "groupBoxOutstanding";
            this.groupBoxOutstanding.Size = new System.Drawing.Size(502, 217);
            this.groupBoxOutstanding.TabIndex = 4;
            this.groupBoxOutstanding.Text = "Outstanding Invoices";
            this.tableLayoutPanel1.SetColumnSpan(this.groupBoxOutstanding, 2);
            
            // Outstanding invoices grid
            this.dgvOutstandingInvoices.AllowUserToAddRows = false;
            this.dgvOutstandingInvoices.AllowUserToDeleteRows = false;
            this.dgvOutstandingInvoices.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOutstandingInvoices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOutstandingInvoices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOutstandingInvoices.Location = new System.Drawing.Point(3, 19);
            this.dgvOutstandingInvoices.MultiSelect = false;
            this.dgvOutstandingInvoices.Name = "dgvOutstandingInvoices";
            this.dgvOutstandingInvoices.ReadOnly = true;
            this.dgvOutstandingInvoices.RowHeadersVisible = false;
            this.dgvOutstandingInvoices.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOutstandingInvoices.Size = new System.Drawing.Size(496, 195);
            this.dgvOutstandingInvoices.TabIndex = 0;
            
            // Reports button panel
            this.panel2.Controls.Add(this.btnCashFlow);
            this.panel2.Controls.Add(this.btnBalanceSheet);
            this.panel2.Controls.Add(this.btnIncomeStatement);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(511, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(502, 34);
            this.panel2.TabIndex = 5;
            
            // Add the table layout to the main form
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FinancialDashboardView";
            this.Size = new System.Drawing.Size(1016, 626);
            
            // Button configurations
            this.btnRefresh.Text = "Refresh";
            this.btnIncomeStatement.Text = "Income Statement";
            this.btnBalanceSheet.Text = "Balance Sheet";
            this.btnCashFlow.Text = "Cash Flow";
            
            // Labels for metrics
            this.lblTotalSalesTitle.Text = "Total Sales";
            this.lblTotalExpensesTitle.Text = "Total Expenses";
            this.lblNetIncomeTitle.Text = "Net Income";
            this.lblTotalReceivablesTitle.Text = "Total Receivables";
            this.lblOverdueReceivablesTitle.Text = "Overdue Receivables";
            this.lblCashBalanceTitle.Text = "Cash Balance";
            
            this.lblTotalSalesValue.Text = "$0.00";
            this.lblTotalExpensesValue.Text = "$0.00";
            this.lblNetIncomeValue.Text = "$0.00";
            this.lblTotalReceivablesValue.Text = "$0.00";
            this.lblOverdueReceivablesValue.Text = "$0.00";
            this.lblCashBalanceValue.Text = "$0.00";
            
            // Date pickers
            this.lblStartDate.Text = "Start Date:";
            this.lblEndDate.Text = "End Date:";
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            
            // Additional components setup would go here
            // For brevity, I'm omitting detailed control setup code that would be generated by the designer
            
            this.ResumeLayout(false);
        }
        
        // Control declarations
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.GroupBox groupBoxMetrics;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lblTotalSalesTitle;
        private System.Windows.Forms.Label lblTotalSalesValue;
        private System.Windows.Forms.Label lblTotalExpensesTitle;
        private System.Windows.Forms.Label lblTotalExpensesValue;
        private System.Windows.Forms.Label lblNetIncomeTitle;
        private System.Windows.Forms.Label lblNetIncomeValue;
        private System.Windows.Forms.Label lblTotalReceivablesTitle;
        private System.Windows.Forms.Label lblTotalReceivablesValue;
        private System.Windows.Forms.Label lblOverdueReceivablesTitle;
        private System.Windows.Forms.Label lblOverdueReceivablesValue;
        private System.Windows.Forms.Label lblCashBalanceTitle;
        private System.Windows.Forms.Label lblCashBalanceValue;
        private System.Windows.Forms.GroupBox groupBoxPayments;
        private System.Windows.Forms.DataGridView dgvRecentPayments;
        private System.Windows.Forms.GroupBox groupBoxExpenses;
        private System.Windows.Forms.DataGridView dgvRecentExpenses;
        private System.Windows.Forms.GroupBox groupBoxOutstanding;
        private System.Windows.Forms.DataGridView dgvOutstandingInvoices;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnCashFlow;
        private System.Windows.Forms.Button btnBalanceSheet;
        private System.Windows.Forms.Button btnIncomeStatement;
    }
}