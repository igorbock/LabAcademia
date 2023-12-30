using System.Text;

namespace LabAcademia.Services;

public class AuthenticationService : IAuthenticationService
{
    public IHttpClientFactory C_HttpClientFactory { get; private set; }

    public AuthenticationService(IHttpClientFactory p_HttpClientFactory)
    {
        C_HttpClientFactory = p_HttpClientFactory;
    }

    public async Task CM_LoginAsync(string p_Usuario, string p_Senha)
    {
        var m_HttpClient = C_HttpClientFactory.CreateClient("LabAspNetIdentity");
        var m_JSON = JsonSerializer.Serialize(new { Usuario = p_Usuario, Senha = p_Senha });
        var m_Content = new StringContent(m_JSON, Encoding.UTF8, "application/json");
        var m_Resultado = await m_HttpClient.PostAsync("api/token", m_Content);
        if (m_Resultado.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            throw new UnauthorizedAccessException("Usuário ou senha incorretos!");
        m_Resultado.EnsureSuccessStatusCode();

        var m_Token = await m_Resultado.Content.ReadAsStringAsync();
        await SecureStorage.SetAsync("Token", m_Token);
    }

    public async Task CM_LogoutAsync() => await Task.FromResult(SecureStorage.Remove("Token"));
}