namespace LabAcademia.Pages;

public partial class HistoricoPage : ContentPage
{
    public IHistoricoService C_HistoricoService { get; set; }
    
    public HistoricoPage(IHistoricoService p_HistoricoService)
	{
		InitializeComponent();

        C_HistoricoService = p_HistoricoService;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        lst_Historico.ItemsSource = await C_HistoricoService.CM_VerHistoricoAsync();
    }

    private void lst_Historico_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {

    }
}