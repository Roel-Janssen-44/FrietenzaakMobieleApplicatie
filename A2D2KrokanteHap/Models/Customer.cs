using System.ComponentModel.DataAnnotations;

namespace Frietzaak.Server.Models
{
    public class Customer
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Phonenumber { get; set; }

        [Required]
        public string Streetname { get; set; }

        [Required]
        public string Housenumber { get; set; }

        [Required]
        public string Zipcode { get; set; }

        [Required]
        public ICollection<Order>? Orders { get; set; }
    }
}
