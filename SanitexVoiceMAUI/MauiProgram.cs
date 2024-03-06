using System.Reflection;
using Microsoft.Extensions.Logging;
using SanitexVoiceMAUI.Data;
using SanitexVoiceMAUI.Services;
using Microsoft.AspNetCore.Components.WebView.Maui;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;

namespace SanitexVoiceMAUI;

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
			});

        builder.AddAppSettings();
        string baseUrl = builder.Configuration.GetValue<string>("VOICE_API_BASE_URL");
        
		builder.Services.AddMauiBlazorWebView();
#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif
       
        builder.Services.AddSingleton<WeatherForecastService>();
        builder.Services.AddSingleton<RestService>();
        builder.Services.AddSingleton<VoiceApiService>();
        

        return builder.Build();
	}

    private static void AddAppSettings(this MauiAppBuilder builder)
    {
       using Stream stream = Assembly
            .GetExecutingAssembly()
            .GetManifestResourceStream("SanitexVoiceMAUI.appsettings.json");
       if (stream != null)
       {
           IConfigurationRoot config = new ConfigurationBuilder()
               .AddJsonStream(stream)
               .Build();
           builder.Configuration.AddConfiguration(config);
       }
    }
}
