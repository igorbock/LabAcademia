namespace LabAcademia.Services;

public class ExercicioService : IExercicioService
{
    private static string c_DiretorioPrincipal = FileSystem.Current.AppDataDirectory;
    private static string c_NomeArquivo = "Treinos.json";
    private static string c_DiretorioArquivo = Path.Combine(c_DiretorioPrincipal, c_NomeArquivo);

    public ITreinoService C_TreinoService { get; set; }

    public ExercicioService(ITreinoService p_TreinoService)
    {
        C_TreinoService = p_TreinoService;
    }

    public async Task CM_AlterarCargaExercicioAsync(Exercicio p_Exercicio, int p_NovaCarga)
    {
        using var m_FileStream = File.OpenRead(c_DiretorioArquivo);
        using var m_StreamReader = new StreamReader(m_FileStream);

        var m_TreinosJSON = m_StreamReader.ReadToEnd();
        var m_Treinos = JsonSerializer.Deserialize<List<Treino>>(m_TreinosJSON);
        var m_TreinoSelecionado = m_Treinos.FirstOrDefault(a => a.Exercicios.Any(a => a.Nome.Equals(p_Exercicio.Nome)));

        var m_Exercicio = m_TreinoSelecionado.Exercicios.First(a => a.Nome == p_Exercicio.Nome) ?? throw new KeyNotFoundException();
        m_Exercicio.Carga = p_NovaCarga;

        m_TreinosJSON = JsonSerializer.Serialize(m_Treinos);

        using var m_StreamWriter = new StreamWriter(c_DiretorioArquivo, append: false, System.Text.Encoding.UTF8);
        await m_StreamWriter.WriteAsync(m_TreinosJSON);
    }
}
