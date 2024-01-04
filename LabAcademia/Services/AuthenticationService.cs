namespace LabAcademia.Services;

public class AuthenticationService : IAuthenticationService
{
    public HttpClient C_HttpClient { get; private set; }
    public ISecureStorageService C_Storage { get; private set; }

    public AuthenticationService(IHttpClientFactory p_HttpClientFactory, ISecureStorageService p_SecureStorageService)
    {
        C_HttpClient = p_HttpClientFactory.CreateClient("LabAspNetIdentity");
        C_Storage = p_SecureStorageService;
    }

    public async Task<bool> CM_UsuarioConectadoAsync()
    {
        try
        {
            var m_Token = await C_Storage.CM_ObterAsync("Token") ?? throw new UnauthorizedAccessException();
            var m_JWT = new JwtSecurityToken(m_Token);
            var m_TokenInvalido = m_JWT.ValidTo < DateTime.UtcNow;
            if (m_TokenInvalido) throw new UnauthorizedAccessException();

            return true;
        }
        catch (UnauthorizedAccessException)
        {
            return false;
        }
    }

    public async Task CM_LoginAsync(string p_Usuario, string p_Senha)
    {
        var m_JSON = JsonSerializer.Serialize(new { Usuario = p_Usuario, Senha = p_Senha });
        var m_Content = new StringContent(m_JSON, Encoding.UTF8, "application/json");
        var m_Resultado = await C_HttpClient.PostAsync("api/token", m_Content);
        if (m_Resultado.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            throw new UnauthorizedAccessException("Usuário ou senha incorretos!");
        m_Resultado.EnsureSuccessStatusCode();

        var m_Token = await m_Resultado.Content.ReadAsStringAsync();
        await C_Storage.CM_SalvarAsync("Token", m_Token);
    }

    public void CM_Logout() => C_Storage.CM_Remover("Token");
}