namespace ecommerceBackEnd.Models
{
    public class SmallProduct
    {
        public int ProductId { get; set; }
        public required string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string? ProductPictureURL { get; set; }
        public string? ProductURLSlug { get; set; }
    }
}
