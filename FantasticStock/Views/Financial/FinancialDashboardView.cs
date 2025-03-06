using FantasticStock.ViewModels.Financial;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FantasticStock.Views.Financial
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

            /*
            // Bind grids
            dgvRecentPayments.DataSource = _viewModel.RecentPayments;
            dgvRecentExpenses.DataSource = _viewModel.RecentExpenses;
            dgvOutstandingInvoices.DataSource = _viewModel.OutstandingInvoices;

            // Bind commands to buttons
            btnRefresh.Click += (s, e) => _viewModel.RefreshDataCommand.Execute(null);
            btnIncomeStatement.Click += (s, e) => _viewModel.GenerateIncomeStatementCommand.Execute(null);
            btnBalanceSheet.Click += (s, e) => _viewModel.GenerateBalanceSheetCommand.Execute(null);
            btnCashFlow.Click += (s, e) => _viewModel.GenerateCashFlowCommand.Execute(null);
            */
        }
    }
}
