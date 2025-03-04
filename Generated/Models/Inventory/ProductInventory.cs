using System;

namespace AdminDomain.Models.Inventory
{
    public class ProductInventory
    {
        public int InventoryID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string SKU { get; set; }
        public int LocationID { get; set; }
        public string LocationName { get; set; }
        public decimal QuantityOnHand { get; set; }
        public decimal QuantityReserved { get; set; }
        public decimal QuantityAvailable => QuantityOnHand - QuantityReserved;
        public DateTime LastStockTakeDate { get; set; }
        public DateTime LastRestockDate { get; set; }
        public string UnitOfMeasure { get; set; }
        public string BatchNumber { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}