using MauiReactor;
using RiveApp.Pages;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace RiveApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiReactorApp<MainPage>()
                .UseSkiaSharp()
#if DEBUG
                .EnableMauiReactorHotReload()
#endif
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("Inter-Regular.ttf", "InterRegular");
                    fonts.AddFont("Inter-SemiBold.ttf", "InterSemiBold");
                    fonts.AddFont("Poppins-Bold.ttf", "PoppinsBold");
                });

            Controls.Native.BorderlessEntry.Configure();

            return builder.Build();
        }
    }
}