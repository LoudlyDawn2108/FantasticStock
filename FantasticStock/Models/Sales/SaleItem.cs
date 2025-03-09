namespace FantasticStock.Models.Sales
{
    public class SaleItem
    {
        public int ItemID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal DiscountAmount => UnitPrice * Quantity * (DiscountPercent / 100);
        public decimal LineTotal => (UnitPrice * Quantity) - DiscountAmount;

        public SaleItem Clone()
        {
            return new SaleItem
            {
                ItemID = this.ItemID,
                ProductID = this.ProductID,
                ProductName = this.ProductName,
                Quantity = this.Quantity,
                UnitPrice = this.UnitPrice,
                DiscountPercent = this.DiscountPercent
            };
        }
    }
}
