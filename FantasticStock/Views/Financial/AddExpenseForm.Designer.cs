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
            // Initialize components
            this.components = new System.ComponentModel.Container();
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

            // Form setup
            this.Text = "Add New Expense";
            this.ClientSize = new Size(500, 500);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);

            // Configure Labels
            this.lblExpenseNumber.Text = "Expense #:";
            this.lblExpenseNumber.Location = new Point(20, 20);
            this.lblExpenseNumber.Size = new Size(120, 23);

            this.lblExpenseDate.Text = "Date:";
            this.lblExpenseDate.Location = new Point(20, 50);
            this.lblExpenseDate.Size = new Size(120, 23);

            this.lblSupplier.Text = "Supplier:";
            this.lblSupplier.Location = new Point(20, 80);
            this.lblSupplier.Size = new Size(120, 23);

            this.lblCategory.Text = "Category:";
            this.lblCategory.Location = new Point(20, 110);
            this.lblCategory.Size = new Size(120, 23);

            this.lblPaymentMethod.Text = "Payment Method:";
            this.lblPaymentMethod.Location = new Point(20, 140);
            this.lblPaymentMethod.Size = new Size(120, 23);

            this.lblReferenceNumber.Text = "Reference #:";
            this.lblReferenceNumber.Location = new Point(20, 170);
            this.lblReferenceNumber.Size = new Size(120, 23);

            this.lblAmount.Text = "Amount:";
            this.lblAmount.Location = new Point(20, 200);
            this.lblAmount.Size = new Size(120, 23);

            this.lblTaxAmount.Text = "Tax Amount:";
            this.lblTaxAmount.Location = new Point(20, 230);
            this.lblTaxAmount.Size = new Size(120, 23);

            this.lblTotalAmount.Text = "Total:";
            this.lblTotalAmount.Location = new Point(20, 260);
            this.lblTotalAmount.Size = new Size(120, 23);

            this.lblNotes.Text = "Notes:";
            this.lblNotes.Location = new Point(20, 290);
            this.lblNotes.Size = new Size(120, 23);

            // Configure controls
            this.txtExpenseNumber.Location = new Point(150, 20);
            this.txtExpenseNumber.Size = new Size(300, 23);
            this.txtExpenseNumber.ReadOnly = true;

            this.dtpExpenseDate.Location = new Point(150, 50);
            this.dtpExpenseDate.Size = new Size(200, 23);
            this.dtpExpenseDate.Format = DateTimePickerFormat.Short;

            this.cboSupplier.Location = new Point(150, 80);
            this.cboSupplier.Size = new Size(300, 23);
            this.cboSupplier.DropDownStyle = ComboBoxStyle.DropDownList;

            this.btnAddSupplier.Text = "+";
            this.btnAddSupplier.Location = new Point(460, 80);
            this.btnAddSupplier.Size = new Size(25, 25);

            this.cboCategory.Location = new Point(150, 110);
            this.cboCategory.Size = new Size(300, 23);
            this.cboCategory.DropDownStyle = ComboBoxStyle.DropDownList;

            this.btnAddCategory.Text = "+";
            this.btnAddCategory.Location = new Point(460, 110);
            this.btnAddCategory.Size = new Size(25, 25);

            this.cboPaymentMethod.Location = new Point(150, 140);
            this.cboPaymentMethod.Size = new Size(300, 23);
            this.cboPaymentMethod.DropDownStyle = ComboBoxStyle.DropDownList;

            this.txtReferenceNumber.Location = new Point(150, 170);
            this.txtReferenceNumber.Size = new Size(300, 23);

            this.txtAmount.Location = new Point(150, 200);
            this.txtAmount.Size = new Size(150, 23);
            this.txtAmount.TextAlign = HorizontalAlignment.Right;

            this.txtTaxAmount.Location = new Point(150, 230);
            this.txtTaxAmount.Size = new Size(150, 23);
            this.txtTaxAmount.TextAlign = HorizontalAlignment.Right;

            this.lblTotal.Text = "$0.00";
            this.lblTotal.Location = new Point(150, 260);
            this.lblTotal.Size = new Size(150, 23);
            this.lblTotal.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblTotal.TextAlign = ContentAlignment.MiddleRight;

            this.txtNotes.Location = new Point(150, 290);
            this.txtNotes.Size = new Size(300, 80);
            this.txtNotes.Multiline = true;

            this.chkTaxDeductible.Text = "Tax Deductible";
            this.chkTaxDeductible.Location = new Point(150, 380);
            this.chkTaxDeductible.Size = new Size(150, 24);
            this.chkTaxDeductible.Checked = true;

            this.btnSave.Text = "Save";
            this.btnSave.Location = new Point(230, 420);
            this.btnSave.Size = new Size(100, 30);

            this.btnCancel.Text = "Cancel";
            this.btnCancel.Location = new Point(350, 420);
            this.btnCancel.Size = new Size(100, 30);
            this.btnCancel.DialogResult = DialogResult.Cancel;

            // Add controls to form
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

            // Wire up events
            this.txtAmount.Leave += new EventHandler(this.TxtAmount_Leave);
            this.txtTaxAmount.Leave += new EventHandler(this.TxtTaxAmount_Leave);
            this.btnSave.Click += new EventHandler(this.BtnSave_Click);
            this.btnAddSupplier.Click += new EventHandler(this.BtnAddSupplier_Click);
            this.btnAddCategory.Click += new EventHandler(this.BtnAddCategory_Click);

            this.AcceptButton = this.btnSave;
            this.CancelButton = this.btnCancel;

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