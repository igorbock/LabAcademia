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

            builder.Services.AddScoped<IStreamHelper<Treino>, StreamHelper<Treino>>();
            builder.Services.AddScoped<ITreinoHelper, TreinoHelper>();

            builder.Services.AddScoped<IRepository<Treino>, TreinoRepository>();

            //Configurar DB
            var m_NomeDB = "LabAcademia.db3";
            var m_DiretorioDB = FileHelper.CM_ObterDiretorioLocalComArquivo(m_NomeDB);
            builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<TreinoRepository>(s, m_DiretorioDB));

            return builder.Build();
        }
    }
}