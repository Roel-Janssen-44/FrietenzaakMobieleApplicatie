

using Bogus;
using PropertyChanged;
using System.Windows.Input;
using A2D2KrokanteHap.MVVM.Models;
using A2D2KrokanteHap.Logic;

namespace A2D2KrokanteHap.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class TestPageViewModel
    {
        public List<Product>? Products { get; set; }
        public Product? CurrentProduct { get; set; }

        public ICommand? AddOrUpdateCommand { get; set; }
        public ICommand? DeleteCommand { get; set; }
        public ICommand? TestCommand { get; set; }
        public ICommand? LogoutCommand { get; set; }

        public TestPageViewModel()
        {
            Refresh();
            GenerateNewProduct();
            FetchProductsFromAPI();

            AddOrUpdateCommand = new Command(async () =>
            {
                App.ProductRepo.SaveEntity(CurrentProduct);
                GenerateNewProduct();
                Refresh();
            });

            DeleteCommand = new Command(async () =>
            {
                Refresh();
                GenerateNewProduct();
            });

            TestCommand = new Command(async () =>
            {
                //FetchProductsFromAPI();
                int? loggedInUserId = Preferences.Get("LoggedInUserId", -1);
                Console.WriteLine($"Logged in User ID: {loggedInUserId}");

            });
            LogoutCommand = new Command(async () =>
            {
                Preferences.Set("IsLoggedIn", false);
                Preferences.Set("LoggedInUser", null);
                Preferences.Set("LoggedInUserId", -1);
                //new LoginPage();
            });

        }

        public async void FetchProductsFromAPI()
        {
            var products = await ProductLogic.GetProducts();
            Products = products;
        }

        private void GenerateNewProduct()
        {
            CurrentProduct = new Faker<Product>()
                .RuleFor(x => x.Name, f => f.Person.FullName)
                .Generate();
        }

        private void Refresh()
        {
            Products = App.ProductRepo.GetEntities();
            Console.WriteLine(Products);
        }
    }
}



