namespace LabAcademia.Pages;

public partial class TreinoPage : ContentPage
{
    public ITreinoService C_TreinoService { get; set; }
    public IExercicioService C_ExercicioService { get; set; }
    public Exercicio C_ExercicioSelecionado { get; set; }
    public List<Exercicio> C_Exercicios { get; set; }
    public char C_Id { get; set; }

    public TreinoPage(ITreinoService p_TreinoService, IExercicioService p_ExercicioService, char p_Id)
    {
        InitializeComponent();
        C_Id = p_Id;
        C_TreinoService = p_TreinoService;
        C_ExercicioService = p_ExercicioService;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        var m_Treino = C_TreinoService.CM_LerTreino(C_Id);
        Title = m_Treino.Nome;
        C_Exercicios = m_Treino.Exercicios;

        //Treinos.ItemsSource = C_Exercicios;
        //if(C_Exercicios == null || C_Exercicios.Count == 0)
        //    Remover.IsEnabled = false;
    }

    private async void Treinos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var m_ExercicioSelecionado = e.SelectedItem as Exercicio;
        var m_NovaCarga = await DisplayPromptAsync("Nova carga...", "Atualize a carga do exercício:", accept: "Salvar", cancel: "Cancelar", maxLength: 3, initialValue: m_ExercicioSelecionado.Carga.ToString());
        if (string.IsNullOrEmpty(m_NovaCarga))
            return;

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
        try
        {
            var m_Nome = await DisplayPromptAsync("Nome", "Digite o nome do exercício:") ?? throw new Exception();
            var m_Repeticao = await DisplayPromptAsync("Repetição", "Digite a repetição dos movimentos:") ?? throw new Exception();
            var m_Carga = await DisplayPromptAsync("Carga", "Digite a carga do exercício:") ?? throw new Exception();

            var m_CamposIncompletos = string.IsNullOrEmpty(m_Nome) || string.IsNullOrEmpty(m_Repeticao) || string.IsNullOrEmpty(m_Carga);
            if (m_CamposIncompletos)
                throw new ArgumentNullException();

            var m_Exercicio = new Exercicio(m_Nome, m_Repeticao, int.Parse(m_Carga));
            var m_Treino = await C_TreinoService.CM_LerTreinoAsync(C_Id);

            await C_TreinoService.CM_AdicionarExercicioAoTreinoAsync(m_Treino.Id, m_Exercicio);
        }
        catch (ArgumentNullException)
        {
            var m_Resposta = await DisplayAlert("Erro", "Campos incompletos", "Tentar novamente", "Cancelar");
            if (m_Resposta)
                Button_Clicked(sender, e);
        }
        catch (Exception)
        {
            return;
        }
        finally
        {
            OnAppearing();
        }
    }

    private async void Remover_Clicked(object sender, EventArgs e)
    {
        try
        {
            var m_Nomes = C_Exercicios.Select(a => a.Nome).ToArray();
            var m_ExercicioSelecionado = await DisplayActionSheet("Selecione o exercício a ser removido...", "Cancelar", "Sair", FlowDirection.MatchParent, m_Nomes) ?? throw new Exception();
            if (m_ExercicioSelecionado.Equals("Sair"))
                return;

            var m_Resposta = await DisplayAlert("Remover", $"Confirma a remoção do exercício: {m_ExercicioSelecionado}?", "Sim", "Não");
            if (m_Resposta == false)
                return;

            var m_Exercicio = C_Exercicios.FirstOrDefault(a => a.Nome.Equals(m_ExercicioSelecionado));
            await C_TreinoService.CM_RemoverExercicioDoTreinoAsync(C_Id, m_Exercicio);
        }
        catch (Exception)
        {
            return;
        }
        finally
        {
            OnAppearing();
        }
    }
}