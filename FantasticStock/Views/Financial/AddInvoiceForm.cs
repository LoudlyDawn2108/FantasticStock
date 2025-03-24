using FantasticStock.Models.Sales;
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
using FantasticStock.Models.Financial;

namespace FantasticStock.Views.Financial
{
    public partial class AddInvoiceForm : Form
    {
        private const string ConnectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=FantasticStock;Integrated Security=True;TrustServerCertificate=True";
        private Models.Financial.Invoice _invoice;
        private bool _isEditMode;

        // Form controls
        private TextBox txtInvoiceNumber;
        private DateTimePicker dtpInvoiceDate;
        private DateTimePicker dtpDueDate;
        private ComboBox cboCustomer;
        private TextBox txtAmount;
        private ComboBox cboStatus;
        private TextBox txtDescription;
        private TextBox txtNotes;
        private Button btnSave;
        private Button btnCancel;

        public AddInvoiceForm()
        {
            _isEditMode = false;
            InitializeComponent();
            LoadCustomers();
            GenerateInvoiceNumber();
        }

        public AddInvoiceForm(Models.Financial.Invoice invoice)
        {
            _invoice = invoice;
            _isEditMode = true;
            InitializeComponent();
            LoadCustomers();
            PopulateFormWithInvoice();
        }

        private void LoadCustomers()
        {
            try
            {
                string query = "SELECT CustomerID, CustomerName FROM Customers WHERE IsActive = 1 ORDER BY CustomerName";
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

        private void GenerateInvoiceNumber()
        {
            // Only generate for new invoices, not when editing
            if (!_isEditMode)
            {
                txtInvoiceNumber.Text = $"INV-{DateTime.Now:yyyyMMdd}-{new Random().Next(1, 999):D3}";
            }
        }

        private void PopulateFormWithInvoice()
        {
            if (_invoice == null) return;

            txtInvoiceNumber.Text = _invoice.InvoiceNumber;
            dtpInvoiceDate.Value = _invoice.InvoiceDate;
            dtpDueDate.Value = _invoice.DueDate;

            // Select customer in combobox
            for (int i = 0; i < cboCustomer.Items.Count; i++)
            {
                var item = cboCustomer.Items[i] as ComboboxItem;
                if (item != null && Convert.ToInt32(item.Value) == _invoice.CustomerID)
                {
                    cboCustomer.SelectedIndex = i;
                    break;
                }
            }

            txtAmount.Text = _invoice.Amount.ToString("0.00");

            // Select status in combobox
            for (int i = 0; i < cboStatus.Items.Count; i++)
            {
                if (cboStatus.Items[i].ToString() == _invoice.Status)
                {
                    cboStatus.SelectedIndex = i;
                    break;
                }
            }

            txtDescription.Text = _invoice.Description ?? "";
            txtNotes.Text = _invoice.Notes ?? "";

            // If invoice has payments, disable some fields
            if (_invoice.PaidAmount > 0)
            {
                txtAmount.Enabled = false;
                var lblPaid = new Label
                {
                    Text = $"This invoice has payments applied: {_invoice.PaidAmount:C} of {_invoice.Amount:C}",
                    ForeColor = Color.Red,
                    Left = 140,
                    Top = txtAmount.Bottom + 3,
                    AutoSize = true
                };
                this.Controls.Add(lblPaid);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                // Get form values
                string invoiceNumber = txtInvoiceNumber.Text.Trim();
                DateTime invoiceDate = dtpInvoiceDate.Value;
                DateTime dueDate = dtpDueDate.Value;
                int customerId = Convert.ToInt32(((ComboboxItem)cboCustomer.SelectedItem).Value);
                decimal amount = decimal.Parse(txtAmount.Text);
                string status = cboStatus.SelectedItem.ToString();
                string description = txtDescription.Text.Trim();
                string notes = txtNotes.Text.Trim();

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    // Use transaction for consistency
                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            if (_isEditMode)
                            {
                                // Update existing invoice
                                string updateQuery = @"
                                UPDATE Invoices SET 
                                    InvoiceNumber = @InvoiceNumber,
                                    InvoiceDate = @InvoiceDate,
                                    DueDate = @DueDate,
                                    CustomerID = @CustomerID,
                                    Amount = @Amount,
                                    Status = @Status,
                                    Description = @Description,
                                    Notes = @Notes
                                WHERE InvoiceID = @InvoiceID";

                                using (SqlCommand command = new SqlCommand(updateQuery, connection, transaction))
                                {
                                    command.Parameters.AddWithValue("@InvoiceNumber", invoiceNumber);
                                    command.Parameters.AddWithValue("@InvoiceDate", invoiceDate);
                                    command.Parameters.AddWithValue("@DueDate", dueDate);
                                    command.Parameters.AddWithValue("@CustomerID", customerId);
                                    command.Parameters.AddWithValue("@Amount", amount);
                                    command.Parameters.AddWithValue("@Status", status);
                                    command.Parameters.AddWithValue("@Description", string.IsNullOrEmpty(description) ? DBNull.Value : (object)description);
                                    command.Parameters.AddWithValue("@Notes", string.IsNullOrEmpty(notes) ? DBNull.Value : (object)notes);
                                    command.Parameters.AddWithValue("@InvoiceID", _invoice.InvoiceID);

                                    command.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                // Insert new invoice
                                string insertQuery = @"
                                INSERT INTO Invoices (
                                    InvoiceNumber,
                                    InvoiceDate,
                                    DueDate,
                                    CustomerID,
                                    Amount,
                                    PaidAmount,
                                    Status,
                                    Description,
                                    Notes,
                                    CreatedBy,
                                    CreatedDate
                                ) VALUES (
                                    @InvoiceNumber,
                                    @InvoiceDate,
                                    @DueDate,
                                    @CustomerID,
                                    @Amount,
                                    0,
                                    @Status,
                                    @Description,
                                    @Notes,
                                    @CreatedBy,
                                    @CreatedDate
                                )";

                                using (SqlCommand command = new SqlCommand(insertQuery, connection, transaction))
                                {
                                    command.Parameters.AddWithValue("@InvoiceNumber", invoiceNumber);
                                    command.Parameters.AddWithValue("@InvoiceDate", invoiceDate);
                                    command.Parameters.AddWithValue("@DueDate", dueDate);
                                    command.Parameters.AddWithValue("@CustomerID", customerId);
                                    command.Parameters.AddWithValue("@Amount", amount);
                                    command.Parameters.AddWithValue("@Status", status);
                                    command.Parameters.AddWithValue("@Description", string.IsNullOrEmpty(description) ? DBNull.Value : (object)description);
                                    command.Parameters.AddWithValue("@Notes", string.IsNullOrEmpty(notes) ? DBNull.Value : (object)notes);
                                    command.Parameters.AddWithValue("@CreatedBy", 1); // Use the current user ID in a real app
                                    command.Parameters.AddWithValue("@CreatedDate", DateTime.Now);

                                    command.ExecuteNonQuery();
                                }
                            }

                            transaction.Commit();
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }

                MessageBox.Show(_isEditMode ? "Invoice updated successfully!" : "Invoice saved successfully!",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving invoice: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtInvoiceNumber.Text))
            {
                MessageBox.Show("Please enter an invoice number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtInvoiceNumber.Focus();
                return false;
            }

            if (cboCustomer.SelectedIndex <= 0)
            {
                MessageBox.Show("Please select a customer.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboCustomer.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtAmount.Text) || !decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Please enter a valid amount greater than zero.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAmount.Focus();
                return false;
            }

            if (dtpDueDate.Value < dtpInvoiceDate.Value)
            {
                MessageBox.Show("Due date cannot be earlier than invoice date.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpDueDate.Focus();
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
