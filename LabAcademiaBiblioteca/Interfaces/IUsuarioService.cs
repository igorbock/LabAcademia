namespace LabAcademiaBiblioteca.Interfaces;

public interface IUsuarioService
{
    Task<string> CM_RegistrarUsuarioAsync(string p_Matricula, string p_Senha);
}