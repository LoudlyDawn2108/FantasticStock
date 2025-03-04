using System;
using System.Windows.Forms;
using AdminDomain.ViewModels.Sales;

namespace AdminDomain.Views.Sales
{
    public partial class SalesOrderView : UserControl
    {
        private SalesOrderViewModel _viewModel;
        
        public SalesOrderView()
        {
            InitializeComponent();
            
            // Initialize view model
            _viewModel = new SalesOrderViewModel();
            
            // Set up data bindings
            SetupBindings();
        }
        
        private void SetupBindings()
        {
            // Bind sales orders grid
            dgvOrders.DataSource = _viewModel.SalesOrders;
            
            // Bind order details grid
            dgvOrderDetails.DataSource = _viewModel.OrderDetails;
            
            // Bind customers dropdown
            cmbCustomer.DataSource = _viewModel.Customers;
            cmbCustomer.DisplayMember = "CustomerName";
            cmbCustomer.ValueMember = "CustomerID";
            cmbCustomerFilter.DataSource = _viewModel.Customers;
            cmbCustomerFilter.DisplayMember = "CustomerName";
            cmbCustomerFilter.ValueMember = "CustomerID";
            
            // Bind products dropdown
            cmbProduct.DataSource = _viewModel.Products;
            cmbProduct.DisplayMember = "ProductName";
            cmbProduct.ValueMember = "ProductID";
            
            // Bind filter controls
            txtSearch.DataBindings.Add("Text", _viewModel, "SearchText", true, DataSourceUpdateMode.OnPropertyChanged);
            dtpStartDate.DataBindings.Add("Value", _viewModel, "StartDate", true, DataSourceUpdateMode.OnPropertyChanged);
            dtpEndDate.DataBindings.Add("Value", _viewModel, "EndDate", true, DataSourceUpdateMode.OnPropertyChanged);
            
            // Setup status filter
            cmbStatusFilter.Items.Add("All");
            cmbStatusFilter.Items.Add("Draft");
            cmbStatusFilter.Items.Add("Confirmed");
            cmbStatusFilter.Items.Add("Shipped");
            cmbStatusFilter.Items.Add("Delivered");
            cmbStatusFilter.Items.Add("Invoiced");
            cmbStatusFilter.Items.Add("Cancelled");
            cmbStatusFilter.SelectedIndex = 0;
            cmbStatusFilter.DataBindings.Add("Text", _viewModel, "StatusFilter", true, DataSourceUpdateMode.OnPropertyChanged);
            
            cmbCustomerFilter.DataBindings.Add("SelectedValue", _viewModel, "CustomerFilter", true, DataSourceUpdateMode.OnPropertyChanged);
            
            // Bind order details fields
            cmbCustomer.DataBindings.Add("SelectedValue", _viewModel, "SelectedOrder.CustomerID", true, DataSourceUpdateMode.OnPropertyChanged);
            dtpOrderDate.DataBindings.Add("Value", _viewModel, "SelectedOrder.OrderDate", true, DataSourceUpdateMode.OnPropertyChanged);
            txtOrderNumber.DataBindings.Add("Text", _viewModel, "SelectedOrder.OrderNumber", true, DataSourceUpdateMode.OnPropertyChanged);
            txtStatus.DataBindings.Add("Text", _viewModel, "SelectedOrder.Status", true, DataSourceUpdateMode.OnPropertyChanged);
            txtSubtotal.DataBindings.Add("Text", _viewModel, "SelectedOrder.Subtotal", true, DataSourceUpdateMode.OnPropertyChanged);
            txtTaxAmount.DataBindings.Add("Text", _viewModel, "SelectedOrder.TaxAmount", true, DataSourceUpdateMode.OnPropertyChanged);
            txtShippingAmount.DataBindings.Add("Text", _viewModel, "SelectedOrder.ShippingAmount", true, DataSourceUpdateMode.OnPropertyChanged);
            txtDiscountAmount.DataBindings.Add("Text", _viewModel, "SelectedOrder.DiscountAmount", true, DataSourceUpdateMode.OnPropertyChanged);
            txtTotalAmount.DataBindings.Add("Text", _viewModel, "SelectedOrder.TotalAmount", true, DataSourceUpdateMode.OnPropertyChanged);
            txtNotes.DataBindings.Add("Text", _viewModel, "SelectedOrder.Notes", true, DataSourceUpdateMode.OnPropertyChanged);
            
            // Bind product selection
            cmbProduct.DataBindings.Add("SelectedValue", _viewModel, "SelectedProduct.ProductID", true, DataSourceUpdateMode.OnPropertyChanged);
            
            // Bind commands to buttons
            btnNewOrder.Click += (s, e) => _viewModel.NewOrderCommand.Execute(null);
            btnEditOrder.Click += (s, e) => _viewModel.EditOrderCommand.Execute(null);
            btnSaveOrder.Click += (s, e) => _viewModel.SaveOrderCommand.Execute(null);
            btnDeleteOrder.Click += (s, e) => _viewModel.DeleteOrderCommand.Execute(null);
            btnAddProduct.Click += (s, e) => _viewModel.AddProductCommand.Execute(null);
            btnRemoveProduct.Click += (s, e) => _viewModel.RemoveProductCommand.Execute(null);
            btnFilter.Click += (s, e) => _viewModel.FilterOrdersCommand.Execute(null);
            btnGenerateInvoice.Click += (s, e) => _viewModel.GenerateInvoiceCommand.Execute(null);
            btnPrintOrder.Click += (s, e) => _viewModel.PrintOrderCommand.Execute(null);
            btnUpdateStatus.Click += (s, e) => _viewModel.UpdateOrderStatusCommand.Execute(null);
            btnRefresh.Click += (s, e) => _viewModel.RefreshDataCommand.Execute(null);
            
            // Selection change events
            dgvOrders.SelectionChanged += (s, e) => 
            {
                if (dgvOrders.SelectedRows.Count > 0)
                {
                    _viewModel.SelectedOrder = dgvOrders.SelectedRows[0].DataBoundItem as AdminDomain.Models.Sales.SalesOrder;
                }
            };
            
            dgvOrderDetails.SelectionChanged += (s, e) =>
            {
                if (dgvOrderDetails.SelectedRows.Count > 0)
                {
                    _viewModel.SelectedOrderDetail = dgvOrderDetails.SelectedRows[0].DataBoundItem as AdminDomain.Models.Sales.SalesOrderDetail;
                }
            };
            
            // Handle editing mode changes
            _viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "IsEditing")
                {
                    pnlOrderDetails.Enabled = _viewModel.IsEditing;
                    pnlOrderItems.Enabled = _viewModel.IsEditing;
                    btnSaveOrder.Enabled = _viewModel.IsEditing;
                    btnNewOrder.Enabled = !_viewModel.IsEditing;
                    btnEditOrder.Enabled = !_viewModel.IsEditing;
                    btnDeleteOrder.Enabled = !_viewModel.IsEditing;
                    btnUpdateStatus.Enabled = !_viewModel.IsEditing;
                    btnGenerateInvoice.Enabled = !_viewModel.IsEditing;
                }
            };
        }
        
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnFilter = new System.Windows.Forms.Button();
            this.cmbCustomerFilter = new System.Windows.Forms.ComboBox();
            this.lblCustomerFilter = new System.Windows.Forms.Label();
            this.cmbStatusFilter = new System.Windows.Forms.ComboBox();
            this.lblStatusFilter = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.dgvOrders = new System.Windows.Forms.DataGridView();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.pnlOrderDetails = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.lblNotes = new System.Windows.Forms.Label();
            this.txtTotalAmount = new System.Windows.Forms.TextBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.txtDiscountAmount = new System.Windows.Forms.TextBox();
            this.lblDiscount = new System.Windows.Forms.Label();
            this.txtShippingAmount = new System.Windows.Forms.TextBox();
            this.lblShipping = new System.Windows.Forms.Label();
            this.txtTaxAmount = new System.Windows.Forms.TextBox();
            this.lblTax = new System.Windows.Forms.Label();
            this.txtSubtotal = new System.Windows.Forms.TextBox();
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.dtpOrderDate = new System.Windows.Forms.DateTimePicker();
            this.lblOrderDate = new System.Windows.Forms.Label();
            this.txtOrderNumber = new System.Windows.Forms.TextBox();
            this.lblOrderNumber = new System.Windows.Forms.Label();
            this.cmbCustomer = new System.Windows.Forms.ComboBox();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnUpdateStatus = new System.Windows.Forms.Button();
            this.btnPrintOrder = new System.Windows.Forms.Button();
            this.btnGenerateInvoice = new System.Windows.Forms.Button();
            this.btnDeleteOrder = new System.Windows.Forms.Button();
            this.btnSaveOrder = new System.Windows.Forms.Button();
            this.btnEditOrder = new System.Windows.Forms.Button();
            this.btnNewOrder = new System.Windows.Forms.Button();
            this.pnlOrderItems = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnRemoveProduct = new System.Windows.Forms.Button();
            this.btnAddProduct = new System.Windows.Forms.Button();
            this.cmbProduct = new System.Windows.Forms.ComboBox();
            this.lblProduct = new System.Windows.Forms.Label();
            this.dgvOrderDetails = new System.Windows.Forms.DataGridView();
            
            // Set up basic layout
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.splitContainer1.Size = new System.Drawing.Size(1016, 626);
            this.splitContainer1.SplitterDistance = 300;
            this.splitContainer1.TabIndex = 0;
            
            // Orders list panel
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            
            // Order details and items panel
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            
            // Orders list groupbox
            this.groupBox1.Controls.Add(this.dgvOrders);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1016, 300);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.Text = "Sales Orders";
            
            // Filter panel
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.btnFilter);
            this.panel1.Controls.Add(this.cmbCustomerFilter);
            this.panel1.Controls.Add(this.lblCustomerFilter);
            this.panel1.Controls.Add(this.cmbStatusFilter);
            this.panel1.Controls.Add(this.lblStatusFilter);
            this.panel1.Controls.Add(this.dtpEndDate);
            this.panel1.Controls.Add(this.lblEndDate);
            this.panel1.Controls.Add(this.dtpStartDate);
            this.panel1.Controls.Add(this.lblStartDate);
            this.panel1.Controls.Add(this.txtSearch);
            this.panel1.Controls.Add(this.lblSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1010, 60);
            this.panel1.TabIndex = 0;
            
            // Orders grid
            this.dgvOrders.AllowUserToAddRows = false;
            this.dgvOrders.AllowUserToDeleteRows = false;
            this.dgvOrders.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOrders.Location = new System.Drawing.Point(3, 79);
            this.dgvOrders.MultiSelect = false;
            this.dgvOrders.Name = "dgvOrders";
            this.dgvOrders.ReadOnly = true;
            this.dgvOrders.RowHeadersVisible = false;
            this.dgvOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrders.Size = new System.Drawing.Size(1010, 218);
            this.dgvOrders.TabIndex = 1;
            
            // Split container for order details and items
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Size = new System.Drawing.Size(1016, 322);
            this.splitContainer2.SplitterDistance = 500;
            this.splitContainer2.TabIndex = 0;
            
            // Order details panel
            this.splitContainer2.Panel1.Controls.Add(this.pnlOrderDetails);
            this.splitContainer2.Panel1.Controls.Add(this.panel2);
            
            // Order items panel
            this.splitContainer2.Panel2.Controls.Add(this.pnlOrderItems);
            
            // Order details panel
            this.pnlOrderDetails.Controls.Add(this.groupBox2);
            this.pnlOrderDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOrderDetails.Location = new System.Drawing.Point(0, 40);
            this.pnlOrderDetails.Name = "pnlOrderDetails";
            this.pnlOrderDetails.Size = new System.Drawing.Size(500, 282);
            this.pnlOrderDetails.TabIndex = 1;
            this.pnlOrderDetails.Enabled = false;
            
            // Order details groupbox
            this.groupBox2.Controls.Add(this.txtNotes);
            this.groupBox2.Controls.Add(this.lblNotes);
            this.groupBox2.Controls.Add(this.txtTotalAmount);
            this.groupBox2.Controls.Add(this.lblTotal);
            this.groupBox2.Controls.Add(this.txtDiscountAmount);
            this.groupBox2.Controls.Add(this.lblDiscount);
            this.groupBox2.Controls.Add(this.txtShippingAmount);
            this.groupBox2.Controls.Add(this.lblShipping);
            this.groupBox2.Controls.Add(this.txtTaxAmount);
            this.groupBox2.Controls.Add(this.lblTax);
            this.groupBox2.Controls.Add(this.txtSubtotal);
            this.groupBox2.Controls.Add(this.lblSubtotal);
            this.groupBox2.Controls.Add(this.txtStatus);
            this.groupBox2.Controls.Add(this.lblStatus);
            this.groupBox2.Controls.Add(this.dtpOrderDate);
            this.groupBox2.Controls.Add(this.lblOrderDate);
            this.groupBox2.Controls.Add(this.txtOrderNumber);
            this.groupBox2.Controls.Add(this.lblOrderNumber);
            this.groupBox2.Controls.Add(this.cmbCustomer);
            this.groupBox2.Controls.Add(this.lblCustomer);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(500, 282);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.Text = "Order Details";
            
            // Button panel
            this.panel2.Controls.Add(this.btnUpdateStatus);
            this.panel2.Controls.Add(this.btnPrintOrder);
            this.panel2.Controls.Add(this.btnGenerateInvoice);
            this.panel2.Controls.Add(this.btnDeleteOrder);
            this.panel2.Controls.Add(this.btnSaveOrder);
            this.panel2.Controls.Add(this.btnEditOrder);
            this.panel2.Controls.Add(this.btnNewOrder);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(500, 40);
            this.panel2.TabIndex = 0;
            
            // Order items panel
            this.pnlOrderItems.Controls.Add(this.groupBox3);
            this.pnlOrderItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOrderItems.Location = new System.Drawing.Point(0, 0);
            this.pnlOrderItems.Name = "pnlOrderItems";
            this.pnlOrderItems.Size = new System.Drawing.Size(512, 322);
            this.pnlOrderItems.TabIndex = 0;
            this.pnlOrderItems.Enabled = false;
            
            // Order items groupbox
            this.groupBox3.Controls.Add(this.dgvOrderDetails);
            this.groupBox3.Controls.Add(this.panel3);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(512, 322);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.Text = "Order Items";
            
            // Product selection panel
            this.panel3.Controls.Add(this.btnRemoveProduct);
            this.panel3.Controls.Add(this.btnAddProduct);
            this.panel3.Controls.Add(this.cmbProduct);
            this.panel3.Controls.Add(this.lblProduct);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 19);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(506, 40);
            this.panel3.TabIndex = 0;
            
            // Order details grid
            this.dgvOrderDetails.AllowUserToAddRows = false;
            this.dgvOrderDetails.AllowUserToDeleteRows = false;
            this.dgvOrderDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOrderDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrderDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOrderDetails.Location = new System.Drawing.Point(3, 59);
            this.dgvOrderDetails.MultiSelect = false;
            this.dgvOrderDetails.Name = "dgvOrderDetails";
            this.dgvOrderDetails.ReadOnly = true;
            this.dgvOrderDetails.RowHeadersVisible = false;
            this.dgvOrderDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrderDetails.Size = new System.Drawing.Size(506, 260);
            this.dgvOrderDetails.TabIndex = 1;
            
            // Add the split container to the main form
            this.Controls.Add(this.splitContainer1);
            this.Name = "SalesOrderView";
            this.Size = new System.Drawing.Size(1016, 626);
            
            // Button configurations
            this.btnNewOrder.Text = "New Order";
            this.btnEditOrder.Text = "Edit";
            this.btnSaveOrder.Text = "Save";
            this.btnSaveOrder.Enabled = false;
            this.btnDeleteOrder.Text = "Delete";
            this.btnGenerateInvoice.Text = "Generate Invoice";
            this.btnPrintOrder.Text = "Print";
            this.btnUpdateStatus.Text = "Update Status";
            this.btnRefresh.Text = "Refresh";
            this.btnFilter.Text = "Apply Filter";
            this.btnAddProduct.Text = "+";
            this.btnRemoveProduct.Text = "-";
            
            // Additional components setup would go here
            // For brevity, I'm omitting detailed control setup code that would be generated by the designer
            
            this.ResumeLayout(false);
        }
        
        // Control declarations
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.ComboBox cmbCustomerFilter;
        private System.Windows.Forms.Label lblCustomerFilter;
        private System.Windows.Forms.ComboBox cmbStatusFilter;
        private System.Windows.Forms.Label lblStatusFilter;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.DataGridView dgvOrders;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Panel pnlOrderDetails;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.TextBox txtTotalAmount;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.TextBox txtDiscountAmount;
        private System.Windows.Forms.Label lblDiscount;
        private System.Windows.Forms.TextBox txtShippingAmount;
        private System.Windows.Forms.Label lblShipping;
        private System.Windows.Forms.TextBox txtTaxAmount;
        private System.Windows.Forms.Label lblTax;
        private System.Windows.Forms.TextBox txtSubtotal;
        private System.Windows.Forms.Label lblSubtotal;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.DateTimePicker dtpOrderDate;
        private System.Windows.Forms.Label lblOrderDate;
        private System.Windows.Forms.TextBox txtOrderNumber;
        private System.Windows.Forms.Label lblOrderNumber;
        private System.Windows.Forms.ComboBox cmbCustomer;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnUpdateStatus;
        private System.Windows.Forms.Button btnPrintOrder;
        private System.Windows.Forms.Button btnGenerateInvoice;
        private System.Windows.Forms.Button btnDeleteOrder;
        private System.Windows.Forms.Button btnSaveOrder;
        private System.Windows.Forms.Button btnEditOrder;
        private System.Windows.Forms.Button btnNewOrder;
        private System.Windows.Forms.Panel pnlOrderItems;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnRemoveProduct;
        private System.Windows.Forms.Button btnAddProduct;
        private System.Windows.Forms.ComboBox cmbProduct;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.DataGridView dgvOrderDetails;
    }
}