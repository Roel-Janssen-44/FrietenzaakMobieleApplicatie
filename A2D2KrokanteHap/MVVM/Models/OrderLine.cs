using A2D2KrokanteHap.Abstractions;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace A2D2KrokanteHap.MVVM.Models
{
    [Table("OrderLine")]
    public class OrderLine : TableData, INotifyPropertyChanged
    {

        [ForeignKey(typeof(Order))]
        public int OrderId { get; set; }


        private int _amount;

        [ForeignKey(typeof(Product))]
        public int ProductId { get; set; }

        [OneToOne]
        public Product? Product { get; set; }

        public int Amount
        {
            get => _amount;
            set
            {
                if (_amount != value)
                {
                    _amount = value;
                    OnPropertyChanged(nameof(Amount)); 
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
