using System;

namespace FantasticStock.Models.Sales
{
    public class Product
    {
        public int ProductID { get; set; }
        public string SKU { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal CostPrice { get; set; }
        public bool IsActive { get; set; }
        public string Barcode { get; set; }
        public int UnitsInStock { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public Product Clone()
        {
            return new Product
            {
                ProductID = this.ProductID,
                SKU = this.SKU,
                ProductName = this.ProductName,
                Description = this.Description,
                CategoryID = this.CategoryID,
                CategoryName = this.CategoryName,
                SellingPrice = this.SellingPrice,
                CostPrice = this.CostPrice,
                IsActive = this.IsActive,
                Barcode = this.Barcode,
                UnitsInStock = this.UnitsInStock,
                CreatedDate = this.CreatedDate,
                ModifiedDate = this.ModifiedDate
            };
        }
    }
}
