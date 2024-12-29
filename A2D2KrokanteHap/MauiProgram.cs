using A2D2KrokanteHap.MVVM.Models;
using A2D2KrokanteHap.Repositories;
using Microsoft.Extensions.Logging;

namespace A2D2KrokanteHap
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("showcard.TTF", "Showcard");
                });

            builder.Services.AddSingleton<BaseRepository<Product>>();

#if DEBUG
            builder.Logging.AddDebug();

#endif

            return builder.Build();
        }
    }
}
