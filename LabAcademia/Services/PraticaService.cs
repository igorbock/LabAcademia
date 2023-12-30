namespace LabAcademia.Services;

public class PraticaService : IPraticaService
{
    public HttpClient C_HttpClient { get; set; }

    public PraticaService() { }

    public PraticaService(IHttpClientFactory p_HttpClientFactory)
    {
        C_HttpClient = p_HttpClientFactory.CreateClient("LabAcademiaAPI");
    }

    public async Task CM_ConcluirPraticaAsync(Treino p_Treino)
    {
        await cm_AtribuirTokenAoHttpClientAsync();
        var m_JSON = JsonSerializer.Serialize(p_Treino);
        var m_StringContent = new StringContent(m_JSON, System.Text.Encoding.UTF8, "application/json");
        var m_Resultado = await C_HttpClient.PostAsync("api/historicos/salvar", m_StringContent);
        m_Resultado.EnsureSuccessStatusCode();
    }

    private async Task cm_AtribuirTokenAoHttpClientAsync()
    {
        var m_Token = await SecureStorage.GetAsync("Token");
        C_HttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", m_Token);
    }
}
