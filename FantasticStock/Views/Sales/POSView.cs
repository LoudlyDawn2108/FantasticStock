using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FantasticStock.Models.Sales;
using FantasticStock.Models.Inventory;

namespace FantasticStock.Views.Sales
{
    public partial class POSView : UserControl
    {
        private List<Product> products;
        private List<Customer> customers;
        private List<SaleItem> currentSaleItems;
        private decimal taxRate = 0.08m; // 8% tax rate
      

        public POSView()
        {
            InitializeComponent();
            InitializeSampleData();
            SetupEventHandlers();
            SetupProductGrid();
            UpdateSummary();
        }

        private void InitializeSampleData()
        {
            // Initialize AutoComplete collections first
            productSearchAutoComplete = new AutoCompleteStringCollection();
            customerAutoComplete = new AutoCompleteStringCollection();

            // Initialize sample products using your existing Product model
            products = new List<Product>
            {
                new Product { ProductID = 1, ProductName = "Laptop", SellingPrice = 899.99m, Barcode = "1001", SKU = "LAP001", IsActive = true },
                new Product { ProductID = 2, ProductName = "Smartphone", SellingPrice = 499.99m, Barcode = "1002", SKU = "PHN001", IsActive = true },
                new Product { ProductID = 3, ProductName = "Monitor", SellingPrice = 249.99m, Barcode = "1003", SKU = "MON001", IsActive = true },
                new Product { ProductID = 4, ProductName = "Keyboard", SellingPrice = 49.99m, Barcode = "1004", SKU = "KBD001", IsActive = true },
                new Product { ProductID = 5, ProductName = "Mouse", SellingPrice = 29.99m, Barcode = "1005", SKU = "MOU001", IsActive = true },
                new Product { ProductID = 6, ProductName = "Headphones", SellingPrice = 79.99m, Barcode = "1006", SKU = "HEA001", IsActive = true },
                new Product { ProductID = 7, ProductName = "USB Drive", SellingPrice = 19.99m, Barcode = "1007", SKU = "USB001", IsActive = true },
                new Product { ProductID = 8, ProductName = "External HDD", SellingPrice = 99.99m, Barcode = "1008", SKU = "HDD001", IsActive = true }
            };

            // Initialize sample customers with loyalty points using your existing Customer model
            customers = new List<Customer>
            {
                new Customer { CustomerID = 1, CustomerName = "John Smith", Email = "john@example.com", LoyaltyPoints = 150, Phone = "555-123-4567" },
                new Customer { CustomerID = 2, CustomerName = "Jane Doe", Email = "jane@example.com", LoyaltyPoints = 320, Phone = "555-987-6543" },
                new Customer { CustomerID = 3, CustomerName = "Bob Johnson", Email = "bob@example.com", LoyaltyPoints = 75, Phone = "555-456-7890" },
                new Customer { CustomerID = 4, CustomerName = "Alice Brown", Email = "alice@example.com", LoyaltyPoints = 210, Phone = "555-789-0123" }
            };

            // Initialize current sale
            currentSaleItems = new List<SaleItem>();

            // Set up autocomplete for product search
            foreach (var product in products)
            {
                productSearchAutoComplete.Add(product.ProductName);
            }
            productSearchTextBox.AutoCompleteCustomSource = productSearchAutoComplete;

            // Set up autocomplete for customer search
            foreach (var customer in customers)
            {
                customerAutoComplete.Add(customer.CustomerName);
            }
            customerComboBox.AutoCompleteCustomSource = customerAutoComplete;
            customerComboBox.Items.AddRange(customers.Select(c => c.CustomerName).ToArray());
        }

        private void SetupEventHandlers()
        {
            // Set up event handlers for controls
            productSearchTextBox.KeyDown += ProductSearchTextBox_KeyDown;
            barcodeTextBox.KeyDown += BarcodeTextBox_KeyDown;
            customerSearchButton.Click += CustomerSearchButton_Click;
            customerComboBox.SelectedIndexChanged += CustomerComboBox_SelectedIndexChanged;
            cartDataGridView.CellContentClick += CartDataGridView_CellContentClick;
            cartDataGridView.CellValueChanged += CartDataGridView_CellValueChanged;
            tenderedAmountTextBox.TextChanged += TenderedAmountTextBox_TextChanged;
            completeTransactionButton.Click += CompleteTransactionButton_Click;
            cancelTransactionButton.Click += CancelTransactionButton_Click;
        }

        private void SetupProductGrid()
        {
            // Create product buttons dynamically
            productsFlowLayoutPanel.Controls.Clear();
            foreach (var product in products)
            {
                Button productButton = new Button
                {
                    Text = $"{product.ProductName}\n${product.SellingPrice:F2}",
                    Size = new Size(100, 60),
                    Tag = product,
                    BackColor = Color.AliceBlue,
                    Margin = new Padding(5)
                };

                productButton.Click += ProductButton_Click;
                productsFlowLayoutPanel.Controls.Add(productButton);
            }
        }

        private void ProductButton_Click(object sender, EventArgs e)
        {
            if (sender is Button button && button.Tag is Product product)
            {
                AddProductToCart(product);
            }
        }

        private void ProductSearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string searchText = productSearchTextBox.Text.Trim();
                Product product = products.FirstOrDefault(p => p.ProductName.Equals(searchText, StringComparison.OrdinalIgnoreCase));

                if (product != null)
                {
                    AddProductToCart(product);
                    productSearchTextBox.Clear();
                }

                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void BarcodeTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string barcode = barcodeTextBox.Text.Trim();
                Product product = products.FirstOrDefault(p => p.Barcode == barcode);

                if (product != null)
                {
                    AddProductToCart(product);
                    barcodeTextBox.Clear();
                }
                else
                {
                    MessageBox.Show($"No product found with barcode: {barcode}", "Product Not Found",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void CustomerSearchButton_Click(object sender, EventArgs e)
        {
            UpdateCustomerInfo();
        }

        private void CustomerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCustomerInfo();
            UpdateSummary(); // Recalculate to apply loyalty discounts if applicable
        }

        private void CartDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Handle remove button click
            if (e.RowIndex >= 0 && e.ColumnIndex == removeColumn.Index)
            {
                currentSaleItems.RemoveAt(e.RowIndex);
                UpdateCartDisplay();
                UpdateSummary();
            }
        }

        private void CartDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Handle quantity changes
            if (e.RowIndex >= 0 && e.ColumnIndex == quantityColumn.Index)
            {
                if (int.TryParse(cartDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString(), out int quantity))
                {
                    if (quantity <= 0)
                    {
                        currentSaleItems.RemoveAt(e.RowIndex);
                    }
                    else
                    {
                        // Access via index since we don't have direct quantity property in referenced model
                        var item = currentSaleItems[e.RowIndex];
                        // We'll store quantity in Tag since SaleItem doesn't have a Quantity property
                        item.Tag = quantity;
                        CalculateLineTotal(item);
                    }

                    UpdateCartDisplay();
                    UpdateSummary();
                }
            }
        }

        private void TenderedAmountTextBox_TextChanged(object sender, EventArgs e)
        {
            if (decimal.TryParse(tenderedAmountTextBox.Text, out decimal tenderedAmount))
            {
                decimal total = GetTotalAmount();
                decimal change = tenderedAmount - total;

                changeAmountLabel.Text = change >= 0 ? $"${change:F2}" : "$0.00";
            }
            else
            {
                changeAmountLabel.Text = "$0.00";
            }
        }

        private void CompleteTransactionButton_Click(object sender, EventArgs e)
        {
            if (currentSaleItems.Count == 0)
            {
                MessageBox.Show("Cannot complete transaction with no items.", "Empty Transaction",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (decimal.TryParse(tenderedAmountTextBox.Text, out decimal tenderedAmount))
            {
                decimal total = GetTotalAmount();
                if (tenderedAmount < total && paymentMethodComboBox.Text == "Cash")
                {
                    MessageBox.Show("Insufficient payment amount.", "Payment Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string receiptOptions = "";
                if (printReceiptCheckBox.Checked)
                    receiptOptions += "Print ";
                if (emailReceiptCheckBox.Checked)
                    receiptOptions += "Email ";

                MessageBox.Show($"Transaction completed successfully!\n\n" +
                                $"Total: ${total:F2}\n" +
                                $"Tendered: ${tenderedAmount:F2}\n" +
                                $"Change: ${changeAmountLabel.Text.Replace("$", "")}\n" +
                                $"Payment Method: {paymentMethodComboBox.Text}\n" +
                                $"Receipt: {(receiptOptions.Length > 0 ? receiptOptions : "None")}",
                                "Transaction Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ResetTransaction();
            }
            else
            {
                MessageBox.Show("Please enter a valid payment amount.", "Invalid Amount",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void CancelTransactionButton_Click(object sender, EventArgs e)
        {
            if (currentSaleItems.Count > 0)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to cancel this transaction?",
                    "Confirm Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    ResetTransaction();
                }
            }
        }

        private void AddProductToCart(Product product)
        {
            SaleItem existingItem = currentSaleItems.FirstOrDefault(item =>
                (int)item.ItemID == product.ProductID);

            if (existingItem != null)
            {
                // Increment quantity stored in Tag
                int quantity = (int)existingItem.Tag + 1;
                existingItem.Tag = quantity;
                CalculateLineTotal(existingItem);
            }
            else
            {
                // Create new SaleItem using correct properties from the signature
                var newItem = new SaleItem
                {
                    ItemID = product.ProductID,
                    DiscountAmount = 0,
                    DiscountPercent = 0,
                    Tag = 1 // Store quantity in Tag
                };

                // Store product info in the Tag property for reference
                newItem.ItemInfo = new
                {
                    ProductName = product.ProductName,
                    UnitPrice = product.SellingPrice
                };

                CalculateLineTotal(newItem);
                currentSaleItems.Add(newItem);
            }

            UpdateCartDisplay();
            UpdateSummary();
        }

        private void CalculateLineTotal(SaleItem item)
        {
            // Get quantity from Tag
            int quantity = (int)item.Tag;

            // Get unit price from ItemInfo
            decimal unitPrice = ((dynamic)item.ItemInfo).UnitPrice;

            // Calculate line total accounting for discount
            decimal discountedUnitPrice = unitPrice * (1 - (item.DiscountPercent / 100)) - item.DiscountAmount;
            item.LineTotal = discountedUnitPrice * quantity;
        }

        private void UpdateCustomerInfo()
        {
            string customerName = customerComboBox.Text.Trim();
            Customer customer = customers.FirstOrDefault(c => c.CustomerName.Equals(customerName, StringComparison.OrdinalIgnoreCase));

            if (customer != null)
            {
                loyaltyInfoLabel.Text = $"Loyalty Points: {customer.LoyaltyPoints}";

                // Apply discount if customer has sufficient loyalty points
                if (customer.LoyaltyPoints >= 100)
                {
                    foreach (var item in currentSaleItems)
                    {
                        // Apply 5% discount using DiscountPercent property
                        item.DiscountPercent = 5.0m;
                        CalculateLineTotal(item);
                    }
                    UpdateCartDisplay();
                    UpdateSummary();
                }
                else
                {
                    // Remove discounts if loyalty points are insufficient
                    foreach (var item in currentSaleItems)
                    {
                        item.DiscountPercent = 0;
                        item.DiscountAmount = 0;
                        CalculateLineTotal(item);
                    }
                    UpdateCartDisplay();
                    UpdateSummary();
                }
            }
            else
            {
                loyaltyInfoLabel.Text = "Loyalty Points: None";

                // Remove discounts if no customer selected
                foreach (var item in currentSaleItems)
                {
                    item.DiscountPercent = 0;
                    item.DiscountAmount = 0;
                    CalculateLineTotal(item);
                }
                UpdateCartDisplay();
                UpdateSummary();
            }
        }

        private void UpdateCartDisplay()
        {
            cartDataGridView.Rows.Clear();

            foreach (var item in currentSaleItems)
            {
                // Get product information from ItemInfo
                dynamic itemInfo = item.ItemInfo;
                string productName = itemInfo.ProductName;
                decimal unitPrice = itemInfo.UnitPrice;
                int quantity = (int)item.Tag;

                // Calculate discount amount for display
                decimal totalDiscount = (unitPrice * (item.DiscountPercent / 100) + item.DiscountAmount) * quantity;

                cartDataGridView.Rows.Add(
    (int)item.ItemID,
    productName,
    quantity,
    $"${unitPrice:F2}",
    $"${totalDiscount:F2}",
    $"${item.LineTotal:F2}",
    "Remove"
);
            }
        }

        private void UpdateSummary()
        {
            decimal subtotal = 0;
            decimal totalDiscount = 0;

            foreach (var item in currentSaleItems)
            {
                dynamic itemInfo = item.ItemInfo;
                decimal unitPrice = itemInfo.UnitPrice;
                int quantity = (int)item.Tag;

                decimal itemTotal = unitPrice * quantity;
                decimal itemDiscount = (unitPrice * (item.DiscountPercent / 100) + item.DiscountAmount) * quantity;

                subtotal += itemTotal;
                totalDiscount += itemDiscount;
            }

            decimal discountedSubtotal = subtotal - totalDiscount;
            decimal tax = discountedSubtotal * taxRate;
            decimal total = discountedSubtotal + tax;

            subtotalAmountLabel.Text = $"${subtotal:F2}";
            discountAmountLabel.Text = $"${totalDiscount:F2}";
            taxAmountLabel.Text = $"${tax:F2}";
            totalAmountLabel.Text = $"${total:F2}";

            // Update change if tendered amount is entered
            if (decimal.TryParse(tenderedAmountTextBox.Text, out decimal tenderedAmount))
            {
                decimal change = tenderedAmount - total;
                changeAmountLabel.Text = change >= 0 ? $"${change:F2}" : "$0.00";
            }
        }

        private decimal GetTotalAmount()
        {
            decimal subtotal = currentSaleItems.Sum(item => item.LineTotal);
            decimal tax = subtotal * taxRate;
            return subtotal + tax;
        }

        private void ResetTransaction()
        {
            currentSaleItems.Clear();
            cartDataGridView.Rows.Clear();
            customerComboBox.Text = "";
            loyaltyInfoLabel.Text = "Loyalty Points: None";
            tenderedAmountTextBox.Clear();
            changeAmountLabel.Text = "$0.00";
            paymentMethodComboBox.SelectedIndex = -1;
            printReceiptCheckBox.Checked = false;
            emailReceiptCheckBox.Checked = false;
            UpdateSummary();
        }
    }

    
}
