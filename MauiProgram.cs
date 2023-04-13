using WordleClone.Services;
using WordleClone.ViewModel;

namespace WordleClone;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder.Services.AddSingleton<SolutionService>();
		builder.Services.AddTransient<GameViewModel>();
		//builder.Services.AddSingleton<HttpClient>();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
		builder.Services.AddTransient<MainPage>();
		return builder.Build();
	}
}
