using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using AdminDomain.Models.Sales;
using AdminDomain.Models.Inventory;
using AdminDomain.Services;

namespace AdminDomain.ViewModels.Sales
{
    public class SalesOrderViewModel : BaseViewModel
    {
        private readonly ISalesService _salesService;
        private readonly IInventoryService _inventoryService;
        
        // Lists for data binding
        private BindingList<SalesOrder> _salesOrders;
        private BindingList<SalesOrderDetail> _orderDetails;
        private BindingList<Customer> _customers;
        private BindingList<Product> _products;
        
        // Selected items
        private SalesOrder _selectedOrder;
        private SalesOrderDetail _selectedOrderDetail;
        private Customer _selectedCustomer;
        private Product _selectedProduct;
        
        // Filter properties
        private string _searchText;
        private DateTime _startDate;
        private DateTime _endDate;
        private string _statusFilter;
        private int? _customerFilter;
        
        // Commands
        public ICommand NewOrderCommand { get; private set; }
        public ICommand EditOrderCommand { get; private set; }
        public ICommand SaveOrderCommand { get; private set; }
        public ICommand DeleteOrderCommand { get; private set; }
        public ICommand AddProductCommand { get; private set; }
        public ICommand RemoveProductCommand { get; private set; }
        public ICommand FilterOrdersCommand { get; private set; }
        public ICommand GenerateInvoiceCommand { get; private set; }
        public ICommand PrintOrderCommand { get; private set; }
        public ICommand UpdateOrderStatusCommand { get; private set; }
        public ICommand RefreshDataCommand { get; private set; }
        
        public SalesOrderViewModel()
        {
            _salesService = ServiceLocator.GetService<ISalesService>();
            _inventoryService = ServiceLocator.GetService<IInventoryService>();
            
            // Initialize dates
            _startDate = DateTime.Parse("2025-03-03 16:32:51").AddDays(-30);
            _endDate = DateTime.Parse("2025-03-03 16:32:51");
            
            // Initialize commands
            NewOrderCommand = new RelayCommand(NewOrder);
            EditOrderCommand = new RelayCommand(EditOrder, CanEditOrder);
            SaveOrderCommand = new RelayCommand(SaveOrder, CanSaveOrder);
            DeleteOrderCommand = new RelayCommand(DeleteOrder, CanDeleteOrder);
            AddProductCommand = new RelayCommand(AddProduct, CanAddProduct);
            RemoveProductCommand = new RelayCommand(RemoveProduct, CanRemoveProduct);
            FilterOrdersCommand = new RelayCommand(FilterOrders);
            GenerateInvoiceCommand = new RelayCommand(GenerateInvoice, CanGenerateInvoice);
            PrintOrderCommand = new RelayCommand(PrintOrder, CanPrintOrder);
            UpdateOrderStatusCommand = new RelayCommand(UpdateOrderStatus, CanUpdateOrderStatus);
            RefreshDataCommand = new RelayCommand(LoadData);
            
            // Load initial data
            LoadData();
        }
        
        private void LoadData()
        {
            // Load sales orders
            var orders = _salesService.GetSalesOrdersByDateRange(StartDate, EndDate);
            SalesOrders = new BindingList<SalesOrder>(orders);
            
            // Load customers
            var customers = _salesService.GetAllCustomers();
            Customers = new BindingList<Customer>(customers);
            
            // Load products
            var products = _inventoryService.GetAllProducts();
            Products = new BindingList<Product>(products);
            
            // Clear selections
            SelectedOrder = null;
        }
        
        private void NewOrder()
        {
            IsEditing = true;
            
            // Create a new order
            var newOrder = new SalesOrder
            {
                OrderDate = DateTime.Parse("2025-03-03 16:32:51"),
                Status = "Draft",
                CreatedBy = CurrentUser.UserID,
                CreatedDate = DateTime.Parse("2025-03-03 16:32:51"),
                ModifiedBy = CurrentUser.UserID,
                ModifiedDate = DateTime.Parse("2025-03-03 16:32:51"),
                OrderItems = new List<SalesOrderDetail>()
            };
            
            SelectedOrder = newOrder;
            OrderDetails = new BindingList<SalesOrderDetail>();
        }
        
        private void EditOrder()
        {
            if (SelectedOrder == null)
                return;
                
            IsEditing = true;
            
            // Load order details
            var details = _salesService.GetSalesOrderDetails(SelectedOrder.OrderID);
            OrderDetails = new BindingList<SalesOrderDetail>(details);
        }
        
        private bool CanEditOrder()
        {
            return SelectedOrder != null;
        }
        
        private void SaveOrder()
        {
            if (SelectedOrder == null)
                return;
                
            bool success;
            
            // Update totals based on order details
            CalculateOrderTotals();
            
            // Determine if this is a new or existing order
            if (SelectedOrder.OrderID == 0)
            {
                // New order
                success = _salesService.CreateSalesOrder(SelectedOrder);
            }
            else
            {
                // Update existing order
                SelectedOrder.ModifiedDate = DateTime.Parse("2025-03-04 02:04:43");
                SelectedOrder.ModifiedBy = CurrentUser.UserID;
                success = _salesService.UpdateSalesOrder(SelectedOrder);
            }
            
            if (success)
            {
                IsEditing = false;
                LoadData();
            }
            else
            {
                ShowError("Failed to save order.");
            }
        }
        
        private bool CanSaveOrder()
        {
            if (!IsEditing || SelectedOrder == null)
                return false;
                
            // Basic validation
            return SelectedOrder.CustomerID > 0 &&
                   OrderDetails != null &&
                   OrderDetails.Count > 0;
        }
        
        private void CalculateOrderTotals()
        {
            if (SelectedOrder == null || OrderDetails == null)
                return;
                
            decimal subtotal = 0;
            decimal taxAmount = 0;
            
            foreach (var detail in OrderDetails)
            {
                subtotal += detail.LineTotal;
                taxAmount += detail.TaxAmount;
            }
            
            SelectedOrder.Subtotal = subtotal;
            SelectedOrder.TaxAmount = taxAmount;
            // Shipping and discounts would be set elsewhere
        }
        
        private void DeleteOrder()
        {
            if (SelectedOrder == null)
                return;
                
            if (ShowConfirmation($"Are you sure you want to delete order '{SelectedOrder.OrderNumber}'?"))
            {
                bool success = _salesService.DeleteSalesOrder(SelectedOrder.OrderID);
                
                if (success)
                {
                    LoadData();
                }
                else
                {
                    ShowError("Failed to delete order.");
                }
            }
        }
        
        private bool CanDeleteOrder()
        {
            return SelectedOrder != null && SelectedOrder.Status == "Draft";
        }
        
        private void AddProduct()
        {
            if (SelectedOrder == null || SelectedProduct == null)
                return;
                
            // Check if product already exists in order
            var existingDetail = OrderDetails.FirstOrDefault(d => d.ProductID == SelectedProduct.ProductID);
            
            if (existingDetail != null)
            {
                // Increase quantity if product already in order
                existingDetail.Quantity += 1;
            }
            else
            {
                // Add new product to order
                var newDetail = new SalesOrderDetail
                {
                    OrderID = SelectedOrder.OrderID,
                    OrderNumber = SelectedOrder.OrderNumber,
                    ProductID = SelectedProduct.ProductID,
                    ProductName = SelectedProduct.ProductName,
                    SKU = SelectedProduct.SKU,
                    Quantity = 1,
                    UnitPrice = SelectedProduct.SellingPrice,
                    DiscountPercent = 0,
                    TaxPercent = 10, // Default tax percentage, would be retrieved from settings
                    CreatedDate = DateTime.Parse("2025-03-04 02:04:43")
                };
                
                OrderDetails.Add(newDetail);
            }
            
            // Recalculate order totals
            CalculateOrderTotals();
        }
        
        private bool CanAddProduct()
        {
            return IsEditing && SelectedOrder != null && SelectedProduct != null;
        }
        
        private void RemoveProduct()
        {
            if (SelectedOrder == null || SelectedOrderDetail == null)
                return;
                
            OrderDetails.Remove(SelectedOrderDetail);
            
            // Recalculate order totals
            CalculateOrderTotals();
        }
        
        private bool CanRemoveProduct()
        {
            return IsEditing && SelectedOrder != null && SelectedOrderDetail != null;
        }
        
        private void FilterOrders()
        {
            var filteredOrders = _salesService.GetSalesOrdersByDateRange(StartDate, EndDate);
            
            // Apply additional filters if specified
            if (!string.IsNullOrEmpty(StatusFilter))
            {
                filteredOrders = filteredOrders.Where(o => o.Status == StatusFilter).ToList();
            }
            
            if (CustomerFilter.HasValue)
            {
                filteredOrders = filteredOrders.Where(o => o.CustomerID == CustomerFilter.Value).ToList();
            }
            
            if (!string.IsNullOrEmpty(SearchText))
            {
                filteredOrders = filteredOrders.Where(o => 
                    o.OrderNumber.Contains(SearchText) || 
                    o.CustomerName.Contains(SearchText)).ToList();
            }
            
            SalesOrders = new BindingList<SalesOrder>(filteredOrders);
        }
        
        private void GenerateInvoice()
        {
            if (SelectedOrder == null)
                return;
                
            if (ShowConfirmation($"Generate invoice for order '{SelectedOrder.OrderNumber}'?"))
            {
                bool success = _salesService.GenerateInvoiceFromOrder(SelectedOrder.OrderID, CurrentUser.UserID);
                
                if (success)
                {
                    ShowMessage("Invoice generated successfully.");
                    LoadData();
                }
                else
                {
                    ShowError("Failed to generate invoice.");
                }
            }
        }
        
        private bool CanGenerateInvoice()
        {
            return SelectedOrder != null && 
                  (SelectedOrder.Status == "Confirmed" || SelectedOrder.Status == "Shipped" || SelectedOrder.Status == "Delivered") &&
                  !SelectedOrder.OrderNumber.Contains("INV");
        }
        
        private void PrintOrder()
        {
            if (SelectedOrder == null)
                return;
                
            var document = _salesService.GenerateSalesOrderDocument(SelectedOrder.OrderID);
            
            // Implementation would handle displaying/printing the document
            ShowMessage($"Printing order {SelectedOrder.OrderNumber}...");
        }
        
        private bool CanPrintOrder()
        {
            return SelectedOrder != null;
        }
        
        private void UpdateOrderStatus()
        {
            if (SelectedOrder == null)
                return;
                
            // Implementation would show a dialog to select new status
            // For now, advance to next status in workflow
            string newStatus = GetNextStatus(SelectedOrder.Status);
            
            bool success = _salesService.UpdateSalesOrderStatus(SelectedOrder.OrderID, newStatus, CurrentUser.UserID);
            
            if (success)
            {
                SelectedOrder.Status = newStatus;
                LoadData();
            }
            else
            {
                ShowError("Failed to update order status.");
            }
        }
        
        private string GetNextStatus(string currentStatus)
        {
            return currentStatus switch
            {
                "Draft" => "Confirmed",
                "Confirmed" => "Shipped",
                "Shipped" => "Delivered",
                "Delivered" => "Invoiced",
                _ => currentStatus
            };
        }
        
        private bool CanUpdateOrderStatus()
        {
            return SelectedOrder != null && SelectedOrder.Status != "Cancelled" && SelectedOrder.Status != "Invoiced";
        }
        
        // Properties
        public BindingList<SalesOrder> SalesOrders
        {
            get => _salesOrders;
            set
            {
                _salesOrders = value;
                OnPropertyChanged();
            }
        }
        
        public BindingList<SalesOrderDetail> OrderDetails
        {
            get => _orderDetails;
            set
            {
                _orderDetails = value;
                OnPropertyChanged();
            }
        }
        
        public BindingList<Customer> Customers
        {
            get => _customers;
            set
            {
                _customers = value;
                OnPropertyChanged();
            }
        }
        
        public BindingList<Product> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged();
            }
        }
        
        public SalesOrder SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                _selectedOrder = value;
                OnPropertyChanged();
                
                // Update command states
                ((RelayCommand)EditOrderCommand).RaiseCanExecuteChanged();
                ((RelayCommand)DeleteOrderCommand).RaiseCanExecuteChanged();
                ((RelayCommand)GenerateInvoiceCommand).RaiseCanExecuteChanged();
                ((RelayCommand)PrintOrderCommand).RaiseCanExecuteChanged();
                ((RelayCommand)UpdateOrderStatusCommand).RaiseCanExecuteChanged();
            }
        }
        
        public SalesOrderDetail SelectedOrderDetail
        {
            get => _selectedOrderDetail;
            set
            {
                _selectedOrderDetail = value;
                OnPropertyChanged();
                ((RelayCommand)RemoveProductCommand).RaiseCanExecuteChanged();
            }
        }
        
        public Customer SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                _selectedCustomer = value;
                OnPropertyChanged();
                
                if (_selectedCustomer != null && SelectedOrder != null)
                {
                    SelectedOrder.CustomerID = _selectedCustomer.CustomerID;
                    SelectedOrder.CustomerName = _selectedCustomer.CustomerName;
                    
                    // Could also update shipping address here if needed
                }
            }
        }
        
        public Product SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                OnPropertyChanged();
                ((RelayCommand)AddProductCommand).RaiseCanExecuteChanged();
            }
        }
        
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
            }
        }
        
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
        
        public string StatusFilter
        {
            get => _statusFilter;
            set
            {
                _statusFilter = value;
                OnPropertyChanged();
            }
        }
        
        public int? CustomerFilter
        {
            get => _customerFilter;
            set
            {
                _customerFilter = value;
                OnPropertyChanged();
            }
        }
    }
}