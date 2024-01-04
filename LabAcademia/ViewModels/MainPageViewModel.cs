namespace LabAcademia.ViewModels;

public partial class MainPageViewModel : ObservableObject
{
    [ObservableProperty]
    private string _usuario;

    [ObservableProperty]
    private string _senha;

    [ObservableProperty]
    private bool _carregando;

    public IAuthenticationService C_AuthenticationService { get; set; }

    public MainPageViewModel(IAuthenticationService p_AuthenticationService)
    {
        C_AuthenticationService = p_AuthenticationService;
        Carregando = false;
    }

    [RelayCommand]
    public async Task CM_LoginAsync()
    {
        try
        {
            Carregando = true;

            if (string.IsNullOrEmpty(Usuario))
                throw new Exception("Usuário está vazio!");

            if (string.IsNullOrEmpty(Senha))
                throw new Exception("A senha não pode estar vazia!");

            await C_AuthenticationService.CM_LoginAsync(Usuario, Senha);

            Usuario = string.Empty;
            Senha = string.Empty;

            await Shell.Current.GoToAsync($"//{nameof(HomePage)}", true);
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
        finally
        {
            Carregando = false;
        }
    }

    [RelayCommand]
    public async Task CM_CadastrarQRCodeAsync() => await Shell.Current.GoToAsync($"//{nameof(QRCodePage)}");
}
