namespace LabAcademia.Services;

public class TreinoService : ITreinoService
{
    private static string c_DiretorioPrincipal = FileSystem.Current.AppDataDirectory;
    private static string c_NomeArquivo = "Treinos.json";
    private static string c_DiretorioArquivo = Path.Combine(c_DiretorioPrincipal, c_NomeArquivo);

    public IStreamHelper<Treino> C_StreamHelper { get; set; }
    public ITreinoHelper C_TreinoHelper { get; set; }

    public TreinoService() { }

    public TreinoService(IStreamHelper<Treino> p_StreamHelper, ITreinoHelper p_TreinoHelper)
    {
        C_StreamHelper = p_StreamHelper;
        C_TreinoHelper = p_TreinoHelper;
    }

    public async Task CM_AdicionarExercicioAoTreinoAsync(char p_TreinoId, Exercicio p_Exercicio)
    {
        var m_Treinos = await C_StreamHelper.CM_AbrirArquivoEObterTipoGenericoAsync(c_DiretorioArquivo);
        C_TreinoHelper.CM_SelecionarTreinoEAdicionarExercicio(p_TreinoId, m_Treinos, p_Exercicio);
        await C_StreamHelper.CM_SerializarESalvarNovoJsonAsync(m_Treinos, c_DiretorioArquivo);
    }

    public async Task CM_RemoverExercicioDoTreinoAsync(char p_IdTreino, Exercicio p_Exercicio)
    {
        var m_Treinos = await C_StreamHelper.CM_AbrirArquivoEObterTipoGenericoAsync(c_DiretorioArquivo);
        C_TreinoHelper.CM_SelecionarTreinoERemoverExercicio(p_IdTreino, m_Treinos, p_Exercicio);
        await C_StreamHelper.CM_SerializarESalvarNovoJsonAsync(m_Treinos, c_DiretorioArquivo);
    }

    public async Task CM_ApagarTreinoAsync(char p_Id)
    {
        var m_Treinos = await C_StreamHelper.CM_AbrirArquivoEObterTipoGenericoAsync(c_DiretorioArquivo);
        C_TreinoHelper.CM_SelecionarTreinoERemover(p_Id, m_Treinos);
        await C_StreamHelper.CM_SerializarESalvarNovoJsonAsync(m_Treinos, c_DiretorioArquivo);
    }

    public Task CM_EditarTreinoAsync(Treino p_Treino)
    {
        //using var m_TreinosStream = await FileSystem.OpenAppPackageFileAsync(c_DiretorioArquivo);
        //var m_Treinos = JsonSerializer.Deserialize<List<Treino>>(m_TreinosStream);
        //var m_Treino = m_Treinos.First(a => a.Id == p_Treino.Id) ?? throw new KeyNotFoundException();
        //m_Treinos.Remove(m_Treino);
        //m_Treino.Nome = p_Treino.Nome;
        //m_Treinos.Add(m_Treino);
        //var m_TreinosJSON = JsonSerializer.Serialize(m_Treinos);
        //await File.WriteAllTextAsync(c_DiretorioArquivo, m_TreinosJSON);

        throw new NotImplementedException();
    }

    public async Task CM_EscreverTreinoAsync(string p_Nome)
    {
        var m_NomeVazio = string.IsNullOrEmpty(p_Nome);
        if (m_NomeVazio) throw new Exception("Nome não pode estar em branco!");
        var m_NovoTreino = new Treino(p_Nome);

        try
        {
            var m_Treinos = await C_StreamHelper.CM_AbrirArquivoEObterTipoGenericoAsync(c_DiretorioArquivo);
            m_NovoTreino.Id = C_TreinoHelper.CM_IdNextValue(m_Treinos);
            m_Treinos.Add(m_NovoTreino);
            await C_StreamHelper.CM_SerializarESalvarNovoJsonAsync(m_Treinos, c_DiretorioArquivo);
        }
        catch (FileNotFoundException)
        {
            m_NovoTreino.Id = 'A';
            await C_StreamHelper.CM_SalvarNovoArquivoCasoNaoExistaAsync(m_NovoTreino, c_DiretorioArquivo);
        }
    }

    public async Task<Treino> CM_LerTreinoAsync(char p_Id)
    {
        var m_Treinos = await C_StreamHelper.CM_AbrirArquivoEObterTipoGenericoAsync(c_DiretorioArquivo);
        return m_Treinos.First(a => a.Id == p_Id);
    }

    public Treino CM_LerTreino(char p_Id)
    {
        var m_Treinos = C_StreamHelper.CM_AbrirArquivoEObterTipoGenerico(c_DiretorioArquivo);
        return m_Treinos.First(a => a.Id == p_Id);
    }

    public IEnumerable<Treino> CM_TodosTreinos()
    {
        try
        {
            return C_StreamHelper.CM_AbrirArquivoEObterTipoGenerico(c_DiretorioArquivo);
        }
        catch (FileNotFoundException)
        {
            return new List<Treino>();
        }
    }
}
