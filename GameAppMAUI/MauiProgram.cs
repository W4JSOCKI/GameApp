using Microsoft.Extensions.Logging;
using Wojtalak_Szczerkowski.GameApp.BLC;
using GameAppMAUI.ViewModels;
using System.Configuration;

namespace GameAppMAUI
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

            builder.Services.AddSingleton<BLC>(provider => new BLC(ConfigurationManager.AppSettings["DAOLibraryName"]));
            builder.Services.AddSingleton<GameCollectionViewModel>();
            builder.Services.AddSingleton<DeveloperCollectionViewModel>();

            builder.Services.AddSingleton<GamesPage>();
            builder.Services.AddSingleton<DevelopersPage>();
            builder.Services.AddSingleton<GameAddPage>();
            builder.Services.AddSingleton<DeveloperAddPage>();
            builder.Services.AddSingleton<GameEditPage>();
            builder.Services.AddSingleton<DeveloperEditPage>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
