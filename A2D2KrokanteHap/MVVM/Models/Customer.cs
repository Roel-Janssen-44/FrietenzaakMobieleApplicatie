using SQLite;
using System.ComponentModel.DataAnnotations;

namespace A2D2KrokanteHap.MVVM.Models
{
    [Table("Customer")]
    public class Customer : Abstractions.TableData
    {
        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Phonenumber { get; set; }

        public string? Streetname { get; set; }

        public string? Housenumber { get; set; }
        
        public string? Zipcode { get; set; }

        public ICollection<Order>? Orders { get; set; }
    }
}
