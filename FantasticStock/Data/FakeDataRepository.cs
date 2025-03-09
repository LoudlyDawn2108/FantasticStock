using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using FantasticStock.Models.Sales;

namespace FantasticStock.Data
{
    public class CustomerManagementViewModel
    {
        private static CustomerManagementViewModel _instance;
        private static readonly object _lock = new object();

        private List<Product> _products;
        private List<Category> _categories;
        private List<Customer> _customers;
        private BindingList<SaleItem> _currentSaleItems;
        private int _nextSaleItemId = 1;

        public List<Product> Products => _products;
        public List<Category> Categories => _categories;
        public List<Customer> Customers => _customers;
        public BindingList<SaleItem> CurrentSaleItems => _currentSaleItems;

        public decimal Subtotal => CurrentSaleItems.Sum(item => item.LineTotal);
        public decimal TaxRate => 0.07m; // 7% tax
        public decimal TaxAmount => Subtotal * TaxRate;
        public decimal DiscountAmount { get; set; } = 0;
        public decimal TotalAmount => Subtotal + TaxAmount - DiscountAmount;

        private CustomerManagementViewModel()
        {
            InitializeData();
        }

        public static CustomerManagementViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new CustomerManagementViewModel();
                        }
                    }
                }
                return _instance;
            }
        }

        private void InitializeData()
        {
            // Initialize product categories
            _categories = new List<Category>
            {
                new Category { CategoryID = 1, CategoryName = "Electronics", Description = "Electronic devices and accessories", IsActive = true },
                new Category { CategoryID = 2, CategoryName = "Clothing", Description = "Apparel and accessories", IsActive = true },
                new Category { CategoryID = 3, CategoryName = "Books", Description = "Books, magazines and publications", IsActive = true },
                new Category { CategoryID = 4, CategoryName = "Groceries", Description = "Food items and household supplies", IsActive = true },
                new Category { CategoryID = 5, CategoryName = "Toys", Description = "Toys and games", IsActive = true }
            };

            // Initialize products
            _products = new List<Product>
            {
                // Electronics
                new Product { ProductID = 101, SKU = "E001", Barcode = "9781234567890", ProductName = "Smartphone", Description = "Latest smartphone model", CategoryID = 1, CategoryName = "Electronics", SellingPrice = 599.99m, CostPrice = 400.00m, IsActive = true, UnitsInStock = 15 },
                new Product { ProductID = 102, SKU = "E002", Barcode = "9781234567891", ProductName = "Laptop", Description = "Powerful laptop computer", CategoryID = 1, CategoryName = "Electronics", SellingPrice = 999.99m, CostPrice = 750.00m, IsActive = true, UnitsInStock = 10 },
                new Product { ProductID = 103, SKU = "E003", Barcode = "9781234567892", ProductName = "Headphones", Description = "Wireless noise-cancelling headphones", CategoryID = 1, CategoryName = "Electronics", SellingPrice = 199.99m, CostPrice = 100.00m, IsActive = true, UnitsInStock = 25 },
                new Product { ProductID = 104, SKU = "E004", Barcode = "9781234567893", ProductName = "Tablet", Description = "10-inch tablet", CategoryID = 1, CategoryName = "Electronics", SellingPrice = 349.99m, CostPrice = 200.00m, IsActive = true, UnitsInStock = 12 },
                
                // Clothing
                new Product { ProductID = 201, SKU = "C001", Barcode = "9782345678901", ProductName = "T-Shirt", Description = "Cotton t-shirt", CategoryID = 2, CategoryName = "Clothing", SellingPrice = 19.99m, CostPrice = 5.00m, IsActive = true, UnitsInStock = 50 },
                new Product { ProductID = 202, SKU = "C002", Barcode = "9782345678902", ProductName = "Jeans", Description = "Denim jeans", CategoryID = 2, CategoryName = "Clothing", SellingPrice = 49.99m, CostPrice = 20.00m, IsActive = true, UnitsInStock = 30 },
                new Product { ProductID = 203, SKU = "C003", Barcode = "9782345678903", ProductName = "Jacket", Description = "Winter jacket", CategoryID = 2, CategoryName = "Clothing", SellingPrice = 79.99m, CostPrice = 40.00m, IsActive = true, UnitsInStock = 25 },
                
                // Books
                new Product { ProductID = 301, SKU = "B001", Barcode = "9783456789012", ProductName = "Novel", Description = "Bestselling fiction novel", CategoryID = 3, CategoryName = "Books", SellingPrice = 14.99m, CostPrice = 7.50m, IsActive = true, UnitsInStock = 40 },
                new Product { ProductID = 302, SKU = "B002", Barcode = "9783456789013", ProductName = "Cookbook", Description = "Gourmet recipes", CategoryID = 3, CategoryName = "Books", SellingPrice = 24.99m, CostPrice = 12.00m, IsActive = true, UnitsInStock = 20 },
                
                // Groceries
                new Product { ProductID = 401, SKU = "G001", Barcode = "9784567890123", ProductName = "Coffee", Description = "Premium coffee beans", CategoryID = 4, CategoryName = "Groceries", SellingPrice = 12.99m, CostPrice = 6.00m, IsActive = true, UnitsInStock = 30 },
                new Product { ProductID = 402, SKU = "G002", Barcode = "9784567890124", ProductName = "Chocolate", Description = "Gourmet chocolate", CategoryID = 4, CategoryName = "Groceries", SellingPrice = 7.99m, CostPrice = 3.50m, IsActive = true, UnitsInStock = 50 },
                
                // Toys
                new Product { ProductID = 501, SKU = "T001", Barcode = "9785678901234", ProductName = "Action Figure", Description = "Collectible action figure", CategoryID = 5, CategoryName = "Toys", SellingPrice = 19.99m, CostPrice = 8.00m, IsActive = true, UnitsInStock = 25 },
                new Product { ProductID = 502, SKU = "T002", Barcode = "9785678901235", ProductName = "Board Game", Description = "Family board game", CategoryID = 5, CategoryName = "Toys", SellingPrice = 29.99m, CostPrice = 15.00m, IsActive = true, UnitsInStock = 15 }
            };

            // Initialize customers
            _customers = new List<Customer>
            {
                new Customer { CustomerID = 1, CustomerName = "John Smith", Phone = "555-123-4567", Email = "john.smith@example.com", Address = "123 Main St", LoyaltyPoints = 150, IsActive = true },
                new Customer { CustomerID = 2, CustomerName = "Jane Doe", Phone = "555-987-6543", Email = "jane.doe@example.com", Address = "456 Oak Ave", LoyaltyPoints = 75, IsActive = true },
                new Customer { CustomerID = 3, CustomerName = "Bob Johnson", Phone = "555-555-5555", Email = "bob.johnson@example.com", Address = "789 Pine Rd", LoyaltyPoints = 200, IsActive = true },
                new Customer { CustomerID = 4, CustomerName = "Alice Williams", Phone = "555-246-8101", Email = "alice.williams@example.com", Address = "321 Cedar Ln", LoyaltyPoints = 100, IsActive = true }
            };

            // Initialize empty cart
            _currentSaleItems = new BindingList<SaleItem>();
        }

        public List<Product> GetProductsByCategory(int categoryId)
        {
            return _products.Where(p => p.CategoryID == categoryId && p.IsActive).ToList();
        }

        public Product GetProductByBarcode(string barcode)
        {
            return _products.FirstOrDefault(p => p.Barcode == barcode && p.IsActive);
        }

        public Product GetProductById(int productId)
        {
            return _products.FirstOrDefault(p => p.ProductID == productId && p.IsActive);
        }

        public List<Product> SearchProducts(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return new List<Product>();
                
            searchTerm = searchTerm.ToLower();
            return _products
                .Where(p => p.IsActive && (
                    p.ProductName.ToLower().Contains(searchTerm) || 
                    p.SKU.ToLower().Contains(searchTerm) || 
                    p.Description.ToLower().Contains(searchTerm) || 
                    p.Barcode.ToLower().Contains(searchTerm)))
                .ToList();
        }

        public Customer GetCustomer(int customerId)
        {
            return _customers.FirstOrDefault(c => c.CustomerID == customerId && c.IsActive);
        }

        public List<Customer> SearchCustomers(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return new List<Customer>();
                
            searchTerm = searchTerm.ToLower();
            return _customers
                .Where(c => c.IsActive && (
                    c.CustomerName.ToLower().Contains(searchTerm) || 
                    c.Phone.Contains(searchTerm) || 
                    c.Email.ToLower().Contains(searchTerm)))
                .ToList();
        }

        public void AddItemToCart(Product product, decimal quantity = 1)
        {
            if (product == null || !product.IsActive || product.UnitsInStock < quantity)
                return;
                
            // Check if product already exists in cart
            var existingItem = _currentSaleItems.FirstOrDefault(item => item.ProductID == product.ProductID);
            
            if (existingItem != null)
            {
                // Update existing item
                existingItem.Quantity += quantity;
            }
            else
            {
                // Add new item
                var newItem = new SaleItem
                {
                    ItemID = _nextSaleItemId++,
                    ProductID = product.ProductID,
                    ProductName = product.ProductName,
                    UnitPrice = product.SellingPrice,
                    Quantity = quantity,
                    DiscountPercent = 0
                };
                
                _currentSaleItems.Add(newItem);
            }
        }

        public void UpdateCartItemQuantity(int itemId, decimal newQuantity)
        {
            if (newQuantity <= 0)
                RemoveCartItem(itemId);
            else
            {
                var item = _currentSaleItems.FirstOrDefault(i => i.ItemID == itemId);
                if (item != null)
                {
                    var product = GetProductById(item.ProductID);
                    if (product != null && product.UnitsInStock >= newQuantity)
                    {
                        item.Quantity = newQuantity;
                    }
                }
            }
        }

        public void RemoveCartItem(int itemId)
        {
            var item = _currentSaleItems.FirstOrDefault(i => i.ItemID == itemId);
            if (item != null)
            {
                _currentSaleItems.Remove(item);
            }
        }
        
        public void ClearCart()
        {
            _currentSaleItems.Clear();
            _nextSaleItemId = 1;
            DiscountAmount = 0;
        }
        
        #region Customer Management
        
        private Customer _selectedCustomer;
        
        public Customer SelectedCustomer 
        { 
            get { return _selectedCustomer; }
            set { _selectedCustomer = value; }
        }
        
        public void SelectCustomer(int customerId)
        {
            _selectedCustomer = GetCustomer(customerId);
        }
        
        public void ClearSelectedCustomer()
        {
            _selectedCustomer = null;
        }
        
        public Customer AddCustomer(Customer customer)
        {
            // Generate a new ID
            int newId = _customers.Count > 0 ? _customers.Max(c => c.CustomerID) + 1 : 1;
            
            // Set default values
            customer.CustomerID = newId;
            customer.CreatedDate = DateTime.Now;
            customer.ModifiedDate = DateTime.Now;
            customer.IsActive = true;
            customer.LoyaltyPoints = 0;
            
            _customers.Add(customer);
            return customer;
        }
        
        public bool UpdateCustomer(Customer customer)
        {
            var existingCustomer = _customers.FirstOrDefault(c => c.CustomerID == customer.CustomerID);
            if (existingCustomer == null)
                return false;
                
            existingCustomer.CustomerName = customer.CustomerName;
            existingCustomer.Phone = customer.Phone;
            existingCustomer.Email = customer.Email;
            existingCustomer.Address = customer.Address;
            existingCustomer.IsActive = customer.IsActive;
            existingCustomer.ModifiedDate = DateTime.Now;
            
            // Update selected customer if it's the same one
            if (_selectedCustomer != null && _selectedCustomer.CustomerID == customer.CustomerID)
            {
                _selectedCustomer = existingCustomer;
            }
            
            return true;
        }
        
        public bool DeactivateCustomer(int customerId)
        {
            var customer = _customers.FirstOrDefault(c => c.CustomerID == customerId);
            if (customer == null)
                return false;
                
            customer.IsActive = false;
            customer.ModifiedDate = DateTime.Now;
            
            // Clear selected customer if it's the same one
            if (_selectedCustomer != null && _selectedCustomer.CustomerID == customerId)
            {
                _selectedCustomer = null;
            }
            
            return true;
        }
        #endregion
        
        #region Discount Management
        
        public void ApplyDiscountAmount(decimal amount)
        {
            if (amount >= 0 && amount <= Subtotal)
            {
                DiscountAmount = amount;
            }
        }
        
        public void ApplyDiscountPercentage(decimal percentage)
        {
            if (percentage >= 0 && percentage <= 100)
            {
                DiscountAmount = Subtotal * (percentage / 100);
            }
        }
        
        public void ApplyLoyaltyPointsDiscount()
        {
            if (_selectedCustomer == null || _selectedCustomer.LoyaltyPoints <= 0)
                return;
                
            // Conversion rate: 10 points = $1.00
            decimal discountAmount = Math.Min(_selectedCustomer.LoyaltyPoints / 10, Subtotal);
            DiscountAmount = discountAmount;
        }
        
        public void ClearDiscount()
        {
            DiscountAmount = 0;
        }
        #endregion
        
        #region Transaction Processing
        
        private int _nextTransactionId = 1000;
        private List<Sale> _completedSales = new List<Sale>();
        private List<Sale> _heldSales = new List<Sale>();
        
        public List<Sale> CompletedSales => _completedSales;
        public List<Sale> HeldSales => _heldSales;
        
        public Sale ProcessPayment(string paymentMethod, decimal amountPaid)
        {
            if (CurrentSaleItems.Count == 0)
                return null;
                
            // Create a new sale
            Sale sale = new Sale
            {
                SaleID = _nextTransactionId++,
                SaleDate = DateTime.Now,
                CustomerID = _selectedCustomer?.CustomerID ?? 0,
                CustomerName = _selectedCustomer?.CustomerName ?? "Guest",
                PaymentMethod = paymentMethod,
                Subtotal = Subtotal,
                TaxAmount = TaxAmount,
                DiscountAmount = DiscountAmount,
                TotalAmount = TotalAmount,
                AmountPaid = amountPaid,
                Change = amountPaid - TotalAmount,
                Items = CurrentSaleItems.Select(item => item.Clone()).ToList()
            };
            
            // Add to completed sales
            _completedSales.Add(sale);
            
            // Update inventory
            UpdateInventoryAfterSale(sale);
            
            // Update loyalty points if customer exists
            if (_selectedCustomer != null)
            {
                // Add 1 loyalty point for each dollar spent
                int pointsEarned = (int)Math.Floor(sale.TotalAmount);
                _selectedCustomer.LoyaltyPoints += pointsEarned;
                
                // If loyalty points were used as discount, deduct them
                if (DiscountAmount > 0)
                {
                    int pointsUsed = (int)Math.Ceiling(DiscountAmount * 10);
                    _selectedCustomer.LoyaltyPoints -= Math.Min(pointsUsed, _selectedCustomer.LoyaltyPoints);
                }
                
                // Update customer in the list
                UpdateCustomer(_selectedCustomer);
            }
            
            // Clear cart for next transaction
            ClearCart();
            ClearSelectedCustomer();
            
            return sale;
        }
        
        public void HoldTransaction()
        {
            if (CurrentSaleItems.Count == 0)
                return;
                
            // Create a held sale
            Sale heldSale = new Sale
            {
                SaleID = _nextTransactionId++,
                SaleDate = DateTime.Now,
                CustomerID = _selectedCustomer?.CustomerID ?? 0,
                CustomerName = _selectedCustomer?.CustomerName ?? "Guest",
                PaymentMethod = "Held",
                Subtotal = Subtotal,
                TaxAmount = TaxAmount,
                DiscountAmount = DiscountAmount,
                TotalAmount = TotalAmount,
                AmountPaid = 0,
                Change = 0,
                Items = CurrentSaleItems.Select(item => item.Clone()).ToList(),
                IsHeld = true
            };
            
            // Add to held sales
            _heldSales.Add(heldSale);
            
            // Clear cart for next transaction
            ClearCart();
            ClearSelectedCustomer();
        }
        
        public void RecallHeldTransaction(int saleId)
        {
            var heldSale = _heldSales.FirstOrDefault(s => s.SaleID == saleId);
            if (heldSale == null)
                return;
                
            // Clear current cart first
            ClearCart();
            
            // Restore customer if any
            if (heldSale.CustomerID > 0)
            {
                SelectCustomer(heldSale.CustomerID);
            }
            
            // Restore discount
            DiscountAmount = heldSale.DiscountAmount;
            
            // Restore cart items
            foreach (var item in heldSale.Items)
            {
                var product = GetProductById(item.ProductID);
                if (product != null && product.IsActive)
                {
                    var newItem = new SaleItem
                    {
                        ItemID = _nextSaleItemId++,
                        ProductID = item.ProductID,
                        ProductName = item.ProductName,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice,
                        DiscountPercent = item.DiscountPercent
                    };
                    
                    _currentSaleItems.Add(newItem);
                }
            }
            
            // Remove from held sales
            _heldSales.Remove(heldSale);
        }
        
        public void VoidTransaction()
        {
            ClearCart();
            ClearSelectedCustomer();
        }
        
        private void UpdateInventoryAfterSale(Sale sale)
        {
            foreach (var item in sale.Items)
            {
                var product = GetProductById(item.ProductID);
                if (product != null)
                {
                    product.UnitsInStock -= (int)item.Quantity;
                }
            }
        }
        #endregion
        
        #region Product Management
        
        public Product AddProduct(Product product)
        {
            // Generate a new ID
            int categoryPrefix = product.CategoryID * 100;
            int nextId = _products.Where(p => p.CategoryID == product.CategoryID)
                                 .Select(p => p.ProductID)
                                 .DefaultIfEmpty(categoryPrefix)
                                 .Max() + 1;
            
            // Ensure ID follows category prefix pattern
            if (nextId < categoryPrefix)
                nextId = categoryPrefix + 1;
                
            // Set default values
            product.ProductID = nextId;
            product.CreatedDate = DateTime.Now;
            product.ModifiedDate = DateTime.Now;
            product.IsActive = true;
            
            _products.Add(product);
            return product;
        }
        
        public bool UpdateProduct(Product product)
        {
            var existingProduct = _products.FirstOrDefault(p => p.ProductID == product.ProductID);
            if (existingProduct == null)
                return false;
                
            existingProduct.SKU = product.SKU;
            existingProduct.Barcode = product.Barcode;
            existingProduct.ProductName = product.ProductName;
            existingProduct.Description = product.Description;
            existingProduct.CategoryID = product.CategoryID;
            existingProduct.CategoryName = product.CategoryName;
            existingProduct.SellingPrice = product.SellingPrice;
            existingProduct.CostPrice = product.CostPrice;
            existingProduct.IsActive = product.IsActive;
            existingProduct.UnitsInStock = product.UnitsInStock;
            existingProduct.ModifiedDate = DateTime.Now;
            
            return true;
        }
        
        public bool DeactivateProduct(int productId)
        {
            var product = _products.FirstOrDefault(p => p.ProductID == productId);
            if (product == null)
                return false;
                
            product.IsActive = false;
            product.ModifiedDate = DateTime.Now;
            
            return true;
        }
        
        public void UpdateProductStock(int productId, int newQuantity)
        {
            if (newQuantity < 0)
                return;
                
            var product = _products.FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                product.UnitsInStock = newQuantity;
                product.ModifiedDate = DateTime.Now;
            }
        }
        
        public void AdjustProductStock(int productId, int adjustment)
        {
            var product = _products.FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                int newStock = product.UnitsInStock + adjustment;
                if (newStock >= 0)
                {
                    product.UnitsInStock = newStock;
                    product.ModifiedDate = DateTime.Now;
                }
            }
        }
        #endregion
        
        #region Category Management
        
        public Category AddCategory(Category category)
        {
            // Generate a new ID
            int newId = _categories.Count > 0 ? _categories.Max(c => c.CategoryID) + 1 : 1;
            
            // Set default values
            category.CategoryID = newId;
            category.IsActive = true;
            
            _categories.Add(category);
            return category;
        }
        
        public bool UpdateCategory(Category category)
        {
            var existingCategory = _categories.FirstOrDefault(c => c.CategoryID == category.CategoryID);
            if (existingCategory == null)
                return false;
                
            existingCategory.CategoryName = category.CategoryName;
            existingCategory.Description = category.Description;
            existingCategory.IsActive = category.IsActive;
            
            return true;
        }
        
        public bool DeactivateCategory(int categoryId)
        {
            var category = _categories.FirstOrDefault(c => c.CategoryID == categoryId);
            if (category == null)
                return false;
                
            category.IsActive = false;
            
            return true;
        }
        #endregion
    }
    
    // Sale class to represent a completed transaction
    public class Sale
    {
        public int SaleID { get; set; }
        public DateTime SaleDate { get; set; }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Subtotal { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal Change { get; set; }
        public List<SaleItem> Items { get; set; }
        public bool IsHeld { get; set; }
        public string Status => IsHeld ? "Held" : "Completed";
        
        public Sale()
        {
            Items = new List<SaleItem>();
        }
    }
}