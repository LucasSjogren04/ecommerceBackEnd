﻿namespace ecommerceBackEnd.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string? ProductDescription { get; set; }
        public string? ProductPictureURL { get; set; }
        public string? ProductURLSlug { get; set; }
        public string? SKU { get; set; }
    }
}
