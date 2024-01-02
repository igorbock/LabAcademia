namespace LabAcademia.Pages;

[QueryProperty(nameof(C_TreinoAtual), "treino")]
public partial class PraticaPage : ContentPage
{
    public Treino C_TreinoAtual { get => _viewModel.Treino; set => _viewModel.Treino = value; }

    private PraticaPageViewModel _viewModel;

    public PraticaPage(PraticaPageViewModel p_PraticaPageViewModel)
    {
        InitializeComponent();

        _viewModel = p_PraticaPageViewModel;
        BindingContext = p_PraticaPageViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (C_TreinoAtual.Inicio != null)
            return;

        Title = $"Prática - {C_TreinoAtual.Nome}";
        C_TreinoAtual.Inicio = DateTime.UtcNow;

        await (BindingContext as PraticaPageViewModel).CM_IniciarPraticaAsync();
    }
}