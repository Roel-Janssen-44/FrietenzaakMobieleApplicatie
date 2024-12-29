using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace A2D2KrokanteHap.MVVM.Models
{
    public class Order
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public DateTime EstimatedCompletionTime { get; set; }

        [Required]
        public bool Completed { get; set; }

        [Required]
        public ICollection<OrderLine>? OrderLines { get; set; }

        [AllowNull]
        public int? CustomerId { get; set; }

        [AllowNull]
        public virtual Customer? Customer { get; set; }

        public double TotalPrice
        {
            get
            {
                return OrderLines.Sum(ol => ol.Product.Price * ol.Amount);
            }
        }


        public double CalculateTotalAmount(Order order)
        {
            double total = 0;

            foreach (var line in order.OrderLines)
            {
                double price = line.Product.Price;
                total += price * line.Amount;
            }

            return total;
        }
    }

}
