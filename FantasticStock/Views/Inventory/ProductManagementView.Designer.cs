namespace FantasticStock.Views.Inventory
{
    partial class ProductManagementView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;

            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabProducts = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnFilter = new System.Windows.Forms.Button();
            this.cmbActiveFilter = new System.Windows.Forms.ComboBox();
            this.lblActiveFilter = new System.Windows.Forms.Label();
            this.cmbSupplierFilter = new System.Windows.Forms.ComboBox();
            this.lblSupplierFilter = new System.Windows.Forms.Label();
            this.cmbCategoryFilter = new System.Windows.Forms.ComboBox();
            this.lblCategoryFilter = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.dgvProducts = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnDeleteProduct = new System.Windows.Forms.Button();
            this.btnEditProduct = new System.Windows.Forms.Button();
            this.btnAddProduct = new System.Windows.Forms.Button();
            this.pnlProductDetails = new System.Windows.Forms.Panel();
            this.btnSaveProduct = new System.Windows.Forms.Button();
            this.btnBrowseImage = new System.Windows.Forms.Button();
            this.pictureBoxProduct = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.chkAllowFractional = new System.Windows.Forms.CheckBox();
            this.chkManageStock = new System.Windows.Forms.CheckBox();
            this.txtUnitOfMeasure = new System.Windows.Forms.TextBox();
            this.lblUnitOfMeasure = new System.Windows.Forms.Label();
            this.txtTargetStockLevel = new System.Windows.Forms.TextBox();
            this.lblTargetStockLevel = new System.Windows.Forms.Label();
            this.txtReorderLevel = new System.Windows.Forms.TextBox();
            this.lblReorderLevel = new System.Windows.Forms.Label();
            this.txtWholesalePrice = new System.Windows.Forms.TextBox();
            this.lblWholesalePrice = new System.Windows.Forms.Label();
            this.txtSellingPrice = new System.Windows.Forms.TextBox();
            this.lblSellingPrice = new System.Windows.Forms.Label();
            this.txtCostPrice = new System.Windows.Forms.TextBox();
            this.lblCostPrice = new System.Windows.Forms.Label();
            this.cmbSupplier = new System.Windows.Forms.ComboBox();
            this.lblSupplier = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtProductName = new System.Windows.Forms.TextBox();
            this.lblProductName = new System.Windows.Forms.Label();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.lblBarcode = new System.Windows.Forms.Label();
            this.txtSKU = new System.Windows.Forms.TextBox();
            this.lblSKU = new System.Windows.Forms.Label();
            this.tabInventory = new System.Windows.Forms.TabPage();
            this.dgvInventory = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnTransactionHistory = new System.Windows.Forms.Button();
            this.btnUpdateStock = new System.Windows.Forms.Button();

            // Set up basic layout
            this.tabControl1.Controls.Add(this.tabProducts);
            this.tabControl1.Controls.Add(this.tabInventory);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1016, 626);
            this.tabControl1.TabIndex = 0;

            // Configure products tab
            this.tabProducts.Controls.Add(this.splitContainer1);
            this.tabProducts.Location = new System.Drawing.Point(4, 24);
            this.tabProducts.Name = "tabProducts";
            this.tabProducts.Padding = new System.Windows.Forms.Padding(3);
            this.tabProducts.Size = new System.Drawing.Size(1008, 598);
            this.tabProducts.TabIndex = 0;
            this.tabProducts.Text = "Products";
            this.tabProducts.UseVisualStyleBackColor = true;

            // Split container for product list and details
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.splitContainer1.Size = new System.Drawing.Size(1002, 592);
            this.splitContainer1.SplitterDistance = 300;
            this.splitContainer1.TabIndex = 0;

            // Product list panel
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);

            // Product details panel
            this.splitContainer1.Panel2.Controls.Add(this.pnlProductDetails);
            this.splitContainer1.Panel2.Controls.Add(this.panel2);

            // Product list groupbox
            this.groupBox1.Controls.Add(this.dgvProducts);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1002, 300);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.Text = "Products";

            // Filter panel
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.btnFilter);
            this.panel1.Controls.Add(this.cmbActiveFilter);
            this.panel1.Controls.Add(this.lblActiveFilter);
            this.panel1.Controls.Add(this.cmbSupplierFilter);
            this.panel1.Controls.Add(this.lblSupplierFilter);
            this.panel1.Controls.Add(this.cmbCategoryFilter);
            this.panel1.Controls.Add(this.lblCategoryFilter);
            this.panel1.Controls.Add(this.txtSearch);
            this.panel1.Controls.Add(this.lblSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(996, 60);
            this.panel1.TabIndex = 0;

            // Products grid
            this.dgvProducts.AllowUserToAddRows = false;
            this.dgvProducts.AllowUserToDeleteRows = false;
            this.dgvProducts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProducts.Location = new System.Drawing.Point(3, 79);
            this.dgvProducts.MultiSelect = false;
            this.dgvProducts.Name = "dgvProducts";
            this.dgvProducts.ReadOnly = true;
            this.dgvProducts.RowHeadersVisible = false;
            this.dgvProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProducts.Size = new System.Drawing.Size(996, 218);
            this.dgvProducts.TabIndex = 1;

            // Button panel
            this.panel2.Controls.Add(this.btnDeleteProduct);
            this.panel2.Controls.Add(this.btnEditProduct);
            this.panel2.Controls.Add(this.btnAddProduct);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1002, 40);
            this.panel2.TabIndex = 0;

            // Product details panel
            this.pnlProductDetails.Controls.Add(this.btnSaveProduct);
            this.pnlProductDetails.Controls.Add(this.btnBrowseImage);
            this.pnlProductDetails.Controls.Add(this.pictureBoxProduct);
            this.pnlProductDetails.Controls.Add(this.groupBox2);
            this.pnlProductDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlProductDetails.Location = new System.Drawing.Point(0, 40);
            this.pnlProductDetails.Name = "pnlProductDetails";
            this.pnlProductDetails.Size = new System.Drawing.Size(1002, 248);
            this.pnlProductDetails.TabIndex = 1;
            this.pnlProductDetails.Enabled = false;

            // Product details fields
            this.groupBox2.Controls.Add(this.chkActive);
            this.groupBox2.Controls.Add(this.chkAllowFractional);
            this.groupBox2.Controls.Add(this.chkManageStock);
            this.groupBox2.Controls.Add(this.txtUnitOfMeasure);
            this.groupBox2.Controls.Add(this.lblUnitOfMeasure);
            this.groupBox2.Controls.Add(this.txtTargetStockLevel);
            this.groupBox2.Controls.Add(this.lblTargetStockLevel);
            this.groupBox2.Controls.Add(this.txtReorderLevel);
            this.groupBox2.Controls.Add(this.lblReorderLevel);
            this.groupBox2.Controls.Add(this.txtWholesalePrice);
            this.groupBox2.Controls.Add(this.lblWholesalePrice);
            this.groupBox2.Controls.Add(this.txtSellingPrice);
            this.groupBox2.Controls.Add(this.lblSellingPrice);
            this.groupBox2.Controls.Add(this.txtCostPrice);
            this.groupBox2.Controls.Add(this.lblCostPrice);
            this.groupBox2.Controls.Add(this.cmbSupplier);
            this.groupBox2.Controls.Add(this.lblSupplier);
            this.groupBox2.Controls.Add(this.cmbCategory);
            this.groupBox2.Controls.Add(this.lblCategory);
            this.groupBox2.Controls.Add(this.txtDescription);
            this.groupBox2.Controls.Add(this.lblDescription);
            this.groupBox2.Controls.Add(this.txtProductName);
            this.groupBox2.Controls.Add(this.lblProductName);
            this.groupBox2.Controls.Add(this.txtBarcode);
            this.groupBox2.Controls.Add(this.lblBarcode);
            this.groupBox2.Controls.Add(this.txtSKU);
            this.groupBox2.Controls.Add(this.lblSKU);
            this.groupBox2.Location = new System.Drawing.Point(8, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(780, 202);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.Text = "Product Details";

            // Configure inventory tab
            this.tabInventory.Controls.Add(this.dgvInventory);
            this.tabInventory.Controls.Add(this.panel3);
            this.tabInventory.Location = new System.Drawing.Point(4, 24);
            this.tabInventory.Name = "tabInventory";
            this.tabInventory.Padding = new System.Windows.Forms.Padding(3);
            this.tabInventory.Size = new System.Drawing.Size(1008, 598);
            this.tabInventory.TabIndex = 1;
            this.tabInventory.Text = "Inventory";
            this.tabInventory.UseVisualStyleBackColor = true;

            // Inventory grid
            this.dgvInventory.AllowUserToAddRows = false;
            this.dgvInventory.AllowUserToDeleteRows = false;
            this.dgvInventory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvInventory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInventory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvInventory.Location = new System.Drawing.Point(3, 43);
            this.dgvInventory.MultiSelect = false;
            this.dgvInventory.Name = "dgvInventory";
            this.dgvInventory.ReadOnly = true;
            this.dgvInventory.RowHeadersVisible = false;
            this.dgvInventory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvInventory.Size = new System.Drawing.Size(1002, 552);
            this.dgvInventory.TabIndex = 0;

            // Inventory buttons panel
            this.panel3.Controls.Add(this.btnTransactionHistory);
            this.panel3.Controls.Add(this.btnUpdateStock);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1002, 40);
            this.panel3.TabIndex = 1;

            // Add the tab control to the main form
            this.Controls.Add(this.tabControl1);
            this.Name = "ProductManagementView";
            this.Size = new System.Drawing.Size(1016, 626);

            // Button configurations
            this.btnAddProduct.Text = "Add New";
            this.btnEditProduct.Text = "Edit";
            this.btnSaveProduct.Text = "Save";
            this.btnDeleteProduct.Text = "Delete";
            this.btnRefresh.Text = "Refresh";
            this.btnFilter.Text = "Apply Filter";
            this.btnBrowseImage.Text = "Browse...";
            this.btnUpdateStock.Text = "Update Stock";
            this.btnTransactionHistory.Text = "Transaction History";

            // Additional components setup would go here
            // For brevity, I'm omitting detailed control setup code that would be generated by the designer

            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabProducts;
        private System.Windows.Forms.TabPage tabInventory;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.ComboBox cmbActiveFilter;
        private System.Windows.Forms.Label lblActiveFilter;
        private System.Windows.Forms.ComboBox cmbSupplierFilter;
        private System.Windows.Forms.Label lblSupplierFilter;
        private System.Windows.Forms.ComboBox cmbCategoryFilter;
        private System.Windows.Forms.Label lblCategoryFilter;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.DataGridView dgvProducts;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnDeleteProduct;
        private System.Windows.Forms.Button btnEditProduct;
        private System.Windows.Forms.Button btnAddProduct;
        private System.Windows.Forms.Panel pnlProductDetails;
        private System.Windows.Forms.Button btnSaveProduct;
        private System.Windows.Forms.Button btnBrowseImage;
        private System.Windows.Forms.PictureBox pictureBoxProduct;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.CheckBox chkAllowFractional;
        private System.Windows.Forms.CheckBox chkManageStock;
        private System.Windows.Forms.TextBox txtUnitOfMeasure;
        private System.Windows.Forms.Label lblUnitOfMeasure;
        private System.Windows.Forms.TextBox txtTargetStockLevel;
        private System.Windows.Forms.Label lblTargetStockLevel;
        private System.Windows.Forms.TextBox txtReorderLevel;
        private System.Windows.Forms.Label lblReorderLevel;
        private System.Windows.Forms.TextBox txtWholesalePrice;
        private System.Windows.Forms.Label lblWholesalePrice;
        private System.Windows.Forms.TextBox txtSellingPrice;
        private System.Windows.Forms.Label lblSellingPrice;
        private System.Windows.Forms.TextBox txtCostPrice;
        private System.Windows.Forms.Label lblCostPrice;
        private System.Windows.Forms.ComboBox cmbSupplier;
        private System.Windows.Forms.Label lblSupplier;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtProductName;
        private System.Windows.Forms.Label lblProductName;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.Label lblBarcode;
        private System.Windows.Forms.TextBox txtSKU;
        private System.Windows.Forms.Label lblSKU;
        private System.Windows.Forms.DataGridView dgvInventory;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnTransactionHistory;
        private System.Windows.Forms.Button btnUpdateStock;
    }
}
