using System;

namespace FantasticStock.Models.Financial
{
    public class Expense
    {
        public int ExpenseID { get; set; }
        public string ExpenseNumber { get; set; }
        public DateTime ExpenseDate { get; set; }
        public int? SupplierID { get; set; }
        public string SupplierName { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string PaymentMethod { get; set; }
        public string ReferenceNumber { get; set; }
        public decimal Amount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount => Amount + TaxAmount;
        public string Notes { get; set; }
        public bool IsTaxDeductible { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public DateTime CreatedDate { get; set; }
        
        public string FormattedDate => ExpenseDate.ToString("yyyy-MM-dd HH:mm:ss");
    }
}