using A2D2KrokanteHap.MVVM.Models;
using System.Collections.ObjectModel;

namespace A2D2KrokanteHap
{
    public partial class AddProductModal : ContentPage
    {
        // Define a delegate (callback) to pass back the selected product
        private Action<Product> OnProductSelectedCallback;

        public AddProductModal(Action<Product> onProductSelectedCallback)
        {
            InitializeComponent();  // This line should work if everything is set up correctly

            BindingContext = this;
            ProductListView.ItemsSource = GetProductList();

            // Store the callback method to call later
            OnProductSelectedCallback = onProductSelectedCallback;
        }

        private List<Product> GetProductList()
        {

            try
            {
                // Fetch products from the local database using the repository
                var productsFromDb = App.ProductRepo.GetEntities(); // Replace hardcoded values
                Console.WriteLine(productsFromDb);
                // Return the fetched products
                return productsFromDb ?? new List<Product>(); // Ensure no null values
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading products: {ex.Message}");

                // Return an empty list if there's an error
                return new List<Product>();
            }
            //return new List<Product>
            //{
            //    new Product { Id = 1, Name = "Kleine friet", Price = 3.00, Image = "friet.jpg" },
            //    new Product { Id = 2, Name = "Grote friet", Price = 3.50, Image = "friet.jpg" },
            //    new Product { Id = 3, Name = "Frikandel", Price = 2.75, Image = "snacks.jpg" }
            //};
        }

        private async void CancelButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void OnProductSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedProduct = e.SelectedItem as Product;
            if (selectedProduct != null)
            {
                OnProductSelectedCallback?.Invoke(selectedProduct);

                // Close the modal
                await Navigation.PopModalAsync();
            }
        }
    }
}
