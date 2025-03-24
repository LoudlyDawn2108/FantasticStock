using System.Drawing;
using System.Windows.Forms;
using System;

namespace FantasticStock.Views.Financial
{
    partial class AddInvoiceForm
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
            this.Text = _isEditMode ? "Edit Invoice" : "Add Invoice";
            this.Size = new Size(450, 450);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

            // Create labels
            var lblInvoiceNumber = new Label { Text = "Invoice Number:", Left = 20, Top = 20, Width = 110 };
            var lblInvoiceDate = new Label { Text = "Invoice Date:", Left = 20, Top = 50, Width = 110 };
            var lblDueDate = new Label { Text = "Due Date:", Left = 20, Top = 80, Width = 110 };
            var lblCustomer = new Label { Text = "Customer:", Left = 20, Top = 110, Width = 110 };
            var lblAmount = new Label { Text = "Amount:", Left = 20, Top = 140, Width = 110 };
            var lblStatus = new Label { Text = "Status:", Left = 20, Top = 170, Width = 110 };
            var lblDescription = new Label { Text = "Description:", Left = 20, Top = 200, Width = 110 };
            var lblNotes = new Label { Text = "Notes:", Left = 20, Top = 260, Width = 110 };

            // Create controls
            txtInvoiceNumber = new TextBox { Left = 140, Top = 20, Width = 270 };
            dtpInvoiceDate = new DateTimePicker { Left = 140, Top = 50, Width = 200 };
            dtpDueDate = new DateTimePicker { Left = 140, Top = 80, Width = 200 };
            cboCustomer = new ComboBox { Left = 140, Top = 110, Width = 270, DropDownStyle = ComboBoxStyle.DropDownList };
            txtAmount = new TextBox { Left = 140, Top = 140, Width = 150, TextAlign = HorizontalAlignment.Right };
            cboStatus = new ComboBox { Left = 140, Top = 170, Width = 150, DropDownStyle = ComboBoxStyle.DropDownList };
            txtDescription = new TextBox { Left = 140, Top = 200, Width = 270, Height = 50, Multiline = true };
            txtNotes = new TextBox { Left = 140, Top = 260, Width = 270, Height = 80, Multiline = true };
            btnSave = new Button { Text = "Save", Left = 230, Top = 360, Width = 80, DialogResult = DialogResult.OK };
            btnCancel = new Button { Text = "Cancel", Left = 330, Top = 360, Width = 80, DialogResult = DialogResult.Cancel };

            // Set default values
            dtpInvoiceDate.Value = DateTime.Today;
            dtpDueDate.Value = DateTime.Today.AddDays(30);

            // Load statuses into combobox
            cboStatus.Items.Add("Open");
            cboStatus.Items.Add("Partial");
            cboStatus.Items.Add("Paid");
            cboStatus.Items.Add("Canceled");
            cboStatus.SelectedIndex = 0;

            // Set event handlers
            btnSave.Click += BtnSave_Click;

            // Add controls to form
            this.Controls.AddRange(new Control[] {
            lblInvoiceNumber, txtInvoiceNumber,
            lblInvoiceDate, dtpInvoiceDate,
            lblDueDate, dtpDueDate,
            lblCustomer, cboCustomer,
            lblAmount, txtAmount,
            lblStatus, cboStatus,
            lblDescription, txtDescription,
            lblNotes, txtNotes,
            btnSave, btnCancel
        });

            this.AcceptButton = btnSave;
            this.CancelButton = btnCancel;
        }

        #endregion
    }
}