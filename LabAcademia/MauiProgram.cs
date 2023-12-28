namespace LabAcademia;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseBarcodeReader()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .ConfigureMauiHandlers(a =>
            {
                a.AddHandler(typeof(CameraBarcodeReaderView), typeof(CameraBarcodeReaderViewHandler));
                a.AddHandler(typeof(CameraView), typeof(CameraViewHandler));
                a.AddHandler(typeof(BarcodeGeneratorView), typeof(BarcodeGeneratorViewHandler));
            });

#if DEBUG
		builder.Logging.AddDebug();
#endif
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<AppShell>();
        builder.Services.AddSingleton<ITreinoService, TreinoService>();
        builder.Services.AddSingleton<IExercicioService, ExercicioService>();
        builder.Services.AddSingleton<IPraticaService, PraticaService>();
        builder.Services.AddSingleton<IHistoricoService, HistoricoService>();
        builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();
        builder.Services.AddSingleton<IUsuarioService, UsuarioService>();

        builder.Services.AddScoped<IStreamHelper<Treino>, StreamHelper<Treino>>();
        builder.Services.AddScoped<ITreinoHelper, TreinoHelper>();

        builder.Services.AddHttpClient("LabAspNetIdentity", a =>
        {
#if DEBUG
            a.BaseAddress = new Uri("https://localhost:7121");
            //a.BaseAddress = new Uri("https://providerjwt20231223203207.azurewebsites.net");
#else
            a.BaseAddress = new Uri("https://providerjwt20231223203207.azurewebsites.net");
#endif
        });
        builder.Services.AddHttpClient("LabAcademiaAPI", a =>
        {
#if DEBUG
            a.BaseAddress = new Uri("http://localhost:5239");
            //a.BaseAddress = new Uri("https://labacademiaapi20231226214200.azurewebsites.net");
#else
            a.BaseAddress = new Uri("https://labacademiaapi20231226214200.azurewebsites.net");
#endif
        });

        return builder.Build();
    }
}