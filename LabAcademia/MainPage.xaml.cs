namespace LabAcademia;

public partial class MainPage : ContentPage
{
    public MainPage(MainPageViewModel p_MainPageViewModel)
    {
        InitializeComponent();
        BindingContext = p_MainPageViewModel;
    }

    private async void Entry_Unfocused(object sender, FocusEventArgs e)
    {
        await (sender as Entry).HideKeyboardAsync();
    }
}