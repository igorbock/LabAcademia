namespace LabAcademiaBiblioteca.Interfaces;

public interface IStreamHelper<TipoT>
{
    Task<List<TipoT>> CM_AbrirArquivoEObterTipoGenericoAsync(string p_Diretorio);
    List<TipoT> CM_AbrirArquivoEObterTipoGenerico(string p_Diretorio);
    Task CM_SerializarESalvarNovoJsonAsync(IEnumerable<TipoT> p_Entidade, string p_Diretorio);
    void CM_SerializarESalvarNovoJson(IEnumerable<TipoT> p_Entidade, string p_Diretorio);
    Task CM_SalvarNovoArquivoCasoNaoExistaAsync(TipoT p_Entidade, string p_Diretorio);
}
