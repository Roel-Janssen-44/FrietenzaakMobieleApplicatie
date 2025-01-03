
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

        //public ICollection<OrderLine>? OrderLines { get; set; }
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        //public List<OrderLine> OrderLines { get; set; } = new();
        public ObservableCollection<OrderLine> OrderLines { get; set; } // Use ObservableCollection here



        [ForeignKey(typeof(Customer))]
        public int? CustomerId { get; set; }
        [OneToOne]
        public Customer? Customer { get; set; }

        public double TotalPrice
        {
            get
            {
                return OrderLines.Sum(ol => ol.Product.Price * ol.Amount);
            }
        }

    }
}
