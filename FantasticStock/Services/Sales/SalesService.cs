using System;
using System.Collections.Generic;
using System.Linq;
using FantasticStock.Models.Sales;
using FantasticStock.Common;

namespace FantasticStock.Services
{
    public class SalesService : ISalesService
    {
        private readonly IDatabaseService _databaseService;
        private readonly IAuditService _auditService;
        private readonly IInventoryService _inventoryService;

        public SalesService()
        {
            _databaseService = ServiceLocator.GetService<IDatabaseService>();
            _auditService = ServiceLocator.GetService<IAuditService>();
            _inventoryService = ServiceLocator.GetService<IInventoryService>();
        }

        // Customer methods
        public List<Customer> GetAllCustomers()
        {
            // Implementation would use _databaseService to retrieve customers from database
            // For now, return sample data
            return new List<Customer>
            {
                new Customer { 
                    CustomerID = 1, 
                    CustomerName = "Sample Customer", 
                    //ContactName = "John Doe", 
                    Email = "john@example.com", 
                    Phone = "123-456-7890", 
                    IsActive = true,
                    CreatedDate = DateTime.Parse("2025-03-03 16:31:01"),
                    ModifiedDate = DateTime.Parse("2025-03-03 16:31:01")
                }
            };
        }

        public Customer GetCustomerById(int customerId)
        {
            // Implementation to get customer by ID
            var customers = GetAllCustomers();
            return customers.FirstOrDefault(c => c.CustomerID == customerId);
        }

        public List<Customer> SearchCustomers(string searchQuery, int? customerTypeId, bool? isActive)
        {
            // Implementation to search customers
            var customers = GetAllCustomers();

            // Apply filters
            var query = customers.AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                query = query.Where(c =>
                    c.CustomerName.Contains(searchQuery) ||
                    c.Email.Contains(searchQuery) ||
                    c.Phone.Contains(searchQuery));
            }

            // Removed the filter for CustomerTypeID as it does not exist in the Customer class
            // if (customerTypeId.HasValue)
            // {
            //     query = query.Where(c => c.CustomerTypeID == customerTypeId.Value);
            // }

            if (isActive.HasValue)
            {
                query = query.Where(c => c.IsActive == isActive.Value);
            }

            return query.ToList();
        }

        public bool AddCustomer(Customer customer)
        {
            // Implementation to add a customer
            try
            {
                // Set default values
                customer.CreatedDate = DateTime.Parse("2025-03-03 16:31:01");
                customer.ModifiedDate = DateTime.Parse("2025-03-03 16:31:01");
                
                // Log the action
                _auditService.LogEvent(
                    CurrentUser.UserID,
                    "Create",
                    "Customers",
                    customer.CustomerID.ToString(),
                    null,
                    $"Customer {customer.CustomerName} created by {CurrentUser.Username}"
                );
                
                return true;
            }
            catch (Exception ex)
            {
                // Log error
                ServiceLocator.GetService<IMonitoringService>().LogError(
                    "SalesService",
                    $"Error adding customer: {ex.Message}",
                    ex.StackTrace,
                    3
                );
                return false;
            }
        }

        public bool UpdateCustomer(Customer customer)
        {
            // Implementation to update a customer
            try
            {
                // Update modified date
                customer.ModifiedDate = DateTime.Parse("2025-03-03 16:31:01");
                
                // Log the action
                _auditService.LogEvent(
                    CurrentUser.UserID,
                    "Update",
                    "Customers",
                    customer.CustomerID.ToString(),
                    null, // Original values would be here
                    $"Customer {customer.CustomerName} updated by {CurrentUser.Username}"
                );
                
                return true;
            }
            catch (Exception ex)
            {
                // Log error
                ServiceLocator.GetService<IMonitoringService>().LogError(
                    "SalesService",
                    $"Error updating customer: {ex.Message}",
                    ex.StackTrace,
                    3
                );
                return false;
            }
        }

        public bool DeleteCustomer(int customerId)
        {
            // Implementation to delete (or deactivate) a customer
            try
            {
                var customer = GetCustomerById(customerId);
                if (customer == null)
                    return false;
                    
                // Log the action
                _auditService.LogEvent(
                    CurrentUser.UserID,
                    "Delete",
                    "Customers",
                    customerId.ToString(),
                    null,
                    $"Customer {customer.CustomerName} deleted by {CurrentUser.Username}"
                );
                
                return true;
            }
            catch (Exception ex)
            {
                // Log error
                ServiceLocator.GetService<IMonitoringService>().LogError(
                    "SalesService",
                    $"Error deleting customer: {ex.Message}",
                    ex.StackTrace,
                    3
                );
                return false;
            }
        }

        // Sales Order methods
        public List<SalesOrder> GetAllSalesOrders()
        {
            // Implementation would use _databaseService to retrieve sales orders
            return new List<SalesOrder>
            {
                new SalesOrder {
                    OrderID = 1,
                    OrderNumber = "SO-2025-001",
                    OrderDate = DateTime.Parse("2025-03-03 16:31:01"),
                    CustomerID = 1,
                    CustomerName = "Sample Customer",
                    Status = "Draft",
                    Subtotal = 100.00M,
                    TaxAmount = 10.00M,
                    //TotalAmount = 110.00M,
                    CreatedBy = CurrentUser.UserID,
                    CreatedDate = DateTime.Parse("2025-03-03 16:31:01"),
                    ModifiedBy = CurrentUser.UserID,
                    ModifiedDate = DateTime.Parse("2025-03-03 16:31:01")
                }
            };
        }
        
        public SalesOrder GetSalesOrderById(int orderId)
        {
            var orders = GetAllSalesOrders();
            return orders.FirstOrDefault(o => o.OrderID == orderId);
        }
        
        public SalesOrder GetSalesOrderByNumber(string orderNumber)
        {
            var orders = GetAllSalesOrders();
            return orders.FirstOrDefault(o => o.OrderNumber == orderNumber);
        }
        
        public List<SalesOrder> GetSalesOrdersByCustomer(int customerId)
        {
            var orders = GetAllSalesOrders();
            return orders.Where(o => o.CustomerID == customerId).ToList();
        }
        
        public List<SalesOrder> GetSalesOrdersByDateRange(DateTime startDate, DateTime endDate)
        {
            var orders = GetAllSalesOrders();
            return orders.Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate).ToList();
        }
        
        public List<SalesOrder> GetSalesOrdersByStatus(string status)
        {
            var orders = GetAllSalesOrders();
            return orders.Where(o => o.Status == status).ToList();
        }
        
        public bool CreateSalesOrder(SalesOrder order)
        {
            try
            {
                // Set default values
                order.CreatedBy = CurrentUser.UserID;
                order.CreatedDate = DateTime.Parse("2025-03-03 16:31:01");
                order.ModifiedBy = CurrentUser.UserID;
                order.ModifiedDate = DateTime.Parse("2025-03-03 16:31:01");
                
                // Generate order number if not provided
                if (string.IsNullOrEmpty(order.OrderNumber))
                {
                    order.OrderNumber = $"SO-{DateTime.Now.ToString("yyyy")}-{GetNextOrderNumber().ToString("D3")}";
                }
                
                // Log the action
                _auditService.LogEvent(
                    CurrentUser.UserID,
                    "Create",
                    "SalesOrders",
                    order.OrderID.ToString(),
                    null,
                    $"Sales order {order.OrderNumber} created by {CurrentUser.Username}"
                );
                
                return true;
            }
            catch (Exception ex)
            {
                // Log error
                ServiceLocator.GetService<IMonitoringService>().LogError(
                    "SalesService",
                    $"Error creating sales order: {ex.Message}",
                    ex.StackTrace,
                    3
                );
                return false;
            }
        }
        
        private int GetNextOrderNumber()
        {
            // This would query the database for the next available order number
            // For now, return a sample value
            return 2;
        }
        
        // Implement other sales methods similarly
        // For brevity, I'm not implementing all methods in this example
        
        public bool UpdateSalesOrder(SalesOrder order) => true;
        public bool DeleteSalesOrder(int orderId) => true;
        public bool UpdateSalesOrderStatus(int orderId, string newStatus, int modifiedBy) => true;
        
        // Sales Order Detail methods
        public List<SalesOrderDetail> GetSalesOrderDetails(int orderId) => new List<SalesOrderDetail>();
        public bool AddOrderDetail(SalesOrderDetail detail) => true;
        public bool UpdateOrderDetail(SalesOrderDetail detail) => true;
        public bool DeleteOrderDetail(int orderDetailId) => true;
        
        // Invoice methods
        public List<Invoice> GetAllInvoices() => new List<Invoice>();
        public Invoice GetInvoiceById(int invoiceId) => null;
        public Invoice GetInvoiceByNumber(string invoiceNumber) => null;
        public List<Invoice> GetInvoicesByCustomer(int customerId) => new List<Invoice>();
        public List<Invoice> GetInvoicesByDateRange(DateTime startDate, DateTime endDate) => new List<Invoice>();
        public List<Invoice> GetInvoicesByStatus(string status) => new List<Invoice>();
        public bool CreateInvoice(Invoice invoice) => true;
        public bool UpdateInvoice(Invoice invoice) => true;
        public bool DeleteInvoice(int invoiceId) => true;
        public bool UpdateInvoiceStatus(int invoiceId, string newStatus, int modifiedBy) => true;
        public bool GenerateInvoiceFromOrder(int orderId, int createdBy) => true;
        
        // Reports
        public byte[] GenerateSalesReport(DateTime startDate, DateTime endDate, int? customerId) => new byte[0];
        public byte[] GenerateTopSellingProductsReport(DateTime startDate, DateTime endDate, int topCount) => new byte[0];
        public byte[] GenerateInvoiceDocument(int invoiceId) => new byte[0];
        public byte[] GenerateSalesOrderDocument(int orderId) => new byte[0];
    }
}