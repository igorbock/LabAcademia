namespace LabAcademia.Services;

public class PraticaService : IPraticaService
{
    private static string c_DiretorioArquivo = FileSystemHelper.CM_ObterDiretorioLocalComArquivo("Praticas.json");

    public IStreamHelper<Treino> C_StreamHelper { get; set; }
    public ITreinoHelper C_TreinoHelper { get; set; }

    public PraticaService() { }

    public PraticaService(IStreamHelper<Treino> p_StreamHelper, ITreinoHelper p_TreinoHelper)
    {
        C_StreamHelper = p_StreamHelper;
        C_TreinoHelper = p_TreinoHelper;
    }

    public async Task CM_IniciarPraticaAsync(Treino p_Treino)
    {
        try
        {
            var m_Treinos = await C_StreamHelper.CM_AbrirArquivoEObterTipoGenericoAsync(c_DiretorioArquivo);
            p_Treino.Inicio = DateTime.Now;
            m_Treinos.Add(p_Treino);
            await C_StreamHelper.CM_SerializarESalvarNovoJsonAsync(m_Treinos, c_DiretorioArquivo);
        }
        catch (FileNotFoundException)
        {
            await C_StreamHelper.CM_SalvarNovoArquivoCasoNaoExistaAsync(p_Treino, c_DiretorioArquivo);
        }
    }

    public async Task CM_ConcluirPraticaAsync(Treino p_Treino)
    {
        var m_Treinos = await C_StreamHelper.CM_AbrirArquivoEObterTipoGenericoAsync(c_DiretorioArquivo);
        var m_TreinoSelecionado = m_Treinos.LastOrDefault(a => a.Id == p_Treino.Id);

        m_TreinoSelecionado.Fim = DateTime.Now;

        await C_StreamHelper.CM_SerializarESalvarNovoJsonAsync(m_Treinos, c_DiretorioArquivo);
    }

    public void CM_RemoverPratica(Treino p_Treino)
    {
        var m_Treinos = C_StreamHelper.CM_AbrirArquivoEObterTipoGenerico(c_DiretorioArquivo);
        var m_TreinoSelecionado = m_Treinos.LastOrDefault(a => a.Id == p_Treino.Id);

        m_Treinos.Remove(m_TreinoSelecionado);

        C_StreamHelper.CM_SerializarESalvarNovoJson(m_Treinos, c_DiretorioArquivo);
    }
}
