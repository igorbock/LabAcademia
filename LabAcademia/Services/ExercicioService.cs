namespace LabAcademia.Services;

public class ExercicioService : IExercicioService
{
    private static string c_DiretorioArquivo = FileSystemHelper.CM_ObterDiretorioLocalComArquivo("Treinos.json");

    public ITreinoService C_TreinoService { get; set; }
    public IStreamHelper<Treino> C_StreamHelper { get; set; }

    public ExercicioService(ITreinoService p_TreinoService, IStreamHelper<Treino> p_StreamHelper)
    {
        C_TreinoService = p_TreinoService;
        C_StreamHelper = p_StreamHelper;
    }

    public async Task CM_AlterarCargaExercicioAsync(Exercicio p_Exercicio, int p_NovaCarga)
    {
        var m_Treinos = await C_StreamHelper.CM_AbrirArquivoEObterTipoGenericoAsync(c_DiretorioArquivo);
        var m_TreinoSelecionado = m_Treinos.FirstOrDefault(a => a.Exercicios.Any(a => a.Nome.Equals(p_Exercicio.Nome)));

        var m_Exercicio = m_TreinoSelecionado.Exercicios.First(a => a.Nome == p_Exercicio.Nome) ?? throw new KeyNotFoundException();
        m_Exercicio.Carga = p_NovaCarga;

        await C_StreamHelper.CM_SerializarESalvarNovoJsonAsync(m_Treinos, c_DiretorioArquivo);
    }
}
