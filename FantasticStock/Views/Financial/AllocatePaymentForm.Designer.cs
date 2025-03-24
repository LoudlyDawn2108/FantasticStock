using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FantasticStock.Views.Financial
{
    public partial class AllocatePaymentForm : Form
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
            this.lblPaymentInfo = new System.Windows.Forms.Label();
            this.lblAvailableAmount = new System.Windows.Forms.Label();
            this.dgvInvoices = new System.Windows.Forms.DataGridView();
            this.btnAllocate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInvoices)).BeginInit();
            this.SuspendLayout();
            // 
            // lblPaymentInfo
            // 
            this.lblPaymentInfo.AutoSize = true;
            this.lblPaymentInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblPaymentInfo.Location = new System.Drawing.Point(20, 20);
            this.lblPaymentInfo.Name = "lblPaymentInfo";
            this.lblPaymentInfo.Size = new System.Drawing.Size(247, 13);
            this.lblPaymentInfo.TabIndex = 0;
            this.lblPaymentInfo.Text = "Payment information will be displayed here";
            // 
            // lblAvailableAmount
            // 
            this.lblAvailableAmount.AutoSize = true;
            this.lblAvailableAmount.Location = new System.Drawing.Point(20, 50);
            this.lblAvailableAmount.Name = "lblAvailableAmount";
            this.lblAvailableAmount.Size = new System.Drawing.Size(122, 13);
            this.lblAvailableAmount.TabIndex = 1;
            this.lblAvailableAmount.Text = "Available Amount: $0.00";
            // 
            // dgvInvoices
            // 
            this.dgvInvoices.AllowUserToAddRows = false;
            this.dgvInvoices.AllowUserToDeleteRows = false;
            this.dgvInvoices.AllowUserToResizeRows = false;
            this.dgvInvoices.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvInvoices.Location = new System.Drawing.Point(20, 80);
            this.dgvInvoices.MultiSelect = false;
            this.dgvInvoices.Name = "dgvInvoices";
            this.dgvInvoices.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvInvoices.Size = new System.Drawing.Size(603, 367);
            this.dgvInvoices.TabIndex = 2;
            // 
            // btnAllocate
            // 
            this.btnAllocate.Enabled = false;
            this.btnAllocate.Location = new System.Drawing.Point(457, 453);
            this.btnAllocate.Name = "btnAllocate";
            this.btnAllocate.Size = new System.Drawing.Size(80, 30);
            this.btnAllocate.TabIndex = 3;
            this.btnAllocate.Text = "Allocate";
            this.btnAllocate.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(543, 453);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 30);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // AllocatePaymentForm
            // 
            this.AcceptButton = this.btnAllocate;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(635, 495);
            this.Controls.Add(this.lblPaymentInfo);
            this.Controls.Add(this.lblAvailableAmount);
            this.Controls.Add(this.dgvInvoices);
            this.Controls.Add(this.btnAllocate);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AllocatePaymentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Allocate Payment to Invoice";
            ((System.ComponentModel.ISupportInitialize)(this.dgvInvoices)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label lblPaymentInfo;
        private Label lblAvailableAmount;
        private DataGridView dgvInvoices;
        private Button btnAllocate;
        private Button btnCancel;
    }
}