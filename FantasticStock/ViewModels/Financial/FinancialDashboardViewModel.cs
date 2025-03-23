using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using FantasticStock.Models.Financial;
using FantasticStock.Models.Sales;
using FantasticStock.Services.Admin;
using FantasticStock.Common;

namespace FantasticStock.ViewModels.Financial
{
    public class FinancialDashboardViewModel : ViewModelBase
    {/*
        private readonly IFinancialService _financialService;
        private readonly ISalesService _salesService;
        
        // Lists for data binding
        private BindingList<Payment> _recentPayments;
        private BindingList<Expense> _recentExpenses;
        private BindingList<Invoice> _outstandingInvoices;
        
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
        
        // Commands
        public ICommand RefreshDataCommand { get; private set; }
        public ICommand GenerateIncomeStatementCommand { get; private set; }
        public ICommand GenerateBalanceSheetCommand { get; private set; }
        public ICommand GenerateCashFlowCommand { get; private set; }
        
        public FinancialDashboardViewModel()
        {
            _financialService = ServiceLocator.GetService<IFinancialService>();
            _salesService = ServiceLocator.GetService<ISalesService>();
            
            // Initialize dates
            _startDate = new DateTime(DateTime.Parse("2025-03-04 02:04:43").Year, DateTime.Parse("2025-03-04 02:04:43").Month, 1);
            _endDate = DateTime.Parse("2025-03-04 02:04:43");
            
            // Initialize commands
            RefreshDataCommand = new RelayCommand(LoadData);
            GenerateIncomeStatementCommand = new RelayCommand(GenerateIncomeStatement);
            GenerateBalanceSheetCommand = new RelayCommand(GenerateBalanceSheet);
            GenerateCashFlowCommand = new RelayCommand(GenerateCashFlow);
            
            // Load initial data
            LoadData();
        }
        
        private void LoadData()
        {
            // Load payments
            var payments = _financialService.GetPaymentsByDateRange(StartDate, EndDate);
            RecentPayments = new BindingList<Payment>(payments);
            
            // Load expenses
            var expenses = _financialService.GetExpensesByDateRange(StartDate, EndDate);
            RecentExpenses = new BindingList<Expense>(expenses);
            
            // Load outstanding invoices
            var invoices = _salesService.GetInvoicesByStatus("Overdue");
            invoices.AddRange(_salesService.GetInvoicesByStatus("Issued"));
            OutstandingInvoices = new BindingList<Invoice>(invoices);
            
            // Calculate dashboard metrics
            CalculateMetrics();
        }
        
        private void CalculateMetrics()
        {
            // Calculate totals from loaded data
            
            // Total sales (from invoices for the current month)
            _totalSalesCurrentMonth = 0;
            foreach (var invoice in _salesService.GetInvoicesByDateRange(StartDate, EndDate))
            {
                _totalSalesCurrentMonth += invoice.TotalAmount;
            }
            
            // Total expenses for current month
            _totalExpensesCurrentMonth = 0;
            foreach (var expense in RecentExpenses)
            {
                _totalExpensesCurrentMonth += expense.TotalAmount;
            }
            
            // Net income
            _netIncomeCurrentMonth = _totalSalesCurrentMonth - _totalExpensesCurrentMonth;
            
            // Total receivables
            _totalReceivables = 0;
            _totalReceivablesOverdue = 0;
            
            foreach (var invoice in OutstandingInvoices)
            {
                _totalReceivables += invoice.Balance;
                
                if (invoice.Status == "Overdue")
                {
                    _totalReceivablesOverdue += invoice.Balance;
                }
            }
            
            // For demonstration purposes, set a sample cash balance
            _cashBalance = 50000.00M - _totalExpensesCurrentMonth + (_totalSalesCurrentMonth * 0.7M);
            
            // Notify UI of all changes
            OnPropertyChanged(nameof(TotalSalesCurrentMonth));
            OnPropertyChanged(nameof(TotalExpensesCurrentMonth));
            OnPropertyChanged(nameof(NetIncomeCurrentMonth));
            OnPropertyChanged(nameof(TotalReceivables));
            OnPropertyChanged(nameof(TotalReceivablesOverdue));
            OnPropertyChanged(nameof(CashBalance));
        }
        
        private void GenerateIncomeStatement()
        {
            try
            {
                var report = _financialService.GenerateIncomeStatement(StartDate, EndDate);
                
                // Implementation would handle displaying the report
                ShowMessage("Income Statement generated successfully.");
            }
            catch (Exception ex)
            {
                ShowError($"Failed to generate Income Statement: {ex.Message}");
            }
        }
        
        private void GenerateBalanceSheet()
        {
            try
            {
                var report = _financialService.GenerateBalanceSheet(EndDate);
                
                // Implementation would handle displaying the report
                ShowMessage("Balance Sheet generated successfully.");
            }
            catch (Exception ex)
            {
                ShowError($"Failed to generate Balance Sheet: {ex.Message}");
            }
        }
        
        private void GenerateCashFlow()
        {
            try
            {
                var report = _financialService.GenerateCashFlowStatement(StartDate, EndDate);
                
                // Implementation would handle displaying the report
                ShowMessage("Cash Flow Statement generated successfully.");
            }
            catch (Exception ex)
            {
                ShowError($"Failed to generate Cash Flow Statement: {ex.Message}");
            }
        }
        
        // Properties
        public BindingList<Payment> RecentPayments
        {
            get => _recentPayments;
            set
            {
                _recentPayments = value;
                OnPropertyChanged();
            }
        }
        
        public BindingList<Expense> RecentExpenses
        {
            get => _recentExpenses;
            set
            {
                _recentExpenses = value;
                OnPropertyChanged();
            }
        }
        
        public BindingList<Invoice> OutstandingInvoices
        {
            get => _outstandingInvoices;
            set
            {
                _outstandingInvoices = value;
                OnPropertyChanged();
            }
        }
        
        public decimal TotalSalesCurrentMonth => _totalSalesCurrentMonth;
        
        public string FormattedTotalSalesCurrentMonth => _totalSalesCurrentMonth.ToString("C");
        
        public decimal TotalExpensesCurrentMonth => _totalExpensesCurrentMonth;
        
        public string FormattedTotalExpensesCurrentMonth => _totalExpensesCurrentMonth.ToString("C");
        
        public decimal NetIncomeCurrentMonth => _netIncomeCurrentMonth;
        
        public string FormattedNetIncomeCurrentMonth => _netIncomeCurrentMonth.ToString("C");
        
        public decimal TotalReceivables => _totalReceivables;
        
        public string FormattedTotalReceivables => _totalReceivables.ToString("C");
        
        public decimal TotalReceivablesOverdue => _totalReceivablesOverdue;
        
        public string FormattedTotalReceivablesOverdue => _totalReceivablesOverdue.ToString("C");
        
        public decimal CashBalance => _cashBalance;
        
        public string FormattedCashBalance => _cashBalance.ToString("C");
        
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged();
            }
        }
        
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                OnPropertyChanged();
            }
        }
    */}
}