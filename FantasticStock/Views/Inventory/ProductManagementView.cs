using FantasticStock.ViewModels.Inventory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FantasticStock.Views.Inventory
{
    public partial class ProductManagementView : UserControl
    {
        private ProductViewModel _viewModel;

        public ProductManagementView()
        {
            InitializeComponent();

            // Initialize view model
            _viewModel = new ProductViewModel();

            // Set up data bindings
            SetupBindings();
        }

        private void SetupBindings()
        {/*
            // Bind products grid
            dgvProducts.DataSource = _viewModel.Products;

            // Bind categories
            cmbCategory.DataSource = _viewModel.Categories;
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.ValueMember = "CategoryID";
            cmbCategoryFilter.DataSource = _viewModel.Categories;
            cmbCategoryFilter.DisplayMember = "CategoryName";
            cmbCategoryFilter.ValueMember = "CategoryID";

            // Bind suppliers
            cmbSupplier.DataSource = _viewModel.Suppliers;
            cmbSupplier.DisplayMember = "CompanyName";
            cmbSupplier.ValueMember = "SupplierID";
            cmbSupplierFilter.DataSource = _viewModel.Suppliers;
            cmbSupplierFilter.DisplayMember = "CompanyName";
            cmbSupplierFilter.ValueMember = "SupplierID";

            // Bind inventory grid
            dgvInventory.DataSource = _viewModel.InventoryItems;

            // Bind filter controls
            txtSearch.DataBindings.Add("Text", _viewModel, "SearchText", true, DataSourceUpdateMode.OnPropertyChanged);
            cmbCategoryFilter.DataBindings.Add("SelectedValue", _viewModel, "CategoryFilter", true, DataSourceUpdateMode.OnPropertyChanged);
            cmbSupplierFilter.DataBindings.Add("SelectedValue", _viewModel, "SupplierFilter", true, DataSourceUpdateMode.OnPropertyChanged);

            // Setup active filter
            cmbActiveFilter.Items.Add(new { Text = "All", Value = (bool?)null });
            cmbActiveFilter.Items.Add(new { Text = "Active Only", Value = (bool?)true });
            cmbActiveFilter.Items.Add(new { Text = "Inactive Only", Value = (bool?)false });
            cmbActiveFilter.DisplayMember = "Text";
            cmbActiveFilter.ValueMember = "Value";
            cmbActiveFilter.SelectedIndex = 0;
            cmbActiveFilter.DataBindings.Add("SelectedValue", _viewModel, "ActiveFilter", true, DataSourceUpdateMode.OnPropertyChanged);

            // Bind product details fields
            txtSKU.DataBindings.Add("Text", _viewModel, "SelectedProduct.SKU", true, DataSourceUpdateMode.OnPropertyChanged);
            txtBarcode.DataBindings.Add("Text", _viewModel, "SelectedProduct.Barcode", true, DataSourceUpdateMode.OnPropertyChanged);
            txtProductName.DataBindings.Add("Text", _viewModel, "SelectedProduct.ProductName", true, DataSourceUpdateMode.OnPropertyChanged);
            txtDescription.DataBindings.Add("Text", _viewModel, "SelectedProduct.Description", true, DataSourceUpdateMode.OnPropertyChanged);
            cmbCategory.DataBindings.Add("SelectedValue", _viewModel, "SelectedProduct.CategoryID", true, DataSourceUpdateMode.OnPropertyChanged);
            cmbSupplier.DataBindings.Add("SelectedValue", _viewModel, "SelectedProduct.SupplierID", true, DataSourceUpdateMode.OnPropertyChanged);
            txtCostPrice.DataBindings.Add("Text", _viewModel, "SelectedProduct.CostPrice", true, DataSourceUpdateMode.OnPropertyChanged);
            txtSellingPrice.DataBindings.Add("Text", _viewModel, "SelectedProduct.SellingPrice", true, DataSourceUpdateMode.OnPropertyChanged);
            txtWholesalePrice.DataBindings.Add("Text", _viewModel, "SelectedProduct.WholesalePrice", true, DataSourceUpdateMode.OnPropertyChanged);
            txtReorderLevel.DataBindings.Add("Text", _viewModel, "SelectedProduct.ReorderLevel", true, DataSourceUpdateMode.OnPropertyChanged);
            txtTargetStockLevel.DataBindings.Add("Text", _viewModel, "SelectedProduct.TargetStockLevel", true, DataSourceUpdateMode.OnPropertyChanged);
            txtUnitOfMeasure.DataBindings.Add("Text", _viewModel, "SelectedProduct.UnitOfMeasure", true, DataSourceUpdateMode.OnPropertyChanged);
            chkManageStock.DataBindings.Add("Checked", _viewModel, "SelectedProduct.ManageStock", true, DataSourceUpdateMode.OnPropertyChanged);
            chkAllowFractional.DataBindings.Add("Checked", _viewModel, "SelectedProduct.AllowFractionalQuantity", true, DataSourceUpdateMode.OnPropertyChanged);
            chkActive.DataBindings.Add("Checked", _viewModel, "SelectedProduct.IsActive", true, DataSourceUpdateMode.OnPropertyChanged);

            // Bind commands to buttons
            btnAddProduct.Click += (s, e) => _viewModel.AddProductCommand.Execute(null);
            btnEditProduct.Click += (s, e) => _viewModel.EditProductCommand.Execute(null);
            btnSaveProduct.Click += (s, e) => _viewModel.SaveProductCommand.Execute(null);
            btnDeleteProduct.Click += (s, e) => _viewModel.DeleteProductCommand.Execute(null);
            btnRefresh.Click += (s, e) => _viewModel.RefreshDataCommand.Execute(null);
            btnFilter.Click += (s, e) => _viewModel.FilterProductsCommand.Execute(null);
            btnBrowseImage.Click += (s, e) => _viewModel.BrowseImageCommand.Execute(null);
            btnUpdateStock.Click += (s, e) => _viewModel.UpdateStockCommand.Execute(null);
            btnTransactionHistory.Click += (s, e) => _viewModel.ViewTransactionHistoryCommand.Execute(null);

            // Selection change events
            dgvProducts.SelectionChanged += (s, e) =>
            {
                if (dgvProducts.SelectedRows.Count > 0)
                {
                    _viewModel.SelectedProduct = dgvProducts.SelectedRows[0].DataBoundItem as FantasticStock.Models.Inventory.Product;
                }
            };

            dgvInventory.SelectionChanged += (s, e) =>
            {
                if (dgvInventory.SelectedRows.Count > 0)
                {
                    _viewModel.SelectedInventoryItem = dgvInventory.SelectedRows[0].DataBoundItem as FantasticStock.Models.Inventory.ProductInventory;
                }
            };

            // Handle editing mode changes
            _viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "IsEditing")
                {
                    //pnlProductDetails.Enabled = _viewModel.IsEditing;
                    //btnSaveProduct.Enabled = _viewModel.IsEditing;
                    //btnAddProduct.Enabled = !_viewModel.IsEditing;
                    //btnEditProduct.Enabled = !_viewModel.IsEditing;
                    //btnDeleteProduct.Enabled = !_viewModel.IsEditing;
                }
            };
        */}
    }
}
