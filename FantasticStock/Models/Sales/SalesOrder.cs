using System;
using System.Collections.Generic;

namespace AdminDomain.Models.Sales
{
    public class SalesOrder
    {
        public int OrderID { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string Status { get; set; } // Draft, Confirmed, Shipped, Delivered, Invoiced, Cancelled
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public int? ShipperID { get; set; }
        public string ShipperName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipState { get; set; }
        public string ShipPostalCode { get; set; }
        public string ShipCountry { get; set; }
        public int SalesPersonID { get; set; }
        public string SalesPersonName { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentTerms { get; set; }
        public decimal Subtotal { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal ShippingAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalAmount => Subtotal + TaxAmount + ShippingAmount - DiscountAmount;
        public string Notes { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        
        public List<SalesOrderDetail> OrderItems { get; set; } = new List<SalesOrderDetail>();
    }
}