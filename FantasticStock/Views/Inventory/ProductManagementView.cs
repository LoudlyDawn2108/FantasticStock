using FantasticStock.ViewModels.Inventory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FantasticStock.Views.Inventory
{
    
    public partial class ProductManagementView : UserControl
    {
        private ProductViewModel _viewModel;
        
        string chuoiketnoi = "Data Source=TUNGCORN\\SQLEXPRESS;" +
                             "Initial Catalog = Product;" +
                             "Integrated Security = true";
        SqlConnection conn;
        //public ProductManagementView()
        //{
        //    InitializeComponent();

        //    // Initialize view model
        //    _viewModel = new ProductViewModel();

        //    // Set up data bindings
        //    SetupBindings();
        //}
        
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
        #region Event Handlers
        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            ClearDetails();
            Khoa_Chinh_Sua(true);
        }

        private void btnBulkImport_Click(object sender, EventArgs e)
        {
            // TODO: Implement bulk import functionality
            MessageBox.Show("Bulk import functionality not yet implemented.", "Information",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            // TODO: Implement export functionality
            MessageBox.Show("Export functionality not yet implemented.", "Information",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnPrintList_Click(object sender, EventArgs e)
        {
            // TODO: Implement print functionality
            MessageBox.Show("Print functionality not yet implemented.", "Information",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            
        }

        //private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.RowIndex < 0) return;

        //    if (e.ColumnIndex == dgvProducts.Columns["colEdit"].Index)
        //    {
        //        LoadProductDetails(dgvProducts.Rows[e.RowIndex].Cells["colProductID"].Value.ToString());
        //        EnableDetailsEditing(true);
        //    }
        //    else if (e.ColumnIndex == dgvProducts.Columns["colDelete"].Index)
        //    {
        //        string productId = dgvProducts.Rows[e.RowIndex].Cells["colProductID"].Value.ToString();
        //        DeleteProduct(productId);
        //    }
        //    else
        //    {
        //        LoadProductDetails(dgvProducts.Rows[e.RowIndex].Cells["colProductID"].Value.ToString());
        //        EnableDetailsEditing(false);
        //    }
        //}

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            // TODO: Implement pagination
            MessageBox.Show("Previous page functionality not yet implemented.", "Information",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            // TODO: Implement pagination
            MessageBox.Show("Next page functionality not yet implemented.", "Information",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnUploadImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
                openFileDialog.Title = "Select Product Image";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        pictureBoxProductImage.Image = Image.FromFile(openFileDialog.FileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error loading image: " + ex.Message, "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnRemoveImage_Click(object sender, EventArgs e)
        {
            pictureBoxProductImage.Image = null;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (KiemTraGiaTri())
            {
                SaveProduct();
                decimal SaleP = Convert.ToDecimal(txtSalePrice.Text);
                decimal CostP = Convert.ToDecimal(txtCostPrice.Text);
                decimal Markup = ((SaleP - CostP) / CostP) * 100;
                txtMarkup.Text = Markup.ToString();
                Khoa_Chinh_Sua(false);

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearDetails();
            Khoa_Chinh_Sua(false);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSKU.Text))
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete this product?",
                    "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    DeleteProduct(txtSKU.Text);
                    ClearDetails();
                    //EnableDetailsEditing(false);
                    
                }
            }
        }

        private void txtCostPrice_TextChanged(object sender, EventArgs e)
        {
            CalculateMarkup();
        }

        private void txtSalePrice_TextChanged(object sender, EventArgs e)
        {
            CalculateMarkup();
        }

        #endregion

        #region Helper Methods

        //private void LoadProducts()
        //{
        //    // TODO: Replace with actual data loading from database
        //    dgvProducts.Rows.Clear();

        //    // Sample data
        //    dgvProducts.Rows.Add("P001", "Laptop Pro 15\"", "Electronics", "TechSuppliers Inc.", "$1,299.99", "24", "10");
        //    dgvProducts.Rows.Add("P002", "Wireless Mouse", "Accessories", "TechSuppliers Inc.", "$29.99", "45", "15");
        //    dgvProducts.Rows.Add("P003", "Bluetooth Headphones", "Audio", "AudioTech Ltd.", "$89.99", "18", "20");
        //    dgvProducts.Rows.Add("P004", "4K Monitor 27\"", "Electronics", "VisualTech Corp", "$349.99", "5", "8");
        //    dgvProducts.Rows.Add("P005", "USB-C Hub", "Accessories", "ConnectAll Inc.", "$49.99", "30", "10");
        //    dgvProducts.Rows.Add("P006", "Wireless Keyboard", "Accessories", "TechSuppliers Inc.", "$59.99", "7", "15");
        //}

        //private void FilterProducts(string searchTerm)
        //{
        //    // TODO: Implement filtering logic
        //    if (string.IsNullOrEmpty(searchTerm))
        //    {
        //        LoadProducts();
        //        return;
        //    }

        //    // For now, just reload all products
        //    // In a real implementation, this would filter based on the search term
        //    LoadProducts();
        //}

        //private void LoadProductDetails(string productId)
        //{
        //    // TODO: Replace with actual data loading from database
        //    // For now, just load sample data
        //    if (productId == "P001")
        //    {
        //        txtProductName.Text = "Laptop Pro 15\"";
        //        txtSKU.Text = "LAP-PRO-15";
        //        txtBarcode.Text = "7891234567890";
        //        cmbCategory.Text = "Electronics";
        //        cmbSupplier.Text = "TechSuppliers Inc.";
        //        txtSalePrice.Text = "1299.99";
        //        txtCostPrice.Text = "950.00";
        //        txtQuantityInStock.Text = "24";
        //        txtReorderLevel.Text = "10";
        //        txtDescription.Text = "15-inch professional laptop with Intel Core i7, 16GB RAM, 512GB SSD, and NVIDIA GeForce RTX 2050 graphics.";
        //    }
        //}

        private void ClearDetails()
        {
            txtProductName.Text = string.Empty;
            txtSKU.Text = string.Empty;
            txtBarcode.Text = string.Empty;
            cmbCategory.SelectedIndex = -1;
            cmbSupplier.SelectedIndex = -1;
            txtSalePrice.Text = "0.00";
            txtCostPrice.Text = "0.00";
            txtMarkup.Text = "0.00";
            txtQuantityInStock.Text = "0";
            txtReorderLevel.Text = "0";
            txtDescription.Text = string.Empty;
            pictureBoxProductImage.Image = null;
        }

        private void Khoa_Chinh_Sua(bool enable)
        {
            txtProductName.ReadOnly = !enable;
            txtSKU.ReadOnly = !enable;
            txtBarcode.ReadOnly = !enable;
            cmbCategory.Enabled = enable;
            cmbSupplier.Enabled = enable;
            txtSalePrice.ReadOnly = !enable;
            txtCostPrice.ReadOnly = !enable;
            txtQuantityInStock.ReadOnly = !enable;
            txtReorderLevel.ReadOnly = !enable;
            txtDescription.ReadOnly = !enable;
            btnUploadImage.Enabled = enable;
            btnRemoveImage.Enabled = enable;
            btnSave.Enabled = enable;
            btnCancel.Enabled = enable;
            btnDelete.Enabled = enable;
        }

        private bool KiemTraGiaTri()
        {
            if (string.IsNullOrEmpty(txtProductName.Text))
            {
                MessageBox.Show("Product name is required.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrEmpty(txtSKU.Text))
            {
                MessageBox.Show("SKU is required.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrEmpty(txtSalePrice.Text) || !decimal.TryParse(txtSalePrice.Text, out _))
            {
                MessageBox.Show("Sale price must be a valid number.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrEmpty(txtCostPrice.Text) || !decimal.TryParse(txtCostPrice.Text, out _))
            {
                MessageBox.Show("Cost price must be a valid number.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrEmpty(txtQuantityInStock.Text) || !int.TryParse(txtQuantityInStock.Text, out _))
            {
                MessageBox.Show("Quantity must be a valid number.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrEmpty(txtReorderLevel.Text) || !int.TryParse(txtReorderLevel.Text, out _))
            {
                MessageBox.Show("Reorder level must be a valid number.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void ProductManagementView_Load(object sender, EventArgs e)
        {
            dgvProducts.AutoGenerateColumns = false;

            Load_dvgProducts();
            Load_comboboxCategory();
            Load_cmbSupplier();
        }
        private void Load_comboboxCategory()
        {
            try
            {
                using (conn = new SqlConnection(chuoiketnoi))
                {
                    conn.Open();
                    string query = "select CategoryID, CategoryName from Category";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    cmbCategory.DataSource = dt;
                    cmbCategory.DisplayMember = "CategoryName";
                    cmbCategory.ValueMember = "CategoryID";
                }
            }
            catch (Exception ex) {
                MessageBox.Show("Không kết nối được database");

            }
        }
        private void Load_cmbSupplier()
        {

            try
            {
                using (conn = new SqlConnection(chuoiketnoi))
                {
                    conn.Open();
                    string query = "select SupplierID, ContactName from Supplier";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    cmbSupplier.DataSource = dt;
                    cmbSupplier.DisplayMember = "ContactName";
                    cmbSupplier.ValueMember = "SupplierID";
                }
            }
            catch
            {
                MessageBox.Show("Không kết nối được database");
            }
        }
        private void Load_dvgProducts()
        {
            try
            {
                using (conn = new SqlConnection(chuoiketnoi))
                {
                    conn.Open();
                    string query = "select * from Product p join Category c on p.CategoryID = c.CategoryID join Supplier s on p.SupplierID = s.SupplierID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    //dgvProducts.DataSource = dt;
                    dgvProducts.Columns["dgvProductID"].DataPropertyName = "ProductID";
                    dgvProducts.Columns["dgvName"].DataPropertyName = "ProductName";
                    dgvProducts.Columns["dgvCg"].DataPropertyName = "CategoryID";
                    dgvProducts.Columns["dgvSupp"].DataPropertyName = "SupplierID";
                    dgvProducts.Columns["dgvPrice"].DataPropertyName = "SellingPrice";
                    dgvProducts.Columns["dgvQtt"].DataPropertyName = "StockQuantity";
                    dgvProducts.Columns["dgvRL"].DataPropertyName = "ReorderLevel";
                    dgvProducts.DataSource = dt;
                    DataGridViewButtonColumn actionsColumn = dgvProducts.Columns["Actions"] as DataGridViewButtonColumn;
                    if (actionsColumn != null)
                    {
                        actionsColumn.UseColumnTextForButtonValue = true;
                        actionsColumn.Text = "✏️";
                    }

                }
            }
            catch
            {
                MessageBox.Show("Không kết nối được database");
            }
            
        }
        private byte[] ImageToByteArray(Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }

        private void SaveProduct()
        {
            using(conn = new SqlConnection(chuoiketnoi))
            {
                conn.Open();
                string query = "insert into Product (SKU, Barcode, ProductName, Description, CategoryID, SupplierID, CostPrice, SellingPrice, StockQuantity, ReorderLevel, ProductImage, CreatedDate, ModifiedDate) values(@SKU, @Barcode, @ProductName, @Description, @CategoryID, @SupplierID, @CostPrice, @SellingPrice, @StockQuantity, @ReorderLevel, @ProductImage, @CreatedDate, @ModifiedDate)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SKU", txtSKU.Text);
                cmd.Parameters.AddWithValue("@Barcode", txtBarcode.Text);
                cmd.Parameters.AddWithValue("@ProductName", txtProductName.Text);
                cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                cmd.Parameters.AddWithValue("@CategoryID", cmbCategory.SelectedValue);
                cmd.Parameters.AddWithValue("@SupplierID", cmbSupplier.SelectedValue);
                cmd.Parameters.AddWithValue("@CostPrice", txtCostPrice.Text);
                cmd.Parameters.AddWithValue("@SellingPrice", txtSalePrice.Text);
                cmd.Parameters.AddWithValue("@StockQuantity", txtQuantityInStock.Text);            
                cmd.Parameters.AddWithValue("@ReorderLevel", txtReorderLevel.Text);
                if (pictureBoxProductImage.Image == null) 
                    cmd.Parameters.Add("@ProductImage", SqlDbType.VarBinary).Value = DBNull.Value;
                else cmd.Parameters.AddWithValue("@ProductImage", ImageToByteArray(pictureBoxProductImage.Image));
                cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@CreatedBy", 1);
                cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@ModifiedBy", 1);
                cmd.ExecuteNonQuery();
                Load_dvgProducts();
            }
            
            MessageBox.Show("Product saved successfully.", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void DeleteProduct(string productId)
        {
            // TODO: Implement actual delete functionality from database
            DialogResult result = MessageBox.Show($"Are you sure you want to delete product {productId}?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                MessageBox.Show("Product deleted successfully.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
              
            }
        }

        private void CalculateMarkup()
        {
            if (decimal.TryParse(txtSalePrice.Text, out decimal salePrice) &&
                decimal.TryParse(txtCostPrice.Text, out decimal costPrice) &&
                costPrice > 0)
            {
                decimal markup = ((salePrice - costPrice) / costPrice) * 100;
                txtMarkup.Text = markup.ToString("F2");
            }
            else
            {
                txtMarkup.Text = "0.00";
            }
        }

        //private void LoadCategories()
        //{
        //    // TODO: Replace with actual data loading from database
        //    cmbCategory.Items.Clear();
        //    cmbCategory.Items.Add("Electronics");
        //    cmbCategory.Items.Add("Accessories");
        //    cmbCategory.Items.Add("Audio");
        //    cmbCategory.Items.Add("Peripherals");
        //    cmbCategory.Items.Add("Storage");
        //    cmbCategory.Items.Add("Networking");
        //}

        

        #endregion
        private void lblDescription_Click(object sender, EventArgs e)
        {

        }

        private void txtProductName_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblProductName_Click(object sender, EventArgs e)
        {

        }

        private void txtBarcode_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblBarcode_Click(object sender, EventArgs e)
        {

        }

        private void txtSKU_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblSKU_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_SizeChanged(object sender, EventArgs e)
        {
            if(sender is TextBox textbox)
            {
                textbox.Width = 40;
            }
        }

        private void flowLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {

        }

        
    }
}
