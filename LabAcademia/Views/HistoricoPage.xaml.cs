namespace LabAcademia.Pages;

public partial class HistoricoPage : ContentPage
{
    public HistoricoPage(HistoricoPageViewModel p_ViewModel)
	{
		InitializeComponent();

        BindingContext = p_ViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await (BindingContext as HistoricoPageViewModel).CM_CarregarHistoricoAsync();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        (BindingContext as HistoricoPageViewModel).CM_LimparHistorico();
    }
}