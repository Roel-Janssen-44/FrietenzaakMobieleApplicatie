

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

        public TestPageViewModel()
        {
            Refresh();
            GenerateNewProduct();

            AddOrUpdateCommand = new Command(async () =>
            {
                App.ProductRepo.SaveEntity(CurrentProduct);
                Console.WriteLine(App.ProductRepo.statusMessage);
                GenerateNewProduct();
                Refresh();
            });

            DeleteCommand = new Command(async () =>
            {
                App.ProductRepo.DeleteEntity(CurrentProduct);
                Refresh();
                GenerateNewProduct();
            });

            TestCommand = new Command(async () =>
            {
                TestButtonClicked();
            });

        }


        public async void TestButtonClicked()
        {
            Console.WriteLine("Test");
            var products = await ProductLogic.GetProducts();
            Console.WriteLine(products);
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



