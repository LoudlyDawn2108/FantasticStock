using System;
using System.Drawing;
using System.Windows.Forms;

namespace FantasticStock.Views.Inventory
{
    partial class ProductManagementView
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSupplier;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colReorderLevel;
        private System.Windows.Forms.DataGridViewButtonColumn colEdit;
        private System.Windows.Forms.DataGridViewButtonColumn colDelete;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnAddProduct;
        private System.Windows.Forms.Button btnBulkImport;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnPrintList;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel panelDetails;
        private System.Windows.Forms.TextBox txtProductName;
        private System.Windows.Forms.TextBox txtSKU;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.ComboBox cmbSupplier;
        private System.Windows.Forms.TextBox txtSalePrice;
        private System.Windows.Forms.TextBox txtCostPrice;
        private System.Windows.Forms.TextBox txtMarkup;
        private System.Windows.Forms.TextBox txtQuantityInStock;
        private System.Windows.Forms.TextBox txtReorderLevel;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.PictureBox pictureBoxProductImage;
        private System.Windows.Forms.Button btnUploadImage;
        private System.Windows.Forms.Button btnRemoveImage;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label lblProductName;
        private System.Windows.Forms.Label lblSKU;
        private System.Windows.Forms.Label lblBarcode;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Label lblSupplier;
        private System.Windows.Forms.Label lblSalePrice;
        private System.Windows.Forms.Label lblCostPrice;
        private System.Windows.Forms.Label lblMarkup;
        private System.Windows.Forms.Label lblQuantityInStock;
        private System.Windows.Forms.Label lblReorderLevel;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblProductImage;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblDetailsHeader;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Panel panelActions;
        private System.Windows.Forms.TableLayoutPanel tableLayoutMain;
        private System.Windows.Forms.Panel panelProductsContainer;
        private System.Windows.Forms.Panel paginationPanel;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Label lblPageInfo;
        private System.Windows.Forms.Button btnNext;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.colProductID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSupplier = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colReorderLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEdit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colDelete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnAddProduct = new System.Windows.Forms.Button();
            this.btnBulkImport = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnPrintList = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.panelActions = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutMain = new System.Windows.Forms.TableLayoutPanel();
            this.panelProductsContainer = new System.Windows.Forms.Panel();
            this.dgvProducts = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.paginationPanel = new System.Windows.Forms.Panel();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.lblPageInfo = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.panelDetails = new System.Windows.Forms.Panel();
            this.lblDetailsHeader = new System.Windows.Forms.Label();
            this.lblProductName = new System.Windows.Forms.Label();
            this.txtProductName = new System.Windows.Forms.TextBox();
            this.lblSKU = new System.Windows.Forms.Label();
            this.txtSKU = new System.Windows.Forms.TextBox();
            this.lblBarcode = new System.Windows.Forms.Label();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.lblSupplier = new System.Windows.Forms.Label();
            this.cmbSupplier = new System.Windows.Forms.ComboBox();
            this.lblSalePrice = new System.Windows.Forms.Label();
            this.txtSalePrice = new System.Windows.Forms.TextBox();
            this.lblCostPrice = new System.Windows.Forms.Label();
            this.txtCostPrice = new System.Windows.Forms.TextBox();
            this.lblMarkup = new System.Windows.Forms.Label();
            this.txtMarkup = new System.Windows.Forms.TextBox();
            this.lblQuantityInStock = new System.Windows.Forms.Label();
            this.txtQuantityInStock = new System.Windows.Forms.TextBox();
            this.lblReorderLevel = new System.Windows.Forms.Label();
            this.txtReorderLevel = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblProductImage = new System.Windows.Forms.Label();
            this.pictureBoxProductImage = new System.Windows.Forms.PictureBox();
            this.btnUploadImage = new System.Windows.Forms.Button();
            this.btnRemoveImage = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.panelHeader.SuspendLayout();
            this.panelActions.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutMain.SuspendLayout();
            this.panelProductsContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).BeginInit();
            this.panel1.SuspendLayout();
            this.paginationPanel.SuspendLayout();
            this.panelDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxProductImage)).BeginInit();
            this.SuspendLayout();
            // 
            // colProductID
            // 
            this.colProductID.MinimumWidth = 8;
            this.colProductID.Name = "colProductID";
            this.colProductID.Width = 150;
            // 
            // colName
            // 
            this.colName.MinimumWidth = 8;
            this.colName.Name = "colName";
            this.colName.Width = 150;
            // 
            // colCategory
            // 
            this.colCategory.MinimumWidth = 8;
            this.colCategory.Name = "colCategory";
            this.colCategory.Width = 150;
            // 
            // colSupplier
            // 
            this.colSupplier.MinimumWidth = 8;
            this.colSupplier.Name = "colSupplier";
            this.colSupplier.Width = 150;
            // 
            // colPrice
            // 
            this.colPrice.MinimumWidth = 8;
            this.colPrice.Name = "colPrice";
            this.colPrice.Width = 150;
            // 
            // colQuantity
            // 
            this.colQuantity.MinimumWidth = 8;
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.Width = 150;
            // 
            // colReorderLevel
            // 
            this.colReorderLevel.MinimumWidth = 8;
            this.colReorderLevel.Name = "colReorderLevel";
            this.colReorderLevel.Width = 150;
            // 
            // colEdit
            // 
            this.colEdit.MinimumWidth = 8;
            this.colEdit.Name = "colEdit";
            this.colEdit.Width = 150;
            // 
            // colDelete
            // 
            this.colDelete.MinimumWidth = 8;
            this.colDelete.Name = "colDelete";
            this.colDelete.Width = 150;
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.BackColor = System.Drawing.Color.White;
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Location = new System.Drawing.Point(644, 6);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(200, 20);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // btnAddProduct
            // 
            this.btnAddProduct.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnAddProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAddProduct.ForeColor = System.Drawing.Color.White;
            this.btnAddProduct.Location = new System.Drawing.Point(3, 3);
            this.btnAddProduct.Name = "btnAddProduct";
            this.btnAddProduct.Size = new System.Drawing.Size(109, 44);
            this.btnAddProduct.TabIndex = 0;
            this.btnAddProduct.Text = "New Product";
            this.btnAddProduct.UseVisualStyleBackColor = false;
            this.btnAddProduct.Click += new System.EventHandler(this.btnAddProduct_Click);
            // 
            // btnBulkImport
            // 
            this.btnBulkImport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnBulkImport.Location = new System.Drawing.Point(118, 3);
            this.btnBulkImport.Name = "btnBulkImport";
            this.btnBulkImport.Size = new System.Drawing.Size(109, 44);
            this.btnBulkImport.TabIndex = 1;
            this.btnBulkImport.Text = "Bulk Import";
            this.btnBulkImport.Click += new System.EventHandler(this.btnBulkImport_Click);
            // 
            // btnExport
            // 
            this.btnExport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExport.Location = new System.Drawing.Point(233, 3);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(109, 44);
            this.btnExport.TabIndex = 2;
            this.btnExport.Text = "Export";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnPrintList
            // 
            this.btnPrintList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPrintList.Location = new System.Drawing.Point(348, 3);
            this.btnPrintList.Name = "btnPrintList";
            this.btnPrintList.Size = new System.Drawing.Size(109, 44);
            this.btnPrintList.TabIndex = 3;
            this.btnPrintList.Text = "Print List";
            this.btnPrintList.Click += new System.EventHandler(this.btnPrintList_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRefresh.Location = new System.Drawing.Point(463, 3);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(111, 44);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.lblHeader);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1507, 50);
            this.panelHeader.TabIndex = 2;
            // 
            // lblHeader
            // 
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(15, 15);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(352, 32);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Product Management";
            // 
            // panelActions
            // 
            this.panelActions.Controls.Add(this.tableLayoutPanel1);
            this.panelActions.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelActions.Location = new System.Drawing.Point(0, 50);
            this.panelActions.Name = "panelActions";
            this.panelActions.Size = new System.Drawing.Size(1507, 50);
            this.panelActions.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.btnRefresh, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnAddProduct, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnPrintList, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnBulkImport, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnExport, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(930, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(577, 50);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // tableLayoutMain
            // 
            this.tableLayoutMain.ColumnCount = 2;
            this.tableLayoutMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutMain.Controls.Add(this.panelProductsContainer, 0, 0);
            this.tableLayoutMain.Controls.Add(this.panelDetails, 1, 0);
            this.tableLayoutMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutMain.Location = new System.Drawing.Point(0, 100);
            this.tableLayoutMain.Name = "tableLayoutMain";
            this.tableLayoutMain.RowCount = 1;
            this.tableLayoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutMain.Size = new System.Drawing.Size(1507, 700);
            this.tableLayoutMain.TabIndex = 0;
            // 
            // panelProductsContainer
            // 
            this.panelProductsContainer.Controls.Add(this.dgvProducts);
            this.panelProductsContainer.Controls.Add(this.panel1);
            this.panelProductsContainer.Controls.Add(this.paginationPanel);
            this.panelProductsContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelProductsContainer.Location = new System.Drawing.Point(3, 3);
            this.panelProductsContainer.Name = "panelProductsContainer";
            this.panelProductsContainer.Padding = new System.Windows.Forms.Padding(15);
            this.panelProductsContainer.Size = new System.Drawing.Size(898, 694);
            this.panelProductsContainer.TabIndex = 0;
            // 
            // dgvProducts
            // 
            this.dgvProducts.AllowUserToAddRows = false;
            this.dgvProducts.AllowUserToDeleteRows = false;
            this.dgvProducts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProducts.ColumnHeadersHeight = 34;
            this.dgvProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProducts.Location = new System.Drawing.Point(15, 57);
            this.dgvProducts.Name = "dgvProducts";
            this.dgvProducts.ReadOnly = true;
            this.dgvProducts.RowHeadersWidth = 62;
            this.dgvProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProducts.Size = new System.Drawing.Size(868, 582);
            this.dgvProducts.TabIndex = 8;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(15, 15);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(868, 42);
            this.panel1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Products";
            // 
            // paginationPanel
            // 
            this.paginationPanel.Controls.Add(this.btnPrevious);
            this.paginationPanel.Controls.Add(this.lblPageInfo);
            this.paginationPanel.Controls.Add(this.btnNext);
            this.paginationPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.paginationPanel.Location = new System.Drawing.Point(15, 639);
            this.paginationPanel.Name = "paginationPanel";
            this.paginationPanel.Size = new System.Drawing.Size(868, 40);
            this.paginationPanel.TabIndex = 2;
            // 
            // btnPrevious
            // 
            this.btnPrevious.Location = new System.Drawing.Point(15, 10);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(80, 23);
            this.btnPrevious.TabIndex = 0;
            this.btnPrevious.Text = "Previous";
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // lblPageInfo
            // 
            this.lblPageInfo.AutoSize = true;
            this.lblPageInfo.Location = new System.Drawing.Point(110, 15);
            this.lblPageInfo.Name = "lblPageInfo";
            this.lblPageInfo.Size = new System.Drawing.Size(62, 13);
            this.lblPageInfo.TabIndex = 1;
            this.lblPageInfo.Text = "Page 1 of 4";
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(180, 10);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(80, 23);
            this.btnNext.TabIndex = 2;
            this.btnNext.Text = "Next";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // panelDetails
            // 
            this.panelDetails.AutoScroll = true;
            this.panelDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDetails.Controls.Add(this.lblDetailsHeader);
            this.panelDetails.Controls.Add(this.lblProductName);
            this.panelDetails.Controls.Add(this.txtProductName);
            this.panelDetails.Controls.Add(this.lblSKU);
            this.panelDetails.Controls.Add(this.txtSKU);
            this.panelDetails.Controls.Add(this.lblBarcode);
            this.panelDetails.Controls.Add(this.txtBarcode);
            this.panelDetails.Controls.Add(this.lblCategory);
            this.panelDetails.Controls.Add(this.cmbCategory);
            this.panelDetails.Controls.Add(this.lblSupplier);
            this.panelDetails.Controls.Add(this.cmbSupplier);
            this.panelDetails.Controls.Add(this.lblSalePrice);
            this.panelDetails.Controls.Add(this.txtSalePrice);
            this.panelDetails.Controls.Add(this.lblCostPrice);
            this.panelDetails.Controls.Add(this.txtCostPrice);
            this.panelDetails.Controls.Add(this.lblMarkup);
            this.panelDetails.Controls.Add(this.txtMarkup);
            this.panelDetails.Controls.Add(this.lblQuantityInStock);
            this.panelDetails.Controls.Add(this.txtQuantityInStock);
            this.panelDetails.Controls.Add(this.lblReorderLevel);
            this.panelDetails.Controls.Add(this.txtReorderLevel);
            this.panelDetails.Controls.Add(this.lblDescription);
            this.panelDetails.Controls.Add(this.txtDescription);
            this.panelDetails.Controls.Add(this.lblProductImage);
            this.panelDetails.Controls.Add(this.pictureBoxProductImage);
            this.panelDetails.Controls.Add(this.btnUploadImage);
            this.panelDetails.Controls.Add(this.btnRemoveImage);
            this.panelDetails.Controls.Add(this.btnSave);
            this.panelDetails.Controls.Add(this.btnCancel);
            this.panelDetails.Controls.Add(this.btnDelete);
            this.panelDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDetails.Location = new System.Drawing.Point(907, 3);
            this.panelDetails.Name = "panelDetails";
            this.panelDetails.Padding = new System.Windows.Forms.Padding(15);
            this.panelDetails.Size = new System.Drawing.Size(597, 694);
            this.panelDetails.TabIndex = 1;
            // 
            // lblDetailsHeader
            // 
            this.lblDetailsHeader.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblDetailsHeader.Location = new System.Drawing.Point(15, 15);
            this.lblDetailsHeader.Name = "lblDetailsHeader";
            this.lblDetailsHeader.Size = new System.Drawing.Size(131, 23);
            this.lblDetailsHeader.TabIndex = 0;
            this.lblDetailsHeader.Text = "Product Details";
            // 
            // lblProductName
            // 
            this.lblProductName.Location = new System.Drawing.Point(15, 50);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(100, 23);
            this.lblProductName.TabIndex = 1;
            this.lblProductName.Text = "Product Name";
            // 
            // txtProductName
            // 
            this.txtProductName.Location = new System.Drawing.Point(15, 70);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.Size = new System.Drawing.Size(350, 20);
            this.txtProductName.TabIndex = 2;
            // 
            // lblSKU
            // 
            this.lblSKU.Location = new System.Drawing.Point(15, 100);
            this.lblSKU.Name = "lblSKU";
            this.lblSKU.Size = new System.Drawing.Size(100, 23);
            this.lblSKU.TabIndex = 3;
            this.lblSKU.Text = "SKU";
            // 
            // txtSKU
            // 
            this.txtSKU.Location = new System.Drawing.Point(15, 120);
            this.txtSKU.Name = "txtSKU";
            this.txtSKU.Size = new System.Drawing.Size(350, 20);
            this.txtSKU.TabIndex = 4;
            // 
            // lblBarcode
            // 
            this.lblBarcode.Location = new System.Drawing.Point(15, 150);
            this.lblBarcode.Name = "lblBarcode";
            this.lblBarcode.Size = new System.Drawing.Size(100, 23);
            this.lblBarcode.TabIndex = 5;
            this.lblBarcode.Text = "Barcode";
            // 
            // txtBarcode
            // 
            this.txtBarcode.Location = new System.Drawing.Point(15, 170);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(350, 20);
            this.txtBarcode.TabIndex = 6;
            // 
            // lblCategory
            // 
            this.lblCategory.Location = new System.Drawing.Point(15, 200);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(100, 23);
            this.lblCategory.TabIndex = 7;
            this.lblCategory.Text = "Category";
            // 
            // cmbCategory
            // 
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.Location = new System.Drawing.Point(15, 220);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(350, 21);
            this.cmbCategory.TabIndex = 8;
            // 
            // lblSupplier
            // 
            this.lblSupplier.Location = new System.Drawing.Point(15, 250);
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(100, 23);
            this.lblSupplier.TabIndex = 9;
            this.lblSupplier.Text = "Supplier";
            // 
            // cmbSupplier
            // 
            this.cmbSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSupplier.Location = new System.Drawing.Point(15, 270);
            this.cmbSupplier.Name = "cmbSupplier";
            this.cmbSupplier.Size = new System.Drawing.Size(350, 21);
            this.cmbSupplier.TabIndex = 10;
            // 
            // lblSalePrice
            // 
            this.lblSalePrice.Location = new System.Drawing.Point(15, 300);
            this.lblSalePrice.Name = "lblSalePrice";
            this.lblSalePrice.Size = new System.Drawing.Size(100, 23);
            this.lblSalePrice.TabIndex = 11;
            this.lblSalePrice.Text = "Sale Price";
            // 
            // txtSalePrice
            // 
            this.txtSalePrice.Location = new System.Drawing.Point(15, 320);
            this.txtSalePrice.Name = "txtSalePrice";
            this.txtSalePrice.Size = new System.Drawing.Size(170, 20);
            this.txtSalePrice.TabIndex = 12;
            this.txtSalePrice.TextChanged += new System.EventHandler(this.txtSalePrice_TextChanged);
            // 
            // lblCostPrice
            // 
            this.lblCostPrice.Location = new System.Drawing.Point(15, 350);
            this.lblCostPrice.Name = "lblCostPrice";
            this.lblCostPrice.Size = new System.Drawing.Size(100, 23);
            this.lblCostPrice.TabIndex = 13;
            this.lblCostPrice.Text = "Cost Price";
            // 
            // txtCostPrice
            // 
            this.txtCostPrice.Location = new System.Drawing.Point(15, 370);
            this.txtCostPrice.Name = "txtCostPrice";
            this.txtCostPrice.Size = new System.Drawing.Size(170, 20);
            this.txtCostPrice.TabIndex = 14;
            this.txtCostPrice.TextChanged += new System.EventHandler(this.txtCostPrice_TextChanged);
            // 
            // lblMarkup
            // 
            this.lblMarkup.Location = new System.Drawing.Point(195, 350);
            this.lblMarkup.Name = "lblMarkup";
            this.lblMarkup.Size = new System.Drawing.Size(100, 23);
            this.lblMarkup.TabIndex = 15;
            this.lblMarkup.Text = "Markup (%)";
            // 
            // txtMarkup
            // 
            this.txtMarkup.Location = new System.Drawing.Point(195, 370);
            this.txtMarkup.Name = "txtMarkup";
            this.txtMarkup.ReadOnly = true;
            this.txtMarkup.Size = new System.Drawing.Size(170, 20);
            this.txtMarkup.TabIndex = 16;
            // 
            // lblQuantityInStock
            // 
            this.lblQuantityInStock.Location = new System.Drawing.Point(15, 400);
            this.lblQuantityInStock.Name = "lblQuantityInStock";
            this.lblQuantityInStock.Size = new System.Drawing.Size(100, 23);
            this.lblQuantityInStock.TabIndex = 17;
            this.lblQuantityInStock.Text = "Quantity in Stock";
            // 
            // txtQuantityInStock
            // 
            this.txtQuantityInStock.Location = new System.Drawing.Point(15, 420);
            this.txtQuantityInStock.Name = "txtQuantityInStock";
            this.txtQuantityInStock.Size = new System.Drawing.Size(170, 20);
            this.txtQuantityInStock.TabIndex = 18;
            // 
            // lblReorderLevel
            // 
            this.lblReorderLevel.Location = new System.Drawing.Point(195, 400);
            this.lblReorderLevel.Name = "lblReorderLevel";
            this.lblReorderLevel.Size = new System.Drawing.Size(100, 23);
            this.lblReorderLevel.TabIndex = 19;
            this.lblReorderLevel.Text = "Reorder Level";
            // 
            // txtReorderLevel
            // 
            this.txtReorderLevel.Location = new System.Drawing.Point(195, 420);
            this.txtReorderLevel.Name = "txtReorderLevel";
            this.txtReorderLevel.Size = new System.Drawing.Size(170, 20);
            this.txtReorderLevel.TabIndex = 20;
            // 
            // lblDescription
            // 
            this.lblDescription.Location = new System.Drawing.Point(15, 450);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(100, 23);
            this.lblDescription.TabIndex = 21;
            this.lblDescription.Text = "Description";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(15, 470);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(350, 80);
            this.txtDescription.TabIndex = 22;
            // 
            // lblProductImage
            // 
            this.lblProductImage.Location = new System.Drawing.Point(15, 560);
            this.lblProductImage.Name = "lblProductImage";
            this.lblProductImage.Size = new System.Drawing.Size(100, 23);
            this.lblProductImage.TabIndex = 23;
            this.lblProductImage.Text = "Product Image";
            // 
            // pictureBoxProductImage
            // 
            this.pictureBoxProductImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxProductImage.Location = new System.Drawing.Point(15, 580);
            this.pictureBoxProductImage.Name = "pictureBoxProductImage";
            this.pictureBoxProductImage.Size = new System.Drawing.Size(150, 150);
            this.pictureBoxProductImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxProductImage.TabIndex = 24;
            this.pictureBoxProductImage.TabStop = false;
            // 
            // btnUploadImage
            // 
            this.btnUploadImage.Location = new System.Drawing.Point(175, 580);
            this.btnUploadImage.Name = "btnUploadImage";
            this.btnUploadImage.Size = new System.Drawing.Size(100, 23);
            this.btnUploadImage.TabIndex = 25;
            this.btnUploadImage.Text = "Upload Image";
            this.btnUploadImage.Click += new System.EventHandler(this.btnUploadImage_Click);
            // 
            // btnRemoveImage
            // 
            this.btnRemoveImage.Location = new System.Drawing.Point(175, 610);
            this.btnRemoveImage.Name = "btnRemoveImage";
            this.btnRemoveImage.Size = new System.Drawing.Size(100, 23);
            this.btnRemoveImage.TabIndex = 26;
            this.btnRemoveImage.Text = "Remove";
            this.btnRemoveImage.Click += new System.EventHandler(this.btnRemoveImage_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(15, 740);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(110, 35);
            this.btnSave.TabIndex = 27;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(135, 740);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(110, 35);
            this.btnCancel.TabIndex = 28;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(255, 740);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(110, 35);
            this.btnDelete.TabIndex = 29;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // ProductManagementView
            // 
            this.Controls.Add(this.tableLayoutMain);
            this.Controls.Add(this.panelActions);
            this.Controls.Add(this.panelHeader);
            this.Name = "ProductManagementView";
            this.Size = new System.Drawing.Size(1507, 800);
            this.panelHeader.ResumeLayout(false);
            this.panelActions.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutMain.ResumeLayout(false);
            this.panelProductsContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.paginationPanel.ResumeLayout(false);
            this.paginationPanel.PerformLayout();
            this.panelDetails.ResumeLayout(false);
            this.panelDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxProductImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        #region Event Handlers

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            ClearDetails();
            EnableDetailsEditing(true);
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
            LoadProducts();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            FilterProducts(txtSearch.Text);
        }

        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (e.ColumnIndex == dgvProducts.Columns["colEdit"].Index)
            {
                LoadProductDetails(dgvProducts.Rows[e.RowIndex].Cells["colProductID"].Value.ToString());
                EnableDetailsEditing(true);
            }
            else if (e.ColumnIndex == dgvProducts.Columns["colDelete"].Index)
            {
                string productId = dgvProducts.Rows[e.RowIndex].Cells["colProductID"].Value.ToString();
                DeleteProduct(productId);
            }
            else
            {
                LoadProductDetails(dgvProducts.Rows[e.RowIndex].Cells["colProductID"].Value.ToString());
                EnableDetailsEditing(false);
            }
        }

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
            if (ValidateProductDetails())
            {
                SaveProduct();
                EnableDetailsEditing(false);
                LoadProducts();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearDetails();
            EnableDetailsEditing(false);
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
                    EnableDetailsEditing(false);
                    LoadProducts();
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

        private void LoadProducts()
        {
            // TODO: Replace with actual data loading from database
            dgvProducts.Rows.Clear();

            // Sample data
            dgvProducts.Rows.Add("P001", "Laptop Pro 15\"", "Electronics", "TechSuppliers Inc.", "$1,299.99", "24", "10");
            dgvProducts.Rows.Add("P002", "Wireless Mouse", "Accessories", "TechSuppliers Inc.", "$29.99", "45", "15");
            dgvProducts.Rows.Add("P003", "Bluetooth Headphones", "Audio", "AudioTech Ltd.", "$89.99", "18", "20");
            dgvProducts.Rows.Add("P004", "4K Monitor 27\"", "Electronics", "VisualTech Corp", "$349.99", "5", "8");
            dgvProducts.Rows.Add("P005", "USB-C Hub", "Accessories", "ConnectAll Inc.", "$49.99", "30", "10");
            dgvProducts.Rows.Add("P006", "Wireless Keyboard", "Accessories", "TechSuppliers Inc.", "$59.99", "7", "15");
        }

        private void FilterProducts(string searchTerm)
        {
            // TODO: Implement filtering logic
            if (string.IsNullOrEmpty(searchTerm))
            {
                LoadProducts();
                return;
            }

            // For now, just reload all products
            // In a real implementation, this would filter based on the search term
            LoadProducts();
        }

        private void LoadProductDetails(string productId)
        {
            // TODO: Replace with actual data loading from database
            // For now, just load sample data
            if (productId == "P001")
            {
                txtProductName.Text = "Laptop Pro 15\"";
                txtSKU.Text = "LAP-PRO-15";
                txtBarcode.Text = "7891234567890";
                cmbCategory.Text = "Electronics";
                cmbSupplier.Text = "TechSuppliers Inc.";
                txtSalePrice.Text = "1299.99";
                txtCostPrice.Text = "950.00";
                txtQuantityInStock.Text = "24";
                txtReorderLevel.Text = "10";
                txtDescription.Text = "15-inch professional laptop with Intel Core i7, 16GB RAM, 512GB SSD, and NVIDIA GeForce RTX 2050 graphics.";
            }
        }

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

        private void EnableDetailsEditing(bool enable)
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

        private bool ValidateProductDetails()
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

        private void SaveProduct()
        {
            // TODO: Implement actual save functionality to database
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
                LoadProducts();
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

        private void LoadCategories()
        {
            // TODO: Replace with actual data loading from database
            cmbCategory.Items.Clear();
            cmbCategory.Items.Add("Electronics");
            cmbCategory.Items.Add("Accessories");
            cmbCategory.Items.Add("Audio");
            cmbCategory.Items.Add("Peripherals");
            cmbCategory.Items.Add("Storage");
            cmbCategory.Items.Add("Networking");
        }

        private void LoadSuppliers()
        {
            // TODO: Replace with actual data loading from database
            cmbSupplier.Items.Clear();
            cmbSupplier.Items.Add("TechSuppliers Inc.");
            cmbSupplier.Items.Add("AudioTech Ltd.");
            cmbSupplier.Items.Add("VisualTech Corp");
            cmbSupplier.Items.Add("ConnectAll Inc.");
            cmbSupplier.Items.Add("StorageTech Inc.");
        }

        #endregion

        // Constructor
        public ProductManagementView()
        {
            InitializeComponent();

            // Set up DataGridView columns
            dgvProducts.ColumnCount = 7;
            dgvProducts.Columns[0].Name = "colProductID";
            dgvProducts.Columns[0].HeaderText = "Product ID";
            dgvProducts.Columns[1].Name = "colName";
            dgvProducts.Columns[1].HeaderText = "Name";
            dgvProducts.Columns[2].Name = "colCategory";
            dgvProducts.Columns[2].HeaderText = "Category";
            dgvProducts.Columns[3].Name = "colSupplier";
            dgvProducts.Columns[3].HeaderText = "Supplier";
            dgvProducts.Columns[4].Name = "colPrice";
            dgvProducts.Columns[4].HeaderText = "Price";
            dgvProducts.Columns[5].Name = "colQuantity";
            dgvProducts.Columns[5].HeaderText = "Quantity";
            dgvProducts.Columns[6].Name = "colReorderLevel";
            dgvProducts.Columns[6].HeaderText = "Reorder Level";

            // Add edit and delete button columns
            DataGridViewButtonColumn editButtonColumn = new DataGridViewButtonColumn();
            editButtonColumn.Name = "colEdit";
            editButtonColumn.HeaderText = "Actions";
            editButtonColumn.Text = "✏️";
            editButtonColumn.UseColumnTextForButtonValue = true;
            editButtonColumn.Width = 50;
            dgvProducts.Columns.Add(editButtonColumn);

            DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn();
            deleteButtonColumn.Name = "colDelete";
            deleteButtonColumn.HeaderText = "";
            deleteButtonColumn.Text = "🗑️";
            deleteButtonColumn.UseColumnTextForButtonValue = true;
            deleteButtonColumn.Width = 50;
            dgvProducts.Columns.Add(deleteButtonColumn);

            // Load initial data
            //LoadCategories();
            //LoadSuppliers();
            //LoadProducts();
        }

        private Panel panel1;
        private Label label1;
        private DataGridView dgvProducts;
        private TableLayoutPanel tableLayoutPanel1;
    }
}