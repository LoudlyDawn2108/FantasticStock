using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FantasticStock.Views.Inventory
{
    public partial class SupplierManagementView : UserControl
    {
        public SupplierManagementView()
        {
            InitializeComponent();
        }

        private void SupplierManagementView_Load(object sender, EventArgs e)
        {
            // Initialize suppliers list
            InitializeSuppliersList();

            // Load sample data for the products supplied
            LoadSampleProducts();
        }

        private void InitializeSuppliersList()
        {
            // Configure the ListView
            lstSuppliers.View = View.Details;
            lstSuppliers.FullRowSelect = true;
            lstSuppliers.GridLines = true;

            // Add columns to the ListView
            //lstSuppliers.Columns.Add("Supplier", 200);
            //lstSuppliers.Columns.Add("Contact", 0); // Hidden column

            //// Add sample data
            //AddSupplier("TechSuppliers Inc.", "John Williams\n(555) 123-4567");
            //AddSupplier("AudioTech Ltd.", "Sarah Johnson\n(555) 234-5678");
            //AddSupplier("VisualTech Corp", "Michael Chen\n(555) 345-6789");
            //AddSupplier("ConnectAll Inc.", "Lisa Rodriguez\n(555) 456-7890");
            //AddSupplier("GlobalComputers Ltd.", "David Smith\n(555) 567-8901");
            //AddSupplier("NetworkPro Solutions", "");

            // Select the first item by default
            if (lstSuppliers.Items.Count > 0)
            {
                lstSuppliers.Items[0].Selected = true;
            }

            // Hook up selection change event
            lstSuppliers.SelectedIndexChanged += LstSuppliers_SelectedIndexChanged;
        }

        private void LstSuppliers_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Update the supplier details panel based on the selection
            if (lstSuppliers.SelectedItems.Count > 0)
            {
                // In a real app, you would load the data from a database
                // Here we just simulate different data for different suppliers
                string supplierName = lstSuppliers.SelectedItems[0].Text;
                //UpdateSupplierDetails(supplierName);
            }
        }

  

        private void AddSupplier(string name, string contact)
        {
            ListViewItem item = new ListViewItem(name);
            item.SubItems.Add(contact);
            lstSuppliers.Items.Add(item);
        }

        private void LoadSampleProducts()
        {
            // Clear existing rows
            dgvProductsSupplied.Rows.Clear();

            // Add sample data
            dgvProductsSupplied.Rows.Add("P001", "Laptop Pro 15\"", "7", "2025-02-15");
            dgvProductsSupplied.Rows.Add("P002", "Wireless Mouse", "5", "2025-02-20");
            dgvProductsSupplied.Rows.Add("P006", "Wireless Keyboard", "5", "2025-02-25");
        }

        private void btnAddNewSupplier_Click(object sender, EventArgs e)
        {
            // Logic to add a new supplier
            // This would typically open a new form or clear the current form for data entry
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Logic to enable editing of supplier details
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Logic to delete the selected supplier
            if (lstSuppliers.SelectedItems.Count > 0)
            {
                if (MessageBox.Show("Are you sure you want to delete this supplier?", "Confirm Delete",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    lstSuppliers.Items.Remove(lstSuppliers.SelectedItems[0]);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Logic to save supplier details
            MessageBox.Show("Supplier information saved successfully.", "Save Successful",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Logic to cancel changes
            if (lstSuppliers.SelectedItems.Count > 0)
            {
                // Reload the current supplier details
                //UpdateSupplierDetails(lstSuppliers.SelectedItems[0].Text);
            }
        }

        private Panel panel1;
        private Button btnAddNewSupplier;
        private Label lblTitle;
        private FlowLayoutPanel flowLayoutPanel1;
        private Panel panel3;
        private Panel panel5;
        private Label lblSuppliers;
        private Panel pnlSupplierDetails;
        private Label lblSupplierDetails;
        private Label lblCompanyName;
        private TextBox txtCompanyName;
        private Label lblContactPerson;
        private TextBox txtContactPerson;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblPhone;
        private TextBox txtPhone;
        private Label lblWebsite;
        private TextBox txtWebsite;
        private Label lblPaymentTerms;
        private ComboBox cmbPaymentTerms;
        private Label lblAddress;
        private Label lblStreetAddress;
        private TextBox txtStreetAddress;
        private Label lblCity;
        private TextBox txtCity;
        private Label lblStateProvince;
        private TextBox txtStateProvince;
        private Label lblPostalCode;
        private TextBox txtPostalCode;
        private Label lblCountry;
        private TextBox txtCountry;
        private Label lblNotes;
        private TextBox txtNotes;
        private Label lblProductsSupplied;
        private DataGridView dgvProductsSupplied;
        private DataGridViewTextBoxColumn colProductId;
        private DataGridViewTextBoxColumn colProductName;
        private DataGridViewTextBoxColumn colLeadTime;
        private DataGridViewTextBoxColumn colLastOrderDate;
        private Button btnSave;
        private Button btnCancel;
        private Panel panel2;
        private PictureBox pictureBox1;
        private TextBox txtSearch;
        private ListView lstSuppliers;
        private Panel panel4;
        private TableLayoutPanel tableLayoutPanel1;
        private Button btnEdit;
        private Button btnDelete;
    }
}