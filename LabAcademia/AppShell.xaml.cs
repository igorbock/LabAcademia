namespace LabAcademia;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(QRCodePage), typeof(QRCodePage));
        Routing.RegisterRoute(nameof(RegistroPage), typeof(RegistroPage));
        Routing.RegisterRoute(nameof(PraticaPage), typeof(PraticaPage));
        Routing.RegisterRoute(nameof(TreinoPage), typeof(TreinoPage));
    }
}