namespace LabAcademiaBiblioteca.Helpers;

public class StreamHelper<TipoT> : IStreamHelper<TipoT>
{
    public async Task<List<TipoT>> CM_AbrirArquivoEObterTipoGenericoAsync(string p_Diretorio)
    {
        using var m_FileStream = File.OpenRead(p_Diretorio);
        using var m_StreamReader = new StreamReader(m_FileStream);

        var m_EntidadeJSON = await m_StreamReader.ReadToEndAsync();
        return JsonSerializer.Deserialize<List<TipoT>>(m_EntidadeJSON);
    }

    public List<TipoT> CM_AbrirArquivoEObterTipoGenerico(string p_Diretorio)
    {
        using var m_FileStream = File.OpenRead(p_Diretorio);
        using var m_StreamReader = new StreamReader(m_FileStream);

        var m_EntidadeJSON = m_StreamReader.ReadToEnd();
        return JsonSerializer.Deserialize<List<TipoT>>(m_EntidadeJSON);
    }

    public async Task CM_SerializarESalvarNovoJsonAsync(IEnumerable<TipoT> p_Entidade, string p_Diretorio)
    {
        var m_NovoArquivoJson = JsonSerializer.Serialize(p_Entidade);

        using var m_StreamWriter = new StreamWriter(p_Diretorio, append: false, System.Text.Encoding.UTF8);
        await m_StreamWriter.WriteAsync(m_NovoArquivoJson);
    }

    public async Task CM_SalvarNovoArquivoCasoNaoExistaAsync(TipoT p_Entidade, string p_Diretorio)
    {
        using var m_FileStream = System.IO.File.OpenWrite(p_Diretorio);
        using var m_StreamWriter = new StreamWriter(m_FileStream);

        var m_Entidades = new List<TipoT>
        {
            p_Entidade
        };
        var m_EntidadesJSON = JsonSerializer.Serialize(m_Entidades);

        await m_StreamWriter.WriteAsync(m_EntidadesJSON);
    }
}
