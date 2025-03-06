using FantasticStock.ViewModels.Sales;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FantasticStock.Views.Sales
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
        {/*
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
                    _viewModel.SelectedOrder = dgvOrders.SelectedRows[0].DataBoundItem as FantasticStock.Models.Sales.SalesOrder;
                }
            };

            dgvOrderDetails.SelectionChanged += (s, e) =>
            {
                if (dgvOrderDetails.SelectedRows.Count > 0)
                {
                    _viewModel.SelectedOrderDetail = dgvOrderDetails.SelectedRows[0].DataBoundItem as FantasticStock.Models.Sales.SalesOrderDetail;
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
        */}
    }
}
