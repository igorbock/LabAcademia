namespace LabAcademia.Pages;

[QueryProperty(nameof(C_Id), "id")]
[QueryProperty(nameof(C_Nome), "nome")]
[QueryProperty(nameof(C_Historico), "historico")]
[QueryProperty(nameof(C_Treino), "treino")]
public partial class TreinoPage : ContentPage
{
    private TreinoPageViewModel _viewModel;

    public ITreinoService C_TreinoService { get; set; }
    public List<Exercicio> C_Exercicios { get; set; }
    public Treino C_Treino { get => _viewModel.Treino; set => _viewModel.Treino = value; }
    public int C_Id { set => _viewModel.Id = value; }
    public string C_Nome { get => _viewModel.Nome; set => _viewModel.Nome = value; }
    public bool C_Historico { set => _viewModel.Historico = value; }

    public TreinoPage(TreinoPageViewModel p_ViewModel)
    {
        InitializeComponent();
        _viewModel = p_ViewModel;
        BindingContext = p_ViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        Title = C_Nome;
        await (BindingContext as TreinoPageViewModel).CM_CarregarExerciciosDoTreinoAsync();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        (BindingContext as TreinoPageViewModel).CM_LimparExercicios();
    }
}