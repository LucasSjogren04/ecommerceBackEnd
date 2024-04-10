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
    public class ProductController(IProductService productService, IProductRepo productRepo) : ControllerBase
    {
        private readonly IProductService _productService = productService;
        private readonly IProductRepo _productRepo = productRepo;

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

        [HttpGet("GetProductbySlug/{slug}")]
        public async Task<ActionResult<Product>> GetProductbySlug(string slug)
        {
            Console.WriteLine("searched for " + slug);
            var product = await _productService.GetProductbySlug(slug);
            Console.WriteLine("returned" + product);
            if (product == null)
            {
                return BadRequest();
            }

            return Ok(product);
        }

        [HttpGet("SearchForProducts/{searchValue?}")]
        public async Task<ActionResult<IEnumerable<SmallProduct>>> SearchForProducts(string searchValue = "")
        {
            Console.WriteLine("Searching");
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
        public async Task<ActionResult> UploadProduct([FromForm] ProductEntry entry)
        {
            string result = await _productService.UploadProduct(entry);
            if (result == "Data inserted successfully")
            {
                return StatusCode(200, result);
            }
            return BadRequest(result);
        }


        [HttpDelete("DeleteProduct{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            string result = await _productService.DeleteProduct(id);
            if(result != "Product data deleted" && result != "Data was deleted but the picture was not found and thus not deleted")
            {
                return StatusCode(403, result);
            }
            if(result == "Data was deleted but the picture was not found and thus not deleted")
            {
                return StatusCode(300, result);
            }
            return Ok(result);
        }

        [HttpGet("GetListOfAllProducts")]
        public async Task<ActionResult<IEnumerable<Product>>> GetListOfAllProducts()
        {
            var products = await _productRepo.GetListOfAllProducts();
            return Ok(products);
        }

        [HttpPut("UpdateProduct")]
        public async Task<ActionResult> UpdateProduct([FromForm] FullProduct fullProduct)
        {
            Console.WriteLine("Updating");
            string result = await _productService.UpdateProduct(fullProduct);
            if(result == "The product marked for updating was not found" || result == "Product name not unique")
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("checku/{productname}")]
        public async Task<ActionResult<string>> CheckU(string productname)
        {
            string result = await _productRepo.CheckForProductNameUniqueness(productname);
            if(result == null || result == "")
            {
                return Ok("that is unique");
            }
            return Ok("That is not unique");
        }
    }
}
