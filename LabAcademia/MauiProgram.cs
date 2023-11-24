namespace LabAcademia
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

#if DEBUG
		builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddScoped<ITreinoService, TreinoService>();
            builder.Services.AddScoped<IExercicioService, ExercicioService>();
            builder.Services.AddScoped<IPraticaService, PraticaService>();
            builder.Services.AddScoped<IHistoricoService, HistoricoService>();

            builder.Services.AddScoped<IStreamHelper<Treino>, StreamHelper<Treino>>();
            builder.Services.AddScoped<ITreinoHelper, TreinoHelper>();

            return builder.Build();
        }
    }
}