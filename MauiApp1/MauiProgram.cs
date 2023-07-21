using MauiApp1.Data;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using Microsoft.EntityFrameworkCore;
using MauiApp1.LocalDB;
using MauiApp1.Services;

namespace MauiApp1
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>().ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            }).UseMauiCommunityToolkit();
            builder.Services.AddMauiBlazorWebView();
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<WeatherForecastService>();
            builder.Services.AddSingleton<IMyLocationService, MyLocationService>();

            var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myapp.db"); var connectionString = $"Data Source={path}";


            builder.Services.AddDbContext<MobileDB>(options =>
                options.UseSqlite(connectionString));

            return builder.Build();


        }
    }
}