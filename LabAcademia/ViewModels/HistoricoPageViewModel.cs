namespace LabAcademia.ViewModels;

public partial class HistoricoPageViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Treino> _historico;

    [ObservableProperty]
    private bool _carregando;

    [ObservableProperty]
    private Treino _treino;

    public IHistoricoService C_HistoricoService { get; set; }

    public HistoricoPageViewModel(IHistoricoService p_HistoricoService)
    {
        C_HistoricoService = p_HistoricoService;
    }

    [RelayCommand]
    public async Task CM_CarregarHistoricoAsync()
    {
        try
        {
            Carregando = true;
            Historico = (await C_HistoricoService.CM_VerHistoricoAsync(null, null)).ToObservableCollection();
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            Carregando = false;
        }
    }

    [RelayCommand]
    public void CM_LimparHistorico()
    {
        if (Historico != null)
            Historico.Clear();
    }

    [RelayCommand]
    public async Task CM_HistoricoSelecionadoAsync(Treino p_Treino)
    {
        var m_Parametros = new Dictionary<string, object>
        {
            { "id", p_Treino.Id },
            { "nome", p_Treino.Nome },
            { "historico", true },
            { "treino", p_Treino }
        };

        await Shell.Current.GoToAsync(nameof(TreinoPage), true, m_Parametros);
        Treino = null;
    }
}
