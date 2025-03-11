using System;
using System.Collections.Generic;

namespace FantasticStock.Models.Inventory
{
    public class Product
    {
        public int ProductID { get; set; }
        public string SKU { get; set; }
        public string Barcode { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int BrandID { get; set; }    // null
        public string BrandName { get; set; } // null
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal WholesalePrice { get; set; } // null
        public decimal ReorderLevel { get; set; }   
        public decimal TargetStockLevel { get; set; }// null
        public string UnitOfMeasure { get; set; }// null
        public bool ManageStock { get; set; }// null
        public bool AllowFractionalQuantity { get; set; }// null
        public byte[] ProductImage { get; set; }
        public bool IsActive { get; set; }// null
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }// null
        public DateTime ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }// null

        public string Status => IsActive ? "Active" : "Inactive";// null
    }
}