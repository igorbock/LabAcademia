namespace LabAcademia.Services;

public class TreinoService : ITreinoService
{
    public IHttpClientFactory C_HttpClientFactory { get; private set; }
    public HttpClient C_HttpClient { get; private set; }

    public TreinoService() { }

    public TreinoService(
        IHttpClientFactory p_HttpClientFactory,
        IStreamHelper<Treino> p_StreamHelper,
        ITreinoHelper p_TreinoHelper)
    {
        C_HttpClientFactory = p_HttpClientFactory;

        C_HttpClient = C_HttpClientFactory.CreateClient("LabAcademiaAPI");
    }

    ~TreinoService()
    {
        C_HttpClient?.Dispose();
    }

    public async Task<IEnumerable<Treino>> CM_TodosTreinosAsync(string p_Matricula)
    {
        await cm_AtribuirTokenAoHttpClientAsync();
        var m_Resultado = await C_HttpClient.GetAsync($"api/alunoTreinos?p_Matricula={p_Matricula}");
        m_Resultado.EnsureSuccessStatusCode();

        var m_JSON = await m_Resultado.Content.ReadAsStringAsync();
        var m_Retorno = JsonSerializer.Deserialize<IEnumerable<Treino>>(m_JSON);
        return m_Retorno;
    }

    public async Task<IEnumerable<Exercicio>> CM_ObterExerciciosDoTreinoAsync(int p_CodigoTreino)
    {
        await cm_AtribuirTokenAoHttpClientAsync();
        var m_Resultado = await C_HttpClient.GetAsync($"api/alunoTreinos/exercicios?p_CodigoTreino={p_CodigoTreino}");
        m_Resultado.EnsureSuccessStatusCode();

        var m_JSON = await m_Resultado.Content.ReadAsStringAsync();
        var m_Retorno = JsonSerializer.Deserialize<IEnumerable<Exercicio>>(m_JSON);
        return m_Retorno;
    }

    private async Task cm_AtribuirTokenAoHttpClientAsync()
    {
        var m_Token = await SecureStorage.GetAsync("Token");
        C_HttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", m_Token);
    }
}
