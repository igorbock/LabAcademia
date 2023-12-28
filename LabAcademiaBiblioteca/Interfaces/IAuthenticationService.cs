namespace LabAcademiaBiblioteca.Interfaces;

public interface IAuthenticationService
{
    Task CM_LoginAsync(string p_Usuario, string p_Senha);
    Task CM_LogoutAsync();
}
