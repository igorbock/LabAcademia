﻿namespace LabAcademia.ViewModels;

public partial class PraticaPageViewModel : ObservableObject
{
    //Separar as propriedades do treino para fazer o binding com a tela e o backend
    [ObservableProperty]
    private Treino _treino;

    [ObservableProperty]
    private Exercicio _exercicio;

    public IPraticaService C_PraticaService { get; private set; }

    public PraticaPageViewModel(IPraticaService p_PraticaService)
    {
        C_PraticaService = p_PraticaService;
    }

    [RelayCommand]
    public async Task CM_IniciarPraticaAsync()
    {
        var m_JSON = JsonSerializer.Serialize(Treino);
        await SecureStorage.SetAsync("Treino", m_JSON);
        Treino.Exercicios.ForEach(a => a.Concluido = false);
    }

    [RelayCommand]
    public async Task CM_ExercicioSelecionadoAsync(Exercicio p_Exercicio)
    {
        try
        {
            var m_NovaCarga = await Application.Current.MainPage.DisplayPromptAsync("Nova carga...", "Atualize a carga do exercício:", accept: "Salvar", cancel: "Cancelar", maxLength: 3, initialValue: p_Exercicio.Carga.ToString());
            if (m_NovaCarga == null)
                return;

            if (double.TryParse(m_NovaCarga, out double m_Carga) == false)
                throw new Exception("Use somente números na carga...");

            Treino.Exercicios.Find(a => a.Id == p_Exercicio.Id).Carga = m_Carga;
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "Voltar");
        }
        finally
        {
            Exercicio = null;
        }
    }

    [RelayCommand]
    public async Task CM_ConcluirTreinoAsync()
    {
        var m_ConcluirTreino = true;
        foreach (var item in Treino.Exercicios)
        {
            if (item.Concluido == false)
            {
                m_ConcluirTreino = await Application.Current.MainPage.DisplayAlert("Alerta", "Existem exercícios não concluídos! Você deseja encerrrar o treino?", accept: "Sim", cancel: "Não");
                break;
            }
        }

        if (m_ConcluirTreino)
        {
            Treino.Fim = DateTime.UtcNow;
            await C_PraticaService.CM_ConcluirPraticaAsync(Treino);
            SecureStorage.Remove("Treino");
            Treino = null;
            await Shell.Current.GoToAsync(nameof(HomePage));
        }
    }
}
