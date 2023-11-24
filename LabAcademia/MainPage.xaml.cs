namespace LabAcademia;

public partial class MainPage : ContentPage
{
    public ITreinoService C_TreinoService { get; set; }
    public IExercicioService C_ExercicioService { get; set; }
    public IPraticaService C_PraticaService { get; set; }
    public IHistoricoService C_HistoricoService { get; set; }
    public IEnumerable<Treino> C_Treinos { get; set; }

    public MainPage(
        ITreinoService p_TreinoService, 
        IExercicioService p_ExercicioService, 
        IPraticaService p_PraticaService, 
        IHistoricoService p_HistoricoService)
    {
        InitializeComponent();
        C_TreinoService = p_TreinoService;
        C_ExercicioService = p_ExercicioService;
        C_PraticaService = p_PraticaService;
        C_HistoricoService = p_HistoricoService;

    }

    private async void Treinos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var m_Treino = e.SelectedItem as Treino;
        await Navigation.PushAsync(new TreinoPage(C_TreinoService, C_ExercicioService, m_Treino.Id));
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        C_Treinos = C_TreinoService.CM_TodosTreinos();
        Treinos.ItemsSource = C_Treinos;
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var m_Nome = await DisplayPromptAsync("Nome", "Qual o nome do treino?", placeholder: "Digite o nome do treino...");
        if (m_Nome == null)
            return;

        try
        {
            await C_TreinoService.CM_EscreverTreinoAsync(m_Nome);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "Voltar");
        }

        OnAppearing();
    }

    private async void Apagar_Clicked(object sender, EventArgs e)
    {
        try
        {
            var m_Nomes = C_Treinos.Select(a => a.Nome).ToArray();
            var m_TreinoSelecionado = await DisplayActionSheet("Apagar Treino:", "Cancelar", "Sair", FlowDirection.MatchParent, m_Nomes) ?? throw new Exception();
            if (m_TreinoSelecionado.Equals("Sair"))
                return;
            var m_Treino = C_Treinos.FirstOrDefault(a => a.Nome.Equals(m_TreinoSelecionado));

            var m_Resposta = await DisplayAlert("Remover", $"Confirma a remoção do treino: {m_Treino.Nome}?", "Sim", "Não");
            if (m_Resposta == false)
                return;

            await C_TreinoService.CM_ApagarTreinoAsync(m_Treino.Id);
        }
        catch (Exception)
        {
            return;
        }
        finally
        {
            OnAppearing();
        }
    }

    private async void btn_Iniciar_Clicked(object sender, EventArgs e)
    {
        try
        {
            var m_Nomes = C_Treinos.Select(a => a.Nome).ToArray();
            var m_TreinoSelecionado = await DisplayActionSheet("Iniciar treino...", "Cancelar", null, FlowDirection.MatchParent, m_Nomes) ?? throw new Exception();
            if (m_TreinoSelecionado.Equals("Cancelar"))
                return;

            var m_Treino = C_Treinos.FirstOrDefault(a => a.Nome.Equals(m_TreinoSelecionado));

            await Navigation.PushAsync(new PraticaPage(m_Treino, C_PraticaService, C_ExercicioService));

    }
        catch (Exception)
        {
            return;
        }
    }

    private async void btn_Historico_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HistoricoPage(C_HistoricoService, C_TreinoService, C_ExercicioService));
    }
}