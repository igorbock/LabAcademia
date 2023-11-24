namespace LabAcademia.Pages;

public partial class HistoricoPage : ContentPage
{
    public IHistoricoService C_HistoricoService { get; set; }
    public ITreinoService C_TreinoService { get; set; }
    public IExercicioService C_ExercicioService { get; set; }

    public HistoricoPage(
        IHistoricoService p_HistoricoService,
        ITreinoService p_TreinoService,        
        IExercicioService p_ExercicioService)
	{
		InitializeComponent();

        C_HistoricoService = p_HistoricoService;
        C_TreinoService = p_TreinoService;
        C_ExercicioService = p_ExercicioService;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        lst_Historico.ItemsSource = await C_HistoricoService.CM_VerHistoricoAsync();
    }

    private async void lst_Historico_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var m_Treino = e.SelectedItem as Treino;
        await Navigation.PushAsync(new TreinoPage(m_Treino));
    }
}