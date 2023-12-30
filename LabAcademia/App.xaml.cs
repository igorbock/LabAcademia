namespace LabAcademia;

public partial class App : Application
{
    public App(
        ITreinoService p_TreinoService,
        IExercicioService p_ExercicioService,
        IPraticaService p_PraticaService,
        IHistoricoService p_HistoricoService,
        IUsuarioService p_UsuarioService,
        IAuthenticationService p_AuthenticationService)
    {
        InitializeComponent();

        //MainPage = new AppShell();
        MainPage = new AppShell(false, p_TreinoService, p_ExercicioService, p_PraticaService, p_HistoricoService, p_UsuarioService, p_AuthenticationService);
    }
}