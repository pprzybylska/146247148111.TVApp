using _146247148111.TVApp.ViewModel;
using Microsoft.Extensions.Logging;

namespace _146247148111.TVApp.MAUI
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
                });

            builder.Services.AddSingleton<TVCollectionViewModel>();
            builder.Services.AddSingleton<CatalogTVView>();

            builder.Services.AddSingleton<ProducerCollectionViewModel>();
            builder.Services.AddSingleton<ProducersCatalogView>();

            // add several
            //builder.Services.AddTransient<>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
