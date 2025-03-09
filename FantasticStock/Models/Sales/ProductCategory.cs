using System;

namespace FantasticStock.Models.Sales
{
    public class ProductCategory
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        
        public override string ToString()
        {
            return CategoryName;
        }
    }
}
