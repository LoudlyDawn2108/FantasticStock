using System;
using System.Drawing;
using System.Windows.Forms;

namespace FantasticStock.Views.Financial
{
    partial class AddExpenseForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtExpenseNumber = new System.Windows.Forms.TextBox();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.txtTaxAmount = new System.Windows.Forms.TextBox();
            this.txtReferenceNumber = new System.Windows.Forms.TextBox();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblExpenseNumber = new System.Windows.Forms.Label();
            this.lblExpenseDate = new System.Windows.Forms.Label();
            this.lblSupplier = new System.Windows.Forms.Label();
            this.lblCategory = new System.Windows.Forms.Label();
            this.lblPaymentMethod = new System.Windows.Forms.Label();
            this.lblReferenceNumber = new System.Windows.Forms.Label();
            this.lblAmount = new System.Windows.Forms.Label();
            this.lblTaxAmount = new System.Windows.Forms.Label();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.lblNotes = new System.Windows.Forms.Label();
            this.cboCategory = new System.Windows.Forms.ComboBox();
            this.cboPaymentMethod = new System.Windows.Forms.ComboBox();
            this.chkTaxDeductible = new System.Windows.Forms.CheckBox();
            this.dtpExpenseDate = new System.Windows.Forms.DateTimePicker();
            this.cboSupplier = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAddSupplier = new System.Windows.Forms.Button();
            this.btnAddCategory = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtExpenseNumber
            // 
            this.txtExpenseNumber.Location = new System.Drawing.Point(150, 20);
            this.txtExpenseNumber.Name = "txtExpenseNumber";
            this.txtExpenseNumber.ReadOnly = true;
            this.txtExpenseNumber.Size = new System.Drawing.Size(300, 23);
            this.txtExpenseNumber.TabIndex = 1;
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(150, 200);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(150, 23);
            this.txtAmount.TabIndex = 15;
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAmount.Leave += new System.EventHandler(this.TxtAmount_Leave);
            // 
            // txtTaxAmount
            // 
            this.txtTaxAmount.Location = new System.Drawing.Point(150, 230);
            this.txtTaxAmount.Name = "txtTaxAmount";
            this.txtTaxAmount.Size = new System.Drawing.Size(150, 23);
            this.txtTaxAmount.TabIndex = 17;
            this.txtTaxAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTaxAmount.Leave += new System.EventHandler(this.TxtTaxAmount_Leave);
            // 
            // txtReferenceNumber
            // 
            this.txtReferenceNumber.Location = new System.Drawing.Point(150, 170);
            this.txtReferenceNumber.Name = "txtReferenceNumber";
            this.txtReferenceNumber.Size = new System.Drawing.Size(300, 23);
            this.txtReferenceNumber.TabIndex = 13;
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(150, 290);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(300, 80);
            this.txtNotes.TabIndex = 21;
            // 
            // lblTotal
            // 
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTotal.Location = new System.Drawing.Point(150, 260);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(150, 23);
            this.lblTotal.TabIndex = 19;
            this.lblTotal.Text = "$0.00";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblExpenseNumber
            // 
            this.lblExpenseNumber.Location = new System.Drawing.Point(20, 20);
            this.lblExpenseNumber.Name = "lblExpenseNumber";
            this.lblExpenseNumber.Size = new System.Drawing.Size(120, 23);
            this.lblExpenseNumber.TabIndex = 0;
            this.lblExpenseNumber.Text = "Expense #:";
            // 
            // lblExpenseDate
            // 
            this.lblExpenseDate.Location = new System.Drawing.Point(20, 50);
            this.lblExpenseDate.Name = "lblExpenseDate";
            this.lblExpenseDate.Size = new System.Drawing.Size(120, 23);
            this.lblExpenseDate.TabIndex = 2;
            this.lblExpenseDate.Text = "Date:";
            // 
            // lblSupplier
            // 
            this.lblSupplier.Location = new System.Drawing.Point(20, 80);
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(120, 23);
            this.lblSupplier.TabIndex = 4;
            this.lblSupplier.Text = "Supplier:";
            // 
            // lblCategory
            // 
            this.lblCategory.Location = new System.Drawing.Point(20, 110);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(120, 23);
            this.lblCategory.TabIndex = 7;
            this.lblCategory.Text = "Category:";
            // 
            // lblPaymentMethod
            // 
            this.lblPaymentMethod.Location = new System.Drawing.Point(20, 140);
            this.lblPaymentMethod.Name = "lblPaymentMethod";
            this.lblPaymentMethod.Size = new System.Drawing.Size(120, 23);
            this.lblPaymentMethod.TabIndex = 10;
            this.lblPaymentMethod.Text = "Payment Method:";
            // 
            // lblReferenceNumber
            // 
            this.lblReferenceNumber.Location = new System.Drawing.Point(20, 170);
            this.lblReferenceNumber.Name = "lblReferenceNumber";
            this.lblReferenceNumber.Size = new System.Drawing.Size(120, 23);
            this.lblReferenceNumber.TabIndex = 12;
            this.lblReferenceNumber.Text = "Reference #:";
            // 
            // lblAmount
            // 
            this.lblAmount.Location = new System.Drawing.Point(20, 200);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(120, 23);
            this.lblAmount.TabIndex = 14;
            this.lblAmount.Text = "Amount:";
            // 
            // lblTaxAmount
            // 
            this.lblTaxAmount.Location = new System.Drawing.Point(20, 230);
            this.lblTaxAmount.Name = "lblTaxAmount";
            this.lblTaxAmount.Size = new System.Drawing.Size(120, 23);
            this.lblTaxAmount.TabIndex = 16;
            this.lblTaxAmount.Text = "Tax Amount:";
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.Location = new System.Drawing.Point(20, 260);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(120, 23);
            this.lblTotalAmount.TabIndex = 18;
            this.lblTotalAmount.Text = "Total:";
            // 
            // lblNotes
            // 
            this.lblNotes.Location = new System.Drawing.Point(20, 290);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(120, 23);
            this.lblNotes.TabIndex = 20;
            this.lblNotes.Text = "Notes:";
            // 
            // cboCategory
            // 
            this.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategory.Location = new System.Drawing.Point(150, 110);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Size = new System.Drawing.Size(300, 23);
            this.cboCategory.TabIndex = 8;
            // 
            // cboPaymentMethod
            // 
            this.cboPaymentMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPaymentMethod.Location = new System.Drawing.Point(150, 140);
            this.cboPaymentMethod.Name = "cboPaymentMethod";
            this.cboPaymentMethod.Size = new System.Drawing.Size(300, 23);
            this.cboPaymentMethod.TabIndex = 11;
            // 
            // chkTaxDeductible
            // 
            this.chkTaxDeductible.Checked = true;
            this.chkTaxDeductible.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTaxDeductible.Location = new System.Drawing.Point(150, 380);
            this.chkTaxDeductible.Name = "chkTaxDeductible";
            this.chkTaxDeductible.Size = new System.Drawing.Size(150, 24);
            this.chkTaxDeductible.TabIndex = 22;
            this.chkTaxDeductible.Text = "Tax Deductible";
            // 
            // dtpExpenseDate
            // 
            this.dtpExpenseDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpExpenseDate.Location = new System.Drawing.Point(150, 50);
            this.dtpExpenseDate.Name = "dtpExpenseDate";
            this.dtpExpenseDate.Size = new System.Drawing.Size(200, 23);
            this.dtpExpenseDate.TabIndex = 3;
            // 
            // cboSupplier
            // 
            this.cboSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSupplier.Location = new System.Drawing.Point(150, 80);
            this.cboSupplier.Name = "cboSupplier";
            this.cboSupplier.Size = new System.Drawing.Size(300, 23);
            this.cboSupplier.TabIndex = 5;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(230, 420);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 30);
            this.btnSave.TabIndex = 23;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.LightCoral;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(350, 420);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 30);
            this.btnCancel.TabIndex = 24;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnAddSupplier
            // 
            this.btnAddSupplier.Location = new System.Drawing.Point(460, 80);
            this.btnAddSupplier.Name = "btnAddSupplier";
            this.btnAddSupplier.Size = new System.Drawing.Size(25, 25);
            this.btnAddSupplier.TabIndex = 6;
            this.btnAddSupplier.Text = "+";
            this.btnAddSupplier.Click += new System.EventHandler(this.BtnAddSupplier_Click);
            // 
            // btnAddCategory
            // 
            this.btnAddCategory.Location = new System.Drawing.Point(460, 110);
            this.btnAddCategory.Name = "btnAddCategory";
            this.btnAddCategory.Size = new System.Drawing.Size(25, 25);
            this.btnAddCategory.TabIndex = 9;
            this.btnAddCategory.Text = "+";
            this.btnAddCategory.Click += new System.EventHandler(this.BtnAddCategory_Click);
            // 
            // AddExpenseForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(500, 500);
            this.Controls.Add(this.lblExpenseNumber);
            this.Controls.Add(this.txtExpenseNumber);
            this.Controls.Add(this.lblExpenseDate);
            this.Controls.Add(this.dtpExpenseDate);
            this.Controls.Add(this.lblSupplier);
            this.Controls.Add(this.cboSupplier);
            this.Controls.Add(this.btnAddSupplier);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.cboCategory);
            this.Controls.Add(this.btnAddCategory);
            this.Controls.Add(this.lblPaymentMethod);
            this.Controls.Add(this.cboPaymentMethod);
            this.Controls.Add(this.lblReferenceNumber);
            this.Controls.Add(this.txtReferenceNumber);
            this.Controls.Add(this.lblAmount);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.lblTaxAmount);
            this.Controls.Add(this.txtTaxAmount);
            this.Controls.Add(this.lblTotalAmount);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblNotes);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.chkTaxDeductible);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddExpenseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add New Expense";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        // UI Controls
        private System.Windows.Forms.TextBox txtExpenseNumber;
        private System.Windows.Forms.DateTimePicker dtpExpenseDate;
        private System.Windows.Forms.ComboBox cboSupplier;
        private System.Windows.Forms.ComboBox cboCategory;
        private System.Windows.Forms.ComboBox cboPaymentMethod;
        private System.Windows.Forms.TextBox txtReferenceNumber;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.TextBox txtTaxAmount;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblExpenseNumber;
        private System.Windows.Forms.Label lblExpenseDate;
        private System.Windows.Forms.Label lblSupplier;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Label lblPaymentMethod;
        private System.Windows.Forms.Label lblReferenceNumber;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.Label lblTaxAmount;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.CheckBox chkTaxDeductible;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAddSupplier;
        private System.Windows.Forms.Button btnAddCategory;

        #endregion
    }
}