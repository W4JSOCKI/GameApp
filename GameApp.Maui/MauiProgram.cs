using Microsoft.Extensions.Logging;
using Wojtalak_Szczerkowski.GameApp.Maui.ViewModels;
using CommunityToolkit.Maui;
using Wojtalak_Szczerkowski.GameApp.Maui;


namespace Wojtalak_Szczerkowski.GameApp.Maui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddSingleton<GamesCollectionViewModel>();
            builder.Services.AddSingleton<GamesPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}