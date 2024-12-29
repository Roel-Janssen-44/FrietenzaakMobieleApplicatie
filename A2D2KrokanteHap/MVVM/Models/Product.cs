
using SQLite;
using A2D2KrokanteHap.Abstractions;

namespace A2D2KrokanteHap.MVVM.Models
{
    [Table("Students")]
    public class Product : TableData
    {

        [Column("Name"), Indexed, NotNull]
        public string? Name { get; set; }

        public string? Category { get; set; }

        public double Price { get; set; }

        public string FormattedPrice
        {
            get
            {
                if (Price % 1 == 0)
                    return $"€ {Price:0},-";
                else
                    return $"€ {Price:0.00}".Replace(".", ",");
            }
        }

        public string? Image { get; set; }

        public static string GenerateUrlProducts()
        {
            return Constants.API_BASE_URL;
        }
    }
}
