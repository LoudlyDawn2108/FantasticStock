namespace FantasticStock.Models.Sales
{
    public class SaleItem
    {
        public int ItemID { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal LineTotal { get; set; }
        public object Tag { get; set; }
        public object ItemInfo { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public SaleItem Clone()
        {
            return new SaleItem
            {
                ItemID = this.ItemID,
                ProductID = this.ProductID,
                ProductName = this.ProductName,
                Quantity = this.Quantity,
                UnitPrice = this.UnitPrice,
                DiscountPercent = this.DiscountPercent,
                DiscountAmount = this.DiscountAmount

            };
        }
    }
}
