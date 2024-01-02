namespace LabAcademia.Pages;

public partial class HomePage : ContentPage
{
    public ITreinoService C_TreinoService { get; set; }

    public HomePage(ITreinoService p_TreinoService, HomePageViewModel p_HomePageViewModel)
    {
        InitializeComponent();
        C_TreinoService = p_TreinoService;
        BindingContext = p_HomePageViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await (BindingContext as HomePageViewModel).CM_CarregarTreinosAsync();
        await (BindingContext as HomePageViewModel).CM_VerificarTreinoExistenteAsync();
    }
}