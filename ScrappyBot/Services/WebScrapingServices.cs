using HtmlAgilityPack;
using ScrappyBot.Models;

namespace ScrappyBot.Services
{
    public class WebScrapingServices
    {
        private readonly HttpClient _httpClient;

        public WebScrapingServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Product>> ScrapeProductCardsAsync(string url)
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var pageContents = await response.Content.ReadAsStringAsync();

            var doc = new HtmlDocument();
            doc.LoadHtml(pageContents);

           
            var productContainers = doc.DocumentNode.SelectNodes("//div[contains(@class, 'prdct-cntnr-wrppr')]");

            if (productContainers == null || !productContainers.Any())
            {
               
                return new List<Product>(); 
            }

            var products = new List<Product>();

            foreach (var container in productContainers)
            {
                var productCards = container.SelectNodes(".//div[contains(@class, 'p-card-wrppr with-campaign-view')]");

                if (productCards == null || !productCards.Any())
                {
                    continue;
                }

                foreach (var cardNode in productCards)
                {
                    var product = new Product
                    {
                        Title = cardNode.SelectSingleNode(".//span[contains(@class, 'prdct-desc-cntnr-ttl')]")?.InnerText.Trim() ?? "N/A",
                        ProductUrl = cardNode.SelectSingleNode(".//a")?.GetAttributeValue("href", string.Empty) ?? "N/A",
                        ImageUrl = cardNode.SelectSingleNode(".//img")?.GetAttributeValue("src", string.Empty) ?? "N/A",
                        Rating = cardNode.SelectSingleNode(".//span[@class='rating-score']")?.InnerText.Trim() ?? "N/A",
                        Price = cardNode.SelectSingleNode(".//div[contains(@class, 'prc-box-dscntd')]")?.InnerText.Trim() ??
                                cardNode.SelectSingleNode(".//div[contains(@class, 'prc-box-orgnl')]")?.InnerText.Trim() ?? "N/A",
                        Promotion = cardNode.SelectSingleNode(".//div[contains(@class, 'low-price-in-last-month')]")?.InnerText.Trim() ??
                                    cardNode.SelectSingleNode(".//div[contains(@class, 'product-badge')]")?.InnerText.Trim() ?? "N/A",
                        Favorites = cardNode.SelectSingleNode(".//span[contains(@class, 'focused-text')]")?.InnerText.Replace(" kişi ", "").Trim() ?? "0"
                    };

                    products.Add(product);
                }
            }

            return products;
        }

        public async Task<List<Product>> ScrapeAllProductsAsync(string baseUrl, int maxPages, string filterTitle = null)
        {
            var allProducts = new List<Product>();

            for (int page = 1; page <= maxPages; page++)
            {
                var url = $"{baseUrl}?page={page}";
                var products = await ScrapeProductCardsAsync(url);

                if (products.Count == 0)
                {
                    break; 
                }

                
                if(!string.IsNullOrEmpty(filterTitle))
                {
                    products = products.Where(p => p.Title?.Contains(filterTitle, StringComparison.OrdinalIgnoreCase) == true).ToList();
                }    

                allProducts.AddRange(products);
            }

            return allProducts;
        }

        public async Task<List<SpecificProduct>> ScrapeSpecificProduct(string baseUrl)
        {
            if(!baseUrl.Contains("https://www.trendyol.com" , StringComparison.OrdinalIgnoreCase))
            {
                var trendUrl = "https://www.trendyol.com" + baseUrl;

                var response = await _httpClient.GetAsync(trendUrl);
                response.EnsureSuccessStatusCode();

                var pageContent = await response.Content.ReadAsStringAsync();

                var doc = new HtmlDocument();
                doc.LoadHtml(pageContent);

                var specificProductContainer = doc.DocumentNode.SelectNodes(".//div[contains(@class, 'product-container')]");

                if (specificProductContainer == null || !specificProductContainer.Any())
                {
                    return new List<SpecificProduct>();
                }

                var product = new List<SpecificProduct>();

                foreach (var spec in specificProductContainer)
                {
                    var specificProductWrapper = doc.DocumentNode.SelectNodes(".//div[contains(@class, 'product-detail-wrapper')]");

                    if (specificProductContainer == null || !specificProductContainer.Any())
                    {
                        continue;
                    }

                    foreach (var specProd in specificProductWrapper)
                    {
                        var prods = new SpecificProduct
                        {
                            Title = specProd.SelectSingleNode(".//h1[contains(@class, 'pr-new-br')]")?.InnerText.Trim(),
                            Price = specProd.SelectSingleNode(".//span[contains(@class, 'prc-dsc')]")?.InnerText.Trim(),
                            Link = trendUrl
                        };

                        product.Add(prods);
                    }
                }

                return product;
            }

            else
            {

                var response = await _httpClient.GetAsync(baseUrl);
                response.EnsureSuccessStatusCode();

                var pageContent = await response.Content.ReadAsStringAsync();

                var doc = new HtmlDocument();
                doc.LoadHtml(pageContent);

                var specificProductContainer = doc.DocumentNode.SelectNodes(".//div[contains(@class, 'product-container')]");

                if (specificProductContainer == null || !specificProductContainer.Any())
                {
                    return new List<SpecificProduct>();
                }

                var product = new List<SpecificProduct>();

                foreach (var spec in specificProductContainer)
                {
                    var specificProductWrapper = doc.DocumentNode.SelectNodes(".//div[contains(@class, 'product-detail-wrapper')]");

                    if (specificProductContainer == null || !specificProductContainer.Any())
                    {
                        continue;
                    }

                    foreach (var specProd in specificProductWrapper)
                    {
                        var prods = new SpecificProduct
                        {
                            Title = specProd.SelectSingleNode(".//h1[contains(@class, 'pr-new-br')]")?.InnerText.Trim(),
                            Price = specProd.SelectSingleNode(".//span[contains(@class, 'prc-dsc')]")?.InnerText.Trim(),
                            Link = baseUrl
                        };

                        product.Add(prods);
                    }
                }

                return product;
            } 
        }

    }
}
