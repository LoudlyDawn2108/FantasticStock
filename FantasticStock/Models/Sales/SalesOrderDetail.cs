using System;

namespace AdminDomain.Models.Sales
{
    public class SalesOrderDetail
    {
        public int OrderDetailID { get; set; }
        public int OrderID { get; set; }
        public string OrderNumber { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string SKU { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal LineTotal => Quantity * UnitPrice * (1 - (DiscountPercent / 100));
        public decimal TaxPercent { get; set; }
        public decimal TaxAmount => LineTotal * (TaxPercent / 100);
        public string Notes { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}