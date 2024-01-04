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
        //Views
        builder.Services.AddScoped<MainPage>();
        builder.Services.AddScoped<HomePage>();
        builder.Services.AddScoped<TreinoPage>();
        builder.Services.AddScoped<PraticaPage>();
        builder.Services.AddScoped<HistoricoPage>();
        builder.Services.AddTransient<QRCodePage>();
        builder.Services.AddTransient<RegistroPage>();
        builder.Services.AddTransient<LoadingPage>();

        //ViewModels
        builder.Services.AddTransient<MainPageViewModel>();
        builder.Services.AddTransient<HomePageViewModel>();
        builder.Services.AddTransient<TreinoPageViewModel>();
        builder.Services.AddTransient<PraticaPageViewModel>();
        builder.Services.AddTransient<HistoricoPageViewModel>();
        builder.Services.AddTransient<QRCodePageViewModel>();
        builder.Services.AddTransient<RegistroPageViewModel>();

        //Services
        builder.Services.AddTransient<ITreinoService, TreinoService>();
        builder.Services.AddTransient<IExercicioService, ExercicioService>();
        builder.Services.AddTransient<IPraticaService, PraticaService>();
        builder.Services.AddTransient<IHistoricoService, HistoricoService>();
        builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();
        builder.Services.AddTransient<IUsuarioService, UsuarioService>();

        builder.Services.AddScoped<ISecureStorageService, SecureStorageService>();

        builder.Services.AddHttpClient("LabAspNetIdentity", a =>
        {
#if DEBUG
            //a.BaseAddress = new Uri("https://localhost:7121");
            a.BaseAddress = new Uri("https://providerjwt20231223203207.azurewebsites.net");
#else
            a.BaseAddress = new Uri("https://providerjwt20231223203207.azurewebsites.net");
#endif
        });
        builder.Services.AddHttpClient("LabAcademiaAPI", a =>
        {
#if DEBUG
            //a.BaseAddress = new Uri("http://localhost:5239");
            a.BaseAddress = new Uri("https://labacademiaapi20231226214200.azurewebsites.net");
#else
            a.BaseAddress = new Uri("https://labacademiaapi20231226214200.azurewebsites.net");
#endif
        });

        return builder.Build();
    }
}