namespace LabAcademia.Pages;

public partial class LoadingPage : ContentPage
{
    public IAuthenticationService C_AuthenticationService { get; private set; }

    public LoadingPage(IAuthenticationService p_AuthenticationService)
	{
		InitializeComponent();

        C_AuthenticationService = p_AuthenticationService;
	}

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        var m_UsuarioAutenticado = await C_AuthenticationService.CM_UsuarioConectadoAsync();
        if (m_UsuarioAutenticado)
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        else
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
    }
}