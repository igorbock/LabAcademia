namespace LabAcademia.Services;

public class ExercicioService : IExercicioService
{
    private static string c_DiretorioArquivo = FileSystemHelper.CM_ObterDiretorioLocalComArquivo("Treinos.json");

    public ITreinoService C_TreinoService { get; set; }
    public IStreamHelper<Treino> C_StreamHelper { get; set; }
    public IHttpClientFactory C_HttpClientFactory { get; set; }
    public HttpClient C_HttpClient { get; set; }

    public ExercicioService(
        ITreinoService p_TreinoService, 
        IStreamHelper<Treino> p_StreamHelper,
        IHttpClientFactory p_HttpClientFactory)
    {
        C_TreinoService = p_TreinoService;
        C_StreamHelper = p_StreamHelper;
        C_HttpClientFactory = p_HttpClientFactory;

        C_HttpClient = C_HttpClientFactory.CreateClient("LabAcademiaAPI");
        C_HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SecureStorage.GetAsync("Token").Result);
    }

    public async Task CM_AlterarCargaExercicioAsync(Exercicio p_Exercicio, double p_NovaCarga)
    {
        var m_DTO = new
        {
            p_CodigoExercicio = p_Exercicio.Id,
            p_Carga = p_NovaCarga
        };
        var m_JsonDTO = JsonSerializer.Serialize(m_DTO);
        var m_StringContent = new StringContent(m_JsonDTO, Encoding.UTF8, "application/json");
        var m_Resultado = await C_HttpClient.PutAsync("api/Exercicios/carga", m_StringContent);
        m_Resultado.EnsureSuccessStatusCode();

        var m_Treinos = await C_StreamHelper.CM_AbrirArquivoEObterTipoGenericoAsync(c_DiretorioArquivo);
        var m_TreinoSelecionado = m_Treinos.FirstOrDefault(a => a.Exercicios.Any(a => a.Descricao.Equals(p_Exercicio.Descricao)));

        var m_Exercicio = m_TreinoSelecionado.Exercicios.First(a => a.Descricao == p_Exercicio.Descricao) ?? throw new KeyNotFoundException();
        m_Exercicio.Carga = int.Parse(p_NovaCarga.ToString());

        await C_StreamHelper.CM_SerializarESalvarNovoJsonAsync(m_Treinos, c_DiretorioArquivo);
    }

    ~ExercicioService()
    {
        C_HttpClient?.Dispose();
    }
}
