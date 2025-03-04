using System;
using System.Collections.Generic;
using FantasticStock.Models.Sales;

namespace FantasticStock.Services
{
    public interface ISalesService
    {
        // Customer methods
        List<Customer> GetAllCustomers();
        Customer GetCustomerById(int customerId);
        List<Customer> SearchCustomers(string searchQuery, int? customerTypeId, bool? isActive);
        bool AddCustomer(Customer customer);
        bool UpdateCustomer(Customer customer);
        bool DeleteCustomer(int customerId);
        
        // Sales Order methods
        List<SalesOrder> GetAllSalesOrders();
        SalesOrder GetSalesOrderById(int orderId);
        SalesOrder GetSalesOrderByNumber(string orderNumber);
        List<SalesOrder> GetSalesOrdersByCustomer(int customerId);
        List<SalesOrder> GetSalesOrdersByDateRange(DateTime startDate, DateTime endDate);
        List<SalesOrder> GetSalesOrdersByStatus(string status);
        bool CreateSalesOrder(SalesOrder order);
        bool UpdateSalesOrder(SalesOrder order);
        bool DeleteSalesOrder(int orderId);
        bool UpdateSalesOrderStatus(int orderId, string newStatus, int modifiedBy);
        
        // Sales Order Detail methods
        List<SalesOrderDetail> GetSalesOrderDetails(int orderId);
        bool AddOrderDetail(SalesOrderDetail detail);
        bool UpdateOrderDetail(SalesOrderDetail detail);
        bool DeleteOrderDetail(int orderDetailId);
        
        // Invoice methods
        List<Invoice> GetAllInvoices();
        Invoice GetInvoiceById(int invoiceId);
        Invoice GetInvoiceByNumber(string invoiceNumber);
        List<Invoice> GetInvoicesByCustomer(int customerId);
        List<Invoice> GetInvoicesByDateRange(DateTime startDate, DateTime endDate);
        List<Invoice> GetInvoicesByStatus(string status);
        bool CreateInvoice(Invoice invoice);
        bool UpdateInvoice(Invoice invoice);
        bool DeleteInvoice(int invoiceId);
        bool UpdateInvoiceStatus(int invoiceId, string newStatus, int modifiedBy);
        bool GenerateInvoiceFromOrder(int orderId, int createdBy);
        
        // Reports
        byte[] GenerateSalesReport(DateTime startDate, DateTime endDate, int? customerId);
        byte[] GenerateTopSellingProductsReport(DateTime startDate, DateTime endDate, int topCount);
        byte[] GenerateInvoiceDocument(int invoiceId);
        byte[] GenerateSalesOrderDocument(int orderId);
    }
}