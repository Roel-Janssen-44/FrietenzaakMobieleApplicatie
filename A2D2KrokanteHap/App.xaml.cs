using A2D2KrokanteHap.MVVM.Models;
using A2D2KrokanteHap.MVVM.Views;
using A2D2KrokanteHap.Repositories;
namespace A2D2KrokanteHap
{
    public partial class App : Application
    {
        
        public static BaseRepository<Product>? ProductRepo { get; private set; }

        public App(BaseRepository<Product> productRepo)
        {
            InitializeComponent();

            ProductRepo = productRepo;
            MainPage = new TestPage();
        }
    }
}
