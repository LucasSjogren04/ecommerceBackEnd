using Dapper;
using ecommerceBackEnd.Models;
using ecommerceBackEnd.Repository.Interfaces;
using System.Buffers;
using System.Collections.Generic;
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
                string productName = await db.QueryFirstAsync<string>("CheckForProductNameUniqueness", dynamicParameters, commandType: CommandType.StoredProcedure);
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
                // Generate a unique name for the image
                var imageName = $"{Guid.NewGuid()}{Path.GetExtension(formFile.FileName)}";

                // Upload image to Azure Blob Storage
                var blob = _blobContainer.GetBlockBlobReference(imageName);
                await blob.UploadFromStreamAsync(formFile.OpenReadStream());

                // Return the URL of the uploaded image
                var imageUrl = blob.Uri.ToString();
                return Ok(new { imageUrl });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
