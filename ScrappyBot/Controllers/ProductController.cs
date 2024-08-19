using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ScrappyBot.Context;
using ScrappyBot.Models;
using ScrappyBot.Models.Dto;

namespace ScrappyBot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private ResponseDto _response;

        public ProductController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _response = new ResponseDto();
        }

        [HttpPost("addProduct")]
        public async Task<IActionResult> AddProduct([FromBody] List<SpecificProduct> products)
        {
            var productData = HttpContext.Session.GetString("Data");
            if (string.IsNullOrEmpty(productData))
            {
                return BadRequest("No product data found in session.");
            }

            
            var specificProducts = JsonConvert.DeserializeObject<List<SpecificProduct>>(productData);

            if (specificProducts == null || !specificProducts.Any())
            {
                return BadRequest("Invalid or empty product data.");
            }

            
            foreach (var product in specificProducts)
            {
                var productEntity = new SpecificProduct
                {
                    Id = product.Id,
                    Title = product.Title,
                    Price = product.Price,
                    Link = product.Link
                };

                _appDbContext.SpecificProduct.Add(productEntity);
            }

           
            await _appDbContext.SaveChangesAsync();

            return Ok("Products saved successfully.");
        }

        [HttpGet]
        public ResponseDto GetProducts()
        {
            try
            {
                IEnumerable<SpecificProduct> objList = _appDbContext.SpecificProduct.ToList();
                _response.Item = objList;
            }
            catch (Exception ex)
            { 
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }
            return _response;
        }

        [HttpDelete]
        public ResponseDto DeleteProduct(Guid id)
        {
            try
            {
                SpecificProduct specificProduct = _appDbContext.SpecificProduct.First(x => x.Id == id);
                _appDbContext.Remove(specificProduct);
                _appDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                _response.IsSuccess= false;
                _response.Message= ex.Message;
            }
            return _response;
        }
    }
}
