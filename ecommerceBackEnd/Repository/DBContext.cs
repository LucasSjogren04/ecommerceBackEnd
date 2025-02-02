﻿using ecommerceBackEnd.Repository.Interfaces;
using Microsoft.Data.SqlClient;

namespace ecommerceBackEnd.Repository
{
    public class DBContext(IConfiguration configuration) : IDBContext
    {
        private readonly string? _configuration = configuration.GetConnectionString("DefaultConnection");
        public SqlConnection GetConnection()
        {
            return new SqlConnection(_configuration);
        }
        private readonly string? _blobConfiguration = configuration.GetConnectionString("BlobConnection");
        public string GetBlobConnection()
        {
            return new string(_blobConfiguration);
        }
    }
}
