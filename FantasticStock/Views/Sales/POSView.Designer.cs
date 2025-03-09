namespace FantasticStock.Views.Sales
{
    partial class POSView
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
            this.components = new System.ComponentModel.Container();
            this.mainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.leftSplitContainer = new System.Windows.Forms.SplitContainer();
            this.productSearchPanel = new System.Windows.Forms.Panel();
            this.productsFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.barcodePanel = new System.Windows.Forms.Panel();
            this.barcodeTextBox = new System.Windows.Forms.TextBox();
            this.barcodeLabel = new System.Windows.Forms.Label();
            this.searchPanel = new System.Windows.Forms.Panel();
            this.productSearchTextBox = new System.Windows.Forms.TextBox();
            this.searchLabel = new System.Windows.Forms.Label();
            this.customerPanel = new System.Windows.Forms.Panel();
            this.loyaltyInfoLabel = new System.Windows.Forms.Label();
            this.customerSearchButton = new System.Windows.Forms.Button();
            this.customerComboBox = new System.Windows.Forms.ComboBox();
            this.customerLabel = new System.Windows.Forms.Label();
            this.rightSplitContainer = new System.Windows.Forms.SplitContainer();
            this.currentSalePanel = new System.Windows.Forms.Panel();
            this.cartDataGridView = new System.Windows.Forms.DataGridView();
            this.productIdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.quantityColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.priceColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.discountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.removeColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.currentSaleLabel = new System.Windows.Forms.Label();
            this.paymentPanel = new System.Windows.Forms.Panel();
            this.paymentOptionsPanel = new System.Windows.Forms.Panel();
            this.printReceiptCheckBox = new System.Windows.Forms.CheckBox();
            this.emailReceiptCheckBox = new System.Windows.Forms.CheckBox();
            this.completeTransactionButton = new System.Windows.Forms.Button();
            this.cancelTransactionButton = new System.Windows.Forms.Button();
            this.paymentMethodComboBox = new System.Windows.Forms.ComboBox();
            this.paymentMethodLabel = new System.Windows.Forms.Label();
            this.summaryPanel = new System.Windows.Forms.Panel();
            this.changeAmountLabel = new System.Windows.Forms.Label();
            this.changeLabel = new System.Windows.Forms.Label();
            this.tenderedAmountTextBox = new System.Windows.Forms.TextBox();
            this.tenderedLabel = new System.Windows.Forms.Label();
            this.totalAmountLabel = new System.Windows.Forms.Label();
            this.totalLabel = new System.Windows.Forms.Label();
            this.taxAmountLabel = new System.Windows.Forms.Label();
            this.taxLabel = new System.Windows.Forms.Label();
            this.discountAmountLabel = new System.Windows.Forms.Label();
            this.discountLabel = new System.Windows.Forms.Label();
            this.subtotalAmountLabel = new System.Windows.Forms.Label();
            this.subtotalLabel = new System.Windows.Forms.Label();
            this.paymentLabel = new System.Windows.Forms.Label();
            this.searchAutoCompleteTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).BeginInit();
            this.mainSplitContainer.Panel1.SuspendLayout();
            this.mainSplitContainer.Panel2.SuspendLayout();
            this.mainSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leftSplitContainer)).BeginInit();
            this.leftSplitContainer.Panel1.SuspendLayout();
            this.leftSplitContainer.Panel2.SuspendLayout();
            this.leftSplitContainer.SuspendLayout();
            this.productSearchPanel.SuspendLayout();
            this.barcodePanel.SuspendLayout();
            this.searchPanel.SuspendLayout();
            this.customerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rightSplitContainer)).BeginInit();
            this.rightSplitContainer.Panel1.SuspendLayout();
            this.rightSplitContainer.Panel2.SuspendLayout();
            this.rightSplitContainer.SuspendLayout();
            this.currentSalePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cartDataGridView)).BeginInit();
            this.paymentPanel.SuspendLayout();
            this.paymentOptionsPanel.SuspendLayout();
            this.summaryPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainSplitContainer
            // 
            this.mainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.mainSplitContainer.Name = "mainSplitContainer";
            // 
            // mainSplitContainer.Panel1
            // 
            this.mainSplitContainer.Panel1.Controls.Add(this.leftSplitContainer);
            // 
            // mainSplitContainer.Panel2
            // 
            this.mainSplitContainer.Panel2.Controls.Add(this.rightSplitContainer);
            this.mainSplitContainer.Size = new System.Drawing.Size(900, 597);
            this.mainSplitContainer.SplitterDistance = 449;
            this.mainSplitContainer.TabIndex = 0;
            // 
            // leftSplitContainer
            // 
            this.leftSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.leftSplitContainer.Name = "leftSplitContainer";
            this.leftSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // leftSplitContainer.Panel1
            // 
            this.leftSplitContainer.Panel1.Controls.Add(this.productSearchPanel);
            // 
            // leftSplitContainer.Panel2
            // 
            this.leftSplitContainer.Panel2.Controls.Add(this.customerPanel);
            this.leftSplitContainer.Size = new System.Drawing.Size(449, 597);
            this.leftSplitContainer.SplitterDistance = 472;
            this.leftSplitContainer.TabIndex = 0;
            // 
            // productSearchPanel
            // 
            this.productSearchPanel.Controls.Add(this.productsFlowLayoutPanel);
            this.productSearchPanel.Controls.Add(this.barcodePanel);
            this.productSearchPanel.Controls.Add(this.searchPanel);
            this.productSearchPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.productSearchPanel.Location = new System.Drawing.Point(0, 0);
            this.productSearchPanel.Name = "productSearchPanel";
            this.productSearchPanel.Padding = new System.Windows.Forms.Padding(5);
            this.productSearchPanel.Size = new System.Drawing.Size(449, 472);
            this.productSearchPanel.TabIndex = 0;
            // 
            // productsFlowLayoutPanel
            // 
            this.productsFlowLayoutPanel.AutoScroll = true;
            this.productsFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.productsFlowLayoutPanel.Location = new System.Drawing.Point(5, 71);
            this.productsFlowLayoutPanel.Name = "productsFlowLayoutPanel";
            this.productsFlowLayoutPanel.Padding = new System.Windows.Forms.Padding(5);
            this.productsFlowLayoutPanel.Size = new System.Drawing.Size(439, 396);
            this.productsFlowLayoutPanel.TabIndex = 2;
            // 
            // barcodePanel
            // 
            this.barcodePanel.Controls.Add(this.barcodeTextBox);
            this.barcodePanel.Controls.Add(this.barcodeLabel);
            this.barcodePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.barcodePanel.Location = new System.Drawing.Point(5, 38);
            this.barcodePanel.Name = "barcodePanel";
            this.barcodePanel.Size = new System.Drawing.Size(439, 33);
            this.barcodePanel.TabIndex = 1;
            // 
            // barcodeTextBox
            // 
            this.barcodeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.barcodeTextBox.Location = new System.Drawing.Point(73, 6);
            this.barcodeTextBox.Name = "barcodeTextBox";
            this.barcodeTextBox.Size = new System.Drawing.Size(359, 20);
            this.barcodeTextBox.TabIndex = 1;
            // 
            // barcodeLabel
            // 
            this.barcodeLabel.AutoSize = true;
            this.barcodeLabel.Location = new System.Drawing.Point(3, 9);
            this.barcodeLabel.Name = "barcodeLabel";
            this.barcodeLabel.Size = new System.Drawing.Size(50, 13);
            this.barcodeLabel.TabIndex = 0;
            this.barcodeLabel.Text = "Barcode:";
            // 
            // searchPanel
            // 
            this.searchPanel.Controls.Add(this.productSearchTextBox);
            this.searchPanel.Controls.Add(this.searchLabel);
            this.searchPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchPanel.Location = new System.Drawing.Point(5, 5);
            this.searchPanel.Name = "searchPanel";
            this.searchPanel.Size = new System.Drawing.Size(439, 33);
            this.searchPanel.TabIndex = 0;
            // 
            // productSearchTextBox
            // 
            this.productSearchTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.productSearchTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.productSearchTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.productSearchTextBox.Location = new System.Drawing.Point(73, 6);
            this.productSearchTextBox.Name = "productSearchTextBox";
            this.productSearchTextBox.Size = new System.Drawing.Size(359, 20);
            this.productSearchTextBox.TabIndex = 1;
            // 
            // searchLabel
            // 
            this.searchLabel.AutoSize = true;
            this.searchLabel.Location = new System.Drawing.Point(3, 9);
            this.searchLabel.Name = "searchLabel";
            this.searchLabel.Size = new System.Drawing.Size(44, 13);
            this.searchLabel.TabIndex = 0;
            this.searchLabel.Text = "Search:";
            // 
            // customerPanel
            // 
            this.customerPanel.Controls.Add(this.loyaltyInfoLabel);
            this.customerPanel.Controls.Add(this.customerSearchButton);
            this.customerPanel.Controls.Add(this.customerComboBox);
            this.customerPanel.Controls.Add(this.customerLabel);
            this.customerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customerPanel.Location = new System.Drawing.Point(0, 0);
            this.customerPanel.Name = "customerPanel";
            this.customerPanel.Padding = new System.Windows.Forms.Padding(5);
            this.customerPanel.Size = new System.Drawing.Size(449, 121);
            this.customerPanel.TabIndex = 0;
            // 
            // loyaltyInfoLabel
            // 
            this.loyaltyInfoLabel.AutoSize = true;
            this.loyaltyInfoLabel.Location = new System.Drawing.Point(8, 41);
            this.loyaltyInfoLabel.Name = "loyaltyInfoLabel";
            this.loyaltyInfoLabel.Size = new System.Drawing.Size(104, 13);
            this.loyaltyInfoLabel.TabIndex = 3;
            this.loyaltyInfoLabel.Text = "Loyalty Points: None";
            // 
            // customerSearchButton
            // 
            this.customerSearchButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.customerSearchButton.Location = new System.Drawing.Point(364, 10);
            this.customerSearchButton.Name = "customerSearchButton";
            this.customerSearchButton.Size = new System.Drawing.Size(75, 23);
            this.customerSearchButton.TabIndex = 2;
            this.customerSearchButton.Text = "Search";
            this.customerSearchButton.UseVisualStyleBackColor = true;
            // 
            // customerComboBox
            // 
            this.customerComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.customerComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.customerComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.customerComboBox.FormattingEnabled = true;
            this.customerComboBox.Location = new System.Drawing.Point(73, 11);
            this.customerComboBox.Name = "customerComboBox";
            this.customerComboBox.Size = new System.Drawing.Size(285, 21);
            this.customerComboBox.TabIndex = 1;
            // 
            // customerLabel
            // 
            this.customerLabel.AutoSize = true;
            this.customerLabel.Location = new System.Drawing.Point(3, 14);
            this.customerLabel.Name = "customerLabel";
            this.customerLabel.Size = new System.Drawing.Size(54, 13);
            this.customerLabel.TabIndex = 0;
            this.customerLabel.Text = "Customer:";
            // 
            // rightSplitContainer
            // 
            this.rightSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.rightSplitContainer.Name = "rightSplitContainer";
            this.rightSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // rightSplitContainer.Panel1
            // 
            this.rightSplitContainer.Panel1.Controls.Add(this.currentSalePanel);
            // 
            // rightSplitContainer.Panel2
            // 
            this.rightSplitContainer.Panel2.Controls.Add(this.paymentPanel);
            this.rightSplitContainer.Size = new System.Drawing.Size(447, 597);
            this.rightSplitContainer.SplitterDistance = 298;
            this.rightSplitContainer.TabIndex = 0;
            // 
            // currentSalePanel
            // 
            this.currentSalePanel.Controls.Add(this.cartDataGridView);
            this.currentSalePanel.Controls.Add(this.currentSaleLabel);
            this.currentSalePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.currentSalePanel.Location = new System.Drawing.Point(0, 0);
            this.currentSalePanel.Name = "currentSalePanel";
            this.currentSalePanel.Padding = new System.Windows.Forms.Padding(5);
            this.currentSalePanel.Size = new System.Drawing.Size(447, 298);
            this.currentSalePanel.TabIndex = 0;
            // 
            // cartDataGridView
            // 
            this.cartDataGridView.AllowUserToAddRows = false;
            this.cartDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.cartDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.cartDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.productIdColumn,
            this.productNameColumn,
            this.quantityColumn,
            this.priceColumn,
            this.discountColumn,
            this.totalColumn,
            this.removeColumn});
            this.cartDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cartDataGridView.Location = new System.Drawing.Point(5, 18);
            this.cartDataGridView.Name = "cartDataGridView";
            this.cartDataGridView.Size = new System.Drawing.Size(437, 275);
            this.cartDataGridView.TabIndex = 1;
            // 
            // productIdColumn
            // 
            this.productIdColumn.HeaderText = "ID";
            this.productIdColumn.Name = "productIdColumn";
            this.productIdColumn.ReadOnly = true;
            this.productIdColumn.Visible = false;
            // 
            // productNameColumn
            // 
            this.productNameColumn.HeaderText = "Product";
            this.productNameColumn.Name = "productNameColumn";
            this.productNameColumn.ReadOnly = true;
            // 
            // quantityColumn
            // 
            this.quantityColumn.HeaderText = "Qty";
            this.quantityColumn.Name = "quantityColumn";
            // 
            // priceColumn
            // 
            this.priceColumn.HeaderText = "Price";
            this.priceColumn.Name = "priceColumn";
            this.priceColumn.ReadOnly = true;
            // 
            // discountColumn
            // 
            this.discountColumn.HeaderText = "Discount";
            this.discountColumn.Name = "discountColumn";
            this.discountColumn.ReadOnly = true;
            // 
            // totalColumn
            // 
            this.totalColumn.HeaderText = "Total";
            this.totalColumn.Name = "totalColumn";
            this.totalColumn.ReadOnly = true;
            // 
            // removeColumn
            // 
            this.removeColumn.HeaderText = "";
            this.removeColumn.Name = "removeColumn";
            this.removeColumn.Text = "Remove";
            this.removeColumn.UseColumnTextForButtonValue = true;
            // 
            // currentSaleLabel
            // 
            this.currentSaleLabel.AutoSize = true;
            this.currentSaleLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.currentSaleLabel.Location = new System.Drawing.Point(5, 5);
            this.currentSaleLabel.Name = "currentSaleLabel";
            this.currentSaleLabel.Size = new System.Drawing.Size(68, 13);
            this.currentSaleLabel.TabIndex = 0;
            this.currentSaleLabel.Text = "Current Sale:";
            // 
            // paymentPanel
            // 
            this.paymentPanel.Controls.Add(this.paymentOptionsPanel);
            this.paymentPanel.Controls.Add(this.summaryPanel);
            this.paymentPanel.Controls.Add(this.paymentLabel);
            this.paymentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.paymentPanel.Location = new System.Drawing.Point(0, 0);
            this.paymentPanel.Name = "paymentPanel";
            this.paymentPanel.Padding = new System.Windows.Forms.Padding(5);
            this.paymentPanel.Size = new System.Drawing.Size(447, 295);
            this.paymentPanel.TabIndex = 0;
            // 
            // paymentOptionsPanel
            // 
            this.paymentOptionsPanel.Controls.Add(this.printReceiptCheckBox);
            this.paymentOptionsPanel.Controls.Add(this.emailReceiptCheckBox);
            this.paymentOptionsPanel.Controls.Add(this.completeTransactionButton);
            this.paymentOptionsPanel.Controls.Add(this.cancelTransactionButton);
            this.paymentOptionsPanel.Controls.Add(this.paymentMethodComboBox);
            this.paymentOptionsPanel.Controls.Add(this.paymentMethodLabel);
            this.paymentOptionsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.paymentOptionsPanel.Location = new System.Drawing.Point(5, 215);
            this.paymentOptionsPanel.Name = "paymentOptionsPanel";
            this.paymentOptionsPanel.Size = new System.Drawing.Size(437, 75);
            this.paymentOptionsPanel.TabIndex = 2;
            // 
            // printReceiptCheckBox
            // 
            this.printReceiptCheckBox.AutoSize = true;
            this.printReceiptCheckBox.Location = new System.Drawing.Point(204, 10);
            this.printReceiptCheckBox.Name = "printReceiptCheckBox";
            this.printReceiptCheckBox.Size = new System.Drawing.Size(87, 17);
            this.printReceiptCheckBox.TabIndex = 5;
            this.printReceiptCheckBox.Text = "Print Receipt";
            this.printReceiptCheckBox.UseVisualStyleBackColor = true;
            // 
            // emailReceiptCheckBox
            // 
            this.emailReceiptCheckBox.AutoSize = true;
            this.emailReceiptCheckBox.Location = new System.Drawing.Point(204, 33);
            this.emailReceiptCheckBox.Name = "emailReceiptCheckBox";
            this.emailReceiptCheckBox.Size = new System.Drawing.Size(91, 17);
            this.emailReceiptCheckBox.TabIndex = 4;
            this.emailReceiptCheckBox.Text = "Email Receipt";
            this.emailReceiptCheckBox.UseVisualStyleBackColor = true;
            // 
            // completeTransactionButton
            // 
            this.completeTransactionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.completeTransactionButton.BackColor = System.Drawing.Color.LightGreen;
            this.completeTransactionButton.Location = new System.Drawing.Point(359, 10);
            this.completeTransactionButton.Name = "completeTransactionButton";
            this.completeTransactionButton.Size = new System.Drawing.Size(75, 40);
            this.completeTransactionButton.TabIndex = 3;
            this.completeTransactionButton.Text = "Complete";
            this.completeTransactionButton.UseVisualStyleBackColor = false;
            // 
            // cancelTransactionButton
            // 
            this.cancelTransactionButton.BackColor = System.Drawing.Color.LightCoral;
            this.cancelTransactionButton.Location = new System.Drawing.Point(123, 33);
            this.cancelTransactionButton.Name = "cancelTransactionButton";
            this.cancelTransactionButton.Size = new System.Drawing.Size(75, 23);
            this.cancelTransactionButton.TabIndex = 2;
            this.cancelTransactionButton.Text = "Cancel";
            this.cancelTransactionButton.UseVisualStyleBackColor = false;
            // 
            // paymentMethodComboBox
            // 
            this.paymentMethodComboBox.FormattingEnabled = true;
            this.paymentMethodComboBox.Items.AddRange(new object[] {
            "Cash",
            "Credit Card",
            "Debit Card",
            "Gift Card",
            "Check"});
            this.paymentMethodComboBox.Location = new System.Drawing.Point(92, 6);
            this.paymentMethodComboBox.Name = "paymentMethodComboBox";
            this.paymentMethodComboBox.Size = new System.Drawing.Size(106, 21);
            this.paymentMethodComboBox.TabIndex = 1;
            // 
            // paymentMethodLabel
            // 
            this.paymentMethodLabel.AutoSize = true;
            this.paymentMethodLabel.Location = new System.Drawing.Point(3, 9);
            this.paymentMethodLabel.Name = "paymentMethodLabel";
            this.paymentMethodLabel.Size = new System.Drawing.Size(90, 13);
            this.paymentMethodLabel.TabIndex = 0;
            this.paymentMethodLabel.Text = "Payment Method:";
            // 
            // summaryPanel
            // 
            this.summaryPanel.Controls.Add(this.changeAmountLabel);
            this.summaryPanel.Controls.Add(this.changeLabel);
            this.summaryPanel.Controls.Add(this.tenderedAmountTextBox);
            this.summaryPanel.Controls.Add(this.tenderedLabel);
            this.summaryPanel.Controls.Add(this.totalAmountLabel);
            this.summaryPanel.Controls.Add(this.totalLabel);
            this.summaryPanel.Controls.Add(this.taxAmountLabel);
            this.summaryPanel.Controls.Add(this.taxLabel);
            this.summaryPanel.Controls.Add(this.discountAmountLabel);
            this.summaryPanel.Controls.Add(this.discountLabel);
            this.summaryPanel.Controls.Add(this.subtotalAmountLabel);
            this.summaryPanel.Controls.Add(this.subtotalLabel);
            this.summaryPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.summaryPanel.Location = new System.Drawing.Point(5, 18);
            this.summaryPanel.Name = "summaryPanel";
            this.summaryPanel.Size = new System.Drawing.Size(437, 272);
            this.summaryPanel.TabIndex = 1;
            // 
            // changeAmountLabel
            // 
            this.changeAmountLabel.AutoSize = true;
            this.changeAmountLabel.Location = new System.Drawing.Point(92, 138);
            this.changeAmountLabel.Name = "changeAmountLabel";
            this.changeAmountLabel.Size = new System.Drawing.Size(34, 13);
            this.changeAmountLabel.TabIndex = 11;
            this.changeAmountLabel.Text = "$0.00";
            // 
            // changeLabel
            // 
            this.changeLabel.AutoSize = true;
            this.changeLabel.Location = new System.Drawing.Point(3, 138);
            this.changeLabel.Name = "changeLabel";
            this.changeLabel.Size = new System.Drawing.Size(47, 13);
            this.changeLabel.TabIndex = 10;
            this.changeLabel.Text = "Change:";
            // 
            // tenderedAmountTextBox
            // 
            this.tenderedAmountTextBox.Location = new System.Drawing.Point(92, 108);
            this.tenderedAmountTextBox.Name = "tenderedAmountTextBox";
            this.tenderedAmountTextBox.Size = new System.Drawing.Size(100, 20);
            this.tenderedAmountTextBox.TabIndex = 9;
            // 
            // tenderedLabel
            // 
            this.tenderedLabel.AutoSize = true;
            this.tenderedLabel.Location = new System.Drawing.Point(3, 111);
            this.tenderedLabel.Name = "tenderedLabel";
            this.tenderedLabel.Size = new System.Drawing.Size(56, 13);
            this.tenderedLabel.TabIndex = 8;
            this.tenderedLabel.Text = "Tendered:";
            // 
            // totalAmountLabel
            // 
            this.totalAmountLabel.AutoSize = true;
            this.totalAmountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalAmountLabel.Location = new System.Drawing.Point(92, 84);
            this.totalAmountLabel.Name = "totalAmountLabel";
            this.totalAmountLabel.Size = new System.Drawing.Size(39, 13);
            this.totalAmountLabel.TabIndex = 7;
            this.totalAmountLabel.Text = "$0.00";
            // 
            // totalLabel
            // 
            this.totalLabel.AutoSize = true;
            this.totalLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalLabel.Location = new System.Drawing.Point(3, 84);
            this.totalLabel.Name = "totalLabel";
            this.totalLabel.Size = new System.Drawing.Size(44, 13);
            this.totalLabel.TabIndex = 6;
            this.totalLabel.Text = "Total: ";
            // 
            // taxAmountLabel
            // 
            this.taxAmountLabel.AutoSize = true;
            this.taxAmountLabel.Location = new System.Drawing.Point(92, 58);
            this.taxAmountLabel.Name = "taxAmountLabel";
            this.taxAmountLabel.Size = new System.Drawing.Size(34, 13);
            this.taxAmountLabel.TabIndex = 5;
            this.taxAmountLabel.Text = "$0.00";
            // 
            // taxLabel
            // 
            this.taxLabel.AutoSize = true;
            this.taxLabel.Location = new System.Drawing.Point(3, 58);
            this.taxLabel.Name = "taxLabel";
            this.taxLabel.Size = new System.Drawing.Size(28, 13);
            this.taxLabel.TabIndex = 4;
            this.taxLabel.Text = "Tax:";
            // 
            // discountAmountLabel
            // 
            this.discountAmountLabel.AutoSize = true;
            this.discountAmountLabel.Location = new System.Drawing.Point(92, 32);
            this.discountAmountLabel.Name = "discountAmountLabel";
            this.discountAmountLabel.Size = new System.Drawing.Size(34, 13);
            this.discountAmountLabel.TabIndex = 3;
            this.discountAmountLabel.Text = "$0.00";
            // 
            // discountLabel
            // 
            this.discountLabel.AutoSize = true;
            this.discountLabel.Location = new System.Drawing.Point(3, 32);
            this.discountLabel.Name = "discountLabel";
            this.discountLabel.Size = new System.Drawing.Size(52, 13);
            this.discountLabel.TabIndex = 2;
            this.discountLabel.Text = "Discount:";
            // 
            // subtotalAmountLabel
            // 
            this.subtotalAmountLabel.AutoSize = true;
            this.subtotalAmountLabel.Location = new System.Drawing.Point(92, 6);
            this.subtotalAmountLabel.Name = "subtotalAmountLabel";
            this.subtotalAmountLabel.Size = new System.Drawing.Size(34, 13);
            this.subtotalAmountLabel.TabIndex = 1;
            this.subtotalAmountLabel.Text = "$0.00";
            // 
            // subtotalLabel
            // 
            this.subtotalLabel.AutoSize = true;
            this.subtotalLabel.Location = new System.Drawing.Point(3, 6);
            this.subtotalLabel.Name = "subtotalLabel";
            this.subtotalLabel.Size = new System.Drawing.Size(49, 13);
            this.subtotalLabel.TabIndex = 0;
            this.subtotalLabel.Text = "Subtotal:";
            // 
            // paymentLabel
            // 
            this.paymentLabel.AutoSize = true;
            this.paymentLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.paymentLabel.Location = new System.Drawing.Point(5, 5);
            this.paymentLabel.Name = "paymentLabel";
            this.paymentLabel.Size = new System.Drawing.Size(51, 13);
            this.paymentLabel.TabIndex = 0;
            this.paymentLabel.Text = "Payment:";
            // 
            // searchAutoCompleteTimer
            // 
            this.searchAutoCompleteTimer.Interval = 500;
            // 
            // POSView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainSplitContainer);
            this.Name = "POSView";
            this.Size = new System.Drawing.Size(900, 597);
            this.mainSplitContainer.Panel1.ResumeLayout(false);
            this.mainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).EndInit();
            this.mainSplitContainer.ResumeLayout(false);
            this.leftSplitContainer.Panel1.ResumeLayout(false);
            this.leftSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.leftSplitContainer)).EndInit();
            this.leftSplitContainer.ResumeLayout(false);
            this.productSearchPanel.ResumeLayout(false);
            this.barcodePanel.ResumeLayout(false);
            this.barcodePanel.PerformLayout();
            this.searchPanel.ResumeLayout(false);
            this.searchPanel.PerformLayout();
            this.customerPanel.ResumeLayout(false);
            this.customerPanel.PerformLayout();
            this.rightSplitContainer.Panel1.ResumeLayout(false);
            this.rightSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rightSplitContainer)).EndInit();
            this.rightSplitContainer.ResumeLayout(false);
            this.currentSalePanel.ResumeLayout(false);
            this.currentSalePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cartDataGridView)).EndInit();
            this.paymentPanel.ResumeLayout(false);
            this.paymentPanel.PerformLayout();
            this.paymentOptionsPanel.ResumeLayout(false);
            this.paymentOptionsPanel.PerformLayout();
            this.summaryPanel.ResumeLayout(false);
            this.summaryPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer mainSplitContainer;
        private System.Windows.Forms.SplitContainer leftSplitContainer;
        private System.Windows.Forms.SplitContainer rightSplitContainer;
        private System.Windows.Forms.Panel productSearchPanel;
        private System.Windows.Forms.Panel customerPanel;
        private System.Windows.Forms.Panel currentSalePanel;
        private System.Windows.Forms.Panel paymentPanel;
        private System.Windows.Forms.Panel searchPanel;
        private System.Windows.Forms.Label searchLabel;
        private System.Windows.Forms.TextBox productSearchTextBox;
        private System.Windows.Forms.Panel barcodePanel;
        private System.Windows.Forms.TextBox barcodeTextBox;
        private System.Windows.Forms.Label barcodeLabel;
        private System.Windows.Forms.FlowLayoutPanel productsFlowLayoutPanel;
        private System.Windows.Forms.Label customerLabel;
        private System.Windows.Forms.ComboBox customerComboBox;
        private System.Windows.Forms.Button customerSearchButton;
        private System.Windows.Forms.Label loyaltyInfoLabel;
        private System.Windows.Forms.Label currentSaleLabel;
        private System.Windows.Forms.DataGridView cartDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn productIdColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn productNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn quantityColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn priceColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn discountColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalColumn;
        private System.Windows.Forms.DataGridViewButtonColumn removeColumn;
        private System.Windows.Forms.Label paymentLabel;
        private System.Windows.Forms.Panel summaryPanel;
        private System.Windows.Forms.Label taxAmountLabel;
        private System.Windows.Forms.Label taxLabel;
        private System.Windows.Forms.Label discountAmountLabel;
        private System.Windows.Forms.Label discountLabel;
        private System.Windows.Forms.Label subtotalAmountLabel;
        private System.Windows.Forms.Label subtotalLabel;
        private System.Windows.Forms.Label totalAmountLabel;
        private System.Windows.Forms.Label totalLabel;
        private System.Windows.Forms.Panel paymentOptionsPanel;
        private System.Windows.Forms.Button completeTransactionButton;
        private System.Windows.Forms.Button cancelTransactionButton;
        private System.Windows.Forms.ComboBox paymentMethodComboBox;
        private System.Windows.Forms.Label paymentMethodLabel;
        private System.Windows.Forms.TextBox tenderedAmountTextBox;
        private System.Windows.Forms.Label tenderedLabel;
        private System.Windows.Forms.Label changeAmountLabel;
        private System.Windows.Forms.Label changeLabel;
        private System.Windows.Forms.CheckBox printReceiptCheckBox;
        private System.Windows.Forms.CheckBox emailReceiptCheckBox;
        private System.Windows.Forms.Timer searchAutoCompleteTimer;
        private System.Windows.Forms.AutoCompleteStringCollection productSearchAutoComplete;
        private System.Windows.Forms.AutoCompleteStringCollection customerAutoComplete;
    }
}
