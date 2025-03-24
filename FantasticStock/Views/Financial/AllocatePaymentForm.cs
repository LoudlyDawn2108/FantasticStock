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
    public partial class AllocatePaymentForm : Form
    {
        private const string ConnectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=FantasticStock;Integrated Security=True;TrustServerCertificate=True";
        private Payment _payment;
        private decimal _availableAmount;
        private List<InvoiceAllocation> _invoices;
        private int _selectedInvoiceId;

        public AllocatePaymentForm(Payment payment)
        {
            _payment = payment;
            _availableAmount = payment.Amount;
            _selectedInvoiceId = 0;
            _invoices = new List<InvoiceAllocation>();

            InitializeComponent();
            LoadInvoices();

            this.Text = "Allocate Payment to Invoice";
            lblPaymentInfo.Text = $"Payment: {_payment.PaymentNumber} | Date: {_payment.PaymentDate:yyyy-MM-dd} | " +
                                 $"Customer: {_payment.CustomerName} | Amount: {_payment.Amount:C2}";
            lblAvailableAmount.Text = $"Available Amount: {_availableAmount:C2}";


            dgvInvoices.CellValueChanged += DgvInvoices_CellValueChanged;
            dgvInvoices.CellClick += DgvInvoices_CellClick;
            dgvInvoices.CurrentCellDirtyStateChanged += DgvInvoices_CurrentCellDirtyStateChanged;
            btnAllocate.Click += BtnAllocate_Click;
        }

        private void ConfigureDataGridView()
        {
            dgvInvoices.AutoGenerateColumns = false;
            dgvInvoices.Columns.Clear();

            // Add columns to the grid
            dgvInvoices.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "Select",
                HeaderText = "",
                Width = 40,
                ReadOnly = false
            });

            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "InvoiceNumber",
                DataPropertyName = "InvoiceNumber",
                HeaderText = "Invoice #",
                ReadOnly = true,
                Width = 100
            });

            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "InvoiceDate",
                DataPropertyName = "InvoiceDate",
                HeaderText = "Date",
                ReadOnly = true,
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "yyyy-MM-dd" }
            });

            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DueDate",
                DataPropertyName = "DueDate",
                HeaderText = "Due Date",
                ReadOnly = true,
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "yyyy-MM-dd" }
            });

            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Amount",
                DataPropertyName = "Amount",
                HeaderText = "Total",
                ReadOnly = true,
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Balance",
                DataPropertyName = "Balance",
                HeaderText = "Balance",
                ReadOnly = true,
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "AllocateAmount",
                DataPropertyName = "AllocateAmount",
                HeaderText = "Allocate",
                ReadOnly = false,
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });
        }

        private void LoadInvoices()
        {
            try
            {
                string query = @"
                SELECT 
                    i.InvoiceID,
                    i.InvoiceNumber,
                    i.InvoiceDate,
                    i.DueDate,
                    i.Amount,
                    i.PaidAmount,
                    i.Amount - i.PaidAmount AS Balance
                FROM 
                    Invoices i
                WHERE 
                    i.CustomerID = @CustomerID 
                    AND i.Status <> 'Paid'
                    AND i.Amount > i.PaidAmount
                ORDER BY 
                    i.DueDate ASC";

                SqlParameter[] parameters = { new SqlParameter("@CustomerID", _payment.CustomerID) };
                DataTable invoicesTable = ExecuteQuery(query, parameters);

                _invoices.Clear();
                foreach (DataRow row in invoicesTable.Rows)
                {
                    _invoices.Add(new InvoiceAllocation
                    {
                        InvoiceID = Convert.ToInt32(row["InvoiceID"]),
                        InvoiceNumber = row["InvoiceNumber"].ToString(),
                        InvoiceDate = Convert.ToDateTime(row["InvoiceDate"]),
                        DueDate = Convert.ToDateTime(row["DueDate"]),
                        Amount = Convert.ToDecimal(row["Amount"]),
                        PaidAmount = Convert.ToDecimal(row["PaidAmount"]),
                        Balance = Convert.ToDecimal(row["Balance"]),
                        AllocateAmount = 0m,
                        IsSelected = false
                    });
                }

                // Bind invoices to grid
                dgvInvoices.DataSource = new BindingList<InvoiceAllocation>(_invoices);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading invoices: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvInvoices_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            // This is needed to commit checkbox changes immediately
            if (dgvInvoices.IsCurrentCellDirty && dgvInvoices.CurrentCell.ColumnIndex == 0)
            {
                dgvInvoices.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void DgvInvoices_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Handle checkbox column clicks
            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dgvInvoices.Rows[e.RowIndex].Cells[0];
                bool isChecked = Convert.ToBoolean(cell.Value);

                // Update the model
                _invoices[e.RowIndex].IsSelected = isChecked;

                // If row is selected, set allocation amount
                if (isChecked)
                {
                    _selectedInvoiceId = _invoices[e.RowIndex].InvoiceID;

                    // Auto-fill with either the balance or the available amount, whichever is less
                    decimal balance = _invoices[e.RowIndex].Balance;
                    decimal autoAmount = Math.Min(balance, _availableAmount);
                    _invoices[e.RowIndex].AllocateAmount = autoAmount;

                    // Update the cell
                    dgvInvoices.Rows[e.RowIndex].Cells["AllocateAmount"].Value = autoAmount;
                }
                else
                {
                    // If deselected, clear allocation
                    _invoices[e.RowIndex].AllocateAmount = 0;
                    dgvInvoices.Rows[e.RowIndex].Cells["AllocateAmount"].Value = 0;

                    if (_selectedInvoiceId == _invoices[e.RowIndex].InvoiceID)
                    {
                        _selectedInvoiceId = 0;
                    }
                }

                // Recalculate available amount
                UpdateAvailableAmount();

                // Enable/disable allocate button
                UpdateAllocateButton();
            }
        }

        private void DgvInvoices_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Handle allocation amount changes
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvInvoices.Columns["AllocateAmount"].Index)
            {
                var cell = dgvInvoices.Rows[e.RowIndex].Cells[e.ColumnIndex];

                if (cell.Value != null && decimal.TryParse(cell.Value.ToString(), out decimal amount))
                {
                    // Get the invoice
                    InvoiceAllocation invoice = _invoices[e.RowIndex];

                    // Validate amount
                    if (amount < 0)
                    {
                        amount = 0;
                        cell.Value = amount;
                    }
                    else if (amount > invoice.Balance)
                    {
                        amount = invoice.Balance;
                        cell.Value = amount;
                    }

                    // Update the model
                    invoice.AllocateAmount = amount;

                    // If amount is > 0, make sure the invoice is selected
                    if (amount > 0 && !invoice.IsSelected)
                    {
                        invoice.IsSelected = true;
                        dgvInvoices.Rows[e.RowIndex].Cells[0].Value = true;
                        _selectedInvoiceId = invoice.InvoiceID;
                    }
                    // If amount is 0, deselect the invoice
                    else if (amount == 0 && invoice.IsSelected)
                    {
                        invoice.IsSelected = false;
                        dgvInvoices.Rows[e.RowIndex].Cells[0].Value = false;

                        if (_selectedInvoiceId == invoice.InvoiceID)
                        {
                            _selectedInvoiceId = 0;
                        }
                    }

                    // Recalculate available amount
                    UpdateAvailableAmount();
                }
            }

            // Update allocate button state
            UpdateAllocateButton();
        }

        private void UpdateAvailableAmount()
        {
            // Calculate total allocated
            decimal allocated = _invoices.Sum(i => i.AllocateAmount);

            // Update available amount
            _availableAmount = _payment.Amount - allocated;
            lblAvailableAmount.Text = $"Available Amount: {_availableAmount:C2}";

            // Set text color based on amount
            if (_availableAmount < 0)
            {
                lblAvailableAmount.ForeColor = Color.Red;
            }
            else
            {
                lblAvailableAmount.ForeColor = SystemColors.ControlText;
            }
        }

        private void UpdateAllocateButton()
        {
            // Only enable if at least one invoice is selected and the allocated amount is valid
            decimal allocated = _invoices.Sum(i => i.AllocateAmount);
            btnAllocate.Enabled = _invoices.Any(i => i.IsSelected) && allocated > 0 && allocated <= _payment.Amount;
        }

        private void BtnAllocate_Click(object sender, EventArgs e)
        {
            // Only proceed if at least one invoice is selected
            if (!_invoices.Any(i => i.IsSelected))
            {
                MessageBox.Show("Please select at least one invoice to allocate this payment to.",
                    "No Invoice Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Get selected invoices and their allocation amounts
            var selectedInvoices = _invoices.Where(i => i.IsSelected && i.AllocateAmount > 0).ToList();
            decimal totalAllocated = selectedInvoices.Sum(i => i.AllocateAmount);

            // Validate total
            if (totalAllocated <= 0)
            {
                MessageBox.Show("Please enter an allocation amount greater than zero.",
                    "Invalid Amount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (totalAllocated > _payment.Amount)
            {
                MessageBox.Show($"The total allocated amount ({totalAllocated:C2}) cannot exceed the payment amount ({_payment.Amount:C2}).",
                    "Invalid Amount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Confirm allocation
            var result = MessageBox.Show(
                $"Are you sure you want to allocate {totalAllocated:C2} to {selectedInvoices.Count} invoice(s)?",
                "Confirm Allocation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        connection.Open();
                        using (SqlTransaction transaction = connection.BeginTransaction())
                        {
                            try
                            {
                                // First, update the payment with the first invoice (if multiple, we'll split the payment)
                                var firstInvoice = selectedInvoices.First();

                                // Update the payment record
                                string updatePaymentQuery = "UPDATE Payments SET InvoiceID = @InvoiceID WHERE PaymentID = @PaymentID";

                                using (SqlCommand cmd = new SqlCommand(updatePaymentQuery, connection, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@InvoiceID", firstInvoice.InvoiceID);
                                    cmd.Parameters.AddWithValue("@PaymentID", _payment.PaymentID);
                                    cmd.ExecuteNonQuery();
                                }

                                // Update the first invoice's paid amount
                                string updateInvoiceQuery = @"
                            UPDATE Invoices
                            SET PaidAmount = PaidAmount + @Amount,
                                Status = CASE 
                                          WHEN PaidAmount + @Amount >= Amount THEN 'Paid' 
                                          ELSE 'Partial' 
                                        END
                            WHERE InvoiceID = @InvoiceID";

                                using (SqlCommand cmd = new SqlCommand(updateInvoiceQuery, connection, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@Amount", firstInvoice.AllocateAmount);
                                    cmd.Parameters.AddWithValue("@InvoiceID", firstInvoice.InvoiceID);
                                    cmd.ExecuteNonQuery();
                                }

                                // For each additional invoice, create a new payment record (splitting the original payment)
                                for (int i = 1; i < selectedInvoices.Count; i++)
                                {
                                    var invoice = selectedInvoices[i];

                                    // Insert a new payment record
                                    string insertPaymentQuery = @"
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

                                    using (SqlCommand cmd = new SqlCommand(insertPaymentQuery, connection, transaction))
                                    {
                                        cmd.Parameters.AddWithValue("@PaymentNumber", $"{_payment.PaymentNumber}-{i + 1}");
                                        cmd.Parameters.AddWithValue("@PaymentDate", _payment.PaymentDate);
                                        cmd.Parameters.AddWithValue("@CustomerID", _payment.CustomerID);
                                        cmd.Parameters.AddWithValue("@InvoiceID", invoice.InvoiceID);
                                        cmd.Parameters.AddWithValue("@PaymentMethod", _payment.PaymentMethod);
                                        cmd.Parameters.AddWithValue("@ReferenceNumber",
                                            string.IsNullOrEmpty(_payment.ReferenceNumber) ? DBNull.Value : (object)_payment.ReferenceNumber);
                                        cmd.Parameters.AddWithValue("@Amount", invoice.AllocateAmount);
                                        cmd.Parameters.AddWithValue("@Notes",
                                            string.IsNullOrEmpty(_payment.Notes) ? $"Split from payment {_payment.PaymentNumber}" :
                                            $"{_payment.Notes} | Split from payment {_payment.PaymentNumber}");
                                        cmd.Parameters.AddWithValue("@CreatedBy", 1); // Use current user ID in a real app
                                        cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                                        cmd.ExecuteNonQuery();
                                    }

                                    // Update this invoice's paid amount
                                    using (SqlCommand cmd = new SqlCommand(updateInvoiceQuery, connection, transaction))
                                    {
                                        cmd.Parameters.AddWithValue("@Amount", invoice.AllocateAmount);
                                        cmd.Parameters.AddWithValue("@InvoiceID", invoice.InvoiceID);
                                        cmd.ExecuteNonQuery();
                                    }
                                }

                                // Update the original payment amount to reflect the first allocation if we've split the payment
                                if (selectedInvoices.Count > 1)
                                {
                                    string updateAmountQuery = "UPDATE Payments SET Amount = @Amount WHERE PaymentID = @PaymentID";

                                    using (SqlCommand cmd = new SqlCommand(updateAmountQuery, connection, transaction))
                                    {
                                        cmd.Parameters.AddWithValue("@Amount", firstInvoice.AllocateAmount);
                                        cmd.Parameters.AddWithValue("@PaymentID", _payment.PaymentID);
                                        cmd.ExecuteNonQuery();
                                    }
                                }

                                // Commit the transaction
                                transaction.Commit();

                                MessageBox.Show("Payment allocated successfully!", "Success",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // Set dialog result and close
                                this.DialogResult = DialogResult.OK;
                                this.Close();
                            }
                            catch (Exception ex)
                            {
                                // Roll back the transaction on error
                                transaction.Rollback();
                                throw new Exception($"Error during payment allocation: {ex.Message}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error allocating payment: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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

        // Class to store invoice allocation data
        public class InvoiceAllocation
        {
            public int InvoiceID { get; set; }
            public string InvoiceNumber { get; set; }
            public DateTime InvoiceDate { get; set; }
            public DateTime DueDate { get; set; }
            public decimal Amount { get; set; }
            public decimal PaidAmount { get; set; }
            public decimal Balance { get; set; }
            public decimal AllocateAmount { get; set; }
            public bool IsSelected { get; set; }
        }
    }
}
