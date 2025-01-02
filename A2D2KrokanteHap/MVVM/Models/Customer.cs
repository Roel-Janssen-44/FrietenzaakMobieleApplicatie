using SQLite;
using SQLiteNetExtensions.Attributes;
using System.ComponentModel.DataAnnotations;

namespace A2D2KrokanteHap.MVVM.Models
{
    [Table("Customer")]
    public class Customer : Abstractions.TableData
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }


        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Order> Orders { get; set; } = new(); // Initialized to avoid null issues

        //public ICollection<Order>? Orders { get; set; }
    }
}
