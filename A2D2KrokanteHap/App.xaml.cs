using A2D2KrokanteHap.MVVM.Models;
using A2D2KrokanteHap.MVVM.Views;
using A2D2KrokanteHap.Repositories;

namespace A2D2KrokanteHap
{
    public partial class App : Application
    {
        public static BaseRepository<Product>? ProductRepo { get; private set; }
        public static BaseRepository<Customer>? CustomerRepo { get; private set; }
        public static BaseRepository<OrderLine>? OrderLineRepo { get; private set; }
        public static BaseRepository<Order>? OrderRepo { get; private set; }

        public App(BaseRepository<Product> productRepo, BaseRepository<Order> orderRepo, BaseRepository<Customer> customerRepo, BaseRepository<OrderLine> orderLineRepo)
        {
            InitializeComponent();

            ProductRepo = productRepo;
            OrderRepo = orderRepo;
            CustomerRepo = customerRepo;
            OrderLineRepo = orderLineRepo;

            SeedUsers();

            //Logout();

            // Redirect to LoginPage or CreateOrderPage based on login status
            bool isLoggedIn = Preferences.Get("IsLoggedIn", false);
            Console.WriteLine(isLoggedIn);
            var userId = Preferences.Get("LoggedInUserId", -1); // Default to -1 if not found
            Console.WriteLine(userId);
            MainPage = isLoggedIn ? new TestPage() : new LoginPage();

            //MainPage = new CreateOrderPage();
        }

        private void SeedUsers()
        {
            var users = CustomerRepo?.GetEntities();

            //if (users != null && users.Any())
            //{
            //    foreach (var user in users)
            //    {
            //        CustomerRepo?.DeleteEntity(user);
            //    }
            //}

            if (users == null)
            {
                var sjarel = new Customer
                {
                    UserName = "Sjarel",
                    Password = "password"
                };

                var pieter = new Customer
                {
                    UserName = "Pieter",
                    Password = "password" 
                };

                CustomerRepo?.SaveEntity(sjarel);
                CustomerRepo?.SaveEntity(pieter);
            }

            var updatedUsers = CustomerRepo?.GetEntities();
            Console.WriteLine(updatedUsers);
        }

        public static int? GetLoggedInUserId()
        {
            // Get the logged-in user's ID from Preferences
            var userId = Preferences.Get("LoggedInUserId", -1); // Default to -1 if not found

            // Return null if not found or if the user is not logged in
            return userId == -1 ? null : userId;
        }

        public void Logout()
        {
            Preferences.Set("IsLoggedIn", false);
            Preferences.Set("LoggedInUser", null);
            Preferences.Set("LoggedInUserId", -1);
        }


    }
}
