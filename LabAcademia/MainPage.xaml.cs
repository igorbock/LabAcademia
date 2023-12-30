namespace LabAcademia;

public partial class MainPage : ContentPage
{
    public ITreinoService C_TreinoService { get; set; }
    public IExercicioService C_ExercicioService { get; set; }
    public IPraticaService C_PraticaService { get; set; }
    public IHistoricoService C_HistoricoService { get; set; }
    public IAuthenticationService C_AuthenticationService { get; set; }
    public IUsuarioService C_UsuarioService { get; private set; }

    public MainPage(
        ITreinoService p_TreinoService, 
        IExercicioService p_ExercicioService, 
        IPraticaService p_PraticaService, 
        IHistoricoService p_HistoricoService,
        IAuthenticationService p_AuthenticationService,
        IUsuarioService p_UsuarioService
        //MainPageViewModel p_MainPageViewModel
        )
    {
        InitializeComponent();
        C_TreinoService = p_TreinoService;
        C_ExercicioService = p_ExercicioService;
        C_PraticaService = p_PraticaService;
        C_HistoricoService = p_HistoricoService;
        C_AuthenticationService = p_AuthenticationService;
        C_UsuarioService = p_UsuarioService;

        //BindingContext = new MainPageViewModel(p_TreinoService, p_ExercicioService, p_PraticaService, p_HistoricoService, p_AuthenticationService, p_UsuarioService);
    }

    private async void btn_Login_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(txt_Usuario.Text))
                throw new Exception("Usuário está vazio!");

            if (string.IsNullOrEmpty(txt_Senha.Text))
                throw new Exception("A senha não pode estar vazia!");

            await C_AuthenticationService.CM_LoginAsync(txt_Usuario.Text, txt_Senha.Text);

            Application.Current.MainPage = new AppShell(true, C_TreinoService, C_ExercicioService, C_PraticaService, C_HistoricoService, C_UsuarioService, C_AuthenticationService);
        }
        catch (UnauthorizedAccessException ex)
        {
            var m_Toast = Toast.Make(ex.Message, CommunityToolkit.Maui.Core.ToastDuration.Long);
            await m_Toast.Show();
        }
        catch (Exception ex)
        {
            var m_Toast = Toast.Make(ex.Message, CommunityToolkit.Maui.Core.ToastDuration.Long);
            await m_Toast.Show();
        }
    }

    private async void btn_QRCode_Clicked(object sender, EventArgs e) => await Navigation.PushAsync(new QRCodePage(C_UsuarioService));//, true, new Dictionary<string, object>
//    {
//        { "p_UsuarioService", C_UsuarioService
//}
//    });

protected override async void OnAppearing()
    {
        base.OnAppearing();

        //await (BindingContext as MainPageViewModel).CM_VerificarUsuarioAutenticadoAsync();
        var m_Token = await SecureStorage.GetAsync("Token");
        if (string.IsNullOrEmpty(m_Token) == false)
            Application.Current.MainPage = new AppShell(true, C_TreinoService, C_ExercicioService, C_PraticaService, C_HistoricoService, C_UsuarioService, C_AuthenticationService);
    }
}