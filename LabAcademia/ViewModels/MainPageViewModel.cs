namespace LabAcademia.ViewModels;

public partial class MainPageViewModel : ObservableObject
{
    [ObservableProperty]
    private Login c_Login;

    public ITreinoService C_TreinoService { get; set; }
    public IExercicioService C_ExercicioService { get; set; }
    public IPraticaService C_PraticaService { get; set; }
    public IHistoricoService C_HistoricoService { get; set; }
    public IAuthenticationService C_AuthenticationService { get; set; }
    public IUsuarioService C_UsuarioService { get; private set; }

    public MainPageViewModel(
        ITreinoService p_TreinoService,
        IExercicioService p_ExercicioService,
        IPraticaService p_PraticaService,
        IHistoricoService p_HistoricoService,
        IAuthenticationService p_AuthenticationService,
        IUsuarioService p_UsuarioService)
    {
        C_TreinoService = p_TreinoService;
        C_ExercicioService = p_ExercicioService;
        C_PraticaService = p_PraticaService;
        C_HistoricoService = p_HistoricoService;
        C_AuthenticationService = p_AuthenticationService;
        C_UsuarioService = p_UsuarioService;
    }

    [RelayCommand]
    public async Task CM_LoginAsync()
    {
        try
        {
            if (string.IsNullOrEmpty(C_Login.Usuario))
                throw new Exception("Usuário está vazio!");

            if (string.IsNullOrEmpty(C_Login.Senha))
                throw new Exception("A senha não pode estar vazia!");

            await C_AuthenticationService.CM_LoginAsync(C_Login.Usuario, C_Login.Senha);

            await Shell.Current.GoToAsync(nameof(HomePage));
            //Application.Current.MainPage = new AppShell(true, C_TreinoService, C_ExercicioService, C_PraticaService, C_HistoricoService, C_UsuarioService, C_AuthenticationService);
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

    [RelayCommand]
    public async Task CM_CadastrarQRCodeAsync() => await Shell.Current.GoToAsync(nameof(QRCodePage));

    [RelayCommand]
    public async Task CM_VerificarUsuarioAutenticadoAsync()
    {
        var m_Token = await SecureStorage.GetAsync("Token");
        if (string.IsNullOrEmpty(m_Token) == false)
            await Shell.Current.GoToAsync(nameof(HomePage));
            //Application.Current.MainPage = new AppShell(true, C_TreinoService, C_ExercicioService, C_PraticaService, C_HistoricoService, C_UsuarioService, C_AuthenticationService);
    }
}
