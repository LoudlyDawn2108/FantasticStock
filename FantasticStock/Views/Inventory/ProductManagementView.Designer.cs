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
        private System.Windows.Forms.Button btnAddProduct;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Panel panelActions;

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.colProductID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSupplier = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colReorderLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEdit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colDelete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.panelActions = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnAddProduct = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panelProductsContainer = new System.Windows.Forms.Panel();
            this.dgvProducts = new System.Windows.Forms.DataGridView();
            this.dgvProductID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvCg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvSupp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvQtt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvRL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Actions = new System.Windows.Forms.DataGridViewButtonColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
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
            this.lblCostPrice = new System.Windows.Forms.Label();
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
            this.label2 = new System.Windows.Forms.Label();
            this.txtSalePrice = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCostPrice = new System.Windows.Forms.TextBox();
            this.panelHeader.SuspendLayout();
            this.panelActions.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panelProductsContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.lblHeader);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1481, 50);
            this.panelHeader.TabIndex = 2;
            // 
            // lblHeader
            // 
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.RoyalBlue;
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
            this.panelActions.Size = new System.Drawing.Size(1481, 50);
            this.panelActions.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.btnAddProduct, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnExport, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(1241, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(240, 50);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // btnAddProduct
            // 
            this.btnAddProduct.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnAddProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAddProduct.ForeColor = System.Drawing.Color.White;
            this.btnAddProduct.Image = global::FantasticStock.Properties.Resources.plus_small;
            this.btnAddProduct.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddProduct.Location = new System.Drawing.Point(3, 3);
            this.btnAddProduct.Name = "btnAddProduct";
            this.btnAddProduct.Size = new System.Drawing.Size(114, 44);
            this.btnAddProduct.TabIndex = 0;
            this.btnAddProduct.Text = "     New Product";
            this.btnAddProduct.UseVisualStyleBackColor = false;
            this.btnAddProduct.Click += new System.EventHandler(this.btnAddProduct_Click);
            // 
            // btnExport
            // 
            this.btnExport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExport.Location = new System.Drawing.Point(123, 3);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(114, 44);
            this.btnExport.TabIndex = 2;
            this.btnExport.Text = "Export";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Controls.Add(this.panelProductsContainer);
            this.flowLayoutPanel1.Controls.Add(this.panelDetails);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 100);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1481, 700);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // panelProductsContainer
            // 
            this.panelProductsContainer.AutoScroll = true;
            this.panelProductsContainer.BackColor = System.Drawing.SystemColors.Window;
            this.panelProductsContainer.Controls.Add(this.dgvProducts);
            this.panelProductsContainer.Controls.Add(this.panel1);
            this.panelProductsContainer.Controls.Add(this.paginationPanel);
            this.panelProductsContainer.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelProductsContainer.Location = new System.Drawing.Point(3, 3);
            this.panelProductsContainer.Name = "panelProductsContainer";
            this.panelProductsContainer.Padding = new System.Windows.Forms.Padding(15);
            this.panelProductsContainer.Size = new System.Drawing.Size(809, 833);
            this.panelProductsContainer.TabIndex = 5;
            // 
            // dgvProducts
            // 
            this.dgvProducts.AllowUserToAddRows = false;
            this.dgvProducts.AllowUserToDeleteRows = false;
            this.dgvProducts.AllowUserToResizeColumns = false;
            this.dgvProducts.AllowUserToResizeRows = false;
            this.dgvProducts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProducts.BackgroundColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProducts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvProducts.ColumnHeadersHeight = 34;
            this.dgvProducts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvProductID,
            this.dgvName,
            this.dgvCg,
            this.dgvSupp,
            this.dgvPrice,
            this.dgvQtt,
            this.dgvRL,
            this.Actions});
            this.dgvProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProducts.Location = new System.Drawing.Point(15, 57);
            this.dgvProducts.Name = "dgvProducts";
            this.dgvProducts.ReadOnly = true;
            this.dgvProducts.RowHeadersVisible = false;
            this.dgvProducts.RowHeadersWidth = 62;
            this.dgvProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProducts.Size = new System.Drawing.Size(779, 721);
            this.dgvProducts.TabIndex = 8;
            this.dgvProducts.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProducts_CellClick);
            this.dgvProducts.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgvProducts_RowPrePaint);
            // 
            // dgvProductID
            // 
            this.dgvProductID.HeaderText = "Product ID";
            this.dgvProductID.Name = "dgvProductID";
            this.dgvProductID.ReadOnly = true;
            // 
            // dgvName
            // 
            this.dgvName.HeaderText = "Name";
            this.dgvName.Name = "dgvName";
            this.dgvName.ReadOnly = true;
            // 
            // dgvCg
            // 
            this.dgvCg.HeaderText = "Category";
            this.dgvCg.Name = "dgvCg";
            this.dgvCg.ReadOnly = true;
            // 
            // dgvSupp
            // 
            this.dgvSupp.HeaderText = "Supplier";
            this.dgvSupp.Name = "dgvSupp";
            this.dgvSupp.ReadOnly = true;
            // 
            // dgvPrice
            // 
            this.dgvPrice.HeaderText = "Price";
            this.dgvPrice.Name = "dgvPrice";
            this.dgvPrice.ReadOnly = true;
            // 
            // dgvQtt
            // 
            this.dgvQtt.HeaderText = "Quantity";
            this.dgvQtt.Name = "dgvQtt";
            this.dgvQtt.ReadOnly = true;
            // 
            // dgvRL
            // 
            this.dgvRL.HeaderText = "Reorder Level\t";
            this.dgvRL.Name = "dgvRL";
            this.dgvRL.ReadOnly = true;
            // 
            // Actions
            // 
            this.Actions.HeaderText = "Actions";
            this.Actions.Name = "Actions";
            this.Actions.ReadOnly = true;
            this.Actions.Text = "✏️";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(15, 15);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(779, 42);
            this.panel1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.txtSearch);
            this.panel2.Location = new System.Drawing.Point(579, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(179, 27);
            this.panel2.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::FantasticStock.Properties.Resources.search_interface_symbol;
            this.pictureBox1.Location = new System.Drawing.Point(3, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(27, 14);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.BackColor = System.Drawing.Color.White;
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearch.Location = new System.Drawing.Point(36, 7);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(138, 13);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(11, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "Products";
            // 
            // paginationPanel
            // 
            this.paginationPanel.Controls.Add(this.btnPrevious);
            this.paginationPanel.Controls.Add(this.lblPageInfo);
            this.paginationPanel.Controls.Add(this.btnNext);
            this.paginationPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.paginationPanel.Location = new System.Drawing.Point(15, 778);
            this.paginationPanel.Name = "paginationPanel";
            this.paginationPanel.Size = new System.Drawing.Size(779, 40);
            this.paginationPanel.TabIndex = 2;
            // 
            // btnPrevious
            // 
            this.btnPrevious.Location = new System.Drawing.Point(15, 10);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(80, 23);
            this.btnPrevious.TabIndex = 0;
            this.btnPrevious.Text = "Previous";
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
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click_1);
            // 
            // panelDetails
            // 
            this.panelDetails.AutoScroll = true;
            this.panelDetails.BackColor = System.Drawing.Color.White;
            this.panelDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDetails.Controls.Add(this.label2);
            this.panelDetails.Controls.Add(this.label3);
            this.panelDetails.Controls.Add(this.txtSalePrice);
            this.panelDetails.Controls.Add(this.txtCostPrice);
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
            this.panelDetails.Controls.Add(this.lblCostPrice);
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
            this.panelDetails.Location = new System.Drawing.Point(3, 842);
            this.panelDetails.Name = "panelDetails";
            this.panelDetails.Padding = new System.Windows.Forms.Padding(15);
            this.panelDetails.Size = new System.Drawing.Size(809, 833);
            this.panelDetails.TabIndex = 10;
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
            // lblCostPrice
            // 
            this.lblCostPrice.Location = new System.Drawing.Point(15, 350);
            this.lblCostPrice.Name = "lblCostPrice";
            this.lblCostPrice.Size = new System.Drawing.Size(100, 23);
            this.lblCostPrice.TabIndex = 13;
            this.lblCostPrice.Text = "Cost Price";
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
            this.txtQuantityInStock.Location = new System.Drawing.Point(17, 419);
            this.txtQuantityInStock.Name = "txtQuantityInStock";
            this.txtQuantityInStock.Size = new System.Drawing.Size(156, 20);
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
            this.btnRemoveImage.Location = new System.Drawing.Point(174, 609);
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
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(10, 326);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 20);
            this.label2.TabIndex = 32;
            this.label2.Text = "$";
            // 
            // txtSalePrice
            // 
            this.txtSalePrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSalePrice.Location = new System.Drawing.Point(29, 370);
            this.txtSalePrice.Name = "txtSalePrice";
            this.txtSalePrice.Size = new System.Drawing.Size(145, 20);
            this.txtSalePrice.TabIndex = 33;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(10, 370);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 20);
            this.label3.TabIndex = 33;
            this.label3.Text = "$";
            // 
            // txtCostPrice
            // 
            this.txtCostPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCostPrice.Location = new System.Drawing.Point(29, 326);
            this.txtCostPrice.Name = "txtCostPrice";
            this.txtCostPrice.Size = new System.Drawing.Size(144, 20);
            this.txtCostPrice.TabIndex = 34;
            // 
            // ProductManagementView
            // 
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.panelActions);
            this.Controls.Add(this.panelHeader);
            this.Name = "ProductManagementView";
            this.Size = new System.Drawing.Size(1481, 800);
            this.Load += new System.EventHandler(this.ProductManagementView_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelActions.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panelProductsContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.paginationPanel.ResumeLayout(false);
            this.paginationPanel.PerformLayout();
            this.panelDetails.ResumeLayout(false);
            this.panelDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxProductImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public ProductManagementView()
        {
            InitializeComponent();
        }

        private TableLayoutPanel tableLayoutPanel1;
        private FlowLayoutPanel flowLayoutPanel1;
        private Panel panelProductsContainer;
        private DataGridView dgvProducts;
        private Panel panel1;
        private Label label1;
        private TextBox txtSearch;
        private Panel paginationPanel;
        private Button btnPrevious;
        private Label lblPageInfo;
        private Button btnNext;
        private Panel panelDetails;
        private Label lblDetailsHeader;
        private Label lblProductName;
        private TextBox txtProductName;
        private Label lblSKU;
        private TextBox txtSKU;
        private Label lblBarcode;
        private TextBox txtBarcode;
        private Label lblCategory;
        private ComboBox cmbCategory;
        private Label lblSupplier;
        private ComboBox cmbSupplier;
        private Label lblSalePrice;
        private Label lblCostPrice;
        private Label lblMarkup;
        private TextBox txtMarkup;
        private Label lblQuantityInStock;
        private TextBox txtQuantityInStock;
        private Label lblReorderLevel;
        private TextBox txtReorderLevel;
        private Label lblDescription;
        private TextBox txtDescription;
        private Label lblProductImage;
        private PictureBox pictureBoxProductImage;
        private Button btnUploadImage;
        private Button btnRemoveImage;
        private Button btnSave;
        private Button btnCancel;
        private Button btnDelete;
        private Panel panel2;
        private PictureBox pictureBox1;
        private DataGridViewTextBoxColumn dgvProductID;
        private DataGridViewTextBoxColumn dgvName;
        private DataGridViewTextBoxColumn dgvCg;
        private DataGridViewTextBoxColumn dgvSupp;
        private DataGridViewTextBoxColumn dgvPrice;
        private DataGridViewTextBoxColumn dgvQtt;
        private DataGridViewTextBoxColumn dgvRL;
        private DataGridViewButtonColumn Actions;
        private Button btnExport;
        private Label label2;
        private TextBox txtSalePrice;
        private TextBox txtCostPrice;
        private Label label3;
    }
}
