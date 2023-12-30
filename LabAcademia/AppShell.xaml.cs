namespace LabAcademia;

public partial class AppShell : Shell
{
    public AppShell(
        bool p_Autenticado,
        ITreinoService p_TreinoService,
        IExercicioService p_ExercicioService,
        IPraticaService p_PraticaService,
        IHistoricoService p_HistoricoService,
        IUsuarioService p_UsuarioService,
        IAuthenticationService p_AuthenticationService
        )
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(QRCodePage), typeof(QRCodePage));
        Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
        Routing.RegisterRoute(nameof(HistoricoPage), typeof(HistoricoPage));
        Routing.RegisterRoute(nameof(PraticaPage), typeof(PraticaPage));
        Routing.RegisterRoute(nameof(TreinoPage), typeof(TreinoPage));

        ShellPrincipal.Items.Clear();
        if (p_Autenticado == false)
            ShellPrincipal.Items.Add(new ShellContent() { Title = "Home", Content = new MainPage(p_TreinoService, p_ExercicioService, p_PraticaService, p_HistoricoService, p_AuthenticationService, p_UsuarioService), Route = nameof(MainPage) });
        else
        if (p_Autenticado)
        {
            ShellPrincipal.Items.Add(new ShellContent() { Title = "Home", Content = new HomePage(p_TreinoService, p_ExercicioService, p_PraticaService, p_HistoricoService, p_AuthenticationService, p_UsuarioService), Route = nameof(HomePage) });
            ShellPrincipal.Items.Add(new ShellContent() { Title = "Histórico", Content = new HistoricoPage(p_HistoricoService, p_TreinoService), Route = nameof(HistoricoPage) });
        }
    }
}