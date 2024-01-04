namespace LabAcademiaBiblioteca.Interfaces;

public interface IAuthenticationService
{
    Task<bool> CM_UsuarioConectadoAsync();
    Task CM_LoginAsync(string p_Usuario, string p_Senha);
    void CM_Logout();
}