using ecommerceBackEnd.Models;
using ecommerceBackEnd.Repository.Interfaces;
using System.Buffers;

namespace ecommerceBackEnd.Service
{
    public class ProductService(IProductRepo productRepo) : IProductService
    {
        private readonly IProductRepo _productRepo = productRepo;

        public async Task<string> DeleteProduct(int id, string fileName)
        {
            bool fileDeleted =  await _productRepo.DeletePicture(fileName);
            if (!fileDeleted)
            {
                return "File not found";
            }
            await _productRepo.DeleteProduct(id);
            return "Product data deleted";
            
        }

        public async Task<IEnumerable<SmallProduct>> GetHomePageProducts()
        {
            var products = await _productRepo.GetHomePageProducts();
            return products;
        }

        public async Task<Product> GetProduct(int id)
        {
            Product product = await _productRepo.GetProduct(id);
            return product;
        }

        public async Task<IEnumerable<SmallProduct>> SearchForProducts(string searchValue)
        {
            var products = await _productRepo.SearchForProducts(searchValue);
            return products;
        }

        public async Task<string> UpdateProduct(FullProduct fullProduct)
        {
            string exists = await _productRepo.CheckForProductNameUniquenessOnUpdate(fullProduct.ProductName, fullProduct.ProductId);
            if (exists != null)
            {
                return "Product name not unique";
            }

            //Check if user wants to update the picture
            if (fullProduct.Picture != null)
            {
                Product productToUpdate = await _productRepo.GetProduct(fullProduct.ProductId);
                if(productToUpdate == null || productToUpdate.ProductPictureURL == null)
                {
                    return "The product marked for updating was not found";
                }
                //Delete old picture
                await _productRepo.DeletePicture(productToUpdate.ProductPictureURL);
                //Upload new picture
                await _productRepo.UploadPicture(fullProduct.Picture);

                //Update database data
                await _productRepo.UpdateProductWithPicture(fullProduct);
                return "Product updated successfully";
            }
            
            await _productRepo.UpdateProduct(fullProduct);
            return "Product updated successfully without changing the picture";
        }

        public async Task<string> UploadProduct(ProductEntry entry)
        {
            //Checks for valid inputs
            if(entry.ProductName == null || entry.ProductName == "")
            {
                return "Request did not include ProductName";
            }
            if (entry.ProductPrice == 0.0M || entry.ProductPrice >= 1e19M + 1)
            {
                return "Request did not include ProductPrice or it was 0 or it was larger than or equal to 1e19M + 1";
            }
            if (entry.ProductDescription == null || entry.ProductDescription == "")
            {
                return "Request did not include ProductDescription";
            }
            if(entry.Picture == null || entry.Picture.FileName == null || entry.Picture.Length == 0)
            {
                return "Request did not include ProductPicture";
            }
            
            //Sends the entry.ProductName to the database to see it there is an exact match
            //If there is the upload request is cancelled since a productname has to be unique
            string productName = await _productRepo.CheckForProductNameUniqueness(entry.ProductName);
            if(productName != null)
            {
                return "ProductName has to be unique";
            }
            string fileName = await _productRepo.UploadPicture(entry.Picture);

            await _productRepo.UploadProduct(entry);

            return ("Data inserted successfully");

        }
    }
}
