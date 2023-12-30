namespace LabAcademiaBiblioteca.Extensions;

public static class JWTExtensions
{
    /// <summary>
    /// Método de extensão para validar o token e retornar o número de mátricula do aluno.
    /// </summary>
    /// <param name="p_JWTSecurityToken"></param>
    /// <returns></returns>
    /// <exception cref="UnauthorizedAccessException"></exception>
    public static string CMX_ValidarToken(this JwtSecurityToken p_JWTSecurityToken)
    {
        var m_TokenInvalido = p_JWTSecurityToken.ValidTo < DateTime.UtcNow;
        if (m_TokenInvalido)
        {
            SecureStorage.RemoveAll();
            throw new UnauthorizedAccessException();
        }
            

        var m_ClaimMatricula = p_JWTSecurityToken.Claims.Where(a => a.Type == "matricula").SingleOrDefault();
        return m_ClaimMatricula.Value;
    }
}
