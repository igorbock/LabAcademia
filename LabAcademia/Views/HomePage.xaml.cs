using CommunityToolkit.Maui.Alerts;

namespace LabAcademia.Pages;

public partial class HomePage : ContentPage
{
    public ITreinoService C_TreinoService { get; set; }
    public IExercicioService C_ExercicioService { get; set; }
    public IPraticaService C_PraticaService { get; set; }
    public IHistoricoService C_HistoricoService { get; set; }
    public IAuthenticationService C_AuthenticationService { get; set; }
    public IUsuarioService C_UsuarioService { get; set; }
    public IEnumerable<Treino> C_Treinos { get; set; }

    public HomePage()
    {
        InitializeComponent();
    }

    public HomePage(
        ITreinoService p_TreinoService,
        IExercicioService p_ExercicioService,
        IPraticaService p_PraticaService,
        IHistoricoService p_HistoricoService,
        IAuthenticationService p_AuthenticationService,
        IUsuarioService p_UsuarioService)
    {
        InitializeComponent();
        C_TreinoService = p_TreinoService;
        C_ExercicioService = p_ExercicioService;
        C_PraticaService = p_PraticaService;
        C_HistoricoService = p_HistoricoService;
        C_AuthenticationService = p_AuthenticationService;
        C_UsuarioService = p_UsuarioService;
    }

    private async void Treinos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var m_Treino = e.SelectedItem as Treino;
        await Navigation.PushAsync(new TreinoPage(C_TreinoService, m_Treino.Id, m_Treino.Nome));
        cm_RetirarSelecaoGrid();
    }

    private void cm_RetirarSelecaoGrid()
    {
        Treinos.ItemSelected -= Treinos_ItemSelected;
        Treinos.SelectedItem = null;
        Treinos.ItemSelected += Treinos_ItemSelected;
    }

    protected override async void OnAppearing()
    {
        try
        {
            base.OnAppearing();

            var m_JWT = await SecureStorage.GetAsync("Token");
            var m_Token = new JwtSecurityToken(m_JWT);
            var m_Matricula = m_Token.CMX_ValidarToken();

            if (C_TreinoService is not null)
                C_Treinos = await C_TreinoService.CM_TodosTreinosAsync(m_Matricula);

            Treinos.ItemsSource = C_Treinos;
        }
        catch (UnauthorizedAccessException)
        {
            await DisplayAlert("Desconectado", "O token expirou... Você será desconectado", "OK");
            await Shell.Current.GoToAsync(nameof(MainPage));
            //Application.Current.MainPage = new AppShell(false, C_TreinoService, C_ExercicioService, C_PraticaService, C_HistoricoService, C_UsuarioService, C_AuthenticationService);
        }
        catch(Exception ex)
        {
            var m_Toast = Toast.Make(ex.Message, CommunityToolkit.Maui.Core.ToastDuration.Long);
            await m_Toast.Show();
        }
    }

    private async void btn_Iniciar_Clicked(object sender, EventArgs e)
    {
        try
        {
            var m_Nomes = C_Treinos.Select(a => a.Nome).ToArray();
            var m_TreinoSelecionado = await DisplayActionSheet("Iniciar treino...", "Cancelar", null, FlowDirection.MatchParent, m_Nomes) ?? throw new Exception();
            if (m_TreinoSelecionado.Equals("Cancelar"))
                return;

            var m_Treino = C_Treinos.FirstOrDefault(a => a.Nome.Equals(m_TreinoSelecionado));

            await Navigation.PushAsync(new PraticaPage(m_Treino, C_PraticaService, C_ExercicioService));

        }
        catch (Exception)
        {
            return;
        }
    }

    private async void btn_Historico_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.Navigation.PushAsync(new HistoricoPage(C_HistoricoService, C_TreinoService), true);
    }

    private async void btn_Logout_Clicked(object sender, EventArgs e)
    {
        SecureStorage.RemoveAll();
        await Shell.Current.GoToAsync(nameof(MainPage));
        //Application.Current.MainPage = new AppShell(false, C_TreinoService, C_ExercicioService, C_PraticaService, C_HistoricoService, C_UsuarioService, C_AuthenticationService);
    }
}