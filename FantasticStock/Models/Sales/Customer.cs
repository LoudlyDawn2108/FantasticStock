using System;

namespace FantasticStock.Models.Sales
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int CustomerTypeID { get; set; }
        public string CustomerTypeName { get; set; }
        public decimal CreditLimit { get; set; }
        public decimal Balance { get; set; }
        public string PaymentTerms { get; set; }
        public string TaxID { get; set; }
        public bool IsActive { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}