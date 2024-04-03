namespace ecommerceBackEnd.Models
{
    public class ProductEntry
    {
        public string? ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string? ProductDescription { get; set; }
        public IFormFile? Picture { get; set; }
    }
}
