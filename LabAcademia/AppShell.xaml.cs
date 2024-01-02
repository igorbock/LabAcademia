namespace LabAcademia;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(QRCodePage), typeof(QRCodePage));
        Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
        Routing.RegisterRoute(nameof(HistoricoPage), typeof(HistoricoPage));
        Routing.RegisterRoute(nameof(PraticaPage), typeof(PraticaPage));
        Routing.RegisterRoute(nameof(TreinoPage), typeof(TreinoPage));
        Routing.RegisterRoute(nameof(RegistroPage), typeof(RegistroPage));
    }
}