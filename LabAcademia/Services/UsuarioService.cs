namespace LabAcademia.Services;

public class UsuarioService : IUsuarioService
{
    public IHttpClientFactory C_HttpClientFactory { get; private set; }

    public UsuarioService(IHttpClientFactory p_HttpClientFactory)
    {
        C_HttpClientFactory = p_HttpClientFactory;
    }

    public async Task<string> CM_RegistrarUsuarioAsync(string p_Matricula, string p_Senha)
    {
        var m_HttpClient = C_HttpClientFactory.CreateClient("LabAspNetIdentity");
        var m_DTO = new
        {
            C_Matricula = p_Matricula,
            C_Senha = p_Senha
        };
        var m_DTOJson = JsonSerializer.Serialize(m_DTO);
        var m_StringContent = new StringContent(m_DTOJson, Encoding.UTF8, "application/json");
        var m_Resultado = await m_HttpClient.PostAsync("api/aluno/senha", m_StringContent);
        m_Resultado.EnsureSuccessStatusCode();

        return await m_Resultado.Content.ReadAsStringAsync();
    }
}
