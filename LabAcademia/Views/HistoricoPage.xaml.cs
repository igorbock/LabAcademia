namespace LabAcademia.Pages;

public partial class HistoricoPage : ContentPage
{
    public IHistoricoService C_HistoricoService { get; set; }
    public ITreinoService C_TreinoService { get; set; }

    public HistoricoPage(
        IHistoricoService p_HistoricoService,
        ITreinoService p_TreinoService)
	{
		InitializeComponent();

        C_HistoricoService = p_HistoricoService;
        C_TreinoService = p_TreinoService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        lst_Historico.ItemsSource = await C_HistoricoService.CM_VerHistoricoAsync(null, null);
    }

    private async void lst_Historico_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var m_Treino = e.SelectedItem as Treino;
        await Shell.Current.Navigation.PushAsync(new TreinoPage(C_TreinoService, m_Treino.Id, m_Treino.Nome, true), true);
    }
}