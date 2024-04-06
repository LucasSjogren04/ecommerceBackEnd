using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Dapper;
using ecommerceBackEnd.Models;
using ecommerceBackEnd.Repository.Interfaces;
using System.Buffers;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;

namespace ecommerceBackEnd.Repository
{
    public class ProductRepo(IDBContext dBContext) : IProductRepo
    {
        private readonly IDBContext _dBContext = dBContext;
        //Used for getting a specific product by id
        public async Task<Product> GetProduct(int id)
        {
            try
            {
                using IDbConnection db = _dBContext.GetConnection();
                DynamicParameters dynamicParameters = new();
                dynamicParameters.Add("@Id", id);
                Product? product = await db.QueryFirstOrDefaultAsync<Product>("GetProduct", dynamicParameters, commandType: CommandType.StoredProcedure);
                return product;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //used for home page
        public async Task<IEnumerable<SmallProduct>> GetHomePageProducts()
        {
            try
            {
                using IDbConnection db = _dBContext.GetConnection();
                IEnumerable<SmallProduct> product = await db.QueryAsync<SmallProduct>("GetHomePageProducts", commandType: CommandType.StoredProcedure);
                return product;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //used for searching for products
        public async Task<IEnumerable<SmallProduct>> SearchForProducts(string searchValue)
        {
            try
            {
                using IDbConnection db = _dBContext.GetConnection();
                DynamicParameters dynamicParameters = new();
                dynamicParameters.Add("@SearchValue", searchValue);
                IEnumerable<SmallProduct> products = await db.QueryAsync<SmallProduct>("SearchForProducts", dynamicParameters, commandType: CommandType.StoredProcedure);
                return products;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> CheckForProductNameUniqueness(string name)
        {
            try
            {
                using IDbConnection db = _dBContext.GetConnection();
                DynamicParameters dynamicParameters = new();
                dynamicParameters.Add("@name", name);
                string? product = await db.QueryFirstOrDefaultAsync<string>("CheckForProductNameUniqueness", dynamicParameters, commandType: CommandType.StoredProcedure);
                return product;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string> CheckForProductNameUniquenessOnUpdate(string productName, int productId)
        {
            try
            {
                using IDbConnection db = _dBContext.GetConnection();
                DynamicParameters dynamicParameters = new();
                dynamicParameters.Add("@ProductName", productName);
                dynamicParameters.Add("@ProductId", productId);
                string? product = await db.QueryFirstOrDefaultAsync<string>("CheckForProductNameUniquenessOnUpdate", dynamicParameters, commandType: CommandType.StoredProcedure);
                return product;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> CheckForProductURLSlugUniqueness(string slug)
        {
            try
            {
                using IDbConnection db = _dBContext.GetConnection();
                DynamicParameters dynamicParameters = new();
                dynamicParameters.Add("@slug", slug);
                string? product = await db.QueryFirstOrDefaultAsync<string>("CheckForProductURLSlugUniqueness", dynamicParameters, commandType: CommandType.StoredProcedure);
                return product;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> CheckForProductURLSlugUniquenessOnUpdate(string slug, int productId)
        {
            try
            {
                using IDbConnection db = _dBContext.GetConnection();
                DynamicParameters dynamicParameters = new();
                dynamicParameters.Add("@slug", slug);
                dynamicParameters.Add("@ProductId", productId);
                string? product = await db.QueryFirstOrDefaultAsync<string>("CheckForProductURLSlugUniquenessOnUpdate", dynamicParameters, commandType: CommandType.StoredProcedure);
                return product;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UploadProduct(ProductEntry entry)
        {
            try
            {
                using IDbConnection db = _dBContext.GetConnection();
                DynamicParameters dynamicParameters = new();
                dynamicParameters.Add("@ProductName", entry.ProductName);
                dynamicParameters.Add("@ProductPrice", entry.ProductPrice);
                dynamicParameters.Add("@ProductDescription", entry.ProductDescription);
                dynamicParameters.Add("@ProductPictureURL", entry.Picture.FileName);
                dynamicParameters.Add("@ProductURLSlug", entry.ProductURLSlug);
                await db.ExecuteAsync("UploadProduct", dynamicParameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task UpdateProduct(FullProduct fullProduct)
        {
            try
            {
                using IDbConnection db = _dBContext.GetConnection();
                DynamicParameters dynamicParameters = new();
                dynamicParameters.Add("@ProductId", fullProduct.ProductId);
                dynamicParameters.Add("@ProductName", fullProduct.ProductName);
                dynamicParameters.Add("@ProductPrice", fullProduct.ProductPrice);
                dynamicParameters.Add("@ProductDescription", fullProduct.ProductDescription);
                dynamicParameters.Add("@ProductURLSlug", fullProduct.ProductURLSlug);
                await db.ExecuteAsync("UpdateProduct", dynamicParameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateProductWithPicture(FullProduct fullProduct)
        {
            try
            {
                using IDbConnection db = _dBContext.GetConnection();
                DynamicParameters dynamicParameters = new();
                dynamicParameters.Add("@ProductId", fullProduct.ProductId);
                dynamicParameters.Add("@ProductName", fullProduct.ProductName);
                dynamicParameters.Add("@ProductPrice", fullProduct.ProductPrice);
                dynamicParameters.Add("@ProductDescription", fullProduct.ProductDescription);
                dynamicParameters.Add("@ProductPictureURL", fullProduct.Picture.FileName);
                dynamicParameters.Add("@ProductURLSlug", fullProduct.ProductURLSlug);
                await db.ExecuteAsync("UpdateProductWithPicture", dynamicParameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task DeleteProduct(int id)
        {
            try
            {
                using IDbConnection db = _dBContext.GetConnection();
                DynamicParameters dynamicParameters = new();
                dynamicParameters.Add("@Id", id);
                await db.ExecuteAsync("DeleteProduct", dynamicParameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<IEnumerable<Product>> GetListOfAllProducts()
        {
            try
            {
                using IDbConnection db = _dBContext.GetConnection();
                IEnumerable<Product> productName = await db.QueryAsync<Product>("GetListOfAllProducts", commandType: CommandType.StoredProcedure);
                return productName;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string> UploadPicture(IFormFile formFile)
        {
            try
            {
                string connectionString = _dBContext.GetBlobConnection();
                string containerName = "pictures";

                // Create a BlobServiceClient object which will be used to create a container client
                BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

                // Create the container and return a container client object
                BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

                // Get a reference to a blob
                string fileName = Path.GetFileName(formFile.FileName);
                BlobClient blobClient = containerClient.GetBlobClient(fileName);

                // Set the content type
                var blobHttpHeaders = new BlobHttpHeaders
                {
                    ContentType = formFile.ContentType // Use the content type provided by the formFile
                };

                // Open a stream to the file content
                using (Stream stream = formFile.OpenReadStream())
                {
                    // Upload the file to blob storage with specified content type
                    await blobClient.UploadAsync(stream, new BlobUploadOptions { HttpHeaders = blobHttpHeaders });
                }

                // Return the uploaded filename
                return fileName;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;

            }
        }

        
        public async Task<bool> DeletePicture(string fileName)
        {
            try
            {
                string connectionString = _dBContext.GetBlobConnection();
                string containerName = "pictures";

                // Create a BlobServiceClient object which will be used to create a container client
                BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

                // Create the container and return a container client object
                BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

                // Get a reference to the blob to be deleted
                BlobClient blobClient = containerClient.GetBlobClient(fileName);

                // Delete the blob
                return await blobClient.DeleteIfExistsAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting the picture: {ex.Message}");
                throw;
            }
        }
    }
}
