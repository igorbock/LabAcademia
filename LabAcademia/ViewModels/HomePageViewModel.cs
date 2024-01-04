namespace LabAcademia.ViewModels;

public partial class HomePageViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Treino> _treinos;

    [ObservableProperty]
    private Treino _treino;

    [ObservableProperty]
    private bool _carregando;

    [ObservableProperty]
    private string _resultado;

    [ObservableProperty]
    private string _treinoExistente;

    public ITreinoService C_TreinoService { get; set; }
    public Treino C_TreinoAtual { get; set; }

    public HomePageViewModel(ITreinoService p_TreinoService)
    {
        C_TreinoService = p_TreinoService;
        Carregando = true;
    }

    [RelayCommand]
    public async Task CM_SelecionarTreinoAsync(Treino p_Treino)
    {
        var m_Parametros = new Dictionary<string, object>
        {
            { "id", p_Treino.Id },
            { "nome", p_Treino.Nome },
            { "historico", false },
            { "treino", p_Treino }
        };

        await Shell.Current.GoToAsync(nameof(TreinoPage), true, m_Parametros);
        Treino = null;
    }

    [RelayCommand]
    public async Task CM_LogoutAsync()
    {
        SecureStorage.RemoveAll();
        if (Treinos != null)
            Treinos.Clear();
        C_TreinoAtual = null;
        await Shell.Current.GoToAsync(nameof(MainPage));

        Carregando = true;
    }

    [RelayCommand]
    public async Task CM_VerHistoricoAsync() => await Shell.Current.GoToAsync(nameof(HistoricoPage));

    [RelayCommand]
    public async Task CM_IniciarTreinoAsync()
    {
        if (C_TreinoAtual == null)
        {
            var m_Nomes = Treinos.Select(a => a.Nome).ToArray();
            var m_TreinoSelecionado = await Application.Current.MainPage.DisplayActionSheet("Iniciar treino...", "Cancelar", null, FlowDirection.MatchParent, m_Nomes) ?? throw new Exception();
            if (m_TreinoSelecionado.Equals("Cancelar"))
                return;

            C_TreinoAtual = Treinos.FirstOrDefault(a => a.Nome.Equals(m_TreinoSelecionado));
        }

        var m_Parametros = new Dictionary<string, object>
        {
            { "treino", C_TreinoAtual }
        };

        await Shell.Current.GoToAsync(nameof(PraticaPage), true, m_Parametros);
    }

    [RelayCommand]
    public async Task CM_VerificarTreinoExistenteAsync()
    {
        try
        {
            var m_TreinoExistente = await SecureStorage.GetAsync("Treino");
            if (string.IsNullOrEmpty(m_TreinoExistente))
            {
                TreinoExistente = "Iniciar Novo Treino";
                C_TreinoAtual = null;
            }
            else
            {
                TreinoExistente = $"Continuar Treino - {C_TreinoAtual.Nome}";
                C_TreinoAtual = JsonSerializer.Deserialize<Treino>(m_TreinoExistente);
            }
        }
        catch (Exception ex)
        {
            var m_Toast = Toast.Make(ex.Message, CommunityToolkit.Maui.Core.ToastDuration.Long);
            await m_Toast.Show();
        }
    }

    [RelayCommand]
    public async Task CM_CarregarTreinosAsync()
    {
        try
        {
            if (Treinos != null && Treinos.Any())
                return;

            var m_JWT = await SecureStorage.GetAsync("Token");
            if (string.IsNullOrEmpty(m_JWT))
                throw new UnauthorizedAccessException();

            var m_Token = new JwtSecurityToken(m_JWT);
            var m_Matricula = m_Token.CMX_ValidarToken();

            Treinos = (await C_TreinoService.CM_TodosTreinosAsync(m_Matricula)).ToObservableCollection();
            Carregando = false;
        }
        catch (UnauthorizedAccessException)
        {
            //await Application.Current.MainPage.DisplayAlert("Desconectado", "O token expirou... Você será desconectado", "OK");
            await CM_LogoutAsync();
        }
        catch (Exception ex)
        {
            var m_Toast = Toast.Make(ex.Message, CommunityToolkit.Maui.Core.ToastDuration.Long);
            await m_Toast.Show();
            Resultado = $"Um erro aconteceu: {ex.Message}";
        }
    }
}
