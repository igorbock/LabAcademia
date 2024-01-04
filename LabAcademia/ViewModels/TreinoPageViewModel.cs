namespace LabAcademia.ViewModels;

public partial class TreinoPageViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Exercicio> _exercicios;

    [ObservableProperty]
    private Exercicio _exercicio;

    [ObservableProperty]
    private int _id;

    [ObservableProperty]
    private string _nome;

    [ObservableProperty]
    private Treino _treino;

    [ObservableProperty]
    private bool _historico;

    [ObservableProperty]
    private string _resultado;

    [ObservableProperty]
    private bool _carregando;

    [ObservableProperty]
    private string _tempo;

    public ITreinoService C_TreinoService { get; private set; }

    public TreinoPageViewModel(ITreinoService p_TreinoService)
    {
        C_TreinoService = p_TreinoService;
    }

    [RelayCommand]
    public async Task CM_CarregarExerciciosDoTreinoAsync()
    {
        Carregando = true;
        Resultado = "Carregando...";
        Exercicios = (await C_TreinoService.CM_ObterExerciciosDoTreinoAsync(Id)).ToObservableCollection();
        if (Exercicios.Count == 0)
            Resultado = "Não há exercícios no treino!";
        else
            Carregando = false;

        if (Historico)
        {
            Exercicios = Treino.Exercicios.ToObservableCollection();
            var m_Duracao = Treino.Fim - Treino.Inicio;
            Tempo = string.Format(
                "O treino foi realizado no dia {0}, com inicio às {1} e término às {2}.\n\nVocê levou {3} minutos",
                Treino.Fim.Value.ToString("dd/MM/yyyy"),
                Treino.Inicio.Value.ToString("HH:mm"),
                Treino.Fim.Value.ToString("HH:mm"),
                m_Duracao.Value.ToString("mm"));
        }
    }

    [RelayCommand]
    public async Task CM_ExercicioSelecionadoAsync(Exercicio p_Exercicio)
    {
        var m_ComTempo = "Tempo: {0}";
        var m_SemTempo = "Séries: {0}\nRepetição: {1}\nCarga: {2}";
        var m_Concluido = p_Exercicio.Concluido.GetValueOrDefault(false) ? "Sim" : "Não";
        if (Historico)
        {
            m_ComTempo = "Tempo: {0}\nConcluído: {1}";
            m_SemTempo = "Séries: {0}\nRepetição: {1}\nCarga: {2}\nConcluído: {3}";
        }

        if (p_Exercicio.Tempo == null)
            await Application.Current.MainPage.DisplayAlert(p_Exercicio.Descricao, string.Format(m_SemTempo, p_Exercicio.Series, p_Exercicio.Repeticao, p_Exercicio.Carga, m_Concluido), "Sair");
        else
            await Application.Current.MainPage.DisplayAlert(p_Exercicio.Descricao, string.Format(m_ComTempo, p_Exercicio.Tempo, m_Concluido), "Sair");

        Exercicio = null;
    }

    [RelayCommand]
    public void CM_LimparExercicios()
    {
        if (Exercicios != null)
            Exercicios.Clear();
    }
}
