namespace LabAcademia.Pages;

[QueryProperty(nameof(C_Matricula), "matricula")]
public partial class RegistroPage : ContentPage
{
    private RegistroPageViewModel _viewModel;
    public string C_Matricula { get => _viewModel.Matricula; set => _viewModel.Matricula = value; }

    public RegistroPage(RegistroPageViewModel p_RegistroPageViewModel)
    {
        InitializeComponent();

        _viewModel = p_RegistroPageViewModel;
        BindingContext = p_RegistroPageViewModel;
    }
}