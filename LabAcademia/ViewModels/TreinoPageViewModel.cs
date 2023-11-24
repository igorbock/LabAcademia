namespace LabAcademia.ViewModels;

public partial class TreinoPageViewModel : ObservableObject
{
    [ObservableProperty]
    string nome;

    [ObservableProperty]
    string carga;

    [ObservableProperty]
    int repeticao;

    [RelayCommand]
    public async Task CM_AdicionarExercicio()
    {
        var m_NovoExercicio = new Exercicio(Nome, Carga, Repeticao);
    }
}
