namespace LabAcademia.Pages;

public partial class TreinoPage : ContentPage
{
    public ITreinoService C_TreinoService { get; set; }
    public List<Exercicio> C_Exercicios { get; set; }
    public int C_Id { get; private set; }
    public string C_Nome { get; private set; }
    public bool C_Historico { get; private set; }

    public TreinoPage(ITreinoService p_TreinoService, int p_Id, string p_Nome, bool p_Historico = false)
    {
        InitializeComponent();
        C_Id = p_Id;
        C_Nome = p_Nome;
        C_TreinoService = p_TreinoService;
        C_Historico = p_Historico;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        C_Exercicios = (await C_TreinoService.CM_ObterExerciciosDoTreinoAsync(C_Id)).ToList();

        Title = C_Nome;
        listView_Exercicios.ItemsSource = C_Exercicios;

        if (C_Exercicios.Count == 0)
        {
            listView_Exercicios.IsVisible = false;
            lbl_Exercicios.Text = "Não há exercícios no treino!";
        }
    }

    private async void listView_Exercicios_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var m_Exercicio = e.SelectedItem as Exercicio;
        var m_ComTempo = "Tempo: {0}";
        var m_SemTempo = "Séries: {0}\nRepetição: {1}\nCarga: {2}";
        var m_Concluido = m_Exercicio.Concluido.GetValueOrDefault(false) ? "Sim" : "Não";
        if (C_Historico)
        {
            m_ComTempo = "Tempo: {0}\nConcluído: {1}";
            m_SemTempo = "Séries: {0}\nRepetição: {1}\nCarga: {2}\nConcluído: {3}";
        }

        if(m_Exercicio.Tempo == null)
            await DisplayAlert(m_Exercicio.Descricao, string.Format(m_SemTempo, m_Exercicio.Series, m_Exercicio.Repeticao, m_Exercicio.Carga, m_Concluido), "Sair");
        else
            await DisplayAlert(m_Exercicio.Descricao, string.Format(m_ComTempo, m_Exercicio.Tempo, m_Concluido), "Sair");

        cm_RetirarSelecaoGrid();
    }

    private void cm_RetirarSelecaoGrid()
    {
        listView_Exercicios.ItemSelected -= listView_Exercicios_ItemSelected;
        listView_Exercicios.SelectedItem = null;
        listView_Exercicios.ItemSelected += listView_Exercicios_ItemSelected;
    }

    protected override bool OnBackButtonPressed()
    {
        Shell.Current.GoToAsync("..");

        return base.OnBackButtonPressed();
    }
}