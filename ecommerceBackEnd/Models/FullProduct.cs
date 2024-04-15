namespace ecommerceBackEnd.Models
{
    public class FullProduct
    {
        public int ProductId { get; set; }
        public required string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string? ProductDescription { get; set; }
        public IFormFile? Picture { get; set; }
        public string? ProductURLSlug { get; set; }
        public string? SKU { get; set; }
    }
}
