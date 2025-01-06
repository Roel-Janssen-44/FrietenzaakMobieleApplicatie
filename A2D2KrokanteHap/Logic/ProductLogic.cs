
using A2D2KrokanteHap.MVVM.Models;
using Newtonsoft.Json;

namespace A2D2KrokanteHap.Logic
{
    public class ProductLogic
    {
        public async static Task<List<Product>> GetProducts()
        {
            List<ProductsResponse.ProductLogicResponse> apiProducts = new List<ProductsResponse.ProductLogicResponse>();
            List<Product> products = new List<Product>();
            var url = Product.GenerateUrlProducts();


            // API werkt, echter de foto's van de api zijn groot waardoor de applicatie op mobiel heel lang nodig heeft om te laden.
            // aarom heb ik ze er een aantal hardcoded producten in gezet

            //using (HttpClient client = new HttpClient())
            //{
            //    var response = await client.GetAsync(url);
            //    var json = await response.Content.ReadAsStringAsync();

            //    apiProducts = JsonConvert.DeserializeObject<List<ProductsResponse.ProductLogicResponse>>(json);

            //    products = apiProducts.ConvertAll(apiProduct => MapToProduct(apiProduct));
            //}

            var hardcodedProducts = new List<Product>
            {
                new Product { Id = 1, Name = "Kleine friet", Price = 3.00, Image = "friet.jpg" },
                new Product { Id = 2, Name = "Grote friet", Price = 3.50, Image = "friet.jpg" },
                new Product { Id = 3, Name = "Frikandel", Price = 2.75, Image = "snacks.jpg" }
            };

            products.InsertRange(0, hardcodedProducts);

            return products;
        }

        private static Product MapToProduct(ProductsResponse.ProductLogicResponse apiProduct)
        {
            return new Product
            {
                Name = apiProduct.title,
                Category = apiProduct.category,
                Price = apiProduct.price,
                Image = apiProduct.image
            };
        }

    }
}
