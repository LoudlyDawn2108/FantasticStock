using System;
using System.Collections.Generic;
using FantasticStock.Models.Financial;
using FantasticStock.Models.Sales;

namespace FantasticStock.Services
{
    public interface IFinancialService
    {
        // Payment methods
        List<Payment> GetAllPayments();
        Payment GetPaymentById(int paymentId);
        List<Payment> GetPaymentsByCustomer(int customerId);
        List<Payment> GetPaymentsByInvoice(int invoiceId);
        List<Payment> GetPaymentsByDateRange(DateTime startDate, DateTime endDate);
        bool AddPayment(Payment payment);
        bool UpdatePayment(Payment payment);
        bool DeletePayment(int paymentId);
        bool ApplyPaymentToInvoice(int paymentId, int invoiceId, decimal amount);
        
        // Expense methods
        List<Expense> GetAllExpenses();
        Expense GetExpenseById(int expenseId);
        List<Expense> GetExpensesByDateRange(DateTime startDate, DateTime endDate);
        List<Expense> GetExpensesByCategory(int categoryId);
        List<Expense> GetExpensesBySupplier(int supplierId);
        bool AddExpense(Expense expense);
        bool UpdateExpense(Expense expense);
        bool DeleteExpense(int expenseId);
        
        // Financial Reports
        byte[] GenerateIncomeStatement(DateTime startDate, DateTime endDate);
        byte[] GenerateBalanceSheet(DateTime asOfDate);
        byte[] GenerateCashFlowStatement(DateTime startDate, DateTime endDate);
        byte[] GenerateAccountsReceivableAging();
        byte[] GenerateSalesTaxReport(DateTime startDate, DateTime endDate);
    }
}