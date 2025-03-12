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
using System.Runtime.InteropServices;
namespace FantasticStock.Views.Inventory
{

    public partial class ProductManagementView : UserControl
    {
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);
        private const int EM_SETCUEBANNER = 0x1501;
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
        */
        }
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
            if(ProductID > 0)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete this product?",
                    "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    DeleteProduct(ProductID);
                    ClearDetails();
                    Khoa_Chinh_Sua(false);
                    Load_dvgProducts();
                }
            }
        }
        private void DeleteProduct(int ProductID)
        {
            using(conn = new SqlConnection(chuoiketnoi))
            {
                conn.Open();
                string query = "Delete from Product where ProductID = @ProductID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ProductID", ProductID);
                cmd.ExecuteNonQuery();
            }
            MessageBox.Show("Product deleted successfully.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void ClearDetails()
        {
            ProductID = 0;
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
            SetCueText(txtSearch, "Search by product name");
        }
        private void SetCueText(TextBox textBox, string cueText)
        {
            SendMessage(textBox.Handle, EM_SETCUEBANNER, 1, cueText);
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
            catch (Exception ex)
            {
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
                    dgvProducts.DataSource = dt;
                    dgvProducts.Columns["dgvProductID"].DataPropertyName = "ProductID";
                    dgvProducts.Columns["dgvName"].DataPropertyName = "ProductName";
                    dgvProducts.Columns["dgvCg"].DataPropertyName = "CategoryID";
                    dgvProducts.Columns["dgvSupp"].DataPropertyName = "SupplierID";
                    dgvProducts.Columns["dgvPrice"].DataPropertyName = "SellingPrice";
                    dgvProducts.Columns["dgvQtt"].DataPropertyName = "StockQuantity";
                    dgvProducts.Columns["dgvRL"].DataPropertyName = "ReorderLevel";
                    //dgvProducts.DataSource = dt;
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
            using (conn = new SqlConnection(chuoiketnoi))
            {
                conn.Open();
                SqlCommand cmd;
                string query;
                if (ProductID > 0)
                {
                    query = "update Product set SKU = @SKU, Barcode = @Barcode, ProductName = @ProductName, Description = @Description, CategoryID = @CategoryID,SupplierID = @SupplierID, CostPrice = @CostPrice, SellingPrice = @SellingPrice, StockQuantity = @StockQuantity, ReorderLevel = @ReorderLevel, ProductImage = @ProductImage, ModifiedDate = @ModifiedDate where ProductID = @ProductID";


                    cmd = new SqlCommand(query, conn);
                }
                else
                {
                    query = "insert into Product (SKU, Barcode, ProductName, Description, CategoryID, SupplierID, CostPrice, SellingPrice, StockQuantity, ReorderLevel, ProductImage, CreatedDate) values(@SKU, @Barcode, @ProductName, @Description, @CategoryID, @SupplierID, @CostPrice, @SellingPrice, @StockQuantity, @ReorderLevel, @ProductImage, @CreatedDate)";
                    cmd = new SqlCommand(query, conn);
                    cmd = new SqlCommand(query, conn);
                }

                cmd.Parameters.AddWithValue("@ProductID", ProductID);
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
                if(ProductID == 0) cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                else cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                cmd.ExecuteNonQuery();
                Load_dvgProducts();
                string mes = ProductID > 0 ? "Updated successfully" : "Product saved successfully";
                MessageBox.Show(mes, "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            ProductID = 0;
            
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
            if (sender is TextBox textbox)
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
        private int ProductID = 0;
        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            else
            {
                DataRowView drv = dgvProducts.Rows[e.RowIndex].DataBoundItem as DataRowView;
                if (drv != null)
                {
                    ProductID = Convert.ToInt32(drv["ProductID"]);
                    txtProductName.Text = drv["ProductName"].ToString();
                    txtSKU.Text = drv["SKU"].ToString();
                    txtBarcode.Text = drv["Barcode"].ToString();
                    cmbCategory.SelectedValue = drv["CategoryID"];
                    cmbSupplier.SelectedValue = drv["SupplierID"];
                    txtSalePrice.Text = drv["SellingPrice"].ToString();
                    txtCostPrice.Text = drv["CostPrice"].ToString();
                    txtMarkup.Text = ((Convert.ToDecimal(drv["SellingPrice"]) - Convert.ToDecimal(drv["CostPrice"])) / Convert.ToDecimal(drv["CostPrice"]) * 100).ToString();
                    txtQuantityInStock.Text = drv["StockQuantity"].ToString();
                    txtReorderLevel.Text = drv["ReorderLevel"].ToString();
                    txtDescription.Text = drv["Description"].ToString();
                    if (drv["ProductImage"] != DBNull.Value)
                    {
                        byte[] img = (byte[])drv["ProductImage"];
                        MemoryStream ms = new MemoryStream(img);
                        pictureBoxProductImage.Image = Image.FromStream(ms);
                    }
                    else
                    {
                        pictureBoxProductImage.Image = null;
                    }
                }
                if (e.ColumnIndex == dgvProducts.Columns["Actions"].Index)
                {
                    Khoa_Chinh_Sua(true);
                }
                else
                {
                    Khoa_Chinh_Sua(false);
                }

            }
        }

        private void txtSearch_TextChanged_1(object sender, EventArgs e)
        {
            string textsearch = txtSearch.Text.Trim();
            using(conn = new SqlConnection(chuoiketnoi))
            {
                conn.Open();
                string query = "select * from Product p join Category c on p.CategoryID = c.CategoryID join Supplier s on p.SupplierID = s.SupplierID where ProductName like @textsearch";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@textsearch","%"+ textsearch + "%");
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                dgvProducts.DataSource = dt;
                dgvProducts.Columns["dgvProductID"].DataPropertyName = "ProductID";
                dgvProducts.Columns["dgvName"].DataPropertyName = "ProductName";
                dgvProducts.Columns["dgvCg"].DataPropertyName = "CategoryID";
                dgvProducts.Columns["dgvSupp"].DataPropertyName = "SupplierID";
                dgvProducts.Columns["dgvPrice"].DataPropertyName = "SellingPrice";
                dgvProducts.Columns["dgvQtt"].DataPropertyName = "StockQuantity";
                dgvProducts.Columns["dgvRL"].DataPropertyName = "ReorderLevel";
                //dgvProducts.DataSource = dt;
                DataGridViewButtonColumn actionsColumn = dgvProducts.Columns["Actions"] as DataGridViewButtonColumn;
                if (actionsColumn != null)
                {
                    actionsColumn.UseColumnTextForButtonValue = true;
                    actionsColumn.Text = "✏️";
                }
            }
        }

        private void dgvProducts_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            using (var row = dgvProducts.Rows[e.RowIndex])
            {
                
                var stockQuantity = Convert.ToInt32(row.Cells["dgvQtt"].Value);
                var reorderLevel = Convert.ToInt32(row.Cells["dgvRL"].Value);

                if (stockQuantity < reorderLevel)
                {
                    row.Cells["dgvQtt"].Style.BackColor = Color.Orange; // Đổi màu nền thành màu vàng
                }
            }
            
        }
    }
}
