﻿using ecommerceBackEnd.Models;

namespace ecommerceBackEnd.Repository.Interfaces
{
    public interface IProductRepo
    {
        //Returns an enum of Products without the description
        public Task<IEnumerable<SmallProduct>> SearchForProducts(string searchValue);
        public Task<IEnumerable<SmallProduct>> GetHomePageProducts();
        public Task<Product> GetProduct(int id);
        public Task<string> CheckForProductNameUniqueness(string name);
        public Task<string> UploadPicture(IFormFile formFile);
        public Task UploadProduct(ProductEntry entry);
    }
}
