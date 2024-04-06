using ecommerceBackEnd.Models;

namespace ecommerceBackEnd.Repository.Interfaces
{
    public interface IProductRepo
    {
        //Returns an enum of Products without the description
        public Task<IEnumerable<SmallProduct>> SearchForProducts(string searchValue);
        public Task<IEnumerable<SmallProduct>> GetHomePageProducts();
        public Task<Product> GetProduct(int id);
        public Task<IEnumerable<Product>> GetListOfAllProducts();
        public Task<string> CheckForProductNameUniqueness(string name);
        public Task<string> CheckForProductURLSlugUniqueness(string name);
        public Task<string> CheckForProductNameUniquenessOnUpdate(string productName, int productId);
        public Task<string> CheckForProductURLSlugUniquenessOnUpdate(string productName, int productId);
        public Task<string> UploadPicture(IFormFile formFile);
        public Task UploadProduct(ProductEntry entry);
        public Task DeleteProduct(int id);
        public Task<bool> DeletePicture(string fileName);
        public Task UpdateProduct(FullProduct fullProduct);
        public Task UpdateProductWithPicture(FullProduct fullProduct);
    }
}
