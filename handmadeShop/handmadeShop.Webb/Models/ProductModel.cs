namespace handmadeShop.Web.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string PathImage { get; set; }
        public int Quantity { get; set; }

    }
}