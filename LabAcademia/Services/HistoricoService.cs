namespace LabAcademia.Services;

public class HistoricoService : IHistoricoService
{
    private static string c_DiretorioArquivo = FileSystemHelper.CM_ObterDiretorioLocalComArquivo("Praticas.json");

    public IStreamHelper<Treino> C_StreamHelper { get; set; }
    public ITreinoHelper C_TreinoHelper { get; set; }

    public HistoricoService() { }

    public HistoricoService(IStreamHelper<Treino> p_StreamHelper, ITreinoHelper p_TreinoHelper)
    {
        C_StreamHelper = p_StreamHelper;
        C_TreinoHelper = p_TreinoHelper;
    }

    public async Task<IEnumerable<Treino>> CM_VerHistoricoAsync() => await C_StreamHelper.CM_AbrirArquivoEObterTipoGenericoAsync(c_DiretorioArquivo);
}
