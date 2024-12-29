
namespace A2D2KrokanteHap
{
    public class ProductsResponse
    {
        public class Rating
        {
            public double rate { get; set; }
            public int count { get; set; }
        }

        public class Root
        {
            public int id { get; set; }
            public string title { get; set; }
            public double price { get; set; }
            public string description { get; set; }
            public string category { get; set; }
            public string image { get; set; }
            public Rating rating { get; set; }
        }
    }
}
