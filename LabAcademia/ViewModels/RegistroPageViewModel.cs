namespace LabAcademia.ViewModels;

public partial class RegistroPageViewModel : ObservableObject
{
    public IUsuarioService C_UsuarioService { get; private set; }

    [ObservableProperty]
    private string _matricula;

    [ObservableProperty]
    private string _senha;

    [ObservableProperty]
    private string _confirmaSenha;

    public RegistroPageViewModel(IUsuarioService p_UsuarioService)
    {
        C_UsuarioService = p_UsuarioService;
    }

    [RelayCommand]
    public async Task CM_RegistrarAlunoAsync()
    {
        var m_SenhasIguais = string.Equals(Senha, ConfirmaSenha);

        if (m_SenhasIguais == false)
        {
            var m_Toast = Toast.Make("As senhas devem ser iguais!");
            await m_Toast.Show();
            return;
        }

        await C_UsuarioService.CM_RegistrarUsuarioAsync(Matricula, Senha);
        await Shell.Current.GoToAsync(nameof(MainPage), true);
    }

    [RelayCommand]
    public async Task CM_VoltarAsync()
    {
        await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
    }
}
