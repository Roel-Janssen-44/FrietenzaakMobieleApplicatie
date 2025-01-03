
using A2D2KrokanteHap.Abstractions;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.ObjectModel;

namespace A2D2KrokanteHap.MVVM.Models
{
    [Table("Order")]
    public class Order : TableData
    {

        public DateTime DateTime { get; set; }
        
        public DateTime EstimatedCompletionTime { get; set; }
        
        public bool Completed { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public ObservableCollection<OrderLine> OrderLines { get; set; } = new();

        [ForeignKey(typeof(Customer))]
        public int? CustomerId { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public Customer? Customer { get; set; }

        [Ignore]
        public double TotalPrice
        {
            get
            {
                return OrderLines?.Where(ol => ol?.Product != null)
                                  .Sum(ol => ol.Product.Price * ol.Amount) ?? 0;
            }
        }

    }
}
