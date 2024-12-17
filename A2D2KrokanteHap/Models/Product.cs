using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Frietzaak.Server.Models
{
    public class Product
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public double Price { get; set; }

        [AllowNull]
        public double? DiscountPrice { get; set; }

        [AllowNull]
        public string? Image { get; set; }
    }
}
