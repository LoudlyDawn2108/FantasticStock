using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using FantasticStock.Models.Inventory;
using FantasticStock.Services.Admin;
using FantasticStock.Common;

namespace FantasticStock.ViewModels.Inventory
{
    public class ProductViewModel : ViewModelBase
    {/*
        private readonly IInventoryService _inventoryService;
        
        // Lists for data binding
        private BindingList<Product> _products;
        private BindingList<Category> _categories;
        private BindingList<Supplier> _suppliers;
        private BindingList<ProductInventory> _inventoryItems;
        
        // Selected items
        private Product _selectedProduct;
        private Category _selectedCategory;
        private Supplier _selectedSupplier;
        private ProductInventory _selectedInventoryItem;
        
        // Filter properties
        private string _searchText;
        private int? _categoryFilter;
        private int? _supplierFilter;
        private bool? _activeFilter;
        
        // Commands
        public ICommand AddProductCommand { get; private set; }
        public ICommand EditProductCommand { get; private set; }
        public ICommand SaveProductCommand { get; private set; }
        public ICommand DeleteProductCommand { get; private set; }
        public ICommand RefreshDataCommand { get; private set; }
        public ICommand FilterProductsCommand { get; private set; }
        public ICommand BrowseImageCommand { get; private set; }
        public ICommand UpdateStockCommand { get; private set; }
        public ICommand ViewTransactionHistoryCommand { get; private set; }
        
        public ProductViewModel()
        {
            _inventoryService = ServiceLocator.GetService<IInventoryService>();
            
            // Initialize commands
            AddProductCommand = new RelayCommand(AddProduct);
            EditProductCommand = new RelayCommand(EditProduct, CanEditProduct);
            SaveProductCommand = new RelayCommand(SaveProduct, CanSaveProduct);
            DeleteProductCommand = new RelayCommand(DeleteProduct, CanDeleteProduct);
            RefreshDataCommand = new RelayCommand(LoadData);
            FilterProductsCommand = new RelayCommand(FilterProducts);
            BrowseImageCommand = new RelayCommand(BrowseImage);
            UpdateStockCommand = new RelayCommand(UpdateStock, CanUpdateStock);
            ViewTransactionHistoryCommand = new RelayCommand(ViewTransactionHistory, CanViewTransactionHistory);
            
            // Load initial data
            LoadData();
        }
        
        private void LoadData()
        {
            // Load products, categories, suppliers
            var products = _inventoryService.GetAllProducts();
            var categories = _inventoryService.GetAllCategories();
            var suppliers = _inventoryService.GetAllSuppliers();
            
            Products = new BindingList<Product>(products);
            Categories = new BindingList<Category>(categories);
            Suppliers = new BindingList<Supplier>(suppliers);
            
            // Clear selections
            SelectedProduct = null;
        }
        
        private void AddProduct()
        {
            IsEditing = true;
            
            // Create a new product
            SelectedProduct = new Product
            {
                IsActive = true,
                CreatedDate = DateTime.Parse("2025-03-03 16:32:51"),
                ModifiedDate = DateTime.Parse("2025-03-03 16:32:51"),
                CreatedBy = CurrentUser.UserID,
                ModifiedBy = CurrentUser.UserID
            };
        }
        
        private void EditProduct()
        {
            if (SelectedProduct == null)
                return;
                
            IsEditing = true;
        }
        
        private bool CanEditProduct()
        {
            return SelectedProduct != null;
        }
        
        private void SaveProduct()
        {
            if (SelectedProduct == null)
                return;
                
            bool success;
            
            // Determine if this is a new or existing product
            if (SelectedProduct.ProductID == 0)
            {
                // New product
                success = _inventoryService.AddProduct(SelectedProduct);
            }
            else
            {
                // Update existing product
                SelectedProduct.ModifiedDate = DateTime.Parse("2025-03-03 16:32:51");
                SelectedProduct.ModifiedBy = CurrentUser.UserID;
                success = _inventoryService.UpdateProduct(SelectedProduct);
            }
            
            if (success)
            {
                IsEditing = false;
                LoadData();
            }
            else
            {
                ShowError("Failed to save product.");
            }
        }
        
        private bool CanSaveProduct()
        {
            return IsEditing && SelectedProduct != null && 
                !string.IsNullOrEmpty(SelectedProduct.ProductName) &&
                !string.IsNullOrEmpty(SelectedProduct.SKU);
        }
        
        private void DeleteProduct()
        {
            if (SelectedProduct == null)
                return;
                
            if (ShowConfirmation($"Are you sure you want to delete product '{SelectedProduct.ProductName}'?"))
            {
                bool success = _inventoryService.DeleteProduct(SelectedProduct.ProductID);
                
                if (success)
                {
                    LoadData();
                }
                else
                {
                    ShowError("Failed to delete product.");
                }
            }
        }
        
        private bool CanDeleteProduct()
        {
            return SelectedProduct != null;
        }
        
        private void FilterProducts()
        {
            var filteredProducts = _inventoryService.SearchProducts(SearchText, CategoryFilter, SupplierFilter, ActiveFilter);
            Products = new BindingList<Product>(filteredProducts);
        }
        
        private void BrowseImage()
        {
            // Implementation would show a file dialog to select an image
            // For now, just show a message
            ShowMessage("Image selection dialog would appear here.");
        }
        
        private void UpdateStock()
        {
            if (SelectedProduct == null || SelectedInventoryItem == null)
                return;
                
            // Implementation would show a dialog to update stock
            // For now, just show a message
            ShowMessage("Stock update dialog would appear here.");
        }
        
        private bool CanUpdateStock()
        {
            return SelectedProduct != null && SelectedInventoryItem != null;
        }
        
        private void ViewTransactionHistory()
        {
            if (SelectedProduct == null)
                return;
                
            // Implementation would show transaction history for the selected product
            // For now, just show a message
            ShowMessage($"Transaction history for {SelectedProduct.ProductName} would appear here.");
        }
        
        private bool CanViewTransactionHistory()
        {
            return SelectedProduct != null;
        }
        
        // Properties
        public BindingList<Product> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged();
            }
        }
        
        public BindingList<Category> Categories
        {
            get => _categories;
            set
            {
                _categories = value;
                OnPropertyChanged();
            }
        }
        
        public BindingList<Supplier> Suppliers
        {
            get => _suppliers;
            set
            {
                _suppliers = value;
                OnPropertyChanged();
            }
        }
        
        public BindingList<ProductInventory> InventoryItems
        {
            get => _inventoryItems;
            set
            {
                _inventoryItems = value;
                OnPropertyChanged();
            }
        }
        
        public Product SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                OnPropertyChanged();
                
                // When a product is selected, load its inventory
                if (_selectedProduct != null)
                {
                    var inventory = _inventoryService.GetInventoryByProduct(_selectedProduct.ProductID);
                    InventoryItems = new BindingList<ProductInventory>(inventory);
                }
                else
                {
                    InventoryItems = new BindingList<ProductInventory>();
                }
                
                // Update command states
                ((RelayCommand)EditProductCommand).RaiseCanExecuteChanged();
                ((RelayCommand)DeleteProductCommand).RaiseCanExecuteChanged();
                ((RelayCommand)ViewTransactionHistoryCommand).RaiseCanExecuteChanged();
            }
        }
        
        public Category SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged();
            }
        }
        
        public Supplier SelectedSupplier
        {
            get => _selectedSupplier;
            set
            {
                _selectedSupplier = value;
                OnPropertyChanged();
            }
        }
        
        public ProductInventory SelectedInventoryItem
        {
            get => _selectedInventoryItem;
            set
            {
                _selectedInventoryItem = value;
                OnPropertyChanged();
                ((RelayCommand)UpdateStockCommand).RaiseCanExecuteChanged();
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
        
        public int? CategoryFilter
        {
            get => _categoryFilter;
            set
            {
                _categoryFilter = value;
                OnPropertyChanged();
            }
        }
        
        public int? SupplierFilter
        {
            get => _supplierFilter;
            set
            {
                _supplierFilter = value;
                OnPropertyChanged();
            }
        }
        
        public bool? ActiveFilter
        {
            get => _activeFilter;
            set
            {
                _activeFilter = value;
                OnPropertyChanged();
            }
        }
    */}
}