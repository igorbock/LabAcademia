namespace LabAcademia.Services;

public class HistoricoService : IHistoricoService
{
    public IHttpClientFactory C_HttpClientFactory { get; private set; }
    public HttpClient C_HttpClient { get; private set; }

    public HistoricoService() { }

    public HistoricoService(IHttpClientFactory p_HttpClientFactory, IStreamHelper<Treino> p_StreamHelper, ITreinoHelper p_TreinoHelper)
    {
        C_HttpClientFactory = p_HttpClientFactory;
        C_HttpClient = C_HttpClientFactory.CreateClient("LabAcademiaAPI");
    }

    public async Task<IEnumerable<Treino>> CM_VerHistoricoAsync(DateTime? p_Inicio, DateTime? p_Fim)
    {
        await cm_AtribuirTokenAoHttpClientAsync();
        var m_Entidade = new
        {
            p_Inicio,
            p_Fim
        };
        var m_JSON = JsonSerializer.Serialize(m_Entidade);
        var m_StringContent = new StringContent(m_JSON, System.Text.Encoding.UTF8, "application/json");
        var m_Resultado = await C_HttpClient.PostAsync("api/historicos", m_StringContent);
        m_Resultado.EnsureSuccessStatusCode();

        var m_JSONRetorno = await m_Resultado.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<IEnumerable<Treino>>(m_JSONRetorno);
    }

    private async Task cm_AtribuirTokenAoHttpClientAsync()
    {
        var m_Token = await SecureStorage.GetAsync("Token");
        C_HttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", m_Token);
    }
}
