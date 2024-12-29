
using A2D2KrokanteHap.MVVM.Models;
using Newtonsoft.Json;

namespace A2D2KrokanteHap.Logic
{
    public class ProductLogic
    {
        public async static Task<List<Product>> GetProducts()
        {
            List<Product> products = new List<Product>();
            var url = Product.GenerateUrlProducts();

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();

                // Deserialize directly into a list of Product
                products = JsonConvert.DeserializeObject<List<Product>>(json);

                Console.WriteLine(products);
            }

            return products;
        }

    }
}
