using System;

namespace FantasticStock.Models.Financial
{
    public class Payment
    {
        public int PaymentID { get; set; }
        public string PaymentNumber { get; set; }
        public DateTime PaymentDate { get; set; }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public int? InvoiceID { get; set; }
        public string InvoiceNumber { get; set; }
        public string PaymentMethod { get; set; } // Cash, Check, Credit Card, Bank Transfer
        public string ReferenceNumber { get; set; } // Check number, transaction ID, etc.
        public decimal Amount { get; set; }
        public string Notes { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public DateTime CreatedDate { get; set; }
        
        public string FormattedDate => PaymentDate.ToString("yyyy-MM-dd HH:mm:ss");
    }
    public class OutstandingInvoice
    {
        public int InvoiceID { get; set; }
        public string InvoiceNumber { get; set; }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Amount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal AmountDue { get; set; }
        public int DaysOverdue { get; set; }
    }
}