using A2D2KrokanteHap.Logic;
using A2D2KrokanteHap.MVVM.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace A2D2KrokanteHap.MVVM.Views
{
    public partial class AddProductModal : ContentPage
    {
        // Define a delegate (callback) to pass back the selected product
        private Action<Product> OnProductSelectedCallback;

        public AddProductModal(Action<Product> onProductSelectedCallback)
        {
            InitializeComponent();

            BindingContext = this;
            OnProductSelectedCallback = onProductSelectedCallback;

            // Load products asynchronously
            LoadProductsAsync();
        }

        private async void LoadProductsAsync()
        {
            ProductListView.ItemsSource = await GetProductList();
        }

        private async Task<List<Product>> GetProductList()
        {
            try
            {
                var productsFromAPI = await ProductLogic.GetProducts();
                return productsFromAPI ?? new List<Product>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading products: {ex.Message}");
                return new List<Product>();
            }
        }

        private async void CancelButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        // When an item is selected in the ListView, the callback is invoked to pass the selected product
        private async void OnProductSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedProduct = e.SelectedItem as Product;
            if (selectedProduct != null)
            {
                OnProductSelectedCallback?.Invoke(selectedProduct);

                // Close the modal after selection
                await Navigation.PopModalAsync();
            }
        }
    }
}
