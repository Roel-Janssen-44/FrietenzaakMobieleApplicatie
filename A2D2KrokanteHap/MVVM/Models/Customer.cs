using A2D2KrokanteHap.Abstractions;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace A2D2KrokanteHap.MVVM.Models
{
    [Table("Customer")]
    public class Customer : TableData
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Order> Orders { get; set; } = new();

    }
}
