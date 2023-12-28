using System.IdentityModel.Tokens.Jwt;

namespace LabAcademia.Pages;

public partial class AlunoPage : ContentPage
{
    public ITreinoService C_TreinoService { get; private set; }
    public IExercicioService C_ExercicioService { get; private set; }
    public string C_Matricula { get; private set; }

    public AlunoPage()
    {
        InitializeComponent();
    }

    public AlunoPage(ITreinoService p_TreinoService, IExercicioService p_ExercicioService)
    {
        InitializeComponent();

        C_TreinoService = p_TreinoService;
        C_ExercicioService = p_ExercicioService;

        var m_TokenJSON = SecureStorage.GetAsync("Token").Result;
        var m_Token = new JwtSecurityToken(m_TokenJSON);
        var m_Claim = m_Token.Claims.SingleOrDefault(a => a.ValueType == "matricula");
        C_Matricula = m_Claim.Value;
    }

    protected override async void OnAppearing()
    {
        try
        {
            listViewTreinos.ItemsSource = await C_TreinoService.CM_TodosTreinosAsync(C_Matricula);

            base.OnAppearing();
        }
        catch (Exception)
        {
        }
    }

    private async void listViewTreinos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var m_Treino = e.SelectedItem as Treino;
        await Navigation.PushAsync(new TreinoPage(C_TreinoService, C_ExercicioService, m_Treino.Codigo));
    }

    private void btn_Logout_Clicked(object sender, EventArgs e)
    {
        SecureStorage.Remove("Token");
        Application.Current.MainPage = new MainPage(null, null, null, null, null, null);
    }
}