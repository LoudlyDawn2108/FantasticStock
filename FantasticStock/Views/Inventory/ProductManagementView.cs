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
                             "Initial Catalog = FantasticStock;" +
                             "Integrated Security = true";
        SqlConnection conn;

        private void SetupBindings()
        {
        }

        #region Event Handlers
        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            ClearDetails();
            Khoa_Chinh_Sua(true);
        }

        private void btnBulkImport_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bulk import functionality not yet implemented.", "Information",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Export functionality not yet implemented.", "Information",
            //    MessageBoxButtons.OK, MessageBoxIcon.Information);
            try
            {
                DataTable productData = dgvProducts.DataSource as DataTable;

                if (productData == null || productData.Rows.Count == 0)
                {
                    MessageBox.Show("No product data to export.", "Information",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (SaveFileDialog saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "CSV files (*.csv)|*.csv|Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                    saveDialog.Title = "Export Products";
                    saveDialog.DefaultExt = "csv";
                    saveDialog.FileName = $"Products_Export_{DateTime.Now:yyyyMMdd}";

                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = saveDialog.FileName;

                        if (Path.GetExtension(filePath).ToLower() == ".csv")
                        {
                            StringBuilder csv = new StringBuilder();
                            List<string> headers = new List<string>();
                            List<string> propertyNames = new List<string>();

                            foreach (DataGridViewColumn column in dgvProducts.Columns)
                            {
                                if (column.Visible && !(column is DataGridViewButtonColumn))
                                {
                                    headers.Add(column.HeaderText);
                                    propertyNames.Add(column.DataPropertyName);
                                }
                            }

                            headers.AddRange(new[] { "Product Name", "SKU", "Barcode", "Category", "Supplier",
                                "Cost Price", "Selling Price", "Stock Quantity",
                                "Reorder Level", "Description" });

                            csv.AppendLine(string.Join(",", headers.Select(h => $"\"{h}\"")));

                            foreach (DataRow row in productData.Rows)
                            {
                                List<string> fields = new List<string>();

                                foreach (string property in propertyNames)
                                {
                                    if (!string.IsNullOrEmpty(property) && productData.Columns.Contains(property))
                                    {
                                        fields.Add($"\"{EscapeCsvField(row[property].ToString())}\"");
                                    }
                                }

                                fields.AddRange(new[] {
                            $"\"{EscapeCsvField(row["ProductName"].ToString())}\"",
                            $"\"{EscapeCsvField(row["SKU"].ToString())}\"",
                            $"\"{EscapeCsvField(row["Barcode"].ToString())}\"",
                            $"\"{EscapeCsvField(row["CategoryName"].ToString())}\"",
                            $"\"{EscapeCsvField(row["ContactName"].ToString())}\"",
                            $"\"{row["CostPrice"]}\"",
                            $"\"{row["SellingPrice"]}\"",
                            $"\"{row["StockQuantity"]}\"",
                            $"\"{row["ReorderLevel"]}\"",
                            $"\"{EscapeCsvField(row["Description"].ToString())}\""
                        });

                                csv.AppendLine(string.Join(",", fields));
                            }

                            File.WriteAllText(filePath, csv.ToString());

                            MessageBox.Show($"Successfully exported {productData.Rows.Count} products to {filePath}",
                                "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else if (Path.GetExtension(filePath).ToLower() == ".xlsx")
                        {
                            MessageBox.Show("Excel export functionality not yet implemented.", "Information",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting products: {ex.Message}", "Export Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private string EscapeCsvField(string field)
        {
            if (string.IsNullOrEmpty(field))
                return string.Empty;

       
            return field.Replace("\"", "\"\"");
        }

        private void btnPrintList_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Print functionality not yet implemented.", "Information",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Previous page functionality not yet implemented.", "Information",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
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
                //decimal SaleP = Convert.ToDecimal(txtSalePrice.Text);
                //decimal CostP = Convert.ToDecimal(txtCostPrice.Text);
                //decimal Markup = ((SaleP - CostP) / CostP) * 100;
                //txtMarkup.Text = Markup.ToString();
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
            if (ProductID > 0)
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
            using (conn = new SqlConnection(chuoiketnoi))
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
            TinhMarkup();
        }

        private void txtSalePrice_TextChanged(object sender, EventArgs e)
        {
            TinhMarkup();
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

        private void Load_cmbSupplier()
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
                MessageBox.Show("Cannot connect to database");
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
                if (ProductID == 0) cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                else cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                cmd.ExecuteNonQuery();
                Load_dvgProducts();
                string mes = ProductID > 0 ? "Updated successfully" : "Product saved successfully";
                MessageBox.Show(mes, "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            ProductID = 0;
        }

        private void TinhMarkup()
        {
            if (decimal.TryParse(txtSalePrice.Text, out decimal salePrice) &&
                decimal.TryParse(txtCostPrice.Text, out decimal costPrice) &&
                costPrice > 0)
            {
                decimal markup = Math.Round(((salePrice - costPrice) / costPrice) * 100, 4);
                txtMarkup.Text = markup.ToString();
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
                    txtMarkup.Text = Math.Round(((Convert.ToDecimal(drv["SellingPrice"]) - Convert.ToDecimal(drv["CostPrice"])) / Convert.ToDecimal(drv["CostPrice"]) * 100), 4).ToString();
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
            using (conn = new SqlConnection(chuoiketnoi))
            {
                conn.Open();
                string query = "select * from Product p join Category c on p.CategoryID = c.CategoryID join Supplier s on p.SupplierID = s.SupplierID where ProductName like @textsearch";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@textsearch", "%" + textsearch + "%");
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
                    row.Cells["dgvQtt"].Style.BackColor = Color.Red;
                }
            }
        }

        private void btnNext_Click_1(object sender, EventArgs e)
        {

        }
    }
}
