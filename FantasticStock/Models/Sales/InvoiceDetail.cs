using System;

namespace FantasticStock.Models.Sales
{
    public class InvoiceDetail
    {
        public int InvoiceDetailID { get; set; }
        public int InvoiceID { get; set; }
        public string InvoiceNumber { get; set; }
        public int? OrderDetailID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string SKU { get; set; }
        public string Description { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal LineTotal => Quantity * UnitPrice * (1 - (DiscountPercent / 100));
        public decimal TaxPercent { get; set; }
        public decimal TaxAmount => LineTotal * (TaxPercent / 100);
    }
}