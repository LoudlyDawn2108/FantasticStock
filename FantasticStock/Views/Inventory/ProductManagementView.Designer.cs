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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabProducts = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvProducts = new System.Windows.Forms.DataGridView();
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
            this.pnlProductDetails = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnDeleteProduct = new System.Windows.Forms.Button();
            this.btnEditProduct = new System.Windows.Forms.Button();
            this.btnAddProduct = new System.Windows.Forms.Button();
            this.tabInventory = new System.Windows.Forms.TabPage();
            this.dgvInventory = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnTransactionHistory = new System.Windows.Forms.Button();
            this.btnUpdateStock = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabProducts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).BeginInit();
            this.panel1.SuspendLayout();
            this.pnlProductDetails.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabInventory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventory)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabProducts);
            this.tabControl1.Controls.Add(this.tabInventory);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(677, 407);
            this.tabControl1.TabIndex = 0;
            // 
            // tabProducts
            // 
            this.tabProducts.Controls.Add(this.splitContainer1);
            this.tabProducts.Location = new System.Drawing.Point(4, 22);
            this.tabProducts.Margin = new System.Windows.Forms.Padding(2);
            this.tabProducts.Name = "tabProducts";
            this.tabProducts.Padding = new System.Windows.Forms.Padding(2);
            this.tabProducts.Size = new System.Drawing.Size(669, 381);
            this.tabProducts.TabIndex = 0;
            this.tabProducts.Text = "Products";
            this.tabProducts.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(2, 2);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pnlProductDetails);
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Size = new System.Drawing.Size(665, 377);
            this.splitContainer1.SplitterDistance = 199;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvProducts);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(199, 377);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Products";
            // 
            // dgvProducts
            // 
            this.dgvProducts.AllowUserToAddRows = false;
            this.dgvProducts.AllowUserToDeleteRows = false;
            this.dgvProducts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProducts.Location = new System.Drawing.Point(2, 54);
            this.dgvProducts.Margin = new System.Windows.Forms.Padding(2);
            this.dgvProducts.MultiSelect = false;
            this.dgvProducts.Name = "dgvProducts";
            this.dgvProducts.ReadOnly = true;
            this.dgvProducts.RowHeadersVisible = false;
            this.dgvProducts.RowHeadersWidth = 62;
            this.dgvProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProducts.Size = new System.Drawing.Size(195, 321);
            this.dgvProducts.TabIndex = 1;
            // 
            // panel1
            // 
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
            this.panel1.Location = new System.Drawing.Point(2, 15);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(195, 39);
            this.panel1.TabIndex = 0;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(0, 0);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(50, 15);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "Refresh";
            // 
            // btnFilter
            // 
            this.btnFilter.Location = new System.Drawing.Point(0, 0);
            this.btnFilter.Margin = new System.Windows.Forms.Padding(2);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(50, 15);
            this.btnFilter.TabIndex = 1;
            this.btnFilter.Text = "Apply Filter";
            // 
            // cmbActiveFilter
            // 
            this.cmbActiveFilter.Location = new System.Drawing.Point(0, 0);
            this.cmbActiveFilter.Margin = new System.Windows.Forms.Padding(2);
            this.cmbActiveFilter.Name = "cmbActiveFilter";
            this.cmbActiveFilter.Size = new System.Drawing.Size(82, 21);
            this.cmbActiveFilter.TabIndex = 2;
            // 
            // lblActiveFilter
            // 
            this.lblActiveFilter.Location = new System.Drawing.Point(0, 0);
            this.lblActiveFilter.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblActiveFilter.Name = "lblActiveFilter";
            this.lblActiveFilter.Size = new System.Drawing.Size(67, 15);
            this.lblActiveFilter.TabIndex = 3;
            // 
            // cmbSupplierFilter
            // 
            this.cmbSupplierFilter.Location = new System.Drawing.Point(0, 0);
            this.cmbSupplierFilter.Margin = new System.Windows.Forms.Padding(2);
            this.cmbSupplierFilter.Name = "cmbSupplierFilter";
            this.cmbSupplierFilter.Size = new System.Drawing.Size(82, 21);
            this.cmbSupplierFilter.TabIndex = 4;
            // 
            // lblSupplierFilter
            // 
            this.lblSupplierFilter.Location = new System.Drawing.Point(0, 0);
            this.lblSupplierFilter.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSupplierFilter.Name = "lblSupplierFilter";
            this.lblSupplierFilter.Size = new System.Drawing.Size(67, 15);
            this.lblSupplierFilter.TabIndex = 5;
            // 
            // cmbCategoryFilter
            // 
            this.cmbCategoryFilter.Location = new System.Drawing.Point(0, 0);
            this.cmbCategoryFilter.Margin = new System.Windows.Forms.Padding(2);
            this.cmbCategoryFilter.Name = "cmbCategoryFilter";
            this.cmbCategoryFilter.Size = new System.Drawing.Size(82, 21);
            this.cmbCategoryFilter.TabIndex = 6;
            // 
            // lblCategoryFilter
            // 
            this.lblCategoryFilter.Location = new System.Drawing.Point(0, 0);
            this.lblCategoryFilter.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCategoryFilter.Name = "lblCategoryFilter";
            this.lblCategoryFilter.Size = new System.Drawing.Size(67, 15);
            this.lblCategoryFilter.TabIndex = 7;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(0, 0);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(2);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(68, 20);
            this.txtSearch.TabIndex = 8;
            // 
            // lblSearch
            // 
            this.lblSearch.Location = new System.Drawing.Point(0, 0);
            this.lblSearch.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(67, 15);
            this.lblSearch.TabIndex = 9;
            // 
            // pnlProductDetails
            // 
            this.pnlProductDetails.Controls.Add(this.groupBox2);
            this.pnlProductDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlProductDetails.Enabled = false;
            this.pnlProductDetails.Location = new System.Drawing.Point(0, 26);
            this.pnlProductDetails.Margin = new System.Windows.Forms.Padding(2);
            this.pnlProductDetails.Name = "pnlProductDetails";
            this.pnlProductDetails.Size = new System.Drawing.Size(463, 351);
            this.pnlProductDetails.TabIndex = 1;
            // 
            // groupBox2
            // 
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
            this.groupBox2.Location = new System.Drawing.Point(5, 4);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(520, 131);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Product Details";
            // 
            // txtSellingPrice
            // 
            this.txtSellingPrice.Location = new System.Drawing.Point(0, 0);
            this.txtSellingPrice.Margin = new System.Windows.Forms.Padding(2);
            this.txtSellingPrice.Name = "txtSellingPrice";
            this.txtSellingPrice.Size = new System.Drawing.Size(68, 20);
            this.txtSellingPrice.TabIndex = 11;
            // 
            // lblSellingPrice
            // 
            this.lblSellingPrice.Location = new System.Drawing.Point(0, 0);
            this.lblSellingPrice.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSellingPrice.Name = "lblSellingPrice";
            this.lblSellingPrice.Size = new System.Drawing.Size(67, 15);
            this.lblSellingPrice.TabIndex = 12;
            // 
            // txtCostPrice
            // 
            this.txtCostPrice.Location = new System.Drawing.Point(0, 0);
            this.txtCostPrice.Margin = new System.Windows.Forms.Padding(2);
            this.txtCostPrice.Name = "txtCostPrice";
            this.txtCostPrice.Size = new System.Drawing.Size(68, 20);
            this.txtCostPrice.TabIndex = 13;
            // 
            // lblCostPrice
            // 
            this.lblCostPrice.Location = new System.Drawing.Point(0, 0);
            this.lblCostPrice.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCostPrice.Name = "lblCostPrice";
            this.lblCostPrice.Size = new System.Drawing.Size(67, 15);
            this.lblCostPrice.TabIndex = 14;
            // 
            // cmbSupplier
            // 
            this.cmbSupplier.Location = new System.Drawing.Point(0, 0);
            this.cmbSupplier.Margin = new System.Windows.Forms.Padding(2);
            this.cmbSupplier.Name = "cmbSupplier";
            this.cmbSupplier.Size = new System.Drawing.Size(82, 21);
            this.cmbSupplier.TabIndex = 15;
            // 
            // lblSupplier
            // 
            this.lblSupplier.Location = new System.Drawing.Point(0, 0);
            this.lblSupplier.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(67, 15);
            this.lblSupplier.TabIndex = 16;
            // 
            // cmbCategory
            // 
            this.cmbCategory.Location = new System.Drawing.Point(0, 0);
            this.cmbCategory.Margin = new System.Windows.Forms.Padding(2);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(82, 21);
            this.cmbCategory.TabIndex = 17;
            // 
            // lblCategory
            // 
            this.lblCategory.Location = new System.Drawing.Point(0, 0);
            this.lblCategory.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(67, 15);
            this.lblCategory.TabIndex = 18;
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(0, 0);
            this.txtDescription.Margin = new System.Windows.Forms.Padding(2);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(68, 20);
            this.txtDescription.TabIndex = 19;
            // 
            // lblDescription
            // 
            this.lblDescription.Location = new System.Drawing.Point(0, 0);
            this.lblDescription.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(67, 15);
            this.lblDescription.TabIndex = 20;
            // 
            // txtProductName
            // 
            this.txtProductName.Location = new System.Drawing.Point(0, 0);
            this.txtProductName.Margin = new System.Windows.Forms.Padding(2);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.Size = new System.Drawing.Size(68, 20);
            this.txtProductName.TabIndex = 21;
            // 
            // lblProductName
            // 
            this.lblProductName.Location = new System.Drawing.Point(0, 0);
            this.lblProductName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(67, 15);
            this.lblProductName.TabIndex = 22;
            // 
            // txtBarcode
            // 
            this.txtBarcode.Location = new System.Drawing.Point(0, 0);
            this.txtBarcode.Margin = new System.Windows.Forms.Padding(2);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(68, 20);
            this.txtBarcode.TabIndex = 23;
            // 
            // lblBarcode
            // 
            this.lblBarcode.Location = new System.Drawing.Point(0, 0);
            this.lblBarcode.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblBarcode.Name = "lblBarcode";
            this.lblBarcode.Size = new System.Drawing.Size(67, 15);
            this.lblBarcode.TabIndex = 24;
            // 
            // txtSKU
            // 
            this.txtSKU.Location = new System.Drawing.Point(0, 0);
            this.txtSKU.Margin = new System.Windows.Forms.Padding(2);
            this.txtSKU.Name = "txtSKU";
            this.txtSKU.Size = new System.Drawing.Size(68, 20);
            this.txtSKU.TabIndex = 25;
            // 
            // lblSKU
            // 
            this.lblSKU.Location = new System.Drawing.Point(0, 0);
            this.lblSKU.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSKU.Name = "lblSKU";
            this.lblSKU.Size = new System.Drawing.Size(67, 15);
            this.lblSKU.TabIndex = 26;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnDeleteProduct);
            this.panel2.Controls.Add(this.btnEditProduct);
            this.panel2.Controls.Add(this.btnAddProduct);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(463, 26);
            this.panel2.TabIndex = 0;
            // 
            // btnDeleteProduct
            // 
            this.btnDeleteProduct.Location = new System.Drawing.Point(0, 0);
            this.btnDeleteProduct.Margin = new System.Windows.Forms.Padding(2);
            this.btnDeleteProduct.Name = "btnDeleteProduct";
            this.btnDeleteProduct.Size = new System.Drawing.Size(50, 15);
            this.btnDeleteProduct.TabIndex = 0;
            this.btnDeleteProduct.Text = "Delete";
            // 
            // btnEditProduct
            // 
            this.btnEditProduct.Location = new System.Drawing.Point(0, 0);
            this.btnEditProduct.Margin = new System.Windows.Forms.Padding(2);
            this.btnEditProduct.Name = "btnEditProduct";
            this.btnEditProduct.Size = new System.Drawing.Size(50, 15);
            this.btnEditProduct.TabIndex = 1;
            this.btnEditProduct.Text = "Edit";
            // 
            // btnAddProduct
            // 
            this.btnAddProduct.Location = new System.Drawing.Point(0, 0);
            this.btnAddProduct.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddProduct.Name = "btnAddProduct";
            this.btnAddProduct.Size = new System.Drawing.Size(50, 15);
            this.btnAddProduct.TabIndex = 2;
            this.btnAddProduct.Text = "Add New";
            // 
            // tabInventory
            // 
            this.tabInventory.Controls.Add(this.dgvInventory);
            this.tabInventory.Controls.Add(this.panel3);
            this.tabInventory.Location = new System.Drawing.Point(4, 22);
            this.tabInventory.Margin = new System.Windows.Forms.Padding(2);
            this.tabInventory.Name = "tabInventory";
            this.tabInventory.Padding = new System.Windows.Forms.Padding(2);
            this.tabInventory.Size = new System.Drawing.Size(669, 381);
            this.tabInventory.TabIndex = 1;
            this.tabInventory.Text = "Inventory";
            this.tabInventory.UseVisualStyleBackColor = true;
            // 
            // dgvInventory
            // 
            this.dgvInventory.AllowUserToAddRows = false;
            this.dgvInventory.AllowUserToDeleteRows = false;
            this.dgvInventory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvInventory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInventory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvInventory.Location = new System.Drawing.Point(2, 28);
            this.dgvInventory.Margin = new System.Windows.Forms.Padding(2);
            this.dgvInventory.MultiSelect = false;
            this.dgvInventory.Name = "dgvInventory";
            this.dgvInventory.ReadOnly = true;
            this.dgvInventory.RowHeadersVisible = false;
            this.dgvInventory.RowHeadersWidth = 62;
            this.dgvInventory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvInventory.Size = new System.Drawing.Size(665, 351);
            this.dgvInventory.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnTransactionHistory);
            this.panel3.Controls.Add(this.btnUpdateStock);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(2, 2);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(665, 26);
            this.panel3.TabIndex = 1;
            // 
            // btnTransactionHistory
            // 
            this.btnTransactionHistory.Location = new System.Drawing.Point(0, 0);
            this.btnTransactionHistory.Margin = new System.Windows.Forms.Padding(2);
            this.btnTransactionHistory.Name = "btnTransactionHistory";
            this.btnTransactionHistory.Size = new System.Drawing.Size(50, 15);
            this.btnTransactionHistory.TabIndex = 0;
            this.btnTransactionHistory.Text = "Transaction History";
            // 
            // btnUpdateStock
            // 
            this.btnUpdateStock.Location = new System.Drawing.Point(0, 0);
            this.btnUpdateStock.Margin = new System.Windows.Forms.Padding(2);
            this.btnUpdateStock.Name = "btnUpdateStock";
            this.btnUpdateStock.Size = new System.Drawing.Size(50, 15);
            this.btnUpdateStock.TabIndex = 1;
            this.btnUpdateStock.Text = "Update Stock";
            // 
            // ProductManagementView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ProductManagementView";
            this.Size = new System.Drawing.Size(677, 407);
            this.tabControl1.ResumeLayout(false);
            this.tabProducts.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlProductDetails.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.tabInventory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventory)).EndInit();
            this.panel3.ResumeLayout(false);
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
        private System.Windows.Forms.GroupBox groupBox2;
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
