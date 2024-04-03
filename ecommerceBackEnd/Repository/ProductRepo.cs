﻿using Azure.Storage.Blobs;
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
                string? productName = await db.QueryFirstOrDefaultAsync<string>("CheckForProductNameUniqueness", dynamicParameters, commandType: CommandType.StoredProcedure);
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

                // Open a stream to the file content
                using (Stream stream = formFile.OpenReadStream())
                {
                    // Upload the file to blob storage
                    await blobClient.UploadAsync(stream, true);
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
                await db.ExecuteAsync("UploadProduct", dynamicParameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
