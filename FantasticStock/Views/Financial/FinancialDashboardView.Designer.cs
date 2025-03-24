namespace FantasticStock.Views.Financial
{
    partial class FinancialDashboardView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
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
        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
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
            this.btnRefresh = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBoxMetrics.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBoxPayments.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecentPayments)).BeginInit();
            this.groupBoxExpenses.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecentExpenses)).BeginInit();
            this.groupBoxOutstanding.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOutstandingInvoices)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
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
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dtpEndDate);
            this.panel1.Controls.Add(this.lblEndDate);
            this.panel1.Controls.Add(this.dtpStartDate);
            this.panel1.Controls.Add(this.lblStartDate);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(502, 34);
            this.panel1.TabIndex = 0;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(254, 8);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(126, 20);
            this.dtpEndDate.TabIndex = 1;
            // 
            // lblEndDate
            // 
            this.lblEndDate.Location = new System.Drawing.Point(198, 11);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(100, 23);
            this.lblEndDate.TabIndex = 2;
            this.lblEndDate.Text = "End Date:";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(64, 7);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(128, 20);
            this.dtpStartDate.TabIndex = 3;
            // 
            // lblStartDate
            // 
            this.lblStartDate.Location = new System.Drawing.Point(6, 10);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(100, 23);
            this.lblStartDate.TabIndex = 4;
            this.lblStartDate.Text = "Start Date:";
            // 
            // groupBoxMetrics
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.groupBoxMetrics, 2);
            this.groupBoxMetrics.Controls.Add(this.tableLayoutPanel2);
            this.groupBoxMetrics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxMetrics.Location = new System.Drawing.Point(3, 43);
            this.groupBoxMetrics.Name = "groupBoxMetrics";
            this.groupBoxMetrics.Size = new System.Drawing.Size(1010, 134);
            this.groupBoxMetrics.TabIndex = 1;
            this.groupBoxMetrics.TabStop = false;
            this.groupBoxMetrics.Text = "Financial Metrics";
            // 
            // tableLayoutPanel2
            // 
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
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1004, 115);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // lblTotalSalesTitle
            // 
            this.lblTotalSalesTitle.Location = new System.Drawing.Point(3, 0);
            this.lblTotalSalesTitle.Name = "lblTotalSalesTitle";
            this.lblTotalSalesTitle.Size = new System.Drawing.Size(100, 23);
            this.lblTotalSalesTitle.TabIndex = 0;
            this.lblTotalSalesTitle.Text = "Total Sales";
            // 
            // lblTotalSalesValue
            // 
            this.lblTotalSalesValue.Location = new System.Drawing.Point(3, 28);
            this.lblTotalSalesValue.Name = "lblTotalSalesValue";
            this.lblTotalSalesValue.Size = new System.Drawing.Size(100, 23);
            this.lblTotalSalesValue.TabIndex = 1;
            this.lblTotalSalesValue.Text = "$0.00";
            // 
            // lblTotalExpensesTitle
            // 
            this.lblTotalExpensesTitle.Location = new System.Drawing.Point(254, 0);
            this.lblTotalExpensesTitle.Name = "lblTotalExpensesTitle";
            this.lblTotalExpensesTitle.Size = new System.Drawing.Size(100, 23);
            this.lblTotalExpensesTitle.TabIndex = 2;
            this.lblTotalExpensesTitle.Text = "Total Expenses";
            // 
            // lblTotalExpensesValue
            // 
            this.lblTotalExpensesValue.Location = new System.Drawing.Point(254, 28);
            this.lblTotalExpensesValue.Name = "lblTotalExpensesValue";
            this.lblTotalExpensesValue.Size = new System.Drawing.Size(100, 23);
            this.lblTotalExpensesValue.TabIndex = 3;
            this.lblTotalExpensesValue.Text = "$0.00";
            // 
            // lblNetIncomeTitle
            // 
            this.lblNetIncomeTitle.Location = new System.Drawing.Point(505, 0);
            this.lblNetIncomeTitle.Name = "lblNetIncomeTitle";
            this.lblNetIncomeTitle.Size = new System.Drawing.Size(100, 23);
            this.lblNetIncomeTitle.TabIndex = 4;
            this.lblNetIncomeTitle.Text = "Net Income";
            // 
            // lblNetIncomeValue
            // 
            this.lblNetIncomeValue.Location = new System.Drawing.Point(505, 28);
            this.lblNetIncomeValue.Name = "lblNetIncomeValue";
            this.lblNetIncomeValue.Size = new System.Drawing.Size(100, 23);
            this.lblNetIncomeValue.TabIndex = 5;
            this.lblNetIncomeValue.Text = "$0.00";
            // 
            // lblTotalReceivablesTitle
            // 
            this.lblTotalReceivablesTitle.Location = new System.Drawing.Point(3, 56);
            this.lblTotalReceivablesTitle.Name = "lblTotalReceivablesTitle";
            this.lblTotalReceivablesTitle.Size = new System.Drawing.Size(100, 23);
            this.lblTotalReceivablesTitle.TabIndex = 6;
            this.lblTotalReceivablesTitle.Text = "Total Receivables";
            // 
            // lblTotalReceivablesValue
            // 
            this.lblTotalReceivablesValue.Location = new System.Drawing.Point(3, 84);
            this.lblTotalReceivablesValue.Name = "lblTotalReceivablesValue";
            this.lblTotalReceivablesValue.Size = new System.Drawing.Size(100, 23);
            this.lblTotalReceivablesValue.TabIndex = 7;
            this.lblTotalReceivablesValue.Text = "$0.00";
            // 
            // lblOverdueReceivablesTitle
            // 
            this.lblOverdueReceivablesTitle.Location = new System.Drawing.Point(254, 56);
            this.lblOverdueReceivablesTitle.Name = "lblOverdueReceivablesTitle";
            this.lblOverdueReceivablesTitle.Size = new System.Drawing.Size(100, 23);
            this.lblOverdueReceivablesTitle.TabIndex = 8;
            this.lblOverdueReceivablesTitle.Text = "Overdue Receivables";
            // 
            // lblOverdueReceivablesValue
            // 
            this.lblOverdueReceivablesValue.Location = new System.Drawing.Point(254, 84);
            this.lblOverdueReceivablesValue.Name = "lblOverdueReceivablesValue";
            this.lblOverdueReceivablesValue.Size = new System.Drawing.Size(100, 23);
            this.lblOverdueReceivablesValue.TabIndex = 9;
            this.lblOverdueReceivablesValue.Text = "$0.00";
            // 
            // lblCashBalanceTitle
            // 
            this.lblCashBalanceTitle.Location = new System.Drawing.Point(505, 56);
            this.lblCashBalanceTitle.Name = "lblCashBalanceTitle";
            this.lblCashBalanceTitle.Size = new System.Drawing.Size(100, 23);
            this.lblCashBalanceTitle.TabIndex = 10;
            this.lblCashBalanceTitle.Text = "Cash Balance";
            // 
            // lblCashBalanceValue
            // 
            this.lblCashBalanceValue.Location = new System.Drawing.Point(505, 84);
            this.lblCashBalanceValue.Name = "lblCashBalanceValue";
            this.lblCashBalanceValue.Size = new System.Drawing.Size(100, 23);
            this.lblCashBalanceValue.TabIndex = 11;
            this.lblCashBalanceValue.Text = "$0.00";
            // 
            // groupBoxPayments
            // 
            this.groupBoxPayments.Controls.Add(this.dgvRecentPayments);
            this.groupBoxPayments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxPayments.Location = new System.Drawing.Point(3, 183);
            this.groupBoxPayments.Name = "groupBoxPayments";
            this.groupBoxPayments.Size = new System.Drawing.Size(502, 217);
            this.groupBoxPayments.TabIndex = 2;
            this.groupBoxPayments.TabStop = false;
            this.groupBoxPayments.Text = "Recent Payments";
            // 
            // dgvRecentPayments
            // 
            this.dgvRecentPayments.AllowUserToAddRows = false;
            this.dgvRecentPayments.AllowUserToDeleteRows = false;
            this.dgvRecentPayments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRecentPayments.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvRecentPayments.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvRecentPayments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecentPayments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRecentPayments.GridColor = System.Drawing.Color.Cyan;
            this.dgvRecentPayments.Location = new System.Drawing.Point(3, 16);
            this.dgvRecentPayments.MultiSelect = false;
            this.dgvRecentPayments.Name = "dgvRecentPayments";
            this.dgvRecentPayments.ReadOnly = true;
            this.dgvRecentPayments.RowHeadersVisible = false;
            this.dgvRecentPayments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRecentPayments.Size = new System.Drawing.Size(496, 198);
            this.dgvRecentPayments.TabIndex = 0;
            // 
            // groupBoxExpenses
            // 
            this.groupBoxExpenses.Controls.Add(this.dgvRecentExpenses);
            this.groupBoxExpenses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxExpenses.Location = new System.Drawing.Point(511, 183);
            this.groupBoxExpenses.Name = "groupBoxExpenses";
            this.groupBoxExpenses.Size = new System.Drawing.Size(502, 217);
            this.groupBoxExpenses.TabIndex = 3;
            this.groupBoxExpenses.TabStop = false;
            this.groupBoxExpenses.Text = "Recent Expenses";
            // 
            // dgvRecentExpenses
            // 
            this.dgvRecentExpenses.AllowUserToAddRows = false;
            this.dgvRecentExpenses.AllowUserToDeleteRows = false;
            this.dgvRecentExpenses.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRecentExpenses.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvRecentExpenses.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvRecentExpenses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecentExpenses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRecentExpenses.GridColor = System.Drawing.Color.Cyan;
            this.dgvRecentExpenses.Location = new System.Drawing.Point(3, 16);
            this.dgvRecentExpenses.MultiSelect = false;
            this.dgvRecentExpenses.Name = "dgvRecentExpenses";
            this.dgvRecentExpenses.ReadOnly = true;
            this.dgvRecentExpenses.RowHeadersVisible = false;
            this.dgvRecentExpenses.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRecentExpenses.Size = new System.Drawing.Size(496, 198);
            this.dgvRecentExpenses.TabIndex = 0;
            // 
            // groupBoxOutstanding
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.groupBoxOutstanding, 2);
            this.groupBoxOutstanding.Controls.Add(this.dgvOutstandingInvoices);
            this.groupBoxOutstanding.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxOutstanding.Location = new System.Drawing.Point(3, 406);
            this.groupBoxOutstanding.Name = "groupBoxOutstanding";
            this.groupBoxOutstanding.Size = new System.Drawing.Size(1010, 217);
            this.groupBoxOutstanding.TabIndex = 4;
            this.groupBoxOutstanding.TabStop = false;
            this.groupBoxOutstanding.Text = "Outstanding Invoices";
            // 
            // dgvOutstandingInvoices
            // 
            this.dgvOutstandingInvoices.AllowUserToAddRows = false;
            this.dgvOutstandingInvoices.AllowUserToDeleteRows = false;
            this.dgvOutstandingInvoices.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOutstandingInvoices.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvOutstandingInvoices.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvOutstandingInvoices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOutstandingInvoices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOutstandingInvoices.GridColor = System.Drawing.Color.Cyan;
            this.dgvOutstandingInvoices.Location = new System.Drawing.Point(3, 16);
            this.dgvOutstandingInvoices.MultiSelect = false;
            this.dgvOutstandingInvoices.Name = "dgvOutstandingInvoices";
            this.dgvOutstandingInvoices.ReadOnly = true;
            this.dgvOutstandingInvoices.RowHeadersVisible = false;
            this.dgvOutstandingInvoices.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOutstandingInvoices.Size = new System.Drawing.Size(1004, 198);
            this.dgvOutstandingInvoices.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnRefresh);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(511, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(502, 34);
            this.panel2.TabIndex = 5;
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.Lime;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Location = new System.Drawing.Point(3, 5);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            // 
            // FinancialDashboardView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FinancialDashboardView";
            this.Size = new System.Drawing.Size(1016, 626);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBoxMetrics.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.groupBoxPayments.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecentPayments)).EndInit();
            this.groupBoxExpenses.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecentExpenses)).EndInit();
            this.groupBoxOutstanding.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOutstandingInvoices)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
