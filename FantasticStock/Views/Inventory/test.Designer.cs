namespace FantasticStock.Views.Inventory
{
    partial class test
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lstSuppliers = new System.Windows.Forms.ListView();
            this.txtSearchSuppliers = new System.Windows.Forms.TextBox();
            this.lblSuppliers = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.pnlSupplierDetails = new System.Windows.Forms.Panel();
            this.lblSupplierDetails = new System.Windows.Forms.Label();
            this.lblCompanyName = new System.Windows.Forms.Label();
            this.txtCompanyName = new System.Windows.Forms.TextBox();
            this.lblContactPerson = new System.Windows.Forms.Label();
            this.txtContactPerson = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.lblWebsite = new System.Windows.Forms.Label();
            this.txtWebsite = new System.Windows.Forms.TextBox();
            this.lblPaymentTerms = new System.Windows.Forms.Label();
            this.cmbPaymentTerms = new System.Windows.Forms.ComboBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.lblStreetAddress = new System.Windows.Forms.Label();
            this.txtStreetAddress = new System.Windows.Forms.TextBox();
            this.lblCity = new System.Windows.Forms.Label();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.lblStateProvince = new System.Windows.Forms.Label();
            this.txtStateProvince = new System.Windows.Forms.TextBox();
            this.lblPostalCode = new System.Windows.Forms.Label();
            this.txtPostalCode = new System.Windows.Forms.TextBox();
            this.lblCountry = new System.Windows.Forms.Label();
            this.txtCountry = new System.Windows.Forms.TextBox();
            this.lblNotes = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.lblProductsSupplied = new System.Windows.Forms.Label();
            this.dgvProductsSupplied = new System.Windows.Forms.DataGridView();
            this.colProductId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLeadTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLastOrderDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlSupplierDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductsSupplied)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.panel1);
            this.flowLayoutPanel1.Controls.Add(this.pnlSupplierDetails);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(198, 144);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(303, 307);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lstSuppliers);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(269, 387);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtSearchSuppliers);
            this.panel2.Controls.Add(this.lblSuppliers);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(269, 100);
            this.panel2.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnDelete);
            this.panel3.Controls.Add(this.btnEdit);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 335);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(269, 52);
            this.panel3.TabIndex = 1;
            // 
            // lstSuppliers
            // 
            this.lstSuppliers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstSuppliers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstSuppliers.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstSuppliers.FullRowSelect = true;
            this.lstSuppliers.HideSelection = false;
            this.lstSuppliers.Location = new System.Drawing.Point(0, 100);
            this.lstSuppliers.Name = "lstSuppliers";
            this.lstSuppliers.Size = new System.Drawing.Size(269, 235);
            this.lstSuppliers.TabIndex = 5;
            this.lstSuppliers.UseCompatibleStateImageBehavior = false;
            this.lstSuppliers.View = System.Windows.Forms.View.Details;
            // 
            // txtSearchSuppliers
            // 
            this.txtSearchSuppliers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearchSuppliers.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchSuppliers.Location = new System.Drawing.Point(108, 35);
            this.txtSearchSuppliers.Name = "txtSearchSuppliers";
            this.txtSearchSuppliers.Size = new System.Drawing.Size(158, 31);
            this.txtSearchSuppliers.TabIndex = 5;
            // 
            // lblSuppliers
            // 
            this.lblSuppliers.AutoSize = true;
            this.lblSuppliers.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSuppliers.Location = new System.Drawing.Point(3, 34);
            this.lblSuppliers.Name = "lblSuppliers";
            this.lblSuppliers.Size = new System.Drawing.Size(99, 28);
            this.lblSuppliers.TabIndex = 4;
            this.lblSuppliers.Text = "Suppliers";
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.Crimson;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(120, 14);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(64, 25);
            this.btnDelete.TabIndex = 9;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.White;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.Location = new System.Drawing.Point(84, 14);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(30, 25);
            this.btnEdit.TabIndex = 8;
            this.btnEdit.Text = "✏️";
            this.btnEdit.UseVisualStyleBackColor = true;
            // 
            // pnlSupplierDetails
            // 
            this.pnlSupplierDetails.Controls.Add(this.lblSupplierDetails);
            this.pnlSupplierDetails.Controls.Add(this.lblCompanyName);
            this.pnlSupplierDetails.Controls.Add(this.txtCompanyName);
            this.pnlSupplierDetails.Controls.Add(this.lblContactPerson);
            this.pnlSupplierDetails.Controls.Add(this.txtContactPerson);
            this.pnlSupplierDetails.Controls.Add(this.lblEmail);
            this.pnlSupplierDetails.Controls.Add(this.txtEmail);
            this.pnlSupplierDetails.Controls.Add(this.lblPhone);
            this.pnlSupplierDetails.Controls.Add(this.txtPhone);
            this.pnlSupplierDetails.Controls.Add(this.lblWebsite);
            this.pnlSupplierDetails.Controls.Add(this.txtWebsite);
            this.pnlSupplierDetails.Controls.Add(this.lblPaymentTerms);
            this.pnlSupplierDetails.Controls.Add(this.cmbPaymentTerms);
            this.pnlSupplierDetails.Controls.Add(this.lblAddress);
            this.pnlSupplierDetails.Controls.Add(this.lblStreetAddress);
            this.pnlSupplierDetails.Controls.Add(this.txtStreetAddress);
            this.pnlSupplierDetails.Controls.Add(this.lblCity);
            this.pnlSupplierDetails.Controls.Add(this.txtCity);
            this.pnlSupplierDetails.Controls.Add(this.lblStateProvince);
            this.pnlSupplierDetails.Controls.Add(this.txtStateProvince);
            this.pnlSupplierDetails.Controls.Add(this.lblPostalCode);
            this.pnlSupplierDetails.Controls.Add(this.txtPostalCode);
            this.pnlSupplierDetails.Controls.Add(this.lblCountry);
            this.pnlSupplierDetails.Controls.Add(this.txtCountry);
            this.pnlSupplierDetails.Controls.Add(this.lblNotes);
            this.pnlSupplierDetails.Controls.Add(this.txtNotes);
            this.pnlSupplierDetails.Controls.Add(this.lblProductsSupplied);
            this.pnlSupplierDetails.Controls.Add(this.dgvProductsSupplied);
            this.pnlSupplierDetails.Controls.Add(this.btnSave);
            this.pnlSupplierDetails.Controls.Add(this.btnCancel);
            this.pnlSupplierDetails.Location = new System.Drawing.Point(3, 396);
            this.pnlSupplierDetails.Name = "pnlSupplierDetails";
            this.pnlSupplierDetails.Size = new System.Drawing.Size(810, 608);
            this.pnlSupplierDetails.TabIndex = 6;
            // 
            // lblSupplierDetails
            // 
            this.lblSupplierDetails.AutoSize = true;
            this.lblSupplierDetails.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSupplierDetails.Location = new System.Drawing.Point(14, 16);
            this.lblSupplierDetails.Name = "lblSupplierDetails";
            this.lblSupplierDetails.Size = new System.Drawing.Size(162, 28);
            this.lblSupplierDetails.TabIndex = 6;
            this.lblSupplierDetails.Text = "Supplier Details";
            // 
            // lblCompanyName
            // 
            this.lblCompanyName.AutoSize = true;
            this.lblCompanyName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompanyName.Location = new System.Drawing.Point(118, 107);
            this.lblCompanyName.Name = "lblCompanyName";
            this.lblCompanyName.Size = new System.Drawing.Size(141, 25);
            this.lblCompanyName.TabIndex = 7;
            this.lblCompanyName.Text = "Company Name";
            // 
            // txtCompanyName
            // 
            this.txtCompanyName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCompanyName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCompanyName.Location = new System.Drawing.Point(17, 73);
            this.txtCompanyName.Name = "txtCompanyName";
            this.txtCompanyName.Size = new System.Drawing.Size(525, 31);
            this.txtCompanyName.TabIndex = 8;
            this.txtCompanyName.Text = "TechSuppliers Inc.";
            // 
            // lblContactPerson
            // 
            this.lblContactPerson.AutoSize = true;
            this.lblContactPerson.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContactPerson.Location = new System.Drawing.Point(587, 48);
            this.lblContactPerson.Name = "lblContactPerson";
            this.lblContactPerson.Size = new System.Drawing.Size(131, 25);
            this.lblContactPerson.TabIndex = 9;
            this.lblContactPerson.Text = "Contact Person";
            // 
            // txtContactPerson
            // 
            this.txtContactPerson.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtContactPerson.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtContactPerson.Location = new System.Drawing.Point(590, 73);
            this.txtContactPerson.Name = "txtContactPerson";
            this.txtContactPerson.Size = new System.Drawing.Size(197, 31);
            this.txtContactPerson.TabIndex = 10;
            this.txtContactPerson.Text = "John Williams";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.Location = new System.Drawing.Point(14, 112);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(54, 25);
            this.lblEmail.TabIndex = 11;
            this.lblEmail.Text = "Email";
            // 
            // txtEmail
            // 
            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(17, 138);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(525, 31);
            this.txtEmail.TabIndex = 12;
            this.txtEmail.Text = "john.williams@techsuppliers.com";
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhone.Location = new System.Drawing.Point(587, 112);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(62, 25);
            this.lblPhone.TabIndex = 13;
            this.lblPhone.Text = "Phone";
            // 
            // txtPhone
            // 
            this.txtPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPhone.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhone.Location = new System.Drawing.Point(590, 138);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(197, 31);
            this.txtPhone.TabIndex = 14;
            this.txtPhone.Text = "(555) 123-4567";
            // 
            // lblWebsite
            // 
            this.lblWebsite.AutoSize = true;
            this.lblWebsite.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWebsite.Location = new System.Drawing.Point(14, 177);
            this.lblWebsite.Name = "lblWebsite";
            this.lblWebsite.Size = new System.Drawing.Size(75, 25);
            this.lblWebsite.TabIndex = 15;
            this.lblWebsite.Text = "Website";
            // 
            // txtWebsite
            // 
            this.txtWebsite.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtWebsite.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWebsite.Location = new System.Drawing.Point(17, 202);
            this.txtWebsite.Name = "txtWebsite";
            this.txtWebsite.Size = new System.Drawing.Size(525, 31);
            this.txtWebsite.TabIndex = 16;
            this.txtWebsite.Text = "https://www.techsuppliers.com";
            // 
            // lblPaymentTerms
            // 
            this.lblPaymentTerms.AutoSize = true;
            this.lblPaymentTerms.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPaymentTerms.Location = new System.Drawing.Point(587, 177);
            this.lblPaymentTerms.Name = "lblPaymentTerms";
            this.lblPaymentTerms.Size = new System.Drawing.Size(131, 25);
            this.lblPaymentTerms.TabIndex = 17;
            this.lblPaymentTerms.Text = "Payment Terms";
            // 
            // cmbPaymentTerms
            // 
            this.cmbPaymentTerms.FormattingEnabled = true;
            this.cmbPaymentTerms.Items.AddRange(new object[] {
            "Net 15",
            "Net 30",
            "Net 45",
            "Net 60",
            "Net 90"});
            this.cmbPaymentTerms.Location = new System.Drawing.Point(590, 202);
            this.cmbPaymentTerms.Name = "cmbPaymentTerms";
            this.cmbPaymentTerms.Size = new System.Drawing.Size(197, 28);
            this.cmbPaymentTerms.TabIndex = 18;
            this.cmbPaymentTerms.Text = "Net 45";
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddress.Location = new System.Drawing.Point(14, 241);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(80, 25);
            this.lblAddress.TabIndex = 19;
            this.lblAddress.Text = "Address";
            // 
            // lblStreetAddress
            // 
            this.lblStreetAddress.AutoSize = true;
            this.lblStreetAddress.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStreetAddress.Location = new System.Drawing.Point(14, 275);
            this.lblStreetAddress.Name = "lblStreetAddress";
            this.lblStreetAddress.Size = new System.Drawing.Size(127, 25);
            this.lblStreetAddress.TabIndex = 20;
            this.lblStreetAddress.Text = "Street Address";
            // 
            // txtStreetAddress
            // 
            this.txtStreetAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtStreetAddress.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStreetAddress.Location = new System.Drawing.Point(17, 300);
            this.txtStreetAddress.Name = "txtStreetAddress";
            this.txtStreetAddress.Size = new System.Drawing.Size(525, 31);
            this.txtStreetAddress.TabIndex = 21;
            this.txtStreetAddress.Text = "123 Tech Boulevard";
            // 
            // lblCity
            // 
            this.lblCity.AutoSize = true;
            this.lblCity.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCity.Location = new System.Drawing.Point(14, 339);
            this.lblCity.Name = "lblCity";
            this.lblCity.Size = new System.Drawing.Size(42, 25);
            this.lblCity.TabIndex = 22;
            this.lblCity.Text = "City";
            // 
            // txtCity
            // 
            this.txtCity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCity.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCity.Location = new System.Drawing.Point(17, 364);
            this.txtCity.Name = "txtCity";
            this.txtCity.Size = new System.Drawing.Size(525, 31);
            this.txtCity.TabIndex = 23;
            this.txtCity.Text = "Silicon Valley";
            // 
            // lblStateProvince
            // 
            this.lblStateProvince.AutoSize = true;
            this.lblStateProvince.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStateProvince.Location = new System.Drawing.Point(587, 339);
            this.lblStateProvince.Name = "lblStateProvince";
            this.lblStateProvince.Size = new System.Drawing.Size(125, 25);
            this.lblStateProvince.TabIndex = 24;
            this.lblStateProvince.Text = "State/Province";
            // 
            // txtStateProvince
            // 
            this.txtStateProvince.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtStateProvince.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStateProvince.Location = new System.Drawing.Point(590, 364);
            this.txtStateProvince.Name = "txtStateProvince";
            this.txtStateProvince.Size = new System.Drawing.Size(197, 31);
            this.txtStateProvince.TabIndex = 25;
            this.txtStateProvince.Text = "CA";
            // 
            // lblPostalCode
            // 
            this.lblPostalCode.AutoSize = true;
            this.lblPostalCode.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPostalCode.Location = new System.Drawing.Point(14, 403);
            this.lblPostalCode.Name = "lblPostalCode";
            this.lblPostalCode.Size = new System.Drawing.Size(106, 25);
            this.lblPostalCode.TabIndex = 26;
            this.lblPostalCode.Text = "Postal Code";
            // 
            // txtPostalCode
            // 
            this.txtPostalCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPostalCode.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPostalCode.Location = new System.Drawing.Point(17, 428);
            this.txtPostalCode.Name = "txtPostalCode";
            this.txtPostalCode.Size = new System.Drawing.Size(525, 31);
            this.txtPostalCode.TabIndex = 27;
            this.txtPostalCode.Text = "94025";
            // 
            // lblCountry
            // 
            this.lblCountry.AutoSize = true;
            this.lblCountry.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCountry.Location = new System.Drawing.Point(587, 403);
            this.lblCountry.Name = "lblCountry";
            this.lblCountry.Size = new System.Drawing.Size(75, 25);
            this.lblCountry.TabIndex = 28;
            this.lblCountry.Text = "Country";
            // 
            // txtCountry
            // 
            this.txtCountry.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCountry.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCountry.Location = new System.Drawing.Point(590, 428);
            this.txtCountry.Name = "txtCountry";
            this.txtCountry.Size = new System.Drawing.Size(197, 31);
            this.txtCountry.TabIndex = 29;
            this.txtCountry.Text = "United States";
            // 
            // lblNotes
            // 
            this.lblNotes.AutoSize = true;
            this.lblNotes.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotes.Location = new System.Drawing.Point(14, 467);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(59, 25);
            this.lblNotes.TabIndex = 30;
            this.lblNotes.Text = "Notes";
            // 
            // txtNotes
            // 
            this.txtNotes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNotes.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNotes.Location = new System.Drawing.Point(17, 492);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(534, 70);
            this.txtNotes.TabIndex = 31;
            this.txtNotes.Text = "Preferred supplier for laptop and desktop computers. Offers special discount for " +
    "bulk orders over $10,000.";
            // 
            // lblProductsSupplied
            // 
            this.lblProductsSupplied.AutoSize = true;
            this.lblProductsSupplied.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductsSupplied.Location = new System.Drawing.Point(14, 575);
            this.lblProductsSupplied.Name = "lblProductsSupplied";
            this.lblProductsSupplied.Size = new System.Drawing.Size(166, 25);
            this.lblProductsSupplied.TabIndex = 32;
            this.lblProductsSupplied.Text = "Products Supplied";
            // 
            // dgvProductsSupplied
            // 
            this.dgvProductsSupplied.AllowUserToAddRows = false;
            this.dgvProductsSupplied.AllowUserToDeleteRows = false;
            this.dgvProductsSupplied.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvProductsSupplied.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvProductsSupplied.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductsSupplied.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colProductId,
            this.colProductName,
            this.colLeadTime,
            this.colLastOrderDate});
            this.dgvProductsSupplied.Location = new System.Drawing.Point(17, 599);
            this.dgvProductsSupplied.Name = "dgvProductsSupplied";
            this.dgvProductsSupplied.ReadOnly = true;
            this.dgvProductsSupplied.RowHeadersWidth = 62;
            this.dgvProductsSupplied.Size = new System.Drawing.Size(770, 140);
            this.dgvProductsSupplied.TabIndex = 33;
            // 
            // colProductId
            // 
            this.colProductId.HeaderText = "Product ID";
            this.colProductId.MinimumWidth = 8;
            this.colProductId.Name = "colProductId";
            this.colProductId.ReadOnly = true;
            this.colProductId.Width = 150;
            // 
            // colProductName
            // 
            this.colProductName.HeaderText = "Product Name";
            this.colProductName.MinimumWidth = 8;
            this.colProductName.Name = "colProductName";
            this.colProductName.ReadOnly = true;
            this.colProductName.Width = 250;
            // 
            // colLeadTime
            // 
            this.colLeadTime.HeaderText = "Lead Time (days)";
            this.colLeadTime.MinimumWidth = 8;
            this.colLeadTime.Name = "colLeadTime";
            this.colLeadTime.ReadOnly = true;
            this.colLeadTime.Width = 150;
            // 
            // colLastOrderDate
            // 
            this.colLastOrderDate.HeaderText = "Last Order Date";
            this.colLastOrderDate.MinimumWidth = 8;
            this.colLastOrderDate.Name = "colLastOrderDate";
            this.colLastOrderDate.ReadOnly = true;
            this.colLastOrderDate.Width = 150;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(17, 763);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 30);
            this.btnSave.TabIndex = 34;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(123, 763);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 30);
            this.btnCancel.TabIndex = 35;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // test
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "test";
            this.Size = new System.Drawing.Size(1305, 762);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.pnlSupplierDetails.ResumeLayout(false);
            this.pnlSupplierDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductsSupplied)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListView lstSuppliers;
        private System.Windows.Forms.TextBox txtSearchSuppliers;
        private System.Windows.Forms.Label lblSuppliers;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Panel pnlSupplierDetails;
        private System.Windows.Forms.Label lblSupplierDetails;
        private System.Windows.Forms.Label lblCompanyName;
        private System.Windows.Forms.TextBox txtCompanyName;
        private System.Windows.Forms.Label lblContactPerson;
        private System.Windows.Forms.TextBox txtContactPerson;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label lblWebsite;
        private System.Windows.Forms.TextBox txtWebsite;
        private System.Windows.Forms.Label lblPaymentTerms;
        private System.Windows.Forms.ComboBox cmbPaymentTerms;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label lblStreetAddress;
        private System.Windows.Forms.TextBox txtStreetAddress;
        private System.Windows.Forms.Label lblCity;
        private System.Windows.Forms.TextBox txtCity;
        private System.Windows.Forms.Label lblStateProvince;
        private System.Windows.Forms.TextBox txtStateProvince;
        private System.Windows.Forms.Label lblPostalCode;
        private System.Windows.Forms.TextBox txtPostalCode;
        private System.Windows.Forms.Label lblCountry;
        private System.Windows.Forms.TextBox txtCountry;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Label lblProductsSupplied;
        private System.Windows.Forms.DataGridView dgvProductsSupplied;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLeadTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLastOrderDate;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}
