﻿using ecommerceBackEnd.Models;

namespace ecommerceBackEnd.Service
{
    public interface IProductService
    {
        public Task<IEnumerable<SmallProduct>> SearchForProducts(string searchValue);
        public Task<IEnumerable<SmallProduct>> GetHomePageProducts();
        public Task<Product> GetProduct(int id);
        public Task<string> UploadProduct(ProductEntry entry);
        public Task<string> DeleteProduct(int id, string fileName);
    }
}
