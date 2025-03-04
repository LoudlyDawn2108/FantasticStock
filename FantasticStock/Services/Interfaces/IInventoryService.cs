using System;
using System.Collections.Generic;
using FantasticStock.Models.Inventory;

namespace FantasticStock.Services
{
    public interface IInventoryService
    {
                // Product methods
        List<Product> GetAllProducts();
        Product GetProductById(int productId);
        Product GetProductBySKU(string sku);
        List<Product> SearchProducts(string searchQuery, int? categoryId, int? supplierId, bool? isActive);
        bool AddProduct(Product product);
        bool UpdateProduct(Product product);
        bool DeleteProduct(int productId);
        
        // Category methods
        List<Category> GetAllCategories();
        Category GetCategoryById(int categoryId);
        bool AddCategory(Category category);
        bool UpdateCategory(Category category);
        bool DeleteCategory(int categoryId);
        
        // Supplier methods
        List<Supplier> GetAllSuppliers();
        Supplier GetSupplierById(int supplierId);
        bool AddSupplier(Supplier supplier);
        bool UpdateSupplier(Supplier supplier);
        bool DeleteSupplier(int supplierId);
        
        // Inventory methods
        ProductInventory GetProductInventory(int productId, int locationId);
        List<ProductInventory> GetInventoryByProduct(int productId);
        List<ProductInventory> GetInventoryByLocation(int locationId);
        List<ProductInventory> GetLowStockItems();
        bool UpdateInventoryQuantity(int productId, int locationId, decimal newQuantity, string transactionType, string notes, int userId);
        
        // Inventory transactions
        List<InventoryTransaction> GetTransactionHistory(int? productId, DateTime? startDate, DateTime? endDate, string transactionType);
        bool AddInventoryTransaction(InventoryTransaction transaction);
        
        // Reports
        byte[] GenerateInventoryReport(DateTime startDate, DateTime endDate, int? categoryId, int? supplierId);
        byte[] GenerateStockLevelReport(int? locationId);
        byte[] GenerateInventoryValuationReport();
    }
}