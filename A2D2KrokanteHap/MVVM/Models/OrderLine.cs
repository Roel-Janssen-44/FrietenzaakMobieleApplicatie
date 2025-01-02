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
        private int _amount;

        [ForeignKey(typeof(Product))]
        public int ProductId { get; set; }

        [OneToOne]
        public Product? Product { get; set; }

        // Amount property now triggers PropertyChanged event when updated
        public int Amount
        {
            get => _amount;
            set
            {
                if (_amount != value)
                {
                    _amount = value;
                    OnPropertyChanged(nameof(Amount)); // Notify the UI about the change
                }
            }
        }

        // Event to notify property changes
        public event PropertyChangedEventHandler PropertyChanged;

        // Method to raise PropertyChanged event
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
