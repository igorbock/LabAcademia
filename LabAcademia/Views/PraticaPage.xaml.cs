namespace LabAcademia.Pages;

public partial class PraticaPage : ContentPage
{
    public Treino C_TreinoAtual { get; set; }
    public IPraticaService C_Service { get; set; }
    public IExercicioService C_ExercicioService { get; set; }

    public PraticaPage(Treino p_Treino, IPraticaService p_Service, IExercicioService p_ExercicioService)
	{
		InitializeComponent();

        C_Service = p_Service;
        C_ExercicioService = p_ExercicioService;
		C_TreinoAtual = p_Treino;
		Title = $"Prática - {p_Treino.Nome}";
        C_Service.CM_IniciarPraticaAsync(C_TreinoAtual);
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        Shell.Current.Navigating -= ShellNavigation;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        lst_Exercicios.ItemsSource = C_TreinoAtual.Exercicios;
        Shell.Current.Navigating += ShellNavigation;
    }
    
    private async void ShellNavigation(object sender, ShellNavigatingEventArgs e)
    {
        if(e.CanCancel)
        {
            e.Cancel();

            if (await DisplayAlert("Você deseja sair do treino?", "Os dados serão perdidos", accept: "Sim", cancel: "Não"))
            {
                C_Service.CM_RemoverPratica(C_TreinoAtual);

                Shell.Current.Navigating -= ShellNavigation;
                await Shell.Current.Navigation.PopAsync();
            }
        }
    }

    private async void lst_Exercicios_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var m_ExercicioSelecionado = e.SelectedItem as Exercicio;
        var m_NovaCarga = await DisplayPromptAsync("Nova carga...", "Atualize a carga do exercício:", accept: "Salvar", cancel: "Cancelar", maxLength: 3, initialValue: m_ExercicioSelecionado.Carga.ToString());
        if (string.IsNullOrEmpty(m_NovaCarga))
        {
            lst_Exercicios.ItemSelected -= lst_Exercicios_ItemSelected;
            lst_Exercicios.SelectedItem = null;
            lst_Exercicios.ItemSelected += lst_Exercicios_ItemSelected;
            return;
        }

        if (int.TryParse(m_NovaCarga, out int m_Carga) == false)
        {
            await DisplayAlert("Erro", "Use somente números na carga...", "Voltar");
            return;
        }
        await C_ExercicioService.CM_AlterarCargaExercicioAsync(m_ExercicioSelecionado, m_Carga);

        OnAppearing();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await C_Service.CM_ConcluirPraticaAsync(C_TreinoAtual);
        Shell.Current.Navigating -= ShellNavigation;
        await Navigation.PopToRootAsync();
    }
}