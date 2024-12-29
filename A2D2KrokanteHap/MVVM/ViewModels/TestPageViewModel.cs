

using Bogus;
using PropertyChanged;
using System.Windows.Input;
using A2D2KrokanteHap.MVVM.Models;


namespace A2D2KrokanteHap.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class TestPageViewModel
    {
        public List<Product>? Products { get; set; }
        public Product? CurrentProduct { get; set; }

        public ICommand? AddOrUpdateCommand { get; set; }
        public ICommand? DeleteCommand { get; set; }

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



