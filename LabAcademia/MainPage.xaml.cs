namespace LabAcademia;

public partial class MainPage : ContentPage
{
    public MainPage(MainPageViewModel p_MainPageViewModel)
    {
        InitializeComponent();
        BindingContext = p_MainPageViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await (BindingContext as MainPageViewModel).CM_VerificarUsuarioAutenticadoAsync();
    }

    private async void Entry_Unfocused(object sender, FocusEventArgs e)
    {
        await (sender as Entry).HideKeyboardAsync();
    }
}