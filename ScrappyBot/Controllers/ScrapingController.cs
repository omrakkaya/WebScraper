using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HtmlAgilityPack;
using System.Globalization;
using ScrappyBot.Services;
using ScrappyBot.Models;
using ScrappyBot.Context;
using Newtonsoft.Json;

namespace ScrappyBot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScrapingController : ControllerBase
    {
        private readonly WebScrapingServices _webScrapingServices;
        

        public ScrapingController(WebScrapingServices webScrapingServices)
        {
            _webScrapingServices = webScrapingServices;
        }

        [HttpGet("products")]
        public async Task<IActionResult> TrendyolGetAllProductsAsync([FromQuery] string baseUrl, [FromQuery] int maxPages = 1, [FromQuery] string filterTitle = null)
        {
            if (string.IsNullOrEmpty(baseUrl))
            {
                return BadRequest("Base URL is required");
            }

            try
            {
                var products = await _webScrapingServices.ScrapeAllProductsAsync(baseUrl, maxPages, filterTitle);
                return Ok(products);
            }
            catch (HttpRequestException e)
            {
                return BadRequest($"Error fetching URL: {e.Message}");
            }
        }

        [HttpGet("oneprod")]
        public async Task<IActionResult> TrendyolGetSpecificProductAsync([FromQuery] string baseUrl)
        {
            if (string.IsNullOrEmpty(baseUrl))
            {
                return BadRequest("Base URL is required");
            }

            try
            {
                var specificProd = await _webScrapingServices.ScrapeSpecificProduct(baseUrl);

                HttpContext.Session.SetString("Data", JsonConvert.SerializeObject(specificProd));

                return Ok(specificProd);
            }
            catch (HttpRequestException ex)
            {
                return BadRequest($"Error fetching URL: {ex.Message}");
            }
        }

        
    }
}
