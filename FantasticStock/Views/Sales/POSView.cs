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
using System.Data.SqlClient;

namespace FantasticStock.Views.Sales
{
    public partial class POSView : UserControl
    {
        private List<Product> products;
        private List<Customer> customers;
        private List<SaleItem> currentSaleItems;
        private Customer selectedCustomer;
        private decimal taxRate = 0.08m; // 8% tax rate

        // If ConfigurationManager isn't available, use this fallback
        // private string connectionString = @"Data Source=DESKTOP-DOEUG5N;Initial Catalog=FantasticStock;Integrated Security=True";
        private string connectionString = @"Data Source=TUNGCORN\SQLEXPRESS;Initial Catalog=FantasticStock;Integrated Security=True";
        public POSView()
        {
            InitializeComponent();
            LoadDataFromDatabase();
            SetupEventHandlers();
            SetupProductGrid();
            UpdateSummary();
        }

        private void LoadDataFromDatabase()
        {
            try
            {
                LoadProducts();
                LoadCustomers();

                // Initialize autocomplete collections
                InitializeAutocomplete();

                // Initialize current sale
                currentSaleItems = new List<SaleItem>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data from database: {ex.Message}\nUsing sample data instead.",
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                InitializeSampleData();
            }
        }

        private void LoadProducts()
        {
            products = new List<Product>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT p.ProductID, p.SKU, p.Barcode, p.ProductName, p.Description, 
                           p.CategoryID, p.CategoryName, p.SellingPrice, p.CostPrice,
                           p.IsActive
                    FROM Product p 
                    WHERE p.IsActive = 1
                    ORDER BY p.ProductName";

                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Product product = new Product
                        {
                            ProductID = reader.GetInt32(reader.GetOrdinal("ProductID")),
                            SKU = reader.IsDBNull(reader.GetOrdinal("SKU")) ? null : reader.GetString(reader.GetOrdinal("SKU")),
                            Barcode = reader.IsDBNull(reader.GetOrdinal("Barcode")) ? null : reader.GetString(reader.GetOrdinal("Barcode")),
                            ProductName = reader.GetString(reader.GetOrdinal("ProductName")),
                            Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),
                            CategoryID = reader.IsDBNull(reader.GetOrdinal("CategoryID")) ? 0 : reader.GetInt32(reader.GetOrdinal("CategoryID")),
                            CategoryName = reader.IsDBNull(reader.GetOrdinal("CategoryName")) ? null : reader.GetString(reader.GetOrdinal("CategoryName")),
                            SellingPrice = reader.GetDecimal(reader.GetOrdinal("SellingPrice")),
                            CostPrice = reader.IsDBNull(reader.GetOrdinal("CostPrice")) ? 0 : reader.GetDecimal(reader.GetOrdinal("CostPrice")),
                            IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"))
                        };

                        products.Add(product);
                    }
                }
            }
        }

        private void LoadCustomers()
        {
            customers = new List<Customer>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT c.CustomerID, c.CustomerName, c.Phone, c.Email, 
                   c.Address, c.LoyaltyPoints, c.IsActive
            FROM Customer c 
            WHERE c.IsActive = 1
            ORDER BY c.CustomerName";

                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Customer customer = new Customer
                        {
                            CustomerID = reader.GetInt32(reader.GetOrdinal("CustomerID")),
                            CustomerName = reader.GetString(reader.GetOrdinal("CustomerName")),
                            Phone = reader.IsDBNull(reader.GetOrdinal("Phone")) ? null : reader.GetString(reader.GetOrdinal("Phone")),
                            Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString(reader.GetOrdinal("Email")),
                            Address = reader.IsDBNull(reader.GetOrdinal("Address")) ? null : reader.GetString(reader.GetOrdinal("Address")),
                            LoyaltyPoints = reader.IsDBNull(reader.GetOrdinal("LoyaltyPoints")) ? 0 : reader.GetInt32(reader.GetOrdinal("LoyaltyPoints")),
                            IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"))
                        };

                        customers.Add(customer);
                    }
                }
            }
        }


        private void InitializeAutocomplete()
        {
            // Autocomplete initialization remains unchanged
            productSearchAutoComplete = new AutoCompleteStringCollection();
            customerAutoComplete = new AutoCompleteStringCollection();

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
            customerComboBox.Items.Clear();
            customerComboBox.Items.AddRange(customers.Select(c => c.CustomerName).ToArray());
        }

        // Modified sample data method without UnitsInStock
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

        // Event handlers setup remains unchanged
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

        // Modified SetupProductGrid without UnitsInStock check
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

        // Updated BarcodeTextBox_KeyDown without UnitsInStock check
        private void BarcodeTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string barcode = barcodeTextBox.Text.Trim();

                try
                {
                    // Try to find product in database first
                    Product product = FindProductByBarcode(barcode);

                    // If not found in database, try local cache
                    if (product == null)
                    {
                        product = products.FirstOrDefault(p => p.Barcode == barcode);
                    }

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
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error scanning barcode: {ex.Message}",
                        "Barcode Scan Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        // Updated FindProductByBarcode without UnitsInStock
        private Product FindProductByBarcode(string barcode)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Product WHERE Barcode = @Barcode AND IsActive = 1";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Barcode", barcode);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Product product = new Product
                        {
                            ProductID = reader.GetInt32(reader.GetOrdinal("ProductID")),
                            SKU = reader.IsDBNull(reader.GetOrdinal("SKU")) ? null : reader.GetString(reader.GetOrdinal("SKU")),
                            Barcode = reader.IsDBNull(reader.GetOrdinal("Barcode")) ? null : reader.GetString(reader.GetOrdinal("Barcode")),
                            ProductName = reader.GetString(reader.GetOrdinal("ProductName")),
                            Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),
                            CategoryID = reader.IsDBNull(reader.GetOrdinal("CategoryID")) ? 0 : reader.GetInt32(reader.GetOrdinal("CategoryID")),
                            CategoryName = reader.IsDBNull(reader.GetOrdinal("CategoryName")) ? null : reader.GetString(reader.GetOrdinal("CategoryName")),
                            SellingPrice = reader.GetDecimal(reader.GetOrdinal("SellingPrice")),
                            CostPrice = reader.IsDBNull(reader.GetOrdinal("CostPrice")) ? 0 : reader.GetDecimal(reader.GetOrdinal("CostPrice")),
                            IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"))
                        };

                        return product;
                    }
                }
            }

            return null;
        }

        // UpdateCustomerInfo and ApplyLoyaltyDiscount remain unchanged
        private void UpdateCustomerInfo()
        {
            string customerName = customerComboBox.Text.Trim();
            selectedCustomer = customers.FirstOrDefault(c => c.CustomerName.Equals(customerName, StringComparison.OrdinalIgnoreCase));

            if (selectedCustomer != null)
            {
                loyaltyInfoLabel.Text = $"Loyalty Points: {selectedCustomer.LoyaltyPoints}";

                // Apply discount based on customer's loyalty tier
                ApplyLoyaltyDiscount();

                UpdateCartDisplay();
                UpdateSummary();
            }
            else
            {
                loyaltyInfoLabel.Text = "Loyalty Points: None";
            }
        }

        private void ApplyLoyaltyDiscount()
        {
            // Apply discount based on loyalty points or customer type
            if (selectedCustomer != null)
            {
                decimal discountPercentage = 0;

                // Apply discount based on loyalty points tier
                if (selectedCustomer.LoyaltyPoints >= 500)
                    discountPercentage = 0.10m; // 10% discount for 500+ points
                else if (selectedCustomer.LoyaltyPoints >= 300)
                    discountPercentage = 0.08m; // 8% discount for 300+ points
                else if (selectedCustomer.LoyaltyPoints >= 100)
                    discountPercentage = 0.05m; // 5% discount for 100+ points

                // Apply discount to all items in cart
                foreach (var item in currentSaleItems)
                {
                    item.Discount = item.UnitPrice * discountPercentage;
                }
            }
        }

        // Updated ProductSearchTextBox_KeyDown method without UnitsInStock check
        private void ProductSearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string searchText = productSearchTextBox.Text.Trim();

                try
                {
                    // Try to find product in database directly
                    Product product = FindProductByName(searchText);

                    // If not found in database, try local cache
                    if (product == null)
                    {
                        product = products.FirstOrDefault(p =>
                            p.ProductName.Equals(searchText, StringComparison.OrdinalIgnoreCase));
                    }

                    if (product != null)
                    {
                        AddProductToCart(product);
                        productSearchTextBox.Clear();
                    }

                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error searching for product: {ex.Message}",
                        "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Updated FindProductByName method without UnitsInStock
        private Product FindProductByName(string productName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Product WHERE ProductName LIKE @ProductName AND IsActive = 1";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ProductName", "%" + productName + "%");

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Product product = new Product
                        {
                            ProductID = reader.GetInt32(reader.GetOrdinal("ProductID")),
                            SKU = reader.IsDBNull(reader.GetOrdinal("SKU")) ? null : reader.GetString(reader.GetOrdinal("SKU")),
                            Barcode = reader.IsDBNull(reader.GetOrdinal("Barcode")) ? null : reader.GetString(reader.GetOrdinal("Barcode")),
                            ProductName = reader.GetString(reader.GetOrdinal("ProductName")),
                            Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),
                            CategoryID = reader.IsDBNull(reader.GetOrdinal("CategoryID")) ? 0 : reader.GetInt32(reader.GetOrdinal("CategoryID")),
                            CategoryName = reader.IsDBNull(reader.GetOrdinal("CategoryName")) ? null : reader.GetString(reader.GetOrdinal("CategoryName")),
                            SellingPrice = reader.GetDecimal(reader.GetOrdinal("SellingPrice")),
                            CostPrice = reader.IsDBNull(reader.GetOrdinal("CostPrice")) ? 0 : reader.GetDecimal(reader.GetOrdinal("CostPrice")),
                            IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"))
                        };

                        return product;
                    }
                }
            }

            return null;
        }

        // Updated ProductButton_Click without UnitsInStock check
        private void ProductButton_Click(object sender, EventArgs e)
        {
            if (sender is Button button && button.Tag is Product product)
            {
                AddProductToCart(product);
            }
        }

        // Updated AddProductToCart method without UnitsInStock check
        private void AddProductToCart(Product product)
        {
            SaleItem existingItem = currentSaleItems.FirstOrDefault(item => item.ProductID == product.ProductID);

            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                currentSaleItems.Add(new SaleItem
                {
                    ProductID = product.ProductID,
                    ProductName = product.ProductName,
                    Quantity = 1,
                    UnitPrice = product.SellingPrice,
                    Discount = 0
                });

                // Apply any applicable customer discounts
                if (selectedCustomer != null && selectedCustomer.LoyaltyPoints >= 100)
                {
                    ApplyLoyaltyDiscount();
                }
            }

            UpdateCartDisplay();
            UpdateSummary();
        }

        // Updated CartDataGridView_CellValueChanged method without UnitsInStock check
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
                        currentSaleItems[e.RowIndex].Quantity = quantity;
                    }

                    UpdateCartDisplay();
                    UpdateSummary();
                }
            }
        }

        // CompleteTransactionButton_Click remains largely the same
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

                try
                {
                    // Save transaction to database
                    SaveTransactionToDatabase(tenderedAmount);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error completing transaction: {ex.Message}",
                        "Transaction Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid payment amount.", "Invalid Amount",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Updated SaveTransactionToDatabase to remove inventory tracking since UnitsInStock is not in our schema
        private void SaveTransactionToDatabase(decimal tenderedAmount)
        {
            // Get selected customer ID (if any)
            int? customerID = selectedCustomer?.CustomerID;

            // Calculate transaction totals
            decimal subtotal = currentSaleItems.Sum(item => item.UnitPrice * item.Quantity);
            decimal discountAmount = currentSaleItems.Sum(item => item.Discount * item.Quantity);
            decimal discountedSubtotal = subtotal - discountAmount;
            decimal taxAmount = discountedSubtotal * taxRate;
            decimal totalAmount = discountedSubtotal + taxAmount;
            decimal changeAmount = tenderedAmount - totalAmount;

            // Begin database transaction
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // 1. Insert into Sale table
                    string saleNumber = $"SALE-{DateTime.Now:yyyyMMddHHmmss}";
                    string insertSaleQuery = @"
                INSERT INTO Sale (SaleNumber, CustomerID, SaleDate, SubTotal, TaxAmount, 
                                DiscountAmount, TotalAmount, PaymentMethod, TenderedAmount, 
                                ChangeAmount, PrintReceipt, EmailReceipt, IsCompleted, CreatedDate)
                OUTPUT INSERTED.SaleID
                VALUES (@SaleNumber, @CustomerID, @SaleDate, @SubTotal, @TaxAmount,
                        @DiscountAmount, @TotalAmount, @PaymentMethod, @TenderedAmount,
                        @ChangeAmount, @PrintReceipt, @EmailReceipt, @IsCompleted, @CreatedDate)";

                    SqlCommand insertSaleCmd = new SqlCommand(insertSaleQuery, connection, transaction);
                    insertSaleCmd.Parameters.AddWithValue("@SaleNumber", saleNumber);
                    insertSaleCmd.Parameters.AddWithValue("@CustomerID", customerID.HasValue ? (object)customerID.Value : DBNull.Value);
                    insertSaleCmd.Parameters.AddWithValue("@SaleDate", DateTime.Now);
                    insertSaleCmd.Parameters.AddWithValue("@SubTotal", subtotal);
                    insertSaleCmd.Parameters.AddWithValue("@TaxAmount", taxAmount);
                    insertSaleCmd.Parameters.AddWithValue("@DiscountAmount", discountAmount);
                    insertSaleCmd.Parameters.AddWithValue("@TotalAmount", totalAmount);
                    insertSaleCmd.Parameters.AddWithValue("@PaymentMethod", paymentMethodComboBox.Text);
                    insertSaleCmd.Parameters.AddWithValue("@TenderedAmount", tenderedAmount);
                    insertSaleCmd.Parameters.AddWithValue("@ChangeAmount", changeAmount > 0 ? changeAmount : 0);
                    insertSaleCmd.Parameters.AddWithValue("@PrintReceipt", printReceiptCheckBox.Checked);
                    insertSaleCmd.Parameters.AddWithValue("@EmailReceipt", emailReceiptCheckBox.Checked);
                    insertSaleCmd.Parameters.AddWithValue("@IsCompleted", true);
                    insertSaleCmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);

                    // Get the ID of the inserted sale
                    int saleID = (int)insertSaleCmd.ExecuteScalar();

                    // 2. Insert each sale item
                    foreach (var item in currentSaleItems)
                    {
                        string insertItemQuery = @"
                    INSERT INTO SaleItem (SaleID, ProductID, Quantity, UnitPrice, Discount, TotalPrice)
                    VALUES (@SaleID, @ProductID, @Quantity, @UnitPrice, @Discount, @TotalPrice)";

                        SqlCommand insertItemCmd = new SqlCommand(insertItemQuery, connection, transaction);
                        insertItemCmd.Parameters.AddWithValue("@SaleID", saleID);
                        insertItemCmd.Parameters.AddWithValue("@ProductID", item.ProductID);
                        insertItemCmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                        insertItemCmd.Parameters.AddWithValue("@UnitPrice", item.UnitPrice);
                        insertItemCmd.Parameters.AddWithValue("@Discount", item.Discount);
                        insertItemCmd.Parameters.AddWithValue("@TotalPrice", (item.UnitPrice - item.Discount) * item.Quantity);
                        insertItemCmd.ExecuteNonQuery();
                    }

                    // 3. If customer exists, update their loyalty points
                    if (customerID.HasValue)
                    {
                        // Add 1 point for every $10 spent (rounded down)
                        int pointsEarned = (int)(totalAmount / 10);

                        string updateCustomerQuery = @"
                    UPDATE Customer
                    SET LoyaltyPoints = LoyaltyPoints + @PointsEarned,
                        ModifiedDate = @ModifiedDate
                    WHERE CustomerID = @CustomerID";

                        SqlCommand updateCustomerCmd = new SqlCommand(updateCustomerQuery, connection, transaction);
                        updateCustomerCmd.Parameters.AddWithValue("@PointsEarned", pointsEarned);
                        updateCustomerCmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                        updateCustomerCmd.Parameters.AddWithValue("@CustomerID", customerID.Value);
                        updateCustomerCmd.ExecuteNonQuery();
                    }

                    // Commit the transaction
                    transaction.Commit();

                    // Show success message
                    string receiptOptions = "";
                    if (printReceiptCheckBox.Checked)
                        receiptOptions += "Print ";
                    if (emailReceiptCheckBox.Checked)
                        receiptOptions += "Email ";

                    MessageBox.Show($"Transaction completed successfully!\n\n" +
                                   $"Sale #: {saleNumber}\n" +
                                   $"Total: ${totalAmount:F2}\n" +
                                   $"Tendered: ${tenderedAmount:F2}\n" +
                                   $"Change: ${(changeAmount > 0 ? changeAmount : 0):F2}\n" +
                                   $"Payment Method: {paymentMethodComboBox.Text}\n" +
                                   $"Receipt: {(receiptOptions.Length > 0 ? receiptOptions : "None")}",
                                   "Transaction Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Update cached data
                    if (customerID.HasValue)
                    {
                        // Update local customer data
                        var customer = customers.FirstOrDefault(c => c.CustomerID == customerID.Value);
                        if (customer != null)
                        {
                            customer.LoyaltyPoints += (int)(totalAmount / 10);
                        }
                    }

                    // Reset transaction UI
                    ResetTransaction();
                }
                catch (Exception ex)
                {
                    // Roll back the transaction
                    transaction.Rollback();
                    throw new Exception($"Transaction failed: {ex.Message}", ex);
                }
            }
        }

        // Other UI methods remain unchanged
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

        private void CustomerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCustomerInfo();
        }

        private void CustomerSearchButton_Click(object sender, EventArgs e)
        {
            UpdateCustomerInfo();
        }

        private void UpdateCartDisplay()
        {
            cartDataGridView.Rows.Clear();

            foreach (var item in currentSaleItems)
            {
                decimal totalPrice = (item.UnitPrice - item.Discount) * item.Quantity;

                cartDataGridView.Rows.Add(
                    item.ProductID,
                    item.ProductName,
                    item.Quantity,
                    $"${item.UnitPrice:F2}",
                    $"${item.Discount:F2}",
                    $"${totalPrice:F2}",
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
                decimal itemTotal = item.UnitPrice * item.Quantity;
                decimal itemDiscount = item.Discount * item.Quantity;

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
            decimal subtotal = currentSaleItems.Sum(item => (item.UnitPrice - item.Discount) * item.Quantity);
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
            selectedCustomer = null;
            UpdateSummary();
        }

        // Method to refresh customer data from database
        public void RefreshCustomers()
        {
            try
            {
                customers.Clear();
                LoadCustomers();

                // Update the customer drop-down
                customerComboBox.Items.Clear();
                customerComboBox.Items.AddRange(customers.Select(c => c.CustomerName).ToArray());

                // Update autocomplete
                customerAutoComplete = new AutoCompleteStringCollection();
                foreach (var customer in customers)
                {
                    customerAutoComplete.Add(customer.CustomerName);
                }
                customerComboBox.AutoCompleteCustomSource = customerAutoComplete;

                // If a customer was selected, try to find them again
                if (selectedCustomer != null)
                {
                    var refreshedCustomer = customers.FirstOrDefault(c => c.CustomerID == selectedCustomer.CustomerID);
                    if (refreshedCustomer != null)
                    {
                        selectedCustomer = refreshedCustomer;
                        customerComboBox.Text = selectedCustomer.CustomerName;
                        UpdateCustomerInfo();
                    }
                    else
                    {
                        // The previously selected customer is no longer available
                        selectedCustomer = null;
                        customerComboBox.Text = "";
                        loyaltyInfoLabel.Text = "Loyalty Points: None";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error refreshing customer data: {ex.Message}",
                    "Refresh Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Method to refresh product data from database
        public void RefreshProducts()
        {
            try
            {
                products.Clear();
                LoadProducts();

                // Rebuild product grid
                SetupProductGrid();

                // Update autocomplete
                productSearchAutoComplete = new AutoCompleteStringCollection();
                foreach (var product in products)
                {
                    productSearchAutoComplete.Add(product.ProductName);
                }
                productSearchTextBox.AutoCompleteCustomSource = productSearchAutoComplete;

                // Check if any products in the cart are no longer available
                List<SaleItem> itemsToRemove = new List<SaleItem>();
                foreach (var item in currentSaleItems)
                {
                    if (!products.Any(p => p.ProductID == item.ProductID))
                    {
                        itemsToRemove.Add(item);
                    }
                }

                // Remove unavailable items
                foreach (var item in itemsToRemove)
                {
                    currentSaleItems.Remove(item);
                }

                // Update the cart display if needed
                if (itemsToRemove.Count > 0)
                {
                    UpdateCartDisplay();
                    UpdateSummary();
                    MessageBox.Show($"{itemsToRemove.Count} item(s) have been removed from your cart because they are no longer available.",
                        "Items Removed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error refreshing product data: {ex.Message}",
                    "Refresh Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Helper method to generate a receipt
        private string GenerateReceipt(string saleNumber, decimal subtotal, decimal discountAmount,
                                      decimal taxAmount, decimal totalAmount, decimal tenderedAmount,
                                      decimal changeAmount, string paymentMethod)
        {
            StringBuilder receipt = new StringBuilder();

            // Header
            receipt.AppendLine("FANTASTIC STOCK");
            receipt.AppendLine("123 Main Street, Anytown USA");
            receipt.AppendLine("Phone: (555) 123-4567");
            receipt.AppendLine("======================================");
            receipt.AppendLine($"Sale #: {saleNumber}");
            receipt.AppendLine($"Date: {DateTime.Now:yyyy-MM-dd hh:mm tt}");
            if (selectedCustomer != null)
            {
                receipt.AppendLine($"Customer: {selectedCustomer.CustomerName}");
            }
            receipt.AppendLine("======================================");

            // Items
            receipt.AppendLine("ITEMS");
            receipt.AppendLine("--------------------------------------");
            foreach (var item in currentSaleItems)
            {
                decimal itemTotal = (item.UnitPrice - item.Discount) * item.Quantity;
                receipt.AppendLine($"{item.Quantity} x {item.ProductName}");
                receipt.AppendLine($"  ${item.UnitPrice:F2} each");

                if (item.Discount > 0)
                {
                    decimal discountPercent = (item.Discount / item.UnitPrice) * 100;
                    receipt.AppendLine($"  Discount: {discountPercent:F0}% (${item.Discount:F2})");
                }

                receipt.AppendLine($"  Subtotal: ${itemTotal:F2}");
                receipt.AppendLine();
            }

            // Summary
            receipt.AppendLine("======================================");
            receipt.AppendLine($"Subtotal: ${subtotal:F2}");
            if (discountAmount > 0)
            {
                receipt.AppendLine($"Discount: ${discountAmount:F2}");
            }
            receipt.AppendLine($"Tax ({taxRate * 100:F0}%): ${taxAmount:F2}");
            receipt.AppendLine($"TOTAL: ${totalAmount:F2}");
            receipt.AppendLine("--------------------------------------");
            receipt.AppendLine($"Payment Method: {paymentMethod}");
            receipt.AppendLine($"Amount Tendered: ${tenderedAmount:F2}");
            receipt.AppendLine($"Change: ${changeAmount:F2}");
            receipt.AppendLine("======================================");

            // Footer with loyalty info
            if (selectedCustomer != null)
            {
                int pointsEarned = (int)(totalAmount / 10);
                int newTotalPoints = selectedCustomer.LoyaltyPoints + pointsEarned;
                receipt.AppendLine($"Loyalty Points Earned: {pointsEarned}");
                receipt.AppendLine($"Total Loyalty Points: {newTotalPoints}");
            }

            receipt.AppendLine("Thank you for shopping with us!");

            return receipt.ToString();
        }

        // Helper method to email a receipt
        private bool EmailReceipt(string emailAddress, string subject, string body)
        {
            try
            {
                // This is a placeholder. In a real application, you would use
                // System.Net.Mail.SmtpClient or another email library to send the email.
                // For now, just show a message that it would be emailed
                MessageBox.Show($"Receipt would be emailed to: {emailAddress}\n\nThis is a placeholder for the actual email functionality.",
                    "Email Receipt", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sending email: {ex.Message}",
                    "Email Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Helper method to print a receipt
        private bool PrintReceipt(string receiptText)
        {
            try
            {
                // This is a placeholder. In a real application, you would use
                // System.Drawing.Printing.PrintDocument to format and print the receipt.
                // For now, just show a message that it would be printed
                MessageBox.Show("Receipt would be printed.\n\nThis is a placeholder for the actual printing functionality.",
                    "Print Receipt", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error printing receipt: {ex.Message}",
                    "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }

    // SaleItem class modified to work with the existing Product model
    public class SaleItem
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
    }
}
