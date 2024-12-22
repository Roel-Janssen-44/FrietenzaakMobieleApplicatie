using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Frietzaak.Server.Models
{
    public class OrderLine : INotifyPropertyChanged
    {
        private int _amount;

        [Required]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        // Amount property now triggers PropertyChanged event when updated
        [Required]
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

        [Required]
        public virtual Product? Product { get; set; }

        // Event to notify property changes
        public event PropertyChangedEventHandler PropertyChanged;

        // Method to raise PropertyChanged event
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
