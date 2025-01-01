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
            MainPage = new CreateOrderPage();
        }
    }
}
