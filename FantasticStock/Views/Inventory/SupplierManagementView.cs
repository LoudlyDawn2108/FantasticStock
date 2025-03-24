using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
namespace FantasticStock.Views.Inventory
{
    partial class SupplierManagementView
    {
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);
        private const int EM_SETCUEBANNER = 0x1501;
        string chuoiketnoi = "Server=localhost\\SQLEXPRESS; Database=FantasticStock1; Integrated Security=true";
        SqlConnection conn;

        private System.ComponentModel.IContainer components = null;


        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void SupplierManagementView_Load(object sender, EventArgs e)
        {
            dgvSuppliers.AutoGenerateColumns = false;
            dgvProductsSupplied.AutoGenerateColumns = false;
            KhoaChinhSua(false);
            Load_dgvSuppiler();
            SetCueText(txtSearch, "Search suppliers...");
        }
        private void SetCueText(TextBox textBox, string cueText)
        {
            SendMessage(textBox.Handle, EM_SETCUEBANNER, 1, cueText);
        }
        private void Load_dgvSuppiler()
        {
            using (conn = new SqlConnection(chuoiketnoi))
            {
                conn.Open();
                string query = "Select * from Supplier";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                dgvSuppliers.DataSource = dt;
                dgvSuppliers.Columns["SupplierID"].DataPropertyName = "SupplierID";
                dgvSuppliers.Columns["CompanyName"].DataPropertyName = "CompanyName";
                dgvSuppliers.Columns["ContactName"].DataPropertyName = "ContactName";
                dgvSuppliers.Columns["Phone"].DataPropertyName = "Phone";

            }
        }
        private void Load_dgvProductsSupplied()
        {
            using (conn = new SqlConnection(chuoiketnoi))
            {
                conn.Open();
                string query = "Select * from Product where SupplierID = @supplierID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@supplierID", supplierID);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                dgvProductsSupplied.DataSource = dt;
                dgvProductsSupplied.Columns["ProductID"].DataPropertyName = "ProductID";
                dgvProductsSupplied.Columns["ProductName"].DataPropertyName = "ProductName";
                dgvProductsSupplied.Columns["LastOrderDate"].DataPropertyName = "CreatedDate";
            }
        }





        private void btnAddNewSupplier_Click(object sender, EventArgs e)
        {
            supplierID = 0;
            XoaDuLieu();
            KhoaChinhSua(true);

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            KhoaChinhSua(true);
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            using(conn = new SqlConnection(chuoiketnoi))
            {
                conn.Open();
                string query = "Delete from Supplier where SupplierID = @SupplierID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SupplierID", supplierID);
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Supplier information deleted successfully.", "Delete Successful",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    XoaDuLieu();
                    Load_dgvSuppiler();
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Please delete the product related to the supplier before deleting.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                

            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (KiemTraGiaTri())
            {
                SaveData();
                MessageBox.Show("Supplier information saved successfully.", "Save Successful",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                Load_dgvSuppiler();
                KhoaChinhSua(false);
            }

        }
        private void SaveData()
        {
            using(conn = new SqlConnection(chuoiketnoi))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                if (supplierID > 0)
                {
                    string query = "UPDATE Supplier SET CompanyName = @CompanyName, ContactName = @ContactName, Email = @Email, Phone = @Phone, Website = @Website, PaymentTerms = @PaymentTerms, Address = @Address, City = @City, State = @State, PostalCode = @PostalCode, Country = @Country, Notes = @Notes, ModifiedDate = @ModifiedDate WHERE SupplierID = @SupplierID";
                    cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@SupplierID", supplierID);
                }
                else
                {
                    string query = "Insert into Supplier (CompanyName, ContactName, Address, City, State, PostalCode, Country, Phone, Email, Website, PaymentTerms, Notes ,CreatedDate, ModifiedDate) values (@CompanyName, @ContactName, @Address, @City, @State, @PostalCode, @Country, @Phone, @Email, @Website, @PaymentTerms, @Notes ,@CreatedDate, @ModifiedDate)";
                    cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                }
                
                cmd.Parameters.AddWithValue("@CompanyName", txtCompanyName.Text);
                cmd.Parameters.AddWithValue("@ContactName", txtContactPerson.Text);
                cmd.Parameters.AddWithValue("@Address", txtStreetAddress.Text);
                cmd.Parameters.AddWithValue("@City", txtCity.Text);
                cmd.Parameters.AddWithValue("@State", txtStateProvince.Text);
                cmd.Parameters.AddWithValue("@PostalCode", txtPostalCode.Text);
                cmd.Parameters.AddWithValue("@Country", txtCountry.Text);
                cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@Website", txtWebsite.Text);
                cmd.Parameters.AddWithValue("@PaymentTerms", cmbPaymentTerms.Text);
                cmd.Parameters.AddWithValue("@Notes", txtNotes.Text);
                cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                cmd.ExecuteNonQuery();
                supplierID = 0;
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            XoaDuLieu();
            KhoaChinhSua(false);

        }
        private bool KiemTraGiaTri()
        {
           
            if (string.IsNullOrWhiteSpace(txtCompanyName.Text))
            {
                MessageBox.Show("Company name cannot be empty.", "Input Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCompanyName.Focus();
                return false;
            }

      
            if (string.IsNullOrWhiteSpace(txtContactPerson.Text))
            {
                MessageBox.Show("Contact person cannot be empty.", "Input Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtContactPerson.Focus();
                return false;
            }

   
            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                try
                {
                    var mailAddress = new System.Net.Mail.MailAddress(txtEmail.Text);
                }
                catch
                {
                    MessageBox.Show("Invalid email format.", "Input Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtEmail.Focus();
                    return false;
                }
            }

       
            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Phone number cannot be empty.", "Input Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPhone.Focus();
                return false;
            }

    
            string phonePattern = @"^[0-9\+\-\(\)\s]*$";
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtPhone.Text.Trim(), phonePattern))
            {
                MessageBox.Show("Phone number contains invalid characters.", "Input Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPhone.Focus();
                return false;
            }

    
            if (!string.IsNullOrWhiteSpace(txtWebsite.Text))
            {
                if (!txtWebsite.Text.StartsWith("http://") &&
                    !txtWebsite.Text.StartsWith("https://") &&
                    !txtWebsite.Text.StartsWith("www."))
                {
                    MessageBox.Show("Website should start with http://, https://, or www.", "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                    //https://www.youtube.com/watch?v=1yqv5R5WJGQ
                }
            }


            if (string.IsNullOrWhiteSpace(txtCity.Text) || string.IsNullOrWhiteSpace(txtCountry.Text))
            {
                MessageBox.Show("City and country cannot be empty.", "Input Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (string.IsNullOrWhiteSpace(txtCity.Text))
                    txtCity.Focus();
                else
                    txtCountry.Focus();
                return false;
            }


            if (!string.IsNullOrWhiteSpace(txtPostalCode.Text))
            {

                string postalPattern = @"^[0-9a-zA-Z\s\-]*$";
                if (!System.Text.RegularExpressions.Regex.IsMatch(txtPostalCode.Text.Trim(), postalPattern))
                {
                    MessageBox.Show("Postal code contains invalid characters.", "Input Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPostalCode.Focus();
                    return false;
                }
            }


            if (cmbPaymentTerms.SelectedIndex == -1)
            {
                MessageBox.Show("Please select payment terms.", "Input Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbPaymentTerms.Focus();
                return false;
            }

   
            return true;
        }
        private void KhoaChinhSua(bool enable)
        {
            
            txtCompanyName.ReadOnly = !enable;
            txtContactPerson.ReadOnly = !enable;
            txtEmail.ReadOnly = !enable;
            txtPhone.ReadOnly = !enable;
            txtWebsite.ReadOnly = !enable;
            txtStreetAddress.ReadOnly = !enable;
            txtCity.ReadOnly = !enable;
            txtStateProvince.ReadOnly = !enable;
            txtPostalCode.ReadOnly = !enable;
            txtCountry.ReadOnly = !enable;
            txtNotes.ReadOnly = !enable;

        
            cmbPaymentTerms.Enabled = enable;

            btnSave.Enabled = enable;
            btnCancel.Enabled = enable;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            
        }
        private void XoaDuLieu()
        {

            txtCompanyName.Text = string.Empty;
            txtContactPerson.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtWebsite.Text = string.Empty;
            txtStreetAddress.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtStateProvince.Text = string.Empty;
            txtPostalCode.Text = string.Empty;
            txtCountry.Text = string.Empty;
            txtNotes.Text = string.Empty;

  
            cmbPaymentTerms.SelectedIndex = -1;


            if (dgvProductsSupplied.DataSource != null)
            {
                dgvProductsSupplied.DataSource = null;
            }
            else
            {
                dgvProductsSupplied.Rows.Clear();
            }


        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
        int supplierID = 0;
        private void dgvSuppliers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            else
            {
                DataRowView dataRowView = (DataRowView)dgvSuppliers.Rows[e.RowIndex].DataBoundItem;
                if (dataRowView != null)
                {
                    supplierID = Convert.ToInt32(dataRowView.Row["SupplierID"]);
                    btnEdit.Enabled = true;
                    btnDelete.Enabled = true;
                    txtCompanyName.Text = dataRowView.Row["CompanyName"].ToString();
                    txtContactPerson.Text = dataRowView.Row["ContactName"].ToString();
                    txtPhone.Text = dataRowView.Row["Phone"].ToString();
                    txtEmail.Text = dataRowView.Row["Email"].ToString();
                    txtWebsite.Text = dataRowView.Row["Website"].ToString();
                    txtStreetAddress.Text = dataRowView.Row["Address"].ToString();
                    txtCity.Text = dataRowView.Row["City"].ToString();
                    txtStateProvince.Text = dataRowView.Row["State"].ToString();
                    txtPostalCode.Text = dataRowView.Row["PostalCode"].ToString();
                    txtCountry.Text = dataRowView.Row["Country"].ToString();
                    txtNotes.Text = dataRowView.Row["Notes"].ToString();
                    cmbPaymentTerms.Text = dataRowView.Row["PaymentTerms"].ToString();
                    Load_dgvProductsSupplied();
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            using(conn = new SqlConnection(chuoiketnoi))
            {
                conn.Open();
                string query = "Select * from Supplier where CompanyName like @CompanyName";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CompanyName", "%" + txtSearch.Text + "%");
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                dgvSuppliers.DataSource = dt;
                dgvSuppliers.Columns["SupplierID"].DataPropertyName = "SupplierID";
                dgvSuppliers.Columns["CompanyName"].DataPropertyName = "CompanyName";
                dgvSuppliers.Columns["ContactName"].DataPropertyName = "ContactName";
                dgvSuppliers.Columns["Phone"].DataPropertyName = "Phone";
            }
        }
    }
}