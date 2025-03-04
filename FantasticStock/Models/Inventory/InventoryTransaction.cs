using System;

namespace FantasticStock.Models.Inventory
{
    public class InventoryTransaction
    {
        public int TransactionID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string SKU { get; set; }
        public int LocationID { get; set; }
        public string LocationName { get; set; }
        public string TransactionType { get; set; } // Purchase, Sale, Adjustment, Transfer, Stock Take
        public decimal Quantity { get; set; }
        public decimal PreviousQuantity { get; set; }
        public decimal NewQuantity { get; set; }
        public int? ReferenceID { get; set; }
        public string ReferenceType { get; set; } // PO, SO, StockAdjustment, Transfer, etc.
        public string ReferenceNumber { get; set; }
        public string Notes { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public DateTime CreatedDate { get; set; }
        
        public string FormattedDate => CreatedDate.ToString("yyyy-MM-dd HH:mm:ss");
    }
}