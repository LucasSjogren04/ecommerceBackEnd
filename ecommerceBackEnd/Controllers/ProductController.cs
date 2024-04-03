using ecommerceBackEnd.Models;
using ecommerceBackEnd.Repository.Interfaces;
using ecommerceBackEnd.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Buffers;

namespace ecommerceBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService productService) : ControllerBase
    {
        private readonly IProductService _productService = productService;
        
        [HttpGet("GetProduct/{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productService.GetProduct(id);
            if (product == null)
            {
                return BadRequest();
            }
            return Ok(product);
        }
        [HttpGet("SearchForProducts/{searchValue}")]
        public async Task<ActionResult<IEnumerable<SmallProduct>>> SearchForProducts(string searchValue)
        {
            var products = await _productService.SearchForProducts(searchValue);
            if (products == null)
            {
                return BadRequest();
            }
            return Ok(products);
        }

        [HttpGet("GetHomePageProducts")]
        public async Task<ActionResult<IEnumerable<SmallProduct>>> GetHomePageProducts()
        {
            var products = await _productService.GetHomePageProducts();
            if (products == null)
            {
                return BadRequest();
            }
            return Ok(products);
        }

        [HttpPost("UploadProduct")]
        public async Task<ActionResult> UploadProduct(ProductEntry entry)
        {
            var result = _productService.
        }
    }
}
