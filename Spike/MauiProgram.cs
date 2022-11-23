using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Hosting;
#if IOS
using Spike.Platform.iOS;
#endif

namespace Spike;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
         .ConfigureMauiHandlers((handlers) =>
         {
#if IOS
            handlers.AddHandler(typeof(CustomButton), typeof(CustomButtonHandler));
#endif
         })
         .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		return builder.Build();
	}
}
