using FantasticStock.Models.Financial;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FantasticStock.Views.Financial
{
    public partial class AddPaymentForm : Form
    {
        private const string ConnectionString = "Data Source=TUNGCORN\\SQLEXPRESS;Initial Catalog=FantasticStock1;Integrated Security=True;TrustServerCertificate=True";
        private Payment _payment;
        private bool _isEditMode;

        public AddPaymentForm()
        {
            InitializeComponent();
            _isEditMode = false;
            LoadCustomers();
            LoadInvoices();
            LoadPaymentMethods();
            GeneratePaymentNumber();
        }

        // Constructor for editing an existing payment
        public AddPaymentForm(Payment payment) : this()
        {
            _payment = payment;
            _isEditMode = true;
            PopulateFormWithPaymentData();
        }

        // Method to fill form with existing payment data for editing
        private void PopulateFormWithPaymentData()
        {
            // Change the form title
            this.Text = "Edit Payment";
            btnSave.Text = "Update";

            // Populate fields with payment data
            txtPaymentNumber.Text = _payment.PaymentNumber;
            dtpPaymentDate.Value = _payment.PaymentDate;

            // Select the right customer
            for (int i = 0; i < cboCustomer.Items.Count; i++)
            {
                var item = cboCustomer.Items[i] as ComboboxItem;
                if (item != null && Convert.ToInt32(item.Value) == _payment.CustomerID)
                {
                    cboCustomer.SelectedIndex = i;
                    break;
                }
            }

            // This will trigger loading the invoices for this customer

            // Select the invoice if applicable
            if (_payment.InvoiceID.HasValue)
            {
                for (int i = 0; i < cboInvoice.Items.Count; i++)
                {
                    var item = cboInvoice.Items[i] as ComboboxItem;
                    if (item != null && Convert.ToInt32(item.Value) == _payment.InvoiceID)
                    {
                        cboInvoice.SelectedIndex = i;
                        // Disable changing invoice if already allocated
                        cboInvoice.Enabled = false;
                        break;
                    }
                }

                // Add a warning label to explain why invoice field is disabled
                if (!cboInvoice.Enabled)
                {
                    var label = new Label
                    {
                        Text = "This payment is already allocated",
                        ForeColor = Color.Red,
                        Font = new Font(this.Font, FontStyle.Italic),
                        Location = new Point(cboInvoice.Left, cboInvoice.Bottom + 2),
                        AutoSize = true
                    };
                    this.Controls.Add(label);
                }
            }

            // Select payment method
            for (int i = 0; i < cboPaymentMethod.Items.Count; i++)
            {
                var item = cboPaymentMethod.Items[i] as ComboboxItem;
                if (item != null && item.Value.ToString() == _payment.PaymentMethod)
                {
                    cboPaymentMethod.SelectedIndex = i;
                    break;
                }
            }

            // Set remaining fields
            txtReferenceNumber.Text = _payment.ReferenceNumber ?? string.Empty;
            txtAmount.Text = _payment.Amount.ToString("0.00");
            txtNotes.Text = _payment.Notes ?? string.Empty;
        }

        private void GeneratePaymentNumber()
        {
            if (_isEditMode) return;

            txtPaymentNumber.Text = $"PMT-{DateTime.Now:yyyyMMdd}-{new Random().Next(1, 999):D3}";
        }

        private void LoadCustomers()
        {
            try
            {
                string query = "SELECT CustomerID, CustomerName FROM Customer WHERE IsActive = 1 ORDER BY CustomerName";
                DataTable customersTable = ExecuteQuery(query);

                cboCustomer.Items.Clear();
                cboCustomer.Items.Add(new ComboboxItem { Text = "-- Select Customer --", Value = 0 });

                foreach (DataRow row in customersTable.Rows)
                {
                    cboCustomer.Items.Add(new ComboboxItem
                    {
                        Text = row["CustomerName"].ToString(),
                        Value = Convert.ToInt32(row["CustomerID"])
                    });
                }

                cboCustomer.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customers: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadInvoices()
        {
            cboInvoice.Items.Clear();
            cboInvoice.Items.Add(new ComboboxItem { Text = "-- No Invoice (Unallocated) --", Value = 0 });
            cboInvoice.SelectedIndex = 0;
        }

        private void LoadPaymentMethods()
        {
            cboPaymentMethod.Items.Clear();
            cboPaymentMethod.Items.Add(new ComboboxItem { Text = "-- Select Payment Method --", Value = string.Empty });
            cboPaymentMethod.Items.Add(new ComboboxItem { Text = "Cash", Value = "Cash" });
            cboPaymentMethod.Items.Add(new ComboboxItem { Text = "Check", Value = "Check" });
            cboPaymentMethod.Items.Add(new ComboboxItem { Text = "Credit Card", Value = "Credit Card" });
            cboPaymentMethod.Items.Add(new ComboboxItem { Text = "Bank Transfer", Value = "Bank Transfer" });
            cboPaymentMethod.SelectedIndex = 0;
        }

        private void LoadInvoicesForCustomer(int customerId)
        {
            try
            {
                string query = @"
                SELECT InvoiceID, InvoiceNumber, Amount, PaidAmount 
                FROM Invoices 
                WHERE CustomerID = @CustomerID AND Status <> 'Paid'
                ORDER BY InvoiceDate DESC";

                SqlParameter[] parameters = { new SqlParameter("@CustomerID", customerId) };
                DataTable invoicesTable = ExecuteQuery(query, parameters);

                cboInvoice.Items.Clear();
                cboInvoice.Items.Add(new ComboboxItem { Text = "-- No Invoice (Unallocated) --", Value = 0 });

                foreach (DataRow row in invoicesTable.Rows)
                {
                    decimal amount = Convert.ToDecimal(row["Amount"]);
                    decimal paidAmount = Convert.ToDecimal(row["PaidAmount"]);
                    decimal balance = amount - paidAmount;

                    cboInvoice.Items.Add(new ComboboxItem
                    {
                        Text = $"{row["InvoiceNumber"]} (Balance: {balance:C2})",
                        Value = Convert.ToInt32(row["InvoiceID"])
                    });
                }

                cboInvoice.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading invoices: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CboCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCustomer.SelectedIndex > 0)
            {
                var selectedCustomer = (ComboboxItem)cboCustomer.SelectedItem;
                LoadInvoicesForCustomer(Convert.ToInt32(selectedCustomer.Value));
            }
            else
            {
                LoadInvoices();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                // Get form values
                string paymentNumber = txtPaymentNumber.Text.Trim();
                DateTime paymentDate = dtpPaymentDate.Value;
                int customerId = Convert.ToInt32(((ComboboxItem)cboCustomer.SelectedItem).Value);
                object invoiceId;
                if (cboInvoice.SelectedIndex > 0)
                {
                    invoiceId = Convert.ToInt32(((ComboboxItem)cboInvoice.SelectedItem).Value);
                }
                else
                {
                    invoiceId = DBNull.Value;
                }
                string paymentMethod = ((ComboboxItem)cboPaymentMethod.SelectedItem).Value.ToString();
                string referenceNumber = txtReferenceNumber.Text.Trim();
                decimal amount = decimal.Parse(txtAmount.Text);
                string notes = txtNotes.Text.Trim();

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    // Start a transaction to ensure data consistency
                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            if (_isEditMode)
                            {
                                // UPDATE EXISTING PAYMENT

                                // Check if invoice allocation changed
                                bool invoiceChanged = false;
                                if (_payment.InvoiceID.HasValue)
                                {
                                    // If we already had an invoice and it's different now
                                    invoiceChanged = (invoiceId == DBNull.Value ||
                                        Convert.ToInt32(invoiceId) != _payment.InvoiceID.Value);
                                }
                                else
                                {
                                    // If we didn't have an invoice before but do now
                                    invoiceChanged = (invoiceId != DBNull.Value);
                                }

                                // Check if amount changed
                                bool amountChanged = (_payment.Amount != amount);

                                // If invoice or amount changed, we need to update invoice payment records
                                if (invoiceChanged || amountChanged)
                                {
                                    // If payment was previously allocated to an invoice, update that invoice
                                    if (_payment.InvoiceID.HasValue)
                                    {
                                        // Reduce the paid amount on the old invoice
                                        string reduceOldInvoiceQuery = @"
                                            UPDATE Invoices 
                                            SET PaidAmount = PaidAmount - @Amount,
                                                Status = CASE 
                                                    WHEN PaidAmount - @Amount <= 0 THEN 'Open'
                                                    WHEN PaidAmount - @Amount < Amount THEN 'Partial'
                                                    ELSE Status 
                                                END
                                            WHERE InvoiceID = @InvoiceID";

                                        using (SqlCommand cmd = new SqlCommand(reduceOldInvoiceQuery, connection, transaction))
                                        {
                                            cmd.Parameters.AddWithValue("@Amount", _payment.Amount);
                                            cmd.Parameters.AddWithValue("@InvoiceID", _payment.InvoiceID.Value);
                                            cmd.ExecuteNonQuery();
                                        }
                                    }

                                    // If payment is now allocated to an invoice, update that invoice
                                    if (invoiceId != DBNull.Value && Convert.ToInt32(invoiceId) > 0)
                                    {
                                        string updateNewInvoiceQuery = @"
                                            UPDATE Invoices
                                            SET PaidAmount = PaidAmount + @Amount,
                                                Status = CASE 
                                                    WHEN PaidAmount + @Amount >= Amount THEN 'Paid' 
                                                    ELSE 'Partial' 
                                                END
                                            WHERE InvoiceID = @InvoiceID";

                                        using (SqlCommand cmd = new SqlCommand(updateNewInvoiceQuery, connection, transaction))
                                        {
                                            cmd.Parameters.AddWithValue("@Amount", amount);
                                            cmd.Parameters.AddWithValue("@InvoiceID", invoiceId);
                                            cmd.ExecuteNonQuery();
                                        }
                                    }
                                }

                                // Update the payment record
                                string updateQuery = @"
                                    UPDATE Payments SET 
                                        PaymentDate = @PaymentDate,
                                        CustomerID = @CustomerID,
                                        InvoiceID = @InvoiceID,
                                        PaymentMethod = @PaymentMethod,
                                        ReferenceNumber = @ReferenceNumber,
                                        Amount = @Amount,
                                        Notes = @Notes
                                    WHERE PaymentID = @PaymentID";

                                using (SqlCommand command = new SqlCommand(updateQuery, connection, transaction))
                                {
                                    command.Parameters.AddWithValue("@PaymentDate", paymentDate);
                                    command.Parameters.AddWithValue("@CustomerID", customerId);
                                    command.Parameters.AddWithValue("@InvoiceID", invoiceId);
                                    command.Parameters.AddWithValue("@PaymentMethod", paymentMethod);
                                    command.Parameters.AddWithValue("@ReferenceNumber",
                                        string.IsNullOrEmpty(referenceNumber) ? DBNull.Value : (object)referenceNumber);
                                    command.Parameters.AddWithValue("@Amount", amount);
                                    command.Parameters.AddWithValue("@Notes",
                                        string.IsNullOrEmpty(notes) ? DBNull.Value : (object)notes);
                                    command.Parameters.AddWithValue("@PaymentID", _payment.PaymentID);
                                    command.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                // INSERT NEW PAYMENT - existing code
                                string query = @"
                                INSERT INTO Payments (
                                    PaymentNumber,
                                    PaymentDate,
                                    CustomerID,
                                    InvoiceID,
                                    PaymentMethod,
                                    ReferenceNumber,
                                    Amount,
                                    Notes,
                                    CreatedBy,
                                    CreatedDate
                                ) VALUES (
                                    @PaymentNumber,
                                    @PaymentDate,
                                    @CustomerID,
                                    @InvoiceID,
                                    @PaymentMethod,
                                    @ReferenceNumber,
                                    @Amount,
                                    @Notes,
                                    @CreatedBy,
                                    @CreatedDate
                                )";

                                SqlParameter[] parameters = {
                                    new SqlParameter("@PaymentNumber", paymentNumber),
                                    new SqlParameter("@PaymentDate", paymentDate),
                                    new SqlParameter("@CustomerID", customerId),
                                    new SqlParameter("@InvoiceID", invoiceId),
                                    new SqlParameter("@PaymentMethod", paymentMethod),
                                    new SqlParameter("@ReferenceNumber", string.IsNullOrEmpty(referenceNumber) ? DBNull.Value : (object)referenceNumber),
                                    new SqlParameter("@Amount", amount),
                                    new SqlParameter("@Notes", string.IsNullOrEmpty(notes) ? DBNull.Value : (object)notes),
                                    new SqlParameter("@CreatedBy", 1), // Use the current user ID in a real app
                                    new SqlParameter("@CreatedDate", DateTime.Now)
                                };

                                using (SqlCommand command = new SqlCommand(query, connection, transaction))
                                {
                                    command.Parameters.AddRange(parameters);
                                    command.ExecuteNonQuery();
                                }

                                // If payment is allocated to an invoice, update the invoice paid amount
                                if (invoiceId != DBNull.Value && Convert.ToInt32(invoiceId) > 0)
                                {
                                    string updateInvoiceQuery = @"
                                    UPDATE Invoices
                                    SET PaidAmount = PaidAmount + @Amount,
                                        Status = CASE 
                                                  WHEN PaidAmount + @Amount >= Amount THEN 'Paid' 
                                                  ELSE 'Partial' 
                                                END
                                    WHERE InvoiceID = @InvoiceID";

                                    using (SqlCommand command = new SqlCommand(updateInvoiceQuery, connection, transaction))
                                    {
                                        command.Parameters.AddWithValue("@Amount", amount);
                                        command.Parameters.AddWithValue("@InvoiceID", invoiceId);
                                        command.ExecuteNonQuery();
                                    }
                                }
                            }

                            // Commit the transaction
                            transaction.Commit();
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }

                MessageBox.Show(_isEditMode ? "Payment updated successfully!" : "Payment saved successfully!",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error {(_isEditMode ? "updating" : "saving")} payment: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtPaymentNumber.Text))
            {
                MessageBox.Show("Please enter a payment number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPaymentNumber.Focus();
                return false;
            }

            if (cboCustomer.SelectedIndex <= 0)
            {
                MessageBox.Show("Please select a customer.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboCustomer.Focus();
                return false;
            }

            if (cboPaymentMethod.SelectedIndex <= 0)
            {
                MessageBox.Show("Please select a payment method.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboPaymentMethod.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtAmount.Text) || !decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Please enter a valid amount greater than zero.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAmount.Focus();
                return false;
            }

            return true;
        }

        private DataTable ExecuteQuery(string query, params SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }

        // ComboboxItem class for storing text/value pairs in comboboxes
        public class ComboboxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }
    }
}
